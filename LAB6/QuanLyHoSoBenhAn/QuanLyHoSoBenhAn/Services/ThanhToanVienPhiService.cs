
using System;
using System.Data;
using QuanLyHoSoBenhAn.Data;

namespace QuanLyHoSoBenhAn.Services
{
    public class ThanhToanVienPhiService
    {
        public DataTable LayDanhSach(string key = "")
        {
            return Db.Query(@"
                SELECT
                    tp.MaThanhToan,
                    tp.MaPhieuKham,
                    bn.HoTen AS TenBenhNhan,
                    pk.NgayGioKham,
                    tp.NgayThanhToan,
                    tp.SoTien,
                    tp.PhuongThuc,
                    tp.GhiChu
                FROM dbo.ThanhToanVienPhi tp
                INNER JOIN dbo.PhieuKham pk
                    ON tp.MaPhieuKham = pk.MaPhieuKham
                INNER JOIN dbo.HoSoBenhAn hs
                    ON pk.MaHoSo = hs.MaHoSo
                INNER JOIN dbo.BenhNhan bn
                    ON hs.MaBenhNhan = bn.MaBenhNhan
                WHERE tp.MaThanhToan LIKE @Key
                   OR tp.MaPhieuKham LIKE @Key
                   OR bn.HoTen LIKE @Key
                ORDER BY tp.NgayThanhToan DESC,
                         tp.MaThanhToan DESC",
                Db.P("@Key", "%" +
                    (key ?? "").Trim() + "%"));
        }

        public DataTable LayPhieuKham()
        {
            return Db.Query(@"
                SELECT
                    pk.MaPhieuKham,
                    pk.MaPhieuKham + N' - ' +
                    bn.HoTen AS HienThi
                FROM dbo.PhieuKham pk
                INNER JOIN dbo.HoSoBenhAn hs
                    ON pk.MaHoSo = hs.MaHoSo
                INNER JOIN dbo.BenhNhan bn
                    ON hs.MaBenhNhan = bn.MaBenhNhan
                ORDER BY pk.NgayGioKham DESC");
        }

        public void Them(
            string maThanhToan,
            string maPhieuKham,
            DateTime ngayThanhToan,
            decimal soTien,
            string phuongThuc,
            string ghiChu)
        {
            KiemTra(maThanhToan, maPhieuKham,
                ngayThanhToan, soTien, phuongThuc);

            Db.Execute(@"
                INSERT INTO dbo.ThanhToanVienPhi
                (
                    MaThanhToan, MaPhieuKham,
                    NgayThanhToan, SoTien,
                    PhuongThuc, GhiChu
                )
                VALUES
                (
                    @Ma, @MaPhieu, @Ngay,
                    @SoTien, @PhuongThuc, @GhiChu
                )",
                Db.P("@Ma", maThanhToan.Trim()),
                Db.P("@MaPhieu", maPhieuKham),
                Db.P("@Ngay", ngayThanhToan.Date),
                Db.P("@SoTien", soTien),
                Db.P("@PhuongThuc", phuongThuc),
                Db.P("@GhiChu", NullNeuRong(ghiChu)));
        }

        public void Sua(
            string maThanhToan,
            string maPhieuKham,
            DateTime ngayThanhToan,
            decimal soTien,
            string phuongThuc,
            string ghiChu)
        {
            KiemTra(maThanhToan, maPhieuKham,
                ngayThanhToan, soTien, phuongThuc);

            int n = Db.Execute(@"
                UPDATE dbo.ThanhToanVienPhi
                SET MaPhieuKham = @MaPhieu,
                    NgayThanhToan = @Ngay,
                    SoTien = @SoTien,
                    PhuongThuc = @PhuongThuc,
                    GhiChu = @GhiChu
                WHERE MaThanhToan = @Ma",
                Db.P("@Ma", maThanhToan.Trim()),
                Db.P("@MaPhieu", maPhieuKham),
                Db.P("@Ngay", ngayThanhToan.Date),
                Db.P("@SoTien", soTien),
                Db.P("@PhuongThuc", phuongThuc),
                Db.P("@GhiChu", NullNeuRong(ghiChu)));

            if (n == 0)
                throw new Exception(
                    "Không tìm thấy khoản thanh toán cần sửa.");
        }

        public void Xoa(string maThanhToan)
        {
            if (string.IsNullOrWhiteSpace(maThanhToan))
                throw new Exception(
                    "Hãy chọn khoản thanh toán cần xóa.");

            int n = Db.Execute(@"
                DELETE FROM dbo.ThanhToanVienPhi
                WHERE MaThanhToan = @Ma",
                Db.P("@Ma", maThanhToan.Trim()));

            if (n == 0)
                throw new Exception(
                    "Không tìm thấy khoản thanh toán.");
        }

        private static object NullNeuRong(string s)
        {
            return string.IsNullOrWhiteSpace(s)
                ? (object)DBNull.Value : s.Trim();
        }

        private static void KiemTra(
            string ma,
            string phieu,
            DateTime ngay,
            decimal tien,
            string phuongThuc)
        {
            if (string.IsNullOrWhiteSpace(ma) ||
                ma.Trim().Length > 20)
                throw new Exception(
                    "Mã thanh toán phải có từ 1 đến 20 ký tự.");

            if (string.IsNullOrWhiteSpace(phieu))
                throw new Exception(
                    "Vui lòng chọn phiếu khám.");

            if (ngay.Date > DateTime.Today)
                throw new Exception(
                    "Ngày thanh toán không được ở tương lai.");

            if (tien < 0)
                throw new Exception(
                    "Số tiền không được âm.");

            if (string.IsNullOrWhiteSpace(phuongThuc))
                throw new Exception(
                    "Vui lòng chọn phương thức thanh toán.");
        }
    }
}
