using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyKhachSan.Services;

namespace QuanLyKhachSan.Forms
{
    public partial class FrmDichVu : Form
    {
        readonly DichVuService s = new DichVuService();
        readonly DanhMucService dm = new DanhMucService();

        ComboBox cboLuot, cboDV, cboNV;
        TextBox txtPhong;
        DateTimePicker dtNgay;
        NumericUpDown numSL;
        DataGridView dgvLichSu;

        public FrmDichVu()
        {
            InitializeComponent();
            BuildUI();
            this.Load += Frm_Load;
        }

        static Label Lb(string text) => new Label { Text = text, AutoSize = true, Margin = new Padding(6, 10, 3, 0) };
        static ComboBox Cb(int w = 170) => new ComboBox { Width = w, DropDownStyle = ComboBoxStyle.DropDownList, Margin = new Padding(3, 6, 12, 6) };
        static Button Bt(string text) => new Button { Text = text, AutoSize = true, Padding = new Padding(10, 4, 10, 4), Margin = new Padding(10, 5, 3, 5) };

        void BuildUI()
        {
            this.AutoScaleMode = AutoScaleMode.None;
            this.Text = "Sử dụng dịch vụ";
            this.ClientSize = new Size(850, 500);
            this.StartPosition = FormStartPosition.CenterParent;

            var row1 = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true, Padding = new Padding(10) };
            cboLuot = Cb(200);
            txtPhong = new TextBox { Width = 90, ReadOnly = true, Margin = new Padding(3, 6, 12, 6) };
            cboDV = Cb(180);
            row1.Controls.Add(Lb("Phiếu đang ở:")); row1.Controls.Add(cboLuot);
            row1.Controls.Add(Lb("Phòng:")); row1.Controls.Add(txtPhong);
            row1.Controls.Add(Lb("Dịch vụ:")); row1.Controls.Add(cboDV);

            var row2 = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true, Padding = new Padding(10, 0, 10, 10) };
            dtNgay = new DateTimePicker { Width = 120, Format = DateTimePickerFormat.Short, Margin = new Padding(3, 6, 12, 6) };
            numSL = new NumericUpDown { Width = 80, Minimum = 1, Maximum = 999, Margin = new Padding(3, 6, 12, 6) };
            cboNV = Cb(180);
            var btnGhi = Bt("Ghi nhận");
            row2.Controls.Add(Lb("Ngày sử dụng:")); row2.Controls.Add(dtNgay);
            row2.Controls.Add(Lb("Số lượng:")); row2.Controls.Add(numSL);
            row2.Controls.Add(Lb("Nhân viên:")); row2.Controls.Add(cboNV);
            row2.Controls.Add(btnGhi);

            var lblLichSu = new Label { Text = "Lịch sử sử dụng dịch vụ:", AutoSize = true, Dock = DockStyle.Top, Padding = new Padding(10, 4, 0, 4) };

            dgvLichSu = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White
            };

            Button btnDong = new Button { Text = "Đóng", Dock = DockStyle.Bottom, Height = 36 };
            btnDong.Click += (s2, e) => Close();

            this.Controls.Add(dgvLichSu);
            this.Controls.Add(lblLichSu);
            this.Controls.Add(row2);
            this.Controls.Add(row1);
            this.Controls.Add(btnDong);

            cboLuot.SelectedIndexChanged += cboLuot_SelectedIndexChanged;
            btnGhi.Click += (s2, e) =>
            {
                var k = s.GhiNhan(V(cboLuot), txtPhong.Text.Trim(), dtNgay.Value, V(cboNV), V(cboDV), (int)numSL.Value);
                MessageBox.Show(k.ThongBao);
                if (k.ThanhCong) Tai();
            };
        }

        string V(ComboBox c) => c.SelectedValue == null ? "" : c.SelectedValue.ToString();

        private void Frm_Load(object sender, EventArgs e)
        {
            cboLuot.DataSource = s.LayPhieuDangO(); cboLuot.DisplayMember = "SoPhieuDat"; cboLuot.ValueMember = "SoPhieuDat";
            cboDV.DataSource = s.LayDichVu(); cboDV.DisplayMember = "TenDV"; cboDV.ValueMember = "MaDV";
            cboNV.DataSource = dm.LayNhanVien(); cboNV.DisplayMember = "HoTen"; cboNV.ValueMember = "MaNV";
            Tai();
        }

        void Tai()
        {
            if (cboLuot.SelectedValue != null)
                dgvLichSu.DataSource = s.LayLichSu(cboLuot.SelectedValue.ToString());
        }

        private void cboLuot_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLuot.SelectedItem is System.Data.DataRowView r)
                txtPhong.Text = Convert.ToString(r["SoPhong"]);
            Tai();
        }
    }
}
