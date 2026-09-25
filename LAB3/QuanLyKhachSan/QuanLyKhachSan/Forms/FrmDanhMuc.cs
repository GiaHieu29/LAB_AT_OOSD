using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyKhachSan.Services;

namespace QuanLyKhachSan.Forms
{
    public partial class FrmDanhMuc : Form
    {
        readonly DanhMucService s = new DanhMucService();

        // Khu vực
        TextBox txtKhuMa, txtKhuTen;
        DataGridView dgvKhu;

        // Nhân viên
        TextBox txtNVMa, txtNVTen, txtNVVaiTro, txtNVSDT;
        DataGridView dgvNV;

        // Loại tiện nghi
        TextBox txtLoaiMa, txtLoaiTen;
        DataGridView dgvLoaiTN;

        // Dịch vụ
        TextBox txtDVMa, txtDVTen, txtDVDVT;
        NumericUpDown numDVGia;
        DataGridView dgvDV;

        // Quy định đền bù
        TextBox txtQDMa, txtQDMucDo;
        ComboBox cboQDLoai;
        NumericUpDown numQDTien;
        DataGridView dgvQD;

        public FrmDanhMuc()
        {
            InitializeComponent();
            BuildUI();
            this.Load += FrmDanhMuc_Load;
        }

        // ---------- Helpers dựng UI ----------
        static Label Lb(string text) => new Label { Text = text, AutoSize = true, Margin = new Padding(6, 10, 3, 0) };
        static TextBox Tb(int w = 130) => new TextBox { Width = w, Margin = new Padding(3, 6, 12, 6) };
        static NumericUpDown Nu(decimal max, int w = 110) => new NumericUpDown { Width = w, Maximum = max, ThousandsSeparator = true, Margin = new Padding(3, 6, 12, 6) };
        static Button Bt(string text) => new Button { Text = text, AutoSize = true, Padding = new Padding(10, 4, 10, 4), Margin = new Padding(10, 5, 3, 5) };
        static DataGridView Grid() => new DataGridView
        {
            Dock = DockStyle.Fill,
            ReadOnly = true,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            BackgroundColor = Color.White
        };

        static TableLayoutPanel Wrap(Control top, Control grid)
        {
            var t = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2 };
            t.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            t.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            top.Dock = DockStyle.Fill;
            grid.Dock = DockStyle.Fill;
            t.Controls.Add(top, 0, 0);
            t.Controls.Add(grid, 0, 1);
            return t;
        }

        void BuildUI()
        {
            this.AutoScaleMode = AutoScaleMode.None;
            this.Text = "Danh mục khách sạn";
            this.ClientSize = new Size(900, 560);
            this.StartPosition = FormStartPosition.CenterParent;

            TabControl tabs = new TabControl { Dock = DockStyle.Fill };
            this.Controls.Add(tabs);

            Button btnDong = new Button { Text = "Đóng", Dock = DockStyle.Bottom, Height = 36 };
            btnDong.Click += (s2, e) => Close();
            this.Controls.Add(btnDong);
            tabs.BringToFront();

            // --- Tab Khu vực ---
            var pKhu = new TabPage("Khu vực");
            var rowKhu = new FlowLayoutPanel { AutoSize = true, Padding = new Padding(8) };
            txtKhuMa = Tb(); txtKhuTen = Tb(180);
            var btnThemKhu = Bt("Thêm");
            btnThemKhu.Click += (s2, e) => H(s.ThemKhu(txtKhuMa.Text.Trim(), txtKhuTen.Text.Trim()));
            rowKhu.Controls.Add(Lb("Mã khu:")); rowKhu.Controls.Add(txtKhuMa);
            rowKhu.Controls.Add(Lb("Tên khu:")); rowKhu.Controls.Add(txtKhuTen);
            rowKhu.Controls.Add(btnThemKhu);
            dgvKhu = Grid();
            pKhu.Controls.Add(Wrap(rowKhu, dgvKhu));
            tabs.TabPages.Add(pKhu);

            // --- Tab Nhân viên ---
            var pNV = new TabPage("Nhân viên");
            var rowNV = new FlowLayoutPanel { AutoSize = true, Padding = new Padding(8) };
            txtNVMa = Tb(); txtNVTen = Tb(160); txtNVVaiTro = Tb(120); txtNVSDT = Tb(120);
            var btnThemNV = Bt("Thêm");
            btnThemNV.Click += (s2, e) => H(s.ThemNhanVien(txtNVMa.Text.Trim(), txtNVTen.Text.Trim(), txtNVVaiTro.Text.Trim(), txtNVSDT.Text.Trim()));
            rowNV.Controls.Add(Lb("Mã NV:")); rowNV.Controls.Add(txtNVMa);
            rowNV.Controls.Add(Lb("Họ tên:")); rowNV.Controls.Add(txtNVTen);
            rowNV.Controls.Add(Lb("Vai trò:")); rowNV.Controls.Add(txtNVVaiTro);
            rowNV.Controls.Add(Lb("SĐT:")); rowNV.Controls.Add(txtNVSDT);
            rowNV.Controls.Add(btnThemNV);
            dgvNV = Grid();
            pNV.Controls.Add(Wrap(rowNV, dgvNV));
            tabs.TabPages.Add(pNV);

            // --- Tab Loại tiện nghi ---
            var pLoai = new TabPage("Loại tiện nghi");
            var rowLoai = new FlowLayoutPanel { AutoSize = true, Padding = new Padding(8) };
            txtLoaiMa = Tb(); txtLoaiTen = Tb(180);
            var btnThemLoaiTN = Bt("Thêm");
            btnThemLoaiTN.Click += (s2, e) => H(s.ThemLoaiTN(txtLoaiMa.Text.Trim(), txtLoaiTen.Text.Trim()));
            rowLoai.Controls.Add(Lb("Mã loại:")); rowLoai.Controls.Add(txtLoaiMa);
            rowLoai.Controls.Add(Lb("Tên loại:")); rowLoai.Controls.Add(txtLoaiTen);
            rowLoai.Controls.Add(btnThemLoaiTN);
            dgvLoaiTN = Grid();
            pLoai.Controls.Add(Wrap(rowLoai, dgvLoaiTN));
            tabs.TabPages.Add(pLoai);

            // --- Tab Dịch vụ ---
            var pDV = new TabPage("Dịch vụ");
            var rowDV = new FlowLayoutPanel { AutoSize = true, Padding = new Padding(8) };
            txtDVMa = Tb(); txtDVTen = Tb(160); txtDVDVT = Tb(100); numDVGia = Nu(100000000);
            var btnThemDV = Bt("Thêm");
            btnThemDV.Click += (s2, e) => H(s.ThemDichVu(txtDVMa.Text.Trim(), txtDVTen.Text.Trim(), txtDVDVT.Text.Trim(), numDVGia.Value));
            rowDV.Controls.Add(Lb("Mã DV:")); rowDV.Controls.Add(txtDVMa);
            rowDV.Controls.Add(Lb("Tên DV:")); rowDV.Controls.Add(txtDVTen);
            rowDV.Controls.Add(Lb("Đơn vị tính:")); rowDV.Controls.Add(txtDVDVT);
            rowDV.Controls.Add(Lb("Đơn giá:")); rowDV.Controls.Add(numDVGia);
            rowDV.Controls.Add(btnThemDV);
            dgvDV = Grid();
            pDV.Controls.Add(Wrap(rowDV, dgvDV));
            tabs.TabPages.Add(pDV);

            // --- Tab Quy định đền bù ---
            var pQD = new TabPage("Quy định đền bù");
            var rowQD = new FlowLayoutPanel { AutoSize = true, Padding = new Padding(8) };
            txtQDMa = Tb(); cboQDLoai = new ComboBox { Width = 160, DropDownStyle = ComboBoxStyle.DropDownList, Margin = new Padding(3, 6, 12, 6) };
            txtQDMucDo = Tb(140); numQDTien = Nu(100000000);
            var btnThemQD = Bt("Thêm");
            btnThemQD.Click += (s2, e) => H(s.ThemQuyDinh(txtQDMa.Text.Trim(),
                cboQDLoai.SelectedValue == null ? "" : cboQDLoai.SelectedValue.ToString(),
                txtQDMucDo.Text.Trim(), numQDTien.Value));
            rowQD.Controls.Add(Lb("Mã QĐ:")); rowQD.Controls.Add(txtQDMa);
            rowQD.Controls.Add(Lb("Loại TN:")); rowQD.Controls.Add(cboQDLoai);
            rowQD.Controls.Add(Lb("Mức độ:")); rowQD.Controls.Add(txtQDMucDo);
            rowQD.Controls.Add(Lb("Mức đền bù:")); rowQD.Controls.Add(numQDTien);
            rowQD.Controls.Add(btnThemQD);
            dgvQD = Grid();
            pQD.Controls.Add(Wrap(rowQD, dgvQD));
            tabs.TabPages.Add(pQD);
        }

        private void FrmDanhMuc_Load(object sender, EventArgs e)
        {
            Tai();
        }

        void Tai()
        {
            dgvKhu.DataSource = s.LayKhuVuc();
            dgvNV.DataSource = s.LayNhanVien();
            dgvLoaiTN.DataSource = s.LayLoaiTienNghi();
            dgvDV.DataSource = s.LayDichVu();
            dgvQD.DataSource = s.LayQuyDinhDenBu();
            cboQDLoai.DataSource = s.LayLoaiTienNghi();
            cboQDLoai.DisplayMember = "TenLoaiTN";
            cboQDLoai.ValueMember = "MaLoaiTN";
        }

        void H(KetQuaXuLy k)
        {
            MessageBox.Show(k.ThongBao);
            if (k.ThanhCong) Tai();
        }
    }
}
