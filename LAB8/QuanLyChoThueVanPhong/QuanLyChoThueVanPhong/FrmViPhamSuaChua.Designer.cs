
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyChoThueVanPhong
{
    partial class FrmViPhamSuaChua
    {
        private System.ComponentModel.IContainer components = null;

        private TabControl tabMain;
        private TabPage tabViPham, tabSuaChua;

        // TAB VI PHẠM
        private TextBox txtMaVP, txtNoiDungVP;
        private TextBox txtGhiChuVP, txtTimVP;
        private ComboBox cboHopDong, cboPhongThue;
        private ComboBox cboTrangThaiVP;
        private DateTimePicker dtPhatHien, dtXuLy;
        private NumericUpDown nudTienPhat;
        private DataGridView dgvViPham;
        private Label lblSoVP;
        private Button btnThemVP, btnSuaVP, btnXoaVP;
        private Button btnMoiVP, btnTimVP;

        // TAB SỬA CHỮA
        private TextBox txtMaSC, txtNoiDungSC, txtTimSC;
        private ComboBox cboPhongSC, cboKiemTra;
        private ComboBox cboTrangThaiSC;
        private DateTimePicker dtBatDauSC, dtHoanThanh;
        private NumericUpDown nudChiPhi;
        private DataGridView dgvSuaChua;
        private Label lblSoSC;
        private Button btnThemSC, btnSuaSC, btnXoaSC;
        private Button btnMoiSC, btnTimSC;

        private Button btnDong;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private Label L(string s, int x, int y, int w = 125)
        {
            return new Label
            {
                Text = s,
                Location = new Point(x, y),
                Size = new Size(w, 29),
                TextAlign = ContentAlignment.MiddleLeft
            };
        }

        private TextBox T(
            int x, int y, int w, bool multi = false)
        {
            return new TextBox
            {
                Location = new Point(x, y),
                Size = new Size(w, multi ? 48 : 29),
                Multiline = multi,
                ScrollBars = multi
                    ? ScrollBars.Vertical
                    : ScrollBars.None,
                MaxLength = multi ? 2000 : 150
            };
        }

        private ComboBox C(int x, int y, int w)
        {
            return new ComboBox
            {
                Location = new Point(x, y),
                Size = new Size(w, 29),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
        }

        private Button B(string s, int x, int y, int w = 130)
        {
            return new Button
            {
                Text = s,
                Location = new Point(x, y),
                Size = new Size(w, 38),
                Cursor = Cursors.Hand
            };
        }

        private NumericUpDown N(int x, int y, int w)
        {
            return new NumericUpDown
            {
                Location = new Point(x, y),
                Size = new Size(w, 29),
                Minimum = 0,
                Maximum = 100000000000M,
                ThousandsSeparator = true,
                DecimalPlaces = 2
            };
        }

        private DateTimePicker D(
            int x, int y, int w, bool optional = false)
        {
            return new DateTimePicker
            {
                Location = new Point(x, y),
                Size = new Size(w, 29),
                Format = DateTimePickerFormat.Short,
                ShowCheckBox = optional,
                Checked = !optional
            };
        }

        private DataGridView G()
        {
            return new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                BackgroundColor = Color.White,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                MultiSelect = false,
                AutoGenerateColumns = true,
                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect,
                ColumnHeadersHeight = 37
            };
        }

        private GroupBox Group(string title,
            int x, int y, int w, int h)
        {
            return new GroupBox
            {
                Text = title,
                Location = new Point(x, y),
                Size = new Size(w, h),
                Anchor = AnchorStyles.Top |
                         AnchorStyles.Left |
                         AnchorStyles.Right
            };
        }

        private void InitializeComponent()
        {
            SuspendLayout();

            Name = "FrmViPhamSuaChua";
            Text = "Quản lý vi phạm và sửa chữa phòng";
            ClientSize = new Size(1160, 825);
            MinimumSize = new Size(1080, 770);
            StartPosition = FormStartPosition.CenterParent;
            BackColor = Color.WhiteSmoke;
            Font = new Font("Segoe UI", 10F);
            AutoScaleMode = AutoScaleMode.Font;

            Label title = L(
                "QUẢN LÝ VI PHẠM VÀ SỬA CHỮA PHÒNG",
                20, 10, 1120);
            title.Height = 45;
            title.Font = new Font(
                "Segoe UI", 17F, FontStyle.Bold);
            title.ForeColor = Color.FromArgb(35, 75, 125);
            title.TextAlign = ContentAlignment.MiddleCenter;
            Controls.Add(title);

            tabMain = new TabControl
            {
                Location = new Point(20, 65),
                Size = new Size(1120, 690),
                Anchor = AnchorStyles.Top |
                         AnchorStyles.Bottom |
                         AnchorStyles.Left |
                         AnchorStyles.Right
            };

            tabViPham = new TabPage("Vi phạm phòng");
            tabSuaChua = new TabPage("Sửa chữa phòng");

            tabMain.TabPages.Add(tabViPham);
            tabMain.TabPages.Add(tabSuaChua);
            Controls.Add(tabMain);

            btnDong = B("Đóng", 500, 772, 160);
            btnDong.Anchor = AnchorStyles.Bottom;
            btnDong.Click += btnDong_Click;
            Controls.Add(btnDong);

            // ====================================
            // TAB 1 - VI PHẠM
            // ====================================

            GroupBox grpVP = Group(
                "Thông tin vi phạm", 15, 15, 1080, 275);

            txtMaVP = T(155, 32, 290);
            txtMaVP.MaxLength = 20;
            cboHopDong = C(690, 32, 350);

            cboPhongThue = C(155, 80, 290);
            dtPhatHien = D(690, 80, 350);

            nudTienPhat = N(155, 128, 290);
            cboTrangThaiVP = C(690, 128, 350);

            dtXuLy = D(155, 177, 290, true);
            txtNoiDungVP = T(690, 177, 350, true);

            txtGhiChuVP = T(155, 226, 885);
            txtGhiChuVP.MaxLength = 500;

            grpVP.Controls.AddRange(new Control[]
            {
                L("Mã vi phạm:", 20, 34), txtMaVP,
                L("Hợp đồng:", 540, 34), cboHopDong,
                L("Phòng thuê:", 20, 82), cboPhongThue,
                L("Ngày phát hiện:", 540, 82, 145),
                dtPhatHien,
                L("Số tiền phạt:", 20, 130), nudTienPhat,
                L("Trạng thái:", 540, 130), cboTrangThaiVP,
                L("Ngày xử lý:", 20, 179), dtXuLy,
                L("Nội dung:", 540, 179), txtNoiDungVP,
                L("Ghi chú:", 20, 228), txtGhiChuVP
            });
            tabViPham.Controls.Add(grpVP);

            btnThemVP = B("Thêm", 205, 305);
            btnSuaVP = B("Sửa", 350, 305);
            btnXoaVP = B("Xóa", 495, 305);
            btnMoiVP = B("Làm mới", 640, 305);

            btnThemVP.Click += btnThemVP_Click;
            btnSuaVP.Click += btnSuaVP_Click;
            btnXoaVP.Click += btnXoaVP_Click;
            btnMoiVP.Click += btnMoiVP_Click;

            tabViPham.Controls.AddRange(new Control[]
            {
                btnThemVP, btnSuaVP, btnXoaVP, btnMoiVP
            });

            GroupBox dsVP = Group(
                "Danh sách vi phạm", 15, 360, 1080, 285);

            dsVP.Anchor |= AnchorStyles.Bottom;

            dsVP.Controls.Add(L("Tìm kiếm:", 15, 29, 100));
            txtTimVP = T(120, 27, 350);
            btnTimVP = B("Tìm kiếm", 490, 24);

            lblSoVP = L("Số vi phạm: 0", 790, 29, 250);
            lblSoVP.TextAlign = ContentAlignment.MiddleRight;

            btnTimVP.Click += btnTimVP_Click;

            Panel pVP = new Panel
            {
                Location = new Point(15, 73),
                Size = new Size(1045, 195),
                Anchor = AnchorStyles.Top |
                         AnchorStyles.Bottom |
                         AnchorStyles.Left |
                         AnchorStyles.Right
            };

            dgvViPham = G();
            dgvViPham.SelectionChanged +=
                dgvViPham_SelectionChanged;
            pVP.Controls.Add(dgvViPham);

            dsVP.Controls.AddRange(new Control[]
            {
                txtTimVP, btnTimVP, lblSoVP, pVP
            });
            tabViPham.Controls.Add(dsVP);

            // ====================================
            // TAB 2 - SỬA CHỮA
            // ====================================

            GroupBox grpSC = Group(
                "Thông tin sửa chữa", 15, 15, 1080, 275);

            txtMaSC = T(155, 32, 290);
            txtMaSC.MaxLength = 20;
            cboPhongSC = C(690, 32, 350);

            cboKiemTra = C(155, 80, 290);
            dtBatDauSC = D(690, 80, 350);

            dtHoanThanh = D(155, 128, 290, true);
            nudChiPhi = N(690, 128, 350);

            cboTrangThaiSC = C(155, 178, 290);
            txtNoiDungSC = T(690, 178, 350, true);

            grpSC.Controls.AddRange(new Control[]
            {
                L("Mã sửa chữa:", 20, 34), txtMaSC,
                L("Phòng:", 540, 34), cboPhongSC,
                L("Phiếu kiểm tra:", 20, 82), cboKiemTra,
                L("Ngày bắt đầu:", 540, 82, 145),
                dtBatDauSC,
                L("Ngày hoàn thành:", 20, 130, 145),
                dtHoanThanh,
                L("Chi phí:", 540, 130), nudChiPhi,
                L("Trạng thái:", 20, 180), cboTrangThaiSC,
                L("Nội dung sửa:", 540, 180), txtNoiDungSC
            });
            tabSuaChua.Controls.Add(grpSC);

            btnThemSC = B("Thêm", 205, 305);
            btnSuaSC = B("Sửa", 350, 305);
            btnXoaSC = B("Xóa", 495, 305);
            btnMoiSC = B("Làm mới", 640, 305);

            btnThemSC.Click += btnThemSC_Click;
            btnSuaSC.Click += btnSuaSC_Click;
            btnXoaSC.Click += btnXoaSC_Click;
            btnMoiSC.Click += btnMoiSC_Click;

            tabSuaChua.Controls.AddRange(new Control[]
            {
                btnThemSC, btnSuaSC, btnXoaSC, btnMoiSC
            });

            GroupBox dsSC = Group(
                "Danh sách sửa chữa", 15, 360, 1080, 285);

            dsSC.Anchor |= AnchorStyles.Bottom;

            dsSC.Controls.Add(L("Tìm kiếm:", 15, 29, 100));
            txtTimSC = T(120, 27, 350);
            btnTimSC = B("Tìm kiếm", 490, 24);

            lblSoSC = L("Số sửa chữa: 0", 790, 29, 250);
            lblSoSC.TextAlign = ContentAlignment.MiddleRight;

            btnTimSC.Click += btnTimSC_Click;

            Panel pSC = new Panel
            {
                Location = new Point(15, 73),
                Size = new Size(1045, 195),
                Anchor = AnchorStyles.Top |
                         AnchorStyles.Bottom |
                         AnchorStyles.Left |
                         AnchorStyles.Right
            };

            dgvSuaChua = G();
            dgvSuaChua.SelectionChanged +=
                dgvSuaChua_SelectionChanged;
            pSC.Controls.Add(dgvSuaChua);

            dsSC.Controls.AddRange(new Control[]
            {
                txtTimSC, btnTimSC, lblSoSC, pSC
            });
            tabSuaChua.Controls.Add(dsSC);

            // ====================================
            // EVENTS
            // ====================================

            cboHopDong.SelectedIndexChanged +=
                cboHopDong_SelectedIndexChanged;

            cboPhongSC.SelectedIndexChanged +=
                cboPhongSC_SelectedIndexChanged;

            Load += FrmViPhamSuaChua_Load;

            ResumeLayout(false);
        }
    }
}
