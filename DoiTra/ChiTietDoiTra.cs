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
    public partial class ChiTietDoiTra : UserControl
    {

        private string returnId;
        private db_flowerDataContext db = new db_flowerDataContext();
        public ChiTietDoiTra(string returnId)
        {
            InitializeComponent();
            this.returnId = returnId;
            txt_maDoiTra.Text = returnId;
            txt_maDoiTra.Enabled = false;
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

        private void FormChiTietDoiTra_Load(object sender, EventArgs e)
        {
            LoadFlowerIds();
            LoadChiTietDoiTra();
            AutoResizeDataGridView(dgv_chiTiet);
        }

        private void LoadChiTietDoiTra()
        {
            var chiTietDoiTra = from d in db.return_details
                                where d.return_id == returnId
                                select new
                                {
                                    d.flower_id,
                                    d.quantity,
                                    d.return_amount
                                };

            dgv_chiTiet.DataSource = chiTietDoiTra.ToList();

            dgv_chiTiet.Columns["flower_id"].HeaderText = "Mã Hoa";
            dgv_chiTiet.Columns["quantity"].HeaderText = "Số lượng";
            dgv_chiTiet.Columns["return_amount"].HeaderText = "Giá tiền";
        }

        private void LoadFlowerIds()
        {
            var flowers = from c in db.flowers
                          select new
                          {
                              c.flower_id
                          };

            cbo_maHoa.DataSource = flowers.ToList();
            cbo_maHoa.DisplayMember = "flower_id";
            cbo_maHoa.ValueMember = "flower_id";
        }

        private void dgv_chiTiet_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgv_chiTiet.Rows[e.RowIndex];
                cbo_maHoa.SelectedValue = row.Cells["flower_id"].Value.ToString();
                txt_soLuong.Text = row.Cells["quantity"].Value.ToString();
                txt_giaTien.Text = row.Cells["return_amount"].Value.ToString();
            }
        }

        private void txt_soLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void txt_giaTien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_soLuong.Text) || string.IsNullOrWhiteSpace(txt_giaTien.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ các trường giá trị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var existingDetail = db.return_details.SingleOrDefault(d =>
                d.return_id == returnId &&
                d.flower_id == cbo_maHoa.SelectedValue.ToString());

                if (existingDetail != null)
                {
                    MessageBox.Show("Chi tiết đổi trả với mã hoa này đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var newReturn_details = new return_detail
                {
                    return_id = returnId,
                    flower_id = cbo_maHoa.SelectedValue.ToString(),
                    quantity = int.Parse(txt_soLuong.Text.Trim()),
                    return_amount = decimal.Parse(txt_giaTien.Text.Trim())
                };

                db.return_details.InsertOnSubmit(newReturn_details);
                db.SubmitChanges();
                MessageBox.Show("Thêm chi tiết đơn đổi trả thành công!");
                LoadChiTietDoiTra();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btn_capNhat_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_soLuong.Text) || string.IsNullOrWhiteSpace(txt_giaTien.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ các trường giá trị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var existingDetail = db.return_details.SingleOrDefault(d =>
                    d.return_id == returnId &&
                    d.flower_id == cbo_maHoa.SelectedValue.ToString());

                if (existingDetail != null)
                {
                    existingDetail.quantity = int.Parse(txt_soLuong.Text.Trim());
                    existingDetail.return_amount = decimal.Parse(txt_giaTien.Text.Trim());

                    db.SubmitChanges();
                    MessageBox.Show("Cập nhật chi tiết đơn đổi trả thành công!");
                    LoadChiTietDoiTra();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy chi tiết đổi trả để cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            try
            {
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa chi tiết đổi trả này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    var existingDetail = db.return_details.SingleOrDefault(d =>
                        d.return_id == returnId &&
                        d.flower_id == cbo_maHoa.SelectedValue.ToString());

                    if (existingDetail != null)
                    {
                        db.return_details.DeleteOnSubmit(existingDetail);
                        db.SubmitChanges();
                        MessageBox.Show("Xóa chi tiết đơn đổi trả thành công!");
                        LoadChiTietDoiTra();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy chi tiết đổi trả để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public event EventHandler BackButtonClicked;
        private void btn_quayLai_Click(object sender, EventArgs e)
        {
            BackButtonClicked?.Invoke(this, EventArgs.Empty);
        }


    }
}
