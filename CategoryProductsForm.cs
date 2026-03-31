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
    public partial class CategoryProductsForm : Form
    {
        public CategoryProductsForm()
        {
            InitializeComponent();
        }
        private string selectedCategory; // Store the selected category

        
        public CategoryProductsForm(string category)
        {
            InitializeComponent();
            selectedCategory = category;
            LoadProducts(category);
        }
        private void cmbSortBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSortBy.SelectedItem != null)
            {
                string sortOption = cmbSortBy.SelectedItem.ToString();
                string orderByClause = "";

                switch (sortOption)
                {
                    case "Price - Low to High":
                        orderByClause = "ORDER BY Price ASC";
                        break;
                    case "Price - High to Low":
                        orderByClause = "ORDER BY Price DESC";
                        break;
                    case "Quantity - Low to High":
                        orderByClause = "ORDER BY Quantity ASC";
                        break;
                    case "Quantity - High to Low":
                        orderByClause = "ORDER BY Quantity DESC";
                        break;
                }

                LoadProducts(selectedCategory, orderByClause);
            }
        }

        private void LoadProducts(string category, string orderByClause = "")
        {
            try
            {
                using (OracleConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT Name, Price, StockQuantity 
                 FROM Products 
                 WHERE CategoryID = (SELECT CategoryID FROM Categories WHERE CategoryName = :Category)";


                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add(":Category", OracleDbType.Varchar2).Value = category;

                        using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dgvProducts.DataSource = dt; // Bind DataTable to DataGridView
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading products: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CategoryProductsForm_Load(object sender, EventArgs e)
        {

        }

        private void CategoryProductsForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}
