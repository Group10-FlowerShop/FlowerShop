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
    public partial class QuanLyQuyen : UserControl
    {
        private db_flowerDataContext _context;
        public QuanLyQuyen()
        {
            InitializeComponent();
            _context = new db_flowerDataContext();
            
        }
        public void QuanLyQuyen_Load(object sender, EventArgs e)
        {
            LoadPermissions();
            AutoResizeDataGridView(dgvPermissions);
        }
        public void AutoResizeDataGridView(DataGridView dgv)
        {
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                column.MinimumWidth = 50;
            }
        }

        private void LoadPermissions()
        {
            try
            {
                var permissions = _context.emp_permissions.Select(p => new
                {
                    PermId = p.perm_id,
                    Name = p.name,
                    Description = p.description
                }).ToList();

                dgvPermissions.DataSource = permissions;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string description = txtDescription.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Tên quyền không được để trống.");
                return;
            }

            try
            {
                var newPermission = new emp_permission
                {
                    perm_id = GenerateNewPermId(),
                    name = name,
                    description = description
                };

                _context.emp_permissions.InsertOnSubmit(newPermission);
                _context.SubmitChanges();

                MessageBox.Show("Thêm quyền thành công.");
                LoadPermissions();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm quyền: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvPermissions.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn quyền cần xóa.");
                return;
            }

            string permId = dgvPermissions.SelectedRows[0].Cells["PermId"].Value.ToString();

            try
            {
                // Kiểm tra xem quyền này có đang được gán cho nhân viên nào không
                var roleAssignments = _context.emp_roles.Where(er => er.perm_id == permId).ToList();

                if (roleAssignments.Any())
                {
                    MessageBox.Show("Không thể xóa quyền này vì có nhân viên đang sử dụng quyền này.");
                    return;
                }

                // Nếu không có nhân viên nào sử dụng quyền, thì có thể xóa quyền
                var permission = _context.emp_permissions.SingleOrDefault(p => p.perm_id == permId);
                if (permission != null)
                {
                    _context.emp_permissions.DeleteOnSubmit(permission);
                    _context.SubmitChanges();

                    MessageBox.Show("Xóa quyền thành công.");
                    LoadPermissions(); // Tải lại danh sách quyền
                }
                else
                {
                    MessageBox.Show("Quyền không tồn tại.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa quyền: " + ex.Message);
            }
        }


        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvPermissions.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn quyền cần sửa.");
                return;
            }

            string permId = dgvPermissions.SelectedRows[0].Cells["PermId"].Value.ToString();
            string name = txtName.Text.Trim();
            string description = txtDescription.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Tên quyền không được để trống.");
                return;
            }

            try
            {
                var permission = _context.emp_permissions.SingleOrDefault(p => p.perm_id == permId);
                if (permission != null)
                {
                    permission.name = name;
                    permission.description = description;
                    _context.SubmitChanges();

                    MessageBox.Show("Cập nhật quyền thành công.");
                    LoadPermissions();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Quyền không tồn tại.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật quyền: " + ex.Message);
            }
        }

        private void dgvPermissions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu người dùng click vào một dòng hợp lệ (không phải header)
            if (e.RowIndex >= 0)
            {
                // Lấy dữ liệu của dòng được chọn
                DataGridViewRow row = dgvPermissions.Rows[e.RowIndex];

                // Gán giá trị của các cột cần thiết vào các TextBox
                txtName.Text = row.Cells["name"].Value.ToString();
                txtDescription.Text = row.Cells["description"].Value.ToString();
            }
        }

        private void ClearInputs()
        {
            txtName.Clear();
            txtDescription.Clear();
        }

        private string GenerateNewPermId()
        {
            // Tạo mã quyền mới với format 'PERMXXX' với XXX là số tự tăng
            var lastPerm = _context.emp_permissions.OrderByDescending(p => p.perm_id).FirstOrDefault();
            if (lastPerm != null && lastPerm.perm_id.StartsWith("PERM"))
            {
                int lastId = int.Parse(lastPerm.perm_id.Substring(4));
                return $"PERM{(lastId + 1):D3}";
            }
            return "PERM001";
        }

        
    }
}
