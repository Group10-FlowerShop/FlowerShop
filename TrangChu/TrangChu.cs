using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowerShop.TrangChu
{
    public partial class TrangChu : UserControl
    {
        private ThongKe thongke;
        private DoThi dothi;
        public TrangChu()
        {
            InitializeComponent();
            thongke = new ThongKe();
            thongke.Dock = DockStyle.Fill;
            panel2.Controls.Add(thongke);
            thongke.onShowChart += ShowChart;
        }
        public void ShowChart(int year)
        {
            panel1.Controls.Clear();
            dothi = new DoThi(year);
            dothi.Dock = DockStyle.Fill;
            panel1.Controls.Add(dothi);
        }
        private void TrangChu_Load(object sender, EventArgs e)
        {

        }
    }
}
