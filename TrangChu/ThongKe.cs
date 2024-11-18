using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowerShop.TrangChu
{
    public partial class ThongKe : UserControl
    {
        private static bool isModelTrained = false;
        public class RevenueData
        {
            [LoadColumn(0)] public float DayIndex { get; set; }  // Số ngày từ mốc đầu tiên
            [LoadColumn(1)] public float Revenue { get; set; }   // Doanh thu
        }
        public class RevenuePrediction
        {
            [ColumnName("Score")] public float PredictedRevenue { get; set; }
        }
        public ThongKe()
        {
            InitializeComponent();
        }

        private void ThongKe_Load(object sender, EventArgs e)
        {
            dgvOrdersByStatus.ContextMenuStrip = contextMenuStrip1;
        }
        public void AutoResizeDataGridView(DataGridView dgv)
        {
            // Set the AutoSizeColumnsMode to Fill to have columns adjust to fill the entire width
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Additionally, you can specify minimum widths or other settings for individual columns if needed
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                column.MinimumWidth = 50; // Set a minimum width as an example, adjust as needed
            }
        }
        private void LoadStatistics()
        {
            DateTime startDate = dtpStartDate.Value;
            DateTime endDate = dtpEndDate.Value;

            if (startDate > endDate)
            {
                // endDate nhỏ hơn startDate, hiển thị thông báo lỗi
                MessageBox.Show("Ngày kết thúc phải lớn hơn hoặc bằng ngày bắt đầu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Giả sử bạn có một context kết nối với cơ sở dữ liệu
            var context = new db_flowerDataContext();
            // 1. Tổng số đơn hàng trong khoảng thời gian
            var totalOrders = context.orders
                .Where(o => o.order_date >= startDate && o.order_date <= endDate)
                .Count();
            lblDatetime.Text = $"{startDate.ToShortDateString()} - {endDate.ToShortDateString()}";
            lblTotalOrders.Text = $"Tổng đơn hàng: {totalOrders}";

            // 2. Tổng doanh thu trong khoảng thời gian
            var totalRevenue = context.orders
   .Where(o => o.order_date >= startDate && o.order_date <= endDate)
   .Sum(o => (decimal?)o.total_amount) ?? 0; // Nếu null, sẽ trả về 0

            // Thực hiện xử lý với totalRevenue
            if (totalRevenue == 0)
            {

                lblTotalRevenue.Text = $"Tổng doanh thu: {totalRevenue:N0}VNĐ";
            }
            else
            {
                lblTotalRevenue.Text = $"Tổng doanh thu: {totalRevenue:N0}VNĐ";
            }

            if (radTongHoaDon.Checked == true)
            {
                // 3. Số đơn hàng theo trạng thái trong khoảng thời gian
                var ordersByStatus = context.orders
                    .Where(o => o.order_date >= startDate && o.order_date <= endDate)
                    .GroupBy(o => o.status)
                    .Select(g => new
                    {
                        Status = g.Key,
                        Count = g.Count()
                    })
                    .ToList();

                // Hiển thị kết quả vào DataGridView
                dgvOrdersByStatus.DataSource = ordersByStatus;
            }
            else if (radTheoTrangThai.Checked == true)
            {
                // 4. Số đơn hàng và tổng doanh thu theo phương thức thanh toán trong khoảng thời gian
                var ordersByPaymentMethod = context.orders
                    .Where(o => o.order_date >= startDate && o.order_date <= endDate)
                    .GroupBy(o => o.payment_method)
                    .Select(g => new
                    {
                        PaymentMethod = g.Key,
                        Count = g.Count(),
                        TotalAmount = g.Sum(o => o.total_amount)  // Tính tổng trước khi định dạng
                    })
                    .ToList()
                    .Select(g => new
                    {
                        g.PaymentMethod,
                        g.Count,
                        TotalAmount = string.Format("{0:N0}", g.TotalAmount)  // Định dạng TotalAmount
                    })
                    .ToList();

                // Hiển thị kết quả vào DataGridView
                dgvOrdersByStatus.DataSource = ordersByPaymentMethod;

            }
            else
            {

                // Lấy danh sách hóa đơn theo khoảng thời gian và định dạng TotalAmount
                var orders = from o in context.orders
                             where o.order_date >= startDate && o.order_date <= endDate
                             select new
                             {
                                 OrderID = o.order_id,
                                 CustomerID = o.customer_id,
                                 OrderDate = o.order_date,
                                 TotalAmount = o.total_amount,  // Chỉ lấy giá trị này để format sau
                                 Status = o.status,
                                 PaymentMethod = o.payment_method
                             };

                var formattedOrders = orders
                    .ToList()
                    .Select(o => new
                    {
                        o.OrderID,
                        o.CustomerID,
                        OrderDate = o.OrderDate.ToString("dd/MM/yyyy"),  // Định dạng ngày nếu cần
                        TotalAmount = string.Format("{0:N0}", o.TotalAmount),  // Định dạng TotalAmount
                        o.Status,
                        o.PaymentMethod
                    }).ToList();

                // Hiển thị dữ liệu đã định dạng trong DataGridView
                dgvOrdersByStatus.DataSource = formattedOrders;
            }
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            LoadStatistics();
            AutoResizeDataGridView(dgvOrdersByStatus);
        }

        private void dgvOrdersByStatus_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvOrdersByStatus.ClearSelection();
                dgvOrdersByStatus.Rows[e.RowIndex].Selected = true;
            }
        }
        private void btnDoThi_Click(object sender, EventArgs e)
        {

        }
        private void btnTrainig_Click(object sender, EventArgs e)
        {
            // Dữ liệu mẫu (15 dòng)
            //var orderData = new List<RevenueData>
            //{
            //    new RevenueData { DayIndex = 0, Revenue = 500000 },
            //    new RevenueData { DayIndex = 1, Revenue = 750000 },
            //    new RevenueData { DayIndex = 2, Revenue = 300000 },
            //    new RevenueData { DayIndex = 3, Revenue = 1200000 },
            //    new RevenueData { DayIndex = 4, Revenue = 450000 },
            //    new RevenueData { DayIndex = 5, Revenue = 600000 },
            //    new RevenueData { DayIndex = 6, Revenue = 850000 },
            //    new RevenueData { DayIndex = 7, Revenue = 400000 },
            //    new RevenueData { DayIndex = 8, Revenue = 950000 },
            //    new RevenueData { DayIndex = 9, Revenue = 550000 },
            //    new RevenueData { DayIndex = 10, Revenue = 700000 },
            //    new RevenueData { DayIndex = 11, Revenue = 480000 },
            //    new RevenueData { DayIndex = 12, Revenue = 620000 },
            //    new RevenueData { DayIndex = 13, Revenue = 890000 },
            //    new RevenueData { DayIndex = 14, Revenue = 340000 }
            //};
            var context = new db_flowerDataContext();
            // Lấy dữ liệu từ bảng orders
            var orderData = context.orders
                .Select(o => new RevenueData
                {
                    Revenue = (float)o.total_amount, // Chuyển đổi sang kiểu float
                    DayIndex = (float)(o.order_date - context.orders.Min(x => x.order_date)).TotalDays // Tính số ngày từ mốc đầu tiên
                })
                .ToList();
            // Khởi tạo MLContext
            var mlContext = new MLContext();

            // Tải dữ liệu vào IDataView
            var data = mlContext.Data.LoadFromEnumerable(orderData);

            // Chia dữ liệu thành tập huấn luyện và tập kiểm tra
            var trainTestSplit = mlContext.Data.TrainTestSplit(data, testFraction: 0.2);

            // Tạo pipeline huấn luyện với Linear Regression
            var pipeline = mlContext.Transforms.Concatenate("Features", "DayIndex")
                .Append(mlContext.Regression.Trainers.LbfgsPoissonRegression(labelColumnName: "Revenue", featureColumnName: "Features"));

            // Đo thời gian huấn luyện
            var stopWatch = System.Diagnostics.Stopwatch.StartNew();

            // Huấn luyện mô hình
            var model = pipeline.Fit(trainTestSplit.TrainSet);

            // Dừng đo thời gian
            stopWatch.Stop();

            // In ra thời gian huấn luyện
            MessageBox.Show($"Training time: {stopWatch.ElapsedMilliseconds} ms");

            //// Dự đoán doanh thu cho ngày tiếp theo (DayIndex = 15)
            //var predictionEngine = mlContext.Model.CreatePredictionEngine<RevenueData, RevenuePrediction>(model);
            //var prediction = predictionEngine.Predict(new RevenueData { DayIndex = 15 });

            //// In ra doanh thu dự đoán
            //Console.WriteLine($"Doanh thu dự đoán cho ngày tiếp theo (DayIndex = 15): {prediction.PredictedRevenue:#,0} VNĐ");

            // Lưu mô hình vào file
            mlContext.Model.Save(model, trainTestSplit.TrainSet.Schema, "revenue_model.zip");
            MessageBox.Show("Model saved to revenue_model.zip");
        }

        private async void btnDuDoan_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem mô hình đã được huấn luyện và lưu trữ chưa
            string modelPath = "revenue_model.zip";
            if (!File.Exists(modelPath))
            {
                MessageBox.Show("Mô hình chưa được huấn luyện. Vui lòng huấn luyện mô hình trước!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Tải mô hình đã lưu
            var mlContext = new MLContext();
            var model = mlContext.Model.Load(modelPath, out var inputSchema);
            var predictionEngine = mlContext.Model.CreatePredictionEngine<RevenueData, RevenuePrediction>(model);

            // Kết nối cơ sở dữ liệu để lấy dữ liệu
            using (var context = new db_flowerDataContext())
            {
                var orderData = context.orders
                    .Select(o => new
                    {
                        OrderDate = o.order_date,
                        TotalAmount = o.total_amount
                    })
                    .ToList();

                // Tính số ngày (DayIndex) từ ngày đầu tiên
                var startDate = orderData.Min(o => o.OrderDate);
                var formattedData = orderData.Select(o => new
                {
                    DayIndex = (float)(o.OrderDate - startDate).TotalDays, // Số ngày từ mốc
                    Revenue = (float)o.TotalAmount
                }).ToList();

                // Xác định DayIndex lớn nhất
                float maxDayIndex = formattedData.Max(o => o.DayIndex);

                // Dự đoán doanh thu cho ngày tiếp theo
                var prediction = predictionEngine.Predict(new RevenueData { DayIndex = maxDayIndex + 1 });

                MessageBox.Show($"Doanh thu dự đoán cho ngày tiếp theo: {prediction.PredictedRevenue:#,0} VNĐ", "Kết quả dự đoán", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
