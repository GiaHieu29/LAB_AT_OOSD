
using System;
using System.Data;
using QuanLyChoThueVanPhong.Data;

namespace QuanLyChoThueVanPhong.Services
{
    public class ThongKeService
    {
        // =========================================
        // 1. KIỂM TRA KHOẢNG NGÀY
        // =========================================

        private void KiemTraNgay(DateTime tu, DateTime den)
        {
            if (tu.Date > den.Date)
                throw new Exception(
                    "Ngày bắt đầu không được sau ngày kết thúc.");

            if (tu.Year < 2000 || den.Year > 2100)
                throw new Exception(
                    "Khoảng thời gian thống kê không hợp lệ.");
        }

        // =========================================
        // 2. THỐNG KÊ PHÒNG TẠI NGÀY XÉT
        // =========================================

        // Ngày kết thúc thuê được tính bằng:
        // ngày kết thúc hợp đồng hoặc ngày gia hạn
        // đã duyệt, có xét ngày trả phòng nếu có.

        private const string SqlPhong = @"
            SELECT
                p.MaPhong,
                p.TenPhong,
                t.TenTang,
                p.DienTich,
                p.TrangThai AS TrangThaiDanhMuc,
                CASE
                    WHEN p.TrangThai IN
                        (N'Bảo trì', N'Ngừng sử dụng')
                    THEN p.TrangThai

                    WHEN EXISTS
                    (
                        SELECT 1
                        FROM dbo.ChiTietHopDong ct
                        JOIN dbo.HopDong h
                            ON h.MaHopDong = ct.MaHopDong
                        OUTER APPLY
                        (
                            SELECT MAX(gh.DenNgay) AS DenGiaHan
                            FROM dbo.PhieuGiaHan gh
                            WHERE gh.MaChiTietHD =
                                  ct.MaChiTietHD
                              AND gh.TrangThai = N'Đã duyệt'
                        ) gh
                        WHERE ct.MaPhong = p.MaPhong
                          AND h.TrangThai <> N'Dự thảo'
                          AND ct.NgayNhanPhong <= @Ngay
                          AND
                          (
                              CASE
                                  WHEN ct.NgayTraPhong IS NOT NULL
                                       AND ct.NgayTraPhong <
                                           h.NgayKetThuc
                                  THEN ct.NgayTraPhong

                                  WHEN gh.DenGiaHan >
                                       h.NgayKetThuc
                                  THEN gh.DenGiaHan

                                  ELSE h.NgayKetThuc
                              END
                          ) >= @Ngay
                    )
                    THEN N'Đang thuê'

                    ELSE N'Trống'
                END AS TinhTrangThongKe
            FROM dbo.Phong p
            JOIN dbo.Tang t
                ON t.MaTang = p.MaTang";

        public DataTable LayPhong(DateTime ngayXet)
        {
            return Db.Query(
                SqlPhong + @"
                ORDER BY t.SoThuTu, p.MaPhong",
                Db.P("@Ngay", ngayXet.Date));
        }

        // =========================================
        // 3. THỐNG KÊ HỢP ĐỒNG
        // =========================================

        public DataTable LayHopDong(
            DateTime tu, DateTime den)
        {
            KiemTraNgay(tu, den);

            return Db.Query(@"
                SELECT
                    h.MaHopDong,
                    k.TenKhachHang,
                    h.NgayKy,
                    h.NgayBatDau,
                    h.NgayKetThuc,
                    h.SoThangThue,
                    h.TrangThai,
                    ISNULL(x.SoPhong, 0) AS SoPhong,
                    ISNULL(x.TienThang, 0) AS TienThang
                FROM dbo.HopDong h
                JOIN dbo.KhachHang k
                    ON h.MaKhachHang = k.MaKhachHang
                OUTER APPLY
                (
                    SELECT COUNT(*) AS SoPhong,
                           SUM(DonGiaThue) AS TienThang
                    FROM dbo.ChiTietHopDong
                    WHERE MaHopDong = h.MaHopDong
                ) x
                WHERE h.NgayKy >= @Tu
                  AND h.NgayKy < @SauDen
                ORDER BY h.NgayKy DESC, h.MaHopDong",
                Db.P("@Tu", tu.Date),
                Db.P("@SauDen", den.Date.AddDays(1)));
        }

        // =========================================
        // 4. DOANH THU THỰC THU THEO THÁNG
        // =========================================

        public DataTable LayDoanhThu(
            DateTime tu, DateTime den)
        {
            KiemTraNgay(tu, den);

            return Db.Query(@"
                WITH Thu AS
                (
                    SELECT
                        tt.NgayThanhToan AS NgayThu,
                        tt.SoTien,
                        N'Tiền thuê' AS Loai
                    FROM dbo.ThanhToanTienThue tt
                    WHERE tt.NgayThanhToan >= @Tu
                      AND tt.NgayThanhToan < @SauDen

                    UNION ALL

                    SELECT
                        dv.NgayThanhToan AS NgayThu,
                        dv.SoTien,
                        N'Dịch vụ' AS Loai
                    FROM dbo.ThanhToanDichVu dv
                    WHERE dv.NgayThanhToan >= @Tu
                      AND dv.NgayThanhToan < @SauDen
                )
                SELECT
                    YEAR(NgayThu) AS Nam,
                    MONTH(NgayThu) AS Thang,
                    SUM(CASE WHEN Loai = N'Tiền thuê'
                        THEN SoTien ELSE 0 END)
                        AS ThuTienThue,
                    SUM(CASE WHEN Loai = N'Dịch vụ'
                        THEN SoTien ELSE 0 END)
                        AS ThuDichVu,
                    SUM(SoTien) AS TongDoanhThu
                FROM Thu
                GROUP BY YEAR(NgayThu), MONTH(NgayThu)
                ORDER BY Nam, Thang",
                Db.P("@Tu", tu.Date),
                Db.P("@SauDen", den.Date.AddDays(1)));
        }

        // =========================================
        // 5. CHI TIẾT CÁC KHOẢN THU
        // =========================================

        public DataTable LayChiTietDoanhThu(
            DateTime tu, DateTime den)
        {
            KiemTraNgay(tu, den);

            return Db.Query(@"
                SELECT
                    N'Tiền thuê' AS LoaiThu,
                    t.MaThanhToan AS MaPhieu,
                    ct.MaHopDong,
                    ct.MaPhong,
                    t.NgayThanhToan AS NgayThu,
                    t.SoTien,
                    t.PhuongThuc
                FROM dbo.ThanhToanTienThue t
                JOIN dbo.ChiTietHopDong ct
                    ON ct.MaChiTietHD = t.MaChiTietHD
                WHERE t.NgayThanhToan >= @Tu
                  AND t.NgayThanhToan < @SauDen

                UNION ALL

                SELECT
                    N'Dịch vụ',
                    d.MaThanhToanDV,
                    hd.MaHopDong,
                    CAST(NULL AS NVARCHAR(20)),
                    d.NgayThanhToan,
                    d.SoTien,
                    d.PhuongThuc
                FROM dbo.ThanhToanDichVu d
                JOIN dbo.HoaDonDichVu hd
                    ON hd.MaHoaDon = d.MaHoaDon
                WHERE d.NgayThanhToan >= @Tu
                  AND d.NgayThanhToan < @SauDen

                ORDER BY NgayThu DESC, MaPhieu",
                Db.P("@Tu", tu.Date),
                Db.P("@SauDen", den.Date.AddDays(1)));
        }

        // =========================================
        // 6. CÔNG NỢ TIỀN THUÊ
        // =========================================
        //
        // Thu 6 tháng đầu tại ngày bắt đầu.
        // Từ tháng thứ 7 thu từng tháng.
        // Gia hạn đã duyệt thu từng tháng.
        //
        // Tiền đã thu được tính đến ngày báo cáo.
        // Không tính phiếu Chờ duyệt.

        public DataTable LayNoTienThue(DateTime ngayXet)
        {
            return Db.Query(@"
                WITH So AS
                (
                    SELECT 0 AS N
                    UNION ALL
                    SELECT N + 1
                    FROM So
                    WHERE N < 599
                ),
                KyGoc AS
                (
                    SELECT
                        ct.MaChiTietHD,
                        CASE
                            WHEN s.N = 0
                            THEN DATEADD(DAY, 0, h.NgayBatDau)
                            ELSE DATEADD(
                                MONTH, s.N, h.NgayBatDau)
                        END AS HanThu,
                        CASE
                            WHEN s.N = 0
                            THEN ct.DonGiaThue * 6
                            ELSE ct.DonGiaThue
                        END AS SoTien
                    FROM dbo.ChiTietHopDong ct
                    JOIN dbo.HopDong h
                        ON h.MaHopDong = ct.MaHopDong
                    CROSS JOIN So s
                    WHERE h.TrangThai <> N'Dự thảo'
                      AND
                      (
                          s.N = 0
                          OR
                          (s.N >= 6
                           AND s.N < h.SoThangThue)
                      )
                ),
                KyGiaHan AS
                (
                    SELECT
                        gh.MaChiTietHD,
                        DATEADD(
                            MONTH, s.N, gh.TuNgay) AS HanThu,
                        gh.DonGiaGiaHan AS SoTien
                    FROM dbo.PhieuGiaHan gh
                    CROSS JOIN So s
                    WHERE gh.TrangThai = N'Đã duyệt'
                      AND s.N < gh.SoThangGiaHan
                ),
                PhaiThu AS
                (
                    SELECT MaChiTietHD, SUM(SoTien) AS Tien
                    FROM
                    (
                        SELECT MaChiTietHD, SoTien
                        FROM KyGoc
                        WHERE HanThu <= @Ngay

                        UNION ALL

                        SELECT MaChiTietHD, SoTien
                        FROM KyGiaHan
                        WHERE HanThu <= @Ngay
                    ) x
                    GROUP BY MaChiTietHD
                ),
                DaThu AS
                (
                    SELECT
                        MaChiTietHD,
                        SUM(SoTien) AS Tien
                    FROM dbo.ThanhToanTienThue
                    WHERE NgayThanhToan <= @Ngay
                    GROUP BY MaChiTietHD
                )
                SELECT
                    ct.MaHopDong,
                    ct.MaPhong,
                    kh.TenKhachHang,
                    ISNULL(pt.Tien, 0) AS PhaiThuDenHan,
                    ISNULL(dt.Tien, 0) AS DaThu,
                    CASE
                        WHEN ISNULL(pt.Tien, 0) >
                             ISNULL(dt.Tien, 0)
                        THEN ISNULL(pt.Tien, 0) -
                             ISNULL(dt.Tien, 0)
                        ELSE 0
                    END AS ConNo
                FROM dbo.ChiTietHopDong ct
                JOIN dbo.HopDong h
                    ON ct.MaHopDong = h.MaHopDong
                JOIN dbo.KhachHang kh
                    ON kh.MaKhachHang = h.MaKhachHang
                LEFT JOIN PhaiThu pt
                    ON pt.MaChiTietHD = ct.MaChiTietHD
                LEFT JOIN DaThu dt
                    ON dt.MaChiTietHD = ct.MaChiTietHD
                WHERE h.TrangThai <> N'Dự thảo'
                ORDER BY ConNo DESC,
                         ct.MaHopDong, ct.MaPhong
                OPTION (MAXRECURSION 0)",
                Db.P("@Ngay", ngayXet.Date));
        }

        // =========================================
        // 7. CÔNG NỢ DỊCH VỤ
        // =========================================

        public DataTable LayNoDichVu(DateTime ngayXet)
        {
            return Db.Query(@"
                SELECT
                    hd.MaHoaDon,
                    hd.MaHopDong,
                    hd.Thang,
                    hd.Nam,
                    hd.HanThanhToan,
                    hd.TongTien,
                    ISNULL(t.DaThu, 0) AS DaThu,
                    CASE
                        WHEN hd.TongTien > ISNULL(t.DaThu, 0)
                        THEN hd.TongTien - ISNULL(t.DaThu, 0)
                        ELSE 0
                    END AS ConNo
                FROM dbo.HoaDonDichVu hd
                OUTER APPLY
                (
                    SELECT SUM(SoTien) AS DaThu
                    FROM dbo.ThanhToanDichVu
                    WHERE MaHoaDon = hd.MaHoaDon
                      AND NgayThanhToan <= @Ngay
                ) t
                WHERE hd.HanThanhToan <= @Ngay
                ORDER BY ConNo DESC, hd.MaHoaDon",
                Db.P("@Ngay", ngayXet.Date));
        }

        // =========================================
        // 8. THỐNG KÊ VI PHẠM
        // =========================================

        public DataTable LayViPham(
            DateTime tu, DateTime den)
        {
            KiemTraNgay(tu, den);

            return Db.Query(@"
                SELECT
                    vp.MaViPham,
                    ct.MaHopDong,
                    ct.MaPhong,
                    vp.NgayPhatHien,
                    vp.NoiDung,
                    vp.SoTienPhat,
                    vp.TrangThai,
                    vp.NgayXuLy
                FROM dbo.ViPhamPhong vp
                JOIN dbo.ChiTietHopDong ct
                    ON ct.MaChiTietHD = vp.MaChiTietHD
                WHERE vp.NgayPhatHien >= @Tu
                  AND vp.NgayPhatHien < @SauDen
                ORDER BY vp.NgayPhatHien DESC",
                Db.P("@Tu", tu.Date),
                Db.P("@SauDen", den.Date.AddDays(1)));
        }

        // =========================================
        // 9. THỐNG KÊ SỬA CHỮA
        // =========================================

        public DataTable LaySuaChua(
            DateTime tu, DateTime den)
        {
            KiemTraNgay(tu, den);

            return Db.Query(@"
                SELECT
                    sc.MaSuaChua,
                    sc.MaPhong,
                    p.TenPhong,
                    sc.MaKiemTra,
                    sc.NgayBatDau,
                    sc.NgayHoanThanh,
                    sc.NoiDung,
                    sc.ChiPhi,
                    sc.TrangThai
                FROM dbo.SuaChuaPhong sc
                JOIN dbo.Phong p
                    ON p.MaPhong = sc.MaPhong
                WHERE sc.NgayBatDau >= @Tu
                  AND sc.NgayBatDau < @SauDen
                ORDER BY sc.NgayBatDau DESC",
                Db.P("@Tu", tu.Date),
                Db.P("@SauDen", den.Date.AddDays(1)));
        }
    }
}
