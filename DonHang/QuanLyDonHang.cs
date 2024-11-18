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
        public QuanLyDonHang()
        {
            InitializeComponent();
        }

        void load_cboKh()
        {
            var tblcustomer = from cs in fs.customers
                              select new { cs.customer_id, cs.name };

            cboMakh.DataSource = tblcustomer.ToList();
            cboMakh.DisplayMember = "name";
            cboMakh.ValueMember = "customer_id";
        }

        public void load_Tongtien()
        {
            var ordersToUpdate = fs.orders.ToList();

            foreach (var order in ordersToUpdate)
            {
                // Tính tổng tiền, chuyển đổi kết quả thành decimal và thay thế null bằng 0
                decimal totalAmount = fs.order_details
                                        .Where(od => od.order_id == order.order_id)
                                        .Sum(od => (decimal?)od.quantity * (decimal?)od.price) ?? 0m; // Sử dụng nullable decimal và toán tử null-coalescing

                order.total_amount = totalAmount;
            }
            fs.SubmitChanges();
        }

        public void load_Donhang()
        {
            //load_Tongtien();
            var tblorder = from od in fs.orders
                           select od;
            dgrvDonhang.DataSource = tblorder;

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
            load_Donhang();
            load_cboKh();
            cboMakh.SelectedValue = -1;
            load_cboTrangthai();
            cboTrangthai.SelectedValue = -1;
            load_cboThanhtoan();
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
            cboTrangthai.SelectedItem = dgrvDonhang.Rows[index].Cells[4].Value.ToString();
            cboLoaithanhtoan.SelectedItem = dgrvDonhang.Rows[index].Cells[5].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {

            if (txtMadonhang.Text == string.Empty || cboMakh.SelectedValue.ToString() == string.Empty)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin !!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (txtTongtien.Text == string.Empty)
            {
                txtTongtien.Text = "0";
            }
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
                    od.status = cboTrangthai.SelectedItem.ToString();
                    od.payment_method = cboLoaithanhtoan.SelectedItem.ToString();
                    fs.SubmitChanges();
                    load_Donhang();
                    MessageBox.Show("Cập nhật thành công");
                    xoa_Dulieu();
                }
            }
        }

        //private void btnXemchitiet_Click(object sender, EventArgs e)
        //{
        //    if (dgrvDonhang.CurrentRow != null)
        //    {
        //        string orderId = txtMadonhang.Text;
        //        frmQuanLyCTDH frmCTDH = new frmQuanLyCTDH(orderId);
        //        frmCTDH.Owner = this;
        //        frmCTDH.ShowDialog();
        //    }
        //    else
        //    {
        //        MessageBox.Show("Vui lòng chọn một đơn hàng trước khi xem chi tiết.");
        //    }
        //}

        private void frmQuanLyDonHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đóng form này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                return;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            xoa_Dulieu();
        }
    }
}
