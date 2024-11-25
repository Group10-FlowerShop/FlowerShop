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

namespace FlowerShop.DonHang
{
    public partial class QuanLyDonHang : UserControl
    {
        db_flowerDataContext fs = new db_flowerDataContext();
        private ChiTietDonHang donhang1;
        public QuanLyDonHang()
        {
            InitializeComponent();
            load_cboTrangthai();
            load_cboThanhtoan();
            load_Donhang();
            load_Tongtien();
        }

        void load_cboKh()
        {
            var tblcustomer = from cs in fs.customers
                              select new { cs.customer_id, cs.name };

            cboMakh.DataSource = tblcustomer.ToList();
            cboMakh.DisplayMember = "name";
            cboMakh.ValueMember = "customer_id";
            AutoResizeDataGridView(dgrvDonhang);
        }
        public void AutoResizeDataGridView(DataGridView dgv)
        {
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                column.MinimumWidth = 50;
            }
        }
        public void load_Tongtien()
        {
            var ordersToUpdate = fs.orders.ToList();

            foreach (var order in ordersToUpdate)
            {
                decimal totalAmount = fs.order_details
                                         .Where(od => od.order_id == order.order_id)
                                         .Sum(od => (decimal?)od.quantity * (decimal?)od.price) ?? 0m;

                order.total_amount = totalAmount;
            }

            fs.SubmitChanges(); // Lưu thay đổi vào cơ sở dữ liệu
        }

        public void xoa_Dgrv()
        {
            dgrvDonhang.DataSource = null;

        }

        public void load_Donhang()
        {
            var tblorder = from od in fs.orders
                           select new
                           {
                               od.order_id,
                               od.customer_id,
                               od.order_date,
                               od.total_amount,// Hiển thị tổng tiền
                               od.shipping_address,
                               od.shipping_phone,
                               od.status,
                               od.payment_method
                           };

            // Gán dữ liệu vào DataGridView
            dgrvDonhang.DataSource = tblorder.ToList();
            dgrvDonhang.AllowUserToAddRows = false;

            // Tùy chỉnh cột nếu cần
            if (dgrvDonhang.Columns["customer"] != null)
                dgrvDonhang.Columns["customer"].Visible = false;
        }

        void load_cboTrangthai()
        {
            cboTrangthai.Items.Add("completed");
            cboTrangthai.Items.Add("processing");
            cboTrangthai.Items.Add("cancelled");
        }

        void load_cboThanhtoan()
        {
            cboLoaithanhtoan.Items.Add("cash");
            cboLoaithanhtoan.Items.Add("banking");
            cboLoaithanhtoan.Items.Add("momo");
        }

        void xoa_Dulieu()
        {
            txtMadonhang.Clear(); ;
            cboMakh.SelectedValue = -1;
            cboLoaithanhtoan.SelectedValue = -1;
            cboLoaithanhtoan.SelectedValue = -1;
            txtTongtien.Text = "0";
        }

        private void frmQuanLyDonHang_Load(object sender, EventArgs e)
        {

            load_cboKh();
            load_Donhang();
            cboMakh.SelectedValue = -1;
            cboTrangthai.SelectedValue = -1;
            cboLoaithanhtoan.SelectedValue = -1;
            txtTongtien.Text = "0";

        }

        private void btnTaoma_Click(object sender, EventArgs e)
        {
            using (var context = new db_flowerDataContext())
            {
                var maDhCuoi = context.orders.OrderByDescending(t => t.order_id).FirstOrDefault();
                string maDhMoi;

                if (maDhCuoi != null)
                {
                    string latestID = maDhCuoi.order_id;
                    int ma = int.Parse(latestID.Substring(3));
                    maDhMoi = "ORD" + (ma + 1).ToString("D3");
                }
                else
                {
                    maDhMoi = "ORD001";
                }
                txtMadonhang.Text = maDhMoi;
            }
        }

        private void dgrvDonhang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dgrvDonhang.CurrentRow.Index;
            txtMadonhang.Text = dgrvDonhang.Rows[index].Cells[0].Value.ToString();
            cboMakh.SelectedValue = dgrvDonhang.Rows[index].Cells[1].Value.ToString();
            if (DateTime.TryParse(dgrvDonhang.Rows[index].Cells[2].Value.ToString(), out DateTime dateValue))
            {
                dtpNgaydat.Value = dateValue;
            }
            else
            {
                dtpNgaydat.Value = DateTime.Now;
            }
            txtTongtien.Text = dgrvDonhang.Rows[index].Cells[3].Value.ToString();
            txtDiachi.Text = dgrvDonhang.Rows[index].Cells[4].Value.ToString();
            txtSdt.Text = dgrvDonhang.Rows[index].Cells[5].Value.ToString();
            cboTrangthai.SelectedIndex = cboTrangthai.Items.IndexOf(dgrvDonhang.Rows[index].Cells[6].Value.ToString());
            cboLoaithanhtoan.SelectedIndex = cboLoaithanhtoan.Items.IndexOf(dgrvDonhang.Rows[index].Cells[7].Value.ToString());

        }

        private void btnThem_Click(object sender, EventArgs e)
        {

            if (txtMadonhang.Text == string.Empty || cboMakh.SelectedValue.ToString() == string.Empty||cboTrangthai.SelectedItem.ToString()==string.Empty||txtDiachi.Text==string.Empty||txtSdt.Text==string.Empty)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin !!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(txtTongtien.Text != string.Empty&&txtTongtien.Text!="0")
            {
                MessageBox.Show("Đặt lại tổng tiền về 0", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTongtien.Text = "0";
            }    
            else
            {
                string maDh = txtMadonhang.Text;
                var existingOrder = fs.orders.SingleOrDefault(c => c.order_id == maDh);
                if (existingOrder != null)
                {
                    MessageBox.Show($"Mã đơn hàng '{maDh}' đã tồn tại. Vui lòng nhấn nút tạo mới.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thêm đơn hàng này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        order od = new order();
                        od.order_id = txtMadonhang.Text;
                        od.customer_id = cboMakh.SelectedValue.ToString();
                        od.order_date = dtpNgaydat.Value;
                        od.total_amount = decimal.Parse(txtTongtien.Text);
                        od.shipping_address = txtDiachi.Text;
                        od.shipping_phone = txtSdt.Text;
                        od.status = cboTrangthai.SelectedItem.ToString();
                        od.payment_method = cboLoaithanhtoan.SelectedItem.ToString();
                        fs.orders.InsertOnSubmit(od);
                        fs.SubmitChanges();
                        load_Donhang();
                        MessageBox.Show("Thêm thành công");
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

        private void txtTongtien_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMadonhang.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng chọn 1 đơn hàng để xoá", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xoá đơn hàng này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        string maDon = txtMadonhang.Text;
                        order od = fs.orders.Where(t => t.order_id == maDon).FirstOrDefault();
                        fs.orders.DeleteOnSubmit(od);
                        fs.SubmitChanges();
                        load_Donhang();
                        MessageBox.Show("Xoá thành công");
                        xoa_Dulieu();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMadonhang.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng chọn 1 đơn hàng để cập nhật", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn cập nhật đơn hàng này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    string maDon = txtMadonhang.Text;
                    order od = fs.orders.Where(t => t.order_id == maDon).FirstOrDefault();
                    od.customer_id = cboMakh.SelectedValue.ToString();
                    od.order_date = dtpNgaydat.Value;
                    od.shipping_address = txtDiachi.Text;
                    od.shipping_phone = txtSdt.Text;
                    od.status = cboTrangthai.SelectedItem.ToString();
                    od.payment_method = cboLoaithanhtoan.SelectedItem.ToString();
                    fs.SubmitChanges();
                    load_Donhang();
                    MessageBox.Show("Cập nhật thành công");
                    xoa_Dulieu();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại");
                }
            }
        }

        private void btnXemchitiet_Click(object sender, EventArgs e)
        {
            string madon = txtMadonhang.Text.Trim();

            if (!string.IsNullOrWhiteSpace(madon))
            {
                bool exists = dgrvDonhang.Rows
                    .Cast<DataGridViewRow>()
                    .Any(row => row.Cells["Column1"].Value?.ToString() == madon);

                if (exists)
                {
                    // Ẩn tất cả các control hiện tại
                    foreach (Control control in this.Controls)
                    {
                        control.Visible = false;
                    }

                    // Tạo và hiển thị ChiTietDoiTra

                    donhang1 = new ChiTietDonHang(madon);
                    donhang1.Dock = DockStyle.Fill;
                    // Đăng ký sự kiện BackButtonClicked
                    donhang1.BackButtonClicked += ChiTietControl_BackButtonClicked;
                    this.Controls.Add(donhang1);
                    donhang1.BringToFront();
                    
                }
                else
                {
                    MessageBox.Show("Mã đơn hàng không tồn tại!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một mã đơn hàng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void ChiTietControl_BackButtonClicked(object sender, EventArgs e)
        {
            this.Controls.Remove(donhang1);

            foreach (Control control in this.Controls)
            {
                control.Visible = true;
            }
            //load_Tongtien();
            load_Donhang();
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
