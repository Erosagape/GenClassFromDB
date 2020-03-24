using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace TestClass_CSharp
{
    public class CGLSet
    {
        public string AccSetCode { get; set; }
        public string AccSetName { get; set; }
        public string GLTypeCode { get; set; }

        public CGLSet()
        {
            Create();
        }
        private CGLSet GetData(SqlDataReader rd)
        {
            CGLSet row = new CGLSet
            {
                AccSetCode = Main.CStr(rd["AccSetCode"]),
                AccSetName = Main.CStr(rd["AccSetName"]),
                GLTypeCode = Main.CStr(rd["GLTypeCode"]),

            };
            return row;
        }
        public void AddNew()
        {

            var vFormat = "____";
            var vNumber = 1;
            var vLastNo = Main.GetValueSQL(String.Format("SELECT Max(AccSetCode) FROM Acc_GLSet WHERE AccSetCode Like'{0}'", vFormat));
            if (vLastNo != "")
            {
                vNumber = Convert.ToInt32(vLastNo.PadRight(4)) + 1;
            }
            this.AccSetCode = vNumber.ToString("0000");

        }
        private void SetData(ref DataRow row)
        {
            row["AccSetCode"] = Main.GetDBString(this.AccSetCode);
            row["AccSetName"] = Main.GetDBString(this.AccSetName);
            row["GLTypeCode"] = Main.GetDBString(this.GLTypeCode);

        }
        public List<CGLSet> Read()
        {
            List<CGLSet> lst = new List<CGLSet>();
            try
            {
                using (SqlConnection conn = new SqlConnection(TestClass_CSharp.Properties.Settings.Default.MainConnection))
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

            return sql;
        }
        public string GetSQLSelect()
        {
            string sql = "SELECT * FROM Acc_GLSet " + GetSQLWhere();
            return sql;
        }
        public string GetSQLDelete()
        {
            string sql = "DELETE FROM Acc_GLSet " + GetSQLWhere();
            return sql;
        }
        public void Create()
        {
            this.AccSetCode = "";
            this.AccSetName = "";
            this.GLTypeCode = "";

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
