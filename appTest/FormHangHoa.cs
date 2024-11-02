using appTest.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace appTest
{
    public partial class FormHangHoa : Form
    {
        private string imgdb = null;
        public FormHangHoa()
        {
            InitializeComponent();
        }
        Conn kn = new Conn();
        private void FormHangHoa_Load(object sender, EventArgs e)
        {
            getData();
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            nudSoLuong.Enabled = false;
        }
        public void getData()
        {
            string query = "select * from Hang";
            DataSet ds = new DataSet();
            ds = kn.LayDuLieu(query);
            dgvHH.DataSource = ds.Tables[0];
        }
        public void clear()
        {
            txtMaH.Text = "";
            txtTenH.Text = "";
            nudSoLuong.Value = 0;
            txtDonGiaNhap.Text = "";
            txtDonGiaBan.Text = "";
            txtGhiChu.Text = "";
        }

        private void dgvHH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnThem.Enabled = false;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            nudSoLuong.Enabled=true;
            txtMaH.Enabled = false;
            string iFP= "D:\\k1n3\\DoAn.NET\\appTest";
            int r = e.RowIndex;
            if (r >= 0)
            {
                txtMaH.Text = dgvHH.Rows[r].Cells["MaH"].Value.ToString();
                txtTenH.Text = dgvHH.Rows[r].Cells["TenH"].Value.ToString();
                nudSoLuong.Value = (int)dgvHH.Rows[r].Cells["SoLuong"].Value;
                txtDonGiaNhap.Text = dgvHH.Rows[r].Cells["DonGiaNhap"].Value.ToString();
                txtDonGiaBan.Text = dgvHH.Rows[r].Cells["DonGiaBan"].Value.ToString();
                txtGhiChu.Text = dgvHH.Rows[r].Cells["GhiChu"].Value.ToString();
                string a = dgvHH.Rows[r].Cells["Anh"].Value.ToString();
                if (a != "")
                {
                    string part = Path.Combine(iFP, a);
                    Image img = Image.FromFile(part);
                    pbAnh.Image = img;
                    imgdb = part;
                }
                else
                {
                    imgdb = null;
                    pbAnh.Image = null;
                }
                Console.WriteLine("imgdb :: "+ imgdb);
            }
        }

        private void txtDonGiaNhap_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtDonGiaBan_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            clear();
            getData();
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            nudSoLuong.Enabled = false;
            btnThem.Enabled = true;
            txtMaH.Enabled = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string query = $"insert into Hang values (N'{txtMaH.Text}',N'{txtTenH.Text}','0','{txtDonGiaNhap.Text}','{txtDonGiaBan.Text}',N'{txtGhiChu.Text}',N'{imgdb}')";
            bool kt = kn.ThucThi(query);
            if (kt)
            {
                MessageBox.Show("Thêm mới thành công !");
                imgdb = null;
            }
            else
                MessageBox.Show("Thêm mới thất bại !");
            btnRefresh.PerformClick();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string query = $"delete from Hang where MaH = '{txtMaH.Text}'";
                bool kt = kn.ThucThi(query);
                if (kt)
                    MessageBox.Show("Xóa thành công !");
                else
                    MessageBox.Show("Xóa thất bại ! mã hàng đang tồn tại trong bảng CTHD !");
                btnRefresh.PerformClick();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string query = $"update Hang set TenH = N'{txtTenH.Text}', SoLuong ='{nudSoLuong.Value}', DonGiaNhap = '{txtDonGiaNhap.Text}', DonGiaBan = '{txtDonGiaBan.Text}', GhiChu = N'{txtGhiChu.Text}', Anh = '{imgdb}' where MaH = '{txtMaH.Text}'";
            bool kt = kn.ThucThi(query);
            if (kt)
                MessageBox.Show("Sửa thành công !");
            else
                MessageBox.Show("Sửa thất bại !");
            btnRefresh.PerformClick();
        }

        private void btnbrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileImg = new OpenFileDialog();
            if (fileImg.ShowDialog() == DialogResult.OK)
            {
                // Lưu ảnh vào thư mục trong dự án WinForms
                string imageFolderPath = "D:\\k1n3\\DoAn.NET\\appTest\\UpLoadImg";

                string fileName = Path.GetFileName(fileImg.FileName);
                string destinationPath = Path.Combine(imageFolderPath, fileName);
                if (!File.Exists(destinationPath))
                {
                    File.Copy(fileImg.FileName, destinationPath, true);
                }

                imgdb = Path.Combine("UpLoadImg", fileName);
                Console.WriteLine(destinationPath);
                Console.WriteLine(imgdb);
                Image img = Image.FromFile(destinationPath, true);
                pbAnh.Image = img;
                
            }
        }
    }
}
