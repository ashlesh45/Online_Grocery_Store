using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Security.Cryptography;
using Oracle.ManagedDataAccess.Client;

namespace Online_Grocery_Store
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close(); // Closes the form when Cancel is clicked
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string password = txtPassword.Text;
            string address = txtAddress.Text.Trim();

            if (name == "" || email == "" || phone == "" || password == "" || address == "")
            {
                MessageBox.Show("All fields are required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            string hashedPassword = HashPassword(password); // Hash password before storing it

            using (OracleConnection conn = DatabaseHelper.GetConnection()) // Use global connection
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Customers (CustomerID, Name, Email, Phone, Address, PasswordHash) " +
                                   "VALUES (CUSTOMERS_SEQ.NEXTVAL, :Name, :Email, :Phone, :Address, :PasswordHash)";

                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add(":Name", OracleDbType.Varchar2).Value = name;
                        cmd.Parameters.Add(":Email", OracleDbType.Varchar2).Value = email;
                        cmd.Parameters.Add(":Phone", OracleDbType.Varchar2).Value = phone;
                        cmd.Parameters.Add(":Address", OracleDbType.Varchar2).Value = address;  // ✅ FIXED
                        cmd.Parameters.Add(":PasswordHash", OracleDbType.Varchar2).Value = hashedPassword;
                        
                        int rowsInserted = cmd.ExecuteNonQuery();
                        if (rowsInserted > 0)
                        {
                            MessageBox.Show("Registration Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close(); // Close form after success
                        }
                        else
                        {
                            MessageBox.Show("Registration Failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
