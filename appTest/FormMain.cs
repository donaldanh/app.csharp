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
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            customizeDesing();
        }
        private void customizeDesing()
        {
            panelSubMenuTapTin.Visible = false;
            panelSubMenuDanhMuc.Visible = false;
            panelSubMenuHoaDon.Visible = false;
        }
        private void hideSubMenu()
        {
            if(panelSubMenuTapTin.Visible == true)
            {
                panelSubMenuTapTin.Visible = false;
            }
            if(panelSubMenuDanhMuc.Visible == true)
            {
                panelSubMenuDanhMuc.Visible=false;
            }
            if(panelSubMenuHoaDon.Visible == true)
            {
                panelSubMenuHoaDon.Visible=false;
            }
        }
        private void showSubMenu(Panel subMenu)
        {
            if(subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;
            }
        }

        private void btnTapTin_Click(object sender, EventArgs e)
        {
            showSubMenu(panelSubMenuTapTin);
        }

        private void btnTaoTaiKhoan_Click(object sender, EventArgs e)
        {
            // code ...
            openChildForm(new FormTaoTaiKhoan());
            //
            hideSubMenu();
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            // code ...
            this.Hide();
            FormLogin frm = new FormLogin();
            frm.ShowDialog();
            //
            hideSubMenu();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            // code ...
            Application.Exit();
            //
            hideSubMenu();
        }

        private void btnDanhMuc_Click(object sender, EventArgs e)
        {
            showSubMenu(panelSubMenuDanhMuc);
        }

        private void btnHangHoa_Click(object sender, EventArgs e)
        {
            // code ...
            openChildForm(new FormHangHoa());
            //
            hideSubMenu();
        }

        private void btnNSX_Click(object sender, EventArgs e)
        {
            // code ...
            openChildForm(new FormNSX());
            //
            hideSubMenu();
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            // code ...
            openChildForm(new FormNV());
            //
            hideSubMenu();
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            // code ...
            openChildForm(new FormKhachHang());
            //
            hideSubMenu();
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            showSubMenu(panelSubMenuHoaDon);
        }

        private void btnPhieuNhap_Click(object sender, EventArgs e)
        {
            // code ...
            openChildForm(new FormNhap());
            //
            hideSubMenu();
        }

        private void btnPhieuXuat_Click(object sender, EventArgs e)
        {
            // code ...
            openChildForm(new FormXuat());
            //
            hideSubMenu();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            // code ...
            openChildForm(new FormThongKe());
            //
            hideSubMenu();
        }

        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if(activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            User currentUser = UserContext.CurrentUser;
            if (currentUser.Role == "Admin")
            {
                // Hiển thị các chức năng dành cho admin
                btnTaoTaiKhoan.Enabled = true;
                btnPhieuNhap.Enabled = true;
                btnNhanVien.Enabled = true;
                btnNSX.Enabled = true;
            }
            else
            {
                // Ẩn các chức năng dành cho nhân viên
                btnTaoTaiKhoan.Enabled = false;
                btnPhieuNhap.Enabled = false;
                btnNhanVien.Enabled = false;
                btnNSX.Enabled = false; 
                this.Show();
            }

        }

        
    }
}
