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
    public partial class FormTaoTaiKhoan : Form
    {
        public FormTaoTaiKhoan()
        {
            InitializeComponent();
        }

        private void FormTaoTaiKhoan_Load(object sender, EventArgs e)
        {
            txtName.Enabled = false;
            txtPass.Enabled = false;
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }
        Conn kn = new Conn();
        string loaiTK = "";
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            switch (cmbLoaiTK.Text)
            {
                case "Quản lý":
                    loaiTK = "Admin";
                    break;
                case "Nhân viên":
                    loaiTK = "NhanVien";
                    break;
            }
            getData();
            txtName.Enabled = true;
            txtPass.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Vui lòng nhập tài khoản");
                txtName.Select();
                return;
            }
            if (string.IsNullOrEmpty(txtPass.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu");
            }
            string query = $"insert into Qltk{loaiTK} values ('{txtName.Text}','{txtPass.Text}')";
            bool kt = kn.ThucThi(query);
            if (kt)
                MessageBox.Show("Thêm mới thành công !");
            else
                MessageBox.Show("Thêm mới thất bại !");
            getData();
            clearText();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string query = $"delete from Qltk{loaiTK} where TenDangNhap = '{txtName.Text}'";
            bool kt = kn.ThucThi(query);
            if (kt)
                MessageBox.Show("Xóa thành công !");
            else
                MessageBox.Show("Xóa thất bại !");
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnThem.Enabled = true;
            txtName.Enabled = true;
            getData();
            clearText();
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            string query = $"update Qltk{loaiTK} set MatKhau = '{txtPass.Text}' where TenDangNhap = '{txtName.Text}'";
            bool kt = kn.ThucThi(query);
            if (kt)
                MessageBox.Show("Sửa thành công !");
            else
                MessageBox.Show("Sửa thất bại !");
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnThem.Enabled = true;
            txtName.Enabled = true;
            getData();
            clearText();
        }
        public void clearText()
        {
            txtName.Text = "";
            txtPass.Text = "";
        }
        public void getData()
        {
            string query = $"select * from Qltk{loaiTK}";
            DataSet ds = new DataSet();
            ds = kn.LayDuLieu(query);
            dgvTaoTK.DataSource = ds.Tables[0];
        }

        private void dgvTaoTK_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if( r >= 0 )
            {
                txtName.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnThem.Enabled = false;
                txtName.Text = dgvTaoTK.Rows[r].Cells["TenDangNhap"].Value.ToString();
                txtPass.Text = dgvTaoTK.Rows[r].Cells["MatKhau"].Value.ToString() ;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
