using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
namespace Online_Grocery_Store
{
    public partial class ViewOrdersForm : Form
    {
        private string connectionString = "User Id=system; Password=student; Data Source=localhost:1521/xe;";
        public ViewOrdersForm()
        {
            InitializeComponent();
            LoadOrders();
        }

        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an order to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedStatus = cmbStatus.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedStatus))
            {
                MessageBox.Show("Please select a status.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int orderId = Convert.ToInt32(dgvOrders.SelectedRows[0].Cells["ORDERID"].Value);

            string updateQuery = "UPDATE Orders SET STATUS = :status WHERE ORDERID = :orderId";

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (OracleCommand cmd = new OracleCommand(updateQuery, conn))
                    {
                        cmd.Parameters.Add(":status", selectedStatus);
                        cmd.Parameters.Add(":orderId", orderId);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Order status updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadOrders(); // Refresh data
                        }
                        else
                        {
                            MessageBox.Show("Failed to update status.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating order status: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadOrders()
        {
            string query = @"
                SELECT 
                    o.ORDERID, 
                    c.NAME AS CUSTOMERNAME, 
                    o.ORDERDATE, 
                    o.TOTALAMOUNT, 
                    o.STATUS,
                    od.PRODUCTID, 
                    p.NAME AS PRODUCTNAME, 
                    od.QUANTITY, 
                    od.PRICE 
                FROM Orders o
                JOIN Customers c ON o.CUSTOMERID = c.CUSTOMERID
                JOIN OrderDetails od ON o.ORDERID = od.ORDERID
                JOIN Products p ON od.PRODUCTID = p.PRODUCTID
                ORDER BY o.ORDERID DESC";
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    OracleDataAdapter adapter = new OracleDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvOrders.DataSource = dt;
                    // Set column headers
                    dgvOrders.Columns["ORDERID"].HeaderText = "Order ID";
                    dgvOrders.Columns["CUSTOMERNAME"].HeaderText = "Customer";
                    dgvOrders.Columns["ORDERDATE"].HeaderText = "Order Date";
                    dgvOrders.Columns["TOTALAMOUNT"].HeaderText = "Total (Rs)";
                    dgvOrders.Columns["STATUS"].HeaderText = "Status";
                    dgvOrders.Columns["PRODUCTID"].HeaderText = "Product ID";
                    dgvOrders.Columns["PRODUCTNAME"].HeaderText = "Product Name";
                    dgvOrders.Columns["QUANTITY"].HeaderText = "Quantity";
                    dgvOrders.Columns["PRICE"].HeaderText = "Price (Rs)";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading orders: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

