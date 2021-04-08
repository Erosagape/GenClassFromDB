using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace TestMySQL
{
    public static class CDB
    {
        private static MySqlConnection cn = new MySqlConnection();
        public static string ConnectionString { get; set; }
        public static string ErrorMessage { get; set; }
        public static bool IsConnected
        {
            get
            {
                ErrorMessage = "";
                try
                {
                    cn = new MySqlConnection
                    {
                        ConnectionString = ConnectionString
                    };
                    cn.Open();
                    return cn.State.Equals(System.Data.ConnectionState.Open);
                }
                catch (Exception e)
                {
                    ErrorMessage = e.Message;
                    return false;
                }
            }
        }
        public static System.Data.DataTable GetDataTable(string sql,bool forupdate=false)
        {
            ErrorMessage = "";
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                using(MySqlDataAdapter da=new MySqlDataAdapter(sql, cn))
                {
                    if (forupdate == true)
                    {
                        MySqlCommandBuilder cb = new MySqlCommandBuilder(da);
                    }
                    da.Fill(dt);
                }
            } catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            return dt;
        }
        public static MySqlConnection Connection
        {
            get
            {
                return cn;
            }
            set
            {
                cn = value;
            }
        }
        public static string ReadStructureCS(System.Data.DataTable tb, string tablename, string key, string classname, bool isitemno, bool isforweb)
        {
            string strField = "";
            string strLoad = "";
            string strDefault = "";
            string strSet = "";
            string strRead = "";
            string strSave = "";
            string strClear = "";
            string strGrid = "";
            foreach (System.Data.DataColumn dc in tb.Columns)
            {
                string strType = dc.DataType.FullName.ToString().Replace("System.", "");
                string fldName = @"""" + dc.ColumnName + @"""";
                if (dc.ColumnName != key)
                {
                    strClear += "        txt" + dc.ColumnName + @".Text="""";" + " \r\n";
                }
                switch (strType)
                {
                    case "Double":
                        strField += "       public double " + dc.ColumnName + " { get; set; } \r\n";
                        strLoad += "                " + dc.ColumnName + String.Format(" = Main.CDbl(rd[{0}]),\r\n", fldName);
                        strDefault += "         this." + dc.ColumnName + "=0;\r\n";
                        strSet += @"            row[" + fldName + "] = Main.GetDBDouble(this." + dc.ColumnName + ");\r\n";
                        strRead += "        txt" + dc.ColumnName + ".Text=data[0]." + dc.ColumnName + ".ToString(); \r\n";
                        strGrid += "        txt" + dc.ColumnName + @".Text=dataGridView1.Rows[e.RowIndex].Cells[""" + dc.ColumnName + @"""].Value.ToString();"+" \r\n";
                        strSave += "        " + dc.ColumnName + "=Main.CDbl(txt" + dc.ColumnName + ".Text), \r\n";
                        break;
                    case "Date":
                    case "DateTime":
                        strField += "       public DateTime " + dc.ColumnName + " { get; set; } \r\n";
                        strLoad += "                " + dc.ColumnName + String.Format(" = Main.CDate(rd[{0}]),\r\n", fldName);
                        strDefault += "         this." + dc.ColumnName + "=default(DateTime);\r\n";
                        strSet += @"            row[" + fldName + "] = Main.GetDBDate(this." + dc.ColumnName + ");\r\n";
                        strRead += "        txt" + dc.ColumnName + ".Text=data[0]." + dc.ColumnName + ".ToString(); \r\n";
                        strGrid += "        txt" + dc.ColumnName + @".Text=dataGridView1.Rows[e.RowIndex].Cells[""" + dc.ColumnName + @"""].Value.ToString(); "+"\r\n";
                        strSave += "        " + dc.ColumnName + "=Main.CDate(txt" + dc.ColumnName + ".Text), \r\n";
                        break;
                    case "Int32":
                    case "Integer":
                        strField += "       public int " + dc.ColumnName + " { get; set; } \r\n";
                        strLoad += "                " + dc.ColumnName + String.Format(" = Main.CInt(rd[{0}]),\r\n", fldName);
                        strDefault += "         this." + dc.ColumnName + "=0;\r\n";
                        strSet += @"            row[" + fldName + "] = Main.GetDBInteger(this." + dc.ColumnName + ");\r\n";
                        strRead += "        txt" + dc.ColumnName + ".Text=data[0]." + dc.ColumnName + ".ToString(); \r\n";
                        strGrid += "        txt" + dc.ColumnName + @".Text=dataGridView1.Rows[e.RowIndex].Cells[""" + dc.ColumnName + @"""].Value.ToString(); "+"\r\n";
                        strSave += "        " + dc.ColumnName + "=Main.CInt(txt" + dc.ColumnName + ".Text), \r\n";
                        break;
                    default:
                        strField += "       public string " + dc.ColumnName + " { get; set; } \r\n";
                        strLoad += "                " + dc.ColumnName + String.Format(" = Main.CStr(rd[{0}]),\r\n", fldName);
                        strDefault += "         this." + dc.ColumnName + "=" + @"""""" + ";\r\n";
                        strSet += @"            row[" + fldName + "] = Main.GetDBString(this." + dc.ColumnName + ");\r\n";
                        strRead += "        txt" + dc.ColumnName + ".Text=data[0]." + dc.ColumnName + ".ToString(); \r\n";
                        strGrid += "        txt" + dc.ColumnName + @".Text=dataGridView1.Rows[e.RowIndex].Cells[""" + dc.ColumnName + @"""].Value.ToString();"+" \r\n";
                        strSave += "        " + dc.ColumnName + "=txt" + dc.ColumnName + ".Text, \r\n";
                        break;
                }
            }
            string strAll = @"
    using System;
    using System.Collections.Generic;
    using System.Data;
    using MySql.Data.MySqlClient;
    public class " + classname + @"
    {
" + strField + @"        
        public " + classname + @"()
        {
            Create();
        }
        private " + classname + @" GetData(MySqlDataReader rd)
        {
            " + classname + @" row = new " + classname + @"
            {
" + strLoad + @"
            };
            return row;
        }
        public void AddNew()
        {
" + (isitemno ? @"
            var vNumber = 1;
            var vLastNo = Main.GetValueSQL(String.Format(""SELECT Max(ItemNo) as ItemNo FROM " + tablename + @" WHERE " + key + @" = '{0}'"", this." + key + @"));
            if (vLastNo != """")
            {
                vNumber = Convert.ToInt32(vLastNo) + 1;
            }
            this.ItemNo = vNumber;
" : @"
            var vFormat = ""____"";
            var vNumber = 1;
            var vLastNo = Main.GetValueSQL(String.Format(""SELECT Max(" + key + @") FROM " + tablename + @" WHERE " + key + @" Like'{0}'"", vFormat));
            if (vLastNo != """")
            {
                vNumber = Convert.ToInt32(vLastNo.PadRight(4)) + 1;
            }
            this." + key + @" = vNumber.ToString(""0000"");
            ") + @"
        }
        private void SetData(ref DataRow row)
        {
" + strSet + @"
        }
        public List<" + classname + @"> Read()
        {
            List<" + classname + @"> lst = new List<" + classname + @">();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Properties.Settings.Default.ConnectionString))
                {
                    conn.Open();
                    using (MySqlDataReader reader = new MySqlCommand(GetSQLSelect(), conn).ExecuteReader())
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
            string sql = (this." + key + @" != """" ? String.Format("" WHERE " + key + @"='{0}'"", this." + key + @") : """");
            " + (isitemno ? @"if(this.ItemNo>0) { sql +=String.Format("" AND ItemNo={0}"",this.ItemNo); }" : "") + @"
            return sql;
        }
        public string GetSQLSelect()
        {
            string sql = ""SELECT * FROM " + tablename + @" "" + GetSQLWhere();
            return sql;
        }
        public string GetSQLDelete()
        {
            string sql = ""DELETE FROM " + tablename + @" "" + GetSQLWhere();
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
                using (MySqlConnection conn = new MySqlConnection(Properties.Settings.Default.ConnectionString))
                {
                    conn.Open();
                    string sql = GetSQLDelete();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
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
        public string Update()
        {
            string msg="""";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Properties.Settings.Default.ConnectionString))
                {
                    conn.Open();
                    string sql = GetSQLSelect();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn))
                    {
                        using (MySqlCommandBuilder comm = new MySqlCommandBuilder(adapter))
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
                                msg=""Save "" + row[""" + key + @"""] +"" Complete!"";
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                msg=e.Message;
            }
            return msg;
        }
    }
";
            if (isforweb.Equals(false))
            {
                strAll += @"
    //Sample Using class for win app
    public void ClearData() {
" + strClear + @"
        " + (isitemno ? "txtItemNo.Text=0;\r\n" : "") + @"
    }
    public void RefreshGrid() {
        var data= new " + classname + @"().Read().ToList();
        dataGridView1.DataSource=data;
    }
    public void DeleteData() {
        var v" + key + @" = txt" + key + @".Text;
        " + (isitemno ? @"var vItemNo=Main.CInt(txtItemNo.Text);" : "") + @"
        var chk = new " + classname + @"
        {
            " + key + @" = v" + key + @"
            " + (isitemno ? @",ItemNo=vItemNo" : "") + @"
        }.Delete();
        if(chk) {
            MessageBox.Show(""Delete Complete!"");
        }
    }
    public void LoadGrid(DataGridViewCellEventArgs e) {
" + strGrid + @"
    }
    public void LoadData() {
        var v" + key + @" = txt" + key + @".Text;
        " + (isitemno ? @"var vItemNo=txtItemNo.Text;" : "") + @"
        var data = new " + classname + @"
        {
            " + key + @" = v" + key + @"
            " + (isitemno ? @",ItemNo=vItemNo" : "") + @"
        }.Read();
        if(data.Count>0) {
" + strRead + @"
        }
    }
    public void SaveData() {
        var data=new " + classname + @"() {
" + strSave + @"
        };
        data.Update();
    }
";
            }
            else
            {
                strAll += @"//Controllers And Methods
    public ActionResult " + classname + @"()
    {            
        if(IsLogin) 
        {
            ViewBag.DataList = new " + classname + @"().Read();        
        }
        return GetView('" + classname + @"');
    }
    public JsonResult Get" + classname + @"()
    {
        if(IsLogin)
        {
            var v" + key + @" = Request.QueryString[""" + key + @"""] == null ? """" : Request.QueryString[""" + key + @"""].ToString();
            " + (isitemno ? @"var vItemNo=Request.QueryString[""ItemNo""] == null ? 0 : Convert.ToInt32(Request.QueryString[""ItemNo""].ToString());" : "") + @"
            var data = new " + classname + @"
            {
                " + key + @" = v" + key + @"
                " + (isitemno ? @",ItemNo=vItemNo" : "") + @"
            };
            return GetJson(data.Read());
        }
        return GetJson(null);
    }
    [HttpPost]
    public ActionResult Set" + classname + @"(" + classname + @" data)
    {
        if(IsLogin)
        {
            if(data." + (isitemno ? "ItemNo==0" : key + @"==null") + @") {
                data.AddNew();
            }
            string msg=data.Update();
            return GetContent(msg);
        }
        return GetContent("");
    }
    public ContentResult Del" + classname + @"()
    {
        if(IsLogin)
        {
            string html = ""No Data To Delete"";
            if (Request.QueryString[""" + key + @"""] != null)
            {
                var data = new " + classname + @"
                {
                    " + key + @" = Request.QueryString[""" + key + @"""].ToString(),
                    " + (isitemno ? @"ItemNo=Convert.ToInt32(Request.QueryString[""ItemNo""].ToString())" : "") + @"
                };
                if (data.Delete() == true)
                {
                    html = ""Delete "" + data." + key + (isitemno ? @"+""#""+ data.ItemNo " : "") + @" + "" Complete"";
                }
            }            
            return GetContent(html);
        }
        return GetContent("");
    }
";
            }
            return strAll;
        }
    }
}
