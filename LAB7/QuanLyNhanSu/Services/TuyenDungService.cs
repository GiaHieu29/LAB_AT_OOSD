using System;
using System.Data;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.Services
{
    /// <summary>Quyết định tuyển dụng kèm thời gian thử việc.</summary>
    public class TuyenDungService
    {
        public DataTable LayTheoDoiThuViec()
        {
            return Db.Query(@"SELECT n.MaNV, n.HoTen, p.TenPB, q.SoQD, q.NgayBatDau, q.NgayKetThuc, q.LuongThuViec,
                                     DATEDIFF(DAY, CAST(GETDATE() AS date), q.NgayKetThuc) AS ConLaiNgay,
                                     CASE WHEN q.NgayKetThuc < CAST(GETDATE() AS date) THEN N'Đã hết thời gian thử việc'
                                          WHEN DATEDIFF(DAY, CAST(GETDATE() AS date), q.NgayKetThuc) <= 7 THEN N'Sắp hết thử việc'
                                          ELSE N'Đang thử việc' END AS TinhTrang
                              FROM NhanVien n JOIN QuyetDinhTuyenDung q ON q.MaNV = n.MaNV JOIN PhongBan p ON p.MaPB = n.MaPB
                              WHERE n.TrangThai = 'THUVIEC' ORDER BY q.NgayKetThuc");
        }

        public KetQuaXuLy LapQuyetDinh(HoSoUngVien uv, string soQD, DateTime ngayQD, DateTime ngayBatDau, int soThang,
                                       decimal luongThuViec, string dkCongTy, string dkNhanVien, string nguoiKy)
        {
            if (string.IsNullOrWhiteSpace(uv.MaNV) || string.IsNullOrWhiteSpace(soQD)) return KetQuaXuLy.Fail("Mã nhân viên và số quyết định không được để trống.");
            string loi = NhanVienService.KiemTraNhanThan(uv.HoTen, uv.NgaySinh, uv.GioiTinh, uv.DiaChi, uv.DienThoai, uv.CCCD);
            if (loi != null) return KetQuaXuLy.Fail(loi);
            if (string.IsNullOrWhiteSpace(uv.MaPB)) return KetQuaXuLy.Fail("Hãy chọn phòng ban có nhu cầu tuyển dụng.");
            if (soThang < 1 || soThang > 6) return KetQuaXuLy.Fail("Thời gian thử việc từ 1 đến 6 tháng.");
            if (ngayBatDau.Date < ngayQD.Date) return KetQuaXuLy.Fail("Ngày bắt đầu thử việc không được trước ngày ký quyết định.");
            if (luongThuViec < QuyDinh.LuongToiThieu * 0.85m) return KetQuaXuLy.Fail("Lương thử việc không được thấp hơn 85% lương tối thiểu vùng (" + (QuyDinh.LuongToiThieu * 0.85m).ToString("N0") + " đ).");
            if (string.IsNullOrWhiteSpace(dkCongTy) || string.IsNullOrWhiteSpace(dkNhanVien)) return KetQuaXuLy.Fail("Quyết định phải ghi rõ điều khoản của cả công ty và nhân viên.");
            if (string.IsNullOrWhiteSpace(nguoiKy)) return KetQuaXuLy.Fail("Phải ghi người ký quyết định.");
            DateTime ngayKetThuc = ngayBatDau.Date.AddMonths(soThang).AddDays(-1);
            try
            {
                var pb = Db.Query("SELECT NhuCauTuyen FROM PhongBan WHERE MaPB = @p", Db.P("@p", uv.MaPB));
                if (pb.Rows.Count == 0) return KetQuaXuLy.Fail("Phòng ban không tồn tại.");
                if (Convert.ToInt32(pb.Rows[0]["NhuCauTuyen"]) <= 0) return KetQuaXuLy.Fail("Phòng ban này hiện không có nhu cầu tuyển dụng.");
                if (Db.Scalar("SELECT 1 FROM NhanVien WHERE MaNV = @m", Db.P("@m", uv.MaNV)) != null) return KetQuaXuLy.Fail("Mã nhân viên đã tồn tại.");
                if (Db.Scalar("SELECT 1 FROM NhanVien WHERE CCCD = @c", Db.P("@c", uv.CCCD)) != null) return KetQuaXuLy.Fail("Số CCCD đã tồn tại trong hồ sơ nhân viên.");
                if (Db.Scalar("SELECT 1 FROM QuyetDinhTuyenDung WHERE SoQD = @s", Db.P("@s", soQD)) != null) return KetQuaXuLy.Fail("Số quyết định đã tồn tại.");

                using (var cn = Db.OpenConnection())
                using (var tr = cn.BeginTransaction())
                {
                    try
                    {
                        Db.Exec(cn, tr, @"INSERT INTO NhanVien(MaNV, HoTen, NgaySinh, GioiTinh, DiaChi, DienThoai, CCCD, MaPB, TrangThai)
                                          VALUES(@m, @t, @ns, @gt, @dc, @dt, @c, @pb, 'THUVIEC')",
                            Db.P("@m", uv.MaNV), Db.P("@t", uv.HoTen), Db.P("@ns", uv.NgaySinh.Date), Db.P("@gt", uv.GioiTinh), Db.P("@dc", uv.DiaChi),
                            Db.P("@dt", uv.DienThoai), Db.P("@c", uv.CCCD), Db.P("@pb", uv.MaPB));
                        Db.Exec(cn, tr, @"INSERT INTO QuyetDinhTuyenDung(SoQD, MaNV, MaPB, NgayQD, NgayBatDau, SoThang, NgayKetThuc, LuongThuViec, DieuKhoanCongTy, DieuKhoanNhanVien, NguoiKy)
                                          VALUES(@s, @m, @pb, @nq, @bd, @th, @kt, @l, @ct, @nv, @ky)",
                            Db.P("@s", soQD), Db.P("@m", uv.MaNV), Db.P("@pb", uv.MaPB), Db.P("@nq", ngayQD.Date), Db.P("@bd", ngayBatDau.Date), Db.P("@th", soThang),
                            Db.P("@kt", ngayKetThuc), Db.P("@l", luongThuViec), Db.P("@ct", dkCongTy), Db.P("@nv", dkNhanVien), Db.P("@ky", nguoiKy));
                        // mỗi nhân viên được tuyển làm giảm nhu cầu tuyển dụng của phòng ban
                        Db.Exec(cn, tr, "UPDATE PhongBan SET NhuCauTuyen = NhuCauTuyen - 1 WHERE MaPB = @p AND NhuCauTuyen > 0", Db.P("@p", uv.MaPB));
                        tr.Commit();
                        return KetQuaXuLy.Ok("Đã lập quyết định tuyển dụng " + soQD + ". Thử việc đến hết ngày " + ngayKetThuc.ToString("dd/MM/yyyy") + ".");
                    }
                    catch (Exception ex) { try { tr.Rollback(); } catch { } return KetQuaXuLy.TuLoi(ex); }
                }
            }
            catch (Exception ex) { return KetQuaXuLy.TuLoi(ex); }
        }
    }
}
