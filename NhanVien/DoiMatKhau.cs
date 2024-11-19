using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowerShop.NhanVien
{
    public partial class DoiMatKhau : UserControl
    {
        string maNhanVien;

        public DoiMatKhau(string MaNhanVien)
        {
            InitializeComponent();
            maNhanVien = MaNhanVien;
        }
        public event EventHandler BackButtonClicked;
        private void btnTroVe_Click(object sender, EventArgs e)
        {
            BackButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        private void frmDoiMatKhau_Load(object sender, EventArgs e)
        {
            using (var context = new db_flowerDataContext())
            {
                // Truy vấn để lấy username từ mã nhân viên
                var account = context.accounts
                    .Where(emp => emp.account_id == maNhanVien)
                    .FirstOrDefault();

                // Kiểm tra nếu nhân viên tồn tại và gán username vào txtUsername
                if (account != null)
                {
                    txtUsername.Text = account.username; // Gán username vào TextBox
                }
                else
                {
                    MessageBox.Show("Không tìm thấy tài khoản với mã đã cung cấp.");
                }
            }
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu các trường mật khẩu trống
            if (string.IsNullOrWhiteSpace(txtMatKhau.Text) || string.IsNullOrWhiteSpace(txtReMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra nếu mật khẩu và nhập lại mật khẩu không khớp
            if (txtMatKhau.Text != txtReMatKhau.Text)
            {
                MessageBox.Show("Mật khẩu và Nhập lại mật khẩu không khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var context = new db_flowerDataContext())
            {
                // Tìm tài khoản dựa vào mã nhân viên
                var account = context.accounts
                    .FirstOrDefault(a => a.account_id == maNhanVien);

                if (account != null)
                {
                    //// Mã hóa mật khẩu trước khi lưu
                    //account.password = BCrypt.Net.BCrypt.HashPassword(txtMatKhau.Text);
                    account.password = txtMatKhau.Text;
                    account.updated_at = DateTime.Now;
                    // Lưu thay đổi vào cơ sở dữ liệu
                    context.SubmitChanges();

                    MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Đóng form sau khi đổi mật khẩu thành công
                    BackButtonClicked?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy tài khoản để cập nhật mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
