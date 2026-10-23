
using System;
using System.Data;
using QuanLyHoSoBenhAn.Data;

namespace QuanLyHoSoBenhAn.Services
{
    public class XetNghiemService
    {
        public DataTable LayDanhSach(string tuKhoa = "")
        {
            return Db.Query(@"
                SELECT
                    xn.MaXetNghiem,
                    xn.MaPhieuKham,
                    bn.HoTen AS TenBenhNhan,
                    xn.TenXetNghiem,
                    xn.NgayChiDinh,
                    xn.NgayCoKetQua,
                    xn.KetQua,
                    xn.GhiChu,
                    xn.TrangThai
                FROM dbo.XetNghiem xn
                INNER JOIN dbo.PhieuKham pk
                    ON xn.MaPhieuKham = pk.MaPhieuKham
                INNER JOIN dbo.HoSoBenhAn hs
                    ON pk.MaHoSo = hs.MaHoSo
                INNER JOIN dbo.BenhNhan bn
                    ON hs.MaBenhNhan = bn.MaBenhNhan
                WHERE xn.MaXetNghiem LIKE @Key
                   OR xn.MaPhieuKham LIKE @Key
                   OR xn.TenXetNghiem LIKE @Key
                   OR bn.HoTen LIKE @Key
                ORDER BY xn.NgayChiDinh DESC,
                         xn.MaXetNghiem DESC",
                Db.P("@Key", "%" +
                    (tuKhoa ?? "").Trim() + "%"));
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
            string ma, string maPhieu,
            string ten, DateTime ngayChiDinh,
            DateTime? ngayKetQua,
            string ketQua, string ghiChu,
            string trangThai)
        {
            KiemTra(ma, maPhieu, ten,
                ngayChiDinh, ngayKetQua,
                ketQua, trangThai);

            Db.Execute(@"
                INSERT INTO dbo.XetNghiem
                (
                    MaXetNghiem, MaPhieuKham,
                    TenXetNghiem, NgayChiDinh,
                    NgayCoKetQua, KetQua,
                    GhiChu, TrangThai
                )
                VALUES
                (
                    @Ma, @MaPhieu, @Ten,
                    @NgayChiDinh, @NgayKetQua,
                    @KetQua, @GhiChu, @TrangThai
                )",
                Db.P("@Ma", ma.Trim()),
                Db.P("@MaPhieu", maPhieu),
                Db.P("@Ten", ten.Trim()),
                Db.P("@NgayChiDinh", ngayChiDinh),
                Db.P("@NgayKetQua", NgayNull(ngayKetQua)),
                Db.P("@KetQua", ChuoiNull(ketQua)),
                Db.P("@GhiChu", ChuoiNull(ghiChu)),
                Db.P("@TrangThai", trangThai));
        }

        public void Sua(
            string ma, string maPhieu,
            string ten, DateTime ngayChiDinh,
            DateTime? ngayKetQua,
            string ketQua, string ghiChu,
            string trangThai)
        {
            KiemTra(ma, maPhieu, ten,
                ngayChiDinh, ngayKetQua,
                ketQua, trangThai);

            int n = Db.Execute(@"
                UPDATE dbo.XetNghiem
                SET MaPhieuKham = @MaPhieu,
                    TenXetNghiem = @Ten,
                    NgayChiDinh = @NgayChiDinh,
                    NgayCoKetQua = @NgayKetQua,
                    KetQua = @KetQua,
                    GhiChu = @GhiChu,
                    TrangThai = @TrangThai
                WHERE MaXetNghiem = @Ma",
                Db.P("@Ma", ma.Trim()),
                Db.P("@MaPhieu", maPhieu),
                Db.P("@Ten", ten.Trim()),
                Db.P("@NgayChiDinh", ngayChiDinh),
                Db.P("@NgayKetQua", NgayNull(ngayKetQua)),
                Db.P("@KetQua", ChuoiNull(ketQua)),
                Db.P("@GhiChu", ChuoiNull(ghiChu)),
                Db.P("@TrangThai", trangThai));

            if (n == 0)
                throw new Exception(
                    "Không tìm thấy xét nghiệm cần sửa.");
        }

        public void Xoa(string ma)
        {
            if (string.IsNullOrWhiteSpace(ma))
                throw new Exception(
                    "Hãy chọn xét nghiệm cần xóa.");

            int n = Db.Execute(@"
                DELETE FROM dbo.XetNghiem
                WHERE MaXetNghiem = @Ma",
                Db.P("@Ma", ma.Trim()));

            if (n == 0)
                throw new Exception(
                    "Không tìm thấy xét nghiệm cần xóa.");
        }

        private static object ChuoiNull(string s)
        {
            return string.IsNullOrWhiteSpace(s)
                ? (object)DBNull.Value : s.Trim();
        }

        private static object NgayNull(DateTime? d)
        {
            return d.HasValue
                ? (object)d.Value : DBNull.Value;
        }

        private static void KiemTra(
            string ma, string maPhieu,
            string ten, DateTime ngayChiDinh,
            DateTime? ngayKetQua,
            string ketQua, string trangThai)
        {
            if (string.IsNullOrWhiteSpace(ma) ||
                ma.Trim().Length > 20)
                throw new Exception(
                    "Mã xét nghiệm phải từ 1 đến 20 ký tự.");

            if (string.IsNullOrWhiteSpace(maPhieu))
                throw new Exception(
                    "Vui lòng chọn phiếu khám.");

            if (string.IsNullOrWhiteSpace(ten))
                throw new Exception(
                    "Vui lòng nhập tên xét nghiệm.");

            if (trangThai != "Đã chỉ định" &&
                trangThai != "Đang thực hiện" &&
                trangThai != "Đã có kết quả")
                throw new Exception(
                    "Trạng thái không hợp lệ.");

            if (ngayChiDinh.Date > DateTime.Today)
                throw new Exception(
                    "Ngày chỉ định không được ở tương lai.");

            if (ngayKetQua.HasValue &&
                ngayKetQua.Value.Date < ngayChiDinh.Date)
                throw new Exception(
                    "Ngày có kết quả phải từ ngày chỉ định.");

            if (trangThai == "Đã có kết quả" &&
                (!ngayKetQua.HasValue ||
                 string.IsNullOrWhiteSpace(ketQua)))
                throw new Exception(
                    "Đã có kết quả thì phải nhập " +
                    "ngày có kết quả và nội dung kết quả.");

            if (trangThai != "Đã có kết quả" &&
                (ngayKetQua.HasValue ||
                 !string.IsNullOrWhiteSpace(ketQua)))
                throw new Exception(
                    "Chỉ nhập kết quả khi chọn " +
                    "trạng thái Đã có kết quả.");
        }
    }
}
