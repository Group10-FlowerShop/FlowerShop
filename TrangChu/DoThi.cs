using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms.DataVisualization.Charting;

namespace FlowerShop.TrangChu
{
    public partial class DoThi : UserControl
    {
        public DoThi(int year)
        {
            InitializeComponent();
            LoadSalesChart(year);
        }

        private void LoadSalesChart(int year)
        {
            // Chuẩn bị dữ liệu từ database
            using (var context = new db_flowerDataContext())
            {
                var salesData = from od in context.order_details
                                join o in context.orders on od.order_id equals o.order_id
                                join f in context.flowers on od.flower_id equals f.flower_id
                                where o.order_date.Year == year
                                group od by new { Month = o.order_date.Month, FlowerName = f.name } into monthlyFlowerGroup

                                select new
                                {
                                    Month = monthlyFlowerGroup.Key.Month,
                                    FlowerName = monthlyFlowerGroup.Key.FlowerName,
                                    TotalQuantity = monthlyFlowerGroup.Sum(od => od.quantity)
                                };

                // Xóa các series và legend cũ
                chartSales.Series.Clear();
                chartSales.Legends.Clear();

                if (!salesData.Any())
                {
                    // Nếu không có dữ liệu, hiển thị thông báo trên biểu đồ
                    Series emptySeries = new Series("Chưa có dữ liệu");
                    emptySeries.ChartType = SeriesChartType.Point; // Loại series chỉ để hiển thị thông báo
                    emptySeries.Points.AddXY(0, 0); // Điểm trống để hiển thị thông báo
                    emptySeries.IsVisibleInLegend = false; // Không hiển thị trong chú thích
                    chartSales.Series.Add(emptySeries);

                    chartSales.Titles.Clear();
                    chartSales.Titles.Add($"Chưa có dữ liệu cho năm {year}");
                    return;
                }

                chartSales.Titles.Clear();

                // Thêm tiêu đề chính
                Title mainTitle = new Title("Biểu đồ doanh thu theo tháng trong năm " + year, Docking.Top, new Font("Arial", 14, FontStyle.Bold), Color.Black);
                chartSales.Titles.Add(mainTitle);

                // Thêm ghi chú
                Title subTitle = new Title("Ghi chú: Dữ liệu được tính theo đơn hàng trong năm được chọn.", Docking.Bottom, new Font("Arial", 10, FontStyle.Italic), Color.Gray);
                chartSales.Titles.Add(subTitle);


                // Tạo một legend cho các loại hoa
                Legend legend = new Legend("FlowerLegend");
                legend.Title = "Loại Hoa";
                chartSales.Legends.Add(legend);

                // Tạo Series cho từng loại hoa
                Random random = new Random(); // Khởi tạo Random
                foreach (var flowerGroup in salesData.GroupBy(x => x.FlowerName))
                {
                    Series series = new Series(flowerGroup.Key);
                    series.ChartType = SeriesChartType.Column;

                    // Gán màu sắc ngẫu nhiên
                    series.Color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                    series.Legend = "FlowerLegend";

                    foreach (var data in flowerGroup)
                    {
                        // Thêm dữ liệu vào series (tháng, tổng số lượng)
                        int pointIndex = series.Points.AddXY(data.Month, data.TotalQuantity);
                        series.Points[pointIndex].ToolTip = $"Tháng: {data.Month}, Số lượng: {data.TotalQuantity}, Loại hoa: {data.FlowerName}";
                    }

                    chartSales.Series.Add(series);
                }

                // Cấu hình trục
                chartSales.ChartAreas[0].AxisX.Title = "Tháng";
                chartSales.ChartAreas[0].AxisY.Title = "Số lượng hoa bán";
                chartSales.ChartAreas[0].AxisX.Interval = 1;
                chartSales.ChartAreas[0].AxisX.LabelStyle.Angle = -45; // Xoay nhãn trục X
            }

            // Gắn sự kiện MouseMove
            chartSales.MouseMove += chartSales_MouseMove_1;
        }


        //private void cboChartType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    // Thay đổi loại biểu đồ
        //    ComboBox cboChartType = sender as ComboBox;
        //    SeriesChartType selectedChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), cboChartType.SelectedItem.ToString());

        //    foreach (var series in chartSales.Series)
        //    {
        //        series.ChartType = selectedChartType;
        //    }
        //}

        private void chartSales_MouseMove_1(object sender, MouseEventArgs e)
        {
            // Xử lý sự kiện khi rê chuột vào biểu đồ
            HitTestResult result = chartSales.HitTest(e.X, e.Y);

            if (result.ChartElementType == ChartElementType.DataPoint)
            {
                // Lấy thông tin từ Series và PointIndex
                DataPoint point = result.Series.Points[result.PointIndex];

                // Hiển thị thông tin ToolTip khi rê chuột vào cột
                string tooltipText = point.ToolTip;

                // Gắn thông tin ToolTip vào biểu đồ
                toolTip1.SetToolTip(chartSales, tooltipText); // toolTip1 là một control ToolTip đã thêm vào form
            }
        }
    }
}
