namespace Online_Grocery_Store
{
    partial class CheckoutForm
    {
        private System.ComponentModel.IContainer components = null;

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
            this.lvOrderSummary = new System.Windows.Forms.ListView();
            this.columnProduct = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnQuantity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnPrice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblTotalPrice = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.rbUPI = new System.Windows.Forms.RadioButton();
            this.rbCOD = new System.Windows.Forms.RadioButton();
            this.lblPaymentMethod = new System.Windows.Forms.Label();
            this.pictureBoxQRCode = new System.Windows.Forms.PictureBox();
            this.btnConfirmOrder = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQRCode)).BeginInit();
            this.SuspendLayout();
            // 
            // lvOrderSummary
            // 
            this.lvOrderSummary.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnProduct,
            this.columnQuantity,
            this.columnPrice});
            this.lvOrderSummary.FullRowSelect = true;
            this.lvOrderSummary.GridLines = true;
            this.lvOrderSummary.HideSelection = false;
            this.lvOrderSummary.Location = new System.Drawing.Point(12, 12);
            this.lvOrderSummary.Name = "lvOrderSummary";
            this.lvOrderSummary.Size = new System.Drawing.Size(400, 150);
            this.lvOrderSummary.TabIndex = 0;
            this.lvOrderSummary.UseCompatibleStateImageBehavior = false;
            this.lvOrderSummary.View = System.Windows.Forms.View.Details;
            // 
            // columnProduct
            // 
            this.columnProduct.Text = "Product";
            this.columnProduct.Width = 180;
            // 
            // columnQuantity
            // 
            this.columnQuantity.Text = "Quantity";
            this.columnQuantity.Width = 80;
            // 
            // columnPrice
            // 
            this.columnPrice.Text = "Price";
            this.columnPrice.Width = 80;
            // 
            // lblTotalPrice
            // 
            this.lblTotalPrice.AutoSize = true;
            this.lblTotalPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalPrice.Location = new System.Drawing.Point(12, 175);
            this.lblTotalPrice.Name = "lblTotalPrice";
            this.lblTotalPrice.Size = new System.Drawing.Size(108, 20);
            this.lblTotalPrice.TabIndex = 1;
            this.lblTotalPrice.Text = "Total: ₹0.00";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(12, 220);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(400, 50);
            this.txtAddress.TabIndex = 2;
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(12, 295);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(200, 22);
            this.txtPhone.TabIndex = 3;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblAddress.Location = new System.Drawing.Point(12, 200);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(74, 18);
            this.lblAddress.TabIndex = 4;
            this.lblAddress.Text = "Address:";
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblPhone.Location = new System.Drawing.Point(12, 275);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(61, 18);
            this.lblPhone.TabIndex = 5;
            this.lblPhone.Text = "Phone:";
            // 
            // rbUPI
            // 
            this.rbUPI.AutoSize = true;
            this.rbUPI.Location = new System.Drawing.Point(16, 339);
            this.rbUPI.Name = "rbUPI";
            this.rbUPI.Size = new System.Drawing.Size(50, 20);
            this.rbUPI.TabIndex = 6;
            this.rbUPI.TabStop = true;
            this.rbUPI.Text = "UPI";
            this.rbUPI.UseVisualStyleBackColor = true;
            this.rbUPI.CheckedChanged += new System.EventHandler(this.rbUPI_CheckedChanged);
            // 
            // rbCOD
            // 
            this.rbCOD.AutoSize = true;
            this.rbCOD.Location = new System.Drawing.Point(15, 365);
            this.rbCOD.Name = "rbCOD";
            this.rbCOD.Size = new System.Drawing.Size(130, 20);
            this.rbCOD.TabIndex = 7;
            this.rbCOD.TabStop = true;
            this.rbCOD.Text = "Cash on Delivery";
            this.rbCOD.UseVisualStyleBackColor = true;
            this.rbCOD.CheckedChanged += new System.EventHandler(this.rbCashOnDelivery_CheckedChanged);
            // 
            // lblPaymentMethod
            // 
            this.lblPaymentMethod.AutoSize = true;
            this.lblPaymentMethod.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblPaymentMethod.Location = new System.Drawing.Point(12, 318);
            this.lblPaymentMethod.Name = "lblPaymentMethod";
            this.lblPaymentMethod.Size = new System.Drawing.Size(139, 18);
            this.lblPaymentMethod.TabIndex = 8;
            this.lblPaymentMethod.Text = "Payment Method:";
            // 
            // pictureBoxQRCode
            // 
            this.pictureBoxQRCode.Location = new System.Drawing.Point(290, 285);
            this.pictureBoxQRCode.Name = "pictureBoxQRCode";
            this.pictureBoxQRCode.Size = new System.Drawing.Size(100, 100);
            this.pictureBoxQRCode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxQRCode.TabIndex = 9;
            this.pictureBoxQRCode.TabStop = false;
            this.pictureBoxQRCode.Visible = false;
            // 
            // btnConfirmOrder
            // 
            this.btnConfirmOrder.BackColor = System.Drawing.Color.Green;
            this.btnConfirmOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnConfirmOrder.ForeColor = System.Drawing.Color.White;
            this.btnConfirmOrder.Location = new System.Drawing.Point(12, 400);
            this.btnConfirmOrder.Name = "btnConfirmOrder";
            this.btnConfirmOrder.Size = new System.Drawing.Size(200, 40);
            this.btnConfirmOrder.TabIndex = 10;
            this.btnConfirmOrder.Text = "Confirm Order";
            this.btnConfirmOrder.UseVisualStyleBackColor = false;
            this.btnConfirmOrder.Click += new System.EventHandler(this.btnConfirmOrder_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(290, 401);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(100, 40);
            this.btnBack.TabIndex = 11;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // CheckoutForm
            // 
            this.ClientSize = new System.Drawing.Size(450, 450);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnConfirmOrder);
            this.Controls.Add(this.pictureBoxQRCode);
            this.Controls.Add(this.lblPaymentMethod);
            this.Controls.Add(this.rbCOD);
            this.Controls.Add(this.rbUPI);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.lblTotalPrice);
            this.Controls.Add(this.lvOrderSummary);
            this.Name = "CheckoutForm";
            this.Text = "Checkout";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxQRCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.ListView lvOrderSummary;
        private System.Windows.Forms.ColumnHeader columnProduct;
        private System.Windows.Forms.ColumnHeader columnQuantity;
        private System.Windows.Forms.ColumnHeader columnPrice;
        private System.Windows.Forms.Label lblTotalPrice;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.RadioButton rbUPI;
        private System.Windows.Forms.RadioButton rbCOD;
        private System.Windows.Forms.Label lblPaymentMethod;
        private System.Windows.Forms.PictureBox pictureBoxQRCode;
        private System.Windows.Forms.Button btnConfirmOrder;
        private System.Windows.Forms.Button btnBack;
    }
}
