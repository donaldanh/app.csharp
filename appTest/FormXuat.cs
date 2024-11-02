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
    public partial class FormXuat : Form
    {
        private User currentUser = UserContext.CurrentUser;
        public FormXuat()
        {
            InitializeComponent();
        }
        Conn kn = new Conn();
        private void FormXuat_Load(object sender, EventArgs e)
        {
            panel5.Visible = false;
            btnSua.Visible = false;
            btnXoa.Visible = false;
            btnSuaHD.Enabled = false;
            btnXoaHD.Enabled = false;
            txtMaH.Enabled = false;


            if (currentUser.Role == "Admin")
            {
                getcmb(cmbTenNV, "TenNV", "MaNV", "NV");
            }
            else
            {
                getcmbNV();
            }

            getDataHD();
            getcmb(cmbMaHD, "MaHDBan", "MaHDBan", "HDBan");
            
            getcmb(cmbTenKH, "TenKH", "MaKH", "KH");
        }

        // function 

        public void upLoadSL(string MaH)
        {
            string query = $"update Hang set SoLuong = SoLuong - (select sum(SoLuong) from CTHDBan where MaH = '{MaH}' ) where MaH = '{MaH}'";
            kn.ThucThi(query);
        }

            // các hàm lấy dữ liệu cho datagridview
        public void getDataHD()
        {
            string query = "select MaHDBan, NgayBan, TongTien from HDBan";
            DataSet ds = new DataSet();
            ds = kn.LayDuLieu(query);
            dgvHD.DataSource = ds.Tables[0];
        }
        public void getDataCtHD(string MaHD)
        {
            string query = $"select CTHDBan.MaHDBan, Hang.TenH, Hang.MaH, Hang.GhiChu, CTHDBan.SoLuong, Hang.DonGiaBan, CTHDBan.ThanhTien from CTHDBan inner join Hang on CTHDBan.MaH = Hang.MaH where CTHDBan.MaHDBan = '{MaHD}'";
            DataSet ds = new DataSet();
            ds = kn.LayDuLieu(query);
            dgvCTHD.DataSource = ds.Tables[0];
        }

            // các hàm lấy dữ liệu cho comboBox
        public void getcmb(ComboBox cmb, string ten, string ma, string table)
        {
            string query = $"select * from {table}";
            DataSet ds = new DataSet();
            ds = kn.LayDuLieu(query);
            cmb.DataSource = ds.Tables[0];
            cmb.DisplayMember = ten;
            cmb.ValueMember = ma;
            cmb.SelectedIndex = -1;
        }

        public void getcmbNV()
        {
            if(currentUser.Role == "NhanVien")
            {
                string query = $"select TenNV, MaNV from NV inner join QltkNhanVien on NV.MaTK = QltkNhanVien.Id where TenDangNhap = '{currentUser.Username}'";
                DataSet ds = new DataSet();
                ds = kn.LayDuLieu(query);
                cmbTenNV.DataSource = ds.Tables[0];
                cmbTenNV.DisplayMember = "TenNV";
                cmbTenNV.ValueMember = "MaNV";
            }
        }

        public void getcmbTenH(string MaHD)
        {
            string query = $"select MaH, TenH from Hang where MaH not in (select MaH from CTHDBan where MaHDBan = '{MaHD}')";
            DataSet ds = new DataSet();
            ds = kn.LayDuLieu(query);
            cmbTenH.DataSource = ds.Tables[0];
            cmbTenH.DisplayMember = "TenH";
            cmbTenH.ValueMember = "MaH";
            cmbTenH.SelectedIndex = -1;
        }

            // hàm tẩy trắng
        public void clear()
        {
            cmbMaHD.Text = string.Empty;
            cmbTenNV.Text = string.Empty;
            cmbTenKH.Text = string.Empty;   
            dtpNgayBan.Text = string.Empty;
            txtDiaChi.Text = string.Empty;
            txtTongTien.Text = string.Empty;
            txtDienThoai.Text = string.Empty;
            cmbTenH.Text = string.Empty;
            nudSoLuong.Value = 0;
            txtGiaBan.Text = string.Empty;
            txtGhiChu.Text = string.Empty;
            txtMaH.Text = string.Empty;
        }

            // upload
        public void uploadHD(string MaHD)
        {
            string query = $"update HDBan set TongTien = (select sum(ThanhTien) from CTHDBan inner join HDBan on CTHDBan.MaHDBan = HDBan.MaHDBan where CTHDBan.MaHDBan = '{MaHD}') where MaHDBan = '{MaHD}'";
            kn.ThucThi(query);
        }

        // key press - làm cho các textBox chỉ được nhận các giá trị float

        private void txtTongTien_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Kiểm tra nếu ký tự không phải là số hoặc dấu chấm thập phân và không phải là phím Backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true; // Loại bỏ ký tự không hợp lệ
            }

            // Kiểm tra nếu đã có dấu chấm thập phân và ký tự tiếp theo cũng là dấu chấm thập phân
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains('.'))
            {
                e.Handled = true; // Loại bỏ ký tự không hợp lệ
            }
        }

        private void txtGiaBan_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Kiểm tra nếu ký tự không phải là số hoặc dấu chấm thập phân và không phải là phím Backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true; // Loại bỏ ký tự không hợp lệ
            }

            // Kiểm tra nếu đã có dấu chấm thập phân và ký tự tiếp theo cũng là dấu chấm thập phân
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains('.'))
            {
                e.Handled = true; // Loại bỏ ký tự không hợp lệ
            }
        }

        

        //selected index change

        private void cmbMaHD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbMaHD.SelectedIndex != -1)
            {
                string str1 = $"select TenNV from NV inner join HDBan on NV.MaNV = HDBan.MaNV where MaHDBan = '{cmbMaHD.SelectedValue}'";
                cmbTenNV.Text = Functions.GetFieldValues(str1);
                string str2 = $"select NgayBan from HDBan where MaHDBan = '{cmbMaHD.SelectedValue}'";
                string ngayBanStr = Functions.GetFieldValues(str2);

                if (DateTime.TryParse(ngayBanStr, out DateTime ngayBan))
                {
                    // Chuyển đổi thành công, thiết lập giá trị cho DateTimePicker
                    dtpNgayBan.Value = ngayBan;
                }
                string str3 = $"select TongTien from HDBan where MaHDBan = '{cmbMaHD.SelectedValue}'";
                txtTongTien.Text = Functions.GetFieldValues(str3);
                string str4 = $"select TenKH from KH inner join HDBan on KH.MaKH = HDBan.MaKH where MaHDBan = '{cmbMaHD.SelectedValue}'";
                cmbTenKH.Text = Functions.GetFieldValues(str4);
            }
        }

        private void cmbTenKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTenKH.SelectedIndex != -1)
            {
                string strDC = $"select DiaChi from KH where MaKH = '{cmbTenKH.SelectedValue}'";
                txtDiaChi.Text = Functions.GetFieldValues(strDC);
                string strDT = $"select DienThoai from KH where MaKH = '{cmbTenKH.SelectedValue}'";
                txtDienThoai.Text = Functions.GetFieldValues(strDT);
            }
        }

        // button

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReFresh_Click(object sender, EventArgs e)
        {
            dgvHD.DataSource = null;
            getDataHD();
            btnXoaHD.Enabled = false;
            btnSuaHD.Enabled = false;
            btnThemHD.Enabled = true;
            btnThemSP.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            cmbMaHD.Enabled = true;
            cmbTenH.Enabled = true;
            txtTongTien.Enabled = false;
            getcmb(cmbMaHD, "MaHDBan", "MaHDBan", "HDBan");
            getcmb(cmbTenH, "TenH", "MaH", "Hang");
            dgvCTHD.DataSource = null;
            clear();

        }

            // các button của hóa đơn 

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if( cmbMaHD.Text != string.Empty )
            {
                string query = $"select MaHDBan, NgayBan, TongTien from HDBan where MaHDBan = '{cmbMaHD.Text}'";
                DataSet ds = new DataSet();
                ds = kn.LayDuLieu(query);
                dgvHD.DataSource = ds.Tables[0];
            }
            else
            {
                MessageBox.Show("Vui lòng nhập hoặc chọn mã hóa đơn");
                cmbMaHD.Select();
            }
        }

        private void btnThemHD_Click(object sender, EventArgs e)
        {
            string query = $"insert into HDBan (MaHDBan, MaNV, NgayBan, MaKH) values ('{cmbMaHD.Text}','{cmbTenNV.SelectedValue}','{dtpNgayBan.Value}','{cmbTenKH.SelectedValue}')";
            bool kt = kn.ThucThi(query);
            if (kt)
                MessageBox.Show("Thêm mới hóa đơn thành công !");
            else
                MessageBox.Show("Thêm mới hóa đơn thất bại !");
            btnReFresh.PerformClick();
        }

        private void btnSuaHD_Click(object sender, EventArgs e)
        {
            string query = $"update HDBan set MaNV = '{cmbTenNV.SelectedValue}', NgayBan ='{dtpNgayBan.Value}', MaKH = '{cmbTenKH.SelectedValue}', TongTien = '{txtTongTien.Text}' where MaHDBan = '{cmbMaHD.SelectedValue}'";
            bool kt = kn.ThucThi(query);
            if (kt)
                MessageBox.Show("Sửa hóa đơn thành công !");
            else
                MessageBox.Show("Sửa hóa đơn thất bại !");
            btnReFresh.PerformClick();
        }

        private void btnXoaHD_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string q = $"delete from CTHDBan where MaHDBan = {cmbMaHD.Text}";
                bool k = kn.ThucThi(q);
                if (k)
                {
                    MessageBox.Show("đã xóa hết CHTHD Nhập có MaHD :: " + cmbMaHD.Text);
                    string query = $"delete from HDBan where MaHDBan = '{cmbMaHD.SelectedValue}'";
                    bool kt = kn.ThucThi(query);
                    if (kt)
                        MessageBox.Show("Xóa thành công !");
                    else
                        MessageBox.Show("Xóa thất bại !");
                    btnReFresh.PerformClick();
                }
                else
                {
                    MessageBox.Show("Không thể xóa được CTHD cúa HD có MaHD :: " + cmbMaHD.Text);
                }
            }
        }

            // các button của CTHD 

        private void btnThemSP_Click(object sender, EventArgs e)
        {
            string str = $"select SoLuong from Hang where MaH = '{cmbTenH.SelectedValue.ToString()}'";
            var slHang = Functions.GetFieldValues(str);
            int sl = int.Parse(slHang);
            if(sl < (int)nudSoLuong.Value)
            {
                MessageBox.Show("Không đủ số lượng để xuất, số lượng tối đa :: "+ slHang);
            }
            else
            {
                string query = $"insert into CTHDBan (MaHDBan, MaH, SoLuong) values ('{cmbMaHD.Text}','{cmbTenH.SelectedValue}','{nudSoLuong.Value}')";
                bool kt = kn.ThucThi(query);
                if (kt)
                    MessageBox.Show("Thêm mới chi tiết hóa đơn thành công !");
                else
                    MessageBox.Show("Thêm mới chi tiết hóa đơn thất bại !");
                upLoadSL(cmbTenH.SelectedValue.ToString());
                uploadHD(cmbMaHD.Text);
                btnReFresh.PerformClick();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string str = $"select SoLuong from Hang where MaH = '{txtMaH.Text}'";
            var slHang = Functions.GetFieldValues(str);
            int sl = int.Parse(slHang);
            string str1 = $"select SoLuong from CTHDBan where MaHDBan = '{cmbMaHD.SelectedValue}' and MaH = '{txtMaH.Text}'";
            var slcthdb = Functions.GetFieldValues(str1);
            int oldslb = int.Parse(slcthdb);
            int change = (int)nudSoLuong.Value - oldslb;
            if (sl < change)
            {
                MessageBox.Show("Không đủ số lượng để xuất, số lượng tối đa :: " + slHang);
            }
            else
            {
                string query = $"update CTHDBan set SoLuong = {nudSoLuong.Value} where MaHDBan = '{cmbMaHD.SelectedValue}' and MaH = '{txtMaH.Text}'";
                bool kt = kn.ThucThi(query);
                if (kt)
                    MessageBox.Show("Sửa chi tiết hóa đơn thành công !");
                else
                    MessageBox.Show("Sửa chi tiết hóa đơn thất bại !");
                uploadHD(cmbMaHD.Text);
                string query1 = $"update Hang set SoLuong = SoLuong - {change} where MaH = '{txtMaH.Text}'";
                kn.ThucThi(query1);
                btnReFresh.PerformClick();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string query = $"delete from CTHDBan where MaH = '{txtMaH.Text}' and MaHDBan = '{cmbMaHD.SelectedValue}'";
                bool kt = kn.ThucThi(query);
                if (kt)
                    MessageBox.Show("Xóa thành công !");
                else
                    MessageBox.Show("Xóa thất bại !");
                uploadHD(cmbMaHD.Text);
                btnReFresh.PerformClick();
            }
        }


        //cell click

        private void dgvHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnXoaHD.Enabled = true;
            btnSuaHD.Enabled = true;
            btnThemHD.Enabled = false;
            panel5.Visible = true;
            panel5.Enabled = true;
            cmbMaHD.Enabled = false;
            txtTongTien.Enabled = true;
            int r = e.RowIndex;
            if (r >= 0)
            {
                cmbMaHD.Text = dgvHD.Rows[r].Cells["MaHDBan"].Value.ToString();
                getDataCtHD(cmbMaHD.Text);
                getcmbTenH(cmbMaHD.Text);
                txtMaH.Text = string.Empty;
                cmbTenH.Text = string.Empty;
                nudSoLuong.Value = 0;
                txtGiaBan.Text = string.Empty;
                txtGhiChu.Text = string.Empty;
            }
        }

        private void dgvCTHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnSua.Visible = true;
            btnXoa.Visible = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnThemSP.Enabled = false;
            cmbTenH.Enabled = false;
            int r = e.RowIndex;
            if (r >= 0)
            {
                cmbTenH.Text = dgvCTHD.Rows[r].Cells["TenH"].Value.ToString();
                txtMaH.Text = dgvCTHD.Rows[r].Cells["MaH"].Value.ToString();
                nudSoLuong.Value = (int)dgvCTHD.Rows[r].Cells["SoLuong"].Value;
                txtGiaBan.Text = dgvCTHD.Rows[r].Cells["DonGiaBan"].Value.ToString();
                txtGhiChu.Text = dgvCTHD.Rows[r].Cells["GhiChu"].Value.ToString();
            }
        }
    }
}
