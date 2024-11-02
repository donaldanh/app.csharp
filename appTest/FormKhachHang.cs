using appTest.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace appTest
{
    public partial class FormKhachHang : Form
    {
        public FormKhachHang()
        {
            InitializeComponent();
        }
        Conn kn = new Conn();
        private void FormKhachHang_Load(object sender, EventArgs e)
        {
            getData();
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
        }
        public void getData()
        {
            string query = "select * from KH";
            DataSet ds = new DataSet();
            ds = kn.LayDuLieu(query);
            dgvKH.DataSource = ds.Tables[0];
        }
        public void clear()
        {
            txtMaKH.Text = "";
            txtTenKH.Text = "";
            txtDiaChi.Text = "";
            txtDienThoai.Text = "";
        }

        private void dgvKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnThem.Enabled = false;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            txtMaKH.Enabled = false;
            int r = e.RowIndex;
            if (r >= 0)
            {
                txtMaKH.Text = dgvKH.Rows[r].Cells["MaKH"].Value.ToString();
                txtTenKH.Text = dgvKH.Rows[r].Cells["TenKH"].Value.ToString();
                txtDiaChi.Text = dgvKH.Rows[r].Cells["DiaChi"].Value.ToString();
                txtDienThoai.Text = dgvKH.Rows[r].Cells["DienThoai"].Value.ToString();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            clear();
            getData();
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnThem.Enabled = true;
            txtMaKH.Enabled = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string query = $"insert into KH values ('{txtMaKH.Text}','{txtTenKH.Text}','{txtDiaChi.Text}','{txtDienThoai.Text}')";
            bool kt = kn.ThucThi(query);
            if (kt)
                MessageBox.Show("Thêm mới thành công !");
            else
                MessageBox.Show("Thêm mới thất bại !");
            btnRefresh.PerformClick();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string query = $"delete from KH where MaKH = '{txtMaKH.Text}'";
            bool kt = kn.ThucThi(query);
            if (kt)
                MessageBox.Show("Xóa thành công !");
            else
                MessageBox.Show("Xóa thất bại !");
            btnRefresh.PerformClick();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string query = $"update KH set TenKH = '{txtTenKH.Text}', DiaChi = '{txtDiaChi.Text}', DienThoai = '{txtDienThoai.Text}' where MaKH = '{txtMaKH.Text}'";
            bool kt = kn.ThucThi(query);
            if (kt)
                MessageBox.Show("Sửa thành công !");
            else
                MessageBox.Show("Sửa thất bại !");
            btnRefresh.PerformClick();
        }
    }
}
