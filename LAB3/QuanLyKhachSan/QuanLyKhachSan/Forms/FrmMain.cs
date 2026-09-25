using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyKhachSan.Forms
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            BuildUI();
        }

        private void BuildUI()
        {
            // Tắt auto-scale theo font để tránh lệch layout khi máy bật DPI scaling khác 100%
            this.AutoScaleMode = AutoScaleMode.None;

            this.Text = "Quản lý khách sạn";
            this.ClientSize = new Size(860, 460);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Font = new Font("Segoe UI", 10F);
            this.BackColor = Color.White;

            // ----- Tiêu đề -----
            Label lblTitle = new Label
            {
                Text = "HỆ THỐNG QUẢN LÝ KHÁCH SẠN",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 51, 153),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 90
            };
            this.Controls.Add(lblTitle);

            // ----- Bảng lưới 3 cột x 3 hàng, tự canh giữa và co giãn -----
            TableLayoutPanel grid = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 3,
                Padding = new Padding(40, 10, 40, 30)
            };
            for (int i = 0; i < 3; i++)
                grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / 3));
            for (int i = 0; i < 3; i++)
                grid.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / 3));
            this.Controls.Add(grid);
            grid.BringToFront();

            grid.Controls.Add(CreateMenuButton("📋", "Danh mục",
                (s, e) => { using (var f = new FrmDanhMuc()) f.ShowDialog(this); }), 0, 0);
            grid.Controls.Add(CreateMenuButton("🛏", "Phòng - Tiện nghi",
                (s, e) => { using (var f = new FrmPhongTienNghi()) f.ShowDialog(this); }), 1, 0);
            grid.Controls.Add(CreateMenuButton("🔑", "Đặt / Nhận phòng",
                (s, e) => { using (var f = new FrmDatPhong()) f.ShowDialog(this); }), 2, 0);

            grid.Controls.Add(CreateMenuButton("🛎", "Sử dụng dịch vụ",
                (s, e) => { using (var f = new FrmDichVu()) f.ShowDialog(this); }), 0, 1);
            grid.Controls.Add(CreateMenuButton("💵", "Trả phòng - Thanh toán",
                (s, e) => { using (var f = new FrmTraPhong()) f.ShowDialog(this); }), 1, 1);
            grid.Controls.Add(CreateMenuButton("📊", "Thống kê",
                (s, e) => { using (var f = new FrmThongKe()) f.ShowDialog(this); }), 2, 1);

            // Chỉ đặt nút Thoát vào ô giữa hàng 3 -> tự động canh giữa vì 2 ô hai bên để trống
            grid.Controls.Add(CreateMenuButton("🚪", "Thoát",
                (s, e) =>
                {
                    if (MessageBox.Show("Bạn có thực sự muốn thoát?", "Xác nhận",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        Close();
                }), 1, 2);
        }

        private Button CreateMenuButton(string icon, string text, EventHandler onClick)
        {
            Button btn = new Button
            {
                Text = "  " + icon + "   " + text,
                Font = new Font("Segoe UI", 11F),
                TextAlign = ContentAlignment.MiddleLeft,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(245, 246, 250),
                Cursor = Cursors.Hand,
                Dock = DockStyle.Fill,
                Margin = new Padding(15, 12, 15, 12)
            };
            btn.FlatAppearance.BorderColor = Color.FromArgb(200, 205, 215);
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(230, 236, 250);
            btn.Click += onClick;
            return btn;
        }
    }
}
