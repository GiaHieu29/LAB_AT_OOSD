
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyChoThueVanPhong
{
    partial class FrmKiemTraPhong
    {
        private System.ComponentModel.IContainer components = null;

        private TextBox txtMaKiemTra;
        private TextBox txtNoiDung;
        private TextBox txtNguoiKiemTra;
        private TextBox txtDeXuat;
        private TextBox txtTimKiem;

        private ComboBox cboPhong;
        private ComboBox cboTinhTrang;

        private DateTimePicker dtNgayKiemTra;

        private DataGridView dgvKiemTra;

        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private Button btnTimKiem;
        private Button btnDong;

        private Label lblSoLuong;
        private Label lblHuongDan;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private Label TaoNhan(
            string text, int x, int y,
            int width = 135)
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
            int x, int y, int width,
            bool multiline = false)
        {
            return new TextBox
            {
                Location = new Point(x, y),
                Size = new Size(
                    width, multiline ? 58 : 30),
                Multiline = multiline,
                ScrollBars = multiline
                    ? ScrollBars.Vertical
                    : ScrollBars.None,
                MaxLength = multiline ? 2000 : 150
            };
        }

        private ComboBox TaoCombo(
            int x, int y, int width)
        {
            return new ComboBox
            {
                Location = new Point(x, y),
                Size = new Size(width, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
        }

        private Button TaoNut(
            string text, int x, int y,
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

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // ===================================
            // FORM
            // ===================================
            this.Name = "FrmKiemTraPhong";
            this.Text = "Quản lý kiểm tra tình trạng phòng";
            this.ClientSize = new Size(1140, 790);
            this.MinimumSize = new Size(1060, 730);
            this.StartPosition =
                FormStartPosition.CenterParent;
            this.BackColor = Color.WhiteSmoke;
            this.Font = new Font("Segoe UI", 10F);
            this.AutoScaleMode = AutoScaleMode.Font;

            // ===================================
            // TIÊU ĐỀ
            // ===================================
            Label title = TaoNhan(
                "QUẢN LÝ KIỂM TRA TÌNH TRẠNG PHÒNG",
                20, 12, 1100);

            title.Size = new Size(1100, 48);
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

            // ===================================
            // THÔNG TIN PHIẾU KIỂM TRA
            // ===================================
            GroupBox grp = new GroupBox
            {
                Text = "Thông tin phiếu kiểm tra",
                Location = new Point(20, 70),
                Size = new Size(1100, 290),
                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Left |
                    AnchorStyles.Right
            };

            this.Controls.Add(grp);

            txtMaKiemTra = TaoText(165, 34, 300);
            txtMaKiemTra.MaxLength = 20;

            cboPhong = TaoCombo(695, 34, 365);

            grp.Controls.AddRange(new Control[]
            {
                TaoNhan("Mã kiểm tra:", 20, 36),
                txtMaKiemTra,

                TaoNhan("Phòng kiểm tra:", 540, 36, 145),
                cboPhong
            });

            dtNgayKiemTra = new DateTimePicker
            {
                Location = new Point(165, 82),
                Size = new Size(300, 30),
                Format = DateTimePickerFormat.Short
            };

            cboTinhTrang = TaoCombo(695, 82, 365);

            grp.Controls.AddRange(new Control[]
            {
                TaoNhan("Ngày kiểm tra:", 20, 84),
                dtNgayKiemTra,

                TaoNhan("Tình trạng:", 540, 84, 145),
                cboTinhTrang
            });

            txtNguoiKiemTra = TaoText(
                165, 132, 895);

            grp.Controls.AddRange(new Control[]
            {
                TaoNhan("Người kiểm tra:", 20, 134),
                txtNguoiKiemTra
            });

            txtNoiDung = TaoText(
                165, 180, 895, true);

            txtDeXuat = TaoText(
                165, 242, 895, true);

            // Hai ô nhiều dòng được bố trí
            // gọn trong chiều cao của GroupBox.
            txtNoiDung.Size = new Size(895, 48);
            txtDeXuat.Size = new Size(895, 38);

            grp.Controls.AddRange(new Control[]
            {
                TaoNhan("Nội dung:", 20, 182),
                txtNoiDung,

                TaoNhan("Đề xuất sửa:", 20, 245),
                txtDeXuat
            });

            // ===================================
            // CÁC NÚT CHỨC NĂNG
            // ===================================
            btnThem = TaoNut("Thêm", 180, 377, 145);
            btnSua = TaoNut("Sửa", 340, 377, 145);
            btnXoa = TaoNut("Xóa", 500, 377, 145);
            btnLamMoi = TaoNut("Làm mới", 660, 377, 145);
            btnDong = TaoNut("Đóng", 820, 377, 145);

            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnLamMoi.Click += btnLamMoi_Click;
            btnDong.Click += btnDong_Click;

            this.Controls.AddRange(new Control[]
            {
                btnThem, btnSua,
                btnXoa, btnLamMoi, btnDong
            });

            // ===================================
            // DANH SÁCH PHIẾU KIỂM TRA
            // ===================================
            GroupBox grpDanhSach = new GroupBox
            {
                Text = "Danh sách kiểm tra phòng",
                Location = new Point(20, 435),
                Size = new Size(1100, 335),
                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Bottom |
                    AnchorStyles.Left |
                    AnchorStyles.Right
            };

            this.Controls.Add(grpDanhSach);

            grpDanhSach.Controls.Add(
                TaoNhan("Tìm kiếm:", 20, 31, 100));

            txtTimKiem = TaoText(120, 29, 350);
            txtTimKiem.MaxLength = 200;

            btnTimKiem = TaoNut(
                "Tìm kiếm", 485, 26, 140);

            txtTimKiem.KeyDown +=
                txtTimKiem_KeyDown;

            btnTimKiem.Click +=
                btnTimKiem_Click;

            lblSoLuong = TaoNhan(
                "Số phiếu: 0", 830, 31, 245);

            lblSoLuong.TextAlign =
                ContentAlignment.MiddleRight;

            grpDanhSach.Controls.AddRange(new Control[]
            {
                txtTimKiem,
                btnTimKiem,
                lblSoLuong
            });

            lblHuongDan = TaoNhan(
                "Nhập thông tin mới hoặc chọn phiếu để sửa.",
                20, 72, 1050);

            lblHuongDan.ForeColor =
                Color.DarkSlateGray;

            grpDanhSach.Controls.Add(lblHuongDan);

            // ===================================
            // BẢNG DATAGRIDVIEW
            // ===================================
            dgvKiemTra = new DataGridView
            {
                Location = new Point(20, 108),
                Size = new Size(1060, 208),

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
                ColumnHeadersHeight = 38,
                EnableHeadersVisualStyles = false
            };

            dgvKiemTra.ColumnHeadersDefaultCellStyle
                .BackColor =
                    Color.FromArgb(219, 230, 241);

            dgvKiemTra.ColumnHeadersDefaultCellStyle
                .Font = new Font(
                    "Segoe UI", 9F, FontStyle.Bold);

            dgvKiemTra.DefaultCellStyle
                .SelectionBackColor =
                    Color.FromArgb(205, 225, 245);

            dgvKiemTra.DefaultCellStyle
                .SelectionForeColor =
                    Color.Black;

            dgvKiemTra.SelectionChanged +=
                dgvKiemTra_SelectionChanged;

            grpDanhSach.Controls.Add(dgvKiemTra);

            // ===================================
            // LOAD FORM
            // ===================================
            this.Load += FrmKiemTraPhong_Load;

            this.ResumeLayout(false);
        }
    }
}
