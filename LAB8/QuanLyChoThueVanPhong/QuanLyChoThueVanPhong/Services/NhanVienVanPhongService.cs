
using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyChoThueVanPhong.Data;

namespace QuanLyChoThueVanPhong.Services
{
    public class NhanVienVanPhongService
    {
        public DataTable LayDanhSach(string key = "")
        {
            return Db.Query(@"
                SELECT
                    nv.MaNhanVien,
                    nv.MaKhachHang,
                    kh.TenKhachHang,
                    nv.MaPhong,
                    p.TenPhong,
                    t.TenTang,
                    nv.HoTen,
                    nv.NgaySinh,
                    nv.GioiTinh,
                    nv.CCCD,
                    nv.ChucVu,
                    nv.DienThoai,
                    nv.AnhNhanVien,
                    nv.TrangThai
                FROM dbo.NhanVienVanPhong nv
                JOIN dbo.KhachHang kh
                    ON nv.MaKhachHang = kh.MaKhachHang
                LEFT JOIN dbo.Phong p
                    ON nv.MaPhong = p.MaPhong
                LEFT JOIN dbo.Tang t
                    ON p.MaTang = t.MaTang
                WHERE nv.MaNhanVien LIKE @Key
                   OR nv.HoTen LIKE @Key
                   OR kh.TenKhachHang LIKE @Key
                   OR p.MaPhong LIKE @Key
                   OR nv.ChucVu LIKE @Key
                   OR nv.DienThoai LIKE @Key
                ORDER BY nv.MaNhanVien",
                Db.P("@Key", "%" + (key ?? "").Trim() + "%"));
        }

        public DataTable LayCongTy()
        {
            return Db.Query(@"
                SELECT MaKhachHang, TenKhachHang
                FROM dbo.KhachHang
                WHERE LoaiKhachHang = N'Công ty'
                ORDER BY TenKhachHang");
        }

        public DataTable LayPhongTheoCongTy(string maKH)
        {
            return Db.Query(@"
                SELECT DISTINCT
                    p.MaPhong,
                    p.MaPhong + N' - ' + p.TenPhong
                    + N' (' + t.TenTang + N')' AS HienThi
                FROM dbo.Phong p
                JOIN dbo.Tang t ON p.MaTang = t.MaTang
                JOIN dbo.ChiTietHopDong ct
                    ON ct.MaPhong = p.MaPhong
                JOIN dbo.HopDong h
                    ON h.MaHopDong = ct.MaHopDong
                WHERE h.MaKhachHang = @MaKH
                  AND h.TrangThai = N'Có hiệu lực'
                  AND ct.NgayNhanPhong <=
                      CAST(GETDATE() AS DATE)
                  AND
                  (
                      ct.NgayTraPhong IS NULL
                      OR ct.NgayTraPhong >=
                         CAST(GETDATE() AS DATE)
                  )
                  AND
                  (
                      h.NgayKetThuc >=
                          CAST(GETDATE() AS DATE)
                      OR EXISTS
                      (
                          SELECT 1
                          FROM dbo.PhieuGiaHan gh
                          WHERE gh.MaChiTietHD = ct.MaChiTietHD
                            AND gh.TrangThai = N'Đã duyệt'
                            AND gh.DenNgay >=
                                CAST(GETDATE() AS DATE)
                      )
                  )
                ORDER BY p.MaPhong",
                Db.P("@MaKH", maKH ?? ""));
        }

        private static object NullNeuRong(string value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? (object)DBNull.Value
                : value.Trim();
        }

        private static void KiemTraDuLieu(
            string ma, string maKH, string maPhong,
            string hoTen, DateTime? ngaySinh,
            string gioiTinh, string cccd,
            string chucVu, string dienThoai,
            string anh, string trangThai)
        {
            if (string.IsNullOrWhiteSpace(ma) ||
                ma.Trim().Length > 20)
                throw new Exception(
                    "Mã nhân viên phải từ 1 đến 20 ký tự.");

            if (string.IsNullOrWhiteSpace(maKH))
                throw new Exception("Hãy chọn công ty.");

            if (string.IsNullOrWhiteSpace(hoTen) ||
                hoTen.Trim().Length > 150)
                throw new Exception(
                    "Họ tên phải từ 1 đến 150 ký tự.");

            if (ngaySinh.HasValue &&
                ngaySinh.Value.Date > DateTime.Today)
                throw new Exception(
                    "Ngày sinh không được ở tương lai.");

            if (!string.IsNullOrWhiteSpace(gioiTinh) &&
                gioiTinh != "Nam" && gioiTinh != "Nữ" &&
                gioiTinh != "Khác")
                throw new Exception("Giới tính không hợp lệ.");

            if (!string.IsNullOrWhiteSpace(cccd) &&
                cccd.Trim().Length > 20)
                throw new Exception("CCCD quá 20 ký tự.");

            if (!string.IsNullOrWhiteSpace(chucVu) &&
                chucVu.Trim().Length > 100)
                throw new Exception("Chức vụ quá 100 ký tự.");

            if (!string.IsNullOrWhiteSpace(dienThoai) &&
                dienThoai.Trim().Length > 20)
                throw new Exception(
                    "Điện thoại quá 20 ký tự.");

            if (!string.IsNullOrWhiteSpace(anh) &&
                anh.Trim().Length > 500)
                throw new Exception("Đường dẫn ảnh quá dài.");

            if (trangThai != "Đang làm việc" &&
                trangThai != "Đã nghỉ")
                throw new Exception("Trạng thái không hợp lệ.");
        }

        private static void KiemTraCongTyPhong(
            SqlConnection conn, SqlTransaction tran,
            string maKH, string maPhong)
        {
            using (SqlCommand cmd = new SqlCommand(@"
                SELECT COUNT(*)
                FROM dbo.KhachHang
                WHERE MaKhachHang = @MaKH
                  AND LoaiKhachHang = N'Công ty'",
                conn, tran))
            {
                cmd.Parameters.AddWithValue("@MaKH", maKH);

                if (Convert.ToInt32(cmd.ExecuteScalar()) == 0)
                    throw new Exception(
                        "Công ty không tồn tại.");
            }

            if (string.IsNullOrWhiteSpace(maPhong))
                return;

            using (SqlCommand cmd = new SqlCommand(@"
                SELECT COUNT(*)
                FROM dbo.ChiTietHopDong ct
                JOIN dbo.HopDong h
                    ON ct.MaHopDong = h.MaHopDong
                WHERE ct.MaPhong = @Phong
                  AND h.MaKhachHang = @MaKH
                  AND h.TrangThai = N'Có hiệu lực'
                  AND ct.NgayNhanPhong <=
                      CAST(GETDATE() AS DATE)
                  AND
                  (
                      ct.NgayTraPhong IS NULL
                      OR ct.NgayTraPhong >=
                         CAST(GETDATE() AS DATE)
                  )
                  AND
                  (
                      h.NgayKetThuc >=
                          CAST(GETDATE() AS DATE)
                      OR EXISTS
                      (
                          SELECT 1
                          FROM dbo.PhieuGiaHan gh
                          WHERE gh.MaChiTietHD = ct.MaChiTietHD
                            AND gh.TrangThai = N'Đã duyệt'
                            AND gh.DenNgay >=
                                CAST(GETDATE() AS DATE)
                      )
                  )",
                conn, tran))
            {
                cmd.Parameters.AddWithValue("@Phong", maPhong);
                cmd.Parameters.AddWithValue("@MaKH", maKH);

                if (Convert.ToInt32(cmd.ExecuteScalar()) == 0)
                    throw new Exception(
                        "Phòng không thuộc hợp đồng đang " +
                        "có hiệu lực của công ty đã chọn.");
            }
        }

        public void Them(
            string ma, string maKH, string maPhong,
            string hoTen, DateTime? ngaySinh,
            string gioiTinh, string cccd,
            string chucVu, string dienThoai,
            string anh, string trangThai)
        {
            Luu(ma, maKH, maPhong, hoTen, ngaySinh,
                gioiTinh, cccd, chucVu, dienThoai,
                anh, trangThai, false);
        }

        public void Sua(
            string ma, string maKH, string maPhong,
            string hoTen, DateTime? ngaySinh,
            string gioiTinh, string cccd,
            string chucVu, string dienThoai,
            string anh, string trangThai)
        {
            Luu(ma, maKH, maPhong, hoTen, ngaySinh,
                gioiTinh, cccd, chucVu, dienThoai,
                anh, trangThai, true);
        }

        private void Luu(
            string ma, string maKH, string maPhong,
            string hoTen, DateTime? ngaySinh,
            string gioiTinh, string cccd,
            string chucVu, string dienThoai,
            string anh, string trangThai, bool sua)
        {
            KiemTraDuLieu(ma, maKH, maPhong,
                hoTen, ngaySinh, gioiTinh, cccd,
                chucVu, dienThoai, anh, trangThai);

            using (SqlConnection conn = Db.OpenConnection())
            using (SqlTransaction tran =
                conn.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                    KiemTraCongTyPhong(
                        conn, tran, maKH.Trim(), maPhong);

                    string sql = sua
                        ? @"
                        UPDATE dbo.NhanVienVanPhong
                        SET MaKhachHang = @MaKH,
                            MaPhong = @MaPhong,
                            HoTen = @HoTen,
                            NgaySinh = @NgaySinh,
                            GioiTinh = @GioiTinh,
                            CCCD = @CCCD,
                            ChucVu = @ChucVu,
                            DienThoai = @DienThoai,
                            AnhNhanVien = @Anh,
                            TrangThai = @TrangThai
                        WHERE MaNhanVien = @Ma"
                        : @"
                        INSERT INTO dbo.NhanVienVanPhong
                        (
                            MaNhanVien, MaKhachHang,
                            MaPhong, HoTen, NgaySinh,
                            GioiTinh, CCCD, ChucVu,
                            DienThoai, AnhNhanVien,
                            TrangThai
                        )
                        VALUES
                        (
                            @Ma, @MaKH, @MaPhong,
                            @HoTen, @NgaySinh, @GioiTinh,
                            @CCCD, @ChucVu, @DienThoai,
                            @Anh, @TrangThai
                        )";

                    using (SqlCommand cmd =
                        new SqlCommand(sql, conn, tran))
                    {
                        cmd.Parameters.AddWithValue(
                            "@Ma", ma.Trim());

                        cmd.Parameters.AddWithValue(
                            "@MaKH", maKH.Trim());

                        cmd.Parameters.AddWithValue(
                            "@MaPhong", NullNeuRong(maPhong));

                        cmd.Parameters.AddWithValue(
                            "@HoTen", hoTen.Trim());

                        cmd.Parameters.AddWithValue(
                            "@NgaySinh",
                            ngaySinh.HasValue
                                ? (object)ngaySinh.Value.Date
                                : DBNull.Value);

                        cmd.Parameters.AddWithValue(
                            "@GioiTinh", NullNeuRong(gioiTinh));

                        cmd.Parameters.AddWithValue(
                            "@CCCD", NullNeuRong(cccd));

                        cmd.Parameters.AddWithValue(
                            "@ChucVu", NullNeuRong(chucVu));

                        cmd.Parameters.AddWithValue(
                            "@DienThoai", NullNeuRong(dienThoai));

                        cmd.Parameters.AddWithValue(
                            "@Anh", NullNeuRong(anh));

                        cmd.Parameters.AddWithValue(
                            "@TrangThai", trangThai);

                        int n = cmd.ExecuteNonQuery();

                        if (sua && n == 0)
                            throw new Exception(
                                "Không tìm thấy nhân viên cần sửa.");
                    }

                    tran.Commit();
                }
                catch
                {
                    if (tran.Connection != null)
                        tran.Rollback();
                    throw;
                }
            }
        }

        public void Xoa(string ma)
        {
            if (string.IsNullOrWhiteSpace(ma))
                throw new Exception(
                    "Hãy chọn nhân viên cần xóa.");

            // Không xóa nhân viên đang có thẻ giữ xe.
            int soThe = Convert.ToInt32(Db.Scalar(@"
                SELECT COUNT(*)
                FROM dbo.TheGiuXe
                WHERE MaNhanVien = @Ma",
                Db.P("@Ma", ma.Trim())));

            if (soThe > 0)
                throw new Exception(
                    "Nhân viên đã được cấp thẻ giữ xe. " +
                    "Hãy xử lý thẻ trước khi xóa.");

            int n = Db.Execute(@"
                DELETE FROM dbo.NhanVienVanPhong
                WHERE MaNhanVien = @Ma",
                Db.P("@Ma", ma.Trim()));

            if (n == 0)
                throw new Exception(
                    "Không tìm thấy nhân viên cần xóa.");
        }
    }
}
