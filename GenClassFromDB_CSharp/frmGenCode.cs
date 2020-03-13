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
                    DataTable tables = cn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                    txtTable.Items.Clear();
                    foreach(DataRow row in tables.Rows)
                    {
                        txtTable.Items.Add(row["TABLE_NAME"].ToString());
                    }
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
                        strField += "       public double " + dc.ColumnName + " { get; set; } \r\n";
                        strLoad += "                " +dc.ColumnName+ String.Format(" = Main.CDbl(rd[{0}]),\r\n",fldName);
                        strDefault += "         this." + dc.ColumnName + "=0;\r\n";
                        strSet += @"            row["+fldName+"] = Main.GetDBDouble(this."+dc.ColumnName+ ");\r\n";
                        break;
                    case "Date":
                    case "DateTime":
                        strField += "       public DateTime " + dc.ColumnName + " { get; set; } \r\n";
                        strLoad += "                " + dc.ColumnName + String.Format(" = Main.CDate(rd[{0}]),\r\n", fldName);
                        strDefault += "         this." + dc.ColumnName + "=default(DateTime);\r\n";
                        strSet += @"            row[" + fldName + "] = Main.GetDBDate(this." + dc.ColumnName + ");\r\n";
                        break;
                    case "Int32":
                    case "Integer":
                        strField += "       public int " + dc.ColumnName + " { get; set; } \r\n";
                        strLoad += "                " + dc.ColumnName + String.Format(" = Main.CInt(rd[{0}]),\r\n", fldName);
                        strDefault += "         this." + dc.ColumnName + "=0;\r\n";
                        strSet += @"            row[" + fldName + "] = Main.GetDBInteger(this." + dc.ColumnName + ");\r\n";
                        break;
                    default:
                        strField += "       public string " + dc.ColumnName + " { get; set; } \r\n";
                        strLoad += "                " + dc.ColumnName + String.Format(" = Main.CStr(rd[{0}]),\r\n", fldName);
                        strDefault += "         this." + dc.ColumnName + "="+@""""""+";\r\n";
                        strSet += @"            row[" + fldName + "] = Main.GetDBString(this." + dc.ColumnName + ");\r\n";
                        break;
                }
            }
            string strAll = @"
    using System.Data;
    using System.Data.SqlClient;
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
        public void AddNew()
        {
" + (chkItemNo.Checked ? @"
            var vNumber = 1;
            var vLastNo = Main.GetValueSQL(String.Format(""SELECT Max(ItemNo) as ItemNo FROM " + txtTable.Text + @" WHERE " + txtKey.Text + @" = '{0}'"", this." + txtKey.Text + @"));
            if (vLastNo != """")
            {
                vNumber = Convert.ToInt32(vLastNo) + 1;
            }
            this.ItemNo = vNumber;
" : @"
            var vFormat = ""____"";
            var vNumber = 1;
            var vLastNo = Main.GetValueSQL(String.Format(""SELECT Max("+ txtKey.Text +@") FROM "+txtTable.Text+@" WHERE "+txtKey.Text + @" Like'{0}'"", vFormat));
            if (vLastNo != """")
            {
                vNumber = Convert.ToInt32(vLastNo.PadRight(4)) + 1;
            }
            this." + txtKey.Text + @" = vNumber.ToString(""0000"");
            ") +@"
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
            " + (chkItemNo.Checked ? @"if(this.ItemNo>0) { sql +=String.Format("" AND ItemNo={0}"",this.ItemNo); }":"") +@"
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
                                msg=""Save "" + row["""+ txtKey.Text +@"""] +"" Complete!"";
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
        if(IsLogin) 
        {
            ViewBag.DataList = new "+txtClassName.Text+ @"().Read();        
        }
        return GetView('" + txtClassName.Text +@"');
    }
    public JsonResult Get" + txtClassName.Text+@"()
    {
        if(IsLogin)
        {
            var v"+txtKey.Text+@" = Request.QueryString[""" + txtKey.Text +@"""] == null ? """" : Request.QueryString[""" + txtKey.Text+@"""].ToString();
            "+ (chkItemNo.Checked ? @"var vItemNo=Request.QueryString[""ItemNo""] == null ? 0 : Convert.ToInt32(Request.QueryString[""ItemNo""].ToString());":"") +@"
            var data = new "+ txtClassName.Text+@"
            {
                "+txtKey.Text+@" = v"+txtKey.Text+ @"
                "+ (chkItemNo.Checked ? @",ItemNo=vItemNo" : "") + @"
            };
            return GetJson(data.Read());
        }
        return GetJson(null);
    }
    [HttpPost]
    public ActionResult Set"+txtClassName.Text+@"("+txtClassName.Text+@" data)
    {
        if(IsLogin)
        {
            if(data."+ (chkItemNo.Checked? "ItemNo==0" : txtKey.Text +@"==null") + @") {
                data.AddNew();
            }
            string msg=data.Update();
            return GetContent(msg);
        }
        return GetContent("");
    }
    public ContentResult Del" + txtClassName.Text+@"()
    {
        if(IsLogin)
        {
            string html = ""No Data To Delete"";
            if (Request.QueryString["""+txtKey.Text+@"""] != null)
            {
                var data = new "+txtClassName.Text+@"
                {
                    "+txtKey.Text+@" = Request.QueryString["""+txtKey.Text+@"""].ToString(),
                    "+ (chkItemNo.Checked ? @"ItemNo=Convert.ToInt32(Request.QueryString[""ItemNo""].ToString())" : "") + @"
                };
                if (data.Delete() == true)
                {
                    html = ""Delete "" + data."+txtKey.Text + (chkItemNo.Checked ? @"+""#""+ data.ItemNo " : "") + @" + "" Complete"";
                }
            }            
            return GetContent(html);
        }
        return GetContent("");
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
            string strListH = @"<table class=""table table-responsive"">";
            string strListD = "";
            strListH += "\r\n   <thead>\r\n        <tr>";
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
                strListH += "\r\n           <th>" + dc.ColumnName+ "</th>";
                if (txtKey.Text == dc.ColumnName)
                {
                    strListD += "\r\n                   "+@"<td><a onclick=""SetData('@item."+ dc.ColumnName + (chkItemNo.Checked ? "',@item.ItemNo":"'")+ @")"">" + "@item." + dc.ColumnName + "</a></td>";
                } else
                {
                    strListD += "\r\n                   <td>@item." + dc.ColumnName + "</td>";
                }
                strAll += @"    <div class=""col-sm-6"">" + "\r\n";
                strAll += @"        " + dc.ColumnName + @"<br/>" + "\r\n";
                strAll += @"        <div style=""display:flex"">" + "\r\n";

                string strType = dc.DataType.FullName.ToString().Replace("System.", "");
                switch (strType)
                {
                    case "Date":
                    case "DateTime":
                        strAll += @"            <input type=""date"" class=""form-control"" id=""txt" + dc.ColumnName + @""" value="""" />" + "\r\n";
                        strClear += "       $('#txt" + dc.ColumnName + @"').val(new Date());" + "\r\n";
                        break;
                    case "Int32":
                    case "Int16":
                    case "Integer":
                    case "Double":
                        strAll += @"            <input type=""number"" class=""form-control"" id=""txt" + dc.ColumnName + @""" value="""" />" + "\r\n";
                        strClear += "       $('#txt" + dc.ColumnName + @"').val('0');" + "\r\n";
                        break;
                    default:
                        strAll += @"            <input type=""text"" class=""form-control"" id=""txt" + dc.ColumnName + @""" value="""" />" + "\r\n";
                        strClear += "       $('#txt" + dc.ColumnName + @"').val('');" + "\r\n";
                        break;
                }
                strAll += @"        </div>" + "\r\n";
                strAll += @"    </div>" + "\r\n";

                strSave += "            " + dc.ColumnName + ": $('#txt" + dc.ColumnName + "').val(),\r\n";
                strLoad += "                $('#txt" + dc.ColumnName + @"').val(data."+ dc.ColumnName +");" + "\r\n";
                cols += 1;
            }
            if (cols > 0)
            {
                strAll += @"</div>" + "\r\n";
            }
            strListH += "\r\n       </tr>\r\n   </thead>\r\n";
            strListH += @"
    <tbody>
        @foreach (var item in ViewBag.DataList)
        {
            <tr>
"+ strListD + @"
            </tr>
        }
    </tbody>
";
            strListH += "</table>\r\n";
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
        " + (chkItemNo.Checked? "let vItemNo=$('#txtItemNo').val();":"") + @"
        $.get('/" + txtController.Text + @"/Get"+ txtClassName.Text+@"?" + txtKey.Text+@"=' + v"+txtKey.Text+ (chkItemNo.Checked ? "&ItemNo='+ vItemNo":"")+ @").done(function(r){
            if(r.length>0){    
                let data=r[0];
" + strLoad+ @"
            } else {
                alert('Data Not Found');
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
                if (response !== "")
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
            strAll += strListH;
            strAll += @"
<script type=""text/javascript"">
    $('#txt"+ (chkItemNo.Checked ? "ItemNo": txtKey.Text) +@"').on('change',function(){
        ReadData();
    });
    function SetData(id"+ (chkItemNo.Checked? ",no":"")+@") {
        $('#txt"+ txtKey.Text +@"').val(id);
        " + (chkItemNo.Checked ? "$('#txtItemNo').val(no);":"")+ @"
        ReadData();
    }
    " + strClear+ @"
    " + strLoad + @"
    " + strSave + @"
    function DeleteData(){
        let v" + txtKey.Text + @"=$('#txt" + txtKey.Text + @"').val();
        $.get('/" + txtController.Text + @"/Del" + txtClassName.Text + @"?" + txtKey.Text + @"=' + v" + txtKey.Text + @").done(function(r){
            if(r!=="") {
                alert(r);
                window.location.reload();
            }
        });
    }
</script>
";
            return strAll;
        }

        private void TxtTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSQL.Text = "SELECT * FROM " + txtTable.Items[txtTable.SelectedIndex];
        }

    }

}
