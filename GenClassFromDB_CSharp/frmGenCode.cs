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
            string strRead = "";
            string strSave = "";
            string strClear = "";
            foreach(DataColumn dc in tb.Columns)
            {
                string strType = dc.DataType.FullName.ToString().Replace("System.","");
                string fldName = @"""" + dc.ColumnName + @"""";
                if (dc.ColumnName != txtKey.Text)
                {
                    strClear += "        txt" + dc.ColumnName + @".Text="""";" + " \r\n";
                }
                switch (strType)
                {
                    case "Double":
                        strField += "       public double " + dc.ColumnName + " { get; set; } \r\n";
                        strLoad += "                " +dc.ColumnName+ String.Format(" = Main.CDbl(rd[{0}]),\r\n",fldName);
                        strDefault += "         this." + dc.ColumnName + "=0;\r\n";
                        strSet += @"            row["+fldName+"] = Main.GetDBDouble(this."+dc.ColumnName+ ");\r\n";
                        strRead += "        txt" + dc.ColumnName + ".Text=data[0]." + dc.ColumnName + ".ToString(); \r\n";
                        strSave += "        data." + dc.ColumnName + "=Main.CDbl(txt" + dc.ColumnName + ".Text); \r\n";
                        break;
                    case "Date":
                    case "DateTime":
                        strField += "       public DateTime " + dc.ColumnName + " { get; set; } \r\n";
                        strLoad += "                " + dc.ColumnName + String.Format(" = Main.CDate(rd[{0}]),\r\n", fldName);
                        strDefault += "         this." + dc.ColumnName + "=default(DateTime);\r\n";
                        strSet += @"            row[" + fldName + "] = Main.GetDBDate(this." + dc.ColumnName + ");\r\n";
                        strRead += "        txt" + dc.ColumnName + ".Text=data[0]." + dc.ColumnName + ".ToString(); \r\n";
                        strSave += "        data." + dc.ColumnName + "=Main.CDate(txt" + dc.ColumnName + ".Text); \r\n";
                        break;
                    case "Int32":
                    case "Integer":
                        strField += "       public int " + dc.ColumnName + " { get; set; } \r\n";
                        strLoad += "                " + dc.ColumnName + String.Format(" = Main.CInt(rd[{0}]),\r\n", fldName);
                        strDefault += "         this." + dc.ColumnName + "=0;\r\n";
                        strSet += @"            row[" + fldName + "] = Main.GetDBInteger(this." + dc.ColumnName + ");\r\n";
                        strRead += "        txt" + dc.ColumnName + ".Text=data[0]." + dc.ColumnName + ".ToString(); \r\n";
                        strSave += "        data." + dc.ColumnName + "=Main.CInt(txt" + dc.ColumnName + ".Text); \r\n";
                        break;
                    default:
                        strField += "       public string " + dc.ColumnName + " { get; set; } \r\n";
                        strLoad += "                " + dc.ColumnName + String.Format(" = Main.CStr(rd[{0}]),\r\n", fldName);
                        strDefault += "         this." + dc.ColumnName + "="+@""""""+";\r\n";
                        strSet += @"            row[" + fldName + "] = Main.GetDBString(this." + dc.ColumnName + ");\r\n";
                        strRead += "        txt" + dc.ColumnName + ".Text=data[0]." + dc.ColumnName + ".ToString(); \r\n";
                        strSave += "        data." + dc.ColumnName + "=txt" + dc.ColumnName + ".Text; \r\n";
                        break;
                }
            }
            string strAll = @"
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace Models 
{
    public class " + txtClassName.Text + @"
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
                using (SqlConnection conn = new SqlConnection(" + txtConnectUse.Text + @"))
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
                using (SqlConnection conn = new SqlConnection(" + txtConnectUse.Text + @"))
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
                using (SqlConnection conn = new SqlConnection(" + txtConnectUse.Text + @"))
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
}
";
            strAll += @"
    //Sample Using class
    public void ClearData() {
"+strClear+@"
        "+ (chkItemNo.Checked?"txtItemNo.Text=0;\r\n":"") + @"
    }
    public void RefreshGrid() {
        var data= new " + txtClassName.Text + @"().Read().ToList();
        dataGridView1.DataSource=data;
    }
    public void DeleteData() {
        var v" + txtKey.Text + @" = txt" + txtKey.Text + @".Text;
        " + (chkItemNo.Checked ? @"var vItemNo=Main.CInt(txtItemNo.Text);" : "") + @"
        var chk = new " + txtClassName.Text + @"
        {
            " + txtKey.Text + @" = v" + txtKey.Text + @"
            " + (chkItemNo.Checked ? @",ItemNo=vItemNo" : "") + @"
        }.Delete();
        if(chk) {
            MessageBox.Show(""Delete Complete!"");
        }
    }
    public void LoadData() {
        var v" + txtKey.Text + @" = txt" + txtKey.Text + @".Text;
        " + (chkItemNo.Checked ? @"var vItemNo=txtItemNo.Text;" : "") + @"
        var data = new " + txtClassName.Text + @"
        {
            " + txtKey.Text + @" = v" + txtKey.Text + @"
            " + (chkItemNo.Checked ? @",ItemNo=vItemNo" : "") + @"
        }.Read();
        if(data.Count>0) {
" + strRead + @"
        }
    }
    public void SaveData() {
        var data=new " + txtClassName.Text + @"();
" + strSave +@"
        data.Update();
    }
    //Controllers And Methods
    public ActionResult " + txtClassName.Text+ @"()
    {            
        ViewBag.Data" + (chkDetail.Checked ? "Detail" : "Header") + @" = new " + txtClassName.Text+ @"().Read();        
        return View();
    }
    public ActionResult Get" + txtClassName.Text+@"()
    {
        var v"+txtKey.Text+@" = Request.QueryString[""" + txtKey.Text +@"""] == null ? """" : Request.QueryString[""" + txtKey.Text+@"""].ToString();
        "+ (chkItemNo.Checked ? @"var vItemNo=Request.QueryString[""ItemNo""] == null ? 0 : Convert.ToInt32(Request.QueryString[""ItemNo""].ToString());":"") +@"
        var data = new "+ txtClassName.Text+@"
        {
            "+txtKey.Text+@" = v"+txtKey.Text+ @"
            "+ (chkItemNo.Checked ? @",ItemNo=vItemNo" : "") + @"
        };
        var json=JsonConvert.SerializeObject(data.Read());
        return Content(json,""text/json"");
    }
    [HttpPost]
    public ActionResult Set" + txtClassName.Text+@"("+txtClassName.Text+@" data)
    {
        if(data."+ (chkItemNo.Checked? "ItemNo==0" : txtKey.Text +@"==null") + @") {
            data.AddNew();
        }
        string msg=data.Update();
        return Content(msg,""text/html"");
    }
    public ActionResult Del" + txtClassName.Text+@"()
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
        return Content(html,""text/html"");
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
            string strValidate = "";
            string strListH = @"<div style=""overflow-x:auto"">" + "\r\n"+ @"<table class=""table table-responsive"">";
            string strListD = "";
            strListH += "\r\n   <thead>\r\n        <tr>\r\n<th>#</th>";
            int cols = 0;
            foreach (DataColumn dc in tb.Columns)
            {
                strListH += "\r\n           <th>" + dc.ColumnName + "</th>";
                                if (txtKey.Text == dc.ColumnName)
                {
                    strListD += "\r\n                   "+ @"<td><button class=""btn btn-warning"" onclick=""Set" + (chkDetail.Checked ? "Detail" : "Data") + @"('@item." + dc.ColumnName + (chkItemNo.Checked ? "',@item.ItemNo":"'")+ @")"">Edit</button></td>";
                } 
                if(dc.DataType.FullName.ToString().Replace("System.", "") == "DateTime")
                {
                    strListD += "\r\n                   <td>@Main.ShowDate(item." + dc.ColumnName + @")</td>";
                    strLoad += "        $('#txt" + dc.ColumnName + @"').val(CDateEN(data." + dc.ColumnName + "));" + "\r\n";
                } else
                {
                    strListD += "\r\n                   <td>@item." + dc.ColumnName + "</td>";
                    strLoad += "        $('#txt" + dc.ColumnName + @"').val(data." + dc.ColumnName + ");" + "\r\n";
                }
                if (cols == 0 || cols % 2 == 0)
                {
                    strAll += @"<div class=""row"">" + "\r\n";
                }
                strAll += @"    <div class=""col-sm-2"">" + "\r\n";
                strAll += @"        <label id=""lbl"+dc.ColumnName+@""">" + dc.ColumnName + @"</label>" + "\r\n";
                strAll += @"    </div>" + "\r\n";
                strAll += @"    <div class=""col-sm-4"">" + "\r\n";

                string strType = dc.DataType.FullName.ToString().Replace("System.", "");
                switch (strType)
                {
                    case "Date":
                    case "DateTime":
                        strAll += @"            <input type=""date"" class=""form-control"" id=""txt" + dc.ColumnName + @""" value="""" />" + "\r\n";
                        if(dc.ColumnName.ToLower().Equals("lastupdate")|| dc.ColumnName.ToLower().Equals("createdate"))
                        {
                            strClear += "        $('#txt" + dc.ColumnName + @"').val(CDateEN(GetToday()));" + "\r\n";
                        } else
                        {
                            strClear += "        $('#txt" + dc.ColumnName + @"').val(new Date());" + "\r\n";
                        }
                        break;
                    case "Int32":
                    case "Int16":
                    case "Integer":
                    case "Boolean":
                    case "Double":
                        strAll += @"            <input type=""number"" class=""form-control"" id=""txt" + dc.ColumnName + @""" value="""" />" + "\r\n";
                        if (dc.ColumnName.ToLower().Contains("status"))
                        {
                            strClear += "        $('#txt" + dc.ColumnName + @"').val('1');" + "\r\n";
                        } else
                        {
                            strClear += "        $('#txt" + dc.ColumnName + @"').val('0');" + "\r\n";
                        }
                        break;
                    default:
                        strAll += @"            <input type=""text"" class=""form-control"" id=""txt" + dc.ColumnName + @""" value="""" />" + "\r\n";
                        strClear += "        $('#txt" + dc.ColumnName + @"').val('');" + "\r\n";
                        break;
                }
                strAll += @"    </div>" + "\r\n";
                if ((cols + 1) % 2 == 0||(cols+1)==tb.Columns.Count)
                {
                    strAll += @"</div>" + "\r\n";
                }
                strSave += "            " + dc.ColumnName + ": $('#txt" + dc.ColumnName + "').val(),\r\n";                

                strValidate += "        if($('#txt" + dc.ColumnName + "').val()=='') {\r\n";
                strValidate += "            alert('" + dc.ColumnName + " must be entered');\r\n";
                strValidate += "            $('#txt" + dc.ColumnName + "').focus();\r\n";
                strValidate += "            return false;\r\n";
                strValidate += "        }\r\n";
                cols += 1;
            }
            strListH += "\r\n       </tr>\r\n   </thead>\r\n";
            strListH += @"
    <tbody>
        @foreach (var item in ViewBag.Data"+(chkDetail.Checked?"Detail":"Header")+@")
        {
            <tr>
"+ strListD + @"
            </tr>
        }
    </tbody>
";
            strListH += "</table>\r\n</div>\r\n";
            strAll += @"
<div style=""display:flex"">
    <div>
        <input type=""button"" id=""btnClear"+(chkDetail.Checked?"D":"")+ @""" class=""btn btn-default"" value=""Clear"" onclick=""Clear" + (chkDetail.Checked ? "Detail" : "Data") + @"()"" />
    </div>
    <div>
        <input type=""button"" id=""btnSave" + (chkDetail.Checked ? "D" : "") + @""" class=""btn btn-success"" value=""Save"" onclick=""Save" + (chkDetail.Checked ? "Detail" : "Data") + @"()"" disabled />
    </div>
    <div>
        <input type=""button"" id=""btnDelete" + (chkDetail.Checked ? "D" : "") + @""" class=""btn btn-danger"" value=""Delete"" onclick=""Delete" + (chkDetail.Checked ? "Detail" : "Data") + @"()"" disabled />
    </div>
</div>
";
            strClear = @"
    function Clear" + (chkDetail.Checked ? "Detail" : "Data") + @"(){
" + strClear+ @"
        $('#btnSave" + (chkDetail.Checked ? "D" : "") + @"').removeAttr('disabled');
        $('#btnDelete" + (chkDetail.Checked ? "D" : "") + @"').attr('disabled','disabled');
        delStorage('twt" + txtClassName.Text.ToLower() + @"');
    }
    ";
                strLoad = @"
    function Read" + (chkDetail.Checked ? "Detail" : "Data") + @"(){
        let v" + txtKey.Text+@"=$('#txt"+txtKey.Text+@"').val();  
        " + (chkItemNo.Checked? "let vItemNo=$('#txtItemNo').val();":"") + @"
        $.get('/" + txtController.Text + @"/Get"+ txtClassName.Text+@"?" + txtKey.Text+@"=' + v"+txtKey.Text+ (chkItemNo.Checked ? "+'&ItemNo='+ vItemNo":"")+ @").done(function(r){
            if(r.length>0){    
                let data=r[0];
                Load" + (chkDetail.Checked ? "Detail" : "Data") + @"(data);
            } else {
                alert('Data Not Found');
            }
        });
    }
    function Load" + (chkDetail.Checked ? "Detail" : "Data") + @"(data){
" + strLoad + @"
        $('#btnSave" + (chkDetail.Checked ? "D" : "") + @"').removeAttr('disabled');        
        $('#btnDelete" + (chkDetail.Checked ? "D" : "") + @"').removeAttr('disabled');
        setStorage('twt" + txtClassName.Text.ToLower() + @"',JSON.stringify(data));
    }
    ";
                strSave = @"
    function Save" + (chkDetail.Checked ? "Detail" : "Data") + @"(){
        if(!Validate" + (chkDetail.Checked ? "Detail" : "Data") + @"())
            return;
        let data={
" + strSave+ @"
        }
        setStorage('twt" + txtClassName.Text.ToLower() + @"',JSON.stringify(data));
        $.post('/" + txtController.Text + @"/Set" + txtClassName.Text+@"',data).done(function(response) {
                if (response !== """")
                {
                    alert(response);
                    location.reload(true);
                }
            }).fail(function(e) {
                alert(e.responseText);
            });        
    }
";
            strAll += strListH;
            strAll += @"
<script type=""text/javascript"">
    let saveObj" + (chkDetail.Checked ? "D" : "") + @" = getStorage('twt" + txtClassName.Text.ToLower()+ @"', '');
    if (saveObj" + (chkDetail.Checked ? "D" : "") + @" == '') {
        Clear" + (chkDetail.Checked ? "Detail" : "Data") + @"();
    } else {
        let data = JSON.parse(saveObj);
        Load" + (chkDetail.Checked ? "Detail" : "Data") + @"(data);
    }
    $('#txt" + (chkItemNo.Checked ? "ItemNo": txtKey.Text) + @"').on('keydown',function(e){
        if(e.which==13){
            Read" + (chkDetail.Checked ? "Detail" : "Data") + @"();
        }
    });
    function Set" + (chkDetail.Checked ? "Detail" : "Data") + @"(id" + (chkItemNo.Checked? ",no":"")+@") {
        $('#txt"+ txtKey.Text +@"').val(id);
        " + (chkItemNo.Checked ? "$('#txtItemNo').val(no);":"")+ @"
        Read" + (chkDetail.Checked ? "Detail" : "Data") + @"();
    }
    " + strClear+ @"
    " + strLoad + @"
    " + strSave + @"
    function Delete" + (chkDetail.Checked ? "Detail" : "Data") + @"(){
        let v" + txtKey.Text + @"=$('#txt" + txtKey.Text + @"').val();
        $.get('/" + txtController.Text + @"/Del" + txtClassName.Text + @"?" + txtKey.Text + @"=' + v" + txtKey.Text + @").done(function(r){
            alert(r);
            location.reload(true);
        });
    }
    function Validate" + (chkDetail.Checked ? "Detail" : "Data") + @"(){
" + strValidate+@"
        return true;
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
