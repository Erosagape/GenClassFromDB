using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestClass_CSharp
{
    public partial class frmAccSetItem : Form
    {
        public frmAccSetItem()
        {
            InitializeComponent();
        }
        public void SetParameter(string accset,int item)
        {
            txtAccSetCode.Text = accset;
            txtItemNo.Text = item.ToString();
        }
        public void LoadData(CGLSetItem data)
        {
            LoadCombo();
            txtAccSetCode.Text = data.AccSetCode;
            txtItemNo.Text = data.ItemNo.ToString();
            txtGLAccountCode.SelectedValue = data.GLAccountCode;
            txtAccSide.SelectedIndex = (data.AccSide == "D" ? 0 : 1);
            txtRequired.Checked = (data.Required == 1 ? true : false);
            txtCloseToItemNo.Value = data.CloseToItemNo;                
        }
        private void FrmAccSetItem_Load(object sender, EventArgs e)
        {
            if (txtGLAccountCode.Items.Count == 0)
            {
                LoadCombo();
            }
        }
        private void LoadCombo()
        {
            DataTable acc = Main.GetDataSQL("SELECT AccCode,AccCode +' '+AccTName as AccName FROM Mas_Account");
            txtGLAccountCode.DataSource = acc;
            txtGLAccountCode.DisplayMember = "AccName";
            txtGLAccountCode.ValueMember = "AccCode";
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            txtItemNo.Text = "0";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            SaveData();
            this.Close();
        }
        public void SaveData()
        {
            var data = new CGLSetItem();
            data.AccSetCode = txtAccSetCode.Text;
            data.ItemNo = Main.CInt(txtItemNo.Text);
            if (data.ItemNo == 0)
                data.AddNew();
            data.GLAccountCode = txtGLAccountCode.SelectedValue.ToString();
            data.AccSide = txtAccSide.Text.Substring(0,1);
            data.Required = (txtRequired.Checked ==true ? 1 : 0);
            data.CloseToItemNo = Main.CInt(txtCloseToItemNo.Value);

            MessageBox.Show(data.Update());
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DeleteData();
        }
        public void DeleteData()
        {
            var vAccSetCode = txtAccSetCode.Text;
            var vItemNo = Main.CInt(txtItemNo.Text);
            var chk = new CGLSetItem
            {
                AccSetCode = vAccSetCode,
                ItemNo = vItemNo
            }.Delete();
            if (chk)
            {
                MessageBox.Show("Delete Complete!");
                this.Close();
            }
        }
    }
}
