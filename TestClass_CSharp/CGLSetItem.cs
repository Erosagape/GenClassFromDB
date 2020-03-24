using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace TestClass_CSharp
{
    public class CGLSetItem
    {
        public string AccSetCode { get; set; }
        public int ItemNo { get; set; }
        public string GLAccountCode { get; set; }
        public string AccSide { get; set; }
        public int Required { get; set; }
        public int CloseToItemNo { get; set; }

        public CGLSetItem()
        {
            Create();
        }
        private CGLSetItem GetData(SqlDataReader rd)
        {
            CGLSetItem row = new CGLSetItem
            {
                AccSetCode = Main.CStr(rd["AccSetCode"]),
                ItemNo = Main.CInt(rd["ItemNo"]),
                GLAccountCode = Main.CStr(rd["GLAccountCode"]),
                AccSide = Main.CStr(rd["AccSide"]),
                Required = Main.CInt(rd["Required"]),
                CloseToItemNo = Main.CInt(rd["CloseToItemNo"]),

            };
            return row;
        }
        public void AddNew()
        {

            var vNumber = 1;
            var vLastNo = Main.GetValueSQL(String.Format("SELECT Max(ItemNo) as ItemNo FROM Acc_GLSetItem WHERE AccSetCode = '{0}'", this.AccSetCode));
            if (vLastNo != "")
            {
                vNumber = Convert.ToInt32(vLastNo) + 1;
            }
            this.ItemNo = vNumber;

        }
        private void SetData(ref DataRow row)
        {
            row["AccSetCode"] = Main.GetDBString(this.AccSetCode);
            row["ItemNo"] = Main.GetDBInteger(this.ItemNo);
            row["GLAccountCode"] = Main.GetDBString(this.GLAccountCode);
            row["AccSide"] = Main.GetDBString(this.AccSide);
            row["Required"] = Main.GetDBInteger(this.Required);
            row["CloseToItemNo"] = Main.GetDBInteger(this.CloseToItemNo);

        }
        public List<CGLSetItem> Read()
        {
            List<CGLSetItem> lst = new List<CGLSetItem>();
            try
            {
                using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.MainConnection))
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
            string sql = (this.AccSetCode != "" ? String.Format(" WHERE AccSetCode='{0}'", this.AccSetCode) : "");
            if (this.ItemNo > 0) { sql += String.Format(" AND ItemNo={0}", this.ItemNo); }
            return sql;
        }
        public string GetSQLSelect()
        {
            string sql = "SELECT * FROM Acc_GLSetItem " + GetSQLWhere();
            return sql;
        }
        public string GetSQLDelete()
        {
            string sql = "DELETE FROM Acc_GLSetItem " + GetSQLWhere();
            return sql;
        }
        public void Create()
        {
            this.AccSetCode = "";
            this.ItemNo = 0;
            this.GLAccountCode = "";
            this.AccSide = "";
            this.Required = 0;
            this.CloseToItemNo = 0;

        }
        public bool Delete()
        {
            bool success;
            try
            {
                using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.MainConnection))
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
            string msg = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.MainConnection))
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
                                msg = "Save " + row["AccSetCode"] + " Complete!";
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                msg = e.Message;
            }
            return msg;
        }
    }
}
