﻿using System;
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
    public partial class PhanQuyen : UserControl
    {
        public PhanQuyen()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            using (var context = new db_flowerDataContext())
            {
                // Lấy danh sách nhân viên từ cơ sở dữ liệu
                var employees = (from e in context.employees
                                 select new
                                 {
                                     EmployeeId = e.employee_id,
                                     EmployeeName = e.name
                                 }).ToList();

                // Thêm mục "Tất cả nhân viên" vào đầu danh sách
                employees.Insert(0, new { EmployeeId = "ALL", EmployeeName = "Tất cả nhân viên" });

                // Liên kết danh sách với ComboBox
                cboNhanVien.DataSource = employees;
                cboNhanVien.DisplayMember = "EmployeeName";  // Hiển thị tên nhân viên
                cboNhanVien.ValueMember = "EmployeeId";     // Giá trị là EmployeeId

                // Lấy danh sách quyền
                var permissions = from p in context.emp_permissions
                                  select new
                                  {
                                      PermId = p.perm_id,
                                      PermName = p.name
                                  };

                // Liên kết danh sách quyền với ComboBox
                cboQuyen.DataSource = permissions.ToList();
                cboQuyen.DisplayMember = "PermName";  // Hiển thị tên quyền
                cboQuyen.ValueMember = "PermId";     // Giá trị là PermId
            }
        }

        // Phương thức load dữ liệu vào DataGridView
        private void LoadEmpRolesData()
        {
            try
            {
                // Lấy danh sách emp_role từ cơ sở dữ liệu sử dụng LINQ
                using (var context = new db_flowerDataContext())
                {
                    var empRoles = from er in context.emp_roles
                                   join e in context.employees on er.employee_id equals e.employee_id
                                   join p in context.emp_permissions on er.perm_id equals p.perm_id
                                   select new
                                   {
                                       EmployeeId = er.employee_id,
                                       PermId = er.perm_id,
                                       EmployeeName = e.name, // Lấy tên nhân viên từ bảng employees
                                       PermName = p.name      // Lấy tên quyền từ bảng emp_permissions
                                   };

                    // Hiển thị dữ liệu trong DataGridView
                    dgvDanhSach.DataSource = empRoles.ToList();

                    // Cài đặt tiêu đề cột cho DataGridView (tuỳ thuộc vào thuộc tính của DTO)
                    dgvDanhSach.Columns["EmployeeId"].HeaderText = "Mã Nhân Viên";
                    dgvDanhSach.Columns["PermId"].HeaderText = "Mã Quyền";
                    dgvDanhSach.Columns["EmployeeName"].HeaderText = "Tên Nhân Viên";
                    dgvDanhSach.Columns["PermName"].HeaderText = "Tên Quyền";
                    // Ẩn các cột EmployeeId và PermId
                    dgvDanhSach.Columns["EmployeeId"].Visible = false;
                    dgvDanhSach.Columns["PermId"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void frmPhanQuyen_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadEmpRolesData();
            AutoResizeDataGridView(dgvDanhSach);
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


        private void btnThemQuyen_Click(object sender, EventArgs e)
        {
            // Lấy giá trị EmployeeId và PermId từ các ComboBox
            string employeeId = cboNhanVien.SelectedValue.ToString();
            string permId = cboQuyen.SelectedValue.ToString();

            // Kiểm tra nếu các giá trị không rỗng
            if (string.IsNullOrEmpty(employeeId) || string.IsNullOrEmpty(permId))
            {
                MessageBox.Show("Vui lòng chọn nhân viên và quyền.");
                return;
            }
            if (employeeId == "ALL")
            {
                MessageBox.Show("Vui lòng chọn nhân viên và quyền.");
                return;
            }

            // Kiểm tra quyền đã tồn tại chưa bằng LINQ
            using (var context = new db_flowerDataContext())
            {
                var existingRole = context.emp_roles
                    .FirstOrDefault(er => er.employee_id == employeeId && er.perm_id == permId);

                if (existingRole != null)
                {
                    MessageBox.Show("Quyền này đã tồn tại cho nhân viên được chọn.");
                    return;
                }

                // Thêm quyền cho nhân viên bằng LINQ
                var newRole = new emp_role
                {
                    employee_id = employeeId,
                    perm_id = permId
                };
                context.emp_roles.InsertOnSubmit(newRole);

                try
                {
                    context.SubmitChanges(); // Lưu thay đổi vào cơ sở dữ liệu
                    MessageBox.Show("Thêm quyền cho nhân viên thành công.");
                    LoadData();
                    LoadEmpRolesData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Thêm quyền không thành công: " + ex.Message);
                }
            }
        }


        private void btnXoaQuyen_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có hàng nào được chọn không
            if (dgvDanhSach.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn hàng cần xóa.");
                return;
            }
            // Hiển thị thông báo xác nhận xóa
            var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa quyền này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmResult == DialogResult.No)
            {
                // Nếu người dùng chọn "No", thoát hàm
                return;
            }
            // Lấy employeeId và permId từ các cột của hàng được chọn
            string employeeId = dgvDanhSach.CurrentRow.Cells["EmployeeId"].Value.ToString();
            string permId = dgvDanhSach.CurrentRow.Cells["PermId"].Value.ToString();

            // Tạo đối tượng DAL (có thể loại bỏ nếu không cần nữa)
            using (var context = new db_flowerDataContext())
            {
                var roleToDelete = context.emp_roles
                    .FirstOrDefault(er => er.employee_id == employeeId && er.perm_id == permId);

                if (roleToDelete != null)
                {
                    context.emp_roles.DeleteOnSubmit(roleToDelete);

                    try
                    {
                        context.SubmitChanges(); // Lưu thay đổi vào cơ sở dữ liệu
                        MessageBox.Show("Xóa quyền của nhân viên thành công.");
                        LoadData();
                        LoadEmpRolesData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Xóa quyền không thành công: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy quyền cần xóa.");
                }
            }
        }

        private void cboNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra nếu không có giá trị nào được chọn
            if (cboNhanVien.SelectedValue == null)
                return;
            // Lấy giá trị EmployeeId từ ComboBox
            string selectedEmployeeId = cboNhanVien.SelectedValue as string;

            if (selectedEmployeeId == "ALL")
            {
                // Nếu chọn "Tất cả nhân viên", gọi hàm LoadEmpRolesData để hiển thị toàn bộ quyền
                LoadEmpRolesData();
            }
            else
            {
                // Nếu chọn một nhân viên cụ thể, tải quyền của nhân viên đó
                LoadEmpRolesByEmployee(selectedEmployeeId);
            }
        }
        private void LoadEmpRolesByEmployee(string employeeId)
        {
            try
            {
                using (var context = new db_flowerDataContext())
                {
                    var empRoles = from er in context.emp_roles
                                   join p in context.emp_permissions on er.perm_id equals p.perm_id
                                   join e in context.employees on er.employee_id equals e.employee_id
                                   where er.employee_id == employeeId
                                   select new
                                   {
                                       EmployeeId = er.employee_id,
                                       PermId = er.perm_id,
                                       EmployeeName = e.name, // Lấy tên nhân viên
                                       PermName = p.name      // Lấy tên quyền
                                   };

                    // Hiển thị dữ liệu trong DataGridView
                    dgvDanhSach.DataSource = empRoles.ToList();

                    // Cài đặt tiêu đề cột cho DataGridView
                    dgvDanhSach.Columns["EmployeeId"].HeaderText = "Mã Nhân Viên";
                    dgvDanhSach.Columns["PermId"].HeaderText = "Mã Quyền";
                    dgvDanhSach.Columns["EmployeeName"].HeaderText = "Tên Nhân Viên";
                    dgvDanhSach.Columns["PermName"].HeaderText = "Tên Quyền";

                    // Ẩn cột EmployeeId và PermId
                    dgvDanhSach.Columns["EmployeeId"].Visible = false;
                    dgvDanhSach.Columns["PermId"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data for selected employee: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
