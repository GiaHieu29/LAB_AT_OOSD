namespace QuanLyNhanSu.Forms
{
    partial class FrmMain
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
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.btnDanhMuc = new System.Windows.Forms.Button();
            this.btnNhanVien = new System.Windows.Forms.Button();
            this.btnTuyenDung = new System.Windows.Forms.Button();
            this.btnHopDong = new System.Windows.Forms.Button();
            this.btnQuyetDinh = new System.Windows.Forms.Button();
            this.btnPhanCong = new System.Windows.Forms.Button();
            this.btnChamCong = new System.Windows.Forms.Button();
            this.btnPhuTroi = new System.Windows.Forms.Button();
            this.btnBangLuong = new System.Windows.Forms.Button();
            this.btnKhenThuong = new System.Windows.Forms.Button();
            this.btnKyLuat = new System.Windows.Forms.Button();
            this.btnThongKe = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Location = new System.Drawing.Point(0, 25);
            this.lblTieuDe.Size = new System.Drawing.Size(640, 40);
            this.lblTieuDe.AutoSize = false;
            this.lblTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTieuDe.Text = "QUẢN LÝ NHÂN SỰ - CÔNG TY SƠN ĐÔNG Á";
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.TabIndex = 0;
            this.Controls.Add(this.lblTieuDe);
            this.btnDanhMuc.Name = "btnDanhMuc";
            this.btnDanhMuc.Location = new System.Drawing.Point(30, 85);
            this.btnDanhMuc.Size = new System.Drawing.Size(180, 55);
            this.btnDanhMuc.Text = "Danh mục";
            this.btnDanhMuc.UseVisualStyleBackColor = true;
            this.btnDanhMuc.TabIndex = 1;
            this.btnDanhMuc.Click += new System.EventHandler(this.btnDanhMuc_Click);
            this.Controls.Add(this.btnDanhMuc);
            this.btnNhanVien.Name = "btnNhanVien";
            this.btnNhanVien.Location = new System.Drawing.Point(230, 85);
            this.btnNhanVien.Size = new System.Drawing.Size(180, 55);
            this.btnNhanVien.Text = "Hồ sơ nhân viên";
            this.btnNhanVien.UseVisualStyleBackColor = true;
            this.btnNhanVien.TabIndex = 2;
            this.btnNhanVien.Click += new System.EventHandler(this.btnNhanVien_Click);
            this.Controls.Add(this.btnNhanVien);
            this.btnTuyenDung.Name = "btnTuyenDung";
            this.btnTuyenDung.Location = new System.Drawing.Point(430, 85);
            this.btnTuyenDung.Size = new System.Drawing.Size(180, 55);
            this.btnTuyenDung.Text = "Quyết định tuyển dụng";
            this.btnTuyenDung.UseVisualStyleBackColor = true;
            this.btnTuyenDung.TabIndex = 3;
            this.btnTuyenDung.Click += new System.EventHandler(this.btnTuyenDung_Click);
            this.Controls.Add(this.btnTuyenDung);
            this.btnHopDong.Name = "btnHopDong";
            this.btnHopDong.Location = new System.Drawing.Point(30, 160);
            this.btnHopDong.Size = new System.Drawing.Size(180, 55);
            this.btnHopDong.Text = "Hợp đồng và bảo hiểm";
            this.btnHopDong.UseVisualStyleBackColor = true;
            this.btnHopDong.TabIndex = 4;
            this.btnHopDong.Click += new System.EventHandler(this.btnHopDong_Click);
            this.Controls.Add(this.btnHopDong);
            this.btnQuyetDinh.Name = "btnQuyetDinh";
            this.btnQuyetDinh.Location = new System.Drawing.Point(230, 160);
            this.btnQuyetDinh.Size = new System.Drawing.Size(180, 55);
            this.btnQuyetDinh.Text = "Quyết định chức vụ - lương";
            this.btnQuyetDinh.UseVisualStyleBackColor = true;
            this.btnQuyetDinh.TabIndex = 5;
            this.btnQuyetDinh.Click += new System.EventHandler(this.btnQuyetDinh_Click);
            this.Controls.Add(this.btnQuyetDinh);
            this.btnPhanCong.Name = "btnPhanCong";
            this.btnPhanCong.Location = new System.Drawing.Point(430, 160);
            this.btnPhanCong.Size = new System.Drawing.Size(180, 55);
            this.btnPhanCong.Text = "Kế hoạch phân công tuần";
            this.btnPhanCong.UseVisualStyleBackColor = true;
            this.btnPhanCong.TabIndex = 6;
            this.btnPhanCong.Click += new System.EventHandler(this.btnPhanCong_Click);
            this.Controls.Add(this.btnPhanCong);
            this.btnChamCong.Name = "btnChamCong";
            this.btnChamCong.Location = new System.Drawing.Point(30, 235);
            this.btnChamCong.Size = new System.Drawing.Size(180, 55);
            this.btnChamCong.Text = "Chấm công";
            this.btnChamCong.UseVisualStyleBackColor = true;
            this.btnChamCong.TabIndex = 7;
            this.btnChamCong.Click += new System.EventHandler(this.btnChamCong_Click);
            this.Controls.Add(this.btnChamCong);
            this.btnPhuTroi.Name = "btnPhuTroi";
            this.btnPhuTroi.Location = new System.Drawing.Point(230, 235);
            this.btnPhuTroi.Size = new System.Drawing.Size(180, 55);
            this.btnPhuTroi.Text = "Phiếu phụ trội";
            this.btnPhuTroi.UseVisualStyleBackColor = true;
            this.btnPhuTroi.TabIndex = 8;
            this.btnPhuTroi.Click += new System.EventHandler(this.btnPhuTroi_Click);
            this.Controls.Add(this.btnPhuTroi);
            this.btnBangLuong.Name = "btnBangLuong";
            this.btnBangLuong.Location = new System.Drawing.Point(430, 235);
            this.btnBangLuong.Size = new System.Drawing.Size(180, 55);
            this.btnBangLuong.Text = "Bảng lương";
            this.btnBangLuong.UseVisualStyleBackColor = true;
            this.btnBangLuong.TabIndex = 9;
            this.btnBangLuong.Click += new System.EventHandler(this.btnBangLuong_Click);
            this.Controls.Add(this.btnBangLuong);
            this.btnKhenThuong.Name = "btnKhenThuong";
            this.btnKhenThuong.Location = new System.Drawing.Point(30, 310);
            this.btnKhenThuong.Size = new System.Drawing.Size(180, 55);
            this.btnKhenThuong.Text = "Khen thưởng";
            this.btnKhenThuong.UseVisualStyleBackColor = true;
            this.btnKhenThuong.TabIndex = 10;
            this.btnKhenThuong.Click += new System.EventHandler(this.btnKhenThuong_Click);
            this.Controls.Add(this.btnKhenThuong);
            this.btnKyLuat.Name = "btnKyLuat";
            this.btnKyLuat.Location = new System.Drawing.Point(230, 310);
            this.btnKyLuat.Size = new System.Drawing.Size(180, 55);
            this.btnKyLuat.Text = "Kỷ luật";
            this.btnKyLuat.UseVisualStyleBackColor = true;
            this.btnKyLuat.TabIndex = 11;
            this.btnKyLuat.Click += new System.EventHandler(this.btnKyLuat_Click);
            this.Controls.Add(this.btnKyLuat);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Location = new System.Drawing.Point(430, 310);
            this.btnThongKe.Size = new System.Drawing.Size(180, 55);
            this.btnThongKe.Text = "Thống kê";
            this.btnThongKe.UseVisualStyleBackColor = true;
            this.btnThongKe.TabIndex = 12;
            this.btnThongKe.Click += new System.EventHandler(this.btnThongKe_Click);
            this.Controls.Add(this.btnThongKe);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Location = new System.Drawing.Point(30, 385);
            this.btnThoat.Size = new System.Drawing.Size(180, 55);
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.TabIndex = 13;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            this.Controls.Add(this.btnThoat);
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 480);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Công ty sơn Đông Á - Quản lý nhân sự";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Button btnDanhMuc;
        private System.Windows.Forms.Button btnNhanVien;
        private System.Windows.Forms.Button btnTuyenDung;
        private System.Windows.Forms.Button btnHopDong;
        private System.Windows.Forms.Button btnQuyetDinh;
        private System.Windows.Forms.Button btnPhanCong;
        private System.Windows.Forms.Button btnChamCong;
        private System.Windows.Forms.Button btnPhuTroi;
        private System.Windows.Forms.Button btnBangLuong;
        private System.Windows.Forms.Button btnKhenThuong;
        private System.Windows.Forms.Button btnKyLuat;
        private System.Windows.Forms.Button btnThongKe;
        private System.Windows.Forms.Button btnThoat;
    }
}
