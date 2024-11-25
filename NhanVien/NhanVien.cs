using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowerShop.NhanVien
{
    public partial class NhanVien : UserControl
    {
        public NhanVien()
        {
            InitializeComponent();
        }
        private void NhanVien_Load(object sender, EventArgs e)
        {
            quanLyNhanVien1.frmQuanLyNhanVien_Load(sender,e);
            quanLyNhanVien1.BringToFront();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            quanLyNhanVien1.frmQuanLyNhanVien_Load(sender, e);
            quanLyNhanVien1.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            quanLyTaiKhoanNV1.frmQuanLyTaiKhoan_Load(sender, e);
            quanLyTaiKhoanNV1.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            quanLyQuyen1.QuanLyQuyen_Load(sender, e);
            quanLyQuyen1.BringToFront();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            phanQuyen1.frmPhanQuyen_Load(sender,e);
            phanQuyen1.BringToFront();
        }
    }
}
