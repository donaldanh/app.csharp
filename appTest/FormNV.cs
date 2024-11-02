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
using System.Xml.Linq;

namespace appTest
{
    public partial class FormNV : Form
    {
        public FormNV()
        {
            InitializeComponent();
        }
        Conn kn = new Conn();

        private void FormNV_Load(object sender, EventArgs e)
        {
            getData();
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
        }
        public void getData()
        {
            string query = "select * from NV";
            DataSet ds = new DataSet();
            ds = kn.LayDuLieu(query);
            dgvNV.DataSource = ds.Tables[0];
        }
        public void clear()
        {
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            txtDiaChi.Text = "";
            txtDienThoai.Text = "";
        }

        private void dgvNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnThem.Enabled = false;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            txtMaNV.Enabled = false;
            int r = e.RowIndex;
            if( r >= 0 )
            {
                txtMaNV.Text = dgvNV.Rows[r].Cells["MaNV"].Value.ToString();
                txtTenNV.Text = dgvNV.Rows[r].Cells["TenNV"].Value.ToString();
                txtDiaChi.Text = dgvNV.Rows[r].Cells["DiaChi"].Value.ToString();
                txtDienThoai.Text = dgvNV.Rows[r].Cells["DienThoai"].Value.ToString();
                cmbGioiTinh.Text = dgvNV.Rows[r].Cells["GioiTinh"].Value.ToString();
                dtpDate.Value = (DateTime)dgvNV.Rows[r].Cells["NgaySinh"].Value;
                numericUpDown1.Value = (int)dgvNV.Rows[r].Cells["MaTK"].Value;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            clear();
            getData();
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnThem.Enabled = true;
            txtMaNV.Enabled = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string query = $"insert into NV values ('{txtMaNV.Text}','{txtTenNV.Text}','{cmbGioiTinh.Text}','{txtDiaChi.Text}','{txtDienThoai.Text}','{dtpDate.Value}',{numericUpDown1.Value})";
            bool kt = kn.ThucThi(query);
            if (kt)
                MessageBox.Show("Thêm mới thành công !");
            else
                MessageBox.Show("Thêm mới thất bại !");
            btnRefresh.PerformClick();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string query = $"delete from NV where MaNV = '{txtMaNV.Text}'";
            bool kt = kn.ThucThi(query);
            if (kt)
                MessageBox.Show("Xóa thành công !");
            else
                MessageBox.Show("Xóa thất bại !");
            btnRefresh.PerformClick();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string query = $"update NV set TenNV = '{txtTenNV.Text}', GioiTinh ='{cmbGioiTinh.Text}', DiaChi = '{txtDiaChi.Text}', DienThoai = '{txtDienThoai.Text}', NgaySinh = '{dtpDate.Value}', MaTK = {numericUpDown1.Value} where MaNV = '{txtMaNV.Text}'";
            bool kt = kn.ThucThi(query);
            if (kt)
                MessageBox.Show("Sửa thành công !");
            else
                MessageBox.Show("Sửa thất bại !");
            btnRefresh.PerformClick();
        }

        private void dgvNV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
