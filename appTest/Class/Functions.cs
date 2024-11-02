using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appTest.Class
{
    class Functions
    {
        public static string conStr = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=QLBH;Integrated Security=True";
        public static SqlConnection con;
        public static string GetFieldValues(string sql)
        {
            con = new SqlConnection(conStr);
            con.Open();
            string ma = "";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
                ma = reader.GetValue(0).ToString();
            reader.Close();
            con.Close();
            return ma;

        }
    }
}
