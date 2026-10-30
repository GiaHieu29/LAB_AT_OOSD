
namespace QuanLyChoThueVanPhong
{
    partial class FrmMain
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblThongTin;

        private System.Windows.Forms.Button btnDanhMuc;
        private System.Windows.Forms.Button btnKhachHang;
        private System.Windows.Forms.Button btnHopDong;
        private System.Windows.Forms.Button btnGiaHan;
        private System.Windows.Forms.Button btnThanhToan;
        private System.Windows.Forms.Button btnDichVu;
        private System.Windows.Forms.Button btnNhanVien;
        private System.Windows.Forms.Button btnTheXe;
        private System.Windows.Forms.Button btnKiemTra;
        private System.Windows.Forms.Button btnViPham;
        private System.Windows.Forms.Button btnThongKe;
        private System.Windows.Forms.Button btnThoat;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.lblThongTin = new System.Windows.Forms.Label();

            this.btnDanhMuc = new System.Windows.Forms.Button();
            this.btnKhachHang = new System.Windows.Forms.Button();
            this.btnHopDong = new System.Windows.Forms.Button();
            this.btnGiaHan = new System.Windows.Forms.Button();
            this.btnThanhToan = new System.Windows.Forms.Button();
            this.btnDichVu = new System.Windows.Forms.Button();
            this.btnNhanVien = new System.Windows.Forms.Button();
            this.btnTheXe = new System.Windows.Forms.Button();
            this.btnKiemTra = new System.Windows.Forms.Button();
            this.btnViPham = new System.Windows.Forms.Button();
            this.btnThongKe = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // =========================================
            // THÔNG TIN FORM
            // =========================================
            this.Name = "FrmMain";
            this.Text = "Quản lý cho thuê văn phòng cao ốc";

            this.ClientSize =
                new System.Drawing.Size(900, 660);

            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;

            this.FormBorderStyle =
                System.Windows.Forms.FormBorderStyle.FixedSingle;

            this.MaximizeBox = false;

            this.Font =
                new System.Drawing.Font("Segoe UI", 10F);

            this.BackColor =
                System.Drawing.Color.WhiteSmoke;

            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;

            // =========================================
            // TIÊU ĐỀ
            // =========================================
            this.lblTieuDe.Text =
                "HỆ THỐNG QUẢN LÝ CHO THUÊ VĂN PHÒNG CAO ỐC";

            this.lblTieuDe.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    17F,
                    System.Drawing.FontStyle.Bold);

            this.lblTieuDe.ForeColor =
                System.Drawing.Color.FromArgb(35, 75, 125);

            this.lblTieuDe.Location =
                new System.Drawing.Point(20, 35);

            this.lblTieuDe.Size =
                new System.Drawing.Size(860, 50);

            this.lblTieuDe.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;

            // =========================================
            // HÀNG 1
            // =========================================
            CaiDatNut(btnDanhMuc,
                "Danh mục", 45, 135);

            CaiDatNut(btnKhachHang,
                "Khách hàng - Công ty", 335, 135);

            CaiDatNut(btnHopDong,
                "Hợp đồng thuê", 625, 135);

            // =========================================
            // HÀNG 2
            // =========================================
            CaiDatNut(btnGiaHan,
                "Gia hạn hợp đồng", 45, 220);

            CaiDatNut(btnThanhToan,
                "Thanh toán tiền thuê", 335, 220);

            CaiDatNut(btnDichVu,
                "Dịch vụ - Hóa đơn", 625, 220);

            // =========================================
            // HÀNG 3
            // =========================================
            CaiDatNut(btnNhanVien,
                "Nhân viên văn phòng", 45, 305);

            CaiDatNut(btnTheXe,
                "Thẻ giữ xe", 335, 305);

            CaiDatNut(btnKiemTra,
                "Kiểm tra phòng", 625, 305);

            // =========================================
            // HÀNG 4
            // =========================================
            CaiDatNut(btnViPham,
                "Vi phạm - Sửa chữa", 45, 390);

            CaiDatNut(btnThongKe,
                "Thống kê báo cáo", 335, 390);

            // =========================================
            // HÀNG 5 - THOÁT
            // =========================================
            CaiDatNut(btnThoat,
                "Thoát", 335, 480);

            // =========================================
            // SỰ KIỆN NÚT BẤM
            // =========================================
            this.btnDanhMuc.Click +=
                new System.EventHandler(
                    this.btnDanhMuc_Click);

            this.btnKhachHang.Click +=
                new System.EventHandler(
                    this.btnKhachHang_Click);

            this.btnHopDong.Click +=
                new System.EventHandler(
                    this.btnHopDong_Click);

            this.btnGiaHan.Click +=
                new System.EventHandler(
                    this.btnGiaHan_Click);

            this.btnThanhToan.Click +=
                new System.EventHandler(
                    this.btnThanhToan_Click);

            this.btnDichVu.Click +=
                new System.EventHandler(
                    this.btnDichVu_Click);

            this.btnNhanVien.Click +=
                new System.EventHandler(
                    this.btnNhanVien_Click);

            this.btnTheXe.Click +=
                new System.EventHandler(
                    this.btnTheXe_Click);

            this.btnKiemTra.Click +=
                new System.EventHandler(
                    this.btnKiemTra_Click);

            this.btnViPham.Click +=
                new System.EventHandler(
                    this.btnViPham_Click);

            this.btnThongKe.Click +=
                new System.EventHandler(
                    this.btnThongKe_Click);

            this.btnThoat.Click +=
                new System.EventHandler(
                    this.btnThoat_Click);

            // =========================================
            // TRẠNG THÁI KẾT NỐI
            // =========================================
            this.lblThongTin.Text =
                "Đang kiểm tra kết nối SQL Server...";

            this.lblThongTin.Location =
                new System.Drawing.Point(45, 580);

            this.lblThongTin.Size =
                new System.Drawing.Size(810, 30);

            this.lblThongTin.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;

            this.lblThongTin.ForeColor =
                System.Drawing.Color.DimGray;

            // =========================================
            // THÊM CONTROL VÀO FORM
            // =========================================
            this.Controls.Add(this.lblTieuDe);
            this.Controls.Add(this.lblThongTin);

            this.Controls.Add(this.btnDanhMuc);
            this.Controls.Add(this.btnKhachHang);
            this.Controls.Add(this.btnHopDong);

            this.Controls.Add(this.btnGiaHan);
            this.Controls.Add(this.btnThanhToan);
            this.Controls.Add(this.btnDichVu);

            this.Controls.Add(this.btnNhanVien);
            this.Controls.Add(this.btnTheXe);
            this.Controls.Add(this.btnKiemTra);

            this.Controls.Add(this.btnViPham);
            this.Controls.Add(this.btnThongKe);
            this.Controls.Add(this.btnThoat);

            this.Load +=
                new System.EventHandler(this.FrmMain_Load);

            this.ResumeLayout(false);
        }

        // =========================================
        // CẤU HÌNH CHUNG CHO CÁC NÚT
        // =========================================
        private void CaiDatNut(
            System.Windows.Forms.Button btn,
            string noiDung,
            int x,
            int y)
        {
            btn.Text = noiDung;

            btn.Location =
                new System.Drawing.Point(x, y);

            btn.Size =
                new System.Drawing.Size(235, 60);

            btn.BackColor =
                System.Drawing.Color.White;

            btn.ForeColor =
                System.Drawing.Color.Black;

            btn.FlatStyle =
                System.Windows.Forms.FlatStyle.Standard;

            btn.Cursor =
                System.Windows.Forms.Cursors.Hand;

            btn.TabStop = true;
        }
    }
}
