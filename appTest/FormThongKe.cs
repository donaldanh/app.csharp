using appTest.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace appTest
{
    public partial class FormThongKe : Form
    {
        public FormThongKe()
        {
            InitializeComponent();
        }

        Conn kn = new Conn();

        private void FormThongKe_Load(object sender, EventArgs e)
        {
            getcmb();
        }

        public void getcmb()
        {
            string query = "SELECT DISTINCT YEAR(NgayBan) AS Year FROM HDBan UNION SELECT DISTINCT YEAR(NgayNhap) AS Year FROM HDNhap;";
            DataSet ds = new DataSet();
            ds = kn.LayDuLieu(query);
            cmbNam.DataSource = ds.Tables[0];
            cmbNam.DisplayMember = "Year";
            cmbNam.ValueMember = "Year";
            cmbNam.SelectedIndex = 0;
        }

        public void Test(string nam)
        {
            // Kết nối đến cơ sở dữ liệu
            string connectionString = @"Data Source=MSI\SQLEXPRESS;Initial Catalog=QLBH;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                // Mở kết nối
                connection.Open();

                // Tạo câu truy vấn SQL để lấy dữ liệu từ hai bảng và tính toán tiền lãi
                string query = $@"SELECT Thang, Nam, SUM(TienBan - TienNhap) AS TienLai
                                FROM (
                                    SELECT MONTH(NgayBan) AS Thang, YEAR(NgayBan) AS Nam, SUM(TongTien) AS TienBan, 0 AS TienNhap
                                    FROM HDBan
                                    WHERE YEAR(NgayBan) = @Nam
                                    GROUP BY MONTH(NgayBan), YEAR(NgayBan)
                                    UNION ALL
                                    SELECT MONTH(NgayNhap) AS Thang, YEAR(NgayNhap) AS Nam, 0 AS TienBan, SUM(TongTien) AS TienNhap
                                    FROM HDNhap
                                    WHERE YEAR(NgayNhap) = @Nam
                                    GROUP BY MONTH(NgayNhap), YEAR(NgayNhap)
                                ) AS CombinedData
                                GROUP BY Thang, Nam
                                ORDER BY Nam, Thang;";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Nam", nam); // Năm bạn muốn thống kê

                SqlDataReader reader = command.ExecuteReader();

                // Đọc dữ liệu từ nguồn dữ liệu (reader) và thêm vào biểu đồ
                while (reader.Read())
                {
                    int thang = (int)reader["Thang"];
                    double tienLai = (double)reader["TienLai"];

                    // Thêm dữ liệu vào biểu đồ
                    chart1.Series["Month"].Points.AddXY(thang, tienLai);
                }

                // Đóng kết nối và hiển thị biểu đồ
                reader.Close();
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            chart1.Series["Month"].Points.Clear();
            Test(cmbNam.Text);
        }
    }
}
