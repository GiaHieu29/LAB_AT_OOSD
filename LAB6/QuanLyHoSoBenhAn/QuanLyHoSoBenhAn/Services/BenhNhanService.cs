
using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyHoSoBenhAn.Data;

namespace QuanLyHoSoBenhAn.Services
{
    public class BenhNhanService
    {
        // HIỂN THỊ DANH SÁCH BỆNH NHÂN
        public DataTable LayDanhSach()
        {
            return Db.Query(@"
                SELECT
                    MaBenhNhan,
                    HoTen,
                    NgaySinh,
                    GioiTinh,
                    DiaChi,
                    DienThoai,
                    Anh3x4
                FROM dbo.BenhNhan
                ORDER BY MaBenhNhan");
        }

        // TÌM KIẾM THEO MÃ, TÊN, SỐ ĐIỆN THOẠI
        public DataTable TimKiem(string tuKhoa)
        {
            string key = (tuKhoa ?? "").Trim();

            return Db.Query(@"
                SELECT
                    MaBenhNhan,
                    HoTen,
                    NgaySinh,
                    GioiTinh,
                    DiaChi,
                    DienThoai,
                    Anh3x4
                FROM dbo.BenhNhan
                WHERE MaBenhNhan LIKE @TuKhoa
                   OR HoTen LIKE @TuKhoa
                   OR DienThoai LIKE @TuKhoa
                ORDER BY MaBenhNhan",
                Db.P("@TuKhoa", "%" + key + "%"));
        }

        // THÊM BỆNH NHÂN
        public void Them(
            string ma,
            string hoTen,
            DateTime ngaySinh,
            string gioiTinh,
            string diaChi,
            string dienThoai,
            string anh3x4)
        {
            KiemTraDuLieu(
                ma, hoTen, ngaySinh, diaChi, dienThoai);

            Db.Execute(@"
                INSERT INTO dbo.BenhNhan
                (
                    MaBenhNhan,
                    HoTen,
                    NgaySinh,
                    GioiTinh,
                    DiaChi,
                    DienThoai,
                    Anh3x4
                )
                VALUES
                (
                    @Ma,
                    @HoTen,
                    @NgaySinh,
                    @GioiTinh,
                    @DiaChi,
                    @DienThoai,
                    @Anh
                )",
                Db.P("@Ma", ma.Trim()),
                Db.P("@HoTen", hoTen.Trim()),
                Db.P("@NgaySinh", ngaySinh.Date),
                Db.P("@GioiTinh", GiaTriRong(gioiTinh)),
                Db.P("@DiaChi", diaChi.Trim()),
                Db.P("@DienThoai", dienThoai.Trim()),
                Db.P("@Anh", GiaTriRong(anh3x4))
            );
        }

        // CẬP NHẬT BỆNH NHÂN
        public void Sua(
            string ma,
            string hoTen,
            DateTime ngaySinh,
            string gioiTinh,
            string diaChi,
            string dienThoai,
            string anh3x4)
        {
            KiemTraDuLieu(
                ma, hoTen, ngaySinh, diaChi, dienThoai);

            int soDong = Db.Execute(@"
                UPDATE dbo.BenhNhan
                SET
                    HoTen = @HoTen,
                    NgaySinh = @NgaySinh,
                    GioiTinh = @GioiTinh,
                    DiaChi = @DiaChi,
                    DienThoai = @DienThoai,
                    Anh3x4 = @Anh
                WHERE MaBenhNhan = @Ma",
                Db.P("@Ma", ma.Trim()),
                Db.P("@HoTen", hoTen.Trim()),
                Db.P("@NgaySinh", ngaySinh.Date),
                Db.P("@GioiTinh", GiaTriRong(gioiTinh)),
                Db.P("@DiaChi", diaChi.Trim()),
                Db.P("@DienThoai", dienThoai.Trim()),
                Db.P("@Anh", GiaTriRong(anh3x4))
            );

            if (soDong == 0)
                throw new Exception(
                    "Không tìm thấy bệnh nhân cần sửa.");
        }

        // XÓA BỆNH NHÂN
        public void Xoa(string ma)
        {
            if (string.IsNullOrWhiteSpace(ma))
                throw new Exception(
                    "Vui lòng chọn bệnh nhân cần xóa.");

            int soDong = Db.Execute(@"
                DELETE FROM dbo.BenhNhan
                WHERE MaBenhNhan = @Ma",
                Db.P("@Ma", ma.Trim()));

            if (soDong == 0)
                throw new Exception(
                    "Không tìm thấy bệnh nhân cần xóa.");
        }

        // KIỂM TRA DỮ LIỆU ĐẦU VÀO
        private void KiemTraDuLieu(
            string ma,
            string hoTen,
            DateTime ngaySinh,
            string diaChi,
            string dienThoai)
        {
            if (string.IsNullOrWhiteSpace(ma))
                throw new Exception(
                    "Mã bệnh nhân không được để trống.");

            if (ma.Trim().Length > 20)
                throw new Exception(
                    "Mã bệnh nhân tối đa 20 ký tự.");

            if (string.IsNullOrWhiteSpace(hoTen))
                throw new Exception(
                    "Họ tên không được để trống.");

            if (string.IsNullOrWhiteSpace(diaChi))
                throw new Exception(
                    "Địa chỉ không được để trống.");

            if (string.IsNullOrWhiteSpace(dienThoai))
                throw new Exception(
                    "Số điện thoại không được để trống.");

            if (ngaySinh.Date > DateTime.Today)
                throw new Exception(
                    "Ngày sinh không được lớn hơn hôm nay.");
        }

        private object GiaTriRong(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return DBNull.Value;

            return value.Trim();
        }
    }
}
