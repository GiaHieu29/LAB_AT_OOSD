using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyKhachSan.Services;

namespace QuanLyKhachSan.Forms
{
    public partial class FrmThongKe : Form
    {
        readonly ThongKeService s = new ThongKeService();

        DateTimePicker dtTu, dtDen;
        DataGridView dgvTongHop, dgvDV;

        public FrmThongKe()
        {
            InitializeComponent();
            BuildUI();
        }

        static Label Lb(string text) => new Label { Text = text, AutoSize = true, Margin = new Padding(6, 10, 3, 0) };

        void BuildUI()
        {
            this.AutoScaleMode = AutoScaleMode.None;
            this.Text = "Thống kê khách sạn";
            this.ClientSize = new Size(850, 550);
            this.StartPosition = FormStartPosition.CenterParent;

            var row = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true, Padding = new Padding(10) };
            dtTu = new DateTimePicker { Width = 130, Format = DateTimePickerFormat.Short, Value = DateTime.Now.AddMonths(-1), Margin = new Padding(3, 6, 12, 6) };
            dtDen = new DateTimePicker { Width = 130, Format = DateTimePickerFormat.Short, Value = DateTime.Now, Margin = new Padding(3, 6, 12, 6) };
            var btnTK = new Button { Text = "Thống kê", AutoSize = true, Padding = new Padding(10, 4, 10, 4), Margin = new Padding(10, 5, 3, 5) };
            row.Controls.Add(Lb("Từ ngày:")); row.Controls.Add(dtTu);
            row.Controls.Add(Lb("Đến ngày:")); row.Controls.Add(dtDen);
            row.Controls.Add(btnTK);

            var lblTongHop = new Label { Text = "Tổng hợp:", AutoSize = true, Dock = DockStyle.Top, Padding = new Padding(10, 6, 0, 4) };
            dgvTongHop = new DataGridView
            {
                Dock = DockStyle.Top, Height = 90,
                ReadOnly = true, AllowUserToAddRows = false, AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White
            };

            var lblDV = new Label { Text = "Dịch vụ sử dụng:", AutoSize = true, Dock = DockStyle.Top, Padding = new Padding(10, 10, 0, 4) };
            dgvDV = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true, AllowUserToAddRows = false, AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White
            };

            Button btnDong = new Button { Text = "Đóng", Dock = DockStyle.Bottom, Height = 36 };
            btnDong.Click += (s2, e) => Close();

            this.Controls.Add(dgvDV);
            this.Controls.Add(lblDV);
            this.Controls.Add(dgvTongHop);
            this.Controls.Add(lblTongHop);
            this.Controls.Add(row);
            this.Controls.Add(btnDong);

            btnTK.Click += (s2, e) =>
            {
                if (dtDen.Value.Date < dtTu.Value.Date)
                {
                    MessageBox.Show("Đến ngày không được trước từ ngày.");
                    return;
                }
                dgvTongHop.DataSource = s.TongHop(dtTu.Value, dtDen.Value);
                dgvDV.DataSource = s.DichVu(dtTu.Value, dtDen.Value);
            };
        }
    }
}
