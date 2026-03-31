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
using System.Security.Cryptography;

namespace Online_Grocery_Store
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private bool VerifyPassword(string enteredPassword, string storedHash)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(enteredPassword);
                byte[] hash = sha256.ComputeHash(bytes);
                string enteredHash = Convert.ToBase64String(hash);
                return enteredHash == storedHash;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter email and password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (OracleConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT CUSTOMERID, PasswordHash FROM Customers WHERE Email = :Email";
                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add(":Email", OracleDbType.Varchar2).Value = email;

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int customerId = reader.GetInt32(0);
                                string storedHash = reader.GetString(1);

                                if (VerifyPassword(password, storedHash))
                                {
                                    // ✅ Store the logged-in customer ID in SessionManager
                                    SessionManager.LoggedInCustomerId = customerId;

                                    MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    // Open Dashboard Form
                                    DashboardForm dashboard = new DashboardForm();
                                    dashboard.Show();
                                    this.Hide(); // Hide the login form
                                }
                                else
                                {
                                    MessageBox.Show("Invalid password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("User not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void LoginForm_Load(object sender, EventArgs e)
        {
            // This method is executed when the LoginForm loads.
            // If you need to initialize something, you can add it here.
        }
        private void button2_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
        }

        private void btnAdminLogin_Click(object sender, EventArgs e)
        {
            AdminLoginForm adminLogin = new AdminLoginForm();
            adminLogin.Show();
        }
    }
}
