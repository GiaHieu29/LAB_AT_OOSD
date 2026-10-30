
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChoThueVanPhong.Data;

namespace QuanLyChoThueVanPhong
{
    public partial class Form1 : Form
    {
        private Button btnKiemTra;
        private Button btnXemPhong;

        private Label lblTieuDe;
        private Label lblKetQua;

        private DataGridView dgvPhong;

        public Form1()
        {
            InitializeComponent();
            TaoGiaoDien();
        }

        private void TaoGiaoDien()
        {
            this.Text =
                "Kiểm tra kết nối SQL Server - Lab 08";

            this.ClientSize = new Size(850, 570);

            this.StartPosition =
                FormStartPosition.CenterScreen;

            this.BackColor = Color.WhiteSmoke;

            this.Font = new Font(
                "Segoe UI", 10F);

            // TIÊU ĐỀ
            lblTieuDe = new Label();

            lblTieuDe.Text =
                "KIỂM TRA KẾT NỐI DATABASE LAB 08";

            lblTieuDe.Font = new Font(
                "Segoe UI", 16F,
                FontStyle.Bold);

            lblTieuDe.ForeColor =
                Color.FromArgb(35, 75, 125);

            lblTieuDe.Location =
                new Point(30, 25);

            lblTieuDe.Size =
                new Size(780, 45);

            lblTieuDe.TextAlign =
                ContentAlignment.MiddleCenter;

            // NÚT KIỂM TRA
            btnKiemTra = new Button();

            btnKiemTra.Text =
                "Kiểm tra kết nối";

            btnKiemTra.Location =
                new Point(190, 100);

            btnKiemTra.Size =
                new Size(210, 45);

            btnKiemTra.Click +=
                btnKiemTra_Click;

            // NÚT XEM PHÒNG
            btnXemPhong = new Button();

            btnXemPhong.Text =
                "Xem danh sách phòng";

            btnXemPhong.Location =
                new Point(430, 100);

            btnXemPhong.Size =
                new Size(210, 45);

            btnXemPhong.Click +=
                btnXemPhong_Click;

            // THÔNG BÁO
            lblKetQua = new Label();

            lblKetQua.Text =
                "Nhấn Kiểm tra kết nối để bắt đầu.";

            lblKetQua.Location =
                new Point(40, 170);

            lblKetQua.Size =
                new Size(770, 70);

            lblKetQua.TextAlign =
                ContentAlignment.MiddleCenter;

            // BẢNG PHÒNG
            dgvPhong = new DataGridView();

            dgvPhong.Location =
                new Point(40, 260);

            dgvPhong.Size =
                new Size(770, 265);

            dgvPhong.BackgroundColor =
                Color.White;

            dgvPhong.ReadOnly = true;

            dgvPhong.AllowUserToAddRows = false;
            dgvPhong.AllowUserToDeleteRows = false;

            dgvPhong.RowHeadersVisible = false;

            dgvPhong.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            // THÊM CONTROL
            this.Controls.Add(lblTieuDe);
            this.Controls.Add(btnKiemTra);
            this.Controls.Add(btnXemPhong);
            this.Controls.Add(lblKetQua);
            this.Controls.Add(dgvPhong);
        }

        // =====================================
        // KIỂM TRA KẾT NỐI
        // =====================================

        private void btnKiemTra_Click(
            object sender, EventArgs e)
        {
            string message;

            bool ok = Db.TestConnection(
                out message);

            lblKetQua.Text = message;

            lblKetQua.ForeColor =
                ok ? Color.Green : Color.Red;

            MessageBox.Show(
                message,
                ok ? "Thành công" : "Lỗi kết nối",
                MessageBoxButtons.OK,
                ok ? MessageBoxIcon.Information
                   : MessageBoxIcon.Error);
        }

        // =====================================
        // LẤY DANH SÁCH PHÒNG TỪ SQL SERVER
        // =====================================

        private void btnXemPhong_Click(
            object sender, EventArgs e)
        {
            try
            {
                string sql = @"
                    SELECT
                        p.MaPhong AS [Mã phòng],
                        p.TenPhong AS [Tên phòng],
                        t.TenTang AS [Tầng],
                        p.DienTich AS [Diện tích (m2)],
                        p.HuongPhong AS [Hướng],
                        p.DonGiaThang AS [Giá thuê/tháng],
                        p.TrangThai AS [Trạng thái]
                    FROM dbo.Phong p
                    INNER JOIN dbo.Tang t
                        ON p.MaTang = t.MaTang
                    ORDER BY
                        t.SoThuTu,
                        p.MaPhong";

                DataTable dt = Db.Query(sql);

                dgvPhong.DataSource = dt;

                if (dgvPhong.Columns.Contains(
                    "Giá thuê/tháng"))
                {
                    dgvPhong.Columns[
                        "Giá thuê/tháng"]
                        .DefaultCellStyle.Format = "N0";
                }

                lblKetQua.Text =
                    "Đã tải " + dt.Rows.Count +
                    " phòng từ database QuanLyChoThueVanPhong.";

                lblKetQua.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Lỗi truy vấn SQL Server",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
