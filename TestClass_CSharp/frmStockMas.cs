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
    public partial class frmStockMas : Form
    {
        List<CWarehouseType> wt = new List<CWarehouseType>();
        List<CWarehouse> wh = new List<CWarehouse>();
        public frmStockMas()
        {
            InitializeComponent();
        }

        private void FrmStockMas_Load(object sender, EventArgs e)
        {
            LoadDataSource();
        }
        private void LoadDataSource()
        {
            wt = new CWarehouseType().Read();
            cboWarehouseType.Items.Clear();
            cboWarehouseType.Items.AddRange((from CWarehouseType row in wt
                                             select row.WarehouseType).ToArray());

        }
        private void LoadWarehouse()
        {
            try
            {
                wh = new CWarehouse().Read().FindAll(el => el.WarehouseType.Equals(cboWarehouseType.Text));
                cboWarehouse.Items.Clear();
                cboWarehouse.Items.AddRange((from CWarehouse row in wh
                                             select row.WarehouseID).ToArray());

            }
            catch
            {

            }
        }
        private void CboWarehouseType_Validated(object sender, EventArgs e)
        {
            wh = new List<CWarehouse>();
            try
            {
                var value = wt.FirstOrDefault(t => t.WarehouseType.Equals(cboWarehouseType.Text)).WarehouseTypeName;
                txtWarehouseTypeName.Text = value;
            }
            catch
            {
                txtWarehouseTypeName.Text = "";
            }
            LoadWarehouse();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var data = new CWarehouseType();
            data.WarehouseType = cboWarehouseType.Text;
            data.WarehouseTypeName = txtWarehouseTypeName.Text;

            MessageBox.Show(data.Update());
            LoadDataSource();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            var vWarehouseType = cboWarehouseType.Text;
            var chk = new CWarehouseType
            {
                WarehouseType = vWarehouseType

            }.Delete();
            if (chk)
            {
                MessageBox.Show("Delete Complete!");
            }
            LoadDataSource();
        }

        private void CboWarehouseType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CboWarehouse_Validated(object sender, EventArgs e)
        {
            try
            {
                var value = wh.FirstOrDefault(t => t.WarehouseID.Equals(cboWarehouse.Text));
                txtWarehouseName.Text = value.WarehouseName;
                txtWarehouseAddress.Text = value.WarehouseAddress;
            } catch
            {
                txtWarehouseName.Text = "";
                txtWarehouseAddress.Text = "";
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            var data = new CWarehouse();
            data.WarehouseID = cboWarehouse.Text;
            data.WarehouseName = txtWarehouseName.Text;
            data.WarehouseAddress = txtWarehouseAddress.Text;
            data.WarehouseType = cboWarehouseType.Text;

            MessageBox.Show(data.Update());
            LoadWarehouse();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            var vWarehouseID = cboWarehouse.Text;

            var chk = new CWarehouse
            {
                WarehouseID = vWarehouseID


            }.Delete();
            if (chk)
            {
                MessageBox.Show("Delete Complete!");
            }
            LoadWarehouse();
        }
    }
}
