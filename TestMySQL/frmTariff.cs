using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestMySQL
{
    public partial class frmTariff : Form
    {
        public frmTariff()
        {
            InitializeComponent();
        }

        private void genClassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGemClass frm = new frmGemClass();
            frm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //Sample Using class for win app
        public void ClearData()
        {
            txtID.Text = "0";
            txtTRFCLS.Text = "";
            txtDSCTH.Text = "";
            txtDSCEN.Text = "";
            txtDATESTR.Text = "";
            txtDATEFIN.Text = "";
        }
        public void RefreshGrid()
        {
            var data = new CTariffClass().Read().ToList();
            dataGridView1.DataSource = data;
        }
        public void DeleteData()
        {
            var vTRFCLS = txtTRFCLS.Text;

            var chk = new CTariffClass
            {
                TRFCLS = vTRFCLS

            }.Delete();
            if (chk)
            {
                MessageBox.Show("Delete Complete!");
            }
        }
        public void LoadData()
        {
            var vTRFCLS = txtTRFCLS.Text;
            var data = new CTariffClass
            {
                TRFCLS = vTRFCLS

            }.Read();
            if (data.Count > 0)
            {
                txtID.Text = data[0].ID.ToString();
                txtTRFCLS.Text = data[0].TRFCLS.ToString();
                txtDSCTH.Text = data[0].DSCTH.ToString();
                txtDSCEN.Text = data[0].DSCEN.ToString();
                txtDATESTR.Text = data[0].DATESTR.ToString();
                txtDATEFIN.Text = data[0].DATEFIN.ToString();
            } else
            {
                MessageBox.Show("Data Not Found");
            }
        }
        public void SaveData()
        {
            var data = new CTariffClass
            {
                ID = Main.CInt(txtID.Text),
                TRFCLS = txtTRFCLS.Text,
                DSCTH = txtDSCTH.Text,
                DSCEN = txtDSCEN.Text,
                DATESTR = txtDATESTR.Text,
                DATEFIN = txtDATEFIN.Text
            };

            MessageBox.Show(data.Update());
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteData();
            RefreshGrid();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
            RefreshGrid();
        }

        private void txtTRFCLS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                LoadData();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtID.Text = dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString();
                txtTRFCLS.Text = dataGridView1.Rows[e.RowIndex].Cells["TRFCLS"].Value.ToString();
                txtDSCTH.Text = dataGridView1.Rows[e.RowIndex].Cells["DSCTH"].Value.ToString();
                txtDSCEN.Text = dataGridView1.Rows[e.RowIndex].Cells["DSCEN"].Value.ToString();
                txtDATESTR.Text = dataGridView1.Rows[e.RowIndex].Cells["DATESTR"].Value.ToString();
                txtDATEFIN.Text = dataGridView1.Rows[e.RowIndex].Cells["DATEFIN"].Value.ToString();
            }
        }

        private void txtTRFCLS_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
