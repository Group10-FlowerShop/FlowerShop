namespace FlowerShop.SanPham
{
    partial class DanhMuc
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
            this.txtMahoa = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnKieucach = new System.Windows.Forms.Button();
            this.btnLuudip = new System.Windows.Forms.Button();
            this.clbKieucach = new System.Windows.Forms.CheckedListBox();
            this.clbDip = new System.Windows.Forms.CheckedListBox();
            this.clbMauhoa = new System.Windows.Forms.CheckedListBox();
            this.clbLoaihoa = new System.Windows.Forms.CheckedListBox();
            this.btnThemloaihoa = new System.Windows.Forms.Button();
            this.btnLuumau = new System.Windows.Forms.Button();
            this.btn_back = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_back);
            this.groupBox1.Controls.Add(this.txtMahoa);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1073, 728);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Quản lý danh mục";
            // 
            // txtMahoa
            // 
            this.txtMahoa.Enabled = false;
            this.txtMahoa.Location = new System.Drawing.Point(142, 48);
            this.txtMahoa.Name = "txtMahoa";
            this.txtMahoa.Size = new System.Drawing.Size(255, 27);
            this.txtMahoa.TabIndex = 27;
            this.txtMahoa.TextChanged += new System.EventHandler(this.txtMahoa_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 22);
            this.label1.TabIndex = 26;
            this.label1.Text = "Mã hoa";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 249F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 268F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 261F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 253F));
            this.tableLayoutPanel1.Controls.Add(this.btnKieucach, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnLuudip, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.clbKieucach, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.clbDip, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.clbMauhoa, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.clbLoaihoa, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnThemloaihoa, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnLuumau, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(18, 99);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 91.09462F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.90538F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1031, 539);
            this.tableLayoutPanel1.TabIndex = 25;
            // 
            // btnKieucach
            // 
            this.btnKieucach.Location = new System.Drawing.Point(781, 494);
            this.btnKieucach.Name = "btnKieucach";
            this.btnKieucach.Size = new System.Drawing.Size(148, 42);
            this.btnKieucach.TabIndex = 9;
            this.btnKieucach.Text = "Lưu kiểu cách";
            this.btnKieucach.UseVisualStyleBackColor = true;
            this.btnKieucach.Click += new System.EventHandler(this.btnKieucach_Click);
            // 
            // btnLuudip
            // 
            this.btnLuudip.Location = new System.Drawing.Point(520, 494);
            this.btnLuudip.Name = "btnLuudip";
            this.btnLuudip.Size = new System.Drawing.Size(104, 42);
            this.btnLuudip.TabIndex = 8;
            this.btnLuudip.Text = "Lưu dịp";
            this.btnLuudip.UseVisualStyleBackColor = true;
            this.btnLuudip.Click += new System.EventHandler(this.btnLuudip_Click);
            // 
            // clbKieucach
            // 
            this.clbKieucach.FormattingEnabled = true;
            this.clbKieucach.Location = new System.Drawing.Point(781, 3);
            this.clbKieucach.Name = "clbKieucach";
            this.clbKieucach.Size = new System.Drawing.Size(247, 466);
            this.clbKieucach.TabIndex = 5;
            // 
            // clbDip
            // 
            this.clbDip.FormattingEnabled = true;
            this.clbDip.Location = new System.Drawing.Point(520, 3);
            this.clbDip.Name = "clbDip";
            this.clbDip.Size = new System.Drawing.Size(255, 466);
            this.clbDip.TabIndex = 4;
            // 
            // clbMauhoa
            // 
            this.clbMauhoa.FormattingEnabled = true;
            this.clbMauhoa.Location = new System.Drawing.Point(252, 3);
            this.clbMauhoa.Name = "clbMauhoa";
            this.clbMauhoa.Size = new System.Drawing.Size(262, 466);
            this.clbMauhoa.TabIndex = 3;
            // 
            // clbLoaihoa
            // 
            this.clbLoaihoa.FormattingEnabled = true;
            this.clbLoaihoa.Location = new System.Drawing.Point(3, 3);
            this.clbLoaihoa.Name = "clbLoaihoa";
            this.clbLoaihoa.Size = new System.Drawing.Size(243, 466);
            this.clbLoaihoa.TabIndex = 2;
            // 
            // btnThemloaihoa
            // 
            this.btnThemloaihoa.Location = new System.Drawing.Point(3, 494);
            this.btnThemloaihoa.Name = "btnThemloaihoa";
            this.btnThemloaihoa.Size = new System.Drawing.Size(93, 42);
            this.btnThemloaihoa.TabIndex = 0;
            this.btnThemloaihoa.Text = "Lưu loại";
            this.btnThemloaihoa.UseVisualStyleBackColor = true;
            this.btnThemloaihoa.Click += new System.EventHandler(this.btnThemloaihoa_Click);
            // 
            // btnLuumau
            // 
            this.btnLuumau.Location = new System.Drawing.Point(252, 494);
            this.btnLuumau.Name = "btnLuumau";
            this.btnLuumau.Size = new System.Drawing.Size(102, 42);
            this.btnLuumau.TabIndex = 1;
            this.btnLuumau.Text = "Lưu màu";
            this.btnLuumau.UseVisualStyleBackColor = true;
            this.btnLuumau.Click += new System.EventHandler(this.btnLuumau_Click);
            // 
            // btn_back
            // 
            this.btn_back.Location = new System.Drawing.Point(898, 40);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(148, 42);
            this.btn_back.TabIndex = 28;
            this.btn_back.Text = "Quay về";
            this.btn_back.UseVisualStyleBackColor = true;
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // DanhMuc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "DanhMuc";
            this.Size = new System.Drawing.Size(1076, 731);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtMahoa;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnKieucach;
        private System.Windows.Forms.Button btnLuudip;
        private System.Windows.Forms.CheckedListBox clbKieucach;
        private System.Windows.Forms.CheckedListBox clbDip;
        private System.Windows.Forms.CheckedListBox clbMauhoa;
        private System.Windows.Forms.CheckedListBox clbLoaihoa;
        private System.Windows.Forms.Button btnThemloaihoa;
        private System.Windows.Forms.Button btnLuumau;
        private System.Windows.Forms.Button btn_back;
    }
}
