using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowerShop.NhanVien
{
    public partial class QuanLyNhanVien : UserControl
    {
        private db_flowerDataContext _context;
        public QuanLyNhanVien()
        {
            InitializeComponent();
            _context = new db_flowerDataContext();
        }

        private void frmQuanLyNhanVien_Load(object sender, EventArgs e)
        {
            LoadInactiveAccounts();
            LoadEmployeeData();
            AutoResizeDataGridView(dgvNhanVien);
        }
        public void AutoResizeDataGridView(DataGridView dgv)
        {
            // Set the AutoSizeColumnsMode to Fill to have columns adjust to fill the entire width
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Additionally, you can specify minimum widths or other settings for individual columns if needed
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                column.MinimumWidth = 50; // Set a minimum width as an example, adjust as needed
            }
        }
        public void LoadInactiveAccounts()
        {
            // Giả sử _context là đối tượng DbContext của bạn
            var accounts = _context.accounts  // Truy vấn từ bảng Accounts
                                  .Where(a => a.status == "inactive")  // Lọc tài khoản không hoạt động
                                  .Select(a => new { a.account_id, a.username })
                                  .ToList();

            // Gán dữ liệu vào ComboBox
            cboAccId.DataSource = accounts;
            cboAccId.DisplayMember = "username";  // Hiển thị tên tài khoản
            cboAccId.ValueMember = "account_id";  // Lưu giá trị account_id
        }

        public bool IsValidPhoneNumber(string phoneNumber)
        {
            string pattern = @"^(0[3|5|7|8|9])([0-9]{8})$"; // Định dạng số điện thoại Việt Nam
            return Regex.IsMatch(phoneNumber, pattern);
        }
        public bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }
        private string GenerateEmployeeCode()
        {
            // Tạo mã nhân viên theo định dạng "EMPxxx"
            string prefix = "EMP";
            int newCodeNumber = 1;

            // Tìm mã nhân viên lớn nhất trong cơ sở dữ liệu
            var maxEmployeeCode = _context.employees
                                          .Where(e => e.employee_id.StartsWith(prefix))
                                          .Select(e => e.employee_id)
                                          .OrderByDescending(e => e) // Sắp xếp giảm dần
                                          .FirstOrDefault();

            // Nếu tìm thấy mã nhân viên lớn nhất
            if (maxEmployeeCode != null)
            {
                // Lấy phần số sau tiền tố "NV"
                string lastNumber = maxEmployeeCode.Substring(prefix.Length);

                // Chuyển phần số thành số nguyên và cộng thêm 1
                if (int.TryParse(lastNumber, out int lastCodeNumber))
                {
                    newCodeNumber = lastCodeNumber + 1;
                }
            }

            // Đảm bảo rằng số nhân viên mới luôn có 3 chữ số
            return $"{prefix}{newCodeNumber.ToString("D3")}";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPhone.Text) || cboAccId.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Email không hợp lệ.");
                return;
            }

            if (!IsValidPhoneNumber(txtPhone.Text))
            {
                MessageBox.Show("Số điện thoại không hợp lệ.");
                return;
            }

            string name = txtName.Text;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;
            string address = txtAddress.Text;
            string accountId = cboAccId.SelectedValue.ToString();  // Lấy accountId từ ComboBox
            string maNV = GenerateEmployeeCode();
            var newEmployee = new employee
            {
                employee_id = maNV,
                name = name,
                email = email,
                phone = phone,
                address = address,
                account_id = accountId
            };

            // Thêm nhân viên vào cơ sở dữ liệu
            _context.employees.InsertOnSubmit(newEmployee);
            _context.SubmitChanges();  // Lưu thay đổi

            // Cập nhật trạng thái tài khoản thành "Hoạt Động"
            var account = _context.accounts.FirstOrDefault(a => a.account_id == accountId);
            if (account != null)
            {
                account.status = "active";
                _context.SubmitChanges();
            }

            MessageBox.Show("Thêm nhân viên thành công và trạng thái tài khoản đã được cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadEmployeeData();  // Tải lại dữ liệu nhân viên
            LoadInactiveAccounts();
        }

        private void LoadEmployeeData()
        {
            // Truy vấn nhân viên và các tài khoản của họ
            var employees = _context.employees
                                    .Join(_context.accounts,  // Kết hợp với bảng Accounts
                                          emp => emp.account_id,  // Khóa ngoại trong bảng Employee
                                          acc => acc.account_id,  // Khóa chính trong bảng Accounts
                                          (emp, acc) => new  // Lựa chọn các trường cần thiết
                                          {
                                              emp.employee_id,
                                              emp.name,
                                              emp.email,
                                              emp.phone,
                                              emp.address,
                                              acc.account_id
                                          })
                                    .ToList();

            // Gán kết quả vào DataGridView
            dgvNhanVien.DataSource = employees;
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có dòng nào được chọn không
            if (dgvNhanVien.SelectedRows.Count > 0)
            {
                // Lấy thông tin từ dòng được chọn
                DataGridViewRow row = dgvNhanVien.SelectedRows[0];

                // Lấy EmployeeId và AccountId từ các cột của dòng được chọn
                string employeeId = row.Cells["employee_id"].Value.ToString();
                string accountId = row.Cells["account_id"].Value.ToString();

                // Lấy thông tin từ các TextBox
                string name = txtName.Text;
                string email = txtEmail.Text;
                string phone = txtPhone.Text;
                string address = txtAddress.Text;

                // Kiểm tra tính hợp lệ của Email
                if (!IsValidEmail(email))
                {
                    MessageBox.Show("Email không hợp lệ.");
                    return;
                }

                // Kiểm tra tính hợp lệ của SĐT
                if (!IsValidPhoneNumber(phone))
                {
                    MessageBox.Show("Số điện thoại không hợp lệ.");
                    return;
                }

                // Cập nhật thông tin nhân viên trong cơ sở dữ liệu bằng LINQ
                var employeeToUpdate = _context.employees
                                                .FirstOrDefault(emp => emp.employee_id == employeeId); // Đổi tên "e" thành "emp"

                if (employeeToUpdate != null)
                {
                    employeeToUpdate.name = name;
                    employeeToUpdate.email = email;
                    employeeToUpdate.phone = phone;
                    employeeToUpdate.address = address;

                    // Cập nhật vào cơ sở dữ liệu bằng SubmitChanges() trong LINQ to SQL
                    _context.SubmitChanges();

                    // Hiển thị thông báo thành công
                    MessageBox.Show("Cập nhật thông tin nhân viên thành công!");
                    LoadEmployeeData(); // Cập nhật lại dữ liệu trong DataGridView
                }
                else
                {
                    MessageBox.Show("Không tìm thấy nhân viên để sửa.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để sửa.");
            }
        }

        private void dgvNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem có dòng nào được chọn không
            if (dgvNhanVien.SelectedRows.Count > 0)
            {
                // Lấy thông tin từ dòng được chọn
                DataGridViewRow row = dgvNhanVien.SelectedRows[0];

                // Điền thông tin vào các TextBox
                txtName.Text = row.Cells["Name"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtPhone.Text = row.Cells["Phone"].Value.ToString();
                txtAddress.Text = row.Cells["Address"].Value.ToString();
            }
            TrimEndWhitespace();
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có dòng nào được chọn không
            if (dgvNhanVien.SelectedRows.Count > 0)
            {
                // Lấy thông tin từ dòng được chọn
                DataGridViewRow row = dgvNhanVien.SelectedRows[0];

                // Lấy EmployeeId và AccountId từ các cột của dòng được chọn
                string employeeId = row.Cells["employee_id"].Value.ToString();
                string accountId = row.Cells["account_id"].Value.ToString();

                // Xác nhận người dùng có chắc chắn muốn xóa nhân viên
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này và làm tài khoản của họ không hoạt động?",
                                                      "Xác nhận xóa", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // Sử dụng LINQ để tìm tài khoản và cập nhật trạng thái
                    var account = _context.accounts.FirstOrDefault(a => a.account_id == accountId);
                    if (account != null)
                    {
                        account.status = "inactive"; // Cập nhật trạng thái tài khoản

                        // Lưu thay đổi của tài khoản
                        _context.SubmitChanges();  // Sử dụng SubmitChanges thay vì SaveChanges
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy tài khoản để cập nhật.");
                        return;
                    }

                    // Sử dụng LINQ để tìm và xóa nhân viên
                    var employeeToDelete = _context.employees.FirstOrDefault(emp => emp.employee_id == employeeId);
                    if (employeeToDelete != null)
                    {
                        _context.employees.DeleteOnSubmit(employeeToDelete); // Xóa nhân viên khỏi DataContext

                        // Lưu thay đổi
                        _context.SubmitChanges();  // Sử dụng SubmitChanges thay vì SaveChanges

                        MessageBox.Show("Nhân viên đã được xóa và tài khoản của họ đã được làm không hoạt động.");
                        LoadEmployeeData(); // Cập nhật lại dữ liệu trong DataGridView
                        LoadInactiveAccounts();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy nhân viên để xóa.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để xóa.");
            }
        }
        private void TrimEndWhitespace()
        {
            // Xóa khoảng trắng ở cuối chuỗi trong txtSDT
            txtPhone.Text = txtPhone.Text.TrimEnd();
        }
    }
}
