using FlowerShop.DonHang;
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
    public partial class NhaCungCap : UserControl
    {
        public NhaCungCap()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            quanLyNhaCungCap1.BringToFront();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            quanLyNhapHang1.BringToFront();
        }

        private void NhaCungCap_Load(object sender, EventArgs e)
        {
            quanLyNhaCungCap1.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            quanLyKhoHang1.BringToFront();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
