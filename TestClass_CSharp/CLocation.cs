using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace TestClass_CSharp
{
    public class CLocation
    {
        public string LocationID { get; set; }
        public string LocationName { get; set; }
        public string WarehouseID { get; set; }

        public CLocation()
        {
            Create();
        }
        private CLocation GetData(SqlDataReader rd)
        {
            CLocation row = new CLocation
            {
                LocationID = Main.CStr(rd["LocationID"]),
                LocationName = Main.CStr(rd["LocationName"]),
                WarehouseID = Main.CStr(rd["WarehouseID"]),

            };
            return row;
        }
        public void AddNew()
        {

            var vFormat = "____";
            var vNumber = 1;
            var vLastNo = Main.GetValueSQL(String.Format("SELECT Max(LocationID) FROM Mas_Location WHERE LocationID Like'{0}'", vFormat));
            if (vLastNo != "")
            {
                vNumber = Convert.ToInt32(vLastNo.PadRight(4)) + 1;
            }
            this.LocationID = vNumber.ToString("0000");

        }
        private void SetData(ref DataRow row)
        {
            row["LocationID"] = Main.GetDBString(this.LocationID);
            row["LocationName"] = Main.GetDBString(this.LocationName);
            row["WarehouseID"] = Main.GetDBString(this.WarehouseID);

        }
        public List<CLocation> Read()
        {
            List<CLocation> lst = new List<CLocation>();
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
            string sql = (this.LocationID != "" ? String.Format(" WHERE LocationID='{0}'", this.LocationID) : "");

            return sql;
        }
        public string GetSQLSelect()
        {
            string sql = "SELECT * FROM Mas_Location " + GetSQLWhere();
            return sql;
        }
        public string GetSQLDelete()
        {
            string sql = "DELETE FROM Mas_Location " + GetSQLWhere();
            return sql;
        }
        public void Create()
        {
            this.LocationID = "";
            this.LocationName = "";
            this.WarehouseID = "";

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
                                msg = "Save " + row["LocationID"] + " Complete!";
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
