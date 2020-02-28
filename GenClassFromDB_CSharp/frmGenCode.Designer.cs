namespace GenClassFromDB_CSharp
{
    partial class frmGenCode
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
            this.txtConnect = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtSQL = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtClassName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.txtConnectUse = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtData = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.txtTable = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Connection";
            // 
            // txtConnect
            // 
            this.txtConnect.Location = new System.Drawing.Point(91, 13);
            this.txtConnect.Name = "txtConnect";
            this.txtConnect.Size = new System.Drawing.Size(628, 20);
            this.txtConnect.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(726, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(64, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "TEST";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // txtSQL
            // 
            this.txtSQL.Location = new System.Drawing.Point(13, 45);
            this.txtSQL.Multiline = true;
            this.txtSQL.Name = "txtSQL";
            this.txtSQL.Size = new System.Drawing.Size(775, 132);
            this.txtSQL.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(16, 207);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(410, 231);
            this.dataGridView1.TabIndex = 4;
            // 
            // txtClassName
            // 
            this.txtClassName.Location = new System.Drawing.Point(77, 183);
            this.txtClassName.Name = "txtClassName";
            this.txtClassName.Size = new System.Drawing.Size(84, 20);
            this.txtClassName.TabIndex = 12;
            this.txtClassName.Text = "CTableName";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 188);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Class Name";
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(347, 185);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(79, 20);
            this.txtKey.TabIndex = 21;
            this.txtKey.Text = "Key1";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(316, 188);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(25, 13);
            this.Label3.TabIndex = 20;
            this.Label3.Text = "Key";
            // 
            // txtConnectUse
            // 
            this.txtConnectUse.Location = new System.Drawing.Point(236, 185);
            this.txtConnectUse.Name = "txtConnectUse";
            this.txtConnectUse.Size = new System.Drawing.Size(76, 20);
            this.txtConnectUse.TabIndex = 19;
            this.txtConnectUse.Text = "JobMasConn";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(167, 188);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Connection";
            // 
            // txtData
            // 
            this.txtData.Location = new System.Drawing.Point(432, 207);
            this.txtData.Multiline = true;
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(356, 231);
            this.txtData.TabIndex = 22;
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(713, 180);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 23;
            this.button2.Text = "Execute";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // txtTable
            // 
            this.txtTable.Location = new System.Drawing.Point(481, 183);
            this.txtTable.Name = "txtTable";
            this.txtTable.Size = new System.Drawing.Size(150, 20);
            this.txtTable.TabIndex = 25;
            this.txtTable.Text = "Table1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(432, 186);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Table";
            // 
            // frmGenCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtTable);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtData);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.txtConnectUse);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtClassName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtSQL);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtConnect);
            this.Controls.Add(this.label1);
            this.Name = "frmGenCode";
            this.Text = "Gen Class From DB";
            this.Load += new System.EventHandler(this.FrmGenCode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtConnect;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtSQL;
        private System.Windows.Forms.DataGridView dataGridView1;
        internal System.Windows.Forms.TextBox txtClassName;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox txtKey;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.TextBox txtConnectUse;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.TextBox txtData;
        private System.Windows.Forms.Button button2;
        internal System.Windows.Forms.TextBox txtTable;
        internal System.Windows.Forms.Label label5;
    }
}

