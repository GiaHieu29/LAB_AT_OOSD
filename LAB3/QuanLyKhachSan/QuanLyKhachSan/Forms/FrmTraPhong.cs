using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using QuanLyKhachSan.Services;

namespace QuanLyKhachSan.Forms
{
    public partial class FrmTraPhong : Form
    {
        readonly TraPhongService s = new TraPhongService();
        readonly DanhMucService dm = new DanhMucService();
        readonly BindingList<DenBuItem> db = new BindingList<DenBuItem>();

        ComboBox cboDat, cboNV, cboNV2, cboHT;
        DataGridView dgvPhong, dgvTN, dgvDBChon, dgvHD;
        TextBox txtPhong, txtSoDB, txtMucDo, txtSoHD, txtHDChon, txtMaTT;
        NumericUpDown numDenBu, numSoNgay, numTienTT;

        public FrmTraPhong()
        {
            InitializeComponent();
            BuildUI();
            this.Load += Frm_Load;
        }

        static Label Lb(string text) => new Label { Text = text, AutoSize = true, Margin = new Padding(6, 10, 3, 0) };
        static TextBox Tb(int w = 110) => new TextBox { Width = w, Margin = new Padding(3, 6, 10, 6) };
        static NumericUpDown Nu(decimal max, int w = 100) => new NumericUpDown { Width = w, Maximum = max, ThousandsSeparator = true, Margin = new Padding(3, 6, 10, 6) };
        static ComboBox Cb(int w = 160) => new ComboBox { Width = w, DropDownStyle = ComboBoxStyle.DropDownList, Margin = new Padding(3, 6, 10, 6) };
        static Button Bt(string text) => new Button { Text = text, AutoSize = true, Padding = new Padding(10, 4, 10, 4), Margin = new Padding(10, 5, 3, 5) };
        static DataGridView Grid(int h = 130) => new DataGridView
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
            this.Text = "Trả phòng - Đền bù - Hóa đơn - Thanh toán";
            this.ClientSize = new Size(1000, 720);
            this.StartPosition = FormStartPosition.CenterParent;

            Panel scroll = new Panel { Dock = DockStyle.Fill, AutoScroll = true };
            this.Controls.Add(scroll);
            Button btnDong = new Button { Text = "Đóng", Dock = DockStyle.Bottom, Height = 36 };
            btnDong.Click += (s2, e) => Close();
            this.Controls.Add(btnDong);
            scroll.BringToFront();

            // ---------- Chọn phiếu đang ở + phòng + tiện nghi ----------
            var boxChon = Box("Phiếu đang ở");
            var rowChon = new FlowLayoutPanel { AutoSize = true, Dock = DockStyle.Top };
            cboDat = Cb(220);
            rowChon.Controls.Add(Lb("Phiếu đang ở:")); rowChon.Controls.Add(cboDat);

            var lblPhong = new Label { Text = "Phòng thuộc phiếu (chọn 1 dòng):", AutoSize = true, Dock = DockStyle.Top, Padding = new Padding(4) };
            dgvPhong = Grid(90);
            var lblTN = new Label { Text = "Tiện nghi của phòng đã chọn:", AutoSize = true, Dock = DockStyle.Top, Padding = new Padding(4) };
            dgvTN = Grid(110);

            dgvPhong.SelectionChanged += (s2, e) =>
            {
                if (dgvPhong.CurrentRow == null) return;
                txtPhong.Text = Convert.ToString(dgvPhong.CurrentRow.Cells["SoPhong"].Value);
                dgvTN.DataSource = s.LayTienNghiPhong(txtPhong.Text);
            };

            boxChon.Controls.Add(dgvTN);
            boxChon.Controls.Add(lblTN);
            boxChon.Controls.Add(dgvPhong);
            boxChon.Controls.Add(lblPhong);
            boxChon.Controls.Add(rowChon);
            scroll.Controls.Add(boxChon);

            // ---------- Đền bù ----------
            var boxDB = Box("Đền bù tiện nghi hư hỏng / mất");
            var rowDB = new FlowLayoutPanel { AutoSize = true, Dock = DockStyle.Top };
            txtPhong = new TextBox { Width = 90, ReadOnly = true, Margin = new Padding(3, 6, 10, 6) };
            txtSoDB = Tb(); txtMucDo = Tb(140); numDenBu = Nu(100000000, 110);
            cboNV = Cb(160);
            var btnThemDB = Bt("Thêm vào phiếu đền bù");
            var btnLapDB = Bt("Lập phiếu đền bù");
            rowDB.Controls.Add(Lb("Phòng:")); rowDB.Controls.Add(txtPhong);
            rowDB.Controls.Add(Lb("Số phiếu ĐB:")); rowDB.Controls.Add(txtSoDB);
            rowDB.Controls.Add(Lb("Mức độ:")); rowDB.Controls.Add(txtMucDo);
            rowDB.Controls.Add(Lb("Số tiền:")); rowDB.Controls.Add(numDenBu);
            rowDB.Controls.Add(Lb("Nhân viên lập:")); rowDB.Controls.Add(cboNV);
            rowDB.Controls.Add(btnThemDB); rowDB.Controls.Add(btnLapDB);

            dgvDBChon = Grid(100);
            dgvDBChon.DataSource = db;

            btnThemDB.Click += (s2, e) =>
            {
                if (dgvTN.CurrentRow == null) return;
                string ma = Convert.ToString(dgvTN.CurrentRow.Cells["MaTienNghi"].Value);
                string ten = Convert.ToString(dgvTN.CurrentRow.Cells["TenLoaiTN"].Value);
                foreach (var x in db) if (x.MaTienNghi == ma) { MessageBox.Show("Tiện nghi đã có trong phiếu đền bù."); return; }
                db.Add(new DenBuItem { MaTienNghi = ma, TenLoaiTN = ten, MucDoThietHai = txtMucDo.Text.Trim(), SoTien = numDenBu.Value });
            };
            btnLapDB.Click += (s2, e) =>
            {
                var k = s.LapPhieuDenBu(txtSoDB.Text.Trim(), V(cboDat), txtPhong.Text.Trim(), DateTime.Now, V(cboNV), new List<DenBuItem>(db));
                MessageBox.Show(k.ThongBao);
                if (k.ThanhCong) db.Clear();
            };

            boxDB.Controls.Add(dgvDBChon);
            boxDB.Controls.Add(rowDB);
            scroll.Controls.Add(boxDB);

            // ---------- Hóa đơn ----------
            var boxHD = Box("Hóa đơn");
            var rowHD = new FlowLayoutPanel { AutoSize = true, Dock = DockStyle.Top };
            txtSoHD = Tb(); numSoNgay = Nu(365, 80); cboNV2 = Cb(160);
            var btnLapHD = Bt("Lập hóa đơn");
            rowHD.Controls.Add(Lb("Số hóa đơn:")); rowHD.Controls.Add(txtSoHD);
            rowHD.Controls.Add(Lb("Số ngày tính tiền:")); rowHD.Controls.Add(numSoNgay);
            rowHD.Controls.Add(Lb("Nhân viên lập:")); rowHD.Controls.Add(cboNV2);
            rowHD.Controls.Add(btnLapHD);

            dgvHD = Grid(140);
            dgvHD.SelectionChanged += (s2, e) =>
            {
                if (dgvHD.CurrentRow != null)
                    txtHDChon.Text = Convert.ToString(dgvHD.CurrentRow.Cells["SoHoaDon"].Value);
            };
            btnLapHD.Click += (s2, e) =>
            {
                var k = s.LapHoaDon(txtSoHD.Text.Trim(), V(cboDat), DateTime.Now, V(cboNV2), (int)numSoNgay.Value);
                MessageBox.Show(k.ThongBao);
                Tai();
            };

            boxHD.Controls.Add(dgvHD);
            boxHD.Controls.Add(rowHD);
            scroll.Controls.Add(boxHD);

            // ---------- Thanh toán + Trả phòng ----------
            var boxTT = Box("Thanh toán - Trả phòng");
            var rowTT = new FlowLayoutPanel { AutoSize = true, Dock = DockStyle.Top };
            txtHDChon = new TextBox { Width = 110, ReadOnly = true, Margin = new Padding(3, 6, 10, 6) };
            txtMaTT = Tb(); cboHT = Cb(140); numTienTT = Nu(1000000000, 130);
            var btnThanhToan = Bt("Thanh toán");
            var btnTraPhong = Bt("Hoàn tất trả phòng");
            rowTT.Controls.Add(Lb("Hóa đơn đang chọn:")); rowTT.Controls.Add(txtHDChon);
            rowTT.Controls.Add(Lb("Mã thanh toán:")); rowTT.Controls.Add(txtMaTT);
            rowTT.Controls.Add(Lb("Hình thức:")); rowTT.Controls.Add(cboHT);
            rowTT.Controls.Add(Lb("Số tiền:")); rowTT.Controls.Add(numTienTT);
            rowTT.Controls.Add(btnThanhToan); rowTT.Controls.Add(btnTraPhong);

            btnThanhToan.Click += (s2, e) =>
            {
                var k = s.ThanhToan(txtMaTT.Text.Trim(), txtHDChon.Text.Trim(), DateTime.Now, cboHT.Text, numTienTT.Value);
                MessageBox.Show(k.ThongBao);
                Tai();
            };
            btnTraPhong.Click += (s2, e) =>
            {
                var k = s.TraPhong(V(cboDat), DateTime.Now);
                MessageBox.Show(k.ThongBao);
                Tai();
            };

            boxTT.Controls.Add(rowTT);
            scroll.Controls.Add(boxTT);
        }

        string V(ComboBox c) => c.SelectedValue == null ? "" : c.SelectedValue.ToString();

        private void Frm_Load(object sender, EventArgs e)
        {
            cboDat.DataSource = s.LayPhieuDangO(); cboDat.DisplayMember = "SoPhieuDat"; cboDat.ValueMember = "SoPhieuDat";
            cboNV.DataSource = dm.LayNhanVien(); cboNV.DisplayMember = "HoTen"; cboNV.ValueMember = "MaNV";
            cboNV2.DataSource = dm.LayNhanVien(); cboNV2.DisplayMember = "HoTen"; cboNV2.ValueMember = "MaNV";
            cboHT.Items.AddRange(new object[] { "Tiền mặt", "Chuyển khoản", "Thẻ", "Ví điện tử" });
            cboHT.SelectedIndex = 0;
            Tai();
        }

        void Tai()
        {
            if (cboDat.SelectedValue != null)
                dgvPhong.DataSource = s.LayPhongTheoPhieu(V(cboDat));
            dgvHD.DataSource = s.LayHoaDon();
        }
    }
}
