
using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Data;

namespace QuanLyHoSoBenhAn
{
    public partial class Form1 : Form
    {

        private Button btnDocDuLieu;
        private Label lblTieuDe;
        private Label lblTrangThai;

        public Form1()
        {
            InitializeComponent();
            ThietKeGiaoDien();
        }

        private void ThietKeGiaoDien()
        {
            // Cấu hình cửa sổ
            this.Text = "Quản lý hồ sơ bệnh án";
            this.ClientSize = new Size(650, 350);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;
            this.Font = new Font("Segoe UI", 10F);

            // Tiêu đề
            lblTieuDe = new Label();
            lblTieuDe.Text = "QUẢN LÝ HỒ SƠ BỆNH ÁN";
            lblTieuDe.Font = new Font(
                "Segoe UI", 17F, FontStyle.Bold);
            lblTieuDe.ForeColor = Color.DarkBlue;
            lblTieuDe.TextAlign =
                ContentAlignment.MiddleCenter;
            lblTieuDe.Location = new Point(50, 35);
            lblTieuDe.Size = new Size(550, 55);

            // Nút kiểm tra kết nối
            btnKetNoi = new Button();
            btnKetNoi.Name = "btnKetNoi";
            btnKetNoi.Text = "Kiểm tra kết nối";
            btnKetNoi.Location = new Point(115, 125);
            btnKetNoi.Size = new Size(190, 45);
            btnKetNoi.BackColor = Color.SteelBlue;
            btnKetNoi.ForeColor = Color.White;
            btnKetNoi.FlatStyle = FlatStyle.Flat;
            btnKetNoi.Click += btnKetNoi_Click;

            // Nút đọc dữ liệu
            btnDocDuLieu = new Button();
            btnDocDuLieu.Name = "btnDocDuLieu";
            btnDocDuLieu.Text = "Đọc dữ liệu bệnh nhân";
            btnDocDuLieu.Location = new Point(325, 125);
            btnDocDuLieu.Size = new Size(210, 45);
            btnDocDuLieu.BackColor = Color.SeaGreen;
            btnDocDuLieu.ForeColor = Color.White;
            btnDocDuLieu.FlatStyle = FlatStyle.Flat;
            btnDocDuLieu.Click += btnDocDuLieu_Click;

            // Nhãn trạng thái
            lblTrangThai = new Label();
            lblTrangThai.Text =
                "Nhấn nút để kiểm tra kết nối SQL Server.";
            lblTrangThai.Location = new Point(45, 210);
            lblTrangThai.Size = new Size(560, 80);
            lblTrangThai.TextAlign =
                ContentAlignment.MiddleCenter;

            // Thêm controls vào Form
            this.Controls.Add(lblTieuDe);
            this.Controls.Add(btnKetNoi);
            this.Controls.Add(btnDocDuLieu);
            this.Controls.Add(lblTrangThai);
        }

        // Kiểm tra kết nối SQL Server
        private void btnKetNoi_Click(
            object sender, EventArgs e)
        {
            string thongBao;

            bool thanhCong =
                Db.TestConnection(out thongBao);

            lblTrangThai.Text = thongBao;

            MessageBox.Show(
                thongBao,
                thanhCong ? "Thành công" : "Lỗi kết nối",
                MessageBoxButtons.OK,
                thanhCong
                    ? MessageBoxIcon.Information
                    : MessageBoxIcon.Error);
        }

        // Đọc số lượng bệnh nhân
        private void btnDocDuLieu_Click(
            object sender, EventArgs e)
        {
            try
            {
                object ketQua = Db.Scalar(
                    "SELECT COUNT(*) FROM dbo.BenhNhan");

                string thongBao =
                    "Đọc dữ liệu thành công!\n" +
                    "Số bệnh nhân hiện có: " +
                    Convert.ToString(ketQua);

                lblTrangThai.Text = thongBao;

                MessageBox.Show(
                    thongBao,
                    "Dữ liệu bệnh nhân",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                lblTrangThai.Text =
                    "Không thể đọc dữ liệu bệnh nhân.";

                MessageBox.Show(
                    "Lỗi đọc dữ liệu:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}

