using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace FlowerShop.NhaCungCap
{
    public partial class QuanLyNhapHang : UserControl
    {
        db_flowerDataContext fs = new db_flowerDataContext();
        public QuanLyNhapHang()
        {
            InitializeComponent();
        }
        void loadNcc()
        {
            var tblsupply = from sl in fs.suppliers
                            select sl;
            dgrvChitietdondat.DataSource = tblsupply;
            dgrvChitietdondat.AllowUserToAddRows = false;
        }

        void load_cboMaNhaHoa()
        {
            var tblflower = from fl in fs.flowers
                            select new { fl.name, fl.flower_id };
            cboMahoa.DataSource = tblflower.ToList();
            cboMahoa.ValueMember = "flower_id";
            cboMahoa.DisplayMember = "name";
        }


        void xoa_Dulieudondat()
        {
            //txtMaNcc.Clear();
            dtpNgaycungcap.Value = DateTime.Now;
            txtSoluong.Clear();
            txtGia.Clear();
        }

        void loadDondat()
        {
            if (!string.IsNullOrEmpty(txtMaNcc.Text))
            {
                var tblsupply = from fl_sl in fs.flower_supplies
                                where fl_sl.supplier_id == txtMaNcc.Text
                                select fl_sl;
                dgrvChitietdondat.DataSource = tblsupply.ToList();
                dgrvChitietdondat.Columns["flower"].Visible = false;
                dgrvChitietdondat.Columns["supplier"].Visible = false;
            }
            else
            {
                dgrvChitietdondat.DataSource = null;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtMaNcc.Text == string.Empty || txtSoluong.Text == string.Empty || txtGia.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin !!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (dtpNgaycungcap.Value.Date > DateTime.Now)
            {
                MessageBox.Show("Ngày cung cấp không được trước ngày bắt đầu. Tự động đặt lại ngày cung cấp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgaycungcap.Value = DateTime.Now;
            }
            else
            {
                string flowerId = cboMahoa.SelectedValue.ToString();
                string supplierId = txtMaNcc.Text;
                var existingFlower = fs.flower_supplies
                    .SingleOrDefault(c => c.flower_id == flowerId && c.supplier_id == supplierId);

                if (existingFlower != null)
                {
                    MessageBox.Show($"Hoa '{flowerId}' với nhà cung cấp '{supplierId}' đã tồn tại. Vui lòng chọn hoa khác ",
                                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thêm nhà cung cấp cho loại hoa này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        flower_supply fl_sl = new flower_supply();
                        fl_sl.supplier_id = txtMaNcc.Text;
                        fl_sl.flower_id = cboMahoa.SelectedValue.ToString();
                        fl_sl.supply_date = dtpNgaycungcap.Value;
                        fl_sl.quantity = int.Parse(txtSoluong.Text);
                        fl_sl.price = decimal.Parse(txtGia.Text);
                        fs.flower_supplies.InsertOnSubmit(fl_sl);
                        fs.SubmitChanges();
                        loadDondat();
                        MessageBox.Show("Thêm thành công");
                        xoa_Dulieudondat();
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
                        string flowerId = cboMahoa.SelectedValue.ToString();
                        string maNcc = txtMaNcc.Text;
                        flower_supply fl_sl = fs.flower_supplies.Where(t => t.supplier_id == maNcc).FirstOrDefault();
                        fs.flower_supplies.DeleteOnSubmit(fl_sl);
                        fs.SubmitChanges();
                        loadDondat();
                        MessageBox.Show("Xoá thành công");
                        xoa_Dulieudondat();
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
                MessageBox.Show("Vui lòng chọn 1 nhà cung cấp loại hoa đang có", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn cập nhật nhà cung cấp này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                try
                {
                    string supplierId = txtMaNcc.Text;
                    string newFlowerId = cboMahoa.SelectedValue.ToString();
                    flower_supply existingSupply = fs.flower_supplies
                        .Where(t => t.supplier_id == supplierId && t.flower_id == cboMahoa.SelectedValue.ToString())
                        .FirstOrDefault();

                    if (existingSupply != null)
                    {
                        fs.flower_supplies.DeleteOnSubmit(existingSupply);
                        flower_supply newSupply = new flower_supply
                        {
                            supplier_id = supplierId,
                            flower_id = newFlowerId,
                            supply_date = dtpNgaycungcap.Value,
                            quantity = int.Parse(txtSoluong.Text),
                            price = decimal.Parse(txtGia.Text)
                        };

                        fs.flower_supplies.InsertOnSubmit(newSupply);
                        fs.SubmitChanges();

                        loadDondat();
                        MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        xoa_Dulieudondat();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy bản ghi để cập nhật", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {

        }
    }
}
