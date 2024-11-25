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

        public void frmQuanLyNhanVien_Load(object sender, EventArgs e)
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
            // Truy vấn từ bảng Accounts
            var accounts = _context.accounts
                                   .Where(a => a.status == "inactive" && a.account_id.StartsWith("ACCNV")) // Lọc tài khoản
                                   .Select(a => new { a.account_id, a.username })
                                   .ToList();

            // Thêm phần tử "Không Có Tài Khoản" vào đầu danh sách
            var accountsWithNoAccountOption = new List<object>
    {
        new { account_id = (string)null, username = "Không Có Tài Khoản" } // Mục đặc biệt
    };
            accountsWithNoAccountOption.AddRange(accounts);

            // Gán dữ liệu vào ComboBox
            cboAccId.DataSource = accountsWithNoAccountOption;
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
            // Kiểm tra thông tin đầu vào
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPhone.Text) || cboAccId.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra tính hợp lệ của Email
            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Email không hợp lệ.");
                return;
            }

            // Kiểm tra tính hợp lệ của Số điện thoại
            if (!IsValidPhoneNumber(txtPhone.Text))
            {
                MessageBox.Show("Số điện thoại không hợp lệ.");
                return;
            }

            // Lấy thông tin từ các trường nhập liệu
            string name = txtName.Text;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;
            string address = txtAddress.Text;
            string accountId = cboAccId.SelectedValue?.ToString(); // Lấy accountId từ ComboBox
            string maNV = GenerateEmployeeCode();

            // Kiểm tra nếu "Không Có Tài Khoản" được chọn
            if (string.IsNullOrEmpty(accountId))
            {
                accountId = null; // Để account_id là null nếu không có tài khoản
            }

            // Thêm nhân viên mới vào cơ sở dữ liệu
            var newEmployee = new employee
            {
                employee_id = maNV,
                name = name,
                email = email,
                phone = phone,
                address = address,
                account_id = accountId
            };

            _context.employees.InsertOnSubmit(newEmployee);
            _context.SubmitChanges(); // Lưu thay đổi vào cơ sở dữ liệu

            // Cập nhật trạng thái tài khoản thành "active" nếu có tài khoản
            if (!string.IsNullOrEmpty(accountId))
            {
                var account = _context.accounts.FirstOrDefault(a => a.account_id == accountId);
                if (account != null)
                {
                    account.status = "active";
                    _context.SubmitChanges();
                }
            }

            // Hiển thị thông báo thành công
            MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Tải lại dữ liệu
            LoadEmployeeData();
            LoadInactiveAccounts();
        }

        private void LoadEmployeeData()
        {
            var employees = _context.employees
                                    .GroupJoin(
                                        _context.accounts,  // Bảng Accounts
                                        emp => emp.account_id,  // Khóa ngoại trong Employees
                                        acc => acc.account_id,  // Khóa chính trong Accounts
                                        (emp, accGroup) => new { emp, accGroup })  // Kết quả nhóm
                                    .SelectMany(
                                        x => x.accGroup.DefaultIfEmpty(),  // Lấy tài khoản nếu có, ngược lại null
                                        (x, acc) => new
                                        {
                                            x.emp.employee_id,
                                            x.emp.name,
                                            x.emp.email,
                                            x.emp.phone,
                                            x.emp.address,
                                            account_id = acc != null ? acc.account_id : ""  // Xử lý khi không có tài khoản
                                })
                                    .ToList();

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
                string accountId = row.Cells["account_id"].Value?.ToString(); // Có thể null

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
                                                .FirstOrDefault(emp => emp.employee_id == employeeId);

                if (employeeToUpdate != null)
                {
                    // Cập nhật thông tin nhân viên
                    employeeToUpdate.name = name;
                    employeeToUpdate.email = email;
                    employeeToUpdate.phone = phone;
                    employeeToUpdate.address = address;

                    // Chỉ cập nhật bảng Account nếu accountId không rỗng hoặc null
                    if (!string.IsNullOrEmpty(accountId))
                    {
                        var accountToUpdate = _context.accounts.FirstOrDefault(acc => acc.account_id == accountId);
                        if (accountToUpdate != null)
                        {
                            accountToUpdate.updated_at = DateTime.Now;
                        }
                    }

                    // Lưu thay đổi vào cơ sở dữ liệu
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

                // Kiểm tra xem nhân viên có tồn tại trong các bảng returns hoặc emp_role không
                bool existsInReturns = _context.returns.Any(r => r.processed_by == employeeId);
                bool existsInEmpRole = _context.emp_roles.Any(er => er.employee_id == employeeId);

                if (existsInReturns || existsInEmpRole)
                {
                    MessageBox.Show("Không thể xóa nhân viên này vì họ đã tồn tại trong các bảng khác (returns hoặc emp_role).",
                                    "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

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
        private void btnGanTaiKhoan_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có dòng nào trong DataGridView được chọn không
            if (dgvNhanVien.SelectedRows.Count > 0)
            {
                // Lấy thông tin từ dòng được chọn
                DataGridViewRow row = dgvNhanVien.SelectedRows[0];

                // Lấy EmployeeId và AccountId từ các cột của dòng được chọn
                string employeeId = row.Cells["employee_id"].Value.ToString();
                string accountId = row.Cells["account_id"].Value.ToString(); // Nếu đã có tài khoản, accountId sẽ không trống

                // Kiểm tra xem nhân viên đã có tài khoản chưa
                if (string.IsNullOrEmpty(accountId))
                {
                    // Nếu không có tài khoản, thực hiện gán tài khoản
                    // Giả sử bạn có ComboBox để chọn tài khoản và accountId được chọn từ ComboBox
                    string selectedAccountId = cboAccId.SelectedValue?.ToString(); // Lấy account_id từ ComboBox

                    if (string.IsNullOrEmpty(selectedAccountId))
                    {
                        MessageBox.Show("Vui lòng chọn tài khoản để gán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Hiển thị hộp thoại xác nhận
                    DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn gán tài khoản này cho nhân viên không?",
                                                          "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // Cập nhật account_id của nhân viên với tài khoản được chọn
                        var employeeToUpdate = _context.employees.FirstOrDefault(emp => emp.employee_id == employeeId);
                        if (employeeToUpdate != null)
                        {
                            employeeToUpdate.account_id = selectedAccountId;
                            _context.SubmitChanges();  // Lưu thay đổi vào cơ sở dữ liệu

                            // Cập nhật trạng thái tài khoản thành "active"
                            var accountToUpdate = _context.accounts.FirstOrDefault(acc => acc.account_id == selectedAccountId);
                            if (accountToUpdate != null)
                            {
                                accountToUpdate.status = "active"; // Cập nhật trạng thái thành "active"
                                _context.SubmitChanges();  // Lưu thay đổi trạng thái
                            }

                            MessageBox.Show("Tài khoản đã được gán cho nhân viên thành công và trạng thái tài khoản đã được cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Tải lại dữ liệu để cập nhật
                            LoadEmployeeData();
                            LoadInactiveAccounts();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy nhân viên để gán tài khoản.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Nhân viên này đã có tài khoản rồi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để gán tài khoản.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnGoTaiKhoan_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có dòng nào trong DataGridView được chọn không
            if (dgvNhanVien.SelectedRows.Count > 0)
            {
                // Lấy thông tin từ dòng được chọn
                DataGridViewRow row = dgvNhanVien.SelectedRows[0];

                // Lấy AccountId từ cột của dòng được chọn
                string accountId = row.Cells["account_id"].Value.ToString();
                string employeeId = row.Cells["employee_id"].Value.ToString();

                // Kiểm tra nếu accountId không rỗng (tức là đã có tài khoản)
                if (!string.IsNullOrEmpty(accountId))
                {
                    // Hiển thị hộp thoại xác nhận
                    DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản này và cập nhật trạng thái tài khoản thành 'inactive'?",
                                                          "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // Truy vấn tài khoản từ cơ sở dữ liệu
                        var accountToUpdate = _context.accounts.FirstOrDefault(acc => acc.account_id == accountId);
                        if (accountToUpdate != null)
                        {
                            // Cập nhật trạng thái tài khoản thành "inactive"
                            accountToUpdate.status = "inactive";

                            // Xóa account_id của nhân viên
                            var employeeToUpdate = _context.employees.FirstOrDefault(emp => emp.employee_id == employeeId);
                            if (employeeToUpdate != null)
                            {
                                employeeToUpdate.account_id = null;  // Xóa account_id của nhân viên
                            }

                            // Lưu thay đổi vào cơ sở dữ liệu
                            _context.SubmitChanges();

                            // Thông báo cho người dùng
                            MessageBox.Show("Tài khoản đã được xóa và trạng thái tài khoản đã được cập nhật thành 'inactive'.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Cập nhật lại dữ liệu trong DataGridView
                            LoadEmployeeData();
                            LoadInactiveAccounts();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy tài khoản để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Nhân viên này chưa có tài khoản để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để thực hiện thao tác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
