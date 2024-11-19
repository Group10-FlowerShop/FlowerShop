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
    public partial class ChiTietDonHang : UserControl
    {
        db_flowerDataContext fs = new db_flowerDataContext();
        private string orderId;
        public ChiTietDonHang(string orderId)
        {
            InitializeComponent();
            this.orderId = orderId;
            LoadOrderDetails();
        }
        public void AutoResizeDataGridView(DataGridView dgv)
        {
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                column.MinimumWidth = 50;
            }
        }
        void xoa_Dulieu()
        {
            txtSoluong.Clear();
            cboMahoa.SelectedValue = -1;
        }

        private void LoadOrderDetails()
        {
            var orderDetails = fs.order_details
                                 .Where(od => od.order_id == orderId)
                                 .Select(od => new
                                 {
                                     od.flower_id,
                                     od.quantity,
                                     od.price,
                                     TotalPrice = od.quantity * od.price
                                 }).ToList();

            dgrvChitiethoadon.DataSource = orderDetails;
            AutoResizeDataGridView(dgrvChitiethoadon);
        }

        void load_Cthd()
        {
            var tblorderdetail = from od in fs.order_details
                                 select od;
            dgrvChitiethoadon.DataSource = tblorderdetail;
            dgrvChitiethoadon.AllowUserToAddRows = false;
        }

        void load_cboMahoa()
        {
            var tblflower = from fl in fs.flowers
                            select new { fl.flower_id, fl.name };
            cboMahoa.DataSource = tblflower.ToList();
            cboMahoa.DisplayMember = "name";
            cboMahoa.ValueMember = "flower_id";
        }

        /*void kiemTon()
        {
            int soLuongNhap = 0;
            string productId = cboMahoa.SelectedValue.ToString();
            if (int.TryParse(txtSoluong.Text, out soLuongNhap))
            {
                var product = fs.flowers.SingleOrDefault(t => t.flower_id == productId);
                if (product != null)
                {
                    int stock = product.stock;
                    if (soLuongNhap > stock)
                    {
                        MessageBox.Show("Số lượng nhập vào không thể lớn hơn số lượng tồn kho. " +
                            "Số lượng hiện tại trong kho :" + stock);
                    }
                }
            }
        }*/
        private void frmQuanLyCTDH_Load(object sender, EventArgs e)
        {
            //load_Cthd();
            txtMahoadon.Text = orderId;
            load_cboMahoa();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMahoadon.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng chọn 1 đơn hàng để xoá", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xoá đơn hàng này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    string maDon = txtMahoadon.Text;
                    order_detail od_dt = fs.order_details.Where(t => t.order_id == maDon).FirstOrDefault();
                    fs.order_details.DeleteOnSubmit(od_dt);
                    fs.SubmitChanges();
                    load_Cthd();
                    MessageBox.Show("Xoá thành công");
                    xoa_Dulieu();
                }
                else
                {
                    MessageBox.Show("Xoá thất bại");
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMahoadon.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng chọn 1 đơn hàng để cập nhật", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn cập nhật đơn hàng này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    string maDon = txtMahoadon.Text;
                    order_detail od_dt = fs.order_details.Where(t => t.order_id == maDon).FirstOrDefault();
                    od_dt.quantity = int.Parse(txtSoluong.Text);
                    fs.SubmitChanges();
                    load_Cthd();
                    MessageBox.Show("Cập nhật thành công");
                    xoa_Dulieu();
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            xoa_Dulieu();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtMahoadon.Text == string.Empty || txtSoluong.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin !!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thêm sản phẩm này vào đơn hàng không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    //kiemTon();
                    order_detail od_dt = new order_detail();
                    od_dt.order_id = txtMahoadon.Text;
                    od_dt.flower_id = cboMahoa.SelectedValue.ToString();
                    od_dt.quantity = int.Parse(txtSoluong.Text);
                    od_dt.price = decimal.Parse(txtGia.Text);
                    fs.order_details.InsertOnSubmit(od_dt);
                    fs.SubmitChanges();
                    LoadOrderDetails();
                    MessageBox.Show("Thêm thành công");
                    xoa_Dulieu();
                }
            }
        }


        private void cboMahoa_SelectedValueChanged(object sender, EventArgs e)
        {
            string selectedFlowerId = cboMahoa.SelectedValue?.ToString();

            if (!string.IsNullOrEmpty(selectedFlowerId))
            {
                var price = (from fl in fs.flowers
                             where fl.flower_id == selectedFlowerId
                             select fl.price).FirstOrDefault();
                txtGia.Text = price.ToString("0.00");
            }
        }

        private void frmQuanLyCTDH_FormClosing(object sender, FormClosingEventArgs e)
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

        private void dgrvChitiethoadon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dgrvChitiethoadon.CurrentRow.Index;
            cboMahoa.SelectedValue = dgrvChitiethoadon.Rows[index].Cells[0].Value.ToString();
            txtSoluong.Text = dgrvChitiethoadon.Rows[index].Cells[1].Value.ToString();
        }

        private void dgrvChitiethoadon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dgrvChitiethoadon.CurrentRow.Index;
            cboMahoa.SelectedValue = dgrvChitiethoadon.Rows[index].Cells[0].Value.ToString();
            txtSoluong.Text = dgrvChitiethoadon.Rows[index].Cells[1].Value.ToString();
        }

        private void txtSoluong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }

        }

        private void txtGia_KeyPress(object sender, KeyPressEventArgs e)
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
        public event EventHandler BackButtonClicked;
        private void button1_Click(object sender, EventArgs e)
        {
            BackButtonClicked?.Invoke(this, EventArgs.Empty);
        }

    }
}
