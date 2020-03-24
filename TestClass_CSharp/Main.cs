using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace TestClass_CSharp
{
    static class Main
    {
        public static int CInt(object objCheck)
        {
            try
            {
                return Convert.ToInt32(objCheck);
            }
            catch
            {
                return default;
            }
        }
        public static Double CDbl(object objCheck)
        {
            try
            {
                return Convert.ToDouble(objCheck);
            }
            catch
            {
                return default;
            }
        }
        public static string CStr(object objCheck)
        {
            try
            {
                return Convert.ToString(objCheck);
            }
            catch
            {
                return default;
            }
        }
        public static DateTime CDate(object objCheck)
        {
            try
            {
                return Convert.ToDateTime(objCheck);
            }
            catch
            {
                return default;
            }
        }
        public static object GetDBInteger(object pVal, Int32 pDefault = 0)
        {
            if (pVal == null)
            {
                return pDefault;
            }
            return Convert.ToInt32(pVal);
        }
        public static object GetDBDouble(object pVal, Double pDefault = 0)
        {
            if (pVal == null)
            {
                return pDefault;
            }
            return Convert.ToDouble(pVal);
        }
        public static object GetDBString(object pVal, string pDefault = "")
        {
            if (pVal == null)
            {
                return pDefault;
            }
            return Convert.ToString(pVal);
        }
        public static object GetDBDate(DateTime pDate, bool pTodayAsDefault = false)
        {
            if (pDate.Year > 2000)
            {
                if (pDate.Year > 2500)
                {
                    return pDate.AddYears(-543);
                }
                else
                {
                    if (pDate.Year > 2200)
                    {
                        return DateTime.MinValue;
                    }
                    else
                    {
                        return pDate;
                    }
                }
            }
            else
            {
                if (pTodayAsDefault)
                {
                    return DateTime.Today;
                }
                else
                {
                    return System.DBNull.Value;
                }
            }
        }
        public static string ExecuteSQL(string sql)
        {
            string val = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.MainConnection))
                {
                    cn.Open();
                    using (SqlCommand cm = new SqlCommand(sql, cn))
                    {
                        cm.CommandType = CommandType.Text;
                        cm.ExecuteNonQuery();
                        val = "OK";
                    }
                    cn.Close();
                }
            }
            catch (Exception e)
            {
                val = e.Message;
            }
            return val;
        }
        public static string GetValueSQL(string sql)
        {
            string val = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.MainConnection))
                {
                    cn.Open();
                    using (SqlDataReader rd = new SqlCommand(sql, cn).ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            val = rd[0].ToString();
                        }
                        rd.Close();
                    }
                    cn.Close();
                }
            }
            catch (Exception e)
            {
                val = e.Message;
            }
            return val;
        }
        public static DataTable GetDataSQL(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                using(SqlConnection cn=new SqlConnection(Properties.Settings.Default.MainConnection))
                {
                    cn.Open();
                    using(SqlDataAdapter da=new SqlDataAdapter(sql, cn))
                    {
                        da.Fill(dt);
                    }
                    cn.Close();
                }
            }
            catch
            {

            }
            return dt;
        }
    }
}
