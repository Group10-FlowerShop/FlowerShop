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

namespace FlowerShop.SanPham
{
    public partial class QuanLyDanhMuc : UserControl
    {
        db_flowerDataContext fs= new db_flowerDataContext();
        private string flowerId;
        public QuanLyDanhMuc(string flowerId)
        {
            InitializeComponent();
            this.flowerId = flowerId;
            txtMahoa.Text = flowerId;
        }

        private void btnThemloaihoa_Click(object sender, EventArgs e)
        {
            string selectedFlowerId = txtMahoa.Text;

            if (!string.IsNullOrEmpty(selectedFlowerId))
            {
                var existingCategories = fs.flower_categories.Where(fc => fc.flower_id == selectedFlowerId);
                fs.flower_categories.DeleteAllOnSubmit(existingCategories);
                fs.SubmitChanges();
                foreach (var item in clbLoaihoa.CheckedItems)
                {
                    var category = (dynamic)item;
                    string categoryId = category.category_id;
                    flower_category newFlowerCategory = new flower_category
                    {
                        flower_id = selectedFlowerId,
                        category_id = categoryId
                    };
                    fs.flower_categories.InsertOnSubmit(newFlowerCategory);
                }
                fs.SubmitChanges();
                MessageBox.Show("Lưu danh mục thành công.");
            }
            else
            {
                MessageBox.Show("Vui lòng nhập mã hoa trước khi lưu danh mục.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnLuumau_Click(object sender, EventArgs e)
        {
            string selectedFlowerId = txtMahoa.Text;

            if (!string.IsNullOrEmpty(selectedFlowerId))
            {
                var existingColors = fs.flower_colors.Where(fc => fc.flower_id == selectedFlowerId);
                fs.flower_colors.DeleteAllOnSubmit(existingColors);
                fs.SubmitChanges();
                foreach (var item in clbMauhoa.CheckedItems)
                {
                    var color = (dynamic)item;
                    string colorId = color.color_id;

                    flower_color newFlowerColor = new flower_color
                    {
                        flower_id = selectedFlowerId,
                        color_id = colorId
                    };

                    fs.flower_colors.InsertOnSubmit(newFlowerColor);
                }
                fs.SubmitChanges();
                MessageBox.Show("Lưu màu thành công.");
            }
            else
            {
                MessageBox.Show("Vui lòng nhập mã hoa trước khi lưu màu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnLuudip_Click(object sender, EventArgs e)
        {
            string selectedFlowerId = txtMahoa.Text;

            if (!string.IsNullOrEmpty(selectedFlowerId))
            {
                var existingOccasion = fs.flower_occasions.Where(fc => fc.flower_id == selectedFlowerId);
                fs.flower_occasions.DeleteAllOnSubmit(existingOccasion);
                fs.SubmitChanges();
                foreach (var item in clbDip.CheckedItems)
                {
                    var occasion = (dynamic)item;
                    string occasionId = occasion.occasion_id;

                    flower_occasion newFlowerOccasion = new flower_occasion
                    {
                        flower_id = selectedFlowerId,
                        occasion_id = occasionId
                    };

                    fs.flower_occasions.InsertOnSubmit(newFlowerOccasion);
                }
                fs.SubmitChanges();
                MessageBox.Show("Lưu dịp thành công.");
            }
            else
            {
                MessageBox.Show("Vui lòng nhập mã hoa trước khi dịp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnKieucach_Click(object sender, EventArgs e)
        {
            string selectedFlowerId = txtMahoa.Text;

            if (!string.IsNullOrEmpty(selectedFlowerId))
            {
                var existingStyle = fs.flower_styles.Where(fc => fc.flower_id == selectedFlowerId);
                fs.flower_styles.DeleteAllOnSubmit(existingStyle);
                fs.SubmitChanges();
                foreach (var item in clbKieucach.CheckedItems)
                {
                    var style = (dynamic)item;
                    string styleId = style.style_id;
                    flower_style newFlowerStyle = new flower_style
                    {
                        flower_id = selectedFlowerId,
                        style_id = styleId
                    };

                    fs.flower_styles.InsertOnSubmit(newFlowerStyle);
                }
                fs.SubmitChanges();
                MessageBox.Show("Lưu kiểu cách thành công.");
            }
            else
            {
                MessageBox.Show("Vui lòng nhập mã hoa trước khi lưu kiểu cách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtMahoa_TextChanged(object sender, EventArgs e)
        {
            string selectedFlowerId = txtMahoa.Text;

            //Lấy loại hoa
            var allCategories = from cg in fs.categories
                                select new { cg.name, cg.category_id };

            clbLoaihoa.DataSource = allCategories.ToList();
            clbLoaihoa.DisplayMember = "name";
            clbLoaihoa.ValueMember = "category_id";
            if (!string.IsNullOrEmpty(selectedFlowerId))
            {
                var selectedCategories = from fl_cg in fs.flower_categories
                                         where fl_cg.flower_id == selectedFlowerId
                                         select fl_cg.category_id;
                for (int i = 0; i < clbLoaihoa.Items.Count; i++)
                {
                    var item = (dynamic)clbLoaihoa.Items[i];
                    string categoryId = item.category_id;
                    clbLoaihoa.SetItemChecked(i, selectedCategories.Contains(categoryId));
                }
            }
            else
            {
                for (int i = 0; i < clbLoaihoa.Items.Count; i++)
                {
                    clbLoaihoa.SetItemChecked(i, false);
                }
            }

            //Lấy màu hoa
            var allColor = from fc in fs.colors
                           select new { fc.name, fc.color_id };

            clbMauhoa.DataSource = allColor.ToList();
            clbMauhoa.DisplayMember = "name";
            clbMauhoa.ValueMember = "color_id";
            if (!string.IsNullOrEmpty(selectedFlowerId))
            {
                var selectedColor = from fl_cl in fs.flower_colors
                                    where fl_cl.flower_id == selectedFlowerId
                                    select fl_cl.color_id;
                for (int i = 0; i < clbMauhoa.Items.Count; i++)
                {
                    var item = (dynamic)clbMauhoa.Items[i];
                    string colorId = item.color_id;
                    clbMauhoa.SetItemChecked(i, selectedColor.Contains(colorId));
                }
            }
            else
            {
                for (int i = 0; i < clbMauhoa.Items.Count; i++)
                {
                    clbMauhoa.SetItemChecked(i, false);
                }
            }

            //Lấy dịp dùng hoa
            var allOccasion = from oc in fs.occasions
                              select new { oc.name, oc.occasion_id };

            clbDip.DataSource = allOccasion.ToList();
            clbDip.DisplayMember = "name";
            clbDip.ValueMember = "occasion_id";
            if (!string.IsNullOrEmpty(selectedFlowerId))
            {
                var selectedOccasion = from fl_oc in fs.flower_occasions
                                       where fl_oc.flower_id == selectedFlowerId
                                       select fl_oc.occasion_id;
                for (int i = 0; i < clbDip.Items.Count; i++)
                {
                    var item = (dynamic)clbDip.Items[i];
                    string occasionId = item.occasion_id;
                    clbDip.SetItemChecked(i, selectedOccasion.Contains(occasionId));
                }
            }
            else
            {
                for (int i = 0; i < clbDip.Items.Count; i++)
                {
                    clbDip.SetItemChecked(i, false);
                }
            }

            //Lấy kiểu cách của hoa
            var allStyle = from st in fs.styles
                           select new { st.name, st.style_id };

            clbKieucach.DataSource = allStyle.ToList();
            clbKieucach.DisplayMember = "name";
            clbKieucach.ValueMember = "style_id";
            if (!string.IsNullOrEmpty(selectedFlowerId))
            {
                var selectedStyle = from fl_st in fs.flower_styles
                                    where fl_st.flower_id == selectedFlowerId
                                    select fl_st.style_id;
                for (int i = 0; i < clbKieucach.Items.Count; i++)
                {
                    var item = (dynamic)clbKieucach.Items[i];
                    string styleId = item.style_id;
                    clbKieucach.SetItemChecked(i, selectedStyle.Contains(styleId));
                }
            }
            else
            {
                for (int i = 0; i < clbKieucach.Items.Count; i++)
                {
                    clbKieucach.SetItemChecked(i, false);
                }
            }
        }
    }
}
