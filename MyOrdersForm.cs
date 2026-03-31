using System;
using System.Data;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace Online_Grocery_Store
{
    public partial class MyOrdersForm : Form
    {
        private readonly int customerId;

        public MyOrdersForm()
        {
            InitializeComponent();
            customerId = SessionManager.LoggedInCustomerId;
            ConfigureDataGridView();
        }

        private void ConfigureDataGridView()
        {
            // Configure main orders grid
            dgvOrders.AutoGenerateColumns = false;
            dgvOrders.Columns.Clear(); // Ensure no duplicate columns

            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "OrderID", HeaderText = "Order #", DataPropertyName = "OrderID" });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "OrderDate", HeaderText = "Date", DataPropertyName = "OrderDate" });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "TotalAmount", HeaderText = "Total", DataPropertyName = "TotalAmount", DefaultCellStyle = { Format = "C2" } });
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn { Name = "Status", HeaderText = "Status", DataPropertyName = "Status" });

            // Configure order items grid
            dgvOrderItems.AutoGenerateColumns = false;
            dgvOrderItems.Columns.Clear();

            dgvOrderItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "ProductName", HeaderText = "Product", DataPropertyName = "ProductName" });
            dgvOrderItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Quantity", HeaderText = "Qty", DataPropertyName = "Quantity" });
            dgvOrderItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Price", HeaderText = "Unit Price", DataPropertyName = "Price", DefaultCellStyle = { Format = "C2" } });
        }

        private void MyOrdersForm_Load(object sender, EventArgs e)
        {
            LoadOrders();
        }

        private void LoadOrders()
        {
            try
            {
                using (OracleConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    string query = @"
                        SELECT OrderID, OrderDate, TotalAmount, Status 
                        FROM Orders 
                        WHERE CustomerID = :customerId
                        ORDER BY OrderDate DESC";

                    OracleCommand cmd = new OracleCommand(query, conn);
                    cmd.Parameters.Add("customerId", OracleDbType.Int32).Value = customerId;

                    DataTable dt = new DataTable();
                    new OracleDataAdapter(cmd).Fill(dt);

                    dgvOrders.DataSource = dt;

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("You have no orders yet.", "Information",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading orders: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvOrders_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count > 0 && dgvOrders.SelectedRows[0].Cells["OrderID"].Value != DBNull.Value)
            {
                int orderId = Convert.ToInt32(dgvOrders.SelectedRows[0].Cells["OrderID"].Value);
                LoadOrderItems(orderId);
            }
        }

        private void LoadOrderItems(int orderId)
        {
            dgvOrderItems.DataSource = null; // Clear previous data

            try
            {
                using (OracleConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    string query = @"
                        SELECT p.Name AS ProductName, od.Quantity, od.Price
                        FROM OrderDetails od
                        JOIN Products p ON od.ProductID = p.ProductID
                        WHERE od.OrderID = :orderId";

                    OracleCommand cmd = new OracleCommand(query, conn);
                    cmd.Parameters.Add("orderId", OracleDbType.Int32).Value = orderId;

                    DataTable dt = new DataTable();
                    new OracleDataAdapter(cmd).Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No items found for this order.", "Information",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    dgvOrderItems.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading order items: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelOrder_Click(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an order to cancel.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int orderId = Convert.ToInt32(dgvOrders.SelectedRows[0].Cells["OrderID"].Value);
            string status = dgvOrders.SelectedRows[0].Cells["Status"].Value.ToString();

            if (status != "Pending")
            {
                MessageBox.Show("Only pending orders can be cancelled.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are you sure you want to cancel this order?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (OracleConnection conn = DatabaseHelper.GetConnection())
                    {
                        conn.Open();
                        using (OracleTransaction transaction = conn.BeginTransaction())
                        {
                            try
                            {
                                // First delete payments associated with the order
                                string deletePayments = "DELETE FROM Payments WHERE OrderID = :orderId";
                                OracleCommand cmdPayments = new OracleCommand(deletePayments, conn);
                                cmdPayments.Parameters.Add("orderId", OracleDbType.Int32).Value = orderId;
                                cmdPayments.ExecuteNonQuery();

                                // Then delete order items
                                string deleteItems = "DELETE FROM OrderDetails WHERE OrderID = :orderId";
                                OracleCommand cmdItems = new OracleCommand(deleteItems, conn);
                                cmdItems.Parameters.Add("orderId", OracleDbType.Int32).Value = orderId;
                                cmdItems.ExecuteNonQuery();

                                // Finally delete the order
                                string deleteOrder = "DELETE FROM Orders WHERE OrderID = :orderId";
                                OracleCommand cmdOrder = new OracleCommand(deleteOrder, conn);
                                cmdOrder.Parameters.Add("orderId", OracleDbType.Int32).Value = orderId;
                                cmdOrder.ExecuteNonQuery();

                                transaction.Commit();
                                MessageBox.Show("Order cancelled successfully.", "Success",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadOrders(); // Refresh the list
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                throw ex; // Re-throw to be caught by outer catch block
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error cancelling order: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}