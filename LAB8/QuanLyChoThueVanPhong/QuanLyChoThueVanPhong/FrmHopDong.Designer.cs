
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyChoThueVanPhong
{
    partial class FrmHopDong
    {
        private System.ComponentModel.IContainer components = null;

        private TextBox txtMaHopDong;
        private TextBox txtNoiDung;
        private TextBox txtTimKiem;

        private ComboBox cboKhachHang;
        private ComboBox cboTrangThai;
        private ComboBox cboPhong;

        private DateTimePicker dtNgayKy;
        private DateTimePicker dtBatDau;
        private DateTimePicker dtKetThuc;

        private NumericUpDown nudSoThang;
        private NumericUpDown nudGiaPhong;

        private Button btnThemPhong;
        private Button btnBoPhong;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private Button btnTimKiem;
        private Button btnDong;

        private DataGridView dgvPhongChon;
        private DataGridView dgvHopDong;

        private Label lblTongTien;
        private Label lblSoLuong;
        private Label lblHuongDan;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private Label TaoNhan(
            string text, int x, int y, int width = 130)
        {
            return new Label
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(width, 27),
                TextAlign = ContentAlignment.MiddleLeft
            };
        }

        private TextBox TaoText(
            int x, int y, int width, bool multiline = false)
        {
            return new TextBox
            {
                Location = new Point(x, y),
                Size = new Size(
                    width, multiline ? 48 : 28),
                Multiline = multiline
            };
        }

        private Button TaoNut(
            string text, int x, int y, int width = 125)
        {
            return new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(width, 36),
                Cursor = Cursors.Hand
            };
        }

        private DataGridView TaoBang()
        {
            return new DataGridView
            {
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
                ColumnHeadersHeight = 34
            };
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.Name = "FrmHopDong";
            this.Text = "Quản lý hợp đồng thuê văn phòng";
            this.ClientSize = new Size(1180, 850);
            this.MinimumSize = new Size(1120, 800);
            this.StartPosition =
                FormStartPosition.CenterParent;
            this.BackColor = Color.WhiteSmoke;
            this.Font = new Font("Segoe UI", 10F);
            this.AutoScaleMode = AutoScaleMode.Font;

            Label title = TaoNhan(
                "QUẢN LÝ HỢP ĐỒNG THUÊ VĂN PHÒNG",
                15, 10, 1150);

            title.Height = 45;
            title.Font = new Font(
                "Segoe UI", 17F, FontStyle.Bold);
            title.ForeColor =
                Color.FromArgb(35, 75, 125);
            title.TextAlign =
                ContentAlignment.MiddleCenter;

            title.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right;

            this.Controls.Add(title);

            // =====================================
            // GROUP THÔNG TIN HỢP ĐỒNG
            // =====================================

            GroupBox grpThongTin = new GroupBox
            {
                Text = "Thông tin hợp đồng",
                Location = new Point(20, 65),
                Size = new Size(1140, 208),
                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Left |
                    AnchorStyles.Right
            };

            this.Controls.Add(grpThongTin);

            txtMaHopDong = TaoText(160, 29, 300);
            txtMaHopDong.MaxLength = 20;

            cboKhachHang = new ComboBox
            {
                Location = new Point(690, 29),
                Size = new Size(420, 28),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            dtNgayKy = new DateTimePicker
            {
                Location = new Point(160, 71),
                Size = new Size(300, 28),
                Format = DateTimePickerFormat.Short
            };

            dtBatDau = new DateTimePicker
            {
                Location = new Point(690, 71),
                Size = new Size(420, 28),
                Format = DateTimePickerFormat.Short
            };

            nudSoThang = new NumericUpDown
            {
                Location = new Point(160, 113),
                Size = new Size(300, 28),
                Minimum = 6,
                Maximum = 600,
                Value = 6
            };

            dtKetThuc = new DateTimePicker
            {
                Location = new Point(690, 113),
                Size = new Size(420, 28),
                Format = DateTimePickerFormat.Short,
                Enabled = false
            };

            cboTrangThai = new ComboBox
            {
                Location = new Point(160, 155),
                Size = new Size(300, 28),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            txtNoiDung = TaoText(690, 155, 420);
            txtNoiDung.MaxLength = 2000;

            grpThongTin.Controls.AddRange(new Control[]
            {
                TaoNhan("Mã hợp đồng:", 20, 31),
                txtMaHopDong,

                TaoNhan("Khách hàng:", 550, 31),
                cboKhachHang,

                TaoNhan("Ngày ký:", 20, 73),
                dtNgayKy,

                TaoNhan("Ngày bắt đầu:", 550, 73),
                dtBatDau,

                TaoNhan("Số tháng thuê:", 20, 115),
                nudSoThang,

                TaoNhan("Ngày kết thúc:", 550, 115),
                dtKetThuc,

                TaoNhan("Trạng thái:", 20, 157),
                cboTrangThai,

                TaoNhan("Nội dung:", 550, 157),
                txtNoiDung
            });

            dtBatDau.ValueChanged += dtBatDau_ValueChanged;
            nudSoThang.ValueChanged += nudSoThang_ValueChanged;

            // =====================================
            // GROUP PHÒNG THUÊ
            // =====================================

            GroupBox grpPhong = new GroupBox
            {
                Text = "Chọn các phòng trong hợp đồng",
                Location = new Point(20, 285),
                Size = new Size(1140, 200),
                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Left |
                    AnchorStyles.Right
            };

            this.Controls.Add(grpPhong);

            grpPhong.Controls.Add(
                TaoNhan("Chọn phòng:", 20, 30, 105));

            cboPhong = new ComboBox
            {
                Location = new Point(125, 28),
                Size = new Size(270, 28),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            cboPhong.SelectedIndexChanged +=
                cboPhong_SelectedIndexChanged;

            grpPhong.Controls.Add(cboPhong);

            grpPhong.Controls.Add(
                TaoNhan("Đơn giá:", 415, 30, 90));

            nudGiaPhong = new NumericUpDown
            {
                Location = new Point(505, 28),
                Size = new Size(225, 28),
                Minimum = 0,
                Maximum = 100000000000M,
                ThousandsSeparator = true
            };

            grpPhong.Controls.Add(nudGiaPhong);

            btnThemPhong =
                TaoNut("Thêm phòng", 750, 25, 155);

            btnBoPhong =
                TaoNut("Bỏ phòng", 920, 25, 155);

            btnThemPhong.Click += btnThemPhong_Click;
            btnBoPhong.Click += btnBoPhong_Click;

            grpPhong.Controls.Add(btnThemPhong);
            grpPhong.Controls.Add(btnBoPhong);

            dgvPhongChon = TaoBang();
            dgvPhongChon.Location = new Point(20, 68);
            dgvPhongChon.Size = new Size(1090, 89);

            dgvPhongChon.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right;

            grpPhong.Controls.Add(dgvPhongChon);

            lblTongTien = TaoNhan(
                "Tổng tiền/tháng: 0 đ",
                20, 162, 1090);

            lblTongTien.ForeColor =
                Color.FromArgb(25, 100, 65);

            lblTongTien.Font = new Font(
                "Segoe UI", 10F, FontStyle.Bold);

            grpPhong.Controls.Add(lblTongTien);

            // =====================================
            // NÚT THÊM / SỬA / XÓA
            // =====================================

            btnThem = TaoNut("Thêm hợp đồng", 180, 500, 175);
            btnSua = TaoNut("Sửa hợp đồng", 370, 500, 175);
            btnXoa = TaoNut("Xóa hợp đồng", 560, 500, 175);
            btnLamMoi = TaoNut("Làm mới", 750, 500, 150);
            btnDong = TaoNut("Đóng", 915, 500, 125);

            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnLamMoi.Click += btnLamMoi_Click;
            btnDong.Click += btnDong_Click;

            this.Controls.AddRange(new Control[]
            {
                btnThem, btnSua, btnXoa,
                btnLamMoi, btnDong
            });

            // =====================================
            // DANH SÁCH HỢP ĐỒNG
            // =====================================

            GroupBox grpDanhSach = new GroupBox
            {
                Text = "Danh sách hợp đồng",
                Location = new Point(20, 552),
                Size = new Size(1140, 276),
                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Bottom |
                    AnchorStyles.Left |
                    AnchorStyles.Right
            };

            this.Controls.Add(grpDanhSach);

            grpDanhSach.Controls.Add(
                TaoNhan("Tìm kiếm:", 20, 28, 100));

            txtTimKiem = TaoText(120, 26, 335);
            txtTimKiem.KeyDown += txtTimKiem_KeyDown;

            btnTimKiem =
                TaoNut("Tìm kiếm", 472, 23, 125);

            btnTimKiem.Click += btnTimKiem_Click;

            lblSoLuong = TaoNhan(
                "Số hợp đồng: 0", 825, 28, 285);

            lblSoLuong.TextAlign =
                ContentAlignment.MiddleRight;

            grpDanhSach.Controls.AddRange(new Control[]
            {
                txtTimKiem, btnTimKiem, lblSoLuong
            });

            lblHuongDan = TaoNhan(
                "Nhập hợp đồng mới và chọn các phòng thuê.",
                20, 65, 1090);

            lblHuongDan.ForeColor =
                Color.DarkSlateGray;

            grpDanhSach.Controls.Add(lblHuongDan);

            dgvHopDong = TaoBang();
            dgvHopDong.Location = new Point(20, 100);
            dgvHopDong.Size = new Size(1090, 160);

            dgvHopDong.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right;

            dgvHopDong.SelectionChanged +=
                dgvHopDong_SelectionChanged;

            grpDanhSach.Controls.Add(dgvHopDong);

            this.Load += FrmHopDong_Load;

            this.ResumeLayout(false);
        }
    }
}
