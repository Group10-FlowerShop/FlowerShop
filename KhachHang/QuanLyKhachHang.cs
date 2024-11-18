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
        db_flowerDataContext fs = new db_flowerDataContext();
        public QuanLyKhachHang()
        {
            InitializeComponent();
        }

        void load_Khachhang()
        {
            var tblcustomer = from kh in fs.customers
                              select kh;
            dgrvKhachhang.DataSource = tblcustomer;
        }
        void xoa_Dulieu()
        {
            txtMakh.Clear();
            txtHoten.Clear();
            txtEmail.Clear();
            txtSdt.Clear();
            txtDiachi.Clear();
        }

        private void frmQuanLyKhachHang_Load(object sender, EventArgs e)
        {
            load_Khachhang();
            AutoResizeDataGridView(dgrvKhachhang);
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

        bool kiemEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@gmail\.com$";
            return Regex.IsMatch(email, pattern);
        }
        private void btnTaoma_Click(object sender, EventArgs e)
        {
            using (var context = new db_flowerDataContext())
            {
                var maKhCuoi = context.customers.OrderByDescending(t => t.customer_id).FirstOrDefault();
                string maKhMoi;

                if (maKhCuoi != null)
                {
                    string latestID = maKhCuoi.customer_id;
                    int ma = int.Parse(latestID.Substring(2));

                    maKhMoi = "KH" + (ma + 1).ToString("D3");
                }
                else
                {
                    maKhMoi = "KH001";
                }
                txtMakh.Text = maKhMoi;
            }
        }

        private void dgrvKhachhang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dgrvKhachhang.CurrentRow.Index;
            txtMakh.Text = dgrvKhachhang.Rows[index].Cells[0].Value.ToString();
            txtHoten.Text = dgrvKhachhang.Rows[index].Cells[1].Value.ToString();
            txtEmail.Text = dgrvKhachhang.Rows[index].Cells[2].Value.ToString();
            if (dgrvKhachhang.Rows[index].Cells[3].Value.ToString() == null)
            {
                txtSdt.Text = "";
            }
            txtSdt.Text = dgrvKhachhang.Rows[index].Cells[3].Value.ToString();
            if (dgrvKhachhang.Rows[index].Cells[4].Value.ToString() == null)
            {
                txtDiachi.Text = "";
            }
            txtDiachi.Text = dgrvKhachhang.Rows[index].Cells[4].Value.ToString();
        }

        private void dgrvKhachhang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dgrvKhachhang.CurrentRow.Index;
            txtMakh.Text = dgrvKhachhang.Rows[index].Cells[0].Value.ToString();
            txtHoten.Text = dgrvKhachhang.Rows[index].Cells[1].Value.ToString();
            txtEmail.Text = dgrvKhachhang.Rows[index].Cells[2].Value.ToString();
            if (dgrvKhachhang.Rows[index].Cells[3].Value.ToString() == null)
            {
                txtSdt.Text = "";
            }
            txtSdt.Text = dgrvKhachhang.Rows[index].Cells[3].Value.ToString();
            if (dgrvKhachhang.Rows[index].Cells[4].Value.ToString() == null)
            {
                txtDiachi.Text = "";
            }
            txtDiachi.Text = dgrvKhachhang.Rows[index].Cells[4].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            if (txtMakh.Text == string.Empty || txtHoten.Text == string.Empty || txtEmail.Text == string.Empty || txtSdt.Text == string.Empty || txtDiachi.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!kiemEmail(email))
            {
                MessageBox.Show("Email phải có định dạng đúng @gmail.com.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtSdt.Text.Length < 10)
            {
                MessageBox.Show("Vui lòng nhập đủ 10 số", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string customerId = txtMakh.Text;
                var existingCustomer = fs.customers.SingleOrDefault(c => c.customer_id == customerId);
                if (existingCustomer != null)
                {
                    MessageBox.Show($"Mã khách hàng '{customerId}' đã tồn tại. Vui lòng nhấn nút tạo mã để tạo mới.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thêm khách hàng này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        customer cs = new customer();
                        cs.customer_id = customerId;
                        cs.name = txtHoten.Text;
                        cs.email = txtEmail.Text;
                        cs.phone = txtSdt.Text;
                        cs.address = txtDiachi.Text;

                        fs.customers.InsertOnSubmit(cs);
                        fs.SubmitChanges();

                        load_Khachhang();
                        MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        xoa_Dulieu();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Thêm thất bại");
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMakh.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng chọn 1 khách hàng để xoá", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xoá khách hàng này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        string maKh = txtMakh.Text;
                        customer cs = fs.customers.Where(t => t.customer_id == maKh).FirstOrDefault();
                        fs.customers.DeleteOnSubmit(cs);
                        fs.SubmitChanges();
                        load_Khachhang();
                        MessageBox.Show("Xoá thành công");
                        xoa_Dulieu();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Xoá thất bại");
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMakh.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng chọn 1 khách hàng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn cập nhật khách hàng này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        string maKh = txtMakh.Text;
                        customer cs = fs.customers.Where(t => t.customer_id == maKh).FirstOrDefault();
                        cs.name = txtHoten.Text;
                        cs.email = txtEmail.Text;
                        cs.phone = txtSdt.Text;
                        cs.address = txtDiachi.Text;
                        fs.SubmitChanges();
                        load_Khachhang();
                        MessageBox.Show("Cập nhật thành công");
                        xoa_Dulieu();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại");
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            xoa_Dulieu();
        }

        private void txtSdt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
            TextBox textBox = sender as TextBox;
            if (textBox.Text.Length >= 10 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
