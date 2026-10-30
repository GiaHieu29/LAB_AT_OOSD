
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyChoThueVanPhong
{
    partial class FrmNhanVienVanPhong
    {
        private System.ComponentModel.IContainer components = null;

        private TextBox txtMaNhanVien;
        private TextBox txtHoTen;
        private TextBox txtCCCD;
        private TextBox txtChucVu;
        private TextBox txtDienThoai;
        private TextBox txtDuongDanAnh;
        private TextBox txtTimKiem;

        private ComboBox cboCongTy;
        private ComboBox cboPhong;
        private ComboBox cboGioiTinh;
        private ComboBox cboTrangThai;

        private DateTimePicker dtNgaySinh;
        private PictureBox picAnh;

        private Button btnChonAnh;
        private Button btnXoaAnh;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private Button btnTimKiem;
        private Button btnDong;

        private DataGridView dgvNhanVien;
        private Label lblSoLuong;
        private Label lblHuongDan;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (picAnh != null && picAnh.Image != null)
                {
                    picAnh.Image.Dispose();
                    picAnh.Image = null;
                }

                if (components != null)
                    components.Dispose();
            }

            base.Dispose(disposing);
        }

        private Label TaoNhan(
            string text, int x, int y, int w = 125)
        {
            return new Label
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(w, 28),
                TextAlign = ContentAlignment.MiddleLeft
            };
        }

        private TextBox TaoText(
            int x, int y, int w, int max = 150)
        {
            return new TextBox
            {
                Location = new Point(x, y),
                Size = new Size(w, 29),
                MaxLength = max
            };
        }

        private ComboBox TaoCombo(int x, int y, int w)
        {
            return new ComboBox
            {
                Location = new Point(x, y),
                Size = new Size(w, 29),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
        }

        private Button TaoNut(
            string text, int x, int y, int w = 125)
        {
            return new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(w, 39),
                Cursor = Cursors.Hand
            };
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.Name = "FrmNhanVienVanPhong";
            this.Text = "Quản lý nhân viên văn phòng";
            this.ClientSize = new Size(1180, 835);
            this.MinimumSize = new Size(1120, 790);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Font = new Font("Segoe UI", 10F);
            this.BackColor = Color.WhiteSmoke;
            this.AutoScaleMode = AutoScaleMode.Font;

            Label title = TaoNhan(
                "QUẢN LÝ NHÂN VIÊN LÀM VIỆC TẠI CAO ỐC",
                20, 10, 1140);

            title.Height = 45;
            title.Font = new Font(
                "Segoe UI", 17F, FontStyle.Bold);
            title.ForeColor =
                Color.FromArgb(35, 75, 125);
            title.TextAlign = ContentAlignment.MiddleCenter;

            this.Controls.Add(title);

            // ======================================
            // GROUP THÔNG TIN NHÂN VIÊN
            // ======================================

            GroupBox grp = new GroupBox
            {
                Text = "Thông tin nhân viên",
                Location = new Point(20, 65),
                Size = new Size(1140, 335),
                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Left |
                    AnchorStyles.Right
            };

            this.Controls.Add(grp);

            txtMaNhanVien = TaoText(155, 32, 245, 20);
            txtHoTen = TaoText(570, 32, 320);

            cboCongTy = TaoCombo(155, 77, 245);
            cboPhong = TaoCombo(570, 77, 320);

            dtNgaySinh = new DateTimePicker
            {
                Location = new Point(155, 122),
                Size = new Size(245, 29),
                Format = DateTimePickerFormat.Short,
                ShowCheckBox = true,
                Checked = false
            };

            cboGioiTinh = TaoCombo(570, 122, 320);

            txtCCCD = TaoText(155, 167, 245, 20);
            txtChucVu = TaoText(570, 167, 320, 100);

            txtDienThoai = TaoText(155, 212, 245, 20);
            cboTrangThai = TaoCombo(570, 212, 320);

            grp.Controls.AddRange(new Control[]
            {
                TaoNhan("Mã nhân viên:", 20, 34),
                txtMaNhanVien,

                TaoNhan("Họ tên:", 440, 34, 115),
                txtHoTen,

                TaoNhan("Công ty:", 20, 79),
                cboCongTy,

                TaoNhan("Phòng:", 440, 79, 115),
                cboPhong,

                TaoNhan("Ngày sinh:", 20, 124),
                dtNgaySinh,

                TaoNhan("Giới tính:", 440, 124, 115),
                cboGioiTinh,

                TaoNhan("CCCD:", 20, 169),
                txtCCCD,

                TaoNhan("Chức vụ:", 440, 169, 115),
                txtChucVu,

                TaoNhan("Điện thoại:", 20, 214),
                txtDienThoai,

                TaoNhan("Trạng thái:", 440, 214, 115),
                cboTrangThai
            });

            // ẢNH NHÂN VIÊN
            picAnh = new PictureBox
            {
                Location = new Point(920, 32),
                Size = new Size(190, 190),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                SizeMode = PictureBoxSizeMode.Zoom
            };

            grp.Controls.Add(picAnh);

            btnChonAnh = TaoNut(
                "Chọn ảnh", 920, 230, 90);

            btnXoaAnh = TaoNut(
                "Xóa ảnh", 1020, 230, 90);

            btnChonAnh.Click += btnChonAnh_Click;
            btnXoaAnh.Click += btnXoaAnh_Click;

            grp.Controls.Add(btnChonAnh);
            grp.Controls.Add(btnXoaAnh);

            txtDuongDanAnh = TaoText(
                155, 274, 735, 500);

            txtDuongDanAnh.ReadOnly = true;
            txtDuongDanAnh.BackColor = Color.White;

            grp.Controls.AddRange(new Control[]
            {
                TaoNhan("Đường dẫn ảnh:", 20, 276),
                txtDuongDanAnh
            });

            cboCongTy.SelectedIndexChanged +=
                cboCongTy_SelectedIndexChanged;

            // ======================================
            // NÚT CHỨC NĂNG
            // ======================================

            btnThem = TaoNut("Thêm", 235, 415, 135);
            btnSua = TaoNut("Sửa", 385, 415, 135);
            btnXoa = TaoNut("Xóa", 535, 415, 135);
            btnLamMoi = TaoNut("Làm mới", 685, 415, 135);
            btnDong = TaoNut("Đóng", 835, 415, 135);

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

            // ======================================
            // DANH SÁCH NHÂN VIÊN
            // ======================================

            GroupBox grpDS = new GroupBox
            {
                Text = "Danh sách nhân viên văn phòng",
                Location = new Point(20, 470),
                Size = new Size(1140, 345),
                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Bottom |
                    AnchorStyles.Left |
                    AnchorStyles.Right
            };

            grpDS.Controls.Add(
                TaoNhan("Tìm kiếm:", 20, 31, 100));

            txtTimKiem = TaoText(120, 28, 340, 200);
            txtTimKiem.KeyDown += txtTimKiem_KeyDown;

            btnTimKiem = TaoNut(
                "Tìm kiếm", 475, 26, 130);

            btnTimKiem.Click += btnTimKiem_Click;

            lblSoLuong = TaoNhan(
                "Số nhân viên: 0", 840, 31, 265);

            lblSoLuong.TextAlign =
                ContentAlignment.MiddleRight;

            lblHuongDan = TaoNhan(
                "Chọn nhân viên để xem hoặc sửa thông tin.",
                20, 72, 1090);

            lblHuongDan.ForeColor =
                Color.DarkSlateGray;

            grpDS.Controls.AddRange(new Control[]
            {
                txtTimKiem, btnTimKiem,
                lblSoLuong, lblHuongDan
            });

            dgvNhanVien = new DataGridView
            {
                Location = new Point(20, 110),
                Size = new Size(1100, 215),
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

            dgvNhanVien.ColumnHeadersDefaultCellStyle
                .BackColor = Color.FromArgb(219, 230, 241);

            dgvNhanVien.ColumnHeadersDefaultCellStyle
                .Font = new Font(
                    "Segoe UI", 9F, FontStyle.Bold);

            dgvNhanVien.DefaultCellStyle
                .SelectionBackColor =
                    Color.FromArgb(205, 225, 245);

            dgvNhanVien.DefaultCellStyle
                .SelectionForeColor = Color.Black;

            dgvNhanVien.SelectionChanged +=
                dgvNhanVien_SelectionChanged;

            grpDS.Controls.Add(dgvNhanVien);
            this.Controls.Add(grpDS);

            // ======================================
            // LOAD FORM
            // ======================================

            this.Load += FrmNhanVienVanPhong_Load;

            this.ResumeLayout(false);
        }
    }
}
