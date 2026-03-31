namespace Online_Grocery_Store
{
    partial class AddProductForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private System.Windows.Forms.Label lblProductID;
        private System.Windows.Forms.TextBox txtProductID;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.ComboBox cmbSupplier;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.TextBox txtStock;
        private System.Windows.Forms.Label lblImage;
        private System.Windows.Forms.TextBox txtImagePath;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        private void InitializeComponent()
        {
            this.lblProductID = new System.Windows.Forms.Label();
            this.txtProductID = new System.Windows.Forms.TextBox();
            this.lblProductName = new System.Windows.Forms.Label();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.cmbSupplier = new System.Windows.Forms.ComboBox();
            this.lblPrice = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.lblStock = new System.Windows.Forms.Label();
            this.txtStock = new System.Windows.Forms.TextBox();
            this.lblImage = new System.Windows.Forms.Label();
            this.txtImagePath = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // Product ID Label
            this.lblProductID.Location = new System.Drawing.Point(30, 10);
            this.lblProductID.Name = "lblProductID";
            this.lblProductID.Size = new System.Drawing.Size(100, 23);
            this.lblProductID.Text = "Product ID:";

            // Product ID TextBox
            this.txtProductID.Location = new System.Drawing.Point(150, 10);
            this.txtProductID.Name = "txtProductID";
            this.txtProductID.Size = new System.Drawing.Size(100, 22);

            // Product Name Label
            this.lblProductName.Location = new System.Drawing.Point(30, 50);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(100, 23);
            this.lblProductName.Text = "Product Name:";

            // Product Name TextBox
            this.txtProductName.Location = new System.Drawing.Point(150, 50);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(200, 22);

            // Category Label
            this.lblCategory.Location = new System.Drawing.Point(30, 90);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(100, 23);
            this.lblCategory.Text = "Category:";

            // Category ComboBox
            this.cmbCategory.Location = new System.Drawing.Point(150, 90);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(200, 24);

            // Supplier Label
            this.lblSupplier.Location = new System.Drawing.Point(30, 130);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(100, 23);
            this.lblSupplier.Text = "Supplier:";

            // Supplier ComboBox
            this.cmbSupplier.Location = new System.Drawing.Point(150, 130);
            this.cmbSupplier.Name = "cmbSupplier";
            this.cmbSupplier.Size = new System.Drawing.Size(200, 24);

            // Price Label
            this.lblPrice.Location = new System.Drawing.Point(30, 170);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(100, 23);
            this.lblPrice.Text = "Price:";

            // Price TextBox
            this.txtPrice.Location = new System.Drawing.Point(150, 170);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(100, 22);

            // Stock Label
            this.lblStock.Location = new System.Drawing.Point(30, 210);
            this.lblStock.Name = "lblStock";
            this.lblStock.Size = new System.Drawing.Size(100, 23);
            this.lblStock.Text = "Stock Quantity:";

            // Stock TextBox
            this.txtStock.Location = new System.Drawing.Point(150, 210);
            this.txtStock.Name = "txtStock";
            this.txtStock.Size = new System.Drawing.Size(100, 22);

            // Image Label
            this.lblImage.Location = new System.Drawing.Point(30, 250);
            this.lblImage.Name = "lblImage";
            this.lblImage.Size = new System.Drawing.Size(100, 23);
            this.lblImage.Text = "Image Path:";

            // Image Path TextBox
            this.txtImagePath.Location = new System.Drawing.Point(150, 250);
            this.txtImagePath.Name = "txtImagePath";
            this.txtImagePath.Size = new System.Drawing.Size(200, 22);
            //this.txtImagePath.TextChanged += new System.EventHandler(this.txtImagePath_TextChanged);

            // Save Button
            this.btnSave.Location = new System.Drawing.Point(80, 300);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // Cancel Button
            this.btnCancel.Location = new System.Drawing.Point(220, 300);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // Add Controls
            this.Controls.Add(this.lblProductID);
            this.Controls.Add(this.txtProductID);
            this.Controls.Add(this.lblProductName);
            this.Controls.Add(this.txtProductName);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.lblSupplier);
            this.Controls.Add(this.cmbSupplier);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.lblStock);
            this.Controls.Add(this.txtStock);
            this.Controls.Add(this.lblImage);
            this.Controls.Add(this.txtImagePath);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);

            // Form Settings
            this.ClientSize = new System.Drawing.Size(402, 380);
            this.Name = "AddProductForm";
            this.Text = "Add Product";
            this.Load += new System.EventHandler(this.AddProductForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
