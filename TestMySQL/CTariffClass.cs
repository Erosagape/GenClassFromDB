using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
namespace TestMySQL
{
        public class CTariffClass
        {
            public int ID { get; set; }
            public string TRFCLS { get; set; }
            public string DSCTH { get; set; }
            public string DSCEN { get; set; }
            public string DATESTR { get; set; }
            public string DATEFIN { get; set; }

            public CTariffClass()
            {
                Create();
            }
            private CTariffClass GetData(MySqlDataReader rd)
            {
                CTariffClass row = new CTariffClass
                {
                    ID = Main.CInt(rd["ID"]),
                    TRFCLS = Main.CStr(rd["TRFCLS"]),
                    DSCTH = Main.CStr(rd["DSCTH"]),
                    DSCEN = Main.CStr(rd["DSCEN"]),
                    DATESTR = Main.CStr(rd["DATESTR"]),
                    DATEFIN = Main.CStr(rd["DATEFIN"]),

                };
                return row;
            }
            public void AddNew()
            {

                var vFormat = "____";
                var vNumber = 1;
                var vLastNo = Main.GetValueSQL(String.Format("SELECT Max(TRFCLS) FROM reftrc WHERE TRFCLS Like'{0}'", vFormat));
                if (vLastNo != "")
                {
                    vNumber = Convert.ToInt32(vLastNo.PadRight(4)) + 1;
                }
                this.TRFCLS = vNumber.ToString("0000");

            }
            private void SetData(ref DataRow row)
            {
                row["ID"] = Main.GetDBInteger(this.ID);
                row["TRFCLS"] = Main.GetDBString(this.TRFCLS);
                row["DSCTH"] = Main.GetDBString(this.DSCTH);
                row["DSCEN"] = Main.GetDBString(this.DSCEN);
                row["DATESTR"] = Main.GetDBString(this.DATESTR);
                row["DATEFIN"] = Main.GetDBString(this.DATEFIN);

            }
            public List<CTariffClass> Read()
            {
                List<CTariffClass> lst = new List<CTariffClass>();
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
                string sql = (this.TRFCLS != "" ? String.Format(" WHERE TRFCLS='{0}'", this.TRFCLS) : "");

                return sql;
            }
            public string GetSQLSelect()
            {
                string sql = "SELECT * FROM reftrc " + GetSQLWhere();
                return sql;
            }
            public string GetSQLDelete()
            {
                string sql = "DELETE FROM reftrc " + GetSQLWhere();
                return sql;
            }
            public void Create()
            {
                this.ID = 0;
                this.TRFCLS = "";
                this.DSCTH = "";
                this.DSCEN = "";
                this.DATESTR = "";
                this.DATEFIN = "";

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
                                    msg = "Save " + row["TRFCLS"] + " Complete!";
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
