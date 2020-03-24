namespace TestClass_CSharp
{
    partial class frmAccSetItem
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtAccSetCode = new System.Windows.Forms.TextBox();
            this.txtItemNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGLAccountCode = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRequired = new System.Windows.Forms.CheckBox();
            this.txtAccSide = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.txtCloseToItemNo = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.txtCloseToItemNo)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Account set";
            // 
            // txtAccSetCode
            // 
            this.txtAccSetCode.Enabled = false;
            this.txtAccSetCode.Location = new System.Drawing.Point(13, 20);
            this.txtAccSetCode.Name = "txtAccSetCode";
            this.txtAccSetCode.Size = new System.Drawing.Size(99, 20);
            this.txtAccSetCode.TabIndex = 1;
            // 
            // txtItemNo
            // 
            this.txtItemNo.Enabled = false;
            this.txtItemNo.Location = new System.Drawing.Point(121, 20);
            this.txtItemNo.Name = "txtItemNo";
            this.txtItemNo.Size = new System.Drawing.Size(49, 20);
            this.txtItemNo.TabIndex = 2;
            this.txtItemNo.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(118, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Item No";
            // 
            // txtGLAccountCode
            // 
            this.txtGLAccountCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtGLAccountCode.FormattingEnabled = true;
            this.txtGLAccountCode.Location = new System.Drawing.Point(13, 62);
            this.txtGLAccountCode.Name = "txtGLAccountCode";
            this.txtGLAccountCode.Size = new System.Drawing.Size(260, 21);
            this.txtGLAccountCode.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Account Code";
            // 
            // txtRequired
            // 
            this.txtRequired.AutoSize = true;
            this.txtRequired.Location = new System.Drawing.Point(362, 20);
            this.txtRequired.Name = "txtRequired";
            this.txtRequired.Size = new System.Drawing.Size(69, 17);
            this.txtRequired.TabIndex = 7;
            this.txtRequired.Text = "Required";
            this.txtRequired.UseVisualStyleBackColor = true;
            // 
            // txtAccSide
            // 
            this.txtAccSide.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtAccSide.FormattingEnabled = true;
            this.txtAccSide.Items.AddRange(new object[] {
            "DEBIT",
            "CREDIT"});
            this.txtAccSide.Location = new System.Drawing.Point(279, 62);
            this.txtAccSide.Name = "txtAccSide";
            this.txtAccSide.Size = new System.Drawing.Size(77, 21);
            this.txtAccSide.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(276, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Default Side";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(359, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Close to Item";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 93);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(95, 93);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "Delete";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(173, 18);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(26, 23);
            this.button3.TabIndex = 14;
            this.button3.Text = "+";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // txtCloseToItemNo
            // 
            this.txtCloseToItemNo.Location = new System.Drawing.Point(362, 62);
            this.txtCloseToItemNo.Name = "txtCloseToItemNo";
            this.txtCloseToItemNo.Size = new System.Drawing.Size(65, 20);
            this.txtCloseToItemNo.TabIndex = 15;
            // 
            // frmAccSetItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 128);
            this.Controls.Add(this.txtCloseToItemNo);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtAccSide);
            this.Controls.Add(this.txtRequired);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtGLAccountCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtItemNo);
            this.Controls.Add(this.txtAccSetCode);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmAccSetItem";
            this.Text = "Edit / Add Item";
            this.Load += new System.EventHandler(this.FrmAccSetItem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtCloseToItemNo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAccSetCode;
        private System.Windows.Forms.TextBox txtItemNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox txtGLAccountCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox txtRequired;
        private System.Windows.Forms.ComboBox txtAccSide;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.NumericUpDown txtCloseToItemNo;
    }
}