using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace TestMySQL
{
    public class CWebUsers
    {
        public string user_id { get; set; }
        public string user_name { get; set; }
        public string user_password { get; set; }
        public string is_active { get; set; }

        public CWebUsers()
        {
            Create();
        }
        private CWebUsers GetData(MySqlDataReader rd)
        {
            CWebUsers row = new CWebUsers
            {
                user_id = Main.CStr(rd["user_id"]),
                user_name = Main.CStr(rd["user_name"]),
                user_password = Main.CStr(rd["user_password"]),
                is_active = Main.CStr(rd["is_active"]),

            };
            return row;
        }
        public void AddNew()
        {

            var vFormat = "____";
            var vNumber = 1;
            var vLastNo = Main.GetValueSQL(String.Format("SELECT Max(user_id) FROM web_users WHERE user_id Like'{0}'", vFormat));
            if (vLastNo != "")
            {
                vNumber = Convert.ToInt32(vLastNo.PadRight(4)) + 1;
            }
            this.user_id = vNumber.ToString("0000");

        }
        private void SetData(ref DataRow row)
        {
            row["user_id"] = Main.GetDBString(this.user_id);
            row["user_name"] = Main.GetDBString(this.user_name);
            row["user_password"] = Main.GetDBString(this.user_password);
            row["is_active"] = Main.GetDBString(this.is_active);

        }
        public List<CWebUsers> Read()
        {
            List<CWebUsers> lst = new List<CWebUsers>();
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
            string sql = (this.user_id != "" ? String.Format(" WHERE user_id='{0}'", this.user_id) : "");

            return sql;
        }
        public string GetSQLSelect()
        {
            string sql = "SELECT * FROM web_users " + GetSQLWhere();
            return sql;
        }
        public string GetSQLDelete()
        {
            string sql = "DELETE FROM web_users " + GetSQLWhere();
            return sql;
        }
        public void Create()
        {
            this.user_id = "";
            this.user_name = "";
            this.user_password = "";
            this.is_active = "";

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
            string msg = "";
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
                                msg = "Save " + row["user_id"] + " Complete!";
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
