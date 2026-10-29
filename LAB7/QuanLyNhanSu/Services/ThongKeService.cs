using System;
using System.Data;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.Services
{
    public class ThongKeService
    {
        public DataTable NhanSuTheoPhongBan()
        {
            return Db.Query(@"SELECT p.MaPB, p.TenPB, t.HoTen AS TruongPhong, COUNT(n.MaNV) AS TongSo,
                                     SUM(CASE WHEN n.TrangThai = 'THUVIEC' THEN 1 ELSE 0 END) AS ThuViec,
                                     SUM(CASE WHEN n.TrangThai = 'CHINHTHUC' THEN 1 ELSE 0 END) AS ChinhThuc,
                                     SUM(CASE WHEN n.TrangThai = 'NGHIVIEC' THEN 1 ELSE 0 END) AS NghiViec,
                                     p.NhuCauTuyen
                              FROM PhongBan p LEFT JOIN NhanVien n ON n.MaPB = p.MaPB LEFT JOIN NhanVien t ON t.MaNV = p.MaTruongPhong
                              GROUP BY p.MaPB, p.TenPB, t.HoTen, p.NhuCauTuyen ORDER BY p.MaPB");
        }

        /// <summary>Thử việc và hợp đồng ngắn hạn sắp hết hạn (hoặc đã hết hạn mà chưa ký tiếp).</summary>
        public DataTable SapHetHan(int soNgay)
        {
            return Db.Query(@"SELECT N'Thử việc' AS Loai, n.MaNV, n.HoTen, q.SoQD AS So, q.NgayKetThuc AS NgayHetHan,
                                     DATEDIFF(DAY, CAST(GETDATE() AS date), q.NgayKetThuc) AS ConLaiNgay
                              FROM NhanVien n JOIN QuyetDinhTuyenDung q ON q.MaNV = n.MaNV
                              WHERE n.TrangThai = 'THUVIEC' AND q.NgayKetThuc <= DATEADD(DAY, @n, CAST(GETDATE() AS date))
                              UNION ALL
                              SELECT N'Hợp đồng ngắn hạn', n.MaNV, n.HoTen, h.SoHD, h.NgayKetThuc,
                                     DATEDIFF(DAY, CAST(GETDATE() AS date), h.NgayKetThuc)
                              FROM HopDong h JOIN NhanVien n ON n.MaNV = h.MaNV
                              WHERE h.LoaiHD = 'NGANHAN' AND n.TrangThai <> 'NGHIVIEC'
                                AND h.NgayKetThuc <= DATEADD(DAY, @n, CAST(GETDATE() AS date))
                                AND NOT EXISTS (SELECT 1 FROM HopDong h2 WHERE h2.MaNV = h.MaNV AND (h2.NgayKetThuc IS NULL OR h2.NgayKetThuc > h.NgayKetThuc))
                              ORDER BY NgayHetHan", Db.P("@n", soNgay));
        }

        public DataTable ThuongPhat(int nam)
        {
            return Db.Query(@"SELECT n.MaNV, n.HoTen, p.TenPB, ISNULL(t.TongThuong, 0) AS TongThuong, ISNULL(f.TongPhat, 0) AS TongPhat
                              FROM NhanVien n JOIN PhongBan p ON p.MaPB = n.MaPB
                                   LEFT JOIN (SELECT x.MaNV, SUM(x.SoTien) AS TongThuong FROM PhieuChiKhenThuong x JOIN QuyetDinhKhenThuong k ON k.SoQD = x.SoQD
                                              WHERE YEAR(k.NgayQD) = @n GROUP BY x.MaNV) t ON t.MaNV = n.MaNV
                                   LEFT JOIN (SELECT x.MaNV, SUM(x.SoTien) AS TongPhat FROM PhieuThuNopPhat x JOIN QuyetDinhKyLuat k ON k.SoQD = x.SoQD
                                              WHERE YEAR(k.NgayQD) = @n GROUP BY x.MaNV) f ON f.MaNV = n.MaNV
                              WHERE t.MaNV IS NOT NULL OR f.MaNV IS NOT NULL ORDER BY n.MaNV", Db.P("@n", nam));
        }
    }
}
