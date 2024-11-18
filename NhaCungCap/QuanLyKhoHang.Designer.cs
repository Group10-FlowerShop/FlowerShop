namespace FlowerShop.NhaCungCap
{
    partial class QuanLyKhoHang
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_timKiem = new System.Windows.Forms.Button();
            this.btn_capNhat = new System.Windows.Forms.Button();
            this.dgv_khoHang = new System.Windows.Forms.DataGridView();
            this.txt_soLuong = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_maHoa = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_khoHang)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_timKiem);
            this.groupBox1.Controls.Add(this.btn_capNhat);
            this.groupBox1.Controls.Add(this.dgv_khoHang);
            this.groupBox1.Controls.Add(this.txt_soLuong);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_maHoa);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1073, 658);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Kho hàng";
            // 
            // btn_timKiem
            // 
            this.btn_timKiem.Location = new System.Drawing.Point(326, 70);
            this.btn_timKiem.Margin = new System.Windows.Forms.Padding(2);
            this.btn_timKiem.Name = "btn_timKiem";
            this.btn_timKiem.Size = new System.Drawing.Size(116, 40);
            this.btn_timKiem.TabIndex = 13;
            this.btn_timKiem.Text = "Tìm kiếm";
            this.btn_timKiem.UseVisualStyleBackColor = true;
            this.btn_timKiem.Click += new System.EventHandler(this.btn_timKiem_Click);
            // 
            // btn_capNhat
            // 
            this.btn_capNhat.Location = new System.Drawing.Point(912, 70);
            this.btn_capNhat.Margin = new System.Windows.Forms.Padding(2);
            this.btn_capNhat.Name = "btn_capNhat";
            this.btn_capNhat.Size = new System.Drawing.Size(131, 40);
            this.btn_capNhat.TabIndex = 12;
            this.btn_capNhat.Text = "Cập nhật";
            this.btn_capNhat.UseVisualStyleBackColor = true;
            this.btn_capNhat.Click += new System.EventHandler(this.btn_capNhat_Click);
            // 
            // dgv_khoHang
            // 
            this.dgv_khoHang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv_khoHang.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_khoHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_khoHang.Location = new System.Drawing.Point(27, 116);
            this.dgv_khoHang.Margin = new System.Windows.Forms.Padding(2);
            this.dgv_khoHang.Name = "dgv_khoHang";
            this.dgv_khoHang.ReadOnly = true;
            this.dgv_khoHang.RowHeadersWidth = 51;
            this.dgv_khoHang.RowTemplate.Height = 24;
            this.dgv_khoHang.Size = new System.Drawing.Size(1016, 252);
            this.dgv_khoHang.TabIndex = 11;
            this.dgv_khoHang.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_khoHang_CellClick);
            // 
            // txt_soLuong
            // 
            this.txt_soLuong.Location = new System.Drawing.Point(738, 85);
            this.txt_soLuong.Margin = new System.Windows.Forms.Padding(2);
            this.txt_soLuong.Name = "txt_soLuong";
            this.txt_soLuong.Size = new System.Drawing.Size(136, 27);
            this.txt_soLuong.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(648, 88);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 22);
            this.label2.TabIndex = 9;
            this.label2.Text = "Số lượng:";
            // 
            // txt_maHoa
            // 
            this.txt_maHoa.Location = new System.Drawing.Point(106, 83);
            this.txt_maHoa.Margin = new System.Windows.Forms.Padding(2);
            this.txt_maHoa.Name = "txt_maHoa";
            this.txt_maHoa.Size = new System.Drawing.Size(170, 27);
            this.txt_maHoa.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 88);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 22);
            this.label1.TabIndex = 7;
            this.label1.Text = "Mã hoa:";
            // 
            // QuanLyKhoHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "QuanLyKhoHang";
            this.Size = new System.Drawing.Size(1076, 662);
            this.Load += new System.EventHandler(this.FormQLKho_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_khoHang)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_timKiem;
        private System.Windows.Forms.Button btn_capNhat;
        private System.Windows.Forms.DataGridView dgv_khoHang;
        private System.Windows.Forms.TextBox txt_soLuong;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_maHoa;
        private System.Windows.Forms.Label label1;
    }
}
