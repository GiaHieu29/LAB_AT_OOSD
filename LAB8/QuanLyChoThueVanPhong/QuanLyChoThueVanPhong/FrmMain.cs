
using System;
using System.Windows.Forms;
using QuanLyChoThueVanPhong.Data;

namespace QuanLyChoThueVanPhong
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            try
            {
                int soPhong = Convert.ToInt32(
                    Db.Scalar("SELECT COUNT(*) FROM dbo.Phong"));

                int soKhach = Convert.ToInt32(
                    Db.Scalar("SELECT COUNT(*) FROM dbo.KhachHang"));

                lblThongTin.Text =
                    "SQL Server đã kết nối | " +
                    "Số phòng: " + soPhong +
                    " | Số khách hàng: " + soKhach;
            }
            catch (Exception ex)
            {
                lblThongTin.Text =
                    "Chưa kết nối được SQL Server";

                MessageBox.Show(
                    ex.Message,
                    "Lỗi kết nối SQL Server",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        // Mở Form con theo tên lớp khi Form đã được tạo.
        // Nếu chưa có Form, hiển thị thông báo.
        private void MoChucNang(string tenLop, string tenChucNang)
        {
            try
            {
                Type loaiForm = typeof(FrmMain).Assembly.GetType(
                    "QuanLyChoThueVanPhong." + tenLop);

                if (loaiForm == null)
                {
                    MessageBox.Show(
                        "Chức năng \"" + tenChucNang +
                        "\" sẽ được xây dựng ở bước tiếp theo.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                if (!typeof(Form).IsAssignableFrom(loaiForm))
                    throw new Exception(
                        tenLop + " không phải Windows Form.");

                using (Form frm = (Form)Activator.CreateInstance(loaiForm))
                {
                    frm.StartPosition =
                        FormStartPosition.CenterParent;

                    frm.ShowDialog(this);
                }

                // Tải lại số liệu sau khi đóng Form con.
                FrmMain_Load(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Lỗi mở chức năng",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnDanhMuc_Click(object sender, EventArgs e)
        {
            MoChucNang("FrmDanhMuc", "Danh mục");
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            MoChucNang("FrmKhachHang", "Khách hàng");
        }

        private void btnHopDong_Click(object sender, EventArgs e)
        {
            MoChucNang("FrmHopDong", "Hợp đồng thuê");
        }

        private void btnGiaHan_Click(object sender, EventArgs e)
        {
            MoChucNang("FrmGiaHan", "Gia hạn hợp đồng");
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            MoChucNang(
                "FrmThanhToanTienThue",
                "Thanh toán tiền thuê");
        }

        private void btnDichVu_Click(object sender, EventArgs e)
        {
            MoChucNang("FrmDichVu", "Dịch vụ và hóa đơn");
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            MoChucNang(
                "FrmNhanVienVanPhong",
                "Nhân viên văn phòng");
        }

        private void btnTheXe_Click(object sender, EventArgs e)
        {
            MoChucNang("FrmTheGiuXe", "Thẻ giữ xe");
        }

        private void btnKiemTra_Click(object sender, EventArgs e)
        {
            MoChucNang("FrmKiemTraPhong", "Kiểm tra phòng");
        }

        private void btnViPham_Click(object sender, EventArgs e)
        {
            MoChucNang("FrmViPhamSuaChua", "Vi phạm – Sửa chữa");
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            MoChucNang("FrmThongKe", "Thống kê");
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "Bạn có muốn thoát chương trình?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Close();
            }
        }
    }
}
