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
            SetDefaultVariable();
        }
        private void SetDefaultVariable()
        {
            txtConnect.Text = Properties.Settings.Default["ConnectionStr"].ToString();
            cboType.SelectedIndex = 0;
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
                    if (cboType.SelectedIndex == 0)
                    {
                        txtData.Text = ReadStructureCS(dt);
                    }
                    if(cboType.SelectedIndex == 1)
                    {
                        txtData.Text = ReadStructureHTML(dt);
                    }
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
        string ReadStructureCS(DataTable tb)
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
        public string Update()
        {
            string msg="""";
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
                                msg=""Save "" + row["""+ txtKey.Text +@"""] +""Complete"";
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
            strAll += @"
    //Controllers And Methods
    public ActionResult "+txtClassName.Text+@"()
    {            
        ViewBag.DataList = new "+txtClassName.Text+@"().Read();
        return View();
    }
    public JsonResult Get" + txtClassName.Text+@"()
    {
        var v"+txtKey.Text+@" = Request.QueryString[""" + txtKey.Text +@"""] == null ? """" : Request.QueryString[""" + txtKey.Text+@"""].ToString();
        var data = new "+ txtClassName.Text+@"
        {
            "+txtKey.Text+@" = v"+txtKey.Text+ @"
        };
        return Json(data.Read(), JsonRequestBehavior.AllowGet);
    }
    [HttpPost]
    public ActionResult Set"+txtClassName.Text+@"("+txtClassName.Text+@" data)
    {
        string msg=data.Update();
        return Content(msg);
    }
    public ContentResult Del" +txtClassName.Text+@"()
    {
        string html = ""No Data To Delete"";
        if (Request.QueryString["""+txtKey.Text+@"""] != null)
        {
            var data = new "+txtClassName.Text+@"
            {
                "+txtKey.Text+@" = Request.QueryString["""+txtKey.Text+@"""].ToString()
            };
            if (data.Delete() == true)
            {
                html = ""Delete "" + data."+txtKey.Text+@" + "" Complete"";
            }
        }            
        return Content(html);
    }
";
            return strAll;
        }
        string ReadStructureHTML(DataTable tb)
        {
            string strAll = "";
            string strClear = "";
            string strLoad = "";
            string strSave = "";
            int cols = 0;
            foreach (DataColumn dc in tb.Columns)
            {
                if (cols == 0 || (cols % 2) == 0)
                {
                    if (cols > 0)
                    {
                        strAll += "</div>\r\n";
                    }
                    strAll += @"<div class=""row"">"+ "\r\n";
                }
                strAll += @"    <div class=""col-sm-6"">" + "\r\n";
                strAll += @"        " + dc.ColumnName + @"<br/>" + "\r\n";
                strAll += @"        <div style=""display:flex"">" + "\r\n";
                strAll += @"            <input type=""text"" class=""form-control"" id=""txt"+ dc.ColumnName+ @""" value="""" />" + "\r\n";
                strAll += @"        </div>" + "\r\n";
                strAll += @"    </div>" + "\r\n";

                strClear += "   $('#txt" + dc.ColumnName + @"').val('');" + "\r\n";
                strSave += "    " + dc.ColumnName + ": $('#txt" + dc.ColumnName + "').val(),\r\n";
                strLoad += "    $('#txt" + dc.ColumnName + @"').val(data."+ dc.ColumnName +");" + "\r\n";
                cols += 1;
            }
            if (cols > 0)
            {
                strAll += @"</div>" + "\r\n";
            }
            strAll += @"
<div style=""display:flex"">
    <div>
        <input type=""button"" id=""btnClear"" class=""btn btn-default"" value=""Clear"" onclick=""ClearData()""/>
    </div>
    <div>
        <input type=""button"" id=""btnSave"" class=""btn btn-success"" value=""Save"" onclick=""SaveData()"" />
    </div>
    <div>
        <input type=""button"" id=""btnDelete"" class=""btn btn-danger"" value=""Delete"" onclick=""DeleteData()"" />
    </div>
</div>
";
            strClear = @"
    function ClearData(){
        " + strClear+@"
    }
    ";
                strLoad = @"
    function ReadData(){
        let v"+txtKey.Text+@"=$('#txt"+txtKey.Text+@"').val();
        $.get('/" + txtController.Text + @"/Get"+ txtClassName.Text+@"?" + txtKey.Text+@"=' + v"+txtKey.Text+@").done(function(r){
            if(r.length>0){    
                let data=r[0];
                " + strLoad+ @"
            }
        });
    }
    ";
                strSave = @"
    function SaveData(){
        let obj={
            "+ strSave+ @"
        }
        let jsonText = JSON.stringify({ data: obj });
        
        $.ajax({
            url: ""@Url.Action(""Set"+txtClassName.Text+@""", """ + txtController.Text + @""")"",
            type: ""POST"",
            contentType: ""application/json"",
            data: jsonText,
            success: function(response) {
                if (response != null)
                {
                    alert(response);
                    window.location.reload();
                }
            },
            error: function(e) {
                alert(e.responseText);
            }
        });        
    }
";
            strAll += @"
<script type=""text/javascript"">
    $('#txt"+txtKey.Text+@"').on('change',function(){
        ReadData();
    });
    " + strClear+ @"
    " + strLoad + @"
    " + strSave + @"
    function DeleteData(){
        let v" + txtKey.Text + @"=$('#txt" + txtKey.Text + @"').val();
        $.get('/" + txtController.Text + @"/Del" + txtClassName.Text + @"?" + txtKey.Text + @"=' + v" + txtKey.Text + @").done(function(r){
            alert(r);
        });
    }
</script>
";
            return strAll;
        }
    }

}
