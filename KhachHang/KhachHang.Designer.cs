namespace FlowerShop.KhachHang
{
    partial class KhachHang
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
            this.quanLyKhachHang1 = new FlowerShop.KhachHang.QuanLyKhachHang();
            this.SuspendLayout();
            // 
            // quanLyKhachHang1
            // 
            this.quanLyKhachHang1.Location = new System.Drawing.Point(0, 0);
            this.quanLyKhachHang1.Name = "quanLyKhachHang1";
            this.quanLyKhachHang1.Size = new System.Drawing.Size(1076, 662);
            this.quanLyKhachHang1.TabIndex = 2;
            // 
            // KhachHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.quanLyKhachHang1);
            this.Name = "KhachHang";
            this.Size = new System.Drawing.Size(1076, 731);
            this.ResumeLayout(false);

        }

        #endregion
        private QuanLyKhachHang quanLyKhachHang1;
    }
}
