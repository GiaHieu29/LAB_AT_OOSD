
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyChoThueVanPhong
{
    partial class FrmKhachHang
    {
        private System.ComponentModel.IContainer components = null;

        private TextBox txtMaKhachHang;
        private TextBox txtTenKhachHang;
        private TextBox txtMaSoThue;
        private TextBox txtNguoiDaiDien;
        private TextBox txtDienThoai;
        private TextBox txtEmail;
        private TextBox txtDiaChi;
        private TextBox txtGhiChu;
        private TextBox txtTimKiem;

        private ComboBox cboLoaiKhachHang;

        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private Button btnTimKiem;
        private Button btnDong;

        private DataGridView dgvKhachHang;

        private Label lblSoLuong;
        private Label lblHuongDan;

        private GroupBox grpThongTin;
        private GroupBox grpDanhSach;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        // ===================================
        // HÀM TẠO CONTROL DÙNG CHUNG
        // ===================================

        private Label TaoNhan(
            string text, int x, int y, int width = 135)
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
            int maxLength = 150,
            bool multiline = false)
        {
            return new TextBox
            {
                Location = new Point(x, y),
                Size = new Size(
                    width, multiline ? 48 : 29),
                MaxLength = maxLength,
                Multiline = multiline
            };
        }

        private Button TaoNut(
            string text, int x, int y, int width = 135)
        {
            return new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(width, 39),
                Cursor = Cursors.Hand
            };
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // ===================================
            // FORM
            // ===================================

            this.Name = "FrmKhachHang";
            this.Text = "Quản lý khách hàng - Công ty";

            this.ClientSize = new Size(1120, 795);

            this.MinimumSize = new Size(1000, 730);

            this.StartPosition =
                FormStartPosition.CenterParent;

            this.BackColor = Color.WhiteSmoke;

            this.Font = new Font("Segoe UI", 10F);

            this.AutoScaleMode = AutoScaleMode.Font;

            // ===================================
            // TIÊU ĐỀ
            // ===================================

            Label title = TaoNhan(
                "QUẢN LÝ KHÁCH HÀNG - CÔNG TY",
                20, 12, 1080);

            title.Font = new Font(
                "Segoe UI", 17F, FontStyle.Bold);

            title.ForeColor =
                Color.FromArgb(35, 75, 125);

            title.Height = 45;
            title.TextAlign = ContentAlignment.MiddleCenter;

            title.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right;

            this.Controls.Add(title);

            // ===================================
            // GROUP THÔNG TIN KHÁCH HÀNG
            // ===================================

            grpThongTin = new GroupBox
            {
                Text = "Thông tin khách hàng",
                Location = new Point(20, 68),
                Size = new Size(1080, 260),
                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Left |
                    AnchorStyles.Right
            };

            this.Controls.Add(grpThongTin);

            // HÀNG 1
            txtMaKhachHang =
                TaoText(160, 36, 295, 20);

            txtTenKhachHang =
                TaoText(630, 36, 420, 200);

            grpThongTin.Controls.AddRange(
                new Control[]
                {
                    TaoNhan("Mã khách hàng:", 20, 38),
                    txtMaKhachHang,

                    TaoNhan("Tên khách hàng:", 485, 38),
                    txtTenKhachHang
                });

            // HÀNG 2
            cboLoaiKhachHang = new ComboBox
            {
                Location = new Point(160, 81),
                Size = new Size(295, 29),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            txtMaSoThue =
                TaoText(630, 81, 420, 30);

            grpThongTin.Controls.AddRange(
                new Control[]
                {
                    TaoNhan("Loại khách hàng:", 20, 83),
                    cboLoaiKhachHang,

                    TaoNhan("Mã số thuế:", 485, 83),
                    txtMaSoThue
                });

            // HÀNG 3
            txtNguoiDaiDien =
                TaoText(160, 126, 295, 150);

            txtDienThoai =
                TaoText(630, 126, 420, 20);

            grpThongTin.Controls.AddRange(
                new Control[]
                {
                    TaoNhan("Người đại diện:", 20, 128),
                    txtNguoiDaiDien,

                    TaoNhan("Điện thoại:", 485, 128),
                    txtDienThoai
                });

            // HÀNG 4
            txtEmail =
                TaoText(160, 171, 295, 150);

            txtDiaChi =
                TaoText(630, 171, 420, 300);

            grpThongTin.Controls.AddRange(
                new Control[]
                {
                    TaoNhan("Email:", 20, 173),
                    txtEmail,

                    TaoNhan("Địa chỉ:", 485, 173),
                    txtDiaChi
                });

            // HÀNG 5
            txtGhiChu =
                TaoText(160, 212, 890, 500, true);

            txtGhiChu.Size = new Size(890, 38);

            grpThongTin.Controls.AddRange(
                new Control[]
                {
                    TaoNhan("Ghi chú:", 20, 214),
                    txtGhiChu
                });

            // ===================================
            // CÁC NÚT CHỨC NĂNG
            // ===================================

            btnThem = TaoNut("Thêm", 200, 345);
            btnSua = TaoNut("Sửa", 350, 345);
            btnXoa = TaoNut("Xóa", 500, 345);
            btnLamMoi = TaoNut("Làm mới", 650, 345);
            btnDong = TaoNut("Đóng", 800, 345);

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

            // ===================================
            // GROUP DANH SÁCH KHÁCH HÀNG
            // ===================================

            grpDanhSach = new GroupBox
            {
                Text = "Danh sách khách hàng",
                Location = new Point(20, 405),
                Size = new Size(1080, 370),
                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Bottom |
                    AnchorStyles.Left |
                    AnchorStyles.Right
            };

            this.Controls.Add(grpDanhSach);

            // TÌM KIẾM
            grpDanhSach.Controls.Add(
                TaoNhan("Tìm kiếm:", 20, 32, 100));

            txtTimKiem =
                TaoText(120, 29, 350, 200);

            txtTimKiem.KeyDown +=
                txtTimKiem_KeyDown;

            btnTimKiem =
                TaoNut("Tìm kiếm", 485, 27, 125);

            btnTimKiem.Click += btnTimKiem_Click;

            lblSoLuong =
                TaoNhan("Số khách hàng: 0", 740, 32, 310);

            lblSoLuong.TextAlign =
                ContentAlignment.MiddleRight;

            grpDanhSach.Controls.AddRange(
                new Control[]
                {
                    txtTimKiem,
                    btnTimKiem,
                    lblSoLuong
                });

            // HƯỚNG DẪN
            lblHuongDan = TaoNhan(
                "Nhập thông tin khách hàng mới, sau đó nhấn Thêm.",
                20, 68, 1025);

            lblHuongDan.ForeColor =
                Color.DarkSlateGray;

            grpDanhSach.Controls.Add(lblHuongDan);

            // ===================================
            // DATAGRIDVIEW
            // ===================================

            dgvKhachHang = new DataGridView
            {
                Location = new Point(20, 105),
                Size = new Size(1040, 245),

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
                EnableHeadersVisualStyles = false
            };

            dgvKhachHang.ColumnHeadersHeight = 38;

            dgvKhachHang.ColumnHeadersDefaultCellStyle
                .BackColor =
                    Color.FromArgb(219, 230, 241);

            dgvKhachHang.ColumnHeadersDefaultCellStyle
                .Font =
                    new Font(
                        "Segoe UI", 9F,
                        FontStyle.Bold);

            dgvKhachHang.DefaultCellStyle
                .SelectionBackColor =
                    Color.FromArgb(205, 225, 245);

            dgvKhachHang.DefaultCellStyle
                .SelectionForeColor =
                    Color.Black;

            dgvKhachHang.SelectionChanged +=
                dgvKhachHang_SelectionChanged;

            grpDanhSach.Controls.Add(dgvKhachHang);

            // ===================================
            // SỰ KIỆN LOAD
            // ===================================

            this.Load += FrmKhachHang_Load;

            this.ResumeLayout(false);
        }
    }
}
