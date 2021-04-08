namespace TestClass_CSharp
{
    partial class frmStockMas
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.cboWarehouseType = new System.Windows.Forms.ComboBox();
            this.txtWarehouseTypeName = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.cboWarehouse = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtWarehouseName = new System.Windows.Forms.TextBox();
            this.txtWarehouseAddress = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.txtLocationName = new System.Windows.Forms.TextBox();
            this.cboLocation = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Warehouse Type";
            // 
            // cboWarehouseType
            // 
            this.cboWarehouseType.FormattingEnabled = true;
            this.cboWarehouseType.Location = new System.Drawing.Point(28, 25);
            this.cboWarehouseType.Name = "cboWarehouseType";
            this.cboWarehouseType.Size = new System.Drawing.Size(121, 21);
            this.cboWarehouseType.TabIndex = 3;
            this.cboWarehouseType.SelectedIndexChanged += new System.EventHandler(this.CboWarehouseType_SelectedIndexChanged);
            this.cboWarehouseType.Validated += new System.EventHandler(this.CboWarehouseType_Validated);
            // 
            // txtWarehouseTypeName
            // 
            this.txtWarehouseTypeName.Location = new System.Drawing.Point(156, 25);
            this.txtWarehouseTypeName.Name = "txtWarehouseTypeName";
            this.txtWarehouseTypeName.Size = new System.Drawing.Size(377, 20);
            this.txtWarehouseTypeName.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(540, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(621, 25);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Delete";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // cboWarehouse
            // 
            this.cboWarehouse.FormattingEnabled = true;
            this.cboWarehouse.Location = new System.Drawing.Point(28, 73);
            this.cboWarehouse.Name = "cboWarehouse";
            this.cboWarehouse.Size = new System.Drawing.Size(121, 21);
            this.cboWarehouse.TabIndex = 7;
            this.cboWarehouse.Validated += new System.EventHandler(this.CboWarehouse_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Warehouse";
            // 
            // txtWarehouseName
            // 
            this.txtWarehouseName.Location = new System.Drawing.Point(156, 73);
            this.txtWarehouseName.Name = "txtWarehouseName";
            this.txtWarehouseName.Size = new System.Drawing.Size(540, 20);
            this.txtWarehouseName.TabIndex = 9;
            // 
            // txtWarehouseAddress
            // 
            this.txtWarehouseAddress.Location = new System.Drawing.Point(28, 100);
            this.txtWarehouseAddress.Multiline = true;
            this.txtWarehouseAddress.Name = "txtWarehouseAddress";
            this.txtWarehouseAddress.Size = new System.Drawing.Size(668, 61);
            this.txtWarehouseAddress.TabIndex = 10;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(109, 167);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(72, 24);
            this.button3.TabIndex = 12;
            this.button3.Text = "Delete";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(28, 167);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(72, 24);
            this.button4.TabIndex = 11;
            this.button4.Text = "Save";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // txtLocationName
            // 
            this.txtLocationName.Location = new System.Drawing.Point(156, 217);
            this.txtLocationName.Name = "txtLocationName";
            this.txtLocationName.Size = new System.Drawing.Size(377, 20);
            this.txtLocationName.TabIndex = 15;
            // 
            // cboLocation
            // 
            this.cboLocation.FormattingEnabled = true;
            this.cboLocation.Location = new System.Drawing.Point(28, 217);
            this.cboLocation.Name = "cboLocation";
            this.cboLocation.Size = new System.Drawing.Size(121, 21);
            this.cboLocation.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 200);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Location ID";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(621, 217);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 17;
            this.button5.Text = "Delete";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(540, 217);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 16;
            this.button6.Text = "Save";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // frmStockMas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 272);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.txtLocationName);
            this.Controls.Add(this.cboLocation);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.txtWarehouseAddress);
            this.Controls.Add(this.txtWarehouseName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboWarehouse);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtWarehouseTypeName);
            this.Controls.Add(this.cboWarehouseType);
            this.Controls.Add(this.label1);
            this.Name = "frmStockMas";
            this.Text = "frmStockMas";
            this.Load += new System.EventHandler(this.FrmStockMas_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboWarehouseType;
        private System.Windows.Forms.TextBox txtWarehouseTypeName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox cboWarehouse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtWarehouseName;
        private System.Windows.Forms.TextBox txtWarehouseAddress;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox txtLocationName;
        private System.Windows.Forms.ComboBox cboLocation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
    }
}