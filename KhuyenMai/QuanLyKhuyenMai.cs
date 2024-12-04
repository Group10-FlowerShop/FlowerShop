using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowerShop.KhuyenMai
{
    public partial class QuanLyKhuyenMai : UserControl
    {
        db_flowerDataContext fs = new db_flowerDataContext();
        public QuanLyKhuyenMai()
        {
            InitializeComponent();
        }

        void loadKhuyenmai()
        {
            var tblpromotion = from pm in fs.promotions
                               select pm;
            dgrvKhuyenmai.DataSource = tblpromotion;
            dgrvKhuyenmai.AllowUserToAddRows = false;
        }

        void load_cboLoaigiamgia()
        {
            cboLoaigiamgia.Items.Add("percentage");
            cboLoaigiamgia.Items.Add("fixed");
        }

        void load_cboTrangthai()
        {
            cboTrangthai.Items.Add("active");
            cboTrangthai.Items.Add("inactive");
        }

        void xoa_Dulieu()
        {
            txtMagiamgia.Clear();
            txtTengiamgia.Clear();
            txtMota.Clear();
            cboLoaigiamgia.SelectedIndex = -1;
            txtGiatrigiam.Clear();
            dtpNgaybd.Value = DateTime.Now;
            dtpNgaykt.Value = DateTime.Now;
            txtTongtoithieu.Clear();
            txtGiamtoida.Clear();
            txtSoluong.Clear();
            txtDasudung.Clear();
            txtCodegiam.Clear();
            cboTrangthai.SelectedIndex = -1;
            dtpNgaytao.Value = DateTime.Now;
        }

        private void frmQuanLyKhuyenMai_Load(object sender, EventArgs e)
        {
            loadKhuyenmai();
            load_cboLoaigiamgia();
            load_cboTrangthai();
        }

        private void dgrvKhuyenmai_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                int index = dgrvKhuyenmai.CurrentRow.Index;
                txtMagiamgia.Text = dgrvKhuyenmai.Rows[index].Cells[0].Value.ToString();
                txtTengiamgia.Text = dgrvKhuyenmai.Rows[index].Cells[1].Value.ToString();
                txtMota.Text = dgrvKhuyenmai.Rows[index].Cells[2].Value.ToString();
                cboLoaigiamgia.SelectedItem = dgrvKhuyenmai.Rows[index].Cells[3].Value.ToString();
                txtGiatrigiam.Text = dgrvKhuyenmai.Rows[index].Cells[4].Value.ToString();
                if (DateTime.TryParse(dgrvKhuyenmai.Rows[index].Cells[5].Value.ToString(), out DateTime dateValue))
                {
                    dtpNgaybd.Value = dateValue;
                }
                else
                {
                    dtpNgaybd.Value = DateTime.Now;
                }
                if (DateTime.TryParse(dgrvKhuyenmai.Rows[index].Cells[6].Value.ToString(), out dateValue))
                {
                    dtpNgaykt.Value = dateValue;
                }
                else
                {
                    dtpNgaykt.Value = DateTime.Now;
                }
                if (dgrvKhuyenmai.Rows[index].Cells[7].Value != null)
                {
                    txtTongtoithieu.Text = dgrvKhuyenmai.Rows[index].Cells[7].Value.ToString();
                }
                else
                {
                    txtTongtoithieu.Text = string.Empty;
                }
                if (dgrvKhuyenmai.Rows[index].Cells[8].Value != null)
                {
                    txtGiamtoida.Text = dgrvKhuyenmai.Rows[index].Cells[8].Value.ToString();
                }
                else
                {
                    txtGiamtoida.Text = string.Empty;
                }
                if (dgrvKhuyenmai.Rows[index].Cells[9].Value != null)
                {
                    txtSoluong.Text = dgrvKhuyenmai.Rows[index].Cells[9].Value.ToString();
                }
                else
                {
                    txtSoluong.Text = string.Empty;
                }
                txtDasudung.Text = dgrvKhuyenmai.Rows[index].Cells[10].Value.ToString();
                txtCodegiam.Text = dgrvKhuyenmai.Rows[index].Cells[11].Value.ToString();
                cboTrangthai.SelectedItem = dgrvKhuyenmai.Rows[index].Cells[12].Value.ToString();
                if (DateTime.TryParse(dgrvKhuyenmai.Rows[index].Cells[13].Value.ToString(), out dateValue))
                {
                    dtpNgaytao.Value = dateValue;
                }
                else
                {
                    dtpNgaytao.Value = DateTime.Now;
                }
                if (dgrvKhuyenmai.Rows[index].Cells[14].Value != null &&
                    !string.IsNullOrEmpty(dgrvKhuyenmai.Rows[index].Cells[14].Value.ToString()) &&
                    DateTime.TryParse(dgrvKhuyenmai.Rows[index].Cells[14].Value.ToString(), out dateValue))
                {
                    dtpNgaycapnhat.Value = dateValue;
                }
                else
                {
                    dtpNgaycapnhat.Value = DateTime.Now;
                }
            }
        }

        private void btnTaoma_Click(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtTengiamgia.Text == string.Empty || cboLoaigiamgia.SelectedIndex == -1 || txtGiatrigiam.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (dtpNgaykt.Value.Date < dtpNgaybd.Value.Date)
            {
                MessageBox.Show("Ngày kết thúc không được trước ngày bắt đầu. Tự động đặt lại ngày kết thúc.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgaykt.Value = dtpNgaybd.Value;
            }
            else if (dtpNgaytao.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày tạo không được sau ngày hiện tại. Tự động đặt lại ngày tạo.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgaytao.Value = DateTime.Now;
            }
            else
            {
                string promoId = txtMagiamgia.Text;
                var existingPromo = fs.promotions.SingleOrDefault(c => c.promotion_id == promoId);
                if (existingPromo != null)
                {
                    MessageBox.Show($"Mã khuyến mãi '{promoId}' đã tồn tại. Vui lòng nhấn nút tạo mã để tạo mới.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thêm khuyến mãi này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        promotion pr = new promotion();
                        pr.promotion_id = txtMagiamgia.Text;
                        pr.name = txtTengiamgia.Text;
                        pr.description = txtMota.Text;
                        pr.discount_type = cboLoaigiamgia.SelectedItem.ToString();
                        pr.discount_value = decimal.Parse(txtGiatrigiam.Text);
                        pr.start_date = dtpNgaybd.Value;
                        pr.end_date = dtpNgaykt.Value;
                        pr.min_order_amount = decimal.Parse(txtTongtoithieu.Text);
                        pr.max_discount = decimal.Parse(txtGiamtoida.Text);
                        pr.usage_limit = int.Parse(txtSoluong.Text);
                        pr.used_count = int.Parse(txtGiamtoida.Text);
                        pr.promo_code = txtCodegiam.Text;
                        pr.status = cboTrangthai.SelectedItem.ToString();
                        pr.created_at = dtpNgaytao.Value;
                        pr.updated_at = null;
                        fs.promotions.InsertOnSubmit(pr);
                        fs.SubmitChanges();
                        loadKhuyenmai();
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

        private void btnHuy_Click(object sender, EventArgs e)
        {
            xoa_Dulieu();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMagiamgia.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng chọn 1 khuyến mãi để xoá", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xoá khuyến mãi này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        string maGg = txtMagiamgia.Text;
                        promotion pr = fs.promotions.Where(t => t.promotion_id == maGg).FirstOrDefault();
                        fs.promotions.DeleteOnSubmit(pr);
                        fs.SubmitChanges();
                        loadKhuyenmai();
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
            if (string.IsNullOrWhiteSpace(txtMagiamgia.Text))
            {
                MessageBox.Show("Vui lòng chọn 1 khuyến mãi", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn cập nhật khuyến mãi này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                try
                {
                    string maGg = txtMagiamgia.Text;
                    promotion pr = fs.promotions.Where(t => t.promotion_id == maGg).FirstOrDefault();
                    if (pr != null)
                    {
                        pr.name = txtTengiamgia.Text;
                        pr.description = txtMota.Text;
                        pr.discount_type = cboLoaigiamgia.SelectedItem?.ToString();

                        if (decimal.TryParse(txtGiatrigiam.Text, out decimal discountValue))
                            pr.discount_value = discountValue;
                        else
                            throw new FormatException("Giá trị giảm không hợp lệ!");

                        pr.start_date = dtpNgaybd.Value;
                        pr.end_date = dtpNgaykt.Value;

                        if (decimal.TryParse(txtTongtoithieu.Text, out decimal minOrderAmount))
                            pr.min_order_amount = minOrderAmount;
                        else
                            throw new FormatException("Tổng tối thiểu không hợp lệ!");

                        if (decimal.TryParse(txtGiamtoida.Text, out decimal maxDiscount))
                            pr.max_discount = maxDiscount;
                        else
                            throw new FormatException("Giảm tối đa không hợp lệ!");

                        if (int.TryParse(txtSoluong.Text, out int usageLimit))
                            pr.usage_limit = usageLimit;
                        else
                            throw new FormatException("Số lượng không hợp lệ!");

                        if (int.TryParse(txtDasudung.Text, out int usedCount))
                            pr.used_count = usedCount;
                        else
                            throw new FormatException("Số lượng đã sử dụng không hợp lệ!");

                        pr.promo_code = txtCodegiam.Text;
                        pr.status = cboTrangthai.SelectedItem?.ToString();
                        pr.created_at = dtpNgaytao.Value;
                        pr.updated_at = dtpNgaycapnhat.Value;

                        fs.SubmitChanges();
                        loadKhuyenmai();
                        MessageBox.Show("Cập nhật thành công");
                        xoa_Dulieu();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy khuyến mãi để cập nhật", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (FormatException fe)
                {
                    MessageBox.Show(fe.Message, "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void txtGiatrigiam_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            TextBox textBox = sender as TextBox;
            if (e.KeyChar == '.' && textBox.Text.Contains('.'))
            {
                e.Handled = true;
            }
        }

        private void txtTongtoithieu_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTongtoithieu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            TextBox textBox = sender as TextBox;
            if (e.KeyChar == '.' && textBox.Text.Contains('.'))
            {
                e.Handled = true;
            }
        }

        private void txtGiamtoida_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            TextBox textBox = sender as TextBox;
            if (e.KeyChar == '.' && textBox.Text.Contains('.'))
            {
                e.Handled = true;
            }
        }

        private void txtSoluong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtDasudung_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void dgrvKhuyenmai_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                int index = dgrvKhuyenmai.CurrentRow.Index;
                txtMagiamgia.Text = dgrvKhuyenmai.Rows[index].Cells[0].Value.ToString();
                txtTengiamgia.Text = dgrvKhuyenmai.Rows[index].Cells[1].Value.ToString();
                txtMota.Text = dgrvKhuyenmai.Rows[index].Cells[2].Value.ToString();
                cboLoaigiamgia.SelectedItem = dgrvKhuyenmai.Rows[index].Cells[3].Value.ToString();
                txtGiatrigiam.Text = dgrvKhuyenmai.Rows[index].Cells[4].Value.ToString();
                if (DateTime.TryParse(dgrvKhuyenmai.Rows[index].Cells[5].Value.ToString(), out DateTime dateValue))
                {
                    dtpNgaybd.Value = dateValue;
                }
                else
                {
                    dtpNgaybd.Value = DateTime.Now;
                }
                if (DateTime.TryParse(dgrvKhuyenmai.Rows[index].Cells[6].Value.ToString(), out dateValue))
                {
                    dtpNgaykt.Value = dateValue;
                }
                else
                {
                    dtpNgaykt.Value = DateTime.Now;
                }
                if (dgrvKhuyenmai.Rows[index].Cells[7].Value != null)
                {
                    txtTongtoithieu.Text = dgrvKhuyenmai.Rows[index].Cells[7].Value.ToString();
                }
                else
                {
                    txtTongtoithieu.Text = string.Empty;
                }
                if (dgrvKhuyenmai.Rows[index].Cells[8].Value != null)
                {
                    txtGiamtoida.Text = dgrvKhuyenmai.Rows[index].Cells[8].Value.ToString();
                }
                else
                {
                    txtGiamtoida.Text = string.Empty;
                }
                if (dgrvKhuyenmai.Rows[index].Cells[9].Value != null)
                {
                    txtSoluong.Text = dgrvKhuyenmai.Rows[index].Cells[9].Value.ToString();
                }
                else
                {
                    txtSoluong.Text = string.Empty;
                }
                txtDasudung.Text = dgrvKhuyenmai.Rows[index].Cells[10].Value.ToString();
                txtCodegiam.Text = dgrvKhuyenmai.Rows[index].Cells[11].Value.ToString();
                cboTrangthai.SelectedItem = dgrvKhuyenmai.Rows[index].Cells[12].Value.ToString();
                if (DateTime.TryParse(dgrvKhuyenmai.Rows[index].Cells[13].Value.ToString(), out dateValue))
                {
                    dtpNgaytao.Value = dateValue;
                }
                else
                {
                    dtpNgaytao.Value = DateTime.Now;
                }
                if (dgrvKhuyenmai.Rows[index].Cells[14].Value != null &&
                    !string.IsNullOrEmpty(dgrvKhuyenmai.Rows[index].Cells[14].Value.ToString()) &&
                    DateTime.TryParse(dgrvKhuyenmai.Rows[index].Cells[14].Value.ToString(), out dateValue))
                {
                    dtpNgaycapnhat.Value = dateValue;
                }
                else
                {
                    dtpNgaycapnhat.Value = DateTime.Now;
                }
            }
        }  
    }
}
