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
    public partial class frmMMSCustomers : Form
    {
        public frmMMSCustomers()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClearData();
        }
        public void ClearData()
        {
            txtcustomer_id.Text = "0";
            txtcustomer_name.Text = "";
            txtcustomer_addr.Text = "";
            txtcustomer_contact.Text = "";
            txtcustomer_tel.Text = "";
            txtcustomer_fax.Text = "";
            txtcustomer_taxid.Text = "";
            chkIsActive.Checked= true;


        }
        public void RefreshGrid()
        {
            var data = new MMSCustomers().Read().ToList();
            dataGridView1.DataSource = data;
        }
        public void DeleteData()
        {
            var vcustomer_id = Main.CInt(txtcustomer_id.Text);

            var chk = new MMSCustomers
            {
                customer_id = vcustomer_id

            }.Delete();
            if (chk)
            {
                MessageBox.Show("Delete Complete!");
            }
        }
        public void LoadData()
        {
            var vcustomer_id = Main.CInt(txtcustomer_id.Text);

            var data = new MMSCustomers
            {
                customer_id = vcustomer_id

            }.Read();
            if (data.Count > 0)
            {
                txtcustomer_id.Text = data[0].customer_id.ToString();
                txtcustomer_name.Text = data[0].customer_name.ToString();
                txtcustomer_addr.Text = data[0].customer_addr.ToString();
                txtcustomer_contact.Text = data[0].customer_contact.ToString();
                txtcustomer_tel.Text = data[0].customer_tel.ToString();
                txtcustomer_fax.Text = data[0].customer_fax.ToString();
                txtcustomer_taxid.Text = data[0].customer_taxid.ToString();
                chkIsActive.Checked = data[0].is_active==1? true : false;

            }
        }
        public void SaveData()
        {
            var data = new MMSCustomers()
            {
                customer_id = Main.CInt(txtcustomer_id.Text),
                customer_name = txtcustomer_name.Text,
                customer_addr = txtcustomer_addr.Text,
                customer_contact = txtcustomer_contact.Text,
                customer_tel = txtcustomer_tel.Text,
                customer_fax = txtcustomer_fax.Text,
                customer_taxid = txtcustomer_taxid.Text,
                is_active = chkIsActive.Checked==true? 1 :0,
            };
            data.Update();
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtcustomer_id.Text = dataGridView1.Rows[e.RowIndex].Cells["customer_id"].Value.ToString();
            txtcustomer_name.Text = dataGridView1.Rows[e.RowIndex].Cells["customer_name"].Value.ToString();
            txtcustomer_addr.Text = dataGridView1.Rows[e.RowIndex].Cells["customer_addr"].Value.ToString();
            txtcustomer_contact.Text = dataGridView1.Rows[e.RowIndex].Cells["customer_contact"].Value.ToString();
            txtcustomer_tel.Text = dataGridView1.Rows[e.RowIndex].Cells["customer_tel"].Value.ToString();
            txtcustomer_fax.Text = dataGridView1.Rows[e.RowIndex].Cells["customer_fax"].Value.ToString();
            txtcustomer_taxid.Text = dataGridView1.Rows[e.RowIndex].Cells["customer_taxid"].Value.ToString();
            chkIsActive.Checked = dataGridView1.Rows[e.RowIndex].Cells["is_active"].Value.ToString() == "1" ? true : false;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
            RefreshGrid();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteData();
            RefreshGrid();
        }

        private void frmMMSCustomers_Load(object sender, EventArgs e)
        {
            RefreshGrid();
        }
    }
}
