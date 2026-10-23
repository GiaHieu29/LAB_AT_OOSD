
using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyHoSoBenhAn.Data;

namespace QuanLyHoSoBenhAn.Services
{
    public class PhieuKhamService
    {
        // =========================================
        // 1. LẤY DANH SÁCH VÀ TÌM KIẾM PHIẾU KHÁM
        // =========================================

        public DataTable LayDanhSach(string tuKhoa = "")
        {
            return Db.Query(@"
                SELECT
                    pk.MaPhieuKham,
                    pk.MaHoSo,
                    bn.HoTen AS TenBenhNhan,
                    pk.MaKhoa,
                    k.TenKhoa,
                    pk.MaBacSi,
                    bs.HoTen AS TenBacSi,
                    pk.NgayGioKham,
                    pk.LoaiKham,
                    pk.TrieuChung,
                    pk.NhietDo,
                    pk.HuyetAp,
                    pk.ChanDoan,
                    pk.HuongDieuTri,
                    pk.NgayXuatVien,
                    pk.GhiChu,
                    pk.TrangThai
                FROM dbo.PhieuKham pk
                INNER JOIN dbo.HoSoBenhAn hs
                    ON hs.MaHoSo = pk.MaHoSo
                INNER JOIN dbo.BenhNhan bn
                    ON bn.MaBenhNhan = hs.MaBenhNhan
                INNER JOIN dbo.KhoaDieuTri k
                    ON k.MaKhoa = pk.MaKhoa
                INNER JOIN dbo.BacSi bs
                    ON bs.MaBacSi = pk.MaBacSi
                WHERE pk.MaPhieuKham LIKE @Key
                   OR pk.MaHoSo LIKE @Key
                   OR bn.HoTen LIKE @Key
                   OR bs.HoTen LIKE @Key
                ORDER BY
                    pk.NgayGioKham DESC,
                    pk.MaPhieuKham DESC",
                Db.P("@Key",
                    "%" + (tuKhoa ?? "").Trim() + "%"));
        }

        // =========================================
        // 2. LẤY HỒ SƠ BỆNH ÁN
        // =========================================

        public DataTable LayHoSo()
        {
            return Db.Query(@"
                SELECT
                    hs.MaHoSo,
                    hs.MaHoSo + N' - ' + bn.HoTen AS HienThi
                FROM dbo.HoSoBenhAn hs
                INNER JOIN dbo.BenhNhan bn
                    ON bn.MaBenhNhan = hs.MaBenhNhan
                ORDER BY hs.MaHoSo");
        }

        // =========================================
        // 3. LẤY KHOA ĐIỀU TRỊ
        // =========================================

        public DataTable LayKhoa()
        {
            return Db.Query(@"
                SELECT MaKhoa, TenKhoa
                FROM dbo.KhoaDieuTri
                ORDER BY TenKhoa");
        }

        // =========================================
        // 4. LẤY BÁC SĨ THEO KHOA
        // =========================================

        public DataTable LayBacSiTheoKhoa(string maKhoa)
        {
            return Db.Query(@"
                SELECT MaBacSi, HoTen
                FROM dbo.BacSi
                WHERE MaKhoa = @MaKhoa
                  AND DangLamViec = 1
                ORDER BY HoTen",
                Db.P("@MaKhoa", maKhoa));
        }

        // =========================================
        // 5. THÊM PHIẾU KHÁM
        // =========================================

        public void Them(
            string maPhieu,
            string maHoSo,
            string maKhoa,
            string maBacSi,
            DateTime ngayGio,
            string loaiKham,
            string trieuChung,
            decimal? nhietDo,
            string huyetAp,
            string chanDoan,
            string huongDieuTri,
            DateTime? ngayXuatVien,
            string ghiChu,
            string trangThai)
        {
            KiemTra(
                maPhieu, maHoSo, maKhoa,
                maBacSi, ngayGio, loaiKham,
                nhietDo, ngayXuatVien, trangThai);

            KiemTraLienKet(
                maHoSo, maKhoa, maBacSi);

            Db.Execute(@"
                INSERT INTO dbo.PhieuKham
                (
                    MaPhieuKham,
                    MaHoSo,
                    MaBacSi,
                    MaKhoa,
                    NgayGioKham,
                    LoaiKham,
                    TrieuChung,
                    NhietDo,
                    HuyetAp,
                    ChanDoan,
                    HuongDieuTri,
                    NgayXuatVien,
                    GhiChu,
                    TrangThai
                )
                VALUES
                (
                    @MaPhieu,
                    @MaHS,
                    @MaBS,
                    @MaKhoa,
                    @NgayGio,
                    @LoaiKham,
                    @TrieuChung,
                    @NhietDo,
                    @HuyetAp,
                    @ChanDoan,
                    @HuongDieuTri,
                    @NgayXuatVien,
                    @GhiChu,
                    @TrangThai
                )",
                Db.P("@MaPhieu", maPhieu.Trim()),
                Db.P("@MaHS", maHoSo),
                Db.P("@MaBS", maBacSi),
                Db.P("@MaKhoa", maKhoa),
                Db.P("@NgayGio", ngayGio),
                Db.P("@LoaiKham", loaiKham),
                Db.P("@TrieuChung",
                    RongThanhNull(trieuChung)),
                Db.P("@NhietDo",
                    nhietDo.HasValue
                        ? (object)nhietDo.Value
                        : DBNull.Value),
                Db.P("@HuyetAp",
                    RongThanhNull(huyetAp)),
                Db.P("@ChanDoan",
                    RongThanhNull(chanDoan)),
                Db.P("@HuongDieuTri",
                    RongThanhNull(huongDieuTri)),
                Db.P("@NgayXuatVien",
                    ngayXuatVien.HasValue
                        ? (object)ngayXuatVien.Value
                        : DBNull.Value),
                Db.P("@GhiChu",
                    RongThanhNull(ghiChu)),
                Db.P("@TrangThai", trangThai));
        }

        // =========================================
        // 6. SỬA PHIẾU KHÁM
        // =========================================

        public void Sua(
            string maPhieu,
            string maHoSo,
            string maKhoa,
            string maBacSi,
            DateTime ngayGio,
            string loaiKham,
            string trieuChung,
            decimal? nhietDo,
            string huyetAp,
            string chanDoan,
            string huongDieuTri,
            DateTime? ngayXuatVien,
            string ghiChu,
            string trangThai)
        {
            KiemTra(
                maPhieu, maHoSo, maKhoa,
                maBacSi, ngayGio, loaiKham,
                nhietDo, ngayXuatVien, trangThai);

            KiemTraLienKet(
                maHoSo, maKhoa, maBacSi);

            int n = Db.Execute(@"
                UPDATE dbo.PhieuKham
                SET
                    MaHoSo = @MaHS,
                    MaBacSi = @MaBS,
                    MaKhoa = @MaKhoa,
                    NgayGioKham = @NgayGio,
                    LoaiKham = @LoaiKham,
                    TrieuChung = @TrieuChung,
                    NhietDo = @NhietDo,
                    HuyetAp = @HuyetAp,
                    ChanDoan = @ChanDoan,
                    HuongDieuTri = @HuongDieuTri,
                    NgayXuatVien = @NgayXuatVien,
                    GhiChu = @GhiChu,
                    TrangThai = @TrangThai
                WHERE MaPhieuKham = @MaPhieu",
                Db.P("@MaPhieu", maPhieu.Trim()),
                Db.P("@MaHS", maHoSo),
                Db.P("@MaBS", maBacSi),
                Db.P("@MaKhoa", maKhoa),
                Db.P("@NgayGio", ngayGio),
                Db.P("@LoaiKham", loaiKham),
                Db.P("@TrieuChung",
                    RongThanhNull(trieuChung)),
                Db.P("@NhietDo",
                    nhietDo.HasValue
                        ? (object)nhietDo.Value
                        : DBNull.Value),
                Db.P("@HuyetAp",
                    RongThanhNull(huyetAp)),
                Db.P("@ChanDoan",
                    RongThanhNull(chanDoan)),
                Db.P("@HuongDieuTri",
                    RongThanhNull(huongDieuTri)),
                Db.P("@NgayXuatVien",
                    ngayXuatVien.HasValue
                        ? (object)ngayXuatVien.Value
                        : DBNull.Value),
                Db.P("@GhiChu",
                    RongThanhNull(ghiChu)),
                Db.P("@TrangThai", trangThai));

            if (n == 0)
                throw new Exception(
                    "Không tìm thấy phiếu khám cần sửa.");
        }

        // =========================================
        // 7. XÓA PHIẾU KHÁM
        // =========================================

        public void Xoa(string maPhieu)
        {
            if (string.IsNullOrWhiteSpace(maPhieu))
                throw new Exception(
                    "Hãy chọn phiếu khám cần xóa.");

            int n = Db.Execute(@"
                DELETE FROM dbo.PhieuKham
                WHERE MaPhieuKham = @Ma",
                Db.P("@Ma", maPhieu.Trim()));

            if (n == 0)
                throw new Exception(
                    "Không tìm thấy phiếu khám.");
        }

        // =========================================
        // 8. KIỂM TRA DỮ LIỆU
        // =========================================

        private static object RongThanhNull(string s)
        {
            return string.IsNullOrWhiteSpace(s)
                ? (object)DBNull.Value
                : s.Trim();
        }

        private static void KiemTra(
            string ma,
            string hoSo,
            string khoa,
            string bacSi,
            DateTime ngay,
            string loai,
            decimal? nhietDo,
            DateTime? xuatVien,
            string trangThai)
        {
            if (string.IsNullOrWhiteSpace(ma) ||
                ma.Trim().Length > 20)
                throw new Exception(
                    "Mã phiếu khám phải có từ 1 đến 20 ký tự.");

            if (string.IsNullOrWhiteSpace(hoSo) ||
                string.IsNullOrWhiteSpace(khoa) ||
                string.IsNullOrWhiteSpace(bacSi))
                throw new Exception(
                    "Phải chọn hồ sơ, khoa điều trị và bác sĩ.");

            if (loai != "Khám bệnh" &&
                loai != "Khám định kỳ")
                throw new Exception(
                    "Loại khám không hợp lệ.");

            if (trangThai != "Đang khám" &&
                trangThai != "Đang điều trị" &&
                trangThai != "Đã hoàn tất")
                throw new Exception(
                    "Trạng thái không hợp lệ.");

            if (ngay > DateTime.Now.AddMinutes(2))
                throw new Exception(
                    "Thời điểm khám không được ở tương lai.");

            if (nhietDo.HasValue &&
                (nhietDo.Value < 25 ||
                 nhietDo.Value > 45))
                throw new Exception(
                    "Nhiệt độ ngoài khoảng kiểm tra nhập liệu.");

            if (xuatVien.HasValue &&
                xuatVien.Value.Date < ngay.Date)
                throw new Exception(
                    "Ngày xuất viện không được trước ngày khám.");
        }

        // =========================================
        // 9. KIỂM TRA QUAN HỆ KHÓA NGOẠI
        // =========================================

        private static void KiemTraLienKet(
            string hoSo,
            string khoa,
            string bacSi)
        {
            object soHoSo = Db.Scalar(@"
                SELECT COUNT(*)
                FROM dbo.HoSoBenhAn
                WHERE MaHoSo = @MaHS",
                Db.P("@MaHS", hoSo));

            if (Convert.ToInt32(soHoSo) == 0)
                throw new Exception(
                    "Hồ sơ bệnh án không tồn tại.");

            object soBacSi = Db.Scalar(@"
                SELECT COUNT(*)
                FROM dbo.BacSi
                WHERE MaBacSi = @MaBS
                  AND MaKhoa = @MaKhoa",
                Db.P("@MaBS", bacSi),
                Db.P("@MaKhoa", khoa));

            if (Convert.ToInt32(soBacSi) == 0)
                throw new Exception(
                    "Bác sĩ không thuộc khoa đã chọn.");
        }
    }
}
