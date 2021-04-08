using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace TestClass_CSharp
{
    public class CWarehouse
    {
        public string WarehouseID { get; set; }
        public string WarehouseName { get; set; }
        public string WarehouseAddress { get; set; }
        public string WarehouseType { get; set; }

        public CWarehouse()
        {
            Create();
        }
        private CWarehouse GetData(SqlDataReader rd)
        {
            CWarehouse row = new CWarehouse
            {
                WarehouseID = Main.CStr(rd["WarehouseID"]),
                WarehouseName = Main.CStr(rd["WarehouseName"]),
                WarehouseAddress = Main.CStr(rd["WarehouseAddress"]),
                WarehouseType = Main.CStr(rd["WarehouseType"]),

            };
            return row;
        }
        public void AddNew()
        {

            var vFormat = "____";
            var vNumber = 1;
            var vLastNo = Main.GetValueSQL(String.Format("SELECT Max(WarehouseID) FROM Mas_Warehouse WHERE WarehouseID Like'{0}'", vFormat));
            if (vLastNo != "")
            {
                vNumber = Convert.ToInt32(vLastNo.PadRight(4)) + 1;
            }
            this.WarehouseID = vNumber.ToString("0000");

        }
        private void SetData(ref DataRow row)
        {
            row["WarehouseID"] = Main.GetDBString(this.WarehouseID);
            row["WarehouseName"] = Main.GetDBString(this.WarehouseName);
            row["WarehouseAddress"] = Main.GetDBString(this.WarehouseAddress);
            row["WarehouseType"] = Main.GetDBString(this.WarehouseType);

        }
        public List<CWarehouse> Read()
        {
            List<CWarehouse> lst = new List<CWarehouse>();
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
            string sql = (this.WarehouseID != "" ? String.Format(" WHERE WarehouseID='{0}'", this.WarehouseID) : "");

            return sql;
        }
        public string GetSQLSelect()
        {
            string sql = "SELECT * FROM Mas_Warehouse " + GetSQLWhere();
            return sql;
        }
        public string GetSQLDelete()
        {
            string sql = "DELETE FROM Mas_Warehouse " + GetSQLWhere();
            return sql;
        }
        public void Create()
        {
            this.WarehouseID = "";
            this.WarehouseName = "";
            this.WarehouseAddress = "";
            this.WarehouseType = "";

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
                                msg = "Save " + row["WarehouseID"] + " Complete!";
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
