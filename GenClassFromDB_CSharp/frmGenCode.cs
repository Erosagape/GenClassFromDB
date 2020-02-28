using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenClassFromDB_CSharp
{
    public partial class frmGenCode : Form
    {
        public frmGenCode()
        {
            InitializeComponent();
        }

        private void FrmGenCode_Load(object sender, EventArgs e)
        {
            txtConnect.Text = Properties.Settings.Default["ConnectionStr"].ToString();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                using(OleDbConnection cn=new OleDbConnection(txtConnect.Text))
                {
                    cn.Open();
                    MessageBox.Show("Connected");
                    cn.Close();
                    button2.Enabled = true;
                }
            }
            catch(Exception ex)
            {
                button2.Enabled = false;
                MessageBox.Show(ex.Message);
            }

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                using(OleDbDataAdapter da=new OleDbDataAdapter(txtSQL.Text, txtConnect.Text))
                {
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    txtData.Text = ReadStructure(dt);
                    if (txtData.Text != "")
                    {
                        Clipboard.SetText(txtData.Text, TextDataFormat.UnicodeText);
                        MessageBox.Show("Copy to Clipboard OK!");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        string ReadStructure(DataTable tb)
        {
            string strField = "";
            string strLoad = "";
            string strDefault = "";
            string strSet = "";
            foreach(DataColumn dc in tb.Columns)
            {
                string strType = dc.DataType.FullName.ToString().Replace("System.","");
                string fldName = @"""" + dc.ColumnName + @"""";
                switch (strType)
                {
                    case "Double":
                        strField += "public double " + dc.ColumnName + " { get; set; } \r\n";
                        strLoad += dc.ColumnName+ String.Format(" = rd.GetDouble(rd.GetOrdinal({0})),\r\n",fldName);
                        strDefault += "this." + dc.ColumnName + "=0;\r\n";
                        strSet += @"row["+fldName+"] = this."+dc.ColumnName+ ";\r\n";
                        break;
                    case "Date":
                        strField += "public DateTime " + dc.ColumnName + " { get; set; } \r\n";
                        strLoad += dc.ColumnName + String.Format(" = rd.GetDateTime(rd.GetOrdinal({0})),\r\n", fldName);
                        strDefault += "this." + dc.ColumnName + "=default(DateTime);\r\n";
                        strSet += @"row[" + fldName + "] = this." + dc.ColumnName + @" == null? default(DateTime) : this." + dc.ColumnName + ";\r\n";
                        break;
                    case "DateTime":
                        strField += "public DateTime " + dc.ColumnName + " { get; set; } \r\n";
                        strLoad += dc.ColumnName + String.Format(" = rd.GetDateTime(rd.GetOrdinal({0})),\r\n", fldName);
                        strDefault += "this." + dc.ColumnName + "=default(DateTime);\r\n";
                        strSet += @"row[" + fldName + "] = this." + dc.ColumnName + @" == null? default(DateTime) : this." + dc.ColumnName + ";\r\n";
                        break;
                    case "Int32":
                        strField += "public int " + dc.ColumnName + " { get; set; } \r\n";
                        strLoad += dc.ColumnName + String.Format(" = rd.GetInt32(rd.GetOrdinal({0})),\r\n", fldName);
                        strDefault += "this." + dc.ColumnName + "=0;\r\n";
                        strSet += @"row[" + fldName + "] = this." + dc.ColumnName + ";\r\n";
                        break;
                    case "Integer":
                        strField += "public int " + dc.ColumnName + " { get; set; } \r\n";
                        strLoad += dc.ColumnName + String.Format(" = rd.GetInt32(rd.GetOrdinal({0})),\r\n", fldName);
                        strDefault += "this." + dc.ColumnName + "=0;\r\n";
                        strSet += @"row[" + fldName + "] = this." + dc.ColumnName + ";\r\n";
                        break;
                    default:
                        strField += "public string " + dc.ColumnName + " { get; set; } \r\n";
                        strLoad += dc.ColumnName + String.Format(" = rd.GetString(rd.GetOrdinal({0})),\r\n", fldName);
                        strDefault += "this." + dc.ColumnName + "="+@""""""+";\r\n";
                        strSet += @"row[" + fldName + "] = this." + dc.ColumnName + @" == null? """" : this." + dc.ColumnName + ";\r\n";
                        break;
                }
            }
            string strAll = @"
    public class " + txtClassName.Text + @" : ITableInterface
    {
        " + strField + @"        
        public " + txtClassName.Text + @"()
        {
            Create();
        }
        private " + txtClassName.Text + @" GetData(SqlDataReader rd)
        {
            " + txtClassName.Text + @" row = new " + txtClassName.Text + @"
            {
                " + strLoad + @"
            };
            return row;
        }
        private void SetData(ref DataRow row)
        {
            " + strSet + @"
        }
        public List<" + txtClassName.Text + @"> Read()
        {
            List<" + txtClassName.Text + @"> lst = new List<" + txtClassName.Text + @">();
            try
            {
                using (SqlConnection conn = new SqlConnection(Properties.Settings.Default." + txtConnectUse.Text + @"))
                {
                    conn.Open();
                    using (SqlDataReader reader = new SqlCommand(GetSQLSelect(), conn).ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lst.Add(GetData(reader));
                        }
                        reader.Close();
                    }
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return lst;
        }
        public string GetSQLWhere()
        {
            string sql = (this." + txtKey.Text + @" != """" ? String.Format("" WHERE " + txtKey.Text + @"='{0}'"", this." + txtKey.Text + @") : """");
            return sql;
        }
        public string GetSQLSelect()
        {
            string sql = ""SELECT * FROM " + txtTable.Text + @" "" + GetSQLWhere();
            return sql;
        }
        public string GetSQLDelete()
        {
            string sql = ""DELETE FROM " + txtTable.Text + @" "" + GetSQLWhere();
            return sql;
        }
        public void Create()
        {
            " + strDefault + @"
        }
        public bool Delete()
        {
            bool success;
            try
            {
                using (SqlConnection conn = new SqlConnection(Properties.Settings.Default." + txtConnectUse.Text + @"))
                {
                    conn.Open();
                    string sql = GetSQLDelete();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                }
                success = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return success;

        }
        public bool Update()
        {
            bool success;
            try
            {
                using (SqlConnection conn = new SqlConnection(Properties.Settings.Default." + txtConnectUse.Text + @"))
                {
                    conn.Open();
                    string sql = GetSQLSelect();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(sql, conn))
                    {
                        using (SqlCommandBuilder comm = new SqlCommandBuilder(adapter))
                        {
                            using (DataTable tb = new DataTable())
                            {
                                adapter.Fill(tb);
                                DataRow row = tb.NewRow();
                                if (tb.Rows.Count > 0)
                                {
                                    row = tb.Rows[0];
                                }
                                SetData(ref row);
                                if (row.RowState == DataRowState.Detached)
                                {
                                    tb.Rows.Add(row);
                                }
                                adapter.Update(tb);
                            }
                        }
                    }
                }
                success = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            return success;
        }
    }
";
            return strAll;
        }
    }
}
