
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace TestClass_CSharp
{
    public class CStockHeader
    {
        public string StockRefNo { get; set; }
        public DateTime EntryDate { get; set; }
        public string LocationID { get; set; }
        public DateTime CheckDate { get; set; }
        public int StockStatus { get; set; }
        public DateTime ApproveDate { get; set; }
        public DateTime CancelDate { get; set; }
        public int StockType { get; set; }
        public string StockDesc { get; set; }

        public CStockHeader()
        {
            Create();
        }
        private CStockHeader GetData(SqlDataReader rd)
        {
            CStockHeader row = new CStockHeader
            {
                StockRefNo = Main.CStr(rd["StockRefNo"]),
                EntryDate = Main.CDate(rd["EntryDate"]),
                LocationID = Main.CStr(rd["LocationID"]),
                CheckDate = Main.CDate(rd["CheckDate"]),
                StockStatus = Main.CInt(rd["StockStatus"]),
                ApproveDate = Main.CDate(rd["ApproveDate"]),
                CancelDate = Main.CDate(rd["CancelDate"]),
                StockType = Main.CInt(rd["StockType"]),
                StockDesc = Main.CStr(rd["StockDesc"]),

            };
            return row;
        }
        public void AddNew()
        {

            var vFormat = "____";
            var vNumber = 1;
            var vLastNo = Main.GetValueSQL(String.Format("SELECT Max(StockRefNo) FROM Acc_StockHeader WHERE StockRefNo Like'{0}'", vFormat));
            if (vLastNo != "")
            {
                vNumber = Convert.ToInt32(vLastNo.PadRight(4)) + 1;
            }
            this.StockRefNo = vNumber.ToString("0000");

        }
        private void SetData(ref DataRow row)
        {
            row["StockRefNo"] = Main.GetDBString(this.StockRefNo);
            row["EntryDate"] = Main.GetDBDate(this.EntryDate);
            row["LocationID"] = Main.GetDBString(this.LocationID);
            row["CheckDate"] = Main.GetDBDate(this.CheckDate);
            row["StockStatus"] = Main.GetDBInteger(this.StockStatus);
            row["ApproveDate"] = Main.GetDBDate(this.ApproveDate);
            row["CancelDate"] = Main.GetDBDate(this.CancelDate);
            row["StockType"] = Main.GetDBInteger(this.StockType);
            row["StockDesc"] = Main.GetDBString(this.StockDesc);

        }
        public List<CStockHeader> Read()
        {
            List<CStockHeader> lst = new List<CStockHeader>();
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
            string sql = (this.StockRefNo != "" ? String.Format(" WHERE StockRefNo='{0}'", this.StockRefNo) : "");

            return sql;
        }
        public string GetSQLSelect()
        {
            string sql = "SELECT * FROM Acc_StockHeader " + GetSQLWhere();
            return sql;
        }
        public string GetSQLDelete()
        {
            string sql = "DELETE FROM Acc_StockHeader " + GetSQLWhere();
            return sql;
        }
        public void Create()
        {
            this.StockRefNo = "";
            this.EntryDate = default(DateTime);
            this.LocationID = "";
            this.CheckDate = default(DateTime);
            this.StockStatus = 0;
            this.ApproveDate = default(DateTime);
            this.CancelDate = default(DateTime);
            this.StockType = 0;
            this.StockDesc = "";

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
                                msg = "Save " + row["StockRefNo"] + " Complete!";
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