using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace Online_Grocery_Store
{
    public partial class CheckoutForm : Form
    {
        private List<CartItem> cartItems;
        private decimal totalPrice;
        private int customerId;

        public CheckoutForm(List<CartItem> cartItems, decimal totalPrice, string customerAddress, string customerPhone)
        {
            InitializeComponent();
            this.cartItems = cartItems;
            this.totalPrice = totalPrice;
            this.customerId = SessionManager.LoggedInCustomerId;

            txtAddress.Text = customerAddress;
            txtPhone.Text = customerPhone;

            LoadCartSummary();
        }

        private void LoadCartSummary()
        {
            lvOrderSummary.Items.Clear();
            foreach (var item in cartItems)
            {
                ListViewItem listItem = new ListViewItem(item.ProductName);
                listItem.SubItems.Add(item.Quantity.ToString());
                listItem.SubItems.Add("₹" + item.Price);
                lvOrderSummary.Items.Add(listItem);
            }
            lblTotalPrice.Text = "Total: ₹" + totalPrice.ToString("0.00");
        }

        private void btnConfirmOrder_Click(object sender, EventArgs e)
        {
            string address = txtAddress.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string paymentMethod = rbUPI.Checked ? "UPI" : "Cash on Delivery";

            if (string.IsNullOrEmpty(address) || string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Please enter a valid address and phone number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (OracleConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                using (OracleTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // **INSERT ORDER**
                        string insertOrder = "INSERT INTO Orders (OrderID, CustomerID, OrderDate, TotalAmount, Status) " +
                                             "VALUES (OrderSeq.NEXTVAL, :customerId, SYSDATE, :totalAmount, 'Pending')";

                        using (OracleCommand cmd = new OracleCommand(insertOrder, conn))
                        {
                            cmd.Transaction = transaction;
                            cmd.Parameters.Add(":customerId", OracleDbType.Int32).Value = customerId;
                            cmd.Parameters.Add(":totalAmount", OracleDbType.Decimal).Value = totalPrice;
                            cmd.ExecuteNonQuery();
                        }

                        // **FETCH THE NEWLY CREATED ORDER ID**
                        int orderId;
                        string getOrderId = "SELECT OrderSeq.CURRVAL FROM DUAL";
                        using (OracleCommand cmd = new OracleCommand(getOrderId, conn))
                        {
                            cmd.Transaction = transaction;
                            orderId = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // **INSERT ORDER DETAILS**
                        foreach (var item in cartItems)
                        {
                            string insertOrderDetails = "INSERT INTO OrderDetails (OrderDetailID, OrderID, ProductID, Quantity, Price) " +
                                                        "VALUES (OrderDetailSeq.NEXTVAL, :orderId, :productId, :quantity, :price)";

                            using (OracleCommand cmd = new OracleCommand(insertOrderDetails, conn))
                            {
                                cmd.Transaction = transaction;
                                cmd.Parameters.Add(":orderId", OracleDbType.Int32).Value = orderId;
                                cmd.Parameters.Add(":productId", OracleDbType.Int32).Value = item.ProductId;
                                cmd.Parameters.Add(":quantity", OracleDbType.Int32).Value = item.Quantity;
                                cmd.Parameters.Add(":price", OracleDbType.Decimal).Value = item.Price;
                                cmd.ExecuteNonQuery();
                            }
                        }

                        // **INSERT PAYMENT**
                        string insertPayment = "INSERT INTO Payments (PaymentID, OrderID, PaymentMethod, AmountPaid, PaymentDate) " +
                                               "VALUES (PaymentSeq.NEXTVAL, :orderId, :paymentMethod, :amountPaid, SYSDATE)";

                        using (OracleCommand cmd = new OracleCommand(insertPayment, conn))
                        {
                            cmd.Transaction = transaction;
                            cmd.Parameters.Add(":orderId", OracleDbType.Int32).Value = orderId;
                            cmd.Parameters.Add(":paymentMethod", OracleDbType.Varchar2).Value = paymentMethod;
                            cmd.Parameters.Add(":amountPaid", OracleDbType.Decimal).Value = totalPrice;
                            cmd.ExecuteNonQuery();
                        }

                        // **CLEAR CART**
                        string clearCart = "DELETE FROM Cart WHERE CustomerID = :customerId";
                        using (OracleCommand cmd = new OracleCommand(clearCart, conn))
                        {
                            cmd.Transaction = transaction;
                            cmd.Parameters.Add(":customerId", OracleDbType.Int32).Value = customerId;
                            cmd.ExecuteNonQuery();
                        }

                        // ✅ COMMIT TRANSACTION
                        transaction.Commit();
                        MessageBox.Show("Order Placed Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        // ✅ Handle rollback safely
                        try
                        {
                            transaction.Rollback();
                        }
                        catch (Exception rollbackEx)
                        {
                            MessageBox.Show("Rollback failed: " + rollbackEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        MessageBox.Show("Order failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            CartForm existingCartForm = Application.OpenForms["CartForm"] as CartForm;
            if (existingCartForm != null)
            {
                existingCartForm.Show();
            }
            else
            {
                CartForm cartForm = new CartForm();
                cartForm.Show();
            }
        }

        private void rbCashOnDelivery_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCOD.Checked)
            {
                HideQRCode();
            }
        }

        private void ShowQRCode()
        {
            pictureBoxQRCode.Image = Properties.Resources.QR;
            pictureBoxQRCode.Visible = true;
        }

        private void HideQRCode()
        {
            pictureBoxQRCode.Image = null;
            pictureBoxQRCode.Visible = false;
        }

        private void rbUPI_CheckedChanged(object sender, EventArgs e)
        {
            if (rbUPI.Checked)
            {
                ShowQRCode();
            }
        }
    }
}
