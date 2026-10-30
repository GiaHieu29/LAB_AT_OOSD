
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QuanLyChoThueVanPhong.Services;

namespace QuanLyChoThueVanPhong
{
    public partial class FrmThongKe : Form
    {
        private readonly ThongKeService svc =
            new ThongKeService();

        public FrmThongKe()
        {
            InitializeComponent();
        }

        // =======================================
        // 1. LOAD FORM
        // =======================================

        private void FrmThongKe_Load(
            object sender, EventArgs e)
        {
            dtTuNgay.Value =
                new DateTime(DateTime.Today.Year, 1, 1);

            dtDenNgay.Value = DateTime.Today;

            TaiThongKe();
        }

        // =======================================
        // 2. HÀM ĐỊNH DẠNG BẢNG
        // =======================================

        private void Cot(
            DataGridView grid,
            string ma, string tieuDe,
            string format = "")
        {
            if (!grid.Columns.Contains(ma))
                return;

            grid.Columns[ma].HeaderText = tieuDe;

            if (format != "")
                grid.Columns[ma]
                    .DefaultCellStyle.Format = format;
        }

        private void DinhDangNgay(
            DataGridView g, params string[] cols)
        {
            foreach (string s in cols)
                Cot(g, s,
                    g.Columns.Contains(s)
                        ? g.Columns[s].HeaderText : s,
                    "dd/MM/yyyy");
        }

        private void DinhDangTien(
            DataGridView g, params string[] cols)
        {
            foreach (string s in cols)
                Cot(g, s,
                    g.Columns.Contains(s)
                        ? g.Columns[s].HeaderText : s,
                    "N0");
        }

        private decimal Tong(
            DataTable dt, string cot)
        {
            if (dt == null ||
                !dt.Columns.Contains(cot))
                return 0;

            return dt.AsEnumerable()
                .Sum(r => r[cot] == DBNull.Value
                    ? 0m
                    : Convert.ToDecimal(r[cot]));
        }

        private int Dem(
            DataTable dt, string cot, string value)
        {
            return dt.AsEnumerable()
                .Count(r =>
                    Convert.ToString(r[cot]) == value);
        }

        private void GanBang(
            DataGridView grid, DataTable dt)
        {
            grid.DataSource = dt;
            grid.ClearSelection();
        }

        private string Tien(decimal n)
        {
            return n.ToString("N0") + " đ";
        }

        // =======================================
        // 3. TẢI TOÀN BỘ THỐNG KÊ
        // =======================================

        private void TaiThongKe()
        {
            DateTime tu = dtTuNgay.Value.Date;
            DateTime den = dtDenNgay.Value.Date;

            if (tu > den)
            {
                MessageBox.Show(
                    "Từ ngày không được sau Đến ngày.");
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                btnThongKe.Enabled = false;

                DataTable phong = svc.LayPhong(den);
                DataTable hopDong = svc.LayHopDong(tu, den);
                DataTable doanhThu = svc.LayDoanhThu(tu, den);
                DataTable chiTietThu =
                    svc.LayChiTietDoanhThu(tu, den);
                DataTable noThue = svc.LayNoTienThue(den);
                DataTable noDV = svc.LayNoDichVu(den);
                DataTable viPham = svc.LayViPham(tu, den);
                DataTable suaChua = svc.LaySuaChua(tu, den);

                // ================================
                // TAB 1 - TỔNG QUAN
                // ================================

                int tongPhong = phong.Rows.Count;
                int dangThue =
                    Dem(phong, "TinhTrangThongKe", "Đang thuê");
                int trong =
                    Dem(phong, "TinhTrangThongKe", "Trống");
                int baoTri =
                    tongPhong - dangThue - trong;

                decimal thuThue =
                    Tong(doanhThu, "ThuTienThue");
                decimal thuDV =
                    Tong(doanhThu, "ThuDichVu");

                decimal noTienThue =
                    Tong(noThue, "ConNo");
                decimal noDichVu =
                    Tong(noDV, "ConNo");

                lblTongPhong.Text = tongPhong.ToString();
                lblDangThue.Text = dangThue.ToString();
                lblPhongTrong.Text = trong.ToString();
                lblBaoTri.Text = baoTri.ToString();

                lblTongHopDong.Text =
                    hopDong.Rows.Count.ToString();

                lblTongThu.Text = Tien(thuThue + thuDV);
                lblNoThue.Text = Tien(noTienThue);
                lblNoDichVu.Text = Tien(noDichVu);

                lblKyBaoCao.Text =
                    "Doanh thu: " + tu.ToString("dd/MM/yyyy") +
                    " - " + den.ToString("dd/MM/yyyy") +
                    " | Công nợ và phòng: tại " +
                    den.ToString("dd/MM/yyyy");

                GanBang(dgvPhong, phong);
                Cot(dgvPhong, "MaPhong", "Mã phòng");
                Cot(dgvPhong, "TenPhong", "Tên phòng");
                Cot(dgvPhong, "TenTang", "Tầng");
                Cot(dgvPhong, "DienTich", "Diện tích", "N2");
                Cot(dgvPhong, "TrangThaiDanhMuc",
                    "Trạng thái danh mục");
                Cot(dgvPhong, "TinhTrangThongKe",
                    "Tình trạng tại ngày xét");

                GanBang(dgvHopDong, hopDong);
                Cot(dgvHopDong, "MaHopDong", "Hợp đồng");
                Cot(dgvHopDong, "TenKhachHang", "Khách hàng");
                Cot(dgvHopDong, "NgayKy", "Ngày ký");
                Cot(dgvHopDong, "SoPhong", "Số phòng");
                Cot(dgvHopDong, "TienThang", "Tiền/tháng");
                DinhDangNgay(dgvHopDong,
                    "NgayKy", "NgayBatDau", "NgayKetThuc");
                DinhDangTien(dgvHopDong, "TienThang");

                // ================================
                // TAB 2 - DOANH THU
                // ================================

                GanBang(dgvDoanhThu, doanhThu);
                Cot(dgvDoanhThu, "Nam", "Năm");
                Cot(dgvDoanhThu, "Thang", "Tháng");
                Cot(dgvDoanhThu, "ThuTienThue", "Tiền thuê");
                Cot(dgvDoanhThu, "ThuDichVu", "Dịch vụ");
                Cot(dgvDoanhThu, "TongDoanhThu", "Tổng thu");

                DinhDangTien(dgvDoanhThu,
                    "ThuTienThue", "ThuDichVu",
                    "TongDoanhThu");

                GanBang(dgvChiTietThu, chiTietThu);
                Cot(dgvChiTietThu, "LoaiThu", "Loại thu");
                Cot(dgvChiTietThu, "MaPhieu", "Mã phiếu");
                Cot(dgvChiTietThu, "MaHopDong", "Hợp đồng");
                Cot(dgvChiTietThu, "MaPhong", "Phòng");
                Cot(dgvChiTietThu, "NgayThu", "Ngày thu");
                Cot(dgvChiTietThu, "SoTien", "Số tiền");
                Cot(dgvChiTietThu, "PhuongThuc", "Phương thức");
                DinhDangNgay(dgvChiTietThu, "NgayThu");
                DinhDangTien(dgvChiTietThu, "SoTien");

                lblThuThue.Text =
                    "Đã thu tiền thuê: " + Tien(thuThue);
                lblThuDV.Text =
                    "Đã thu dịch vụ: " + Tien(thuDV);
                lblThuTong.Text =
                    "Tổng thực thu: " + Tien(thuThue + thuDV);

                // ================================
                // TAB 3 - CÔNG NỢ
                // ================================

                GanBang(dgvNoThue, noThue);
                Cot(dgvNoThue, "MaHopDong", "Hợp đồng");
                Cot(dgvNoThue, "MaPhong", "Phòng");
                Cot(dgvNoThue, "TenKhachHang", "Khách hàng");
                Cot(dgvNoThue, "PhaiThuDenHan",
                    "Phải thu đến hạn");
                Cot(dgvNoThue, "DaThu", "Đã thu");
                Cot(dgvNoThue, "ConNo", "Còn nợ");
                DinhDangTien(dgvNoThue,
                    "PhaiThuDenHan", "DaThu", "ConNo");

                GanBang(dgvNoDichVu, noDV);
                Cot(dgvNoDichVu, "MaHoaDon", "Hóa đơn");
                Cot(dgvNoDichVu, "MaHopDong", "Hợp đồng");
                Cot(dgvNoDichVu, "Thang", "Tháng");
                Cot(dgvNoDichVu, "Nam", "Năm");
                Cot(dgvNoDichVu, "HanThanhToan",
                    "Hạn thanh toán");
                Cot(dgvNoDichVu, "TongTien", "Tổng tiền");
                Cot(dgvNoDichVu, "DaThu", "Đã thu");
                Cot(dgvNoDichVu, "ConNo", "Còn nợ");
                DinhDangNgay(dgvNoDichVu, "HanThanhToan");
                DinhDangTien(dgvNoDichVu,
                    "TongTien", "DaThu", "ConNo");

                lblTongNo.Text =
                    "Tổng công nợ đến hạn: " +
                    Tien(noTienThue + noDichVu);

                // ================================
                // TAB 4 - VẬN HÀNH
                // ================================

                GanBang(dgvSuaChua, suaChua);
                Cot(dgvSuaChua, "MaSuaChua", "Mã sửa chữa");
                Cot(dgvSuaChua, "MaPhong", "Phòng");
                Cot(dgvSuaChua, "MaKiemTra", "Kiểm tra");
                Cot(dgvSuaChua, "NgayBatDau", "Bắt đầu");
                Cot(dgvSuaChua, "NgayHoanThanh",
                    "Hoàn thành");
                Cot(dgvSuaChua, "ChiPhi", "Chi phí");
                Cot(dgvSuaChua, "TrangThai", "Trạng thái");
                DinhDangNgay(dgvSuaChua,
                    "NgayBatDau", "NgayHoanThanh");
                DinhDangTien(dgvSuaChua, "ChiPhi");

                GanBang(dgvViPham, viPham);
                Cot(dgvViPham, "MaViPham", "Mã vi phạm");
                Cot(dgvViPham, "MaHopDong", "Hợp đồng");
                Cot(dgvViPham, "MaPhong", "Phòng");
                Cot(dgvViPham, "NgayPhatHien", "Ngày phát hiện");
                Cot(dgvViPham, "SoTienPhat", "Tiền phạt");
                Cot(dgvViPham, "TrangThai", "Trạng thái");
                Cot(dgvViPham, "NgayXuLy", "Ngày xử lý");
                DinhDangNgay(dgvViPham,
                    "NgayPhatHien", "NgayXuLy");
                DinhDangTien(dgvViPham, "SoTienPhat");

                lblChiPhiSua.Text =
                    "Chi phí sửa chữa: " +
                    Tien(Tong(suaChua, "ChiPhi"));

                lblSoSuaChua.Text =
                    "Số công việc: " + suaChua.Rows.Count;

                lblSoViPham.Text =
                    "Số vi phạm: " + viPham.Rows.Count;

                lblTienPhat.Text =
                    "Tiền phạt ghi nhận: " +
                    Tien(Tong(viPham, "SoTienPhat"));
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
            finally
            {
                Cursor = Cursors.Default;
                btnThongKe.Enabled = true;
            }
        }

        // =======================================
        // 4. NÚT THỐNG KÊ
        // =======================================

        private void btnThongKe_Click(
            object sender, EventArgs e)
        {
            TaiThongKe();
        }

        // =======================================
        // 5. NÚT NĂM HIỆN TẠI
        // =======================================

        private void btnNamNay_Click(
            object sender, EventArgs e)
        {
            dtTuNgay.Value =
                new DateTime(DateTime.Today.Year, 1, 1);

            dtDenNgay.Value = DateTime.Today;

            TaiThongKe();
        }

        // =======================================
        // 6. NÚT THÁNG HIỆN TẠI
        // =======================================

        private void btnThangNay_Click(
            object sender, EventArgs e)
        {
            dtTuNgay.Value =
                new DateTime(DateTime.Today.Year,
                             DateTime.Today.Month, 1);

            dtDenNgay.Value = DateTime.Today;

            TaiThongKe();
        }

        // =======================================
        // 7. NÚT ĐÓNG
        // =======================================

        private void btnDong_Click(
            object sender, EventArgs e)
        {
            Close();
        }

        // =======================================
        // 8. HIỂN THỊ LỖI
        // =======================================

        private void BaoLoi(Exception ex)
        {
            SqlException sql = ex as SqlException;

            if (sql != null && sql.Number == 1205)
            {
                MessageBox.Show(
                    "SQL Server đang có giao dịch khác. " +
                    "Hãy thử tải thống kê lại.",
                    "Xung đột giao dịch",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show(
                ex.Message,
                "Lỗi thống kê",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}
