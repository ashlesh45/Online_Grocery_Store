namespace Online_Grocery_Store
{
    partial class DashboardForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox cmbCategories;
        private System.Windows.Forms.Button btnMyOrders;
        private System.Windows.Forms.Button btnCart;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelProducts;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label storeNameLabel;
        private System.Windows.Forms.Label categoryLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cmbCategories = new System.Windows.Forms.ComboBox();
            this.btnMyOrders = new System.Windows.Forms.Button();
            this.btnCart = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.flowLayoutPanelProducts = new System.Windows.Forms.FlowLayoutPanel();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.storeNameLabel = new System.Windows.Forms.Label();
            this.categoryLabel = new System.Windows.Forms.Label();
            this.headerPanel.SuspendLayout();
            this.SuspendLayout();

            // headerPanel
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(60)))));
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(900, 60);
            this.headerPanel.TabIndex = 6;

            // storeNameLabel
            this.storeNameLabel.AutoSize = true;
            this.storeNameLabel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.storeNameLabel.ForeColor = System.Drawing.Color.White;
            this.storeNameLabel.Location = new System.Drawing.Point(20, 15);
            this.storeNameLabel.Name = "storeNameLabel";
            this.storeNameLabel.Size = new System.Drawing.Size(250, 30);
            this.storeNameLabel.Text = "Online Grocery Store";
            this.headerPanel.Controls.Add(this.storeNameLabel);

            // ComboBox: Categories
            this.cmbCategories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategories.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCategories.FormattingEnabled = true;
            this.cmbCategories.Location = new System.Drawing.Point(110, 80);
            this.cmbCategories.Name = "cmbCategories";
            this.cmbCategories.Size = new System.Drawing.Size(180, 25);
            this.cmbCategories.TabIndex = 0;
            this.cmbCategories.SelectedIndexChanged += new System.EventHandler(this.cmbCategories_SelectedIndexChanged);

            // categoryLabel
            this.categoryLabel.AutoSize = true;
            this.categoryLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.categoryLabel.Location = new System.Drawing.Point(30, 83);
            this.categoryLabel.Name = "categoryLabel";
            this.categoryLabel.Size = new System.Drawing.Size(75, 19);
            this.categoryLabel.Text = "Category:";

            // Button: My Orders
            this.btnMyOrders.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(60)))));
            this.btnMyOrders.FlatAppearance.BorderSize = 0;
            this.btnMyOrders.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMyOrders.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnMyOrders.ForeColor = System.Drawing.Color.White;
            this.btnMyOrders.Location = new System.Drawing.Point(650, 15);
            this.btnMyOrders.Name = "btnMyOrders";
            this.btnMyOrders.Size = new System.Drawing.Size(110, 30);
            this.btnMyOrders.Text = "My Orders";
            this.btnMyOrders.UseVisualStyleBackColor = false;
            this.btnMyOrders.Click += new System.EventHandler(this.btnMyOrders_Click);
            this.headerPanel.Controls.Add(this.btnMyOrders);

            // Button: Cart
            this.btnCart.BackColor = System.Drawing.Color.White;
            this.btnCart.FlatAppearance.BorderSize = 0;
            this.btnCart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCart.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(60)))));
            this.btnCart.Location = new System.Drawing.Point(770, 15);
            this.btnCart.Name = "btnCart";
            this.btnCart.Size = new System.Drawing.Size(110, 30);
            this.btnCart.Text = "Cart";
            this.btnCart.UseVisualStyleBackColor = false;
            this.btnCart.Click += new System.EventHandler(this.btnCart_Click);
            this.headerPanel.Controls.Add(this.btnCart);

            // TextBox: Search
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.Location = new System.Drawing.Point(500, 80);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(250, 25);
            this.txtSearch.TabIndex = 3;

            // Button: Search
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(60)))));
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(760, 80);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(110, 25);
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            // FlowLayoutPanel: Display Product Images
            this.flowLayoutPanelProducts.AutoScroll = true;
            this.flowLayoutPanelProducts.BackColor = System.Drawing.Color.WhiteSmoke;
            this.flowLayoutPanelProducts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanelProducts.Location = new System.Drawing.Point(30, 120);
            this.flowLayoutPanelProducts.Name = "flowLayoutPanelProducts";
            this.flowLayoutPanelProducts.Padding = new System.Windows.Forms.Padding(10);
            this.flowLayoutPanelProducts.Size = new System.Drawing.Size(840, 450);
            this.flowLayoutPanelProducts.TabIndex = 5;
            this.flowLayoutPanelProducts.WrapContents = true;
            this.flowLayoutPanelProducts.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;

            // DashboardForm
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.headerPanel);
            this.Controls.Add(this.categoryLabel);
            this.Controls.Add(this.cmbCategories);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.flowLayoutPanelProducts);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "DashboardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Online Grocery Store";
            this.Load += new System.EventHandler(this.DashboardForm_Load);
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}