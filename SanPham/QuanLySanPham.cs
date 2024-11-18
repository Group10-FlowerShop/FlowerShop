using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowerShop.SanPham
{
    public partial class QuanLySanPham : UserControl
    {
        db_flowerDataContext fs = new db_flowerDataContext();
        public QuanLySanPham()
        {
            InitializeComponent();
        }

        void load_Sanpham()
        {
            var tblproduct = from sp in fs.flowers
                             select sp;
            dgrvSanpham.DataSource = tblproduct;
            dgrvSanpham.Columns["inventory"].Visible = false;
            dgrvSanpham.AllowUserToAddRows = false;
        }

        void xoa_Dulieu()
        {
            txtMahoa.Clear();
            txtTensp.Clear();
            txtMota.Clear();
            txtGia.Clear();
            cboTrangthai.SelectedValue = -1;
        }

        void load_cboLoaihoa()
        {

        }

        void load_cboTrangthai()
        {
            cboTrangthai.Items.Add("active");
            cboTrangthai.Items.Add("inactive");
        }

        private void frmQuanLyNhanVien_Load(object sender, EventArgs e)
        {
            load_Sanpham();
            load_cboTrangthai();
            load_cboLoaihoa();
        }

        private void btnTaoma_Click(object sender, EventArgs e)
        {
            xoa_Dulieu();
            using (var context = new db_flowerDataContext())
            {
                var maHoaCuoi = context.flowers.OrderByDescending(a => a.flower_id).FirstOrDefault();
                string maHoaMoi;

                if (maHoaCuoi != null)
                {
                    string latestID = maHoaCuoi.flower_id;
                    int ma = int.Parse(latestID.Substring(3));

                    maHoaMoi = "FLW" + (ma + 1).ToString("D3");
                }
                else
                {
                    maHoaMoi = "FLW001";
                }
                txtMahoa.Text = maHoaMoi;
            }

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

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtMahoa.Text == string.Empty || txtTensp.Text == string.Empty || txtGia.Text == string.Empty || cboTrangthai.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin !!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string flowerId = txtMahoa.Text;
                var existingFlower = fs.flowers.SingleOrDefault(c => c.flower_id == flowerId);
                if (existingFlower != null)
                {
                    MessageBox.Show($"Mã hoa '{flowerId}' đã tồn tại. Vui lòng nhấn nút tạo mã để tạo mới.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thêm sản phẩm này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        flower fl = new flower();
                        fl.flower_id = txtMahoa.Text;
                        fl.name = txtTensp.Text;
                        fl.description = txtMota.Text;
                        fl.price = decimal.Parse(txtGia.Text);
                        //fl.stock = int.Parse(txtSoluongton.Text);
                        fl.status = cboTrangthai.SelectedItem.ToString();
                        fs.flowers.InsertOnSubmit(fl);
                        fs.SubmitChanges();
                        load_Sanpham();
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

        private void btnHuy_Click(object sender, EventArgs e)
        {
            xoa_Dulieu();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMahoa.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng chọn 1 sản phẩm để xoá", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xoá sản phảm này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        string maHoa = txtMahoa.Text;
                        flower fl = fs.flowers.Where(t => t.flower_id == maHoa).FirstOrDefault();
                        fs.flowers.DeleteOnSubmit(fl);
                        fs.SubmitChanges();
                        load_Sanpham();
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
            if (txtMahoa.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng chọn 1 sản phẩm để cập nhật", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn cập nhật sản phảm này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        string maHoa = txtMahoa.Text;
                        flower fl = fs.flowers.Where(t => t.flower_id == maHoa).FirstOrDefault();
                        fl.name = txtTensp.Text;
                        fl.description = txtMota.Text;
                        fl.price = decimal.Parse(txtGia.Text);
                        fl.status = cboTrangthai.SelectedItem.ToString();
                        fs.SubmitChanges();
                        load_Sanpham();
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

        //private void btnXemanh_Click(object sender, EventArgs e)
        //{
        //    if (dgrvSanpham.CurrentRow != null)
        //    {
        //        string maHoa = txtMahoa.Text;
        //        frmQuanLyAnhHoa frmAnhHoa = new frmQuanLyAnhHoa(maHoa);
        //        frmAnhHoa.Owner = this;
        //        frmAnhHoa.ShowDialog();
        //    }
        //    else
        //    {
        //        MessageBox.Show("Vui lòng chọn một đơn hàng trước khi xem chi tiết.");
        //    }
        //}

        private void dgrvSanpham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                int index = dgrvSanpham.CurrentRow.Index;
                txtMahoa.Text = dgrvSanpham.Rows[index].Cells[0].Value.ToString();
                txtTensp.Text = dgrvSanpham.Rows[index].Cells[1].Value.ToString();
                txtMota.Text = dgrvSanpham.Rows[index].Cells[2].Value.ToString();
                txtGia.Text = dgrvSanpham.Rows[index].Cells[3].Value.ToString();
                cboTrangthai.SelectedItem = dgrvSanpham.Rows[index].Cells[4].Value.ToString();
            }
        }

        private void txtMahoa_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgrvSanpham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                int index = dgrvSanpham.CurrentRow.Index;
                txtMahoa.Text = dgrvSanpham.Rows[index].Cells[0].Value.ToString();
                txtTensp.Text = dgrvSanpham.Rows[index].Cells[1].Value.ToString();
                txtMota.Text = dgrvSanpham.Rows[index].Cells[2].Value.ToString();
                txtGia.Text = dgrvSanpham.Rows[index].Cells[3].Value.ToString();
                cboTrangthai.SelectedItem = dgrvSanpham.Rows[index].Cells[4].Value.ToString();
            }
        }

        //private void btnXemdanhmuc_Click(object sender, EventArgs e)
        //{
        //    if (dgrvSanpham.CurrentRow != null || txtMahoa.Text != string.Empty)
        //    {
        //        string flowerId = txtMahoa.Text;
        //        frmQuanLyDanhMucSanPham frmQLDM = new frmQuanLyDanhMucSanPham(flowerId);
        //        frmQLDM.Owner = this;
        //        frmQLDM.ShowDialog();
        //    }
        //    else
        //    {
        //        MessageBox.Show("Vui lòng chọn một loại trước khi xem danh mục.");
        //    }
        //}
    }
}
