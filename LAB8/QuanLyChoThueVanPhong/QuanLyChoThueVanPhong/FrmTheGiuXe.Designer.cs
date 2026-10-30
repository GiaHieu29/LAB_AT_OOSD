
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyChoThueVanPhong
{
    partial class FrmTheGiuXe
    {
        private System.ComponentModel.IContainer components = null;

        private TextBox txtMaThe;
        private TextBox txtBienSo;
        private TextBox txtMauXe;
        private TextBox txtTimKiem;

        private ComboBox cboNhanVien;
        private ComboBox cboLoaiXe;
        private ComboBox cboTrangThai;

        private DateTimePicker dtNgayCap;

        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private Button btnTimKiem;
        private Button btnDong;

        private DataGridView dgvTheGiuXe;

        private Label lblSoLuong;
        private Label lblHuongDan;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        // =====================================
        // HÀM TẠO LABEL
        // =====================================
        private Label TaoNhan(
            string text,
            int x,
            int y,
            int width = 140)
        {
            return new Label
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(width, 29),
                TextAlign =
                    ContentAlignment.MiddleLeft
            };
        }

        // =====================================
        // HÀM TẠO TEXTBOX
        // =====================================
        private TextBox TaoText(
            int x,
            int y,
            int width,
            int maxLength = 100)
        {
            return new TextBox
            {
                Location = new Point(x, y),
                Size = new Size(width, 30),
                MaxLength = maxLength
            };
        }

        // =====================================
        // HÀM TẠO COMBOBOX
        // =====================================
        private ComboBox TaoCombo(
            int x,
            int y,
            int width)
        {
            return new ComboBox
            {
                Location = new Point(x, y),
                Size = new Size(width, 30),
                DropDownStyle =
                    ComboBoxStyle.DropDownList
            };
        }

        // =====================================
        // HÀM TẠO BUTTON
        // =====================================
        private Button TaoNut(
            string text,
            int x,
            int y,
            int width = 135)
        {
            return new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(width, 40),
                Cursor = Cursors.Hand
            };
        }

        // =====================================
        // TẠO GIAO DIỆN
        // =====================================
        private void InitializeComponent()
        {
            this.SuspendLayout();

            // FORM
            this.Name = "FrmTheGiuXe";
            this.Text = "Quản lý thẻ giữ xe cao ốc";

            this.ClientSize = new Size(1120, 740);
            this.MinimumSize = new Size(1030, 690);

            this.StartPosition =
                FormStartPosition.CenterParent;

            this.BackColor = Color.WhiteSmoke;
            this.Font = new Font("Segoe UI", 10F);
            this.AutoScaleMode = AutoScaleMode.Font;

            // TIÊU ĐỀ
            Label title = TaoNhan(
                "QUẢN LÝ THẺ GIỮ XE NHÂN VIÊN CAO ỐC",
                20, 12, 1080);

            title.Height = 48;

            title.Font = new Font(
                "Segoe UI",
                17F,
                FontStyle.Bold);

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
            // NHÓM THÔNG TIN THẺ GIỮ XE
            // =====================================
            GroupBox grpThongTin = new GroupBox
            {
                Text = "Thông tin thẻ giữ xe",
                Location = new Point(20, 70),
                Size = new Size(1080, 245),
                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Left |
                    AnchorStyles.Right
            };

            this.Controls.Add(grpThongTin);

            // HÀNG 1
            txtMaThe = TaoText(
                160, 35, 300, 20);

            cboNhanVien = TaoCombo(
                660, 35, 390);

            grpThongTin.Controls.AddRange(
                new Control[]
                {
                    TaoNhan("Mã thẻ:", 20, 37),
                    txtMaThe,

                    TaoNhan("Nhân viên:", 510, 37),
                    cboNhanVien
                });

            // HÀNG 2
            txtBienSo = TaoText(
                160, 85, 300, 30);

            cboLoaiXe = TaoCombo(
                660, 85, 390);

            grpThongTin.Controls.AddRange(
                new Control[]
                {
                    TaoNhan("Biển số xe:", 20, 87),
                    txtBienSo,

                    TaoNhan("Loại xe:", 510, 87),
                    cboLoaiXe
                });

            // HÀNG 3
            txtMauXe = TaoText(
                160, 135, 300, 100);

            dtNgayCap = new DateTimePicker
            {
                Location = new Point(660, 135),
                Size = new Size(390, 30),
                Format = DateTimePickerFormat.Short
            };

            grpThongTin.Controls.AddRange(
                new Control[]
                {
                    TaoNhan("Màu xe:", 20, 137),
                    txtMauXe,

                    TaoNhan("Ngày cấp:", 510, 137),
                    dtNgayCap
                });

            // HÀNG 4
            cboTrangThai = TaoCombo(
                160, 185, 300);

            grpThongTin.Controls.AddRange(
                new Control[]
                {
                    TaoNhan("Trạng thái:", 20, 187),
                    cboTrangThai
                });

            // =====================================
            // NÚT CHỨC NĂNG
            // =====================================
            btnThem = TaoNut(
                "Thêm", 185, 335, 140);

            btnSua = TaoNut(
                "Sửa", 340, 335, 140);

            btnXoa = TaoNut(
                "Xóa", 495, 335, 140);

            btnLamMoi = TaoNut(
                "Làm mới", 650, 335, 140);

            btnDong = TaoNut(
                "Đóng", 805, 335, 140);

            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnLamMoi.Click += btnLamMoi_Click;
            btnDong.Click += btnDong_Click;

            this.Controls.AddRange(
                new Control[]
                {
                    btnThem,
                    btnSua,
                    btnXoa,
                    btnLamMoi,
                    btnDong
                });

            // =====================================
            // NHÓM DANH SÁCH THẺ GIỮ XE
            // =====================================
            GroupBox grpDanhSach = new GroupBox
            {
                Text = "Danh sách thẻ giữ xe",
                Location = new Point(20, 395),
                Size = new Size(1080, 325),
                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Bottom |
                    AnchorStyles.Left |
                    AnchorStyles.Right
            };

            this.Controls.Add(grpDanhSach);

            // TÌM KIẾM
            grpDanhSach.Controls.Add(
                TaoNhan("Tìm kiếm:", 20, 30, 100));

            txtTimKiem = TaoText(
                120, 28, 350, 200);

            btnTimKiem = TaoNut(
                "Tìm kiếm", 485, 25, 130);

            btnTimKiem.Click += btnTimKiem_Click;

            txtTimKiem.KeyDown +=
                txtTimKiem_KeyDown;

            lblSoLuong = TaoNhan(
                "Số thẻ: 0", 820, 30, 230);

            lblSoLuong.TextAlign =
                ContentAlignment.MiddleRight;

            grpDanhSach.Controls.AddRange(
                new Control[]
                {
                    txtTimKiem,
                    btnTimKiem,
                    lblSoLuong
                });

            // THÔNG BÁO
            lblHuongDan = TaoNhan(
                "Chọn một dòng để sửa hoặc " +
                "nhập thông tin để cấp thẻ mới.",
                20, 70, 1030);

            lblHuongDan.ForeColor =
                Color.DarkSlateGray;

            grpDanhSach.Controls.Add(lblHuongDan);

            // =====================================
            // DATAGRIDVIEW
            // =====================================
            dgvTheGiuXe = new DataGridView
            {
                Location = new Point(20, 110),
                Size = new Size(1040, 195),

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
                ColumnHeadersHeight = 37,

                EnableHeadersVisualStyles = false
            };

            dgvTheGiuXe.ColumnHeadersDefaultCellStyle
                .BackColor =
                    Color.FromArgb(219, 230, 241);

            dgvTheGiuXe.ColumnHeadersDefaultCellStyle
                .Font =
                    new Font("Segoe UI", 9F,
                        FontStyle.Bold);

            dgvTheGiuXe.DefaultCellStyle
                .SelectionBackColor =
                    Color.FromArgb(205, 225, 245);

            dgvTheGiuXe.DefaultCellStyle
                .SelectionForeColor =
                    Color.Black;

            dgvTheGiuXe.SelectionChanged +=
                dgvTheGiuXe_SelectionChanged;

            grpDanhSach.Controls.Add(dgvTheGiuXe);

            // LOAD FORM
            this.Load += FrmTheGiuXe_Load;

            this.ResumeLayout(false);
        }
    }
}
