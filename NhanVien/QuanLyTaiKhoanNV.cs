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
        public QuanLyTaiKhoanNV()
        {
            InitializeComponent();
            _context = new db_flowerDataContext(); // Khởi tạo DataContext
            LoadComboBoxStatus();
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

        private void LoadComboBoxStatus()
        {
            cboTrangThai.Items.Add("Hoạt Động");
            cboTrangThai.Items.Add("Không Hoạt Động");
            cboTrangThai.SelectedIndex = 0;
        }

        private void LoadAccounts()
        {
            var accounts = _context.accounts.ToList();

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

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            // Kiểm tra các trường nhập liệu
            if (string.IsNullOrEmpty(txtUsername.Text) || cboTrangThai.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!");
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
            // Lấy tất cả các account_id bắt đầu bằng "ACC" và có phần số sau đó
            var accountIds = _context.accounts
                                      .Where(a => a.account_id.StartsWith("ACC"))
                                      .Select(a => a.account_id)
                                      .ToList();

            // Tìm các số đã tồn tại sau "ACC" và chuyển chúng thành kiểu int
            var numbers = accountIds
                            .Select(id =>
                            {
                                int number;
                                // Chỉ lấy phần số sau "ACC" nếu định dạng hợp lệ
                                return int.TryParse(id.Substring(3), out number) ? number : (int?)null;
                            })
                            .Where(num => num.HasValue) // Loại bỏ các giá trị null
                            .Select(num => num.Value)
                            .ToList();

            // Nếu không có account_id hợp lệ, khởi tạo maxId = 0
            int maxId = numbers.Any() ? numbers.Max() : 0;

            // Tạo ID mới với số tiếp theo
            return "ACC" + (maxId + 1).ToString("D3"); // Đảm bảo là 3 chữ số
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
    }
}
