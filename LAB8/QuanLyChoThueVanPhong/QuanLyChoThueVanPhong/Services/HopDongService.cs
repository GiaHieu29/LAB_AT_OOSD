
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using QuanLyChoThueVanPhong.Data;

namespace QuanLyChoThueVanPhong.Services
{
    // Thông tin một phòng được chọn trong hợp đồng.
    public class PhongThueItem
    {
        public string MaPhong { get; set; }
        public decimal DonGiaThue { get; set; }
    }

    public class HopDongService
    {
        // ============================================
        // 1. DANH SÁCH HỢP ĐỒNG
        // ============================================

        public DataTable LayDanhSach(string key = "")
        {
            return Db.Query(@"
                SELECT
                    h.MaHopDong,
                    h.MaKhachHang,
                    k.TenKhachHang,
                    h.NgayKy,
                    h.NgayBatDau,
                    h.NgayKetThuc,
                    h.SoThangThue,
                    h.TrangThai,
                    h.NoiDung,
                    ISNULL(x.SoPhong, 0) AS SoPhong,
                    ISNULL(x.TongTienThang, 0)
                        AS TongTienThang
                FROM dbo.HopDong h
                INNER JOIN dbo.KhachHang k
                    ON h.MaKhachHang = k.MaKhachHang
                OUTER APPLY
                (
                    SELECT
                        COUNT(*) AS SoPhong,
                        SUM(ct.DonGiaThue) AS TongTienThang
                    FROM dbo.ChiTietHopDong ct
                    WHERE ct.MaHopDong = h.MaHopDong
                ) x
                WHERE h.MaHopDong LIKE @Key
                   OR h.MaKhachHang LIKE @Key
                   OR k.TenKhachHang LIKE @Key
                   OR h.TrangThai LIKE @Key
                ORDER BY h.NgayKy DESC, h.MaHopDong",
                Db.P("@Key",
                    "%" + (key ?? "").Trim() + "%"));
        }

        // ============================================
        // 2. DANH SÁCH KHÁCH HÀNG
        // ============================================

        public DataTable LayKhachHang()
        {
            return Db.Query(@"
                SELECT
                    MaKhachHang,
                    TenKhachHang
                FROM dbo.KhachHang
                ORDER BY TenKhachHang");
        }

        // ============================================
        // 3. CHI TIẾT PHÒNG TRONG HỢP ĐỒNG
        // ============================================

        public DataTable LayChiTiet(string maHD)
        {
            return Db.Query(@"
                SELECT
                    ct.MaChiTietHD,
                    ct.MaPhong,
                    p.TenPhong,
                    t.TenTang,
                    p.DienTich,
                    ct.DonGiaThue
                FROM dbo.ChiTietHopDong ct
                INNER JOIN dbo.Phong p
                    ON ct.MaPhong = p.MaPhong
                INNER JOIN dbo.Tang t
                    ON p.MaTang = t.MaTang
                WHERE ct.MaHopDong = @MaHD
                ORDER BY t.SoThuTu, ct.MaPhong",
                Db.P("@MaHD", maHD));
        }

        // ============================================
        // 4. ĐIỀU KIỆN KIỂM TRA TRÙNG LỊCH THUÊ
        // ============================================
        //
        // Áp dụng cho một phòng trong thời gian
        // từ @BatDau đến @KetThuc, bao gồm cả 2 ngày.
        //
        // - Bỏ qua hợp đồng đang sửa (@MaHD).
        // - Bỏ qua hợp đồng Dự thảo.
        // - Xét thời hạn thuê gốc.
        // - Xét phiếu gia hạn đã được duyệt.
        // - Xét ngày trả phòng thực tế nếu có.
        //
        // Dùng OUTER APPLY để lấy ngày gia hạn lớn nhất.

        private const string SqlTrungLich = @"
            SELECT COUNT(*)
            FROM dbo.ChiTietHopDong ct
            INNER JOIN dbo.HopDong hd
                ON ct.MaHopDong = hd.MaHopDong

            OUTER APPLY
            (
                SELECT MAX(gh.DenNgay) AS NgayGiaHan
                FROM dbo.PhieuGiaHan gh
                WHERE gh.MaChiTietHD = ct.MaChiTietHD
                  AND gh.TrangThai = N'Đã duyệt'
            ) gh

            WHERE ct.MaPhong = @MaPhong
              AND hd.MaHopDong <> @MaHD
              AND hd.TrangThai <> N'Dự thảo'

              AND ct.NgayNhanPhong <= @KetThuc

              AND
              (
                  CASE
                      WHEN ct.NgayTraPhong IS NOT NULL
                      THEN ct.NgayTraPhong

                      WHEN gh.NgayGiaHan IS NOT NULL
                           AND gh.NgayGiaHan >
                               hd.NgayKetThuc
                      THEN gh.NgayGiaHan

                      ELSE hd.NgayKetThuc
                  END
              ) >= @BatDau";

        // ============================================
        // 5. LẤY PHÒNG KHẢ DỤNG
        // ============================================

        public DataTable LayPhongKhaDung(
            DateTime batDau,
            DateTime ketThuc,
            string maHDHienTai = "")
        {
            if (ketThuc.Date < batDau.Date)
                throw new Exception(
                    "Ngày kết thúc không được trước ngày bắt đầu.");

            // Dùng cùng quy tắc kiểm tra với khi lưu.
            // Dự thảo không giữ chỗ.

            string sql = @"
                SELECT
                    p.MaPhong,
                    p.TenPhong,
                    t.TenTang,
                    p.DienTich,
                    p.DonGiaThang
                FROM dbo.Phong p
                INNER JOIN dbo.Tang t
                    ON p.MaTang = t.MaTang
                WHERE p.TrangThai NOT IN
                    (N'Bảo trì', N'Ngừng sử dụng')

                  AND NOT EXISTS
                  (
                      SELECT 1
                      FROM dbo.ChiTietHopDong ct
                      INNER JOIN dbo.HopDong hd
                          ON ct.MaHopDong = hd.MaHopDong
                      OUTER APPLY
                      (
                          SELECT
                              MAX(gh.DenNgay) AS NgayGiaHan
                          FROM dbo.PhieuGiaHan gh
                          WHERE gh.MaChiTietHD =
                                ct.MaChiTietHD
                            AND gh.TrangThai = N'Đã duyệt'
                      ) gh
                      WHERE ct.MaPhong = p.MaPhong
                        AND hd.MaHopDong <> @MaHD
                        AND hd.TrangThai <> N'Dự thảo'
                        AND ct.NgayNhanPhong <= @KetThuc
                        AND
                        (
                            CASE
                                WHEN ct.NgayTraPhong IS NOT NULL
                                THEN ct.NgayTraPhong

                                WHEN gh.NgayGiaHan IS NOT NULL
                                     AND gh.NgayGiaHan >
                                         hd.NgayKetThuc
                                THEN gh.NgayGiaHan

                                ELSE hd.NgayKetThuc
                            END
                        ) >= @BatDau
                  )
                ORDER BY t.SoThuTu, p.MaPhong";

            return Db.Query(
                sql,
                Db.P("@MaHD", maHDHienTai ?? ""),
                Db.P("@BatDau", batDau.Date),
                Db.P("@KetThuc", ketThuc.Date));
        }

        // ============================================
        // 6. TÍNH NGÀY KẾT THÚC HỢP ĐỒNG
        // ============================================

        public DateTime TinhNgayKetThuc(
            DateTime ngayBatDau,
            int soThang)
        {
            if (soThang < 6)
                throw new Exception(
                    "Thời gian thuê tối thiểu là 6 tháng.");

            if (soThang > 600)
                throw new Exception(
                    "Thời gian thuê không được vượt quá 600 tháng.");

            try
            {
                return ngayBatDau.Date
                    .AddMonths(soThang)
                    .AddDays(-1);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception(
                    "Thời gian thuê vượt quá phạm vi ngày hợp lệ.");
            }
        }

        // ============================================
        // 7. LƯU HỢP ĐỒNG - THÊM HOẶC SỬA
        // ============================================

        public void Luu(
            string maHD,
            string maKH,
            DateTime ngayKy,
            DateTime batDau,
            int soThang,
            string noiDung,
            string trangThai,
            List<PhongThueItem> dsPhong,
            bool capNhat)
        {
            KiemTraDuLieu(
                maHD, maKH, ngayKy, batDau,
                soThang, trangThai, dsPhong);

            maHD = maHD.Trim();
            maKH = maKH.Trim();

            DateTime ketThuc =
                TinhNgayKetThuc(batDau, soThang);

            using (SqlConnection conn =
                Db.OpenConnection())
            using (SqlTransaction tran =
                conn.BeginTransaction(
                    IsolationLevel.Serializable))
            {
                try
                {
                    // --------------------------------
                    // 7.1. KHÓA CÁC PHÒNG ĐƯỢC CHỌN
                    // --------------------------------

                    foreach (PhongThueItem phong in
                        dsPhong.OrderBy(
                            x => x.MaPhong,
                            StringComparer.Ordinal))
                    {
                        object value = Scalar(
                            conn, tran, @"
                            SELECT TrangThai
                            FROM dbo.Phong
                                WITH (UPDLOCK, HOLDLOCK)
                            WHERE MaPhong = @MaPhong",
                            P("@MaPhong",
                                phong.MaPhong.Trim()));

                        if (value == null)
                        {
                            throw new Exception(
                                "Không tồn tại phòng " +
                                phong.MaPhong);
                        }

                        string tinhTrang =
                            Convert.ToString(value);

                        if (tinhTrang == "Bảo trì" ||
                            tinhTrang == "Ngừng sử dụng")
                        {
                            throw new Exception(
                                "Phòng " + phong.MaPhong +
                                " đang bảo trì hoặc ngừng sử dụng.");
                        }
                    }

                    // --------------------------------
                    // 7.2. KIỂM TRA KHÁCH HÀNG
                    // --------------------------------

                    int soKhach = Convert.ToInt32(
                        Scalar(conn, tran, @"
                            SELECT COUNT(*)
                            FROM dbo.KhachHang
                            WHERE MaKhachHang = @MaKH",
                            P("@MaKH", maKH)));

                    if (soKhach == 0)
                    {
                        throw new Exception(
                            "Khách hàng không tồn tại.");
                    }

                    // --------------------------------
                    // 7.3. KIỂM TRA MÃ HỢP ĐỒNG
                    // --------------------------------

                    int soHopDong = Convert.ToInt32(
                        Scalar(conn, tran, @"
                            SELECT COUNT(*)
                            FROM dbo.HopDong
                                WITH (UPDLOCK, HOLDLOCK)
                            WHERE MaHopDong = @MaHD",
                            P("@MaHD", maHD)));

                    if (capNhat && soHopDong == 0)
                    {
                        throw new Exception(
                            "Không tìm thấy hợp đồng cần sửa.");
                    }

                    if (!capNhat && soHopDong > 0)
                    {
                        throw new Exception(
                            "Mã hợp đồng đã tồn tại.");
                    }

                    // --------------------------------
                    // 7.4. KIỂM TRA QUYỀN SỬA
                    // --------------------------------

                    if (capNhat)
                    {
                        KiemTraDuocSuaXoa(
                            conn, tran, maHD);
                    }

                    // --------------------------------
                    // 7.5. KIỂM TRA TRÙNG LỊCH
                    // --------------------------------

                    // Chỉ hợp đồng khác Dự thảo
                    // mới chiếm dụng phòng.

                    if (trangThai != "Dự thảo")
                    {
                        foreach (PhongThueItem phong in dsPhong)
                        {
                            int soTrung = Convert.ToInt32(
                                Scalar(
                                    conn,
                                    tran,
                                    SqlTrungLich,
                                    P("@MaPhong",
                                        phong.MaPhong.Trim()),
                                    P("@MaHD", maHD),
                                    P("@BatDau",
                                        batDau.Date),
                                    P("@KetThuc",
                                        ketThuc.Date)));

                            if (soTrung > 0)
                            {
                                throw new Exception(
                                    "Phòng " +
                                    phong.MaPhong +
                                    " đã được thuê trong " +
                                    "khoảng thời gian này.\n" +
                                    "Hãy chọn phòng hoặc " +
                                    "thời gian khác.");
                            }
                        }
                    }

                    // --------------------------------
                    // 7.6. THÊM / CẬP NHẬT HỢP ĐỒNG
                    // --------------------------------

                    if (capNhat)
                    {
                        Execute(conn, tran, @"
                            UPDATE dbo.HopDong
                            SET
                                MaKhachHang = @MaKH,
                                NgayKy = @NgayKy,
                                NgayBatDau = @BatDau,
                                NgayKetThuc = @KetThuc,
                                SoThangThue = @SoThang,
                                NoiDung = @NoiDung,
                                TrangThai = @TrangThai
                            WHERE MaHopDong = @MaHD",
                            P("@MaHD", maHD),
                            P("@MaKH", maKH),
                            P("@NgayKy",
                                ngayKy.Date),
                            P("@BatDau",
                                batDau.Date),
                            P("@KetThuc", ketThuc),
                            P("@SoThang", soThang),
                            P("@NoiDung",
                                NullNeuRong(noiDung)),
                            P("@TrangThai", trangThai));

                        // Chỉ xóa và tạo lại chi tiết
                        // khi không có nghiệp vụ phụ thuộc.
                        Execute(conn, tran, @"
                            DELETE FROM dbo.ChiTietHopDong
                            WHERE MaHopDong = @MaHD",
                            P("@MaHD", maHD));
                    }
                    else
                    {
                        Execute(conn, tran, @"
                            INSERT INTO dbo.HopDong
                            (
                                MaHopDong,
                                MaKhachHang,
                                NgayKy,
                                NgayBatDau,
                                NgayKetThuc,
                                SoThangThue,
                                NoiDung,
                                TrangThai
                            )
                            VALUES
                            (
                                @MaHD,
                                @MaKH,
                                @NgayKy,
                                @BatDau,
                                @KetThuc,
                                @SoThang,
                                @NoiDung,
                                @TrangThai
                            )",
                            P("@MaHD", maHD),
                            P("@MaKH", maKH),
                            P("@NgayKy",
                                ngayKy.Date),
                            P("@BatDau",
                                batDau.Date),
                            P("@KetThuc", ketThuc),
                            P("@SoThang", soThang),
                            P("@NoiDung",
                                NullNeuRong(noiDung)),
                            P("@TrangThai", trangThai));
                    }

                    // --------------------------------
                    // 7.7. THÊM CHI TIẾT PHÒNG THUÊ
                    // --------------------------------

                    foreach (PhongThueItem phong in dsPhong)
                    {
                        // CT + 16 ký tự ngẫu nhiên
                        // Tổng 18 ký tự, hợp VARCHAR(20).
                        string maChiTiet = "CT" +
                            Guid.NewGuid()
                                .ToString("N")
                                .Substring(0, 16)
                                .ToUpperInvariant();

                        Execute(conn, tran, @"
                            INSERT INTO dbo.ChiTietHopDong
                            (
                                MaChiTietHD,
                                MaHopDong,
                                MaPhong,
                                DonGiaThue,
                                NgayNhanPhong,
                                NgayTraPhong
                            )
                            VALUES
                            (
                                @MaCT,
                                @MaHD,
                                @MaPhong,
                                @Gia,
                                @NgayNhan,
                                NULL
                            )",
                            P("@MaCT", maChiTiet),
                            P("@MaHD", maHD),
                            P("@MaPhong",
                                phong.MaPhong.Trim()),
                            P("@Gia",
                                phong.DonGiaThue),
                            P("@NgayNhan",
                                batDau.Date));
                    }

                    // --------------------------------
                    // 7.8. HOÀN TẤT
                    // --------------------------------

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

        // ============================================
        // 8. KIỂM TRA DỮ LIỆU ĐẦU VÀO
        // ============================================

        private void KiemTraDuLieu(
            string maHD,
            string maKH,
            DateTime ngayKy,
            DateTime batDau,
            int soThang,
            string trangThai,
            List<PhongThueItem> dsPhong)
        {
            if (string.IsNullOrWhiteSpace(maHD) ||
                maHD.Trim().Length > 20)
            {
                throw new Exception(
                    "Mã hợp đồng phải có từ 1 đến 20 ký tự.");
            }

            if (string.IsNullOrWhiteSpace(maKH) ||
                maKH.Trim().Length > 20)
            {
                throw new Exception(
                    "Hãy chọn khách hàng hợp lệ.");
            }

            if (ngayKy.Date > batDau.Date)
            {
                throw new Exception(
                    "Ngày ký không được sau ngày bắt đầu thuê.");
            }

            if (soThang < 6)
            {
                throw new Exception(
                    "Thời gian thuê tối thiểu là 6 tháng.");
            }

            if (trangThai != "Dự thảo" &&
                trangThai != "Có hiệu lực" &&
                trangThai != "Hết hạn" &&
                trangThai != "Đã chấm dứt")
            {
                throw new Exception(
                    "Trạng thái hợp đồng không hợp lệ.");
            }

            if (dsPhong == null || dsPhong.Count == 0)
            {
                throw new Exception(
                    "Hợp đồng phải có ít nhất một phòng thuê.");
            }

            foreach (PhongThueItem phong in dsPhong)
            {
                if (phong == null ||
                    string.IsNullOrWhiteSpace(phong.MaPhong) ||
                    phong.MaPhong.Trim().Length > 20)
                {
                    throw new Exception(
                        "Mã phòng thuê không hợp lệ.");
                }

                if (phong.DonGiaThue < 0)
                {
                    throw new Exception(
                        "Giá thuê phòng không được âm.");
                }
            }

            bool coPhongTrung = dsPhong
                .GroupBy(
                    p => p.MaPhong.Trim(),
                    StringComparer.OrdinalIgnoreCase)
                .Any(g => g.Count() > 1);

            if (coPhongTrung)
            {
                throw new Exception(
                    "Một phòng không thể xuất hiện " +
                    "nhiều lần trong cùng hợp đồng.");
            }
        }

        // ============================================
        // 9. KIỂM TRA HỢP ĐỒNG ĐƯỢC SỬA / XÓA
        // ============================================

        private void KiemTraDuocSuaXoa(
            SqlConnection conn,
            SqlTransaction tran,
            string maHD)
        {
            int phatSinh = Convert.ToInt32(
                Scalar(conn, tran, @"
                    SELECT
                    (
                        SELECT COUNT(*)
                        FROM dbo.ThanhToanTienThue tt
                        INNER JOIN dbo.ChiTietHopDong ct
                            ON tt.MaChiTietHD = ct.MaChiTietHD
                        WHERE ct.MaHopDong = @MaHD
                    )
                    +
                    (
                        SELECT COUNT(*)
                        FROM dbo.PhieuGiaHan gh
                        INNER JOIN dbo.ChiTietHopDong ct
                            ON gh.MaChiTietHD = ct.MaChiTietHD
                        WHERE ct.MaHopDong = @MaHD
                    )
                    +
                    (
                        SELECT COUNT(*)
                        FROM dbo.SuDungDichVu sd
                        INNER JOIN dbo.ChiTietHopDong ct
                            ON sd.MaChiTietHD = ct.MaChiTietHD
                        WHERE ct.MaHopDong = @MaHD
                    )
                    +
                    (
                        SELECT COUNT(*)
                        FROM dbo.HoaDonDichVu
                        WHERE MaHopDong = @MaHD
                    )
                    +
                    (
                        SELECT COUNT(*)
                        FROM dbo.ViPhamPhong vp
                        INNER JOIN dbo.ChiTietHopDong ct
                            ON vp.MaChiTietHD = ct.MaChiTietHD
                        WHERE ct.MaHopDong = @MaHD
                    )",
                    P("@MaHD", maHD)));

            if (phatSinh > 0)
            {
                throw new Exception(
                    "Hợp đồng đã phát sinh thanh toán, " +
                    "gia hạn, dịch vụ, hóa đơn hoặc vi phạm.\n\n" +
                    "Không được sửa hoặc xóa hợp đồng này " +
                    "để bảo toàn dữ liệu.");
            }
        }

        // ============================================
        // 10. XÓA HỢP ĐỒNG
        // ============================================

        public void Xoa(string maHD)
        {
            if (string.IsNullOrWhiteSpace(maHD))
            {
                throw new Exception(
                    "Hãy chọn hợp đồng cần xóa.");
            }

            maHD = maHD.Trim();

            using (SqlConnection conn =
                Db.OpenConnection())
            using (SqlTransaction tran =
                conn.BeginTransaction(
                    IsolationLevel.Serializable))
            {
                try
                {
                    int tonTai = Convert.ToInt32(
                        Scalar(conn, tran, @"
                            SELECT COUNT(*)
                            FROM dbo.HopDong
                                WITH (UPDLOCK, HOLDLOCK)
                            WHERE MaHopDong = @MaHD",
                            P("@MaHD", maHD)));

                    if (tonTai == 0)
                    {
                        throw new Exception(
                            "Không tìm thấy hợp đồng cần xóa.");
                    }

                    KiemTraDuocSuaXoa(
                        conn, tran, maHD);

                    // Xóa bảng con trước.
                    Execute(conn, tran, @"
                        DELETE FROM dbo.ChiTietHopDong
                        WHERE MaHopDong = @MaHD",
                        P("@MaHD", maHD));

                    // Xóa bảng cha sau.
                    int soDong = Execute(
                        conn, tran, @"
                        DELETE FROM dbo.HopDong
                        WHERE MaHopDong = @MaHD",
                        P("@MaHD", maHD));

                    if (soDong == 0)
                    {
                        throw new Exception(
                            "Không tìm thấy hợp đồng.");
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

        // ============================================
        // 11. HÀM SQL DÙNG CHUNG
        // ============================================

        private static SqlParameter P(
            string name, object value)
        {
            return new SqlParameter(
                name,
                value ?? DBNull.Value);
        }

        private static object NullNeuRong(
            string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return DBNull.Value;

            return text.Trim();
        }

        private static object Scalar(
            SqlConnection conn,
            SqlTransaction tran,
            string sql,
            params SqlParameter[] parameters)
        {
            using (SqlCommand cmd =
                new SqlCommand(sql, conn, tran))
            {
                cmd.CommandTimeout = 30;

                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                return cmd.ExecuteScalar();
            }
        }

        private static int Execute(
            SqlConnection conn,
            SqlTransaction tran,
            string sql,
            params SqlParameter[] parameters)
        {
            using (SqlCommand cmd =
                new SqlCommand(sql, conn, tran))
            {
                cmd.CommandTimeout = 30;

                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                return cmd.ExecuteNonQuery();
            }
        }
    }
}
