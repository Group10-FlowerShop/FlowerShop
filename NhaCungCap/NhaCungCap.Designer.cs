namespace FlowerShop.NhaCungCap
{
    partial class NhaCungCap
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.quanLyNhapHang1 = new FlowerShop.NhaCungCap.QuanLyNhapHang();
            this.quanLyNhaCungCap1 = new FlowerShop.NhaCungCap.QuanLyNhaCungCap();
            this.quanLyKhoHang1 = new FlowerShop.NhaCungCap.QuanLyKhoHang();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1076, 60);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(729, 10);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(171, 40);
            this.button2.TabIndex = 4;
            this.button2.Text = "Kho hàng";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(433, 10);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(171, 40);
            this.button4.TabIndex = 3;
            this.button4.Text = "Đơn nhập hàng";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(118, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(192, 40);
            this.button1.TabIndex = 0;
            this.button1.Text = "Danh sách nhà cung cấp";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // quanLyNhapHang1
            // 
            this.quanLyNhapHang1.Location = new System.Drawing.Point(0, 66);
            this.quanLyNhapHang1.Name = "quanLyNhapHang1";
            this.quanLyNhapHang1.Size = new System.Drawing.Size(1076, 662);
            this.quanLyNhapHang1.TabIndex = 3;
            // 
            // quanLyNhaCungCap1
            // 
            this.quanLyNhaCungCap1.Location = new System.Drawing.Point(0, 66);
            this.quanLyNhaCungCap1.Name = "quanLyNhaCungCap1";
            this.quanLyNhaCungCap1.Size = new System.Drawing.Size(1076, 662);
            this.quanLyNhaCungCap1.TabIndex = 2;
            // 
            // quanLyKhoHang1
            // 
            this.quanLyKhoHang1.Location = new System.Drawing.Point(0, 66);
            this.quanLyKhoHang1.Name = "quanLyKhoHang1";
            this.quanLyKhoHang1.Size = new System.Drawing.Size(1076, 662);
            this.quanLyKhoHang1.TabIndex = 4;
            // 
            // NhaCungCap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.quanLyKhoHang1);
            this.Controls.Add(this.quanLyNhapHang1);
            this.Controls.Add(this.quanLyNhaCungCap1);
            this.Controls.Add(this.panel1);
            this.Name = "NhaCungCap";
            this.Size = new System.Drawing.Size(1076, 731);
            this.Load += new System.EventHandler(this.NhaCungCap_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button1;
        private QuanLyNhaCungCap quanLyNhaCungCap1;
        private QuanLyNhapHang quanLyNhapHang1;
        private System.Windows.Forms.Button button2;
        private QuanLyKhoHang quanLyKhoHang1;
    }
}
