
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyChoThueVanPhong
{
    partial class FrmDichVu
    {
        private System.ComponentModel.IContainer components = null;

        private TabControl tabMain;

        private TabPage tabSuDung;
        private TabPage tabHoaDon;
        private TabPage tabThanhToan;

        private ComboBox cboHopDong;
        private ComboBox cboPhong;
        private ComboBox cboLoaiDV;
        private ComboBox cboPhuongThuc;

        private NumericUpDown numThang;
        private NumericUpDown numNam;
        private NumericUpDown numSoLuong;
        private NumericUpDown numTienThu;

        private TextBox txtMaSuDung;
        private TextBox txtMaHoaDon;
        private TextBox txtDonGia;
        private TextBox txtHoaDonChon;
        private TextBox txtMaThu;
        private TextBox txtGhiChu;

        private DateTimePicker dtNgayThu;

        private DataGridView dgvSuDung;
        private DataGridView dgvHoaDon;
        private DataGridView dgvChiTiet;
        private DataGridView dgvThanhToan;

        private Button btnGhiSuDung;
        private Button btnLapHoaDon;
        private Button btnThu;
        private Button btnLamMoi;
        private Button btnDong;

        private Label lblCongNo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        // ==========================================
        // CÁC HÀM TẠO GIAO DIỆN
        // ==========================================

        private Label L(
            string s, int x, int y, int width = 135)
        {
            return new Label
            {
                Text = s,
                Location = new Point(x, y),
                Size = new Size(width, 28),
                TextAlign = ContentAlignment.MiddleLeft
            };
        }

        private TextBox T(
            int x, int y, int width, int max = 100)
        {
            return new TextBox
            {
                Location = new Point(x, y),
                Size = new Size(width, 29),
                MaxLength = max
            };
        }

        private Button B(
            string s, int x, int y, int width = 150)
        {
            return new Button
            {
                Text = s,
                Location = new Point(x, y),
                Size = new Size(width, 38),
                Cursor = Cursors.Hand
            };
        }

        private ComboBox C(int x, int y, int width)
        {
            return new ComboBox
            {
                Location = new Point(x, y),
                Size = new Size(width, 29),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
        }

        private NumericUpDown N(
            int x, int y, int width,
            decimal min, decimal max, int decimals = 0)
        {
            return new NumericUpDown
            {
                Location = new Point(x, y),
                Size = new Size(width, 29),
                Minimum = min,
                Maximum = max,
                DecimalPlaces = decimals,
                ThousandsSeparator = true
            };
        }

        private DataGridView G()
        {
            return new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                MultiSelect = false,
                SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect,
                AutoGenerateColumns = true,
                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill,
                ColumnHeadersHeight = 36
            };
        }

        private Panel GridPanel(
            int x, int y, int width, int height,
            DataGridView grid)
        {
            Panel panel = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(width, height),
                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Bottom |
                    AnchorStyles.Left |
                    AnchorStyles.Right
            };

            panel.Controls.Add(grid);
            return panel;
        }

        private void InitializeComponent()
        {
            SuspendLayout();

            // ====================================
            // FORM CHÍNH
            // ====================================

            Name = "FrmDichVu";

            Text = "Quản lý dịch vụ và hóa đơn - Lab 08";

            ClientSize = new Size(1150, 780);
            MinimumSize = new Size(1050, 710);

            StartPosition =
                FormStartPosition.CenterParent;

            BackColor = Color.WhiteSmoke;
            Font = new Font("Segoe UI", 10F);
            AutoScaleMode = AutoScaleMode.Font;

            Label title = L(
                "QUẢN LÝ DỊCH VỤ - HÓA ĐƠN - CÔNG NỢ",
                20, 12, 1100);

            title.Size = new Size(1100, 45);
            title.TextAlign =
                ContentAlignment.MiddleCenter;

            title.Font = new Font(
                "Segoe UI", 17F, FontStyle.Bold);

            title.ForeColor =
                Color.FromArgb(35, 75, 125);

            Controls.Add(title);

            // ====================================
            // CHỌN HỢP ĐỒNG VÀ KỲ DỊCH VỤ
            // ====================================

            GroupBox gChon = new GroupBox
            {
                Text = "Chọn hợp đồng và kỳ dịch vụ",
                Location = new Point(20, 65),
                Size = new Size(1110, 105),
                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Left |
                    AnchorStyles.Right
            };

            gChon.Controls.Add(
                L("Hợp đồng:", 15, 30, 100));

            cboHopDong = C(115, 28, 350);
            gChon.Controls.Add(cboHopDong);

            gChon.Controls.Add(
                L("Phòng:", 490, 30, 70));

            cboPhong = C(560, 28, 260);
            gChon.Controls.Add(cboPhong);

            gChon.Controls.Add(
                L("Tháng:", 15, 69, 100));

            numThang = N(115, 67, 90, 1, 12);
            gChon.Controls.Add(numThang);

            gChon.Controls.Add(
                L("Năm:", 235, 69, 60));

            numNam = N(295, 67, 110, 2000, 2100);
            gChon.Controls.Add(numNam);

            btnLamMoi = B(
                "Làm mới", 855, 28, 120);

            btnDong = B(
                "Đóng", 985, 28, 105);

            gChon.Controls.Add(btnLamMoi);
            gChon.Controls.Add(btnDong);

            Controls.Add(gChon);

            // ====================================
            // TAB CONTROL
            // ====================================

            tabMain = new TabControl
            {
                Location = new Point(20, 180),
                Size = new Size(1110, 580),
                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Bottom |
                    AnchorStyles.Left |
                    AnchorStyles.Right
            };

            tabSuDung =
                new TabPage("1. Sử dụng dịch vụ");

            tabHoaDon =
                new TabPage("2. Lập và xem hóa đơn");

            tabThanhToan =
                new TabPage("3. Thanh toán và công nợ");

            tabMain.TabPages.AddRange(
                new TabPage[]
                {
                    tabSuDung,
                    tabHoaDon,
                    tabThanhToan
                });

            Controls.Add(tabMain);

            // ====================================
            // TAB 1 - SỬ DỤNG DỊCH VỤ
            // ====================================

            GroupBox gSD = new GroupBox
            {
                Text = "Ghi sử dụng điện, nước, Internet theo phòng",
                Location = new Point(15, 15),
                Size = new Size(1055, 170),
                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Left |
                    AnchorStyles.Right
            };

            gSD.Controls.Add(
                L("Mã sử dụng:", 20, 35, 120));

            txtMaSuDung = T(145, 33, 210, 20);
            gSD.Controls.Add(txtMaSuDung);

            gSD.Controls.Add(
                L("Dịch vụ:", 390, 35, 95));

            cboLoaiDV = C(490, 33, 390);
            gSD.Controls.Add(cboLoaiDV);

            gSD.Controls.Add(
                L("Số lượng:", 20, 85, 120));

            numSoLuong = N(
                145, 83, 210, 0.01m, 1000000m, 2);

            gSD.Controls.Add(numSoLuong);

            gSD.Controls.Add(
                L("Đơn giá hiện tại:", 390, 85, 150));

            txtDonGia = T(540, 83, 240);
            txtDonGia.ReadOnly = true;

            gSD.Controls.Add(txtDonGia);

            btnGhiSuDung = B(
                "Ghi sử dụng", 820, 118, 170);

            gSD.Controls.Add(btnGhiSuDung);

            tabSuDung.Controls.Add(gSD);

            tabSuDung.Controls.Add(
                L("DANH SÁCH SỬ DỤNG DỊCH VỤ TRONG THÁNG",
                  20, 205, 750));

            dgvSuDung = G();

            tabSuDung.Controls.Add(
                GridPanel(
                    20, 240, 1050, 285, dgvSuDung));

            // ====================================
            // TAB 2 - HÓA ĐƠN DỊCH VỤ
            // ====================================

            GroupBox gHD = new GroupBox
            {
                Text = "Lập hóa đơn dịch vụ cuối tháng",
                Location = new Point(15, 15),
                Size = new Size(1055, 90),
                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Left |
                    AnchorStyles.Right
            };

            gHD.Controls.Add(
                L("Mã hóa đơn:", 20, 35, 120));

            txtMaHoaDon = T(145, 33, 265, 20);

            gHD.Controls.Add(txtMaHoaDon);

            btnLapHoaDon = B(
                "Lập hóa đơn", 450, 29, 180);

            gHD.Controls.Add(btnLapHoaDon);

            gHD.Controls.Add(
                L("Chọn hợp đồng + tháng/năm ở phía trên.",
                  650, 35, 390));

            tabHoaDon.Controls.Add(gHD);

            tabHoaDon.Controls.Add(
                L("DANH SÁCH HÓA ĐƠN",
                  20, 115, 500));

            dgvHoaDon = G();

            tabHoaDon.Controls.Add(
                GridPanel(
                    20, 145, 1050, 200, dgvHoaDon));

            tabHoaDon.Controls.Add(
                L("CHI TIẾT HÓA ĐƠN ĐÃ CHỌN",
                  20, 357, 500));

            dgvChiTiet = G();

            tabHoaDon.Controls.Add(
                GridPanel(
                    20, 389, 1050, 135, dgvChiTiet));

            // ====================================
            // TAB 3 - THANH TOÁN VÀ CÔNG NỢ
            // ====================================

            GroupBox gTT = new GroupBox
            {
                Text = "Thu tiền theo hóa đơn đã chọn ở Tab 2",
                Location = new Point(15, 15),
                Size = new Size(1055, 220),
                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Left |
                    AnchorStyles.Right
            };

            gTT.Controls.Add(
                L("Hóa đơn:", 20, 32, 130));

            txtHoaDonChon = T(160, 30, 245, 20);
            txtHoaDonChon.ReadOnly = true;

            gTT.Controls.Add(txtHoaDonChon);

            gTT.Controls.Add(
                L("Mã phiếu thu:", 450, 32, 130));

            txtMaThu = T(585, 30, 285, 20);
            gTT.Controls.Add(txtMaThu);

            gTT.Controls.Add(
                L("Ngày thu:", 20, 77, 130));

            dtNgayThu = new DateTimePicker
            {
                Location = new Point(160, 75),
                Size = new Size(245, 29),
                Format = DateTimePickerFormat.Short
            };

            gTT.Controls.Add(dtNgayThu);

            gTT.Controls.Add(
                L("Số tiền thu:", 450, 77, 130));

            numTienThu = N(
                585, 75, 285, 0.01m, 999999999999m, 2);

            gTT.Controls.Add(numTienThu);

            gTT.Controls.Add(
                L("Phương thức:", 20, 122, 130));

            cboPhuongThuc = C(160, 120, 245);
            gTT.Controls.Add(cboPhuongThuc);

            gTT.Controls.Add(
                L("Ghi chú:", 450, 122, 130));

            txtGhiChu = T(585, 120, 285, 500);
            gTT.Controls.Add(txtGhiChu);

            lblCongNo = L(
                "Công nợ hóa đơn: 0 đ",
                20, 172, 570);

            lblCongNo.Font = new Font(
                "Segoe UI", 11F, FontStyle.Bold);

            gTT.Controls.Add(lblCongNo);

            btnThu = B(
                "Ghi nhận thanh toán",
                690, 164, 260);

            btnThu.Enabled = false;
            gTT.Controls.Add(btnThu);

            tabThanhToan.Controls.Add(gTT);

            tabThanhToan.Controls.Add(
                L("LỊCH SỬ THANH TOÁN CỦA HÓA ĐƠN",
                  20, 247, 700));

            dgvThanhToan = G();

            tabThanhToan.Controls.Add(
                GridPanel(
                    20, 282, 1050, 242, dgvThanhToan));

            // ====================================
            // GÁN SỰ KIỆN
            // ====================================

            cboHopDong.SelectedIndexChanged +=
                cboHopDong_SelectedIndexChanged;

            cboLoaiDV.SelectedIndexChanged +=
                cboLoaiDV_SelectedIndexChanged;

            numThang.ValueChanged +=
                numKy_ValueChanged;

            numNam.ValueChanged +=
                numKy_ValueChanged;

            btnGhiSuDung.Click +=
                btnGhiSuDung_Click;

            btnLapHoaDon.Click +=
                btnLapHoaDon_Click;

            dgvHoaDon.SelectionChanged +=
                dgvHoaDon_SelectionChanged;

            btnThu.Click +=
                btnThu_Click;

            btnLamMoi.Click +=
                btnLamMoi_Click;

            btnDong.Click +=
                btnDong_Click;

            Load += FrmDichVu_Load;

            ResumeLayout(false);
        }
    }
}
