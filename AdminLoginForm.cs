using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Online_Grocery_Store
{
    public partial class AdminLoginForm : Form
    {
        // Hardcoded admin credentials
        private const string AdminEmail = "admin@mygrocerystore.com";
        private const string AdminPassword = "admin123";

        public AdminLoginForm()
        {
            InitializeComponent();
        }

        private void lblAdminUsername_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string enteredEmail = txtAdminUsername.Text.Trim();
            string enteredPassword = txtAdminPassword.Text.Trim();

            // Check credentials
            if (enteredEmail == AdminEmail && enteredPassword == AdminPassword)
            {
                MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Open Admin Dashboard
                AdminDashboardForm adminDashboard = new AdminDashboardForm();
                adminDashboard.Show();

                // Close the login form
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid email or password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAdminPassword.Clear();
            }
        }
    }
}
