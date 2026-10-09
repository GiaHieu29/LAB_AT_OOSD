
namespace QuanLyCongTyDuLich.Forms
{
    partial class FrmMain
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblTieuDe;

        private System.Windows.Forms.Button btnDanhMuc;
        private System.Windows.Forms.Button btnTour;
        private System.Windows.Forms.Button btnChuyenLe;
        private System.Windows.Forms.Button btnDangKyLe;
        private System.Windows.Forms.Button btnDangKyDoan;
        private System.Windows.Forms.Button btnPhanCong;
        private System.Windows.Forms.Button btnKetThuc;
        private System.Windows.Forms.Button btnThongKe;
        private System.Windows.Forms.Button btnThoat;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlMain = new System.Windows.Forms.Panel();
            this.lblTieuDe = new System.Windows.Forms.Label();

            this.btnDanhMuc = new System.Windows.Forms.Button();
            this.btnTour = new System.Windows.Forms.Button();
            this.btnChuyenLe = new System.Windows.Forms.Button();
            this.btnDangKyLe = new System.Windows.Forms.Button();
            this.btnDangKyDoan = new System.Windows.Forms.Button();
            this.btnPhanCong = new System.Windows.Forms.Button();
            this.btnKetThuc = new System.Windows.Forms.Button();
            this.btnThongKe = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();

            this.pnlMain.SuspendLayout();
            this.SuspendLayout();

            // =========================
            // CẤU HÌNH FORM CHÍNH
            // =========================

            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;

            this.ClientSize = new System.Drawing.Size(492, 370);

            this.FormBorderStyle =
                System.Windows.Forms.FormBorderStyle.FixedSingle;

            this.MaximizeBox = false;

            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;

            this.Name = "FrmMain";

            this.Text = "Quản lý công ty du lịch Văn Hóa Việt";

            this.BackColor = System.Drawing.Color.White;

            this.Font = new System.Drawing.Font(
                "Segoe UI", 9F,
                System.Drawing.FontStyle.Regular);

            // =========================
            // PANEL NỀN
            // =========================

            this.pnlMain.Name = "pnlMain";

            this.pnlMain.Location =
                new System.Drawing.Point(12, 5);

            this.pnlMain.Size =
                new System.Drawing.Size(468, 340);

            this.pnlMain.BackColor =
                System.Drawing.Color.FromArgb(240, 240, 240);

            this.pnlMain.BorderStyle =
                System.Windows.Forms.BorderStyle.FixedSingle;

            // =========================
            // TIÊU ĐỀ
            // =========================

            this.lblTieuDe.Name = "lblTieuDe";

            this.lblTieuDe.Text =
                "CÔNG TY DU LỊCH VĂN HÓA VIỆT";

            this.lblTieuDe.Font = new System.Drawing.Font(
                "Segoe UI", 12F,
                System.Drawing.FontStyle.Bold);

            this.lblTieuDe.ForeColor =
                System.Drawing.Color.FromArgb(24, 67, 111);

            this.lblTieuDe.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;

            this.lblTieuDe.Location =
                new System.Drawing.Point(18, 13);

            this.lblTieuDe.Size =
                new System.Drawing.Size(430, 35);

            // =========================
            // THIẾT KẾ 8 NÚT CHỨC NĂNG
            // =========================

            TaoNut(this.btnDanhMuc, "Danh mục", 40, 54);

            TaoNut(this.btnTour,
                "Tour - hành trình", 240, 54);

            TaoNut(this.btnChuyenLe,
                "Lịch chuyến khách lẻ", 40, 104);

            TaoNut(this.btnDangKyLe,
                "Đăng ký khách lẻ", 240, 104);

            TaoNut(this.btnDangKyDoan,
                "Đăng ký theo đoàn", 40, 154);

            TaoNut(this.btnPhanCong,
                "Phân công hướng dẫn viên", 240, 154);

            TaoNut(this.btnKetThuc,
                "Kết thúc tour - khảo sát", 40, 204);

            TaoNut(this.btnThongKe,
                "Lương - thống kê", 240, 204);

            // =========================
            // NÚT THOÁT
            // =========================

            TaoNut(this.btnThoat, "Thoát", 140, 261);

            // =========================
            // ĐẶT TÊN BUTTON
            // =========================

            this.btnDanhMuc.Name = "btnDanhMuc";
            this.btnTour.Name = "btnTour";
            this.btnChuyenLe.Name = "btnChuyenLe";
            this.btnDangKyLe.Name = "btnDangKyLe";
            this.btnDangKyDoan.Name = "btnDangKyDoan";
            this.btnPhanCong.Name = "btnPhanCong";
            this.btnKetThuc.Name = "btnKetThuc";
            this.btnThongKe.Name = "btnThongKe";
            this.btnThoat.Name = "btnThoat";

            // =========================
            // SỰ KIỆN CLICK
            // =========================

            this.btnDanhMuc.Click +=
                new System.EventHandler(
                    this.btnDanhMuc_Click);

            this.btnTour.Click +=
                new System.EventHandler(
                    this.btnTour_Click);

            this.btnChuyenLe.Click +=
                new System.EventHandler(
                    this.btnChuyenLe_Click);

            this.btnDangKyLe.Click +=
                new System.EventHandler(
                    this.btnDangKyLe_Click);

            this.btnDangKyDoan.Click +=
                new System.EventHandler(
                    this.btnDangKyDoan_Click);

            this.btnPhanCong.Click +=
                new System.EventHandler(
                    this.btnPhanCong_Click);

            this.btnKetThuc.Click +=
                new System.EventHandler(
                    this.btnKetThuc_Click);

            this.btnThongKe.Click +=
                new System.EventHandler(
                    this.btnThongKe_Click);

            this.btnThoat.Click +=
                new System.EventHandler(
                    this.btnThoat_Click);

            // =========================
            // THÊM CÁC CONTROL VÀO PANEL
            // =========================

            this.pnlMain.Controls.Add(this.lblTieuDe);

            this.pnlMain.Controls.Add(this.btnDanhMuc);
            this.pnlMain.Controls.Add(this.btnTour);
            this.pnlMain.Controls.Add(this.btnChuyenLe);
            this.pnlMain.Controls.Add(this.btnDangKyLe);
            this.pnlMain.Controls.Add(this.btnDangKyDoan);
            this.pnlMain.Controls.Add(this.btnPhanCong);
            this.pnlMain.Controls.Add(this.btnKetThuc);
            this.pnlMain.Controls.Add(this.btnThongKe);
            this.pnlMain.Controls.Add(this.btnThoat);

            this.Controls.Add(this.pnlMain);

            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // =========================
        // HÀM THIẾT KẾ NÚT
        // =========================

        private void TaoNut(
            System.Windows.Forms.Button btn,
            string noiDung,
            int x,
            int y)
        {
            btn.Text = noiDung;

            btn.Location =
                new System.Drawing.Point(x, y);

            btn.Size =
                new System.Drawing.Size(186, 38);

            btn.Font = new System.Drawing.Font(
                "Segoe UI", 9F,
                System.Drawing.FontStyle.Regular);

            btn.ForeColor =
                System.Drawing.Color.Black;

            btn.BackColor =
                System.Drawing.Color.FromArgb(225, 225, 225);

            btn.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            btn.FlatAppearance.BorderSize = 1;

            btn.FlatAppearance.BorderColor =
                System.Drawing.Color.FromArgb(210, 210, 210);

            btn.UseVisualStyleBackColor = false;

            btn.Cursor =
                System.Windows.Forms.Cursors.Hand;

            btn.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;
        }
    }
}
