using System;
using System.Data;
using System.Text.RegularExpressions;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.Services
{
    /// <summary>Hồ sơ nhân viên và quá trình công tác.</summary>
    public class NhanVienService
    {
        public DataTable LayNhanVien(string tuKhoa, string maPB, string trangThai)
        {
            string k = "%" + (tuKhoa ?? "").Trim() + "%";
            return Db.Query(@"SELECT n.MaNV, n.HoTen, n.NgaySinh, n.GioiTinh, n.DienThoai, n.CCCD, n.DiaChi, n.MaPB, p.TenPB,
                                     CASE n.TrangThai WHEN 'THUVIEC' THEN N'Thử việc' WHEN 'CHINHTHUC' THEN N'Chính thức' ELSE N'Nghỉ việc' END AS TrangThai,
                                     (SELECT TOP 1 c.TenCV FROM QuyetDinhChucVu q JOIN ChucVu c ON c.MaCV = q.MaCV
                                      WHERE q.MaNV = n.MaNV AND q.NgayHieuLuc <= CAST(GETDATE() AS date) ORDER BY q.NgayHieuLuc DESC) AS ChucVu
                              FROM NhanVien n JOIN PhongBan p ON p.MaPB = n.MaPB
                              WHERE (n.MaNV LIKE @k OR n.HoTen LIKE @k OR n.DienThoai LIKE @k OR n.CCCD LIKE @k)
                                AND (@pb IS NULL OR n.MaPB = @pb) AND (@tt IS NULL OR n.TrangThai = @tt)
                              ORDER BY n.MaNV", new System.Data.SqlClient.SqlParameter("@k", k), Db.P("@pb", maPB), Db.P("@tt", trangThai));
        }

        /// <summary>Combo nhân viên đang làm việc (không gồm đã nghỉ), có thể lọc theo phòng ban.</summary>
        public DataTable LayCombo(string maPB)
        {
            return Db.Query(@"SELECT MaNV, MaNV + N' - ' + HoTen AS HienThi FROM NhanVien
                              WHERE TrangThai <> 'NGHIVIEC' AND (@pb IS NULL OR MaPB = @pb) ORDER BY MaNV", Db.P("@pb", maPB));
        }
        public DataTable LayComboTheoTrangThai(string trangThai)
        {
            return Db.Query("SELECT MaNV, MaNV + N' - ' + HoTen AS HienThi FROM NhanVien WHERE TrangThai = @t ORDER BY MaNV", Db.P("@t", trangThai));
        }

        /// <summary>Kiểm tra thông tin nhân thân; trả về thông báo lỗi hoặc null nếu hợp lệ.</summary>
        public static string KiemTraNhanThan(string hoTen, DateTime ngaySinh, string gioiTinh, string diaChi, string dienThoai, string cccd)
        {
            if (string.IsNullOrWhiteSpace(hoTen) || string.IsNullOrWhiteSpace(diaChi) || string.IsNullOrWhiteSpace(dienThoai) || string.IsNullOrWhiteSpace(cccd))
                return "Họ tên, địa chỉ, điện thoại và số CCCD không được để trống.";
            if (gioiTinh != "Nam" && gioiTinh != "Nữ") return "Giới tính phải là Nam hoặc Nữ.";
            if (ngaySinh.Date > DateTime.Today.AddYears(-18)) return "Nhân viên phải đủ 18 tuổi.";
            if (!Regex.IsMatch(dienThoai.Trim(), @"^\d{9,11}$")) return "Điện thoại chỉ gồm 9 đến 11 chữ số.";
            if (!Regex.IsMatch(cccd.Trim(), @"^(\d{9}|\d{12})$")) return "Số CCCD gồm 9 hoặc 12 chữ số.";
            return null;
        }

        public KetQuaXuLy CapNhat(string maNV, string hoTen, DateTime ngaySinh, string gioiTinh, string diaChi, string dienThoai, string cccd, string maPB)
        {
            if (string.IsNullOrWhiteSpace(maNV)) return KetQuaXuLy.Fail("Hãy chọn nhân viên trên lưới.");
            string loi = KiemTraNhanThan(hoTen, ngaySinh, gioiTinh, diaChi, dienThoai, cccd);
            if (loi != null) return KetQuaXuLy.Fail(loi);
            if (string.IsNullOrWhiteSpace(maPB)) return KetQuaXuLy.Fail("Hãy chọn phòng ban.");
            try
            {
                var dt = Db.Query("SELECT MaPB, TrangThai FROM NhanVien WHERE MaNV = @m", Db.P("@m", maNV));
                if (dt.Rows.Count == 0) return KetQuaXuLy.Fail("Nhân viên không tồn tại.");
                if (Convert.ToString(dt.Rows[0]["TrangThai"]) == QuyDinh.NghiViec) return KetQuaXuLy.Fail("Nhân viên đã nghỉ việc, không cập nhật được.");
                if (Convert.ToString(dt.Rows[0]["MaPB"]) != maPB)
                {
                    object tp = Db.Scalar("SELECT TenPB FROM PhongBan WHERE MaTruongPhong = @m", Db.P("@m", maNV));
                    if (tp != null) return KetQuaXuLy.Fail("Nhân viên đang là trưởng phòng " + tp + ". Hãy đổi trưởng phòng trước khi chuyển phòng ban.");
                }
                if (Db.Scalar("SELECT 1 FROM NhanVien WHERE CCCD = @c AND MaNV <> @m", Db.P("@c", cccd), Db.P("@m", maNV)) != null)
                    return KetQuaXuLy.Fail("Số CCCD đã thuộc nhân viên khác.");
                Db.Execute(@"UPDATE NhanVien SET HoTen = @t, NgaySinh = @ns, GioiTinh = @gt, DiaChi = @dc, DienThoai = @dt, CCCD = @c, MaPB = @pb WHERE MaNV = @m",
                    Db.P("@t", hoTen), Db.P("@ns", ngaySinh.Date), Db.P("@gt", gioiTinh), Db.P("@dc", diaChi), Db.P("@dt", dienThoai), Db.P("@c", cccd), Db.P("@pb", maPB), Db.P("@m", maNV));
                return KetQuaXuLy.Ok("Đã cập nhật hồ sơ nhân viên " + maNV + ".");
            }
            catch (Exception ex) { return KetQuaXuLy.TuLoi(ex); }
        }

        public KetQuaXuLy NghiViec(string maNV, DateTime ngayNghi, string lyDo)
        {
            if (string.IsNullOrWhiteSpace(maNV)) return KetQuaXuLy.Fail("Hãy chọn nhân viên trên lưới.");
            if (string.IsNullOrWhiteSpace(lyDo)) return KetQuaXuLy.Fail("Phải ghi lý do nghỉ việc.");
            try
            {
                var dt = Db.Query("SELECT TrangThai FROM NhanVien WHERE MaNV = @m", Db.P("@m", maNV));
                if (dt.Rows.Count == 0) return KetQuaXuLy.Fail("Nhân viên không tồn tại.");
                if (Convert.ToString(dt.Rows[0]["TrangThai"]) == QuyDinh.NghiViec) return KetQuaXuLy.Fail("Nhân viên đã nghỉ việc.");
                object tp = Db.Scalar("SELECT TenPB FROM PhongBan WHERE MaTruongPhong = @m", Db.P("@m", maNV));
                if (tp != null) return KetQuaXuLy.Fail("Nhân viên đang là trưởng phòng " + tp + ". Hãy đổi trưởng phòng trước.");
                Db.Execute("UPDATE NhanVien SET TrangThai = 'NGHIVIEC', NgayNghi = @d, LyDoNghi = @l WHERE MaNV = @m",
                    Db.P("@d", ngayNghi.Date), Db.P("@l", lyDo), Db.P("@m", maNV));
                return KetQuaXuLy.Ok("Đã ghi nhận nhân viên " + maNV + " nghỉ việc.");
            }
            catch (Exception ex) { return KetQuaXuLy.TuLoi(ex); }
        }

        /// <summary>Quá trình công tác: tuyển dụng, hợp đồng, chức vụ, lương, khen thưởng, kỷ luật.</summary>
        public DataTable LayLichSu(string maNV)
        {
            return Db.Query(@"
SELECT q.NgayQD AS Ngay, N'Quyết định tuyển dụng' AS Loai, q.SoQD,
       N'Thử việc ' + CAST(q.SoThang AS nvarchar(5)) + N' tháng tại ' + p.TenPB + N', lương thử việc ' + FORMAT(q.LuongThuViec, 'N0') AS NoiDung
FROM QuyetDinhTuyenDung q JOIN PhongBan p ON p.MaPB = q.MaPB WHERE q.MaNV = @m
UNION ALL
SELECT h.NgayKy, CASE h.LoaiHD WHEN 'DAIHAN' THEN N'Hợp đồng dài hạn' ELSE N'Hợp đồng ngắn hạn' END, h.SoHD,
       N'Bắt đầu ' + CONVERT(varchar(10), h.NgayBatDau, 103) + ISNULL(N', kết thúc ' + CONVERT(varchar(10), h.NgayKetThuc, 103), N'')
FROM HopDong h WHERE h.MaNV = @m
UNION ALL
SELECT q.NgayHieuLuc, N'Quyết định chức vụ', q.SoQD, c.TenCV + N', phụ cấp chức vụ ' + FORMAT(q.PhuCapChucVu, 'N0')
FROM QuyetDinhChucVu q JOIN ChucVu c ON c.MaCV = q.MaCV WHERE q.MaNV = @m
UNION ALL
SELECT q.NgayHieuLuc, N'Quyết định lương', q.SoQD,
       N'Lương cơ bản ' + FORMAT(q.LuongCoBan, 'N0') + N', phụ cấp lương ' + FORMAT(q.PhuCapLuong, 'N0') + N', độc hại ' + FORMAT(q.PhuCapDocHai, 'N0')
FROM QuyetDinhLuong q WHERE q.MaNV = @m
UNION ALL
SELECT k.NgayQD, N'Khen thưởng', k.SoQD, k.LyDo + N' - ' + FORMAT(x.SoTien, 'N0')
FROM PhieuChiKhenThuong x JOIN QuyetDinhKhenThuong k ON k.SoQD = x.SoQD WHERE x.MaNV = @m
UNION ALL
SELECT k.NgayQD, N'Kỷ luật', k.SoQD, k.LyDo + CASE WHEN k.SoTienPhat > 0 THEN N' - phạt ' + FORMAT(k.SoTienPhat, 'N0') ELSE N'' END
FROM QuyetDinhKyLuat k
WHERE k.MaNV = @m OR (k.DoiTuong = 'TAPTHE' AND k.MaPB = (SELECT MaPB FROM NhanVien WHERE MaNV = @m))
ORDER BY Ngay DESC", Db.P("@m", maNV));
        }
    }
}
