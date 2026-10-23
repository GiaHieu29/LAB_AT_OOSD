
using System;
using System.Data;
using QuanLyHoSoBenhAn.Data;

namespace QuanLyHoSoBenhAn.Services
{
    public class LichHenKhamService
    {
        public DataTable LayDanhSach(string tuKhoa = "")
        {
            return Db.Query(@"
                SELECT
                    lh.MaLichHen,
                    lh.MaHoSo,
                    bn.HoTen AS TenBenhNhan,
                    lh.MaKhoa,
                    k.TenKhoa,
                    lh.NgayGioHen,
                    lh.LoaiHen,
                    lh.NoiDung,
                    lh.TrangThai
                FROM dbo.LichHenKham lh
                INNER JOIN dbo.HoSoBenhAn hs
                    ON lh.MaHoSo = hs.MaHoSo
                INNER JOIN dbo.BenhNhan bn
                    ON hs.MaBenhNhan = bn.MaBenhNhan
                INNER JOIN dbo.KhoaDieuTri k
                    ON lh.MaKhoa = k.MaKhoa
                WHERE lh.MaLichHen LIKE @Key
                   OR lh.MaHoSo LIKE @Key
                   OR bn.HoTen LIKE @Key
                   OR k.TenKhoa LIKE @Key
                ORDER BY lh.NgayGioHen DESC",
                Db.P("@Key",
                    "%" + (tuKhoa ?? "").Trim() + "%"));
        }

        public DataTable LayHoSo()
        {
            return Db.Query(@"
                SELECT
                    hs.MaHoSo,
                    hs.MaHoSo + N' - ' +
                    bn.HoTen AS HienThi
                FROM dbo.HoSoBenhAn hs
                INNER JOIN dbo.BenhNhan bn
                    ON hs.MaBenhNhan = bn.MaBenhNhan
                ORDER BY hs.MaHoSo");
        }

        public DataTable LayKhoa()
        {
            return Db.Query(@"
                SELECT MaKhoa, TenKhoa
                FROM dbo.KhoaDieuTri
                ORDER BY TenKhoa");
        }

        public void Them(
            string ma, string maHoSo, string maKhoa,
            DateTime ngayHen, string loaiHen,
            string noiDung, string trangThai)
        {
            KiemTra(ma, maHoSo, maKhoa,
                ngayHen, loaiHen, trangThai);

            Db.Execute(@"
                INSERT INTO dbo.LichHenKham
                (
                    MaLichHen, MaHoSo, MaKhoa,
                    NgayGioHen, LoaiHen,
                    NoiDung, TrangThai
                )
                VALUES
                (
                    @Ma, @MaHoSo, @MaKhoa,
                    @NgayHen, @LoaiHen,
                    @NoiDung, @TrangThai
                )",
                Db.P("@Ma", ma.Trim()),
                Db.P("@MaHoSo", maHoSo),
                Db.P("@MaKhoa", maKhoa),
                Db.P("@NgayHen", ngayHen),
                Db.P("@LoaiHen", loaiHen),
                Db.P("@NoiDung", NullNeuRong(noiDung)),
                Db.P("@TrangThai", trangThai));
        }

        public void Sua(
            string ma, string maHoSo, string maKhoa,
            DateTime ngayHen, string loaiHen,
            string noiDung, string trangThai)
        {
            KiemTra(ma, maHoSo, maKhoa,
                ngayHen, loaiHen, trangThai);

            int n = Db.Execute(@"
                UPDATE dbo.LichHenKham
                SET MaHoSo = @MaHoSo,
                    MaKhoa = @MaKhoa,
                    NgayGioHen = @NgayHen,
                    LoaiHen = @LoaiHen,
                    NoiDung = @NoiDung,
                    TrangThai = @TrangThai
                WHERE MaLichHen = @Ma",
                Db.P("@Ma", ma.Trim()),
                Db.P("@MaHoSo", maHoSo),
                Db.P("@MaKhoa", maKhoa),
                Db.P("@NgayHen", ngayHen),
                Db.P("@LoaiHen", loaiHen),
                Db.P("@NoiDung", NullNeuRong(noiDung)),
                Db.P("@TrangThai", trangThai));

            if (n == 0)
                throw new Exception(
                    "Không tìm thấy lịch hẹn cần sửa.");
        }

        public void Xoa(string ma)
        {
            if (string.IsNullOrWhiteSpace(ma))
                throw new Exception(
                    "Vui lòng chọn lịch hẹn cần xóa.");

            int n = Db.Execute(@"
                DELETE FROM dbo.LichHenKham
                WHERE MaLichHen = @Ma",
                Db.P("@Ma", ma.Trim()));

            if (n == 0)
                throw new Exception(
                    "Không tìm thấy lịch hẹn cần xóa.");
        }

        private static object NullNeuRong(string text)
        {
            return string.IsNullOrWhiteSpace(text)
                ? (object)DBNull.Value
                : text.Trim();
        }

        private static void KiemTra(
            string ma, string hoSo, string khoa,
            DateTime ngayHen, string loai,
            string trangThai)
        {
            if (string.IsNullOrWhiteSpace(ma) ||
                ma.Trim().Length > 20)
                throw new Exception(
                    "Mã lịch hẹn phải có từ 1 đến 20 ký tự.");

            if (string.IsNullOrWhiteSpace(hoSo))
                throw new Exception(
                    "Vui lòng chọn hồ sơ bệnh án.");

            if (string.IsNullOrWhiteSpace(khoa))
                throw new Exception(
                    "Vui lòng chọn khoa điều trị.");

            if (string.IsNullOrWhiteSpace(loai))
                throw new Exception(
                    "Vui lòng chọn loại lịch hẹn.");

            if (trangThai != "Đã đặt lịch" &&
                trangThai != "Đã khám" &&
                trangThai != "Đã hủy")
            {
                throw new Exception(
                    "Trạng thái lịch hẹn không hợp lệ.");
            }

            if (ngayHen < new DateTime(2000, 1, 1))
                throw new Exception(
                    "Ngày hẹn không hợp lệ.");
        }
    }
}
