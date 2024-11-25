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
    public partial class QuanLyHoaKhuyenMai : UserControl
    {
        db_flowerDataContext fs = new db_flowerDataContext();
        public QuanLyHoaKhuyenMai()
        {
            InitializeComponent();
        }

        void load_cboKhuyenMai()
        {
            var tblpromotion = from pr in fs.promotions
                               select new { pr.promotion_id, pr.name };

            cboMakhuyenmai.DataSource = tblpromotion.ToList();
            cboMakhuyenmai.DisplayMember = "name";
            cboMakhuyenmai.ValueMember = "promotion_id";
        }
        public void AutoResizeDataGridView(DataGridView dgv)
        {
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                column.MinimumWidth = 50;
            }
        }
        void load_cboMahoa()
        {
            var tblflower = from fl in fs.flowers
                            select new { fl.flower_id, fl.name };
            cboMahoa.DataSource = tblflower.ToList();
            cboMahoa.DisplayMember = "name";
            cboMahoa.ValueMember = "flower_id";
        }

        void loadHoaKhuyenMai()
        {
            var tblpromation_flower = from pf in fs.promotion_flowers
                                      select pf;
            dgrvHoakhuyenmai.DataSource = tblpromation_flower;
            dgrvHoakhuyenmai.Columns["promotion"].Visible = false;
            dgrvHoakhuyenmai.Columns["flower"].Visible = false;
            dgrvHoakhuyenmai.AllowUserToAddRows = false;
        }

        void xoa_Dulieu()
        {
            cboMakhuyenmai.SelectedValue = -1;
            cboMahoa.SelectedValue = -1;
            txtGiatrigiam.Clear();
            txtLoaigiamgia.Clear();
        }

        private void frmQuanLyHoaKhuyenMai_Load(object sender, EventArgs e)
        {
            loadHoaKhuyenMai();
            load_cboKhuyenMai();
            cboMakhuyenmai.SelectedValue = -1;
            load_cboMahoa();
            cboMahoa.SelectedValue = -1;
            AutoResizeDataGridView(dgrvHoakhuyenmai);
        }

        private void cboMakhuyenmai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMakhuyenmai.SelectedIndex != -1)
            {
                string selectedPromotionId = cboMakhuyenmai.SelectedValue.ToString();
                var promotion = fs.promotions.FirstOrDefault(p => p.promotion_id == selectedPromotionId);

                if (promotion != null)
                {
                    txtLoaigiamgia.Text = promotion.discount_type;
                    txtGiatrigiam.Text = promotion.discount_value.ToString();
                }
                //else
                //{
                //    MessageBox.Show("Không tìm thấy thông tin khuyến mãi tương ứng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
            }
            else
            {
                txtGiatrigiam.Clear();
                txtLoaigiamgia.Clear();
            }
        }

        private void dgrvHoakhuyenmai_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                int index = dgrvHoakhuyenmai.CurrentRow.Index;
                cboMakhuyenmai.SelectedValue = dgrvHoakhuyenmai.Rows[index].Cells[0].Value.ToString();
                cboMahoa.SelectedValue = dgrvHoakhuyenmai.Rows[index].Cells[1].Value.ToString();
                txtLoaigiamgia.Text = dgrvHoakhuyenmai.Rows[index].Cells[2].Value.ToString();
                txtGiatrigiam.Text = dgrvHoakhuyenmai.Rows[index].Cells[3].Value.ToString();
            }
        }

        private void dgrvHoakhuyenmai_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                int index = dgrvHoakhuyenmai.CurrentRow.Index;
                cboMakhuyenmai.SelectedValue = dgrvHoakhuyenmai.Rows[index].Cells[0].Value.ToString();
                cboMahoa.SelectedValue = dgrvHoakhuyenmai.Rows[index].Cells[1].Value.ToString();
                txtLoaigiamgia.Text = dgrvHoakhuyenmai.Rows[index].Cells[2].Value.ToString();
                txtGiatrigiam.Text = dgrvHoakhuyenmai.Rows[index].Cells[3].Value.ToString();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            xoa_Dulieu();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cboMakhuyenmai.SelectedIndex == -1 || cboMahoa.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thêm khuyến mãi cho hoa này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        promotion_flower pf = new promotion_flower();
                        string promotionId = cboMakhuyenmai.SelectedValue.ToString();
                        pf.promotion_id = promotionId;
                        pf.flower_id = cboMahoa.SelectedValue.ToString();
                        pf.discount_type = txtLoaigiamgia.Text;
                        if (decimal.TryParse(txtGiatrigiam.Text, out decimal discountValue))
                            pf.discount_value = discountValue;
                        else
                            throw new FormatException("Giá trị giảm không hợp lệ!");
                        fs.promotion_flowers.InsertOnSubmit(pf);
                        fs.SubmitChanges();
                        loadHoaKhuyenMai();
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
            if (cboMakhuyenmai.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn 1 loại hoa đang khuyến mãi", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xoá khuyến mãi cho loại hoa này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        string maKm = cboMakhuyenmai.SelectedValue.ToString();
                        promotion_flower pf = fs.promotion_flowers.Where(t => t.promotion_id == maKm).FirstOrDefault();
                        fs.promotion_flowers.DeleteOnSubmit(pf);
                        fs.SubmitChanges();
                        loadHoaKhuyenMai();
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
    }
}
