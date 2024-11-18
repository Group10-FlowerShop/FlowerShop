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
        public string MaNV;
        public Main()
        {
            InitializeComponent();
        }
        private void Main_Load(object sender, EventArgs e)
        {
            trangChu1.BringToFront();
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
            this.MaNV = "";

            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();

            this.Close();
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
            this.Dispose();
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
