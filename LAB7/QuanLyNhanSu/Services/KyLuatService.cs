using System;
using System.Data;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.Services
{
    /// <summary>Quyết định kỷ luật cho cá nhân hoặc tập thể và phiếu thu nộp phạt (nộp trực tiếp hoặc trừ lương).</summary>
    public class KyLuatService
    {
        public DataTable LayQuyetDinh()
        {
            return Db.Query(@"SELECT k.SoQD, k.NgayQD, CASE k.DoiTuong WHEN 'CANHAN' THEN N'Cá nhân' ELSE N'Tập thể' END AS DoiTuong,
                                     COALESCE(n.HoTen, p.TenPB) AS TenDoiTuong,
                                     CASE k.HinhThuc WHEN 'KHIENTRACH' THEN N'Khiển trách' WHEN 'CANHCAO' THEN N'Cảnh cáo' WHEN 'PHATTIEN' THEN N'Phạt tiền' ELSE N'Sa thải' END AS HinhThuc,
                                     k.LyDo, k.SoTienPhat,
                                     CASE k.CachThu WHEN 'NOPPHAT' THEN N'Nộp phạt' WHEN 'TRULUONG' THEN N'Trừ lương' ELSE N'' END AS CachThu,
                                     g.HoTen AS NguoiKy
                              FROM QuyetDinhKyLuat k LEFT JOIN NhanVien n ON n.MaNV = k.MaNV LEFT JOIN PhongBan p ON p.MaPB = k.MaPB
                                   JOIN NhanVien g ON g.MaNV = k.NguoiKy ORDER BY k.NgayQD DESC, k.SoQD DESC");
        }

        public DataTable LayPhieuThu(string soQD)
        {
            return Db.Query(@"SELECT x.SoPhieu, x.MaNV, n.HoTen, x.SoTien, CASE x.CachThu WHEN 'NOPPHAT' THEN N'Nộp phạt' ELSE N'Trừ lương' END AS CachThu,
                                     CASE x.TrangThai WHEN 'DATHU' THEN N'Đã thu' ELSE N'Chưa thu' END AS TrangThai, x.NgayThu, x.ThangTru, x.NamTru
                              FROM PhieuThuNopPhat x JOIN NhanVien n ON n.MaNV = x.MaNV WHERE x.SoQD = @q ORDER BY x.MaNV", Db.P("@q", soQD));
        }

        public KetQuaXuLy LapQuyetDinh(string soQD, DateTime ngayQD, string doiTuong, string maNV, string maPB, string hinhThuc, string lyDo, decimal soTien, string cachThu, string nguoiKy)
        {
            if (string.IsNullOrWhiteSpace(soQD) || string.IsNullOrWhiteSpace(lyDo)) return KetQuaXuLy.Fail("Số quyết định và lý do không được để trống.");
            if (ngayQD.Date > DateTime.Today) return KetQuaXuLy.Fail("Ngày quyết định không được ở tương lai.");
            if (hinhThuc != QuyDinh.KhienTrach && hinhThuc != QuyDinh.CanhCao && hinhThuc != QuyDinh.PhatTien && hinhThuc != QuyDinh.SaThai) return KetQuaXuLy.Fail("Hình thức kỷ luật không hợp lệ.");
            if (string.IsNullOrWhiteSpace(nguoiKy)) return KetQuaXuLy.Fail("Hãy chọn người ký.");
            if (hinhThuc == QuyDinh.PhatTien)
            {
                if (soTien <= 0) return KetQuaXuLy.Fail("Hình thức phạt tiền phải có số tiền phạt lớn hơn 0.");
                if (cachThu != QuyDinh.NopPhat && cachThu != QuyDinh.TruLuong) return KetQuaXuLy.Fail("Phải chọn cách thu: nộp phạt hoặc trừ lương.");
            }
            else { if (soTien != 0) return KetQuaXuLy.Fail("Chỉ hình thức phạt tiền mới có số tiền phạt."); cachThu = null; }
            if (hinhThuc == QuyDinh.SaThai && doiTuong != QuyDinh.CaNhan) return KetQuaXuLy.Fail("Hình thức sa thải chỉ áp dụng cho cá nhân.");
            try
            {
                if (!KhenThuongService.LaGiamDoc(nguoiKy)) return KetQuaXuLy.Fail("Quyết định kỷ luật phải do giám đốc ký.");
                if (Db.Scalar("SELECT 1 FROM QuyetDinhKyLuat WHERE SoQD = @s", Db.P("@s", soQD)) != null) return KetQuaXuLy.Fail("Số quyết định đã tồn tại.");
                string loi;
                var ds = KhenThuongService.LayMucTieu(doiTuong, maNV, maPB, false, out loi);
                if (loi != null) return KetQuaXuLy.Fail(loi);
                if (hinhThuc == QuyDinh.SaThai)
                {
                    object tp = Db.Scalar("SELECT TenPB FROM PhongBan WHERE MaTruongPhong = @m", Db.P("@m", maNV));
                    if (tp != null) return KetQuaXuLy.Fail("Nhân viên đang là trưởng phòng " + tp + ". Hãy đổi trưởng phòng trước khi sa thải.");
                }
                using (var cn = Db.OpenConnection())
                using (var tr = cn.BeginTransaction())
                {
                    try
                    {
                        Db.Exec(cn, tr, @"INSERT INTO QuyetDinhKyLuat(SoQD, NgayQD, DoiTuong, MaNV, MaPB, HinhThuc, LyDo, SoTienPhat, CachThu, NguoiKy)
                                          VALUES(@s, @n, @dt, @m, @p, @h, @ld, @t, @c, @k)",
                            Db.P("@s", soQD), Db.P("@n", ngayQD.Date), Db.P("@dt", doiTuong), Db.P("@m", doiTuong == QuyDinh.CaNhan ? maNV : null),
                            Db.P("@p", doiTuong == QuyDinh.TapThe ? maPB : null), Db.P("@h", hinhThuc), Db.P("@ld", lyDo), Db.P("@t", soTien), Db.P("@c", cachThu), Db.P("@k", nguoiKy));
                        if (soTien > 0)
                            foreach (string ma in ds)
                                Db.Exec(cn, tr, "INSERT INTO PhieuThuNopPhat(SoQD, MaNV, SoTien, CachThu, TrangThai) VALUES(@s, @m, @t, @c, 'CHUATHU')",
                                    Db.P("@s", soQD), Db.P("@m", ma), Db.P("@t", soTien), Db.P("@c", cachThu));
                        if (hinhThuc == QuyDinh.SaThai)
                            Db.Exec(cn, tr, "UPDATE NhanVien SET TrangThai = 'NGHIVIEC', NgayNghi = @d, LyDoNghi = @l WHERE MaNV = @m",
                                Db.P("@d", ngayQD.Date), Db.P("@l", "Kỷ luật sa thải: " + lyDo), Db.P("@m", maNV));
                        tr.Commit();
                        return KetQuaXuLy.Ok("Đã lập quyết định kỷ luật " + soQD + (soTien > 0 ? " và " + ds.Count + " phiếu thu nộp phạt (" + (soTien * ds.Count).ToString("N0") + " đ)." : "."));
                    }
                    catch (Exception ex) { try { tr.Rollback(); } catch { } return KetQuaXuLy.TuLoi(ex); }
                }
            }
            catch (Exception ex) { return KetQuaXuLy.TuLoi(ex); }
        }

        /// <summary>Thu tiền phạt do nhân viên nộp trực tiếp (loại trừ lương được thu khi lập bảng lương).</summary>
        public KetQuaXuLy ThuPhat(string soPhieu)
        {
            int so;
            if (!int.TryParse(soPhieu, out so)) return KetQuaXuLy.Fail("Hãy chọn phiếu thu trên lưới.");
            try
            {
                var dt = Db.Query("SELECT CachThu, TrangThai FROM PhieuThuNopPhat WHERE SoPhieu = @s", Db.P("@s", so));
                if (dt.Rows.Count == 0) return KetQuaXuLy.Fail("Phiếu thu không tồn tại.");
                if (Convert.ToString(dt.Rows[0]["TrangThai"]) == "DATHU") return KetQuaXuLy.Fail("Phiếu thu này đã thu rồi.");
                if (Convert.ToString(dt.Rows[0]["CachThu"]) != QuyDinh.NopPhat) return KetQuaXuLy.Fail("Phiếu này thu bằng cách trừ lương, sẽ được trừ khi lập bảng lương.");
                Db.Execute("UPDATE PhieuThuNopPhat SET TrangThai = 'DATHU', NgayThu = @d WHERE SoPhieu = @s", Db.P("@d", DateTime.Today), Db.P("@s", so));
                return KetQuaXuLy.Ok("Đã thu tiền phạt, phiếu số " + so + ".");
            }
            catch (Exception ex) { return KetQuaXuLy.TuLoi(ex); }
        }
    }
}
