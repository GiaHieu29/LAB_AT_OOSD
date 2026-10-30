
using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyChoThueVanPhong.Data;

namespace QuanLyChoThueVanPhong.Services
{
    public class ViPhamSuaChuaService
    {
        private static SqlParameter P(string n, object v)
        {
            return new SqlParameter(n, v ?? DBNull.Value);
        }

        private static object NullRong(string s)
        {
            return string.IsNullOrWhiteSpace(s)
                ? (object)DBNull.Value : s.Trim();
        }

        private static object Scalar(
            SqlConnection c, SqlTransaction t,
            string sql, params SqlParameter[] ps)
        {
            using (var cmd = new SqlCommand(sql, c, t))
            {
                cmd.Parameters.AddRange(ps);
                return cmd.ExecuteScalar();
            }
        }

        private static int Execute(
            SqlConnection c, SqlTransaction t,
            string sql, params SqlParameter[] ps)
        {
            using (var cmd = new SqlCommand(sql, c, t))
            {
                cmd.Parameters.AddRange(ps);
                return cmd.ExecuteNonQuery();
            }
        }

        // ======================================
        // DANH MỤC CHỌN HỢP ĐỒNG / PHÒNG
        // ======================================

        public DataTable LayHopDong()
        {
            return Db.Query(@"
                SELECT h.MaHopDong,
                       h.MaHopDong + N' - ' +
                       k.TenKhachHang AS HienThi
                FROM dbo.HopDong h
                JOIN dbo.KhachHang k
                    ON h.MaKhachHang = k.MaKhachHang
                WHERE h.TrangThai <> N'Dự thảo'
                ORDER BY h.MaHopDong");
        }

        public DataTable LayPhongThue(string maHD)
        {
            return Db.Query(@"
                SELECT ct.MaChiTietHD,
                       ct.MaPhong + N' - ' +
                       p.TenPhong AS HienThi
                FROM dbo.ChiTietHopDong ct
                JOIN dbo.Phong p
                    ON p.MaPhong = ct.MaPhong
                WHERE ct.MaHopDong = @HD
                ORDER BY ct.MaPhong",
                P("@HD", maHD ?? ""));
        }

        public DataTable LayPhong()
        {
            return Db.Query(@"
                SELECT p.MaPhong,
                       p.MaPhong + N' - ' +
                       p.TenPhong + N' (' +
                       t.TenTang + N')' AS HienThi
                FROM dbo.Phong p
                JOIN dbo.Tang t ON t.MaTang = p.MaTang
                ORDER BY t.SoThuTu, p.MaPhong");
        }

        public DataTable LayPhieuKiemTra(string maPhong)
        {
            DataTable dt = Db.Query(@"
                SELECT MaKiemTra,
                       MaKiemTra + N' - ' +
                       CONVERT(NVARCHAR(10),
                           NgayKiemTra, 103) AS HienThi
                FROM dbo.KiemTraPhong
                WHERE MaPhong = @Phong
                ORDER BY NgayKiemTra DESC, MaKiemTra",
                P("@Phong", maPhong ?? ""));

            // Cho phép sửa chữa không có phiếu kiểm tra.
            DataRow row = dt.NewRow();
            row["MaKiemTra"] = "";
            row["HienThi"] = "-- Không liên kết phiếu --";
            dt.Rows.InsertAt(row, 0);
            return dt;
        }

        // ======================================
        // TAB 1: DANH SÁCH VI PHẠM
        // ======================================

        public DataTable LayViPham(string key = "")
        {
            return Db.Query(@"
                SELECT vp.MaViPham,
                       vp.MaChiTietHD,
                       ct.MaHopDong,
                       ct.MaPhong,
                       kh.TenKhachHang,
                       vp.NgayPhatHien,
                       vp.NoiDung,
                       vp.SoTienPhat,
                       vp.TrangThai,
                       vp.NgayXuLy,
                       vp.GhiChu
                FROM dbo.ViPhamPhong vp
                JOIN dbo.ChiTietHopDong ct
                    ON ct.MaChiTietHD = vp.MaChiTietHD
                JOIN dbo.HopDong h
                    ON h.MaHopDong = ct.MaHopDong
                JOIN dbo.KhachHang kh
                    ON kh.MaKhachHang = h.MaKhachHang
                WHERE vp.MaViPham LIKE @Key
                   OR ct.MaHopDong LIKE @Key
                   OR ct.MaPhong LIKE @Key
                   OR kh.TenKhachHang LIKE @Key
                   OR vp.TrangThai LIKE @Key
                   OR vp.NoiDung LIKE @Key
                ORDER BY vp.NgayPhatHien DESC,
                         vp.MaViPham",
                P("@Key", "%" + (key ?? "").Trim() + "%"));
        }

        private static void KiemTraViPham(
            string ma, string maCT, DateTime ngay,
            string noiDung, decimal tien, string trangThai,
            DateTime? ngayXuLy)
        {
            if (string.IsNullOrWhiteSpace(ma) ||
                ma.Trim().Length > 20)
                throw new Exception(
                    "Mã vi phạm phải có từ 1 đến 20 ký tự.");

            if (string.IsNullOrWhiteSpace(maCT))
                throw new Exception(
                    "Hãy chọn hợp đồng và phòng thuê.");

            if (ngay.Date > DateTime.Today)
                throw new Exception(
                    "Ngày phát hiện không được ở tương lai.");

            if (string.IsNullOrWhiteSpace(noiDung))
                throw new Exception(
                    "Hãy nhập nội dung vi phạm.");

            if (tien < 0)
                throw new Exception(
                    "Tiền phạt không được âm.");

            if (trangThai != "Chưa xử lý" &&
                trangThai != "Đã xử lý")
                throw new Exception(
                    "Trạng thái vi phạm không hợp lệ.");

            if (trangThai == "Đã xử lý" &&
                !ngayXuLy.HasValue)
                throw new Exception(
                    "Vi phạm đã xử lý phải có ngày xử lý.");

            if (trangThai == "Chưa xử lý" &&
                ngayXuLy.HasValue)
                throw new Exception(
                    "Vi phạm chưa xử lý không được " +
                    "có ngày xử lý.");

            if (ngayXuLy.HasValue &&
                (ngayXuLy.Value.Date < ngay.Date ||
                 ngayXuLy.Value.Date > DateTime.Today))
                throw new Exception(
                    "Ngày xử lý phải từ ngày phát hiện " +
                    "đến ngày hiện tại.");
        }

        public void LuuViPham(
            string ma, string maCT, DateTime ngay,
            string noiDung, decimal tien, string trangThai,
            DateTime? ngayXuLy, string ghiChu, bool sua)
        {
            KiemTraViPham(ma, maCT, ngay,
                noiDung, tien, trangThai, ngayXuLy);

            using (var c = Db.OpenConnection())
            using (var t = c.BeginTransaction(
                IsolationLevel.Serializable))
            {
                try
                {
                    // Kiểm tra phòng thuộc hợp đồng,
                    // và ngày phát hiện nằm trong thời gian thuê.
                    int hopLe = Convert.ToInt32(
                        Scalar(c, t, @"
                        SELECT COUNT(*)
                        FROM dbo.ChiTietHopDong ct
                        JOIN dbo.HopDong h
                            ON h.MaHopDong = ct.MaHopDong
                        OUTER APPLY
                        (
                            SELECT MAX(g.DenNgay) AS DenGiaHan
                            FROM dbo.PhieuGiaHan g
                            WHERE g.MaChiTietHD = ct.MaChiTietHD
                              AND g.TrangThai = N'Đã duyệt'
                        ) gh
                        WHERE ct.MaChiTietHD = @CT
                          AND h.TrangThai <> N'Dự thảo'
                          AND ct.NgayNhanPhong <= @Ngay
                          AND
                          (
                              CASE
                                  WHEN ct.NgayTraPhong IS NOT NULL
                                  THEN ct.NgayTraPhong
                                  WHEN gh.DenGiaHan > h.NgayKetThuc
                                  THEN gh.DenGiaHan
                                  ELSE h.NgayKetThuc
                              END
                          ) >= @Ngay",
                        P("@CT", maCT),
                        P("@Ngay", ngay.Date)));

                    if (hopLe == 0)
                        throw new Exception(
                            "Ngày phát hiện không nằm trong " +
                            "thời gian thuê của phòng này.");

                    string sql = sua ? @"
                        UPDATE dbo.ViPhamPhong
                        SET MaChiTietHD = @CT,
                            NgayPhatHien = @Ngay,
                            NoiDung = @NoiDung,
                            SoTienPhat = @Tien,
                            TrangThai = @TT,
                            NgayXuLy = @NgayXuLy,
                            GhiChu = @GhiChu
                        WHERE MaViPham = @Ma"
                        : @"
                        INSERT INTO dbo.ViPhamPhong
                        (
                            MaViPham, MaChiTietHD,
                            NgayPhatHien, NoiDung,
                            SoTienPhat, TrangThai,
                            NgayXuLy, GhiChu
                        )
                        VALUES
                        (
                            @Ma, @CT, @Ngay,
                            @NoiDung, @Tien,
                            @TT, @NgayXuLy, @GhiChu
                        )";

                    int n = Execute(c, t, sql,
                        P("@Ma", ma.Trim()),
                        P("@CT", maCT.Trim()),
                        P("@Ngay", ngay.Date),
                        P("@NoiDung", noiDung.Trim()),
                        P("@Tien", tien),
                        P("@TT", trangThai),
                        P("@NgayXuLy", ngayXuLy.HasValue
                            ? (object)ngayXuLy.Value.Date
                            : DBNull.Value),
                        P("@GhiChu", NullRong(ghiChu)));

                    if (sua && n == 0)
                        throw new Exception(
                            "Không tìm thấy vi phạm cần sửa.");

                    t.Commit();
                }
                catch
                {
                    if (t.Connection != null) t.Rollback();
                    throw;
                }
            }
        }

        public void XoaViPham(string ma)
        {
            if (string.IsNullOrWhiteSpace(ma))
                throw new Exception(
                    "Hãy chọn vi phạm cần xóa.");

            int n = Db.Execute(@"
                DELETE FROM dbo.ViPhamPhong
                WHERE MaViPham = @Ma",
                P("@Ma", ma.Trim()));

            if (n == 0)
                throw new Exception(
                    "Không tìm thấy vi phạm.");
        }

        // ======================================
        // TAB 2: DANH SÁCH SỬA CHỮA
        // ======================================

        public DataTable LaySuaChua(string key = "")
        {
            return Db.Query(@"
                SELECT sc.MaSuaChua,
                       sc.MaPhong,
                       p.TenPhong,
                       t.TenTang,
                       sc.MaKiemTra,
                       sc.NgayBatDau,
                       sc.NgayHoanThanh,
                       sc.NoiDung,
                       sc.ChiPhi,
                       sc.TrangThai
                FROM dbo.SuaChuaPhong sc
                JOIN dbo.Phong p
                    ON p.MaPhong = sc.MaPhong
                JOIN dbo.Tang t
                    ON t.MaTang = p.MaTang
                WHERE sc.MaSuaChua LIKE @Key
                   OR sc.MaPhong LIKE @Key
                   OR p.TenPhong LIKE @Key
                   OR sc.MaKiemTra LIKE @Key
                   OR sc.NoiDung LIKE @Key
                   OR sc.TrangThai LIKE @Key
                ORDER BY sc.NgayBatDau DESC,
                         sc.MaSuaChua",
                P("@Key", "%" + (key ?? "").Trim() + "%"));
        }

        private static void KiemTraSuaChua(
            string ma, string phong, string maKT,
            DateTime batDau, DateTime? hoanThanh,
            string noiDung, decimal chiPhi, string trangThai)
        {
            if (string.IsNullOrWhiteSpace(ma) ||
                ma.Trim().Length > 20)
                throw new Exception(
                    "Mã sửa chữa phải từ 1 đến 20 ký tự.");

            if (string.IsNullOrWhiteSpace(phong))
                throw new Exception("Hãy chọn phòng.");

            if (string.IsNullOrWhiteSpace(noiDung))
                throw new Exception(
                    "Hãy nhập nội dung sửa chữa.");

            if (chiPhi < 0)
                throw new Exception(
                    "Chi phí sửa chữa không được âm.");

            if (trangThai != "Chờ xử lý" &&
                trangThai != "Đang sửa" &&
                trangThai != "Hoàn thành")
                throw new Exception(
                    "Trạng thái sửa chữa không hợp lệ.");

            if (trangThai == "Hoàn thành" &&
                !hoanThanh.HasValue)
                throw new Exception(
                    "Cần nhập ngày hoàn thành.");

            if (trangThai != "Hoàn thành" &&
                hoanThanh.HasValue)
                throw new Exception(
                    "Chỉ nhập ngày hoàn thành khi " +
                    "trạng thái là Hoàn thành.");

            if (hoanThanh.HasValue &&
                (hoanThanh.Value.Date < batDau.Date ||
                 hoanThanh.Value.Date > DateTime.Today))
                throw new Exception(
                    "Ngày hoàn thành phải từ ngày bắt đầu " +
                    "đến ngày hiện tại.");

            if (batDau.Date > DateTime.Today)
                throw new Exception(
                    "Ngày bắt đầu không được ở tương lai.");
        }

        public void LuuSuaChua(
            string ma, string phong, string maKT,
            DateTime batDau, DateTime? hoanThanh,
            string noiDung, decimal chiPhi,
            string trangThai, bool sua)
        {
            KiemTraSuaChua(ma, phong, maKT,
                batDau, hoanThanh, noiDung,
                chiPhi, trangThai);

            using (var c = Db.OpenConnection())
            using (var t = c.BeginTransaction(
                IsolationLevel.Serializable))
            {
                try
                {
                    int coPhong = Convert.ToInt32(
                        Scalar(c, t, @"
                        SELECT COUNT(*)
                        FROM dbo.Phong
                        WHERE MaPhong = @Phong",
                        P("@Phong", phong)));

                    if (coPhong == 0)
                        throw new Exception(
                            "Phòng sửa chữa không tồn tại.");

                    if (!string.IsNullOrWhiteSpace(maKT))
                    {
                        int dungPhong = Convert.ToInt32(
                            Scalar(c, t, @"
                            SELECT COUNT(*)
                            FROM dbo.KiemTraPhong
                            WHERE MaKiemTra = @KT
                              AND MaPhong = @Phong
                              AND NgayKiemTra <= @Ngay",
                            P("@KT", maKT),
                            P("@Phong", phong),
                            P("@Ngay", batDau.Date)));

                        if (dungPhong == 0)
                            throw new Exception(
                                "Phiếu kiểm tra không thuộc " +
                                "phòng này hoặc có ngày kiểm tra " +
                                "sau ngày bắt đầu sửa chữa.");
                    }

                    string sql = sua ? @"
                        UPDATE dbo.SuaChuaPhong
                        SET MaPhong = @Phong,
                            MaKiemTra = @KT,
                            NgayBatDau = @BatDau,
                            NgayHoanThanh = @HoanThanh,
                            NoiDung = @NoiDung,
                            ChiPhi = @ChiPhi,
                            TrangThai = @TT
                        WHERE MaSuaChua = @Ma"
                        : @"
                        INSERT INTO dbo.SuaChuaPhong
                        (
                            MaSuaChua, MaPhong,
                            MaKiemTra, NgayBatDau,
                            NgayHoanThanh, NoiDung,
                            ChiPhi, TrangThai
                        )
                        VALUES
                        (
                            @Ma, @Phong, @KT,
                            @BatDau, @HoanThanh,
                            @NoiDung, @ChiPhi, @TT
                        )";

                    int n = Execute(c, t, sql,
                        P("@Ma", ma.Trim()),
                        P("@Phong", phong.Trim()),
                        P("@KT", NullRong(maKT)),
                        P("@BatDau", batDau.Date),
                        P("@HoanThanh", hoanThanh.HasValue
                            ? (object)hoanThanh.Value.Date
                            : DBNull.Value),
                        P("@NoiDung", noiDung.Trim()),
                        P("@ChiPhi", chiPhi),
                        P("@TT", trangThai));

                    if (sua && n == 0)
                        throw new Exception(
                            "Không tìm thấy sửa chữa cần sửa.");

                    t.Commit();
                }
                catch
                {
                    if (t.Connection != null) t.Rollback();
                    throw;
                }
            }
        }

        public void XoaSuaChua(string ma)
        {
            if (string.IsNullOrWhiteSpace(ma))
                throw new Exception(
                    "Hãy chọn sửa chữa cần xóa.");

            int n = Db.Execute(@"
                DELETE FROM dbo.SuaChuaPhong
                WHERE MaSuaChua = @Ma",
                P("@Ma", ma.Trim()));

            if (n == 0)
                throw new Exception(
                    "Không tìm thấy công việc sửa chữa.");
        }
    }
}
