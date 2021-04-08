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
    public partial class frmWebUsers : Form
    {
        public frmWebUsers()
        {
            InitializeComponent();
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            LoadGrid(e);
        }
        public void ClearData()
        {
            txtuser_id.Text = "";
            txtuser_name.Text = "";
            txtuser_password.Text = "";
            txtis_active.Text = "1";
        }
        public void RefreshGrid()
        {
            var data = new CWebUsers().Read().ToList();
            dataGridView1.DataSource = data;
        }
        public void DeleteData()
        {
            var vuser_id = txtuser_id.Text;

            var chk = new CWebUsers
            {
                user_id = vuser_id

            }.Delete();
            if (chk)
            {
                MessageBox.Show("Delete Complete!");
            }
        }
        public void LoadGrid(DataGridViewCellEventArgs e)
        {
            txtuser_id.Text = dataGridView1.Rows[e.RowIndex].Cells["user_id"].Value.ToString();
            txtuser_name.Text = dataGridView1.Rows[e.RowIndex].Cells["user_name"].Value.ToString();
            txtuser_password.Text = dataGridView1.Rows[e.RowIndex].Cells["user_password"].Value.ToString();
            txtis_active.Text = dataGridView1.Rows[e.RowIndex].Cells["is_active"].Value.ToString();
        }
        public void LoadData()
        {
            var vuser_id = txtuser_id.Text;

            var data = new CWebUsers
            {
                user_id = vuser_id

            }.Read();
            if (data.Count > 0)
            {
                txtuser_id.Text = data[0].user_id.ToString();
                txtuser_name.Text = data[0].user_name.ToString();
                txtuser_password.Text = data[0].user_password.ToString();
                txtis_active.Text = data[0].is_active.ToString();
            }
        }
        public void SaveData()
        {
            var data = new CWebUsers()
            {
                user_id = txtuser_id.Text,
                user_name = txtuser_name.Text,
                user_password = txtuser_password.Text,
                is_active = txtis_active.Text
            };
            data.Update();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClearData();
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmWebUsers_Load(object sender, EventArgs e)
        {
            RefreshGrid();
        }
    }
}
