using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace Online_Grocery_Store
{
    public partial class CartForm : Form
    {
        private Label lblTotalPrice;

        public CartForm()
        {
            InitializeComponent();
            InitializeTotalPriceLabel();
        }

        private void CartForm_Load(object sender, EventArgs e)
        {
            LoadCartItems(); // ✅ Load cart items when the form loads
        }

        private void InitializeTotalPriceLabel()
        {
            lblTotalPrice = new Label
            {
                Text = "Total: ₹0.00",
                Location = new Point(20, 450),
                Font = new Font("Arial", 12, FontStyle.Bold),
                AutoSize = true
            };

            this.Controls.Add(lblTotalPrice);
        }

        private void LoadCartItems()
        {
            int customerId = SessionManager.LoggedInCustomerId;
            decimal totalPrice = 0; // ✅ Reset total price

            flowLayoutPanelCart.Controls.Clear(); // Clear existing items

            using (OracleConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT P.ProductID, P.Name, P.Price, C.QUANTITY, P.ImagePath
                        FROM Cart C
                        JOIN Products P ON C.PRODUCTID = P.PRODUCTID
                        WHERE C.CUSTOMERID = :CustomerId";

                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add(":CustomerId", OracleDbType.Int32).Value = customerId;

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int productId = reader.GetInt32(0);
                                string productName = reader.GetString(1);
                                decimal price = reader.GetDecimal(2);
                                int quantity = reader.GetInt32(3);
                                string imagePath = reader.IsDBNull(4) ? "default.jpg" : reader.GetString(4);

                                decimal itemTotal = price * quantity; // ✅ Calculate total price per item
                                totalPrice += itemTotal; // ✅ Sum up total price

                                Panel itemPanel = CreateCartItemPanel(productId, productName, price, quantity, imagePath);
                                flowLayoutPanelCart.Controls.Add(itemPanel);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading cart items: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            lblTotalPrice.Text = "Total: ₹" + totalPrice.ToString("0.00"); // ✅ Display total price
        }

        private Panel CreateCartItemPanel(int productId, string name, decimal price, int quantity, string imagePath)
        {
            Panel panel = new Panel
            {
                Size = new Size(400, 80),
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(5)
            };

            PictureBox pictureBox = new PictureBox
            {
                Size = new Size(60, 60),
                Location = new Point(5, 10),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = LoadProductImage(imagePath)
            };

            Label lblName = new Label
            {
                Text = name,
                Location = new Point(75, 10),
                Size = new Size(150, 20)
            };

            Label lblPrice = new Label
            {
                Text = "₹" + price.ToString("0.00"),
                Location = new Point(75, 35),
                Size = new Size(100, 20),
                Font = new Font("Arial", 10, FontStyle.Bold)
            };

            Label lblQuantity = new Label
            {
                Text = quantity.ToString(),
                Location = new Point(250, 25),
                Size = new Size(30, 25),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Button btnPlus = new Button
            {
                Text = "+",
                Location = new Point(280, 25),
                Size = new Size(30, 25)
            };
            btnPlus.Click += (sender, e) => UpdateCartQuantity(productId, quantity + 1);

            Button btnMinus = new Button
            {
                Text = "-",
                Location = new Point(215, 25),
                Size = new Size(30, 25)
            };
            btnMinus.Click += (sender, e) =>
            {
                if (quantity > 1)
                {
                    UpdateCartQuantity(productId, quantity - 1);
                }
                else
                {
                    RemoveFromCart(productId);
                }
            };

            panel.Controls.Add(pictureBox);
            panel.Controls.Add(lblName);
            panel.Controls.Add(lblPrice);
            panel.Controls.Add(lblQuantity);
            panel.Controls.Add(btnPlus);
            panel.Controls.Add(btnMinus);

            return panel;
        }

        private void UpdateCartQuantity(int productId, int newQuantity)
        {
            int customerId = SessionManager.LoggedInCustomerId;

            using (OracleConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Cart SET QUANTITY = :NewQuantity WHERE CUSTOMERID = :CustomerId AND PRODUCTID = :ProductId";

                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add(":NewQuantity", OracleDbType.Int32).Value = newQuantity;
                        cmd.Parameters.Add(":CustomerId", OracleDbType.Int32).Value = customerId;
                        cmd.Parameters.Add(":ProductId", OracleDbType.Int32).Value = productId;

                        cmd.ExecuteNonQuery();
                    }

                    LoadCartItems(); // ✅ Refresh UI and update total price
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating cart quantity: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void RemoveFromCart(int productId)
        {
            int customerId = SessionManager.LoggedInCustomerId;

            using (OracleConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM Cart WHERE CUSTOMERID = :CustomerId AND PRODUCTID = :ProductId";

                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add(":CustomerId", OracleDbType.Int32).Value = customerId;
                        cmd.Parameters.Add(":ProductId", OracleDbType.Int32).Value = productId;

                        cmd.ExecuteNonQuery();
                    }

                    LoadCartItems(); // ✅ Refresh UI and update total price
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error removing item from cart: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private Image LoadProductImage(string imagePath)
        {
            try
            {
                if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
                {
                    return Image.FromFile(imagePath);
                }
            }
            catch
            {
                // Ignore error and return default image
            }
            return Image.FromFile("D:\\Project\\default.jpg");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            int customerId = SessionManager.LoggedInCustomerId;
            List<CartItem> cartItems = new List<CartItem>();
            decimal totalPrice = 0;
            string customerAddress = "";
            string customerPhone = "";

            using (OracleConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    // Get cart items
                    string cartQuery = @"
                SELECT P.ProductID, P.Name, P.Price, C.QUANTITY
                FROM Cart C
                JOIN Products P ON C.PRODUCTID = P.PRODUCTID
                WHERE C.CUSTOMERID = :CustomerId";

                    using (OracleCommand cmd = new OracleCommand(cartQuery, conn))
                    {
                        cmd.Parameters.Add(":CustomerId", OracleDbType.Int32).Value = customerId;

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int productId = reader.GetInt32(0);
                                string productName = reader.GetString(1);
                                decimal price = reader.GetDecimal(2);
                                int quantity = reader.GetInt32(3);

                                decimal itemTotal = price * quantity;
                                totalPrice += itemTotal;

                                cartItems.Add(new CartItem(productId, productName, quantity, price));
                            }
                        }
                    }

                    // Get customer address & phone number
                    string customerQuery = "SELECT ADDRESS, PHONE FROM Customers WHERE CUSTOMERID = :CustomerId";
                    using (OracleCommand cmd = new OracleCommand(customerQuery, conn))
                    {
                        cmd.Parameters.Add(":CustomerId", OracleDbType.Int32).Value = customerId;

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                customerAddress = reader.IsDBNull(0) ? "" : reader.GetString(0);
                                customerPhone = reader.IsDBNull(1) ? "" : reader.GetString(1);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching cart data: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // If cart is empty, show error and stop
            if (cartItems.Count == 0)
            {
                MessageBox.Show("Your cart is empty!", "Checkout Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Open CheckoutForm with correct parameters
            CheckoutForm checkoutForm = new CheckoutForm(cartItems, totalPrice, customerAddress, customerPhone);
            checkoutForm.ShowDialog();
        }

        private void lblTotalAmount_Click(object sender, EventArgs e)
        {

        }
    }
}