
namespace TestMySQL
{
    partial class frmMMSCustomers
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtcustomer_id = new System.Windows.Forms.TextBox();
            this.txtcustomer_name = new System.Windows.Forms.TextBox();
            this.txtcustomer_addr = new System.Windows.Forms.TextBox();
            this.txtcustomer_contact = new System.Windows.Forms.TextBox();
            this.txtcustomer_tel = new System.Windows.Forms.TextBox();
            this.txtcustomer_fax = new System.Windows.Forms.TextBox();
            this.txtcustomer_taxid = new System.Windows.Forms.TextBox();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(16, 167);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(775, 271);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
            // 
            // txtcustomer_id
            // 
            this.txtcustomer_id.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtcustomer_id.Location = new System.Drawing.Point(13, 21);
            this.txtcustomer_id.Name = "txtcustomer_id";
            this.txtcustomer_id.Size = new System.Drawing.Size(70, 20);
            this.txtcustomer_id.TabIndex = 1;
            // 
            // txtcustomer_name
            // 
            this.txtcustomer_name.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtcustomer_name.Location = new System.Drawing.Point(89, 21);
            this.txtcustomer_name.Name = "txtcustomer_name";
            this.txtcustomer_name.Size = new System.Drawing.Size(395, 20);
            this.txtcustomer_name.TabIndex = 2;
            // 
            // txtcustomer_addr
            // 
            this.txtcustomer_addr.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtcustomer_addr.Location = new System.Drawing.Point(12, 59);
            this.txtcustomer_addr.Multiline = true;
            this.txtcustomer_addr.Name = "txtcustomer_addr";
            this.txtcustomer_addr.Size = new System.Drawing.Size(472, 58);
            this.txtcustomer_addr.TabIndex = 3;
            // 
            // txtcustomer_contact
            // 
            this.txtcustomer_contact.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtcustomer_contact.Location = new System.Drawing.Point(501, 21);
            this.txtcustomer_contact.Name = "txtcustomer_contact";
            this.txtcustomer_contact.Size = new System.Drawing.Size(287, 20);
            this.txtcustomer_contact.TabIndex = 4;
            // 
            // txtcustomer_tel
            // 
            this.txtcustomer_tel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtcustomer_tel.Location = new System.Drawing.Point(501, 59);
            this.txtcustomer_tel.Name = "txtcustomer_tel";
            this.txtcustomer_tel.Size = new System.Drawing.Size(138, 20);
            this.txtcustomer_tel.TabIndex = 5;
            // 
            // txtcustomer_fax
            // 
            this.txtcustomer_fax.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtcustomer_fax.Location = new System.Drawing.Point(646, 59);
            this.txtcustomer_fax.Name = "txtcustomer_fax";
            this.txtcustomer_fax.Size = new System.Drawing.Size(142, 20);
            this.txtcustomer_fax.TabIndex = 6;
            // 
            // txtcustomer_taxid
            // 
            this.txtcustomer_taxid.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtcustomer_taxid.Location = new System.Drawing.Point(501, 97);
            this.txtcustomer_taxid.Name = "txtcustomer_taxid";
            this.txtcustomer_taxid.Size = new System.Drawing.Size(138, 20);
            this.txtcustomer_taxid.TabIndex = 7;
            // 
            // chkIsActive
            // 
            this.chkIsActive.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Location = new System.Drawing.Point(646, 99);
            this.chkIsActive.Name = "chkIsActive";
            this.chkIsActive.Size = new System.Drawing.Size(56, 17);
            this.chkIsActive.TabIndex = 8;
            this.chkIsActive.Text = "Active";
            this.chkIsActive.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnClose.Location = new System.Drawing.Point(592, 138);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 23;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnRefresh.Location = new System.Drawing.Point(508, 138);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 22;
            this.btnRefresh.Text = "&Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnDelete.Location = new System.Drawing.Point(287, 138);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 21;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSave.Location = new System.Drawing.Point(205, 138);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 20;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnAdd.Location = new System.Drawing.Point(124, 138);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 19;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(86, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Address";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(498, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "Contact";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(498, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 13);
            this.label5.TabIndex = 28;
            this.label5.Text = "Tel";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(645, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 13);
            this.label6.TabIndex = 29;
            this.label6.Text = "Fax";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(498, 81);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 13);
            this.label7.TabIndex = 30;
            this.label7.Text = "Tax Reference ID";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // frmMMSCustomers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.chkIsActive);
            this.Controls.Add(this.txtcustomer_taxid);
            this.Controls.Add(this.txtcustomer_fax);
            this.Controls.Add(this.txtcustomer_tel);
            this.Controls.Add(this.txtcustomer_contact);
            this.Controls.Add(this.txtcustomer_addr);
            this.Controls.Add(this.txtcustomer_name);
            this.Controls.Add(this.txtcustomer_id);
            this.Controls.Add(this.dataGridView1);
            this.Name = "frmMMSCustomers";
            this.Text = "frmMMSCustomers";
            this.Load += new System.EventHandler(this.frmMMSCustomers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtcustomer_id;
        private System.Windows.Forms.TextBox txtcustomer_name;
        private System.Windows.Forms.TextBox txtcustomer_addr;
        private System.Windows.Forms.TextBox txtcustomer_contact;
        private System.Windows.Forms.TextBox txtcustomer_tel;
        private System.Windows.Forms.TextBox txtcustomer_fax;
        private System.Windows.Forms.TextBox txtcustomer_taxid;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}