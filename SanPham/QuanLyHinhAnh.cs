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
using System.IO;

namespace FlowerShop.SanPham
{
    public partial class QuanLyHinhAnh : UserControl
    {
        db_flowerDataContext fs = new db_flowerDataContext();
        private string maHoa;
        private string currentImageFolder = @"D:\PTUD\Code\Hình ảnh";
        public QuanLyHinhAnh(string maHoa)
        {
            InitializeComponent();
            this.maHoa = maHoa;
            LoadImages(maHoa);
            txtMahoa.Text = maHoa;
        }

        private void LoadImages(string flowerId)
        {
            var images = fs.flower_images.Where(img => img.flower_id == flowerId).ToList();
            lstbAnh.Items.Clear();
            foreach (var image in images)
            {
                lstbAnh.Items.Add(image.image_url);
            }
        }

        void load_cboLoaiAnh()
        {
            cboLoaianh.Items.Add("main");
            cboLoaianh.Items.Add("thumbnail");
            cboLoaianh.Items.Add("gallery");
        }

        void load_cboLoaiMan()
        {
            cboLoaiman.Items.Add("1");
            cboLoaiman.Items.Add("2");
            cboLoaiman.Items.Add("3");
        }

        private void btnChonanhchinh_Click(object sender, EventArgs e)
        {
            if (lstbAnh.SelectedItem != null)
            {
                string selectedImageUrl = lstbAnh.SelectedItem.ToString();
                var selectedImage = fs.flower_images.FirstOrDefault(img => img.image_url == selectedImageUrl);
                if (selectedImage != null)
                {
                    var mainImage = fs.flower_images.FirstOrDefault(img => img.flower_id == selectedImage.flower_id && img.image_type == "main");
                    if (mainImage != null)
                    {
                        mainImage.image_type = "gallery";
                    }

                    selectedImage.image_type = "main";
                    fs.SubmitChanges();
                    LoadImages(selectedImage.flower_id);
                    MessageBox.Show("Đã cập nhật hình ảnh chính.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hình ảnh từ danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnThemanh_Click(object sender, EventArgs e)
        {
            if (txtMaanh.Text == string.Empty)
            {
                MessageBox.Show("Vui lòng không để trống mã ảnh!! Nhấn nút tạo mã bên dưới để tạo mới", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (cboLoaianh.SelectedIndex == -1 || cboLoaiman.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn thông tin cho Loại ảnh và Loại màn !!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thêm hình ảnh này không?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            string filePath = openFileDialog.FileName;
                            string fileName = Path.GetFileName(filePath);

                            string destinationPath = Path.Combine(currentImageFolder, fileName);

                            if (!File.Exists(destinationPath))
                            {
                                File.Copy(filePath, destinationPath);
                            }
                            else
                            {
                                MessageBox.Show("Ảnh đã tồn tại trong thư mục lưu trữ. Không cần sao chép.");
                            }

                            string flowerId = txtMahoa.Text;
                            flower_image newImage = new flower_image
                            {
                                image_id = txtMaanh.Text,
                                flower_id = flowerId,
                                image_url = fileName,
                                image_type = cboLoaianh.SelectedItem.ToString(),
                                display_order = int.Parse(cboLoaiman.SelectedItem.ToString())
                            };

                            fs.flower_images.InsertOnSubmit(newImage);
                            fs.SubmitChanges();
                            LoadImages(flowerId);

                            MessageBox.Show("Thêm hình ảnh thành công và đã sao chép vào thư mục lưu trữ.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Có lỗi xảy ra: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Thêm hình ảnh thất bại");
                }
            }
        }

        private void btnTaoma_Click(object sender, EventArgs e)
        {
            using (var context = new db_flowerDataContext())
            {
                var maAnhCuoi = context.flower_images.OrderByDescending(t => t.image_id).FirstOrDefault();
                string maAnhMoi;

                if (maAnhCuoi != null)
                {
                    string latestID = maAnhCuoi.image_id;
                    int ma = int.Parse(latestID.Substring(3));
                    maAnhMoi = "IMG" + (ma + 1).ToString("D3");
                }
                else
                {
                    maAnhMoi = "IMG001";
                }
                txtMaanh.Text = maAnhMoi;
            }
        }

        private void btnDuongdan_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowser = new FolderBrowserDialog())
            {
                folderBrowser.Description = "Chọn thư mục lưu trữ ảnh:";
                folderBrowser.SelectedPath = currentImageFolder;

                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    currentImageFolder = folderBrowser.SelectedPath;

                    MessageBox.Show($"Thư mục lưu trữ ảnh đã được thay đổi thành:\n{currentImageFolder}",
                                    "Thay đổi thành công",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
            }
        }
    }
}
