
namespace FlowerShop.DoiTra
{
    partial class ChiTietDoiTra
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
            this.btn_quayLai = new System.Windows.Forms.Button();
            this.btn_xoa = new System.Windows.Forms.Button();
            this.btn_capNhat = new System.Windows.Forms.Button();
            this.btn_them = new System.Windows.Forms.Button();
            this.dgv_chiTiet = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_giaTien = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_soLuong = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbo_maHoa = new System.Windows.Forms.ComboBox();
            this.txt_maDoiTra = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_chiTiet)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_quayLai
            // 
            this.btn_quayLai.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_quayLai.Location = new System.Drawing.Point(129, 691);
            this.btn_quayLai.Name = "btn_quayLai";
            this.btn_quayLai.Size = new System.Drawing.Size(120, 44);
            this.btn_quayLai.TabIndex = 37;
            this.btn_quayLai.Text = "Quay lại";
            this.btn_quayLai.UseVisualStyleBackColor = true;
            this.btn_quayLai.Click += new System.EventHandler(this.btn_quayLai_Click);
            // 
            // btn_xoa
            // 
            this.btn_xoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_xoa.Location = new System.Drawing.Point(1205, 691);
            this.btn_xoa.Name = "btn_xoa";
            this.btn_xoa.Size = new System.Drawing.Size(97, 44);
            this.btn_xoa.TabIndex = 36;
            this.btn_xoa.Text = "Xóa";
            this.btn_xoa.UseVisualStyleBackColor = true;
            this.btn_xoa.Click += new System.EventHandler(this.btn_xoa_Click);
            // 
            // btn_capNhat
            // 
            this.btn_capNhat.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_capNhat.Location = new System.Drawing.Point(1007, 691);
            this.btn_capNhat.Name = "btn_capNhat";
            this.btn_capNhat.Size = new System.Drawing.Size(140, 44);
            this.btn_capNhat.TabIndex = 35;
            this.btn_capNhat.Text = "Cập nhật";
            this.btn_capNhat.UseVisualStyleBackColor = true;
            this.btn_capNhat.Click += new System.EventHandler(this.btn_capNhat_Click);
            // 
            // btn_them
            // 
            this.btn_them.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_them.Location = new System.Drawing.Point(843, 691);
            this.btn_them.Name = "btn_them";
            this.btn_them.Size = new System.Drawing.Size(120, 44);
            this.btn_them.TabIndex = 34;
            this.btn_them.Text = "Thêm";
            this.btn_them.UseVisualStyleBackColor = true;
            this.btn_them.Click += new System.EventHandler(this.btn_them_Click);
            // 
            // dgv_chiTiet
            // 
            this.dgv_chiTiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_chiTiet.Location = new System.Drawing.Point(129, 216);
            this.dgv_chiTiet.Name = "dgv_chiTiet";
            this.dgv_chiTiet.RowHeadersWidth = 51;
            this.dgv_chiTiet.RowTemplate.Height = 24;
            this.dgv_chiTiet.Size = new System.Drawing.Size(1173, 427);
            this.dgv_chiTiet.TabIndex = 33;
            this.dgv_chiTiet.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_chiTiet_CellClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(819, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 26);
            this.label4.TabIndex = 32;
            this.label4.Text = "Mã hoa";
            // 
            // txt_giaTien
            // 
            this.txt_giaTien.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_giaTien.Location = new System.Drawing.Point(932, 155);
            this.txt_giaTien.Name = "txt_giaTien";
            this.txt_giaTien.Size = new System.Drawing.Size(215, 32);
            this.txt_giaTien.TabIndex = 31;
            this.txt_giaTien.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_giaTien_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(819, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 26);
            this.label3.TabIndex = 30;
            this.label3.Text = "Giá tiền";
            // 
            // txt_soLuong
            // 
            this.txt_soLuong.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_soLuong.Location = new System.Drawing.Point(477, 142);
            this.txt_soLuong.Name = "txt_soLuong";
            this.txt_soLuong.Size = new System.Drawing.Size(198, 32);
            this.txt_soLuong.TabIndex = 29;
            this.txt_soLuong.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_soLuong_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(333, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 26);
            this.label2.TabIndex = 28;
            this.label2.Text = "Số lượng";
            // 
            // cbo_maHoa
            // 
            this.cbo_maHoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_maHoa.FormattingEnabled = true;
            this.cbo_maHoa.Location = new System.Drawing.Point(932, 88);
            this.cbo_maHoa.Name = "cbo_maHoa";
            this.cbo_maHoa.Size = new System.Drawing.Size(215, 34);
            this.cbo_maHoa.TabIndex = 27;
            // 
            // txt_maDoiTra
            // 
            this.txt_maDoiTra.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_maDoiTra.Location = new System.Drawing.Point(477, 75);
            this.txt_maDoiTra.Name = "txt_maDoiTra";
            this.txt_maDoiTra.Size = new System.Drawing.Size(198, 32);
            this.txt_maDoiTra.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(323, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 26);
            this.label1.TabIndex = 25;
            this.label1.Text = "Mã đổi trả";
            // 
            // ChiTietDoiTra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_quayLai);
            this.Controls.Add(this.btn_xoa);
            this.Controls.Add(this.btn_capNhat);
            this.Controls.Add(this.btn_them);
            this.Controls.Add(this.dgv_chiTiet);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_giaTien);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_soLuong);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbo_maHoa);
            this.Controls.Add(this.txt_maDoiTra);
            this.Controls.Add(this.label1);
            this.Name = "ChiTietDoiTra";
            this.Size = new System.Drawing.Size(1431, 810);
            this.Load += new System.EventHandler(this.FormChiTietDoiTra_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_chiTiet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_quayLai;
        private System.Windows.Forms.Button btn_xoa;
        private System.Windows.Forms.Button btn_capNhat;
        private System.Windows.Forms.Button btn_them;
        private System.Windows.Forms.DataGridView dgv_chiTiet;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_giaTien;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_soLuong;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbo_maHoa;
        private System.Windows.Forms.TextBox txt_maDoiTra;
        private System.Windows.Forms.Label label1;
    }
}
