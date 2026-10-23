
namespace QuanLyHoSoBenhAn
{
    partial class FrmMain
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTieuDe;

        private System.Windows.Forms.Button btnBenhNhan;
        private System.Windows.Forms.Button btnHoSo;
        private System.Windows.Forms.Button btnPhieuKham;
        private System.Windows.Forms.Button btnXetNghiem;
        private System.Windows.Forms.Button btnToaThuoc;
        private System.Windows.Forms.Button btnLichHen;
        private System.Windows.Forms.Button btnThanhToan;
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

            this.btnBenhNhan = new System.Windows.Forms.Button();
            this.btnHoSo = new System.Windows.Forms.Button();
            this.btnPhieuKham = new System.Windows.Forms.Button();
            this.btnXetNghiem = new System.Windows.Forms.Button();
            this.btnToaThuoc = new System.Windows.Forms.Button();
            this.btnLichHen = new System.Windows.Forms.Button();
            this.btnThanhToan = new System.Windows.Forms.Button();
            this.btnThongKe = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // FORM CHÍNH
            this.ClientSize = new System.Drawing.Size(880, 510);
            this.Name = "FrmMain";
            this.Text = "Quản lý hồ sơ bệnh án";

            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;

            this.FormBorderStyle =
                System.Windows.Forms.FormBorderStyle.FixedSingle;

            this.MaximizeBox = false;

            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;

            // TIÊU ĐỀ
            this.lblTieuDe.Text =
                "HỆ THỐNG QUẢN LÝ HỒ SƠ BỆNH ÁN";

            this.lblTieuDe.Font = new System.Drawing.Font(
                "Segoe UI", 17F,
                System.Drawing.FontStyle.Bold);

            this.lblTieuDe.ForeColor =
                System.Drawing.Color.FromArgb(35, 75, 125);

            this.lblTieuDe.Location =
                new System.Drawing.Point(30, 35);

            this.lblTieuDe.Size =
                new System.Drawing.Size(820, 45);

            this.lblTieuDe.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;

            // HÀNG 1
            CaiDatNut(btnBenhNhan,
                "Quản lý bệnh nhân", 50, 130);

            CaiDatNut(btnHoSo,
                "Hồ sơ bệnh án", 325, 130);

            CaiDatNut(btnPhieuKham,
                "Phiếu khám bệnh", 600, 130);

            // HÀNG 2
            CaiDatNut(btnXetNghiem,
                "Quản lý xét nghiệm", 50, 210);

            CaiDatNut(btnToaThuoc,
                "Quản lý toa thuốc", 325, 210);

            CaiDatNut(btnLichHen,
                "Lịch hẹn khám", 600, 210);

            // HÀNG 3
            CaiDatNut(btnThanhToan,
                "Thanh toán viện phí", 50, 290);

            CaiDatNut(btnThongKe,
                "Thống kê báo cáo", 325, 290);

            // THOÁT
            CaiDatNut(btnThoat,
                "Thoát", 325, 390);

            // SỰ KIỆN
            btnBenhNhan.Click +=
                new System.EventHandler(btnBenhNhan_Click);

            btnHoSo.Click +=
                new System.EventHandler(btnHoSo_Click);

            btnPhieuKham.Click +=
                new System.EventHandler(btnPhieuKham_Click);

            btnXetNghiem.Click +=
                new System.EventHandler(btnXetNghiem_Click);

            btnToaThuoc.Click +=
                new System.EventHandler(btnToaThuoc_Click);

            btnLichHen.Click +=
                new System.EventHandler(btnLichHen_Click);

            btnThanhToan.Click +=
                new System.EventHandler(btnThanhToan_Click);

            btnThongKe.Click +=
                new System.EventHandler(btnThongKe_Click);

            btnThoat.Click +=
                new System.EventHandler(btnThoat_Click);

            this.Controls.Add(lblTieuDe);

            this.Controls.Add(btnBenhNhan);
            this.Controls.Add(btnHoSo);
            this.Controls.Add(btnPhieuKham);
            this.Controls.Add(btnXetNghiem);
            this.Controls.Add(btnToaThuoc);
            this.Controls.Add(btnLichHen);
            this.Controls.Add(btnThanhToan);
            this.Controls.Add(btnThongKe);
            this.Controls.Add(btnThoat);

            this.ResumeLayout(false);
        }

        private void CaiDatNut(
            System.Windows.Forms.Button btn,
            string text, int x, int y)
        {
            btn.Text = text;

            btn.Location = new System.Drawing.Point(x, y);
            btn.Size = new System.Drawing.Size(230, 55);

            btn.BackColor = System.Drawing.Color.White;
            btn.ForeColor = System.Drawing.Color.Black;

            btn.FlatStyle =
                System.Windows.Forms.FlatStyle.Standard;

            btn.Cursor = System.Windows.Forms.Cursors.Hand;
        }
    }
}
