using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace appTest.Class
{
    internal class Conn
    {
        private string conStr = @"Data Source=DONALDANH\SQLEXPRESS;Initial Catalog=QLBH;Integrated Security=True";
        private SqlConnection conn;
        //private DataTable dt;
        //private SqlCommand cmd;
        public Conn()
        {
            try
            {
                conn = new SqlConnection(conStr);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối" + ex.Message);
            }
        }
        public DataSet LayDuLieu(string query)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(ds);
                return ds;
            }
            catch 
            {
                return null;
            }
        }
        public bool ThucThi(string query)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                int r = cmd.ExecuteNonQuery();
                conn.Close();
                return r > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
