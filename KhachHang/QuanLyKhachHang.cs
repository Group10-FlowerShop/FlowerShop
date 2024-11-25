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

namespace FlowerShop.KhachHang
{
    public partial class QuanLyKhachHang : UserControl
    {
        private db_flowerDataContext _context;
        public QuanLyKhachHang()
        {
            InitializeComponent();
            _context = new db_flowerDataContext();
        }
        private void frmQuanLyKhachHang_Load(object sender, EventArgs e)
        {
            LoadInactiveAccounts();
            LoadCustomerData();
            AutoResizeDataGridView(dgvKhachHang);
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
                                  .Where(a => a.status == "inactive" && a.account_id.StartsWith("ACCKH"))
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
        private string GenerateCustomerCode()
        {
            // Tạo mã nhân viên theo định dạng "EMPxxx"
            string prefix = "CUS";
            int newCodeNumber = 1;

            // Tìm mã nhân viên lớn nhất trong cơ sở dữ liệu
            var maxEmployeeCode = _context.customers
                                          .Where(e => e.customer_id.StartsWith(prefix))
                                          .Select(e => e.customer_id)
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
            string maNV = GenerateCustomerCode();
            var newCustomer = new customer
            {
                customer_id = maNV,
                name = name,
                email = email,
                phone = phone,
                address = address,
                account_id = accountId
            };

            _context.customers.InsertOnSubmit(newCustomer);
            _context.SubmitChanges();  // Lưu thay đổi

            // Cập nhật trạng thái tài khoản thành "Hoạt Động"
            var account = _context.accounts.FirstOrDefault(a => a.account_id == accountId);
            if (account != null)
            {
                account.status = "active";
                _context.SubmitChanges();
            }

            MessageBox.Show("Thêm khách hàng thành công và trạng thái tài khoản đã được cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadCustomerData();
            LoadInactiveAccounts();
        }
        private void LoadCustomerData()
        {
            // Truy vấn nhân viên và các tài khoản của họ
            var employees = _context.customers
                        .GroupJoin(_context.accounts,
                                   emp => emp.account_id,
                                   acc => acc.account_id,
                                   (emp, accGroup) => new
                                   {
                                       emp.customer_id,
                                       emp.name,
                                       emp.email,
                                       emp.phone,
                                       emp.address,
                                       account_id = accGroup.Select(a => a.account_id).FirstOrDefault()
                                   })
                        .ToList();


            // Gán kết quả vào DataGridView
            dgvKhachHang.DataSource = employees;
            
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có dòng nào được chọn không
            if (dgvKhachHang.SelectedRows.Count > 0)
            {
                // Lấy thông tin từ dòng được chọn
                DataGridViewRow row = dgvKhachHang.SelectedRows[0];

                // Lấy EmployeeId và AccountId từ các cột của dòng được chọn
                int index = dgvKhachHang.CurrentRow.Index;

                // Lấy EmployeeId và AccountId từ các cột của dòng được chọn
                string customerId = dgvKhachHang.Rows[index].Cells[0].Value.ToString();
                string accountId = dgvKhachHang.Rows[index].Cells[5].Value.ToString();

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
                var customerToUpdate = _context.customers
                                                .FirstOrDefault(emp => emp.customer_id == customerId); // Đổi tên "e" thành "emp"

                if (customerToUpdate != null)
                {
                    customerToUpdate.name = name;
                    customerToUpdate.email = email;
                    customerToUpdate.phone = phone;
                    customerToUpdate.address = address;
                    customerToUpdate.account.updated_at = DateTime.Now;
                    // Cập nhật vào cơ sở dữ liệu bằng SubmitChanges() trong LINQ to SQL
                    _context.SubmitChanges();

                    // Hiển thị thông báo thành công
                    MessageBox.Show("Cập nhật thông tin khách hàng thành công!");
                    LoadCustomerData(); // Cập nhật lại dữ liệu trong DataGridView
                }
                else
                {
                    MessageBox.Show("Không tìm thấy khách hàng để sửa.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để sửa.");
            }
        }
        private void dgvKhachHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem có dòng nào được chọn không
            if (dgvKhachHang.SelectedRows.Count > 0)
            {
                // Lấy thông tin từ dòng được chọn
                DataGridViewRow row = dgvKhachHang.SelectedRows[0];
                int index = dgvKhachHang.CurrentRow.Index;

                // Điền thông tin vào các TextBox
                txtName.Text = dgvKhachHang.Rows[index].Cells[1].Value.ToString();
                txtEmail.Text = dgvKhachHang.Rows[index].Cells[2].Value.ToString();
                txtPhone.Text = dgvKhachHang.Rows[index].Cells[3].Value.ToString();
                txtAddress.Text = dgvKhachHang.Rows[index].Cells[4].Value.ToString();
            }
            TrimEndWhitespace();
        }

        private void TrimEndWhitespace()
        {
            // Xóa khoảng trắng ở cuối chuỗi trong txtSDT
            txtPhone.Text = txtPhone.Text.TrimEnd();
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có dòng nào được chọn không
            if (dgvKhachHang.SelectedRows.Count > 0)
            {
                // Lấy thông tin từ dòng được chọn
                DataGridViewRow row = dgvKhachHang.SelectedRows[0];
                int index = dgvKhachHang.CurrentRow.Index;

                // Lấy EmployeeId và AccountId từ các cột của dòng được chọn
                string customerId = dgvKhachHang.Rows[index].Cells[0].Value.ToString();
                string accountId = dgvKhachHang.Rows[index].Cells[5].Value.ToString();

                // Xác nhận người dùng có chắc chắn muốn xóa nhân viên
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này và làm tài khoản của họ không hoạt động?",
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
                    var customerToDelete = _context.customers.FirstOrDefault(emp => emp.customer_id == customerId);
                    if (customerToDelete != null)
                    {
                        _context.customers.DeleteOnSubmit(customerToDelete); // Xóa nhân viên khỏi DataContext

                        // Lưu thay đổi
                        _context.SubmitChanges();  // Sử dụng SubmitChanges thay vì SaveChanges

                        MessageBox.Show("Khách hàng đã được xóa và tài khoản của họ đã được làm không hoạt động.");
                        LoadCustomerData(); // Cập nhật lại dữ liệu trong DataGridView
                        LoadInactiveAccounts();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy Khách hàng để xóa.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để xóa.");
            }
        }
    }
}
