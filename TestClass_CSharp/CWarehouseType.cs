using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace TestClass_CSharp
{
    public class CWarehouseType
    {
        public string WarehouseType { get; set; }
        public string WarehouseTypeName { get; set; }

        public CWarehouseType()
        {
            Create();
        }
        private CWarehouseType GetData(SqlDataReader rd)
        {
            CWarehouseType row = new CWarehouseType
            {
                WarehouseType = Main.CStr(rd["WarehouseType"]),
                WarehouseTypeName = Main.CStr(rd["WarehouseTypeName"]),

            };
            return row;
        }
        public void AddNew()
        {

            var vFormat = "____";
            var vNumber = 1;
            var vLastNo = Main.GetValueSQL(String.Format("SELECT Max(WarehouseType) FROM Mas_WarehouseType WHERE WarehouseType Like'{0}'", vFormat));
            if (vLastNo != "")
            {
                vNumber = Convert.ToInt32(vLastNo.PadRight(4)) + 1;
            }
            this.WarehouseType = vNumber.ToString("0000");

        }
        private void SetData(ref DataRow row)
        {
            row["WarehouseType"] = Main.GetDBString(this.WarehouseType);
            row["WarehouseTypeName"] = Main.GetDBString(this.WarehouseTypeName);

        }
        public List<CWarehouseType> Read()
        {
            List<CWarehouseType> lst = new List<CWarehouseType>();
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
            string sql = (this.WarehouseType != "" ? String.Format(" WHERE WarehouseType='{0}'", this.WarehouseType) : "");

            return sql;
        }
        public string GetSQLSelect()
        {
            string sql = "SELECT * FROM Mas_WarehouseType " + GetSQLWhere();
            return sql;
        }
        public string GetSQLDelete()
        {
            string sql = "DELETE FROM Mas_WarehouseType " + GetSQLWhere();
            return sql;
        }
        public void Create()
        {
            this.WarehouseType = "";
            this.WarehouseTypeName = "";

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
                                msg = "Save " + row["WarehouseType"] + " Complete!";
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

