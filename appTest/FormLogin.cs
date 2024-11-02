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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public string tenDanNhap = "";
        public string loaiTK;
        Conn kn = new Conn();

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (cmbChonLoai.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn loại tài khoản");
                return;
            }
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
            tenDanNhap = txtName.Text;
            loaiTK = "";
            switch(cmbChonLoai.Text)
            {
                case "Quản lý":
                    loaiTK = "Admin";
                    break;
                case "Nhân viên":
                    loaiTK = "NhanVien";
                    break;
            }
            string query = $"select * from Qltk{loaiTK} where TenDangNhap = '{txtName.Text}' and MatKhau = '{txtPass.Text}'";
            DataSet ds = new DataSet();
            ds = kn.LayDuLieu(query);
            if (ds.Tables[0].Rows.Count == 1) 
            {
                MessageBox.Show("Đăng nhập thành công !");
                User currentUser = new User();
                currentUser.Username = txtName.Text; // Thay thế bằng tên đăng nhập thực tế.
                currentUser.Role = loaiTK; // Hoặc "nhân viên" tùy theo quyền của người dùng.

                UserContext.CurrentUser = currentUser;
                this.Hide();
                FormMain frm = new FormMain();
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại !");
            }
        }
    }
}
