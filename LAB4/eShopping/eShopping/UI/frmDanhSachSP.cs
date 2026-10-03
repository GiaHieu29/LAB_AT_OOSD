using System;
using System.Drawing;
using System.Windows.Forms;

namespace eShopping.UI
{
    public partial class frmDanhSachSP : Form
    {
        private readonly ComboBox cboNhom = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 220 };
        private readonly DataGridView dgv = new DataGridView();
        private readonly Label lblUser = new Label { Dock = DockStyle.Bottom, Height = 28, TextAlign = ContentAlignment.MiddleLeft };
        private Button btnLogin;

        public frmDanhSachSP()
        {
            Text = "e-SHOPPING - Danh sách sản phẩm";
            Size = new Size(900, 560);
            StartPosition = FormStartPosition.CenterScreen;

            dgv.Dock = DockStyle.Fill;
            dgv.AutoGenerateColumns = false;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã SP", DataPropertyName = "MaSP", Width = 80 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tên sản phẩm", DataPropertyName = "TenSP", Width = 280 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Nhà SX", DataPropertyName = "NhaSX", Width = 120 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Giá (đ)",
                DataPropertyName = "GiaHienHanh",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
            });
            dgv.Columns.Add(new DataGridViewCheckBoxColumn { HeaderText = "Còn hàng", DataPropertyName = "ConHang", Width = 80 });

            var top = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 45, Padding = new Padding(8) };
            top.Controls.Add(new Label { Text = "Nhóm sản phẩm:", AutoSize = true, Margin = new Padding(0, 6, 3, 0) });
            top.Controls.Add(cboNhom);
            FlowBtn(top, "Chi tiết", BtnChiTiet_Click);
            FlowBtn(top, "Thêm vào giỏ", BtnThem_Click);
            FlowBtn(top, "Giỏ hàng", BtnGio_Click);
            btnLogin = FlowBtn(top, "Đăng nhập", BtnLogin_Click);

            // Thứ tự Add quan trọng với Dock: Fill trước, Bottom, rồi Top
            Controls.Add(dgv);
            Controls.Add(lblUser);
            Controls.Add(top);

            Load += delegate
            {
                cboNhom.DisplayMember = "Value";
                cboNhom.ValueMember = "Key";
                cboNhom.DataSource = AppSession.Catalog.LayNhom();
                cboNhom.SelectedIndexChanged += delegate { LoadSP(); };
                LoadSP();
                CapNhatUser();
            };
        }

        private static Button FlowBtn(FlowLayoutPanel p, string text, EventHandler h)
        {
            var b = new Button { Text = text, AutoSize = true };
            b.Click += h;
            p.Controls.Add(b);
            return b;
        }

        private void LoadSP()
        {
            var ma = cboNhom.SelectedValue as string;
            if (ma == null) return;
            dgv.DataSource = AppSession.Catalog.LayTheoNhom(ma);
        }

        private SanPham Chon()
        {
            var sp = dgv.CurrentRow == null ? null : dgv.CurrentRow.DataBoundItem as SanPham;
            if (sp == null) UiHelper.Info("Hãy chọn một sản phẩm.");
            return sp;
        }

        private void CapNhatUser()
        {
            var kh = AppSession.KhachHienTai;
            lblUser.Text = kh == null ? "  Chưa đăng nhập" : "  Xin chào, " + kh.HoTen;
            btnLogin.Text = kh == null ? "Đăng nhập" : "Đăng xuất";
        }

        private void BtnChiTiet_Click(object s, EventArgs e)
        {
            var sp = Chon(); if (sp == null) return;
            using (var f = new frmChiTietSP(sp.MaSP)) f.ShowDialog(this);
        }

        private void BtnThem_Click(object s, EventArgs e)
        {
            var sp = Chon(); if (sp == null) return;
            try
            {
                AppSession.Cart.Add(sp);
                UiHelper.Info("Đã thêm \"" + sp.TenSP + "\" vào giỏ hàng.");
            }
            catch (InvalidOperationException ex) { UiHelper.Error(ex.Message); }   // BR03
        }

        private void BtnGio_Click(object s, EventArgs e)
        {
            using (var f = new frmGioHang()) f.ShowDialog(this);
            CapNhatUser();
        }

        private void BtnLogin_Click(object s, EventArgs e)
        {
            if (AppSession.KhachHienTai != null) { AppSession.KhachHienTai = null; }
            else using (var f = new frmDangNhap()) f.ShowDialog(this);
            CapNhatUser();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // frmDanhSachSP
            // 
            this.ClientSize = new System.Drawing.Size(695, 459);
            this.Name = "frmDanhSachSP";
            this.ResumeLayout(false);

        }
    }
}