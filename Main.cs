using FlowerShop.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowerShop
{
    public partial class Main : Form
    {
        public string id;
        public string name;
        public List<string> roles;
        public Main(TTNhanVien nhanvien)
        {
            InitializeComponent();
            this.id = nhanvien.Id;
            this.name = nhanvien.Name;
            this.roles = nhanvien.Roles;
        }
        private void Main_Load(object sender, EventArgs e)
        {
            trangChu1.BringToFront();
            txt_name.Text = this.name;
            txt_role.Text = roles[0];
            hideB4Check();
            kiemTraQuyen();
        }
        private void hideB4Check()
        {
            trangChu1.Hide();
            nhanVien1.Hide();
            khachHang1.Hide();
            donHang1.Hide();
            sanPham1.Hide();
            nhaCungCap1.Hide();
            doiTra1.Hide();
            khuyenMai1.Hide();

            btn_doitra.Hide();
            btn_hoadon.Hide();
            btn_khachhang.Hide();
            btn_khuyenmai.Hide();
            btn_nhacungcap.Hide();
            btn_nhanvien.Hide();
            btn_sanpham.Hide();
            btn_trangchu.Hide();
        }
        //Toàn quyền quản trị hệ thống
        //Quản lý cửa hàng
        //Nhân viên bán hàng
        //Quản lý kho
        //Kế toán
        //Chăm sóc khách hàng
        //Quản lý marketing
        //Vận chuyển
        //Người cắm hoa
        //Nhân sự
        private void kiemTraQuyen()
        {
            foreach(var role in roles)
            {
                if(role == "HR")
                {
                    nhanVien1.Show();
                    btn_nhanvien.Show();
                }
                if(role == "INVENTORY")
                {
                    nhaCungCap1.Show();
                    btn_nhacungcap.Show();

                    sanPham1.Show();
                    btn_sanpham.Show();
                }
                if(role == "SALES")
                {
                    donHang1.Show();
                    btn_hoadon.Show();
                }
                if(role == "ACCOUNTANT")
                {
                    trangChu1.Show();
                    btn_trangchu.Show();

                    donHang1.Show();
                    btn_hoadon.Show();
                }
                if(role == "CUSTOMER_SERVICE")
                {
                    khachHang1.Show();
                    btn_khachhang.Show();

                    doiTra1.Show();
                    btn_doitra.Show();
                }
                if(role == "MANAGER" || role == "ADMIN")
                {
                    trangChu1.Show();
                    nhanVien1.Show();
                    khachHang1.Show();
                    donHang1.Show();
                    sanPham1.Show();
                    nhaCungCap1.Show();
                    doiTra1.Show();
                    khuyenMai1.Show();

                    btn_doitra.Show();
                    btn_hoadon.Show();
                    btn_khachhang.Show();
                    btn_khuyenmai.Show();
                    btn_nhacungcap.Show();
                    btn_nhanvien.Show();
                    btn_sanpham.Show();
                    btn_trangchu.Show();
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            clock.Text = DateTime.Now.ToString("HH:mm:ss - dd/MM/yyyy");
        }
        private void btn_trangchu_MouseClick(object sender, MouseEventArgs e)
        {
            trangChu1.BringToFront();
        }

        private void btn_nhanvien_MouseClick(object sender, MouseEventArgs e)
        {
            nhanVien1.BringToFront();
        }
        private void btn_khachhang_MouseClick(object sender, MouseEventArgs e)
        {
            khachHang1.BringToFront();
        }

        private void btn_hoadon_MouseClick(object sender, MouseEventArgs e)
        {
            donHang1.BringToFront();
        }

        private void btn_sanpham_MouseClick(object sender, MouseEventArgs e)
        {
            sanPham1.BringToFront();
        }

        private void btn_nhacungcap_MouseClick(object sender, MouseEventArgs e)
        {
            nhaCungCap1.BringToFront();
        }

        private void btn_logout_MouseClick(object sender, MouseEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất?",
                "Thông báo",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question
            );

            if (dialogResult == DialogResult.OK)
            {
                this.Close();
            }
        }
        private void btn_doitra_MouseClick(object sender, MouseEventArgs e)
        {
            doiTra1.BringToFront();
        }
        private void btn_khuyenmai_Click(object sender, EventArgs e)
        {
            khuyenMai1.BringToFront();
        }
        private void btn_trangchu_MouseEnter(object sender, EventArgs e)
        {
            btn_trangchu.BackColor = Color.Gray;
        }

        private void btn_trangchu_MouseLeave(object sender, EventArgs e)
        {
            btn_trangchu.BackColor = Color.FromArgb(26, 37, 39);
        }

        private void btn_nhanvien_MouseEnter(object sender, EventArgs e)
        {
            btn_nhanvien.BackColor = Color.Gray;
        }

        private void btn_nhanvien_MouseLeave(object sender, EventArgs e)
        {
            btn_nhanvien.BackColor = Color.FromArgb(26, 37, 39);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(
                "Bạn có chắc chắn muốn thoát?",
                "Thông báo",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question
            );

            if (dialogResult == DialogResult.OK)
            {
                Application.Exit();
            }
        }
        private void btn_khachhang_MouseEnter(object sender, EventArgs e)
        {
            btn_khachhang.BackColor = Color.Gray;
        }

        private void btn_khachhang_MouseLeave(object sender, EventArgs e)
        {
            btn_khachhang.BackColor = Color.FromArgb(26, 37, 39);
        }

        private void btn_hoadon_MouseEnter(object sender, EventArgs e)
        {
            btn_hoadon.BackColor = Color.Gray;
        }

        private void btn_hoadon_MouseLeave(object sender, EventArgs e)
        {
            btn_hoadon.BackColor = Color.FromArgb(26, 37, 39);
        }

        private void btn_sanpham_MouseEnter(object sender, EventArgs e)
        {
            btn_sanpham.BackColor = Color.Gray;
        }

        private void btn_sanpham_MouseLeave(object sender, EventArgs e)
        {
            btn_sanpham.BackColor = Color.FromArgb(26, 37, 39);
        }

        private void btn_nhacungcap_MouseEnter(object sender, EventArgs e)
        {
            btn_nhacungcap.BackColor = Color.Gray;
        }

        private void btn_nhacungcap_MouseLeave(object sender, EventArgs e)
        {
            btn_nhacungcap.BackColor = Color.FromArgb(26, 37, 39);
        }

        private void btn_doitra_MouseEnter(object sender, EventArgs e)
        {
            btn_doitra.BackColor = Color.Gray;
        }

        private void btn_doitra_MouseLeave(object sender, EventArgs e)
        {
            btn_doitra.BackColor = Color.FromArgb(26, 37, 39);
        }
        private void btn_logout_MouseEnter(object sender, EventArgs e)
        {
            btn_logout.BackColor = Color.Gray;
        }

        private void btn_logout_MouseLeave(object sender, EventArgs e)
        {
            btn_logout.BackColor = Color.FromArgb(26, 37, 39);
        }

        private void btn_khuyenmai_MouseEnter(object sender, EventArgs e)
        {
            btn_khuyenmai.BackColor = Color.Gray;
        }

        private void btn_khuyenmai_MouseLeave(object sender, EventArgs e)
        {
            btn_khuyenmai.BackColor = Color.FromArgb(26, 37, 39);
        }
    }
}
