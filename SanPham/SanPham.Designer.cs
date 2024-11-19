namespace FlowerShop.SanPham
{
    partial class SanPham
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
            this.quanLySanPham1 = new FlowerShop.SanPham.QuanLySanPham();
            this.SuspendLayout();
            // 
            // quanLySanPham1
            // 
            this.quanLySanPham1.Location = new System.Drawing.Point(0, 0);
            this.quanLySanPham1.Name = "quanLySanPham1";
            this.quanLySanPham1.Size = new System.Drawing.Size(1076, 662);
            this.quanLySanPham1.TabIndex = 3;
            // 
            // SanPham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.quanLySanPham1);
            this.Name = "SanPham";
            this.Size = new System.Drawing.Size(1076, 731);
            this.ResumeLayout(false);

        }

        #endregion
        private QuanLySanPham quanLySanPham1;
    }
}
