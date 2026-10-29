using System;
using System.Collections.Generic;
using System.Data;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.Services
{
    /// <summary>Quyết định khen thưởng cho cá nhân hoặc tập thể phòng ban (do giám đốc ký) và phiếu chi khen thưởng.</summary>
    public class KhenThuongService
    {
        /// <summary>Người ký là giám đốc theo quyết định chức vụ hiện hành.</summary>
        public static bool LaGiamDoc(string maNV)
        {
            return Db.Scalar(@"SELECT 1 FROM NhanVien n WHERE n.MaNV = @m AND n.TrangThai = 'CHINHTHUC'
                               AND (SELECT TOP 1 q.MaCV FROM QuyetDinhChucVu q WHERE q.MaNV = n.MaNV AND q.NgayHieuLuc <= CAST(GETDATE() AS date)
                                    ORDER BY q.NgayHieuLuc DESC) = @cv", Db.P("@m", maNV), Db.P("@cv", QuyDinh.MaChucVuGiamDoc)) != null;
        }

        public DataTable LayGiamDoc()
        {
            return Db.Query(@"SELECT n.MaNV, n.MaNV + N' - ' + n.HoTen AS HienThi FROM NhanVien n WHERE n.TrangThai = 'CHINHTHUC'
                              AND (SELECT TOP 1 q.MaCV FROM QuyetDinhChucVu q WHERE q.MaNV = n.MaNV AND q.NgayHieuLuc <= CAST(GETDATE() AS date)
                                   ORDER BY q.NgayHieuLuc DESC) = @cv ORDER BY n.MaNV", Db.P("@cv", QuyDinh.MaChucVuGiamDoc));
        }

        /// <summary>Danh sách nhân viên thuộc đối tượng của quyết định (cá nhân hoặc cả phòng ban).</summary>
        internal static List<string> LayMucTieu(string doiTuong, string maNV, string maPB, bool chiChinhThuc, out string loi)
        {
            loi = null; var ds = new List<string>();
            string dk = chiChinhThuc ? "TrangThai = 'CHINHTHUC'" : "TrangThai <> 'NGHIVIEC'";
            DataTable dt;
            if (doiTuong == QuyDinh.CaNhan)
            {
                if (string.IsNullOrWhiteSpace(maNV)) { loi = "Hãy chọn nhân viên."; return ds; }
                dt = Db.Query("SELECT MaNV FROM NhanVien WHERE MaNV = @m AND " + dk, Db.P("@m", maNV));
                if (dt.Rows.Count == 0) loi = chiChinhThuc ? "Loại thưởng này chỉ dành cho nhân viên chính thức." : "Nhân viên không tồn tại hoặc đã nghỉ việc.";
            }
            else if (doiTuong == QuyDinh.TapThe)
            {
                if (string.IsNullOrWhiteSpace(maPB)) { loi = "Hãy chọn phòng ban."; return ds; }
                dt = Db.Query("SELECT MaNV FROM NhanVien WHERE MaPB = @p AND " + dk, Db.P("@p", maPB));
                if (dt.Rows.Count == 0) loi = "Phòng ban không có nhân viên đủ điều kiện.";
            }
            else { loi = "Đối tượng phải là cá nhân hoặc tập thể phòng ban."; return ds; }
            if (loi == null) foreach (DataRow r in dt.Rows) ds.Add(Convert.ToString(r[0]));
            return ds;
        }

        public DataTable LayQuyetDinh()
        {
            return Db.Query(@"SELECT k.SoQD, k.NgayQD, CASE k.DoiTuong WHEN 'CANHAN' THEN N'Cá nhân' ELSE N'Tập thể' END AS DoiTuong,
                                     COALESCE(n.HoTen, p.TenPB) AS TenDoiTuong,
                                     CASE k.LoaiKT WHEN 'THANHTICH' THEN N'Thành tích' WHEN 'NGAYLE' THEN N'Ngày lễ' WHEN 'CUOINAM' THEN N'Thưởng cuối năm' ELSE N'Lương tháng 13' END AS LoaiKT,
                                     k.LyDo, k.SoTienMoiNguoi, g.HoTen AS NguoiKy,
                                     (SELECT COUNT(*) FROM PhieuChiKhenThuong x WHERE x.SoQD = k.SoQD) AS SoNguoi
                              FROM QuyetDinhKhenThuong k LEFT JOIN NhanVien n ON n.MaNV = k.MaNV LEFT JOIN PhongBan p ON p.MaPB = k.MaPB
                                   JOIN NhanVien g ON g.MaNV = k.NguoiKy ORDER BY k.NgayQD DESC, k.SoQD DESC");
        }

        public DataTable LayPhieuChi(string soQD)
        {
            return Db.Query(@"SELECT x.SoPhieu, x.MaNV, n.HoTen, x.SoTien, CASE x.TrangThai WHEN 'DACHI' THEN N'Đã chi' ELSE N'Chưa chi' END AS TrangThai, x.NgayChi, x.HinhThucChi
                              FROM PhieuChiKhenThuong x JOIN NhanVien n ON n.MaNV = x.MaNV WHERE x.SoQD = @q ORDER BY x.MaNV", Db.P("@q", soQD));
        }

        public KetQuaXuLy LapQuyetDinh(string soQD, DateTime ngayQD, string doiTuong, string maNV, string maPB, string loai, string lyDo, decimal soTien, string nguoiKy)
        {
            if (string.IsNullOrWhiteSpace(soQD) || string.IsNullOrWhiteSpace(lyDo)) return KetQuaXuLy.Fail("Số quyết định và lý do không được để trống.");
            if (soTien <= 0) return KetQuaXuLy.Fail("Mức thưởng phải lớn hơn 0.");
            if (ngayQD.Date > DateTime.Today) return KetQuaXuLy.Fail("Ngày quyết định không được ở tương lai.");
            if (loai != QuyDinh.ThanhTich && loai != QuyDinh.NgayLe && loai != QuyDinh.CuoiNam && loai != QuyDinh.Luong13) return KetQuaXuLy.Fail("Loại khen thưởng không hợp lệ.");
            if (string.IsNullOrWhiteSpace(nguoiKy)) return KetQuaXuLy.Fail("Hãy chọn người ký.");
            try
            {
                if (!LaGiamDoc(nguoiKy)) return KetQuaXuLy.Fail("Quyết định khen thưởng phải do giám đốc ký.");
                if (Db.Scalar("SELECT 1 FROM QuyetDinhKhenThuong WHERE SoQD = @s", Db.P("@s", soQD)) != null) return KetQuaXuLy.Fail("Số quyết định đã tồn tại.");
                string loi;
                var ds = LayMucTieu(doiTuong, maNV, maPB, loai != QuyDinh.ThanhTich, out loi);   // thưởng lễ, cuối năm, tháng 13 chỉ cho nhân viên chính thức
                if (loi != null) return KetQuaXuLy.Fail(loi);
                using (var cn = Db.OpenConnection())
                using (var tr = cn.BeginTransaction())
                {
                    try
                    {
                        Db.Exec(cn, tr, @"INSERT INTO QuyetDinhKhenThuong(SoQD, NgayQD, DoiTuong, MaNV, MaPB, LoaiKT, LyDo, SoTienMoiNguoi, NguoiKy)
                                          VALUES(@s, @n, @dt, @m, @p, @l, @ld, @t, @k)",
                            Db.P("@s", soQD), Db.P("@n", ngayQD.Date), Db.P("@dt", doiTuong), Db.P("@m", doiTuong == QuyDinh.CaNhan ? maNV : null),
                            Db.P("@p", doiTuong == QuyDinh.TapThe ? maPB : null), Db.P("@l", loai), Db.P("@ld", lyDo), Db.P("@t", soTien), Db.P("@k", nguoiKy));
                        foreach (string ma in ds)
                            Db.Exec(cn, tr, "INSERT INTO PhieuChiKhenThuong(SoQD, MaNV, SoTien, TrangThai) VALUES(@s, @m, @t, 'CHUACHI')", Db.P("@s", soQD), Db.P("@m", ma), Db.P("@t", soTien));
                        tr.Commit();
                        return KetQuaXuLy.Ok("Đã lập quyết định " + soQD + " và " + ds.Count + " phiếu chi khen thưởng (" + (soTien * ds.Count).ToString("N0") + " đ).");
                    }
                    catch (Exception ex) { try { tr.Rollback(); } catch { } return KetQuaXuLy.TuLoi(ex); }
                }
            }
            catch (Exception ex) { return KetQuaXuLy.TuLoi(ex); }
        }

        public KetQuaXuLy ChiPhieu(string soPhieu, string hinhThuc)
        {
            int so;
            if (!int.TryParse(soPhieu, out so)) return KetQuaXuLy.Fail("Hãy chọn phiếu chi trên lưới.");
            if (hinhThuc != "TIENMAT" && hinhThuc != "CHUYENKHOAN") return KetQuaXuLy.Fail("Hình thức chi phải là tiền mặt hoặc chuyển khoản.");
            try
            {
                object tt = Db.Scalar("SELECT TrangThai FROM PhieuChiKhenThuong WHERE SoPhieu = @s", Db.P("@s", so));
                if (tt == null) return KetQuaXuLy.Fail("Phiếu chi không tồn tại.");
                if (Convert.ToString(tt) == "DACHI") return KetQuaXuLy.Fail("Phiếu chi này đã chi rồi.");
                Db.Execute("UPDATE PhieuChiKhenThuong SET TrangThai = 'DACHI', NgayChi = @d, HinhThucChi = @h WHERE SoPhieu = @s", Db.P("@d", DateTime.Today), Db.P("@h", hinhThuc), Db.P("@s", so));
                return KetQuaXuLy.Ok("Đã chi phiếu khen thưởng số " + so + ".");
            }
            catch (Exception ex) { return KetQuaXuLy.TuLoi(ex); }
        }
    }
}
