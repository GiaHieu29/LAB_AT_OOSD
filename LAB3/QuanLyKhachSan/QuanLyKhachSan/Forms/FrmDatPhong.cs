using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using QuanLyKhachSan.Services;

namespace QuanLyKhachSan.Forms
{
    public partial class FrmDatPhong : Form
    {
        readonly DatPhongService s = new DatPhongService();
        readonly DanhMucService dm = new DanhMucService();
        readonly BindingList<PhongDatItem> chon = new BindingList<PhongDatItem>();

        // Khách hàng
        TextBox txtMaKH, txtTenKH, txtCMND, txtQT, txtSDT;
        DataGridView dgvKhach;

        // Đầu phiếu đặt
        TextBox txtSoPhieu;
        ComboBox cboKhach, cboNV, cboKenh;
        DateTimePicker dtLap, dtNhan, dtTra;
        NumericUpDown numCoc;

        // Chọn phòng
        DataGridView dgvPhong, dgvChon;
        NumericUpDown numSoNguoi;

        // Danh sách phiếu đặt + chi tiết
        DataGridView dgvPhieu, dgvCT, dgvNguoi;
        TextBox txtPhieuChon, txtNguoiPhong, txtNguoiTen, txtNguoiCMND, txtNguoiQT;

        public FrmDatPhong()
        {
            InitializeComponent();
            BuildUI();
            this.Load += Frm_Load;
        }

        static Label Lb(string text) => new Label { Text = text, AutoSize = true, Margin = new Padding(6, 10, 3, 0) };
        static TextBox Tb(int w = 120) => new TextBox { Width = w, Margin = new Padding(3, 6, 10, 6) };
        static NumericUpDown Nu(decimal max, int w = 90) => new NumericUpDown { Width = w, Maximum = max, ThousandsSeparator = true, Margin = new Padding(3, 6, 10, 6) };
        static ComboBox Cb(int w = 150) => new ComboBox { Width = w, DropDownStyle = ComboBoxStyle.DropDownList, Margin = new Padding(3, 6, 10, 6) };
        static DateTimePicker Dt(int w = 110) => new DateTimePicker { Width = w, Format = DateTimePickerFormat.Short, Margin = new Padding(3, 6, 10, 6) };
        static Button Bt(string text) => new Button { Text = text, AutoSize = true, Padding = new Padding(10, 4, 10, 4), Margin = new Padding(10, 5, 3, 5) };
        static DataGridView Grid(int h = 150) => new DataGridView
        {
            Width = 1, Height = h, Dock = DockStyle.Top,
            ReadOnly = true, AllowUserToAddRows = false, AllowUserToDeleteRows = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            BackgroundColor = Color.White
        };
        static GroupBox Box(string title) => new GroupBox { Text = title, Dock = DockStyle.Top, AutoSize = true, Padding = new Padding(8) };

        void BuildUI()
        {
            this.AutoScaleMode = AutoScaleMode.None;
            this.Text = "Khách hàng - Đặt phòng - Nhận phòng";
            this.ClientSize = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterParent;

            Panel scroll = new Panel { Dock = DockStyle.Fill, AutoScroll = true };
            this.Controls.Add(scroll);
            Button btnDong = new Button { Text = "Đóng", Dock = DockStyle.Bottom, Height = 36 };
            btnDong.Click += (s2, e) => Close();
            this.Controls.Add(btnDong);
            scroll.BringToFront();

            // ---------- Khách hàng ----------
            var boxKhach = Box("Khách hàng");
            var rowKhach = new FlowLayoutPanel { AutoSize = true, Dock = DockStyle.Top };
            txtMaKH = Tb(); txtTenKH = Tb(150); txtCMND = Tb(110); txtQT = Tb(100); txtSDT = Tb(110);
            var btnThemKhach = Bt("Thêm khách");
            btnThemKhach.Click += (s2, e) => H(s.ThemKhach(txtMaKH.Text.Trim(), txtTenKH.Text.Trim(), txtCMND.Text.Trim(), txtQT.Text.Trim(), txtSDT.Text.Trim()));
            rowKhach.Controls.Add(Lb("Mã khách:")); rowKhach.Controls.Add(txtMaKH);
            rowKhach.Controls.Add(Lb("Họ tên:")); rowKhach.Controls.Add(txtTenKH);
            rowKhach.Controls.Add(Lb("CMND:")); rowKhach.Controls.Add(txtCMND);
            rowKhach.Controls.Add(Lb("Quốc tịch:")); rowKhach.Controls.Add(txtQT);
            rowKhach.Controls.Add(Lb("SĐT:")); rowKhach.Controls.Add(txtSDT);
            rowKhach.Controls.Add(btnThemKhach);
            dgvKhach = Grid(130);
            boxKhach.Controls.Add(dgvKhach);
            boxKhach.Controls.Add(rowKhach);
            scroll.Controls.Add(boxKhach);

            // ---------- Đặt phòng ----------
            var boxDat = Box("Đặt phòng");
            var rowDat1 = new FlowLayoutPanel { AutoSize = true, Dock = DockStyle.Top };
            txtSoPhieu = Tb(); cboKhach = Cb(160); cboNV = Cb(150); cboKenh = Cb(120);
            rowDat1.Controls.Add(Lb("Số phiếu:")); rowDat1.Controls.Add(txtSoPhieu);
            rowDat1.Controls.Add(Lb("Khách:")); rowDat1.Controls.Add(cboKhach);
            rowDat1.Controls.Add(Lb("Lễ tân:")); rowDat1.Controls.Add(cboNV);
            rowDat1.Controls.Add(Lb("Kênh đặt:")); rowDat1.Controls.Add(cboKenh);

            var rowDat2 = new FlowLayoutPanel { AutoSize = true, Dock = DockStyle.Top };
            dtLap = Dt(); dtNhan = Dt(); dtTra = Dt(); numCoc = Nu(1000000000, 120);
            rowDat2.Controls.Add(Lb("Ngày lập:")); rowDat2.Controls.Add(dtLap);
            rowDat2.Controls.Add(Lb("Ngày nhận:")); rowDat2.Controls.Add(dtNhan);
            rowDat2.Controls.Add(Lb("Ngày trả dự kiến:")); rowDat2.Controls.Add(dtTra);
            rowDat2.Controls.Add(Lb("Tiền cọc:")); rowDat2.Controls.Add(numCoc);

            var rowChonPhong = new FlowLayoutPanel { AutoSize = true, Dock = DockStyle.Top };
            numSoNguoi = Nu(20, 70);
            var btnThemPhong = Bt("Thêm phòng vào phiếu");
            var btnBoPhong = Bt("Bỏ phòng đã chọn");
            var btnLapPhieu = Bt("Lập phiếu đặt");
            rowChonPhong.Controls.Add(Lb("Số người ở phòng chọn:")); rowChonPhong.Controls.Add(numSoNguoi);
            rowChonPhong.Controls.Add(btnThemPhong); rowChonPhong.Controls.Add(btnBoPhong); rowChonPhong.Controls.Add(btnLapPhieu);

            var lblPhongTrong = new Label { Text = "Danh sách phòng:", AutoSize = true, Dock = DockStyle.Top, Padding = new Padding(4) };
            dgvPhong = Grid(140);
            var lblPhongChon = new Label { Text = "Phòng đã chọn cho phiếu:", AutoSize = true, Dock = DockStyle.Top, Padding = new Padding(4) };
            dgvChon = Grid(110);
            dgvChon.DataSource = chon;

            btnThemPhong.Click += (s2, e) =>
            {
                if (dgvPhong.CurrentRow == null) return;
                string p = Convert.ToString(dgvPhong.CurrentRow.Cells["SoPhong"].Value);
                foreach (var x in chon) if (x.SoPhong == p) { MessageBox.Show("Phòng đã có trong phiếu."); return; }
                decimal g = Convert.ToDecimal(dgvPhong.CurrentRow.Cells["DonGiaNgay"].Value);
                chon.Add(new PhongDatItem { SoPhong = p, SoNguoi = (int)numSoNguoi.Value, DonGiaNgay = g });
            };
            btnBoPhong.Click += (s2, e) =>
            {
                if (dgvChon.CurrentRow != null && dgvChon.CurrentRow.Index >= 0 && dgvChon.CurrentRow.Index < chon.Count)
                    chon.RemoveAt(dgvChon.CurrentRow.Index);
            };
            btnLapPhieu.Click += (s2, e) =>
            {
                var k = s.TaoDatPhong(txtSoPhieu.Text.Trim(), V(cboKhach), V(cboNV), dtLap.Value, dtNhan.Value, dtTra.Value,
                    numCoc.Value, cboKenh.Text, new List<PhongDatItem>(chon));
                MessageBox.Show(k.ThongBao);
                if (k.ThanhCong) { chon.Clear(); Tai(); }
            };

            boxDat.Controls.Add(dgvChon);
            boxDat.Controls.Add(lblPhongChon);
            boxDat.Controls.Add(dgvPhong);
            boxDat.Controls.Add(lblPhongTrong);
            boxDat.Controls.Add(rowChonPhong);
            boxDat.Controls.Add(rowDat2);
            boxDat.Controls.Add(rowDat1);
            scroll.Controls.Add(boxDat);

            // ---------- Danh sách phiếu đặt ----------
            var boxPhieu = Box("Danh sách phiếu đặt phòng (chọn 1 dòng để xem chi tiết / nhận phòng)");
            dgvPhieu = Grid(140);
            dgvPhieu.SelectionChanged += dgvPhieu_SelectionChanged;
            boxPhieu.Controls.Add(dgvPhieu);
            scroll.Controls.Add(boxPhieu);

            // ---------- Người lưu trú / Nhận phòng ----------
            var boxNguoi = Box("Nhận phòng - Người lưu trú");
            var rowNguoi1 = new FlowLayoutPanel { AutoSize = true, Dock = DockStyle.Top };
            txtPhieuChon = new TextBox { Width = 120, ReadOnly = true, Margin = new Padding(3, 6, 10, 6) };
            txtNguoiPhong = Tb(90); txtNguoiTen = Tb(140); txtNguoiCMND = Tb(110); txtNguoiQT = Tb(100);
            var btnThemNguoi = Bt("Thêm người lưu trú");
            rowNguoi1.Controls.Add(Lb("Phiếu đang chọn:")); rowNguoi1.Controls.Add(txtPhieuChon);
            rowNguoi1.Controls.Add(Lb("Phòng:")); rowNguoi1.Controls.Add(txtNguoiPhong);
            rowNguoi1.Controls.Add(Lb("Họ tên:")); rowNguoi1.Controls.Add(txtNguoiTen);
            rowNguoi1.Controls.Add(Lb("CMND:")); rowNguoi1.Controls.Add(txtNguoiCMND);
            rowNguoi1.Controls.Add(Lb("Quốc tịch:")); rowNguoi1.Controls.Add(txtNguoiQT);
            rowNguoi1.Controls.Add(btnThemNguoi);
            btnThemNguoi.Click += (s2, e) => H(s.ThemNguoiLuuTru(txtPhieuChon.Text.Trim(), txtNguoiPhong.Text.Trim(),
                txtNguoiTen.Text.Trim(), txtNguoiCMND.Text.Trim(), txtNguoiQT.Text.Trim()));

            var rowNguoi2 = new FlowLayoutPanel { AutoSize = true, Dock = DockStyle.Top };
            var btnNhanPhong = Bt("Nhận phòng");
            var btnNoShow = Bt("Đánh dấu No-show");
            btnNhanPhong.Click += (s2, e) => H(s.NhanPhong(txtPhieuChon.Text.Trim(), DateTime.Now));
            btnNoShow.Click += (s2, e) => H(s.DanhDauNoShow(txtPhieuChon.Text.Trim()));
            rowNguoi2.Controls.Add(btnNhanPhong); rowNguoi2.Controls.Add(btnNoShow);

            var lblCT = new Label { Text = "Chi tiết phòng của phiếu:", AutoSize = true, Dock = DockStyle.Top, Padding = new Padding(4) };
            dgvCT = Grid(100);
            var lblNguoi = new Label { Text = "Người lưu trú:", AutoSize = true, Dock = DockStyle.Top, Padding = new Padding(4) };
            dgvNguoi = Grid(120);

            boxNguoi.Controls.Add(dgvNguoi);
            boxNguoi.Controls.Add(lblNguoi);
            boxNguoi.Controls.Add(dgvCT);
            boxNguoi.Controls.Add(lblCT);
            boxNguoi.Controls.Add(rowNguoi2);
            boxNguoi.Controls.Add(rowNguoi1);
            scroll.Controls.Add(boxNguoi);
        }

        string V(ComboBox c) => c.SelectedValue == null ? "" : c.SelectedValue.ToString();

        private void Frm_Load(object sender, EventArgs e)
        {
            cboKhach.DataSource = s.LayKhach(); cboKhach.DisplayMember = "HoTen"; cboKhach.ValueMember = "MaKhach";
            cboNV.DataSource = dm.LayNhanVien(); cboNV.DisplayMember = "HoTen"; cboNV.ValueMember = "MaNV";
            cboKenh.Items.AddRange(new object[] { "Điện thoại", "Website", "Trực tiếp" });
            cboKenh.SelectedIndex = 0;
            Tai();
        }

        void Tai()
        {
            dgvKhach.DataSource = s.LayKhach();
            dgvPhong.DataSource = s.LayPhong();
            dgvPhieu.DataSource = s.LayPhieuDat();
        }

        void H(KetQuaXuLy k)
        {
            MessageBox.Show(k.ThongBao);
            if (k.ThanhCong) Tai();
        }

        private void dgvPhieu_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPhieu.CurrentRow == null) return;
            string so = Convert.ToString(dgvPhieu.CurrentRow.Cells["SoPhieuDat"].Value);
            txtPhieuChon.Text = so;
            dgvCT.DataSource = s.LayChiTiet(so);
            dgvNguoi.DataSource = s.LayNguoiLuuTru(so);
        }
    }
}
