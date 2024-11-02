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
    public partial class FormNSX : Form
    {
        public FormNSX()
        {
            InitializeComponent();
        }
        Conn kn = new Conn();
        private void FormNSX_Load(object sender, EventArgs e)
        {
            getData();
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
        }
        public void getData()
        {
            string query = "select * from NCC";
            DataSet ds = new DataSet();
            ds = kn.LayDuLieu(query);
            dgvNCC.DataSource = ds.Tables[0];
        }
        public void clear()
        {
            txtMaNCC.Text = "";
            txtTenNCC.Text = "";
            txtDiaChi.Text = "";
            txtDienThoai.Text = "";
        }

        private void dgvNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnThem.Enabled = false;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            txtMaNCC.Enabled = false;
            int r = e.RowIndex;
            if (r >= 0)
            {
                txtMaNCC.Text = dgvNCC.Rows[r].Cells["MaNCC"].Value.ToString();
                txtTenNCC.Text = dgvNCC.Rows[r].Cells["TenNCC"].Value.ToString();
                txtDienThoai.Text = dgvNCC.Rows[r].Cells["DienThoai"].Value.ToString();
                txtDiaChi.Text = dgvNCC.Rows[r].Cells["DiaChi"].Value.ToString();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            clear();
            getData();
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnThem.Enabled = true;
            txtMaNCC.Enabled = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string query = $"insert into NCC values ('{txtMaNCC.Text}','{txtTenNCC.Text}','{txtDienThoai.Text}','{txtDiaChi.Text}')";
            bool kt = kn.ThucThi(query);
            if (kt)
                MessageBox.Show("Thêm mới thành công !");
            else
                MessageBox.Show("Thêm mới thất bại !");
            btnRefresh.PerformClick();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string query = $"delete from NCC where MaNCC = '{txtMaNCC.Text}'";
            bool kt = kn.ThucThi(query);
            if (kt)
                MessageBox.Show("Xóa thành công !");
            else
                MessageBox.Show("Xóa thất bại !");
            btnRefresh.PerformClick();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string query = $"update NCC set TenNCC = '{txtTenNCC.Text}', DienThoai = '{txtDienThoai.Text}', DiaChi = '{txtDiaChi.Text}' where MaNCC = '{txtMaNCC.Text}'";
            bool kt = kn.ThucThi(query);
            if (kt)
                MessageBox.Show("Sửa thành công !");
            else
                MessageBox.Show("Sửa thất bại !");
            btnRefresh.PerformClick();
        }

    }
}
