
using System;
using System.Data;
using QuanLyChoThueVanPhong.Data;

namespace QuanLyChoThueVanPhong.Services
{
    public class KhachHangService
    {
        // =========================================
        // 1. LẤY DANH SÁCH VÀ TÌM KIẾM
        // =========================================

        public DataTable LayDanhSach(string tuKhoa = "")
        {
            string key = (tuKhoa ?? "").Trim();

            return Db.Query(@"
                SELECT
                    MaKhachHang,
                    TenKhachHang,
                    LoaiKhachHang,
                    MaSoThue,
                    NguoiDaiDien,
                    DienThoai,
                    Email,
                    DiaChi,
                    GhiChu
                FROM dbo.KhachHang
                WHERE MaKhachHang LIKE @Key
                   OR TenKhachHang LIKE @Key
                   OR LoaiKhachHang LIKE @Key
                   OR MaSoThue LIKE @Key
                   OR NguoiDaiDien LIKE @Key
                   OR DienThoai LIKE @Key
                   OR Email LIKE @Key
                ORDER BY MaKhachHang",
                Db.P("@Key", "%" + key + "%"));
        }

        // =========================================
        // 2. THÊM KHÁCH HÀNG
        // =========================================

        public void Them(
            string ma,
            string ten,
            string loai,
            string maSoThue,
            string nguoiDaiDien,
            string dienThoai,
            string email,
            string diaChi,
            string ghiChu)
        {
            KiemTra(
                ma, ten, loai, maSoThue,
                nguoiDaiDien, dienThoai, email);

            Db.Execute(@"
                INSERT INTO dbo.KhachHang
                (
                    MaKhachHang,
                    TenKhachHang,
                    LoaiKhachHang,
                    MaSoThue,
                    NguoiDaiDien,
                    DienThoai,
                    Email,
                    DiaChi,
                    GhiChu
                )
                VALUES
                (
                    @Ma,
                    @Ten,
                    @Loai,
                    @MST,
                    @DaiDien,
                    @DienThoai,
                    @Email,
                    @DiaChi,
                    @GhiChu
                )",
                Db.P("@Ma", ma.Trim()),
                Db.P("@Ten", ten.Trim()),
                Db.P("@Loai", loai),
                Db.P("@MST", NullNeuRong(maSoThue)),
                Db.P("@DaiDien",
                    NullNeuRong(nguoiDaiDien)),
                Db.P("@DienThoai",
                    NullNeuRong(dienThoai)),
                Db.P("@Email", NullNeuRong(email)),
                Db.P("@DiaChi", NullNeuRong(diaChi)),
                Db.P("@GhiChu", NullNeuRong(ghiChu)));
        }

        // =========================================
        // 3. SỬA KHÁCH HÀNG
        // =========================================

        public void Sua(
            string ma,
            string ten,
            string loai,
            string maSoThue,
            string nguoiDaiDien,
            string dienThoai,
            string email,
            string diaChi,
            string ghiChu)
        {
            KiemTra(
                ma, ten, loai, maSoThue,
                nguoiDaiDien, dienThoai, email);

            int soDong = Db.Execute(@"
                UPDATE dbo.KhachHang
                SET
                    TenKhachHang = @Ten,
                    LoaiKhachHang = @Loai,
                    MaSoThue = @MST,
                    NguoiDaiDien = @DaiDien,
                    DienThoai = @DienThoai,
                    Email = @Email,
                    DiaChi = @DiaChi,
                    GhiChu = @GhiChu
                WHERE MaKhachHang = @Ma",
                Db.P("@Ma", ma.Trim()),
                Db.P("@Ten", ten.Trim()),
                Db.P("@Loai", loai),
                Db.P("@MST", NullNeuRong(maSoThue)),
                Db.P("@DaiDien",
                    NullNeuRong(nguoiDaiDien)),
                Db.P("@DienThoai",
                    NullNeuRong(dienThoai)),
                Db.P("@Email", NullNeuRong(email)),
                Db.P("@DiaChi", NullNeuRong(diaChi)),
                Db.P("@GhiChu", NullNeuRong(ghiChu)));

            if (soDong == 0)
            {
                throw new Exception(
                    "Không tìm thấy khách hàng cần sửa.");
            }
        }

        // =========================================
        // 4. XÓA KHÁCH HÀNG
        // =========================================

        public void Xoa(string ma)
        {
            if (string.IsNullOrWhiteSpace(ma))
            {
                throw new Exception(
                    "Vui lòng chọn khách hàng cần xóa.");
            }

            // Không xóa khách đã có hợp đồng.
            int soHopDong = Convert.ToInt32(
                Db.Scalar(@"
                    SELECT COUNT(*)
                    FROM dbo.HopDong
                    WHERE MaKhachHang = @Ma",
                    Db.P("@Ma", ma.Trim())));

            if (soHopDong > 0)
            {
                throw new Exception(
                    "Khách hàng đã có " + soHopDong +
                    " hợp đồng thuê.\n" +
                    "Không thể xóa vì cần bảo toàn " +
                    "lịch sử hợp đồng.");
            }

            // Không xóa khách đang có nhân viên
            // được quản lý trong cao ốc.
            int soNhanVien = Convert.ToInt32(
                Db.Scalar(@"
                    SELECT COUNT(*)
                    FROM dbo.NhanVienVanPhong
                    WHERE MaKhachHang = @Ma",
                    Db.P("@Ma", ma.Trim())));

            if (soNhanVien > 0)
            {
                throw new Exception(
                    "Khách hàng đang có " + soNhanVien +
                    " nhân viên văn phòng.\n" +
                    "Không thể xóa dữ liệu liên kết.");
            }

            int soDong = Db.Execute(@"
                DELETE FROM dbo.KhachHang
                WHERE MaKhachHang = @Ma",
                Db.P("@Ma", ma.Trim()));

            if (soDong == 0)
            {
                throw new Exception(
                    "Không tìm thấy khách hàng cần xóa.");
            }
        }

        // =========================================
        // 5. KIỂM TRA DỮ LIỆU ĐẦU VÀO
        // =========================================

        private static void KiemTra(
            string ma,
            string ten,
            string loai,
            string mst,
            string daiDien,
            string dienThoai,
            string email)
        {
            if (string.IsNullOrWhiteSpace(ma) ||
                ma.Trim().Length > 20)
            {
                throw new Exception(
                    "Mã khách hàng phải có từ 1 đến 20 ký tự.");
            }

            if (string.IsNullOrWhiteSpace(ten) ||
                ten.Trim().Length > 200)
            {
                throw new Exception(
                    "Tên khách hàng phải có từ 1 đến 200 ký tự.");
            }

            if (loai != "Công ty" &&
                loai != "Cá nhân")
            {
                throw new Exception(
                    "Loại khách hàng không hợp lệ.");
            }

            if (!string.IsNullOrWhiteSpace(mst) &&
                mst.Trim().Length > 30)
            {
                throw new Exception(
                    "Mã số thuế không được vượt quá 30 ký tự.");
            }

            if (!string.IsNullOrWhiteSpace(daiDien) &&
                daiDien.Trim().Length > 150)
            {
                throw new Exception(
                    "Người đại diện không vượt quá 150 ký tự.");
            }

            if (!string.IsNullOrWhiteSpace(dienThoai) &&
                dienThoai.Trim().Length > 20)
            {
                throw new Exception(
                    "Số điện thoại không vượt quá 20 ký tự.");
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                string e = email.Trim();

                if (e.Length > 150 ||
                    !e.Contains("@") ||
                    e.StartsWith("@") ||
                    e.EndsWith("@"))
                {
                    throw new Exception(
                        "Email không đúng định dạng cơ bản.");
                }
            }
        }

        private static object NullNeuRong(string text)
        {
            return string.IsNullOrWhiteSpace(text)
                ? (object)DBNull.Value
                : text.Trim();
        }
    }
}
