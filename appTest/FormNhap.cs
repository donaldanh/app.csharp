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
    public partial class FormNhap : Form
    {
        public FormNhap()
        {
            InitializeComponent();
        }
        Conn kn = new Conn();

        private void FormNhap_Load(object sender, EventArgs e)
        {
            panel5.Visible = false;
            btnSua.Visible = false;
            btnXoa.Visible = false;
            btnSuaHD.Enabled = false;
            btnXoaHD.Enabled = false;
            txtMaH.Enabled = false;
            getDataHD();
            getcmb(cmbMaHD, "MaHDNhap", "MaHDNhap", "HDNhap");
            getcmb(cmbTenNCC, "TenNCC", "MaNCC", "NCC");
        }

        // function 

        public void upLoadSL(string MaH)
        {
            string query = $"update Hang set SoLuong = SoLuong + (select sum(SoLuong) from CTHDNhap where MaH = '{MaH}') where MaH = '{MaH}'";
            kn.ThucThi(query);
        }

        // các hàm lấy dữ liệu cho datagridview
        public void getDataHD()
        {
            string query = "select MaHDNhap, NgayNhap, TongTien from HDNhap";
            DataSet ds = new DataSet();
            ds = kn.LayDuLieu(query);
            dgvHD.DataSource = ds.Tables[0];
        }
        public void getDataCtHD(string MaHD)
        {
            string query = $"select CTHDNhap.MaHDNhap, Hang.TenH, Hang.MaH, Hang.GhiChu, CTHDNhap.SoLuong, Hang.DonGiaNhap, CTHDNhap.ThanhTien from CTHDNhap inner join Hang on CTHDNhap.MaH = Hang.MaH where CTHDNhap.MaHDNhap = '{MaHD}'";
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

        public void getcmbTenH(string MaHD)
        {
            string query = $"select MaH, TenH from Hang where MaH not in (select MaH from CTHDNhap where MaHDNhap = '{MaHD}')";
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
            cmbTenNCC.Text = string.Empty;
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
            string query = $"update HDNhap set TongTien = (select sum(ThanhTien) from CTHDNhap inner join HDNhap on CTHDNhap.MaHDNhap = HDNhap.MaHDNhap where CTHDNhap.MaHDNhap = '{MaHD}') where MaHDNhap = '{MaHD}'";
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
            if (cmbMaHD.SelectedIndex != -1)
            {
                string str2 = $"select NgayNhap from HDNhap where MaHDNhap = '{cmbMaHD.SelectedValue}'";
                string ngayNhapStr = Functions.GetFieldValues(str2);

                if (DateTime.TryParse(ngayNhapStr, out DateTime ngayNhap))
                {
                    // Chuyển đổi thành công, thiết lập giá trị cho DateTimePicker
                    dtpNgayNhap.Value = ngayNhap;
                }
                string str3 = $"select TongTien from HDNhap where MaHDNhap = '{cmbMaHD.SelectedValue}'";
                txtTongTien.Text = Functions.GetFieldValues(str3);
                string str4 = $"select TenNCC from NCC inner join HDNhap on NCC.MaNCC = HDNhap.MaNCC where MaHDNhap = '{cmbMaHD.SelectedValue}'";
                cmbTenNCC.Text = Functions.GetFieldValues(str4);
            }
        }

        private void cmbTenNCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTenNCC.SelectedIndex != -1)
            {
                string strDC = $"select DiaChi from NCC where MaNCC = '{cmbTenNCC.SelectedValue}'";
                txtDiaChi.Text = Functions.GetFieldValues(strDC);
                string strDT = $"select DienThoai from NCC where MaNCC = '{cmbTenNCC.SelectedValue}'";
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
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            cmbMaHD.Enabled = true;
            cmbTenH.Enabled = true;
            txtTongTien.Enabled = false;
            getcmb(cmbMaHD, "MaHDBan", "MaHDBan", "HDBan");
            dgvCTHD.DataSource = null;
            clear();
        }


        // các button của hóa đơn 

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (cmbMaHD.Text != string.Empty)
            {
                string query = $"select MaHDNhap, NgayNhap, TongTien from HDNhap where MaHDNhap = '{cmbMaHD.Text}'";
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
            string query = $"insert into HDNhap (MaHDNhap, NgayNhap, MaNCC) values ('{cmbMaHD.Text}','{dtpNgayNhap.Value}','{cmbTenNCC.SelectedValue}')";
            bool kt = kn.ThucThi(query);
            if (kt)
                MessageBox.Show("Thêm mới hóa đơn thành công !");
            else
                MessageBox.Show("Thêm mới hóa đơn thất bại !");
            btnReFresh.PerformClick();
        }

        private void btnSuaHD_Click(object sender, EventArgs e)
        {
            string query = $"update HDNhap set NgayNhap ='{dtpNgayNhap.Value}', MaNCC = '{cmbTenNCC.SelectedValue}', TongTien = '{txtTongTien.Text}' where MaHDNhap = '{cmbMaHD.SelectedValue}'";
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
                string q = $"delete from CTHDNhap where MaHDNhap = {cmbMaHD.Text}";
                bool k = kn.ThucThi(q);
                if (k)
                {
                    MessageBox.Show("đã xóa hết CHTHD Nhập có MaHD :: " + cmbMaHD.Text);
                    string query = $"delete from HDNhap where MaHDNhap = '{cmbMaHD.SelectedValue}'";
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
            string query = $"insert into CTHDNhap (MaHDNhap, MaH, SoLuong) values ('{cmbMaHD.Text}','{cmbTenH.SelectedValue}',{nudSoLuong.Value})";
            bool kt = kn.ThucThi(query);
            if (kt)
                MessageBox.Show("Thêm mới chi tiết hóa đơn thành công !");
            else
                MessageBox.Show("Thêm mới chi tiết hóa đơn thất bại !");
            uploadHD(cmbMaHD.Text);
            upLoadSL(cmbTenH.SelectedValue.ToString());
            btnReFresh.PerformClick();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string query = $"update CTHDNhap set SoLuong = {nudSoLuong.Value} where MaHDNhap = '{cmbMaHD.SelectedValue}' and MaH = '{txtMaH.Text}'";
            bool kt = kn.ThucThi(query);
            if (kt)
                MessageBox.Show("Sửa chi tiết hóa đơn thành công !");
            else
                MessageBox.Show("Sửa chi tiết hóa đơn thất bại !");
            uploadHD(cmbMaHD.Text);
            btnReFresh.PerformClick();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string query = $"delete from CTHDNhap where MaH = '{txtMaH.Text}' and MaHDNhap = '{cmbMaHD.SelectedValue}'";
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
                cmbMaHD.Text = dgvHD.Rows[r].Cells["MaHDNhap"].Value.ToString();
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
            cmbTenH.Enabled = false;
            int r = e.RowIndex;
            if (r >= 0)
            {
                cmbTenH.Text = dgvCTHD.Rows[r].Cells["TenH"].Value.ToString();
                txtMaH.Text = dgvCTHD.Rows[r].Cells["MaH"].Value.ToString();
                nudSoLuong.Value = (int)dgvCTHD.Rows[r].Cells["SoLuong"].Value;
                txtGiaBan.Text = dgvCTHD.Rows[r].Cells["DonGiaNhap"].Value.ToString();
                txtGhiChu.Text = dgvCTHD.Rows[r].Cells["GhiChu"].Value.ToString();
            }
        }

    }
}
