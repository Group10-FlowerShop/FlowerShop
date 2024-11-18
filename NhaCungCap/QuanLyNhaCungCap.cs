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
    public partial class QuanLyNhaCungCap : UserControl
    {
        db_flowerDataContext fs = new db_flowerDataContext();
        public QuanLyNhaCungCap()
        {
            InitializeComponent();
        }

        void loadNcc()
        {
            var tblsupply = from sl in fs.suppliers
                            select sl;
            dgrvNhacungcap.DataSource = tblsupply;
            dgrvNhacungcap.AllowUserToAddRows = false;
        }

        private void dgrvNhacungcap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dgrvNhacungcap.CurrentRow.Index;
            txtMaNcc.Text = dgrvNhacungcap.Rows[index].Cells[0].Value.ToString();
            txtTenncc.Text = dgrvNhacungcap.Rows[index].Cells[1].Value.ToString();
            txtNguoidaidien.Text = dgrvNhacungcap.Rows[index].Cells[2].Value.ToString();
            txtDiachi.Text = dgrvNhacungcap.Rows[index].Cells[3].Value.ToString();
        }

        void xoa_Dulieu()
        {
            txtMaNcc.Clear();
            txtTenncc.Clear();
            txtNguoidaidien.Clear();
            txtDiachi.Clear();
        }

        private void frmQuanLyNhaCungCap_Load(object sender, EventArgs e)
        {
            loadNcc();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtTenncc.Text == string.Empty || txtNguoidaidien.Text == string.Empty || txtDiachi.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                using (var context = new db_flowerDataContext())
                {
                    var maNccCuoi = context.suppliers.OrderByDescending(t => t.supplier_id).FirstOrDefault();
                    string maNccMoi;

                    string latestID = maNccCuoi.supplier_id;
                    int ma = int.Parse(latestID.Substring(3));
                    maNccMoi = "SUP" + (ma + 1).ToString("D3");
                    DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thêm nhà cung cấp này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            supplier sl = new supplier();
                            sl.supplier_id = maNccMoi;
                            sl.name = txtTenncc.Text;
                            sl.contact = txtNguoidaidien.Text;
                            sl.address = txtDiachi.Text;
                            fs.suppliers.InsertOnSubmit(sl);
                            fs.SubmitChanges();
                            loadNcc();
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
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaNcc.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng chọn 1 nhà cung cấp để xoá", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xoá nhà cung cấp này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        string maNcc = txtMaNcc.Text;
                        supplier sl = fs.suppliers.Where(t => t.supplier_id == maNcc).FirstOrDefault();
                        fs.suppliers.DeleteOnSubmit(sl);
                        fs.SubmitChanges();
                        loadNcc();
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
            if (txtMaNcc.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng chọn 1 nhà cung cấp", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn cập nhật khách hàng này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        string maNcc = txtMaNcc.Text;
                        supplier sl = fs.suppliers.Where(t => t.supplier_id == maNcc).FirstOrDefault();
                        sl.name = txtTenncc.Text;
                        sl.contact = txtNguoidaidien.Text;
                        sl.address = txtDiachi.Text;
                        fs.SubmitChanges();
                        loadNcc();
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

        private void btnChitiet_Click(object sender, EventArgs e)
        {
            //if (dgrvNhacungcap.CurrentRow != null || txtMaNcc.Text != string.Empty)
            //{
            //    string suppId = txtMaNcc.Text;
            //    frmQuanLyDonDat frmQLDD = new frmQuanLyDonDat(suppId);
            //    frmQLDD.Owner = this;
            //    frmQLDD.ShowDialog();
            //}
            //else
            //{
            //    MessageBox.Show("Vui lòng chọn một nhà cung cấp trước khi xem chi tiết.");
            //}
        }
    }
}
