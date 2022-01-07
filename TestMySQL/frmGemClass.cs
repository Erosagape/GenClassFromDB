using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace TestMySQL
{
    public partial class frmGemClass : Form
    {
        public frmGemClass()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CDB.ConnectionString = textBox2.Text;
            if(CDB.IsConnected)
            {
                MessageBox.Show( "Connected");
                return;
            }
            MessageBox.Show(CDB.ErrorMessage);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (CDB.IsConnected)
            {
                var dt = CDB.GetDataTable(textBox1.Text);
                if(CDB.ErrorMessage.Equals(""))
                {
                    dataGridView1.DataSource = dt;
                } else
                {
                    MessageBox.Show(CDB.ErrorMessage);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.Text = Properties.Settings.Default.ConnectionString;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (CDB.IsConnected)
            {
                DataTable dt = CDB.GetDataTable(textBox1.Text);
                string result = CDB.ReadStructureCS(dt,textBox3.Text,textBox5.Text,textBox4.Text,checkBox1.Checked,checkBox2.Checked);
                Clipboard.SetText(result, TextDataFormat.UnicodeText);
                MessageBox.Show("Complete,Please Paste on IDE or Notepad");
            }
        }
    }
}
