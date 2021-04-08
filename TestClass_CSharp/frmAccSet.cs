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
    public partial class frmAccSet : Form
    {
        private List<CGLSetItem> rows =new List<CGLSetItem>();
        public frmAccSet()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshGrid();
            LoadCombo();
        }
        public void LoadCombo()
        {
            txtAccSetCode.Items.Clear();
            txtAccSetCode.Items.AddRange((from CGLSet row in new CGLSet().Read()
                                          select row.AccSetCode).ToArray());
        }
        public void RefreshGrid()
        {
            rows= new CGLSetItem
            {
                AccSetCode =txtAccSetCode.Text
            }.Read().ToList();
            dataGridView1.DataSource = rows;            
        }
        public void ClearData()
        {
            txtAccSetCode.Text = "";
            txtAccSetName.Text = "";
            txtGLTypeCode.Text = "";
        }
        public void LoadData()
        {
            var vAccSetCode = txtAccSetCode.Text.ToUpper();
            var data = new CGLSet
            {
                AccSetCode = vAccSetCode

            }.Read();
            if (data.Count > 0)
            {
                txtAccSetCode.Text = data[0].AccSetCode.ToString();
                txtAccSetName.Text = data[0].AccSetName.ToString();
                txtGLTypeCode.Text = data[0].GLTypeCode.ToString();
                RefreshGrid();
            } else
            {
                ClearData();
                txtAccSetCode.Text = vAccSetCode;
            }
        }
        public void SaveData()
        {
            var data = new CGLSet();
            data.AccSetCode = txtAccSetCode.Text;
            data.AccSetName = txtAccSetName.Text;
            data.GLTypeCode = txtGLTypeCode.Text;

            MessageBox.Show(data.Update());
            LoadCombo();
            RefreshGrid();
        }
        public void DeleteData()
        {
            var vAccSetCode = txtAccSetCode.Text;
            var data = new CGLSet
            {
                AccSetCode = vAccSetCode

            }.Delete();
            if (data == true)
            {
                ClearData();
                MessageBox.Show("Delete Complete");
                RefreshGrid();
            }
        }

        private void TxtAccSetCode_Validated(object sender, EventArgs e)
        {
            LoadData();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DeleteData();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            frmAccSetItem frm = new frmAccSetItem();
            frm.SetParameter(txtAccSetCode.Text, 0);
            frm.ShowDialog();
            RefreshGrid();
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                frmAccSetItem frm = new frmAccSetItem();
                frm.LoadData(rows[e.RowIndex]);
                frm.ShowDialog();
                RefreshGrid();
            }
        }

        private void TxtAccSetCode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
