
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyChoThueVanPhong
{
    partial class FrmGiaHan
    {
        private System.ComponentModel.IContainer components = null;

        private TextBox txtMaPhieu, txtGhiChu, txtTimKiem;
        private ComboBox cboHopDong, cboPhong;
        private DateTimePicker dtNgayLap, dtTuNgay, dtDenNgay;
        private NumericUpDown nudSoThang, nudDonGia;

        private Button btnLapPhieu, btnDuyet;
        private Button btnTuChoi, btnHuy;
        private Button btnKiemTraNo, btnLamMoi;
        private Button btnTimKiem, btnDong;

        private DataGridView dgvGiaHan;
        private Label lblGioiHan, lblTrangThai, lblSoLuong;

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

        private TextBox TaoText(int x, int y, int width)
        {
            return new TextBox
            {
                Location = new Point(x, y),
                Size = new Size(width, 28)
            };
        }

        private Button TaoNut(
            string text, int x, int y, int width = 140)
        {
            return new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(width, 40),
                Cursor = Cursors.Hand
            };
        }

        private DateTimePicker TaoNgay(int x, int y, int width)
        {
            return new DateTimePicker
            {
                Location = new Point(x, y),
                Size = new Size(width, 28),
                Format = DateTimePickerFormat.Short
            };
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.Name = "FrmGiaHan";
            this.Text = "Quản lý gia hạn hợp đồng";
            this.ClientSize = new Size(1120, 760);
            this.MinimumSize = new Size(1040, 720);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.WhiteSmoke;
            this.Font = new Font("Segoe UI", 10F);
            this.AutoScaleMode = AutoScaleMode.Font;

            Label title = TaoNhan(
                "QUẢN LÝ GIA HẠN HỢP ĐỒNG THUÊ VĂN PHÒNG",
                20, 10, 1080);

            title.Height = 45;
            title.Font = new Font(
                "Segoe UI", 17F, FontStyle.Bold);
            title.ForeColor =
                Color.FromArgb(35, 75, 125);
            title.TextAlign = ContentAlignment.MiddleCenter;

            this.Controls.Add(title);

            // ====================================
            // THÔNG TIN GIA HẠN
            // ====================================

            GroupBox grp = new GroupBox
            {
                Text = "Thông tin phiếu gia hạn",
                Location = new Point(20, 65),
                Size = new Size(1080, 285)
            };

            this.Controls.Add(grp);

            txtMaPhieu = TaoText(155, 30, 300);
            txtMaPhieu.MaxLength = 20;

            cboHopDong = new ComboBox
            {
                Location = new Point(680, 30),
                Size = new Size(360, 28),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            cboPhong = new ComboBox
            {
                Location = new Point(155, 77),
                Size = new Size(300, 28),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            dtNgayLap = TaoNgay(680, 77, 360);
            dtNgayLap.Enabled = false;

            nudSoThang = new NumericUpDown
            {
                Location = new Point(155, 124),
                Size = new Size(300, 28),
                Minimum = 1,
                Maximum = 300,
                Value = 1
            };

            nudDonGia = new NumericUpDown
            {
                Location = new Point(680, 124),
                Size = new Size(360, 28),
                Minimum = 0,
                Maximum = 100000000000M,
                ThousandsSeparator = true
            };

            dtTuNgay = TaoNgay(155, 171, 300);
            dtDenNgay = TaoNgay(680, 171, 360);

            dtTuNgay.Enabled = false;
            dtDenNgay.Enabled = false;

            txtGhiChu = TaoText(155, 218, 885);
            txtGhiChu.MaxLength = 500;

            grp.Controls.AddRange(new Control[]
            {
                TaoNhan("Mã phiếu:", 20, 32),
                txtMaPhieu,
                TaoNhan("Hợp đồng:", 530, 32),
                cboHopDong,

                TaoNhan("Phòng thuê:", 20, 79),
                cboPhong,
                TaoNhan("Ngày lập:", 530, 79),
                dtNgayLap,

                TaoNhan("Số tháng gia hạn:", 20, 126),
                nudSoThang,
                TaoNhan("Đơn giá/tháng:", 530, 126),
                nudDonGia,

                TaoNhan("Từ ngày:", 20, 173),
                dtTuNgay,
                TaoNhan("Đến ngày:", 530, 173),
                dtDenNgay,

                TaoNhan("Ghi chú:", 20, 220),
                txtGhiChu
            });

            cboHopDong.SelectedIndexChanged +=
                cboHopDong_SelectedIndexChanged;

            cboPhong.SelectedIndexChanged +=
                cboPhong_SelectedIndexChanged;

            nudSoThang.ValueChanged +=
                nudSoThang_ValueChanged;

            // ====================================
            // THÔNG TIN GIỚI HẠN
            // ====================================

            lblGioiHan = TaoNhan(
                "Giới hạn gia hạn",
                25, 355, 1065);

            lblGioiHan.ForeColor = Color.DarkBlue;
            lblGioiHan.Font =
                new Font("Segoe UI", 10F, FontStyle.Bold);

            lblTrangThai = TaoNhan(
                "Trạng thái: Chờ duyệt",
                25, 388, 1065);

            this.Controls.Add(lblGioiHan);
            this.Controls.Add(lblTrangThai);

            // ====================================
            // CÁC NÚT CHỨC NĂNG
            // ====================================

            btnLapPhieu = TaoNut(
                "Lập phiếu", 28, 435, 150);

            btnDuyet = TaoNut(
                "Duyệt gia hạn", 190, 435, 155);

            btnKiemTraNo = TaoNut(
                "Kiểm tra nợ", 357, 435, 155);

            btnTuChoi = TaoNut(
                "Từ chối", 524, 435, 135);

            btnHuy = TaoNut(
                "Hủy phiếu", 671, 435, 135);

            btnLamMoi = TaoNut(
                "Làm mới", 818, 435, 130);

            btnDong = TaoNut(
                "Đóng", 960, 435, 130);

            btnLapPhieu.Click += btnLapPhieu_Click;
            btnDuyet.Click += btnDuyet_Click;
            btnKiemTraNo.Click += btnKiemTraNo_Click;
            btnTuChoi.Click += btnTuChoi_Click;
            btnHuy.Click += btnHuy_Click;
            btnLamMoi.Click += btnLamMoi_Click;
            btnDong.Click += btnDong_Click;

            this.Controls.AddRange(new Control[]
            {
                btnLapPhieu, btnDuyet,
                btnKiemTraNo, btnTuChoi,
                btnHuy, btnLamMoi, btnDong
            });

            // ====================================
            // DANH SÁCH PHIẾU GIA HẠN
            // ====================================

            GroupBox grpDS = new GroupBox
            {
                Text = "Danh sách phiếu gia hạn",
                Location = new Point(20, 492),
                Size = new Size(1080, 250),
                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Bottom |
                    AnchorStyles.Left |
                    AnchorStyles.Right
            };

            grpDS.Controls.Add(
                TaoNhan("Tìm kiếm:", 15, 29, 100));

            txtTimKiem = TaoText(115, 26, 340);

            btnTimKiem =
                TaoNut("Tìm kiếm", 470, 24, 130);

            btnTimKiem.Click += btnTimKiem_Click;

            lblSoLuong =
                TaoNhan("Số phiếu: 0", 790, 29, 270);

            lblSoLuong.TextAlign =
                ContentAlignment.MiddleRight;

            grpDS.Controls.AddRange(new Control[]
            {
                txtTimKiem, btnTimKiem, lblSoLuong
            });

            dgvGiaHan = new DataGridView
            {
                Location = new Point(15, 70),
                Size = new Size(1050, 165),
                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Bottom |
                    AnchorStyles.Left |
                    AnchorStyles.Right,
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

            dgvGiaHan.SelectionChanged +=
                dgvGiaHan_SelectionChanged;

            grpDS.Controls.Add(dgvGiaHan);
            this.Controls.Add(grpDS);

            this.Load += FrmGiaHan_Load;

            this.ResumeLayout(false);
        }
    }
}
