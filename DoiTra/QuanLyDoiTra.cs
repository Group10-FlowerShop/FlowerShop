using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowerShop.DoiTra
{
    public partial class QuanLyDoiTra : UserControl
    {
        private db_flowerDataContext db = new db_flowerDataContext();
        private string MaNV = "EMP001";
        private ChiTietDoiTra chiTietControl;
        public QuanLyDoiTra()
        {
            InitializeComponent();
        }

        private void LoadReturns()
        {
            var returnList = from r in db.returns
                             join c in db.customers on r.customer_id equals c.customer_id
                             join o in db.orders on r.order_id equals o.order_id
                             select new
                             {
                                 r.return_id,
                                 r.order_id,
                                 c.customer_id,
                                 r.return_date,
                                 r.reason,
                                 r.status,
                                 r.processed_by
                             };

            dgv_doiTra.DataSource = returnList.ToList();

            dgv_doiTra.Columns["return_id"].HeaderText = "Mã Đổi Trả";
            dgv_doiTra.Columns["order_id"].HeaderText = "Mã Đơn Hàng";
            dgv_doiTra.Columns["customer_id"].HeaderText = "Mã Khách Hàng";
            dgv_doiTra.Columns["return_date"].HeaderText = "Ngày Đổi Trả";
            dgv_doiTra.Columns["reason"].HeaderText = "Lý Do";
            dgv_doiTra.Columns["status"].HeaderText = "Trạng Thái";
            dgv_doiTra.Columns["processed_by"].HeaderText = "Cập Nhật Bởi";

            LoadOrderIds();
            LoadCustomerIds();
            LoadStatus();
        }

        private void LoadOrderIds()
        {
            var orders = from o in db.orders
                         select new
                         {
                             o.order_id
                         };

            cbo_maDonHang.DataSource = orders.ToList();
            cbo_maDonHang.DisplayMember = "order_id";
            cbo_maDonHang.ValueMember = "order_id";
        }

        private void LoadCustomerIds()
        {
            var customers = from c in db.customers
                            select new
                            {
                                c.customer_id,
                                c.name
                            };

            cbo_maKhachHang.DataSource = customers.ToList();
            cbo_maKhachHang.DisplayMember = "customer_id";
            cbo_maKhachHang.ValueMember = "customer_id";
        }

        private void LoadStatus()
        {
            cbo_trangThai.Items.Clear();
            cbo_trangThai.Items.Add("");
            cbo_trangThai.Items.Add("processing");
            cbo_trangThai.Items.Add("cancelled");
            cbo_trangThai.Items.Add("completed");
            cbo_trangThai.SelectedIndex = 0;
        }

        private void FormQLDoiTra_Load(object sender, EventArgs e)
        {
            LoadReturns();
        }

        private void dgv_doiTra_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgv_doiTra.Rows[e.RowIndex];
                txt_maDoiTra.Text = row.Cells["return_id"].Value.ToString();
                cbo_maDonHang.SelectedValue = row.Cells["order_id"].Value.ToString();
                cbo_maKhachHang.SelectedValue = row.Cells["customer_id"].Value.ToString();
                dt_doiTra.Value = Convert.ToDateTime(row.Cells["return_date"].Value);
                txt_lyDo.Text = row.Cells["reason"].Value.ToString();
                cbo_trangThai.SelectedItem = row.Cells["status"].Value.ToString();

                btn_chiTiet.Enabled = true;
            }
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_lyDo.Text))
                {
                    MessageBox.Show("Lý do không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string newReturnId = GenerateReturnId();

                var newReturn = new @return
                {
                    return_id = newReturnId,
                    order_id = cbo_maDonHang.SelectedValue.ToString(),
                    customer_id = cbo_maKhachHang.SelectedValue.ToString(),
                    return_date = dt_doiTra.Value.Date,
                    reason = txt_lyDo.Text.Trim(),
                    status = "processing",
                    processed_by = MaNV
                };

                db.returns.InsertOnSubmit(newReturn);
                db.SubmitChanges();
                MessageBox.Show("Thêm đơn đổi trả thành công!");
                LoadReturns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private string GenerateReturnId()
        {
            var lastReturn = db.returns
                .OrderByDescending(r => r.return_id)
                .FirstOrDefault();

            if (lastReturn != null)
            {
                string lastId = lastReturn.return_id;
                int nextNumber = int.Parse(lastId.Substring(3)) + 1;
                return "RET" + nextNumber.ToString("D3");
            }
            else
            {
                return "RET001";
            }
        }

        private void btn_capNhat_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_maDoiTra.Text))
                {
                    MessageBox.Show("Vui lòng chọn đơn đổi trả cần cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var updateReturn = db.returns.SingleOrDefault(r => r.return_id == txt_maDoiTra.Text.Trim());
                if (updateReturn != null)
                {
                    string selectedStatus = cbo_trangThai.SelectedItem?.ToString();
                    if (string.IsNullOrWhiteSpace(selectedStatus))
                    {
                        MessageBox.Show("Trạng thái không hợp lệ. Vui lòng chọn trạng thái từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    updateReturn.status = selectedStatus;
                    db.SubmitChanges();

                    MessageBox.Show("Cập nhật trạng thái đổi trả thành công!");
                    LoadReturns();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy đơn đổi trả để cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_maDoiTra.Text))
                {
                    MessageBox.Show("Vui lòng chọn đơn đổi trả cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var deleteReturn = db.returns.SingleOrDefault(r => r.return_id == txt_maDoiTra.Text.Trim());
                if (deleteReturn != null)
                {
                    var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa đơn đổi trả này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirmResult == DialogResult.Yes)
                    {
                        db.returns.DeleteOnSubmit(deleteReturn);
                        db.SubmitChanges();
                        MessageBox.Show("Xóa đơn đổi trả thành công!");
                        LoadReturns();
                        txt_maDoiTra.Clear();
                        txt_lyDo.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy đơn đổi trả để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi: Tồn tại dữ liệu trong đơn đổi trả này!!");
            }
        }

        private void btn_loc_Click(object sender, EventArgs e)
        {
            try
            {
                string maReturn = txt_maDoiTra.Text.Trim();
                string status = cbo_trangThai.SelectedItem?.ToString();

                var searchResult = from r in db.returns
                                   join o in db.orders on r.order_id equals o.order_id
                                   join c in db.customers on r.customer_id equals c.customer_id
                                   where (string.IsNullOrEmpty(maReturn) || r.return_id.Contains(maReturn))
                                         && (string.IsNullOrEmpty(status) || r.status == status)
                                   select new
                                   {
                                       r.return_id,
                                       r.order_id,
                                       r.customer_id,
                                       r.return_date,
                                       r.reason,
                                       r.status,
                                       r.processed_by
                                   };

                dgv_doiTra.DataSource = searchResult.ToList();

                if (!searchResult.Any())
                {
                    MessageBox.Show("Không tìm thấy kết quả phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_chiTiet_Click(object sender, EventArgs e)
        {
            string maDoiTra = txt_maDoiTra.Text.Trim();

            if (!string.IsNullOrWhiteSpace(maDoiTra))
            {
                bool exists = dgv_doiTra.Rows
                    .Cast<DataGridViewRow>()
                    .Any(row => row.Cells["return_id"].Value?.ToString() == maDoiTra);

                if (exists)
                {
                    // Ẩn tất cả các control hiện tại
                    foreach (Control control in this.Controls)
                    {
                        control.Visible = false;
                    }

                    // Tạo và hiển thị ChiTietDoiTra

                    chiTietControl = new ChiTietDoiTra(maDoiTra);
                    chiTietControl.Dock = DockStyle.Fill;
                    // Đăng ký sự kiện BackButtonClicked
                    chiTietControl.BackButtonClicked += ChiTietControl_BackButtonClicked;
                    this.Controls.Add(chiTietControl);
                    chiTietControl.BringToFront();
                }
                else
                {
                    MessageBox.Show("Mã đổi trả không tồn tại trong danh sách!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một mã đổi trả!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgv_doiTra_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_doiTra.SelectedRows.Count == 0)
            {
                btn_chiTiet.Enabled = false;
            }
        }

        private void ChiTietControl_BackButtonClicked(object sender, EventArgs e)
        {
            this.Controls.Remove(chiTietControl);

            foreach (Control control in this.Controls)
            {
                control.Visible = true;
            }
        }
    }
}
