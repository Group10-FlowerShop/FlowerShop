namespace FlowerShop.TrangChu
{
    partial class TrangChu
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
            this.thongKe1 = new FlowerShop.TrangChu.ThongKe();
            this.SuspendLayout();
            // 
            // thongKe1
            // 
            this.thongKe1.Location = new System.Drawing.Point(0, 0);
            this.thongKe1.Name = "thongKe1";
            this.thongKe1.Size = new System.Drawing.Size(1056, 432);
            this.thongKe1.TabIndex = 0;
            // 
            // TrangChu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.thongKe1);
            this.Name = "TrangChu";
            this.Size = new System.Drawing.Size(1076, 731);
            this.ResumeLayout(false);

        }

        #endregion

        private ThongKe thongKe1;
    }
}
