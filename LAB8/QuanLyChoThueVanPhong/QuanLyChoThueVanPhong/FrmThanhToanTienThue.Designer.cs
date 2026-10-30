
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyChoThueVanPhong
{
    partial class FrmThanhToanTienThue
    {
        private System.ComponentModel.IContainer components = null;

        private ComboBox cboHopDong;
        private ComboBox cboPhong;
        private ComboBox cboPhuongThuc;

        private TextBox txtMaThanhToan;
        private TextBox txtTimKiem;

        private DateTimePicker dtNgayThu;
        private DateTimePicker dtTuNgay;
        private DateTimePicker dtDenNgay;

        private NumericUpDown nudSoThang;
        private NumericUpDown nudSoTien;

        private Button btnThuTien;
        private Button btnLamMoi;
        private Button btnDong;
        private Button btnTimKiem;

        private DataGridView dgvKy;
        private DataGridView dgvLichSu;

        private Label lblPhaiThu;
        private Label lblDaThu;
        private Label lblConNo;
        private Label lblThongBao;
        private Label lblSoPhieu;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private Label TaoNhan(
            string text, int x, int y, int width = 140)
        {
            return new Label
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(width, 28),
                TextAlign = ContentAlignment.MiddleLeft
            };
        }

        private TextBox TaoText(
            int x, int y, int width)
        {
            return new TextBox
            {
                Location = new Point(x, y),
                Size = new Size(width, 29)
            };
        }

        private Button TaoNut(
            string text, int x, int y, int width)
        {
            return new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(width, 40),
                Cursor = Cursors.Hand
            };
        }

        private DateTimePicker TaoNgay(
            int x, int y, int width)
        {
            return new DateTimePicker
            {
                Location = new Point(x, y),
                Size = new Size(width, 29),
                Format = DateTimePickerFormat.Short
            };
        }

        private DataGridView TaoBang()
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
                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill,
                AutoGenerateColumns = true,
                ColumnHeadersHeight = 36
            };
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // ====================================
            // THÔNG TIN FORM
            // ====================================

            this.Name = "FrmThanhToanTienThue";
            this.Text = "Thanh toán tiền thuê văn phòng";
            this.ClientSize = new Size(1180, 860);
            this.MinimumSize = new Size(1100, 800);
            this.StartPosition =
                FormStartPosition.CenterParent;

            this.Font = new Font("Segoe UI", 10F);
            this.BackColor = Color.WhiteSmoke;
            this.AutoScaleMode = AutoScaleMode.Font;

            // ====================================
            // TIÊU ĐỀ
            // ====================================

            Label lblTitle = TaoNhan(
                "QUẢN LÝ THANH TOÁN TIỀN THUÊ VĂN PHÒNG",
                20, 10, 1140);

            lblTitle.Height = 45;
            lblTitle.Font = new Font(
                "Segoe UI", 17F, FontStyle.Bold);

            lblTitle.ForeColor =
                Color.FromArgb(35, 75, 125);

            lblTitle.TextAlign =
                ContentAlignment.MiddleCenter;

            this.Controls.Add(lblTitle);

            // ====================================
            // GROUP THÔNG TIN THANH TOÁN
            // ====================================

            GroupBox grpThongTin = new GroupBox
            {
                Text = "Thông tin thanh toán tiền thuê",
                Location = new Point(20, 65),
                Size = new Size(1140, 250),
                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Left |
                    AnchorStyles.Right
            };

            this.Controls.Add(grpThongTin);

            cboHopDong = new ComboBox
            {
                Location = new Point(175, 30),
                Size = new Size(320, 29),
                DropDownStyle =
                    ComboBoxStyle.DropDownList
            };

            cboPhong = new ComboBox
            {
                Location = new Point(720, 30),
                Size = new Size(380, 29),
                DropDownStyle =
                    ComboBoxStyle.DropDownList
            };

            txtMaThanhToan =
                TaoText(175, 77, 320);

            txtMaThanhToan.MaxLength = 20;

            dtNgayThu = TaoNgay(720, 77, 380);

            dtTuNgay = TaoNgay(175, 124, 320);
            dtDenNgay = TaoNgay(720, 124, 380);

            dtTuNgay.Enabled = false;
            dtDenNgay.Enabled = false;

            nudSoThang = new NumericUpDown
            {
                Location = new Point(175, 171),
                Size = new Size(320, 29),
                Minimum = 1,
                Maximum = 600,
                ReadOnly = true,
                Enabled = false
            };

            nudSoTien = new NumericUpDown
            {
                Location = new Point(720, 171),
                Size = new Size(380, 29),
                Minimum = 0,
                Maximum = 1000000000000M,
                ThousandsSeparator = true,
                DecimalPlaces = 2,
                ReadOnly = true,
                Enabled = false
            };

            cboPhuongThuc = new ComboBox
            {
                Location = new Point(175, 215),
                Size = new Size(320, 29),
                DropDownStyle =
                    ComboBoxStyle.DropDownList
            };

            grpThongTin.Controls.AddRange(new Control[]
            {
                TaoNhan("Hợp đồng:", 20, 32),
                cboHopDong,

                TaoNhan("Phòng thuê:", 560, 32),
                cboPhong,

                TaoNhan("Mã thanh toán:", 20, 79),
                txtMaThanhToan,

                TaoNhan("Ngày thu:", 560, 79),
                dtNgayThu,

                TaoNhan("Từ ngày:", 20, 126),
                dtTuNgay,

                TaoNhan("Đến ngày:", 560, 126),
                dtDenNgay,

                TaoNhan("Số tháng thu:", 20, 173),
                nudSoThang,

                TaoNhan("Số tiền:", 560, 173),
                nudSoTien,

                TaoNhan("Phương thức:", 20, 217),
                cboPhuongThuc
            });

            cboHopDong.SelectedIndexChanged +=
                cboHopDong_SelectedIndexChanged;

            cboPhong.SelectedIndexChanged +=
                cboPhong_SelectedIndexChanged;

            // ====================================
            // THÔNG TIN CÔNG NỢ
            // ====================================

            lblPhaiThu = TaoNhan(
                "Tổng phải thu: 0 đ",
                25, 325, 370);

            lblDaThu = TaoNhan(
                "Đã thu: 0 đ",
                410, 325, 350);

            lblConNo = TaoNhan(
                "Nợ đến hạn: 0 đ",
                780, 325, 370);

            lblPhaiThu.Font = new Font(
                "Segoe UI", 10F, FontStyle.Bold);

            lblDaThu.Font = new Font(
                "Segoe UI", 10F, FontStyle.Bold);

            lblConNo.Font = new Font(
                "Segoe UI", 10F, FontStyle.Bold);

            this.Controls.Add(lblPhaiThu);
            this.Controls.Add(lblDaThu);
            this.Controls.Add(lblConNo);

            lblThongBao = TaoNhan(
                "Chọn một kỳ chưa thanh toán trong bảng.",
                25, 360, 1120);

            lblThongBao.ForeColor = Color.DarkBlue;

            this.Controls.Add(lblThongBao);

            // ====================================
            // NÚT CHỨC NĂNG
            // ====================================

            btnThuTien = TaoNut(
                "Thu tiền", 345, 400, 150);

            btnLamMoi = TaoNut(
                "Làm mới", 515, 400, 150);

            btnDong = TaoNut(
                "Đóng", 685, 400, 150);

            btnThuTien.Enabled = false;

            btnThuTien.Click += btnThuTien_Click;
            btnLamMoi.Click += btnLamMoi_Click;
            btnDong.Click += btnDong_Click;

            this.Controls.AddRange(new Control[]
            {
                btnThuTien,
                btnLamMoi,
                btnDong
            });

            // ====================================
            // BẢNG CÁC KỲ THANH TOÁN
            // ====================================

            GroupBox grpKy = new GroupBox
            {
                Text = "Các kỳ thuê và tình trạng thanh toán",
                Location = new Point(20, 455),
                Size = new Size(1140, 170),
                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Left |
                    AnchorStyles.Right
            };

            dgvKy = TaoBang();

            Panel panelKy = new Panel
            {
                Location = new Point(15, 25),
                Size = new Size(1110, 130),
                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Bottom |
                    AnchorStyles.Left |
                    AnchorStyles.Right
            };

            panelKy.Controls.Add(dgvKy);
            grpKy.Controls.Add(panelKy);

            dgvKy.SelectionChanged +=
                dgvKy_SelectionChanged;

            this.Controls.Add(grpKy);

            // ====================================
            // LỊCH SỬ THANH TOÁN
            // ====================================

            GroupBox grpLichSu = new GroupBox
            {
                Text = "Lịch sử thanh toán",
                Location = new Point(20, 635),
                Size = new Size(1140, 210),
                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Bottom |
                    AnchorStyles.Left |
                    AnchorStyles.Right
            };

            grpLichSu.Controls.Add(
                TaoNhan("Tìm kiếm:", 15, 30, 100));

            txtTimKiem =
                TaoText(115, 27, 330);

            txtTimKiem.KeyDown +=
                txtTimKiem_KeyDown;

            btnTimKiem =
                TaoNut("Tìm kiếm", 465, 25, 130);

            btnTimKiem.Click +=
                btnTimKiem_Click;

            lblSoPhieu = TaoNhan(
                "Lịch sử: 0 phiếu",
                840, 30, 275);

            lblSoPhieu.TextAlign =
                ContentAlignment.MiddleRight;

            grpLichSu.Controls.AddRange(
                new Control[]
                {
                    txtTimKiem,
                    btnTimKiem,
                    lblSoPhieu
                });

            Panel panelLS = new Panel
            {
                Location = new Point(15, 75),
                Size = new Size(1110, 120),
                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Bottom |
                    AnchorStyles.Left |
                    AnchorStyles.Right
            };

            dgvLichSu = TaoBang();

            panelLS.Controls.Add(dgvLichSu);
            grpLichSu.Controls.Add(panelLS);

            this.Controls.Add(grpLichSu);

            // ====================================
            // LOAD FORM
            // ====================================

            this.Load +=
                FrmThanhToanTienThue_Load;

            this.ResumeLayout(false);
        }
    }
}
