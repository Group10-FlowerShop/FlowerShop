namespace FlowerShop.TrangChu
{
    partial class ThongKe
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnDuDoan = new System.Windows.Forms.Button();
            this.lblDatetime = new System.Windows.Forms.Label();
            this.lblTotalRevenue = new System.Windows.Forms.Label();
            this.lblTotalOrders = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnDoThi = new System.Windows.Forms.Button();
            this.dgvOrdersByStatus = new System.Windows.Forms.DataGridView();
            this.btnTrainig = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radHoaDon = new System.Windows.Forms.RadioButton();
            this.radTheoTrangThai = new System.Windows.Forms.RadioButton();
            this.radTongHoaDon = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLoc = new System.Windows.Forms.Button();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdersByStatus)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // btnDuDoan
            // 
            this.btnDuDoan.Location = new System.Drawing.Point(796, 373);
            this.btnDuDoan.Name = "btnDuDoan";
            this.btnDuDoan.Size = new System.Drawing.Size(108, 39);
            this.btnDuDoan.TabIndex = 6;
            this.btnDuDoan.Text = "Dự Đoán";
            this.btnDuDoan.UseVisualStyleBackColor = true;
            // 
            // lblDatetime
            // 
            this.lblDatetime.AutoSize = true;
            this.lblDatetime.Location = new System.Drawing.Point(37, 31);
            this.lblDatetime.Name = "lblDatetime";
            this.lblDatetime.Size = new System.Drawing.Size(0, 22);
            this.lblDatetime.TabIndex = 6;
            // 
            // lblTotalRevenue
            // 
            this.lblTotalRevenue.AutoSize = true;
            this.lblTotalRevenue.Location = new System.Drawing.Point(2, 120);
            this.lblTotalRevenue.Name = "lblTotalRevenue";
            this.lblTotalRevenue.Size = new System.Drawing.Size(0, 22);
            this.lblTotalRevenue.TabIndex = 1;
            // 
            // lblTotalOrders
            // 
            this.lblTotalOrders.AutoSize = true;
            this.lblTotalOrders.Location = new System.Drawing.Point(2, 78);
            this.lblTotalOrders.Name = "lblTotalOrders";
            this.lblTotalOrders.Size = new System.Drawing.Size(0, 22);
            this.lblTotalOrders.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblDatetime);
            this.groupBox3.Controls.Add(this.lblTotalRevenue);
            this.groupBox3.Controls.Add(this.lblTotalOrders);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(782, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(268, 159);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Thông Sô";
            // 
            // btnDoThi
            // 
            this.btnDoThi.Location = new System.Drawing.Point(662, 238);
            this.btnDoThi.Name = "btnDoThi";
            this.btnDoThi.Size = new System.Drawing.Size(108, 39);
            this.btnDoThi.TabIndex = 2;
            this.btnDoThi.Text = "Đồ Thị";
            this.btnDoThi.UseVisualStyleBackColor = true;
            // 
            // dgvOrdersByStatus
            // 
            this.dgvOrdersByStatus.AllowUserToAddRows = false;
            this.dgvOrdersByStatus.AllowUserToDeleteRows = false;
            this.dgvOrdersByStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrdersByStatus.Location = new System.Drawing.Point(6, 26);
            this.dgvOrdersByStatus.Name = "dgvOrdersByStatus";
            this.dgvOrdersByStatus.ReadOnly = true;
            this.dgvOrdersByStatus.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrdersByStatus.Size = new System.Drawing.Size(764, 209);
            this.dgvOrdersByStatus.TabIndex = 0;
            // 
            // btnTrainig
            // 
            this.btnTrainig.Location = new System.Drawing.Point(927, 373);
            this.btnTrainig.Name = "btnTrainig";
            this.btnTrainig.Size = new System.Drawing.Size(108, 39);
            this.btnTrainig.TabIndex = 10;
            this.btnTrainig.Text = "Training";
            this.btnTrainig.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnDoThi);
            this.groupBox2.Controls.Add(this.dgvOrdersByStatus);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(0, 135);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(776, 280);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Danh Sách";
            // 
            // radHoaDon
            // 
            this.radHoaDon.AutoSize = true;
            this.radHoaDon.Location = new System.Drawing.Point(395, 90);
            this.radHoaDon.Name = "radHoaDon";
            this.radHoaDon.Size = new System.Drawing.Size(193, 26);
            this.radHoaDon.TabIndex = 7;
            this.radHoaDon.Text = "Danh Sách Hóa Đơn";
            this.radHoaDon.UseVisualStyleBackColor = true;
            // 
            // radTheoTrangThai
            // 
            this.radTheoTrangThai.AutoSize = true;
            this.radTheoTrangThai.Location = new System.Drawing.Point(395, 58);
            this.radTheoTrangThai.Name = "radTheoTrangThai";
            this.radTheoTrangThai.Size = new System.Drawing.Size(164, 26);
            this.radTheoTrangThai.TabIndex = 6;
            this.radTheoTrangThai.Text = "Theo Trạng Thái";
            this.radTheoTrangThai.UseVisualStyleBackColor = true;
            // 
            // radTongHoaDon
            // 
            this.radTongHoaDon.AutoSize = true;
            this.radTongHoaDon.Checked = true;
            this.radTongHoaDon.Location = new System.Drawing.Point(395, 26);
            this.radTongHoaDon.Name = "radTongHoaDon";
            this.radTongHoaDon.Size = new System.Drawing.Size(146, 26);
            this.radTongHoaDon.TabIndex = 5;
            this.radTongHoaDon.TabStop = true;
            this.radTongHoaDon.Text = "Tổng Hóa Đơn";
            this.radTongHoaDon.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 22);
            this.label2.TabIndex = 3;
            this.label2.Text = "Đến Ngày";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 22);
            this.label1.TabIndex = 2;
            this.label1.Text = "Từ Ngày";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(102, 68);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(200, 27);
            this.dtpEndDate.TabIndex = 1;
            this.dtpEndDate.Value = new System.DateTime(2025, 1, 1, 0, 0, 0, 0);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radHoaDon);
            this.groupBox1.Controls.Add(this.radTheoTrangThai);
            this.groupBox1.Controls.Add(this.radTongHoaDon);
            this.groupBox1.Controls.Add(this.btnLoc);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpEndDate);
            this.groupBox1.Controls.Add(this.dtpStartDate);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(776, 124);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thống Kê";
            // 
            // btnLoc
            // 
            this.btnLoc.Location = new System.Drawing.Point(691, 31);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(79, 69);
            this.btnLoc.TabIndex = 4;
            this.btnLoc.Text = "Lọc";
            this.btnLoc.UseVisualStyleBackColor = true;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(102, 26);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(200, 27);
            this.dtpStartDate.TabIndex = 0;
            this.dtpStartDate.Value = new System.DateTime(2023, 1, 1, 0, 0, 0, 0);
            // 
            // ThongKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnDuDoan);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnTrainig);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ThongKe";
            this.Size = new System.Drawing.Size(1056, 432);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdersByStatus)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button btnDuDoan;
        private System.Windows.Forms.Label lblDatetime;
        private System.Windows.Forms.Label lblTotalRevenue;
        private System.Windows.Forms.Label lblTotalOrders;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnDoThi;
        private System.Windows.Forms.DataGridView dgvOrdersByStatus;
        private System.Windows.Forms.Button btnTrainig;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radHoaDon;
        private System.Windows.Forms.RadioButton radTheoTrangThai;
        private System.Windows.Forms.RadioButton radTongHoaDon;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
    }
}
