
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyChoThueVanPhong
{
    partial class FrmDanhMuc
    {
        private System.ComponentModel.IContainer components = null;

        private TabControl tabDanhMuc;
        private TabPage tabTang, tabPhong, tabDichVu;

        // TẦNG
        private TextBox txtMaTang, txtTenTang;
        private TextBox txtMoTaTang, txtTimTang;
        private NumericUpDown nudSoTang;
        private DataGridView dgvTang;
        private Label lblTongTang;
        private Button btnThemTang, btnSuaTang;
        private Button btnXoaTang, btnLamMoiTang;
        private Button btnTimTang;

        // PHÒNG
        private TextBox txtMaPhong, txtTenPhong;
        private TextBox txtMoTaPhong, txtTimPhong;
        private ComboBox cboTangPhong, cboHuong;
        private ComboBox cboTrangThaiPhong;
        private NumericUpDown nudDienTich, nudGiaThue;
        private DataGridView dgvPhong;
        private Label lblTongPhong;
        private Button btnThemPhong, btnSuaPhong;
        private Button btnXoaPhong, btnLamMoiPhong;
        private Button btnTimPhong;

        // DỊCH VỤ
        private TextBox txtMaDichVu, txtTenDichVu;
        private TextBox txtDonViTinh, txtMoTaDichVu;
        private TextBox txtTimDichVu;
        private NumericUpDown nudDonGiaDV;
        private CheckBox chkDangSuDung;
        private DataGridView dgvDichVu;
        private Label lblTongDichVu;
        private Button btnThemDichVu, btnSuaDichVu;
        private Button btnXoaDichVu, btnLamMoiDichVu;
        private Button btnTimDichVu;

        private Button btnDong;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        // ============================================
        // CÁC HÀM TẠO CONTROL
        // ============================================

        private Label TaoNhan(
            string text, int x, int y, int w = 130)
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
            int x, int y, int w, bool nhieuDong = false)
        {
            return new TextBox
            {
                Location = new Point(x, y),
                Size = new Size(w, nhieuDong ? 55 : 29),
                Multiline = nhieuDong,
                MaxLength = nhieuDong ? 500 : 150
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

        private NumericUpDown TaoSo(
            int x, int y, int w, decimal max,
            int le = 0)
        {
            return new NumericUpDown
            {
                Location = new Point(x, y),
                Size = new Size(w, 29),
                Minimum = 0,
                Maximum = max,
                DecimalPlaces = le,
                ThousandsSeparator = true
            };
        }

        private Button TaoNut(
            string text, int x, int y, int w = 125)
        {
            return new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(w, 37),
                Cursor = Cursors.Hand
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

        private GroupBox TaoNhom(
            string text, int x, int y, int w, int h)
        {
            return new GroupBox
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(w, h)
            };
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // ============================================
            // FORM
            // ============================================

            this.Name = "FrmDanhMuc";
            this.Text = "Quản lý danh mục cao ốc";
            this.ClientSize = new Size(1100, 760);

            this.StartPosition =
                FormStartPosition.CenterParent;

            this.BackColor = Color.WhiteSmoke;
            this.Font = new Font("Segoe UI", 10F);
            this.AutoScaleMode = AutoScaleMode.Font;

            this.MinimumSize = new Size(1000, 700);

            Label title = TaoNhan(
                "QUẢN LÝ DANH MỤC CAO ỐC",
                20, 12, 1050);

            title.Font =
                new Font("Segoe UI", 17F, FontStyle.Bold);

            title.ForeColor =
                Color.FromArgb(35, 75, 125);

            title.Height = 45;
            title.TextAlign = ContentAlignment.MiddleCenter;
            title.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right;

            this.Controls.Add(title);

            // TAB CONTROL
            tabDanhMuc = new TabControl
            {
                Location = new Point(20, 65),
                Size = new Size(1060, 620),
                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Bottom |
                    AnchorStyles.Left |
                    AnchorStyles.Right
            };

            tabTang = new TabPage("Tầng");
            tabPhong = new TabPage("Phòng");
            tabDichVu = new TabPage("Dịch vụ");

            tabTang.AutoScroll = true;
            tabPhong.AutoScroll = true;
            tabDichVu.AutoScroll = true;

            tabDanhMuc.TabPages.Add(tabTang);
            tabDanhMuc.TabPages.Add(tabPhong);
            tabDanhMuc.TabPages.Add(tabDichVu);

            this.Controls.Add(tabDanhMuc);

            btnDong = TaoNut("Đóng", 470, 700, 160);
            btnDong.Anchor =
                AnchorStyles.Bottom;

            btnDong.Click += btnDong_Click;
            this.Controls.Add(btnDong);

            // ============================================
            // TAB 1 - TẦNG
            // ============================================

            GroupBox grpTang =
                TaoNhom("Thông tin tầng", 15, 15, 1015, 150);

            grpTang.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right;

            txtMaTang = TaoText(155, 35, 260);
            txtMaTang.MaxLength = 20;
            txtTenTang = TaoText(610, 35, 360);

            nudSoTang = TaoSo(155, 85, 260, 1000);
            txtMoTaTang = TaoText(610, 85, 360);

            grpTang.Controls.AddRange(new Control[]
            {
                TaoNhan("Mã tầng:", 20, 37),
                txtMaTang,

                TaoNhan("Tên tầng:", 470, 37),
                txtTenTang,

                TaoNhan("Số thứ tự:", 20, 87),
                nudSoTang,

                TaoNhan("Mô tả:", 470, 87),
                txtMoTaTang
            });

            tabTang.Controls.Add(grpTang);

            btnThemTang = TaoNut("Thêm", 175, 180);
            btnSuaTang = TaoNut("Sửa", 315, 180);
            btnXoaTang = TaoNut("Xóa", 455, 180);
            btnLamMoiTang = TaoNut("Làm mới", 595, 180);

            btnThemTang.Click += btnThemTang_Click;
            btnSuaTang.Click += btnSuaTang_Click;
            btnXoaTang.Click += btnXoaTang_Click;
            btnLamMoiTang.Click += btnLamMoiTang_Click;

            tabTang.Controls.AddRange(new Control[]
            {
                btnThemTang, btnSuaTang,
                btnXoaTang, btnLamMoiTang
            });

            GroupBox grpDSTang =
                TaoNhom("Danh sách tầng", 15, 230, 1015, 335);

            grpDSTang.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right;

            grpDSTang.Controls.Add(
                TaoNhan("Tìm kiếm:", 15, 30, 100));

            txtTimTang = TaoText(120, 29, 310);
            btnTimTang = TaoNut("Tìm kiếm", 445, 26);

            lblTongTang =
                TaoNhan("Số tầng: 0", 750, 30, 230);

            lblTongTang.TextAlign =
                ContentAlignment.MiddleRight;

            btnTimTang.Click += btnTimTang_Click;

            Panel panelBangTang = new Panel
            {
                Location = new Point(15, 75),
                Size = new Size(980, 245),
                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Bottom |
                    AnchorStyles.Left |
                    AnchorStyles.Right
            };

            dgvTang = TaoBang();
            dgvTang.SelectionChanged +=
                dgvTang_SelectionChanged;

            panelBangTang.Controls.Add(dgvTang);

            grpDSTang.Controls.AddRange(new Control[]
            {
                txtTimTang, btnTimTang,
                lblTongTang, panelBangTang
            });

            tabTang.Controls.Add(grpDSTang);

            // ============================================
            // TAB 2 - PHÒNG
            // ============================================

            GroupBox grpPhong =
                TaoNhom("Thông tin phòng", 15, 15, 1015, 235);

            grpPhong.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right;

            txtMaPhong = TaoText(150, 32, 250);
            txtMaPhong.MaxLength = 20;

            txtTenPhong = TaoText(610, 32, 360);

            cboTangPhong = TaoCombo(150, 78, 250);
            cboHuong = TaoCombo(610, 78, 360);

            nudDienTich = TaoSo(150, 125, 250, 100000, 2);
            nudGiaThue =
                TaoSo(610, 125, 360, 100000000000M);

            cboTrangThaiPhong = TaoCombo(150, 175, 250);
            txtMoTaPhong = TaoText(610, 175, 360);

            grpPhong.Controls.AddRange(new Control[]
            {
                TaoNhan("Mã phòng:", 20, 34),
                txtMaPhong,
                TaoNhan("Tên phòng:", 470, 34),
                txtTenPhong,

                TaoNhan("Tầng:", 20, 80),
                cboTangPhong,
                TaoNhan("Hướng phòng:", 470, 80),
                cboHuong,

                TaoNhan("Diện tích (m²):", 20, 127),
                nudDienTich,
                TaoNhan("Giá thuê/tháng:", 470, 127),
                nudGiaThue,

                TaoNhan("Trạng thái:", 20, 177),
                cboTrangThaiPhong,
                TaoNhan("Mô tả:", 470, 177),
                txtMoTaPhong
            });

            tabPhong.Controls.Add(grpPhong);

            btnThemPhong = TaoNut("Thêm", 175, 260);
            btnSuaPhong = TaoNut("Sửa", 315, 260);
            btnXoaPhong = TaoNut("Xóa", 455, 260);
            btnLamMoiPhong = TaoNut("Làm mới", 595, 260);

            btnThemPhong.Click += btnThemPhong_Click;
            btnSuaPhong.Click += btnSuaPhong_Click;
            btnXoaPhong.Click += btnXoaPhong_Click;
            btnLamMoiPhong.Click += btnLamMoiPhong_Click;

            tabPhong.Controls.AddRange(new Control[]
            {
                btnThemPhong, btnSuaPhong,
                btnXoaPhong, btnLamMoiPhong
            });

            GroupBox grpDSPhong =
                TaoNhom("Danh sách phòng", 15, 310, 1015, 255);

            grpDSPhong.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right;

            grpDSPhong.Controls.Add(
                TaoNhan("Tìm kiếm:", 15, 29, 100));

            txtTimPhong = TaoText(120, 26, 310);
            btnTimPhong = TaoNut("Tìm kiếm", 445, 24);

            lblTongPhong =
                TaoNhan("Số phòng: 0", 750, 29, 230);

            lblTongPhong.TextAlign =
                ContentAlignment.MiddleRight;

            btnTimPhong.Click += btnTimPhong_Click;

            Panel panelBangPhong = new Panel
            {
                Location = new Point(15, 70),
                Size = new Size(980, 170),
                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Bottom |
                    AnchorStyles.Left |
                    AnchorStyles.Right
            };

            dgvPhong = TaoBang();
            dgvPhong.SelectionChanged +=
                dgvPhong_SelectionChanged;

            panelBangPhong.Controls.Add(dgvPhong);

            grpDSPhong.Controls.AddRange(new Control[]
            {
                txtTimPhong, btnTimPhong,
                lblTongPhong, panelBangPhong
            });

            tabPhong.Controls.Add(grpDSPhong);

            // ============================================
            // TAB 3 - DỊCH VỤ
            // ============================================

            GroupBox grpDV =
                TaoNhom("Thông tin dịch vụ", 15, 15, 1015, 205);

            grpDV.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right;

            txtMaDichVu = TaoText(155, 33, 260);
            txtMaDichVu.MaxLength = 20;

            txtTenDichVu = TaoText(610, 33, 360);
            txtDonViTinh = TaoText(155, 80, 260);
            nudDonGiaDV =
                TaoSo(610, 80, 360, 100000000000M);

            chkDangSuDung = new CheckBox
            {
                Text = "Đang sử dụng",
                Location = new Point(155, 133),
                Size = new Size(210, 28),
                Checked = true
            };

            txtMoTaDichVu = TaoText(610, 131, 360);

            grpDV.Controls.AddRange(new Control[]
            {
                TaoNhan("Mã dịch vụ:", 20, 35),
                txtMaDichVu,
                TaoNhan("Tên dịch vụ:", 470, 35),
                txtTenDichVu,

                TaoNhan("Đơn vị tính:", 20, 82),
                txtDonViTinh,
                TaoNhan("Đơn giá:", 470, 82),
                nudDonGiaDV,

                chkDangSuDung,
                TaoNhan("Mô tả:", 470, 133),
                txtMoTaDichVu
            });

            tabDichVu.Controls.Add(grpDV);

            btnThemDichVu = TaoNut("Thêm", 175, 230);
            btnSuaDichVu = TaoNut("Sửa", 315, 230);
            btnXoaDichVu = TaoNut("Xóa", 455, 230);
            btnLamMoiDichVu = TaoNut("Làm mới", 595, 230);

            btnThemDichVu.Click += btnThemDichVu_Click;
            btnSuaDichVu.Click += btnSuaDichVu_Click;
            btnXoaDichVu.Click += btnXoaDichVu_Click;
            btnLamMoiDichVu.Click +=
                btnLamMoiDichVu_Click;

            tabDichVu.Controls.AddRange(new Control[]
            {
                btnThemDichVu, btnSuaDichVu,
                btnXoaDichVu, btnLamMoiDichVu
            });

            GroupBox grpDSDV =
                TaoNhom("Danh sách dịch vụ", 15, 280, 1015, 285);

            grpDSDV.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right;

            grpDSDV.Controls.Add(
                TaoNhan("Tìm kiếm:", 15, 30, 100));

            txtTimDichVu = TaoText(120, 27, 310);
            btnTimDichVu = TaoNut("Tìm kiếm", 445, 25);

            lblTongDichVu =
                TaoNhan("Số dịch vụ: 0", 750, 30, 230);

            lblTongDichVu.TextAlign =
                ContentAlignment.MiddleRight;

            btnTimDichVu.Click += btnTimDichVu_Click;

            Panel panelBangDV = new Panel
            {
                Location = new Point(15, 75),
                Size = new Size(980, 195),
                Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Bottom |
                    AnchorStyles.Left |
                    AnchorStyles.Right
            };

            dgvDichVu = TaoBang();
            dgvDichVu.SelectionChanged +=
                dgvDichVu_SelectionChanged;

            panelBangDV.Controls.Add(dgvDichVu);

            grpDSDV.Controls.AddRange(new Control[]
            {
                txtTimDichVu, btnTimDichVu,
                lblTongDichVu, panelBangDV
            });

            tabDichVu.Controls.Add(grpDSDV);

            // ============================================
            // LOAD FORM
            // ============================================

            this.Load += FrmDanhMuc_Load;
            this.ResumeLayout(false);
        }
    }
}
