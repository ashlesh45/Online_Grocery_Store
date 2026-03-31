using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;


namespace Online_Grocery_Store
{
    public partial class AdminDashboardForm : Form
    {
        string connectionString = "User Id=system; Password=student; Data Source=localhost:1521/xe;";

        public AdminDashboardForm()
        {
            InitializeComponent();
        }
        private void LoadInventoryData()
        {
            string connectionString = "User Id=system; Password=student; Data Source=localhost:1521/xe;";
            string query = @"SELECT 
                        p.PRODUCTID, 
                        p.NAME, 
                        c.CATEGORYNAME, 
                        s.SUPPLIERNAME, 
                        p.PRICE, 
                        p.STOCKQUANTITY
                    FROM Products p
                    JOIN Categories c ON p.CATEGORYID = c.CATEGORYID
                    JOIN Suppliers s ON p.SUPPLIERID = s.SUPPLIERID";

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    OracleDataAdapter adapter = new OracleDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Bind DataTable to DataGridView
                    dgvInventory.DataSource = dt;

                    // Set Column Widths
                    dgvInventory.Columns["PRODUCTID"].Width = 50;
                    dgvInventory.Columns["NAME"].Width = 150;
                    dgvInventory.Columns["CATEGORYNAME"].Width = 100;
                    dgvInventory.Columns["SUPPLIERNAME"].Width = 100;
                    dgvInventory.Columns["PRICE"].Width = 80;
                    dgvInventory.Columns["STOCKQUANTITY"].Width = 80;

                    // Set Column Headers
                    dgvInventory.Columns["PRODUCTID"].HeaderText = "ID";
                    dgvInventory.Columns["NAME"].HeaderText = "Product Name";
                    dgvInventory.Columns["CATEGORYNAME"].HeaderText = "Category";
                    dgvInventory.Columns["SUPPLIERNAME"].HeaderText = "Supplier";
                    dgvInventory.Columns["PRICE"].HeaderText = "Price (Rs)";
                    dgvInventory.Columns["STOCKQUANTITY"].HeaderText = "Stock";

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading inventory: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void AdminDashboardForm_Load(object sender, EventArgs e)
        {
            LoadInventoryData();
        }

        private void btnUpdateStock_Click(object sender, EventArgs e)
        {
            
        }

        private void btnRemoveProduct_Click(object sender, EventArgs e)
        {
            if (dgvInventory.SelectedRows.Count > 0)
            {
                int productId = Convert.ToInt32(dgvInventory.SelectedRows[0].Cells["ProductID"].Value);
                string productName = dgvInventory.SelectedRows[0].Cells["Name"].Value.ToString();

                DialogResult result = MessageBox.Show($"Are you sure you want to remove '{productName}'?",
                                                      "Confirm Removal", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    using (OracleConnection conn = new OracleConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();

                            // First, delete from Inventory (foreign key reference)
                            string deleteInventoryQuery = "DELETE FROM Inventory WHERE ProductID = :productId";
                            using (OracleCommand cmdInventory = new OracleCommand(deleteInventoryQuery, conn))
                            {
                                cmdInventory.Parameters.Add(":productId", productId);
                                cmdInventory.ExecuteNonQuery();
                            }

                            // Then, delete from Products
                            string deleteProductQuery = "DELETE FROM Products WHERE ProductID = :productId";
                            using (OracleCommand cmdProduct = new OracleCommand(deleteProductQuery, conn))
                            {
                                cmdProduct.Parameters.Add(":productId", productId);
                                int rowsAffected = cmdProduct.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Product removed successfully!");
                                    LoadInventoryData(); // Refresh DataGridView
                                }
                                else
                                {
                                    MessageBox.Show("Removal failed!");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error removing product: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a product to remove.");
            }
        }


        private void flowButtons_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            AddProductForm addProductForm = new AddProductForm();
            addProductForm.ShowDialog();
            LoadInventoryData(); // Refresh the DataGridView after adding a product
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            
        }
        private void btnViewOrders_Click(object sender, EventArgs e)
        {
            ViewOrdersForm ordersForm = new ViewOrdersForm();
            ordersForm.ShowDialog();
        }

        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            if (dgvInventory.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product to edit.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedProductId = Convert.ToInt32(dgvInventory.SelectedRows[0].Cells["ProductID"].Value);
            string selectedProductName = dgvInventory.SelectedRows[0].Cells["Name"].Value.ToString();
            decimal selectedPrice = Convert.ToDecimal(dgvInventory.SelectedRows[0].Cells["Price"].Value);
            int selectedStock = Convert.ToInt32(dgvInventory.SelectedRows[0].Cells["StockQuantity"].Value);

            EditProductForm editForm = new EditProductForm(selectedProductId, selectedProductName, selectedPrice, selectedStock);
            editForm.ShowDialog();

            LoadInventoryData(); // Refresh after editing
        }
    }
}
