using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace Online_Grocery_Store
{
    public partial class AddProductForm : Form
    {
        private string connectionString = "User Id=system; Password=student; Data Source=localhost:1521/xe;";

        public AddProductForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Input validation
            if (!int.TryParse(txtProductID.Text, out int productId))
            {
                MessageBox.Show("Please enter a valid Product ID");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Please enter a Product Name");
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price))
            {
                MessageBox.Show("Please enter a valid Price");
                return;
            }

            if (!int.TryParse(txtStock.Text, out int stock))
            {
                MessageBox.Show("Please enter valid Stock Quantity");
                return;
            }

            string imagePath = string.IsNullOrWhiteSpace(txtImagePath.Text) ? null : txtImagePath.Text;
            int categoryId = (int)cmbCategory.SelectedValue;
            int supplierId = (int)cmbSupplier.SelectedValue;

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    OracleTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        // Check if product exists
                        string checkQuery = "SELECT COUNT(*) FROM Products WHERE PRODUCTID = :productId";
                        using (OracleCommand checkCmd = new OracleCommand(checkQuery, conn))
                        {
                            checkCmd.Transaction = transaction;
                            checkCmd.Parameters.Add("productId", OracleDbType.Int32).Value = productId;
                            int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                            if (count > 0)
                            {
                                MessageBox.Show("Product ID already exists");
                                return;
                            }
                        }

                        // Insert product (Inventory table removed)
                        string insertProductQuery = @"INSERT INTO Products 
                                                  (PRODUCTID, NAME, CATEGORYID, SUPPLIERID, PRICE, STOCKQUANTITY, IMAGEPATH) 
                                                  VALUES 
                                                  (:productId, :productName, :categoryId, :supplierId, :price, :stockQuantity, :imagePath)";

                        using (OracleCommand cmd = new OracleCommand(insertProductQuery, conn))
                        {
                            cmd.Transaction = transaction;
                            cmd.Parameters.Add("productId", OracleDbType.Int32).Value = productId;
                            cmd.Parameters.Add("productName", OracleDbType.Varchar2).Value = txtProductName.Text;
                            cmd.Parameters.Add("categoryId", OracleDbType.Int32).Value = categoryId;
                            cmd.Parameters.Add("supplierId", OracleDbType.Int32).Value = supplierId;
                            cmd.Parameters.Add("price", OracleDbType.Decimal).Value = price;
                            cmd.Parameters.Add("stockQuantity", OracleDbType.Int32).Value = stock;
                            cmd.Parameters.Add("imagePath", OracleDbType.Varchar2).Value = imagePath ?? (object)DBNull.Value;

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected == 0)
                            {
                                throw new Exception("Failed to insert product");
                            }
                        }

                        transaction.Commit();
                        MessageBox.Show("Product added successfully!");
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Error saving product: " + ex.Message);
                    }
                }
                catch (OracleException ex)
                {
                    MessageBox.Show("Database error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void AddProductForm_Load(object sender, EventArgs e)
        {
            LoadCategories();
            LoadSuppliers();
        }

        private void LoadCategories()
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT CATEGORYID, CATEGORYNAME FROM Categories ORDER BY CATEGORYNAME";
                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        Dictionary<int, string> categories = new Dictionary<int, string>();
                        while (reader.Read())
                        {
                            categories.Add(reader.GetInt32(0), reader.GetString(1));
                        }
                        cmbCategory.DataSource = new BindingSource(categories, null);
                        cmbCategory.DisplayMember = "Value";
                        cmbCategory.ValueMember = "Key";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading categories: " + ex.Message);
            }
        }

        private void LoadSuppliers()
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT SUPPLIERID, SUPPLIERNAME FROM Suppliers ORDER BY SUPPLIERNAME";
                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        Dictionary<int, string> suppliers = new Dictionary<int, string>();
                        while (reader.Read())
                        {
                            suppliers.Add(reader.GetInt32(0), reader.GetString(1));
                        }
                        cmbSupplier.DataSource = new BindingSource(suppliers, null);
                        cmbSupplier.DisplayMember = "Value";
                        cmbSupplier.ValueMember = "Key";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading suppliers: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}