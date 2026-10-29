namespace QuanLyNhanSu.Forms
{
    partial class FrmBangLuong
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.numThang = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numNam = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNguoiLap = new System.Windows.Forms.TextBox();
            this.btnXemTruoc = new System.Windows.Forms.Button();
            this.btnLap = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvBL = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvCT = new System.Windows.Forms.DataGridView();
            this.btnDong = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numThang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCT)).BeginInit();
            this.SuspendLayout();
            this.label1.Name = "label1";
            this.label1.Location = new System.Drawing.Point(10, 15);
            this.label1.AutoSize = true;
            this.label1.Text = "Tháng";
            this.label1.TabIndex = 0;
            this.Controls.Add(this.label1);
            this.numThang.Name = "numThang";
            this.numThang.Location = new System.Drawing.Point(60, 12);
            this.numThang.Size = new System.Drawing.Size(60, 23);
            this.numThang.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numThang.Maximum = new decimal(new int[] { 12, 0, 0, 0 });
            this.numThang.Value = new decimal(new int[] { 1, 0, 0, 0 });
            this.numThang.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numThang.TabIndex = 1;
            this.Controls.Add(this.numThang);
            this.label2.Name = "label2";
            this.label2.Location = new System.Drawing.Point(170, 15);
            this.label2.AutoSize = true;
            this.label2.Text = "Năm";
            this.label2.TabIndex = 2;
            this.Controls.Add(this.label2);
            this.numNam.Name = "numNam";
            this.numNam.Location = new System.Drawing.Point(210, 12);
            this.numNam.Size = new System.Drawing.Size(80, 23);
            this.numNam.Minimum = new decimal(new int[] { 2000, 0, 0, 0 });
            this.numNam.Maximum = new decimal(new int[] { 2100, 0, 0, 0 });
            this.numNam.Value = new decimal(new int[] { 2026, 0, 0, 0 });
            this.numNam.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numNam.TabIndex = 3;
            this.Controls.Add(this.numNam);
            this.label3.Name = "label3";
            this.label3.Location = new System.Drawing.Point(350, 15);
            this.label3.AutoSize = true;
            this.label3.Text = "Người lập";
            this.label3.TabIndex = 4;
            this.Controls.Add(this.label3);
            this.txtNguoiLap.Name = "txtNguoiLap";
            this.txtNguoiLap.Location = new System.Drawing.Point(415, 12);
            this.txtNguoiLap.Size = new System.Drawing.Size(150, 23);
            this.txtNguoiLap.TabIndex = 5;
            this.Controls.Add(this.txtNguoiLap);
            this.btnXemTruoc.Name = "btnXemTruoc";
            this.btnXemTruoc.Location = new System.Drawing.Point(580, 9);
            this.btnXemTruoc.Size = new System.Drawing.Size(110, 28);
            this.btnXemTruoc.Text = "Xem trước";
            this.btnXemTruoc.UseVisualStyleBackColor = true;
            this.btnXemTruoc.TabIndex = 6;
            this.btnXemTruoc.Click += new System.EventHandler(this.btnXemTruoc_Click);
            this.Controls.Add(this.btnXemTruoc);
            this.btnLap.Name = "btnLap";
            this.btnLap.Location = new System.Drawing.Point(700, 9);
            this.btnLap.Size = new System.Drawing.Size(150, 28);
            this.btnLap.Text = "Lập bảng lương";
            this.btnLap.UseVisualStyleBackColor = true;
            this.btnLap.TabIndex = 7;
            this.btnLap.Click += new System.EventHandler(this.btnLap_Click);
            this.Controls.Add(this.btnLap);
            this.label4.Name = "label4";
            this.label4.Location = new System.Drawing.Point(10, 53);
            this.label4.AutoSize = true;
            this.label4.Text = "Các bảng lương đã lập:";
            this.label4.TabIndex = 8;
            this.Controls.Add(this.label4);
            this.dgvBL.Name = "dgvBL";
            this.dgvBL.Location = new System.Drawing.Point(10, 72);
            this.dgvBL.Size = new System.Drawing.Size(1220, 140);
            this.dgvBL.AllowUserToAddRows = false;
            this.dgvBL.AllowUserToDeleteRows = false;
            this.dgvBL.ReadOnly = true;
            this.dgvBL.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBL.MultiSelect = false;
            this.dgvBL.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBL.RowHeadersVisible = false;
            this.dgvBL.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvBL.TabIndex = 9;
            this.dgvBL.SelectionChanged += new System.EventHandler(this.dgvBL_SelectionChanged);
            this.Controls.Add(this.dgvBL);
            this.label5.Name = "label5";
            this.label5.Location = new System.Drawing.Point(10, 225);
            this.label5.AutoSize = true;
            this.label5.Text = "Chi tiết lương (xem trước hoặc bảng lương đang chọn):";
            this.label5.TabIndex = 10;
            this.Controls.Add(this.label5);
            this.dgvCT.Name = "dgvCT";
            this.dgvCT.Location = new System.Drawing.Point(10, 244);
            this.dgvCT.Size = new System.Drawing.Size(1220, 380);
            this.dgvCT.AllowUserToAddRows = false;
            this.dgvCT.AllowUserToDeleteRows = false;
            this.dgvCT.ReadOnly = true;
            this.dgvCT.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCT.MultiSelect = false;
            this.dgvCT.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvCT.RowHeadersVisible = false;
            this.dgvCT.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvCT.TabIndex = 11;
            this.Controls.Add(this.dgvCT);
            this.btnDong.Name = "btnDong";
            this.btnDong.Location = new System.Drawing.Point(1130, 645);
            this.btnDong.Size = new System.Drawing.Size(100, 28);
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.TabIndex = 12;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            this.Controls.Add(this.btnDong);
            ((System.ComponentModel.ISupportInitialize)(this.numThang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCT)).EndInit();
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1240, 690);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBangLuong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bảng lương hằng tháng";
            this.Load += new System.EventHandler(this.FrmBangLuong_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numThang;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numNam;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNguoiLap;
        private System.Windows.Forms.Button btnXemTruoc;
        private System.Windows.Forms.Button btnLap;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvBL;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvCT;
        private System.Windows.Forms.Button btnDong;
    }
}
