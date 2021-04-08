
using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace TestMySQL
{
    public class MMSCustomers
    {
        public int customer_id { get; set; }
        public string customer_name { get; set; }
        public string customer_addr { get; set; }
        public string customer_contact { get; set; }
        public string customer_tel { get; set; }
        public string customer_fax { get; set; }
        public string customer_taxid { get; set; }
        public int is_active { get; set; }

        public MMSCustomers()
        {
            Create();
        }
        private MMSCustomers GetData(MySqlDataReader rd)
        {
            MMSCustomers row = new MMSCustomers
            {
                customer_id = Main.CInt(rd["customer_id"]),
                customer_name = Main.CStr(rd["customer_name"]),
                customer_addr = Main.CStr(rd["customer_addr"]),
                customer_contact = Main.CStr(rd["customer_contact"]),
                customer_tel = Main.CStr(rd["customer_tel"]),
                customer_fax = Main.CStr(rd["customer_fax"]),
                customer_taxid = Main.CStr(rd["customer_taxid"]),
                is_active = Main.CInt(rd["is_active"]),

            };
            return row;
        }
        public void AddNew()
        {

            var vFormat = "____";
            var vNumber = 1;
            var vLastNo = Main.GetValueSQL(String.Format("SELECT Max(customer_id) FROM customers WHERE customer_id Like'{0}'", vFormat));
            if (vLastNo != "")
            {
                vNumber = Convert.ToInt32(vLastNo.PadRight(4)) + 1;
            }
            this.customer_id = vNumber;

        }
        private void SetData(ref DataRow row)
        {
            //row["customer_id"] = Main.GetDBString(this.customer_id);
            row["customer_name"] = Main.GetDBString(this.customer_name);
            row["customer_addr"] = Main.GetDBString(this.customer_addr);
            row["customer_contact"] = Main.GetDBString(this.customer_contact);
            row["customer_tel"] = Main.GetDBString(this.customer_tel);
            row["customer_fax"] = Main.GetDBString(this.customer_fax);
            row["customer_taxid"] = Main.GetDBString(this.customer_taxid);
            row["is_active"] = Main.GetDBString(this.is_active);

        }
        public List<MMSCustomers> Read()
        {
            List<MMSCustomers> lst = new List<MMSCustomers>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Properties.Settings.Default.ConnectionString))
                {
                    conn.Open();
                    using (MySqlDataReader reader = new MySqlCommand("SELECT * FROM customers ", conn).ExecuteReader())
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
            string sql = String.Format(" WHERE customer_id={0}", this.customer_id);
            return sql;
        }
        public string GetSQLSelect()
        {
            string sql = "SELECT * FROM customers " + GetSQLWhere();
            return sql;
        }
        public string GetSQLDelete()
        {
            string sql = "DELETE FROM customers " + GetSQLWhere();
            return sql;
        }
        public void Create()
        {
            this.customer_id = 0;
            this.customer_name = "";
            this.customer_addr = "";
            this.customer_contact = "";
            this.customer_tel = "";
            this.customer_fax = "";
            this.customer_taxid = "";
            this.is_active = 0;

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
                                msg = "Save " + row["customer_id"] + " Complete!";
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
