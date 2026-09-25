using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyKhachSan.Services;

namespace QuanLyKhachSan.Forms
{
    public partial class FrmPhongTienNghi : Form
    {
        readonly PhongTienNghiService s = new PhongTienNghiService();
        readonly DanhMucService dm = new DanhMucService();

        // Phòng
        TextBox txtPhong; ComboBox cboKhu; NumericUpDown numMax, numGia; DataGridView dgvPhong;

        // Tiện nghi
        TextBox txtMaTN, txtTinhTrang; ComboBox cboLoai; NumericUpDown numSTT; DataGridView dgvTN;

        // Lắp đặt / luân chuyển
        TextBox txtSoLD, txtTTLD, txtGhiChu;
        ComboBox cboTN, cboPhong, cboNV;
        DateTimePicker dtNgay;
        DataGridView dgvLD;

        public FrmPhongTienNghi()
        {
            InitializeComponent();
            BuildUI();
            this.Load += Frm_Load;
        }

        static Label Lb(string text) => new Label { Text = text, AutoSize = true, Margin = new Padding(6, 10, 3, 0) };
        static TextBox Tb(int w = 130) => new TextBox { Width = w, Margin = new Padding(3, 6, 12, 6) };
        static NumericUpDown Nu(decimal max, int w = 100) => new NumericUpDown { Width = w, Maximum = max, ThousandsSeparator = true, Margin = new Padding(3, 6, 12, 6) };
        static ComboBox Cb(int w = 150) => new ComboBox { Width = w, DropDownStyle = ComboBoxStyle.DropDownList, Margin = new Padding(3, 6, 12, 6) };
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
            top.Dock = DockStyle.Fill; grid.Dock = DockStyle.Fill;
            t.Controls.Add(top, 0, 0); t.Controls.Add(grid, 0, 1);
            return t;
        }

        void BuildUI()
        {
            this.AutoScaleMode = AutoScaleMode.None;
            this.Text = "Phòng - Tiện nghi - Phiếu lắp đặt";
            this.ClientSize = new Size(950, 580);
            this.StartPosition = FormStartPosition.CenterParent;

            TabControl tabs = new TabControl { Dock = DockStyle.Fill };
            this.Controls.Add(tabs);
            Button btnDong = new Button { Text = "Đóng", Dock = DockStyle.Bottom, Height = 36 };
            btnDong.Click += (s2, e) => Close();
            this.Controls.Add(btnDong);
            tabs.BringToFront();

            // --- Tab Phòng ---
            var pPhong = new TabPage("Phòng");
            var rowPhong = new FlowLayoutPanel { AutoSize = true, Padding = new Padding(8) };
            txtPhong = Tb(); cboKhu = Cb(140); numMax = Nu(50, 70); numGia = Nu(100000000);
            var btnThemPhong = Bt("Thêm phòng");
            btnThemPhong.Click += (s2, e) => H(s.ThemPhong(txtPhong.Text.Trim(),
                cboKhu.SelectedValue == null ? "" : cboKhu.SelectedValue.ToString(),
                (int)numMax.Value, numGia.Value));
            rowPhong.Controls.Add(Lb("Số phòng:")); rowPhong.Controls.Add(txtPhong);
            rowPhong.Controls.Add(Lb("Khu vực:")); rowPhong.Controls.Add(cboKhu);
            rowPhong.Controls.Add(Lb("Số người tối đa:")); rowPhong.Controls.Add(numMax);
            rowPhong.Controls.Add(Lb("Đơn giá/ngày:")); rowPhong.Controls.Add(numGia);
            rowPhong.Controls.Add(btnThemPhong);
            dgvPhong = Grid();
            pPhong.Controls.Add(Wrap(rowPhong, dgvPhong));
            tabs.TabPages.Add(pPhong);

            // --- Tab Tiện nghi ---
            var pTN = new TabPage("Tiện nghi");
            var rowTN = new FlowLayoutPanel { AutoSize = true, Padding = new Padding(8) };
            txtMaTN = Tb(); cboLoai = Cb(140); numSTT = Nu(999, 70); txtTinhTrang = Tb(160);
            var btnThemTN = Bt("Thêm tiện nghi");
            btnThemTN.Click += (s2, e) => H(s.ThemTienNghi(txtMaTN.Text.Trim(),
                cboLoai.SelectedValue == null ? "" : cboLoai.SelectedValue.ToString(),
                (int)numSTT.Value, txtTinhTrang.Text.Trim()));
            rowTN.Controls.Add(Lb("Mã TN:")); rowTN.Controls.Add(txtMaTN);
            rowTN.Controls.Add(Lb("Loại TN:")); rowTN.Controls.Add(cboLoai);
            rowTN.Controls.Add(Lb("Số thứ tự:")); rowTN.Controls.Add(numSTT);
            rowTN.Controls.Add(Lb("Tình trạng:")); rowTN.Controls.Add(txtTinhTrang);
            rowTN.Controls.Add(btnThemTN);
            dgvTN = Grid();
            pTN.Controls.Add(Wrap(rowTN, dgvTN));
            tabs.TabPages.Add(pTN);

            // --- Tab Lắp đặt / luân chuyển ---
            var pLD = new TabPage("Lắp đặt / luân chuyển");
            var rowLD = new FlowLayoutPanel { AutoSize = true, Padding = new Padding(8) };
            txtSoLD = Tb(); cboTN = Cb(120); cboPhong = Cb(110);
            dtNgay = new DateTimePicker { Width = 120, Format = DateTimePickerFormat.Short, Margin = new Padding(3, 6, 12, 6) };
            txtTTLD = Tb(140); cboNV = Cb(150); txtGhiChu = Tb(160);
            var btnLapDat = Bt("Lập phiếu");
            btnLapDat.Click += (s2, e) => H(s.LapDat(txtSoLD.Text.Trim(),
                cboTN.SelectedValue == null ? "" : cboTN.SelectedValue.ToString(),
                cboPhong.SelectedValue == null ? "" : cboPhong.SelectedValue.ToString(),
                dtNgay.Value, txtTTLD.Text.Trim(),
                cboNV.SelectedValue == null ? "" : cboNV.SelectedValue.ToString(),
                txtGhiChu.Text.Trim()));
            rowLD.Controls.Add(Lb("Số phiếu:")); rowLD.Controls.Add(txtSoLD);
            rowLD.Controls.Add(Lb("Tiện nghi:")); rowLD.Controls.Add(cboTN);
            rowLD.Controls.Add(Lb("Phòng:")); rowLD.Controls.Add(cboPhong);
            rowLD.Controls.Add(Lb("Ngày lắp:")); rowLD.Controls.Add(dtNgay);
            rowLD.Controls.Add(Lb("Tình trạng:")); rowLD.Controls.Add(txtTTLD);
            rowLD.Controls.Add(Lb("Nhân viên:")); rowLD.Controls.Add(cboNV);
            rowLD.Controls.Add(Lb("Ghi chú:")); rowLD.Controls.Add(txtGhiChu);
            rowLD.Controls.Add(btnLapDat);
            dgvLD = Grid();
            pLD.Controls.Add(Wrap(rowLD, dgvLD));
            tabs.TabPages.Add(pLD);
        }

        private void Frm_Load(object sender, EventArgs e)
        {
            cboKhu.DataSource = dm.LayKhuVuc(); cboKhu.DisplayMember = "TenKhuVuc"; cboKhu.ValueMember = "MaKhuVuc";
            cboLoai.DataSource = dm.LayLoaiTienNghi(); cboLoai.DisplayMember = "TenLoaiTN"; cboLoai.ValueMember = "MaLoaiTN";
            cboTN.DataSource = s.LayTienNghi(); cboTN.DisplayMember = "MaTienNghi"; cboTN.ValueMember = "MaTienNghi";
            cboPhong.DataSource = s.LayPhong(); cboPhong.DisplayMember = "SoPhong"; cboPhong.ValueMember = "SoPhong";
            cboNV.DataSource = dm.LayNhanVien(); cboNV.DisplayMember = "HoTen"; cboNV.ValueMember = "MaNV";
            Tai();
        }

        void Tai()
        {
            dgvPhong.DataSource = s.LayPhong();
            dgvTN.DataSource = s.LayTienNghi();
            dgvLD.DataSource = s.LayLapDat();
        }

        void H(KetQuaXuLy k)
        {
            MessageBox.Show(k.ThongBao);
            if (k.ThanhCong) Tai();
        }
    }
}
