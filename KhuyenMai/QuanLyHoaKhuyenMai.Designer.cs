namespace FlowerShop.KhuyenMai
{
    partial class QuanLyHoaKhuyenMai
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
            this.cboMahoa = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dgrvHoakhuyenmai = new System.Windows.Forms.DataGridView();
            this.txtLoaigiamgia = new System.Windows.Forms.TextBox();
            this.cboMakhuyenmai = new System.Windows.Forms.ComboBox();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.txtGiatrigiam = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrvHoakhuyenmai)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboMahoa);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dgrvHoakhuyenmai);
            this.groupBox1.Controls.Add(this.txtLoaigiamgia);
            this.groupBox1.Controls.Add(this.cboMakhuyenmai);
            this.groupBox1.Controls.Add(this.btnHuy);
            this.groupBox1.Controls.Add(this.btnXoa);
            this.groupBox1.Controls.Add(this.btnThem);
            this.groupBox1.Controls.Add(this.txtGiatrigiam);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1073, 658);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Danh sách hoa khuyến mãi";
            // 
            // cboMahoa
            // 
            this.cboMahoa.FormattingEnabled = true;
            this.cboMahoa.Location = new System.Drawing.Point(665, 43);
            this.cboMahoa.Name = "cboMahoa";
            this.cboMahoa.Size = new System.Drawing.Size(225, 28);
            this.cboMahoa.TabIndex = 102;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(532, 121);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 22);
            this.label3.TabIndex = 101;
            this.label3.Text = "Giá trị giảm";
            // 
            // dgrvHoakhuyenmai
            // 
            this.dgrvHoakhuyenmai.AllowUserToAddRows = false;
            this.dgrvHoakhuyenmai.AllowUserToDeleteRows = false;
            this.dgrvHoakhuyenmai.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrvHoakhuyenmai.Location = new System.Drawing.Point(23, 186);
            this.dgrvHoakhuyenmai.Name = "dgrvHoakhuyenmai";
            this.dgrvHoakhuyenmai.ReadOnly = true;
            this.dgrvHoakhuyenmai.Size = new System.Drawing.Size(1020, 334);
            this.dgrvHoakhuyenmai.TabIndex = 100;
            this.dgrvHoakhuyenmai.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrvHoakhuyenmai_CellClick);
            this.dgrvHoakhuyenmai.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgrvHoakhuyenmai_CellContentClick);
            // 
            // txtLoaigiamgia
            // 
            this.txtLoaigiamgia.Enabled = false;
            this.txtLoaigiamgia.Location = new System.Drawing.Point(202, 116);
            this.txtLoaigiamgia.Name = "txtLoaigiamgia";
            this.txtLoaigiamgia.Size = new System.Drawing.Size(222, 27);
            this.txtLoaigiamgia.TabIndex = 99;
            // 
            // cboMakhuyenmai
            // 
            this.cboMakhuyenmai.FormattingEnabled = true;
            this.cboMakhuyenmai.Location = new System.Drawing.Point(205, 43);
            this.cboMakhuyenmai.Name = "cboMakhuyenmai";
            this.cboMakhuyenmai.Size = new System.Drawing.Size(219, 28);
            this.cboMakhuyenmai.TabIndex = 98;
            this.cboMakhuyenmai.SelectedIndexChanged += new System.EventHandler(this.cboMakhuyenmai_SelectedIndexChanged);
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(943, 525);
            this.btnHuy.Margin = new System.Windows.Forms.Padding(2);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(98, 39);
            this.btnHuy.TabIndex = 97;
            this.btnHuy.Text = "Huỷ bỏ";
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(816, 525);
            this.btnXoa.Margin = new System.Windows.Forms.Padding(2);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(98, 39);
            this.btnXoa.TabIndex = 96;
            this.btnXoa.Text = "Xoá";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(689, 525);
            this.btnThem.Margin = new System.Windows.Forms.Padding(2);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(98, 39);
            this.btnThem.TabIndex = 94;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // txtGiatrigiam
            // 
            this.txtGiatrigiam.Enabled = false;
            this.txtGiatrigiam.Location = new System.Drawing.Point(665, 118);
            this.txtGiatrigiam.Margin = new System.Windows.Forms.Padding(2);
            this.txtGiatrigiam.Name = "txtGiatrigiam";
            this.txtGiatrigiam.Size = new System.Drawing.Size(225, 27);
            this.txtGiatrigiam.TabIndex = 93;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(46, 116);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 22);
            this.label4.TabIndex = 92;
            this.label4.Text = "Loại giảm giá";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(532, 48);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 22);
            this.label2.TabIndex = 91;
            this.label2.Text = "Mã hoa";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 49);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 22);
            this.label1.TabIndex = 90;
            this.label1.Text = "Mã khuyến mãi";
            // 
            // QuanLyHoaKhuyenMai
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "QuanLyHoaKhuyenMai";
            this.Size = new System.Drawing.Size(1076, 662);
            this.Load += new System.EventHandler(this.frmQuanLyHoaKhuyenMai_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrvHoakhuyenmai)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboMahoa;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgrvHoakhuyenmai;
        private System.Windows.Forms.TextBox txtLoaigiamgia;
        private System.Windows.Forms.ComboBox cboMakhuyenmai;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.TextBox txtGiatrigiam;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}
