using FlowerShop.SanPham;
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
    public partial class QuanLyTaiKhoanNV : UserControl
    {
        private db_flowerDataContext _context;
        private DoiMatKhau doimatkhau1;
        public QuanLyTaiKhoanNV()
        {
            InitializeComponent();
            _context = new db_flowerDataContext(); // Khởi tạo DataContext
            LoadComboBoxLoaiTaiKhoan();
            LoadAccounts();
        }

        private void frmQuanLyTaiKhoan_Load(object sender, EventArgs e)
        {
            dtpkNgayTao.Value = DateTime.Now;
            AutoResizeDataGridView(dgvTaiKhoan);
        }

        public void AutoResizeDataGridView(DataGridView dgv)
        {
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                column.MinimumWidth = 50;
            }
        }

        private void dgvTaiKhoan_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Kiểm tra xem cột hiện tại có phải là cột mật khẩu không (giả sử là cột thứ 3, bạn có thể thay đổi theo số thứ tự cột của mình)
            if (dgvTaiKhoan.Columns[e.ColumnIndex].Name == "password")
            {
                // Kiểm tra nếu giá trị không phải là DBNull (vì trong trường hợp không có mật khẩu sẽ bị lỗi)
                if (e.Value != DBNull.Value)
                {
                    // Chuyển giá trị mật khẩu thành dấu "*"
                    e.Value = new string('*', e.Value.ToString().Length);
                }
            }
        }

        private void LoadComboBoxLoaiTaiKhoan()
        {
            cboLoaiTaiKhoan.Items.Add("Tất Cả");
            cboLoaiTaiKhoan.Items.Add("Khách Hàng");
            cboLoaiTaiKhoan.Items.Add("Nhân Viên");
            cboLoaiTaiKhoan.SelectedIndex = 0;
        }

        private void LoadAccounts()
        {
            // Lấy loại tài khoản được chọn từ ComboBox
            string selectedType = cboLoaiTaiKhoan.SelectedItem.ToString();

            IQueryable<account> accountsQuery;

            if (selectedType == "Tất Cả")
            {
                // Lấy tất cả tài khoản
                accountsQuery = _context.accounts;
            }
            else if (selectedType == "Nhân Viên")
            {
                // Lọc tài khoản có mã bắt đầu bằng "ACCNV"
                accountsQuery = _context.accounts.Where(a => a.account_id.StartsWith("ACCNV"));
            }
            else if (selectedType == "Khách Hàng")
            {
                // Lọc tài khoản có mã bắt đầu bằng "ACCKH"
                accountsQuery = _context.accounts.Where(a => a.account_id.StartsWith("ACCKH"));
            }
            else
            {
                // Nếu không có loại tài khoản hợp lệ, chỉ trả về một danh sách trống
                accountsQuery = _context.accounts.Where(a => false);
            }

            // Hiển thị dữ liệu vào DataGridView
            dgvTaiKhoan.DataSource = accountsQuery.Select(a => new
            {
                a.account_id,
                a.username,
                a.password,
                a.created_at,
                a.updated_at,
                a.status
            }).ToList();
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu không chọn loại tài khoản
            if (cboLoaiTaiKhoan.SelectedItem.ToString() == "Tất Cả")
            {
                MessageBox.Show("Vui lòng chọn loại tài khoản (Khách Hàng hoặc Nhân Viên)!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra các trường nhập liệu
            if (string.IsNullOrEmpty(txtUsername.Text) || cboLoaiTaiKhoan.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!");
                return;
            }

            // Kiểm tra nếu tên người dùng có chứa khoảng trắng
            if (txtUsername.Text.Contains(" "))
            {
                MessageBox.Show("Tên người dùng không được chứa khoảng trắng!");
                return;
            }

            try
            {
                // Truy vấn tài khoản từ cơ sở dữ liệu
                var account = _context.accounts
                                        .Where(a => a.username == txtUsername.Text)
                                        .FirstOrDefault(); // Trả về tài khoản đầu tiên hoặc null nếu không có tài khoản nào

                if (account != null)
                {
                    // Nếu tài khoản đã tồn tại, hiển thị thông báo hoặc thực hiện hành động
                    MessageBox.Show("Tài khoản đã tồn tại!");
                }
                else
                {
                    // Nếu tài khoản chưa tồn tại, tiếp tục xử lý thêm tài khoản mới
                    var newAccount = new account
                    {
                        account_id = GenerateAccountId(),
                        username = txtUsername.Text,
                        password = "123", // Mật khẩu mặc định
                        created_at = DateTime.Now,
                        updated_at = DateTime.Now,
                        status = "inactive"
                    };

                    _context.accounts.InsertOnSubmit(newAccount);
                    _context.SubmitChanges();
                    LoadAccounts();
                }

                // Thông báo thành công
                MessageBox.Show("Tài khoản đã được thêm thành công!");
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                MessageBox.Show($"Đã xảy ra lỗi khi thêm tài khoản: {ex.Message}");
            }
        }
        private string GenerateAccountId()
        {
            // Lấy loại tài khoản được chọn từ ComboBox
            string selectedType = cboLoaiTaiKhoan.SelectedItem.ToString();

            // Xác định tiền tố cho account_id
            string prefix = selectedType == "Nhân Viên" ? "ACCNV" : "ACCKH";

            // Lấy tất cả các account_id có tiền tố đã chọn và lấy phần số sau tiền tố
            var accountIds = _context.accounts
                                      .Where(a => a.account_id.StartsWith(prefix))
                                      .Select(a => a.account_id)
                                      .ToList();

            // Tìm các số đã tồn tại sau tiền tố và chuyển chúng thành kiểu int
            var numbers = accountIds
                            .Select(id =>
                            {
                                int number;
                                // Lấy phần số sau tiền tố (3 chữ cái) và chuyển sang kiểu int
                                return int.TryParse(id.Substring(prefix.Length), out number) ? number : (int?)null;
                            })
                            .Where(num => num.HasValue) // Loại bỏ các giá trị null
                            .Select(num => num.Value)
                            .ToList();

            // Tìm số lớn nhất đã tồn tại và cộng thêm 1
            int maxId = numbers.Any() ? numbers.Max() : 0;

            // Tạo ID mới với số tiếp theo, đảm bảo là 3 chữ số (ví dụ: "001", "002")
            return prefix + (maxId + 1).ToString("D3"); // Đảm bảo ID mới có 3 chữ số
        }


        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            if (dgvTaiKhoan.SelectedRows.Count > 0)
            {
                // Lấy accountId từ cột 'account_id' của dòng được chọn
                var accountId = dgvTaiKhoan.SelectedRows[0].Cells["account_id"].Value.ToString();
                var account = _context.accounts.FirstOrDefault(a => a.account_id == accountId);

                if (account != null && account.status == "inactive")
                {
                    _context.accounts.DeleteOnSubmit(account);
                    _context.SubmitChanges();
                    LoadAccounts();
                    MessageBox.Show("Xóa Tài Khoản Thành Công");
                }
                else
                {
                    MessageBox.Show("Không thể xóa tài khoản đang hoạt động!");
                    return;
                }
            }
        }

        private void cboLoaiTaiKhoan_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng đã chọn "Tất Cả" hay chưa
            if (cboLoaiTaiKhoan.SelectedItem.ToString() == "Tất Cả")
            {
                // Nếu chọn "Tất Cả", bạn có thể thêm logic ở đây để xử lý, ví dụ: hiện thị tất cả tài khoản trong DataGridView
                LoadAccounts(); // Giả sử bạn muốn tải tất cả tài khoản khi chọn "Tất Cả"
            }
            else if (cboLoaiTaiKhoan.SelectedItem.ToString() == "Nhân Viên")
            {
                // Nếu chọn "Nhân Viên", có thể lọc tài khoản bắt đầu bằng "ACCNV"
                LoadAccountsByType("ACCNV");
            }
            else if (cboLoaiTaiKhoan.SelectedItem.ToString() == "Khách Hàng")
            {
                // Nếu chọn "Khách Hàng", có thể lọc tài khoản bắt đầu bằng "ACCKH"
                LoadAccountsByType("ACCKH");
            }
        }
        private void LoadAccountsByType(string prefix)
        {
            // Lọc tài khoản theo tiền tố (prefix) ví dụ "ACCNV" hoặc "ACCKH"
            var accounts = _context.accounts
                                    .Where(a => a.account_id.StartsWith(prefix))
                                    .ToList();

            // Cập nhật lại DataGridView với danh sách tài khoản đã lọc
            dgvTaiKhoan.DataSource = accounts.Select(a => new
            {
                a.account_id,
                a.username,
                a.password,
                a.created_at,
                a.updated_at,
                a.status
            }).ToList();
        }

        private void btnCapNhatMatKhau_Click(object sender, EventArgs e)
        {
            string matk = dgvTaiKhoan.SelectedRows[0].Cells["account_id"].Value.ToString();

            if (!string.IsNullOrWhiteSpace(matk))
            {
                bool exists = dgvTaiKhoan.Rows
                    .Cast<DataGridViewRow>()
                    .Any(row => row.Cells["account_id"].Value?.ToString() == matk);

                if (exists)
                {
                    // Ẩn tất cả các control hiện tại
                    foreach (Control control in this.Controls)
                    {
                        control.Visible = false;
                    }

                    // Tạo và hiển thị ChiTietDoiTra

                    doimatkhau1 = new DoiMatKhau(matk);
                    doimatkhau1.Dock = DockStyle.Fill;
                    // Đăng ký sự kiện BackButtonClicked
                    doimatkhau1.BackButtonClicked += ChiTietControl_BackButtonClicked;
                    this.Controls.Add(doimatkhau1);
                    doimatkhau1.BringToFront();
                }
                else
                {
                    MessageBox.Show("Mã tài khoản không tồn tại!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một tài khoản!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void ChiTietControl_BackButtonClicked(object sender, EventArgs e)
        {
            this.Controls.Remove(doimatkhau1);

            foreach (Control control in this.Controls)
            {
                control.Visible = true;
            }
        }
    }
}
