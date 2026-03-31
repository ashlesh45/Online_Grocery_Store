using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace Online_Grocery_Store
{
    public partial class DashboardForm : Form
    {
        private string imageDirectory = @"D:\Project\Images\"; // Change this to your actual image folder

        public DashboardForm()
        {
            InitializeComponent();
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            LoadCategories();
        }

        private void LoadCategories()
        {
            try
            {
                using (OracleConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT CategoryName FROM Categories";
                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        cmbCategories.Items.Clear();
                        while (reader.Read())
                        {
                            cmbCategories.Items.Add(reader.GetString(0));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading categories: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Image LoadProductImage(string imagePath)
        {
            try
            {
                if (!string.IsNullOrEmpty(imagePath))
                {
                    imagePath = imagePath.Replace("\\\\", "\\"); // Fix slashes

                    Console.WriteLine("Checking Image Path: " + imagePath);

                    if (File.Exists(imagePath))
                    {
                        Console.WriteLine("Image Found: " + imagePath);
                        return Image.FromFile(imagePath);
                    }
                    else
                    {
                        Console.WriteLine("Image Not Found: " + imagePath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading image: " + ex.Message, "Image Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Load default image
            return Image.FromFile("D:\\Project\\default.jpg");
        }




        private Panel CreateProductCard(string name, decimal price, int stock, string imagePath, int productId)
        {
            Panel panel = new Panel
            {
                Size = new Size(150, 220),
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10)
            };

            PictureBox pictureBox = new PictureBox
            {
                Size = new Size(140, 140),
                Location = new Point(5, 5),
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            // Load image with fallback to default
            Image productImage = LoadProductImage(imagePath);
            if (productImage == null)
            {
                productImage = LoadProductImage("D:\\Project\\FruitsandVeg\\default.jpg");
            }
            pictureBox.Image = productImage;

            Label lblName = new Label
            {
                Text = name,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(5, 150),
                Size = new Size(140, 20)
            };

            Label lblPrice = new Label
            {
                Text = "₹" + price.ToString("0.00"),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 10, FontStyle.Bold),
                Location = new Point(5, 170),
                Size = new Size(140, 20)
            };

            Button btnAddToCart = new Button
            {
                Text = "Add to Cart",
                Size = new Size(140, 30),
                Location = new Point(5, 190)
            };
            btnAddToCart.Click += (sender, e) => AddToCart(productId);
            panel.Controls.Add(pictureBox);
            panel.Controls.Add(lblName);
            panel.Controls.Add(lblPrice);
            panel.Controls.Add(btnAddToCart);

            return panel;
        }
        private int GetCurrentUserId()
        {
            return SessionManager.LoggedInCustomerId;
        }
        private void AddToCart(int productId)
        {
            int customerId = GetCurrentUserId(); // Implement this to get the logged-in user

            using (OracleConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    // Check if the product is already in the cart
                    string checkQuery = "SELECT QUANTITY FROM Cart WHERE CUSTOMERID = :customerId AND PRODUCTID = :productId";
                    using (OracleCommand checkCmd = new OracleCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.Add(":customerId", OracleDbType.Int32).Value = customerId;
                        checkCmd.Parameters.Add(":productId", OracleDbType.Int32).Value = productId;

                        object result = checkCmd.ExecuteScalar();

                        if (result != null) // Product exists, update quantity
                        {
                            int existingQuantity = Convert.ToInt32(result);
                            string updateQuery = "UPDATE Cart SET QUANTITY = QUANTITY + 1 WHERE CUSTOMERID = :customerId AND PRODUCTID = :productId";

                            using (OracleCommand updateCmd = new OracleCommand(updateQuery, conn))
                            {
                                updateCmd.Parameters.Add(":customerId", OracleDbType.Int32).Value = customerId;
                                updateCmd.Parameters.Add(":productId", OracleDbType.Int32).Value = productId;
                                updateCmd.ExecuteNonQuery();
                            }
                        }
                        else // Product not in cart, insert new row
                        {
                            string insertQuery = "INSERT INTO Cart (CARTID, CUSTOMERID, PRODUCTID, QUANTITY) VALUES (CartSeq.NEXTVAL, :customerId, :productId, 1)";

                            using (OracleCommand insertCmd = new OracleCommand(insertQuery, conn))
                            {
                                insertCmd.Parameters.Add(":customerId", OracleDbType.Int32).Value = customerId;
                                insertCmd.Parameters.Add(":productId", OracleDbType.Int32).Value = productId;
                                insertCmd.ExecuteNonQuery();
                            }
                        }
                    }

                    MessageBox.Show("Product added to cart!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding to cart: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void cmbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCategories.SelectedItem != null)
            {
                string selectedCategory = cmbCategories.SelectedItem.ToString();
                LoadProductsByCategory(selectedCategory);
            }
        }

        private void LoadProductsByCategory(string category)
        {
            try
            {
                using (OracleConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT ProductID, Name, Price, StockQuantity, ImagePath 
                             FROM Products 
                             WHERE CategoryID = (SELECT CategoryID FROM Categories WHERE CategoryName = :CategoryName)";

                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add(":CategoryName", OracleDbType.Varchar2).Value = category;

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            flowLayoutPanelProducts.Controls.Clear(); // Clear previous products

                            while (reader.Read())
                            {
                                int productId = reader.GetInt32(0);   // Retrieve ProductID
                                string productName = reader.GetString(1);
                                decimal price = reader.GetDecimal(2);
                                int stock = reader.GetInt32(3);
                                string imagePath = reader.IsDBNull(4) ? "" : reader.GetString(4);

                                // Debug: Print the retrieved data
                                Console.WriteLine($"Product: {productName}, ID: {productId}, Image Path: {imagePath}");

                                // Pass productId to CreateProductCard
                                Panel productPanel = CreateProductCard(productName, price, stock, imagePath, productId);
                                if (productPanel != null)
                                {
                                    flowLayoutPanelProducts.Controls.Add(productPanel);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading products: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();
            if (!string.IsNullOrEmpty(searchText))
            {
                SearchProducts(searchText);
            }
        }

        private void btnCart_Click(object sender, EventArgs e)
        {
            CartForm cartForm = new CartForm();
            cartForm.Show();
        }

        private void btnMyOrders_Click(object sender, EventArgs e)
        {
            MyOrdersForm ordersForm = new MyOrdersForm();
            ordersForm.Show();
        }

        private void SearchProducts(string searchText)
        {
            try
            {
                using (OracleConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT PRODUCTID, Name, Price, StockQuantity, ImagePath 
                             FROM Products 
                             WHERE LOWER(Name) LIKE '%' || LOWER(:SearchTerm) || '%'";

                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add(":SearchTerm", OracleDbType.Varchar2).Value = searchText.ToLower();

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            flowLayoutPanelProducts.Controls.Clear(); // Clear previous search results

                            while (reader.Read())
                            {
                                int productId = reader.GetInt32(0);
                                string productName = reader.GetString(1);
                                decimal price = reader.GetDecimal(2);
                                int stock = reader.GetInt32(3);
                                string imagePath = reader.IsDBNull(4) ? "default.jpg" : reader.GetString(4);

                                // Pass productId to CreateProductCard
                                Panel productPanel = CreateProductCard(productName, price, stock, imagePath, productId);
                                if (productPanel != null)
                                {
                                    flowLayoutPanelProducts.Controls.Add(productPanel);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching products: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
