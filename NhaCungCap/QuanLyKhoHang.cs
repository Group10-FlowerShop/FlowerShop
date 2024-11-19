using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowerShop.NhaCungCap
{
    public partial class QuanLyKhoHang : UserControl
    {
        private db_flowerDataContext db = new db_flowerDataContext();

        public QuanLyKhoHang()
        {
            InitializeComponent();
        }

        private void LoadKhoHang()
        {
            var khoHang = from k in db.inventories
                          join h in db.flowers on k.flower_id equals h.flower_id
                          select new
                          {
                              k.flower_id,
                              h.name,
                              k.quantity,
                              k.last_updated
                          };
            dgv_khoHang.DataSource = khoHang.ToList();

            dgv_khoHang.Columns["flower_id"].HeaderText = "Mã Hoa";
            dgv_khoHang.Columns["name"].HeaderText = "Tên Hoa";
            dgv_khoHang.Columns["quantity"].HeaderText = "Số Lượng";
            dgv_khoHang.Columns["last_updated"].HeaderText = "Ngày Cập Nhật";
        }

        private void FormQLKho_Load(object sender, EventArgs e)
        {
            LoadKhoHang();
        }

        private void btn_timKiem_Click(object sender, EventArgs e)
        {
            string maHoa = txt_maHoa.Text.Trim();
            int soLuong;
            bool isQuantityValid = int.TryParse(txt_soLuong.Text.Trim(), out soLuong);

            var timKiem = from k in db.inventories
                          join h in db.flowers on k.flower_id equals h.flower_id
                          where (string.IsNullOrEmpty(maHoa) || k.flower_id.Contains(maHoa)) && (!isQuantityValid || k.quantity == soLuong)
                          select new
                          {
                              k.flower_id,
                              h.name,
                              k.quantity,
                              k.last_updated
                          };
            dgv_khoHang.DataSource = timKiem.ToList();

            if (!timKiem.Any())
            {
                MessageBox.Show("Không tìm thấy kết quả phù hợp!!!");
            }
        }

        private void dgv_khoHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgv_khoHang.Rows[e.RowIndex];
                txt_maHoa.Text = row.Cells["flower_id"].Value.ToString();
                txt_soLuong.Text = row.Cells["quantity"].Value.ToString();
            }
        }

        private void btn_capNhat_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_maHoa.Text))
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm cần cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var updateInventory = db.inventories.SingleOrDefault(i => i.flower_id == txt_maHoa.Text.Trim());

                if (updateInventory != null)
                {

                    if (int.TryParse(txt_soLuong.Text, out int soLuongMoi) && soLuongMoi >= 0)
                    {

                        updateInventory.quantity = soLuongMoi;
                        updateInventory.last_updated = DateTime.Now.Date;


                        db.SubmitChanges();

                        MessageBox.Show("Cập nhật kho hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadKhoHang();
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng nhập số lượng hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sản phẩm để cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txt_soLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }
    }
}
