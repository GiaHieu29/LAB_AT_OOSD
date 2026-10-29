using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.Services
{
    /// <summary>Kế hoạch phân công theo tuần do trưởng phòng, ban, phân xưởng lập (kèm kế hoạch phụ trội).</summary>
    public class PhanCongService
    {
        public DataTable LayKeHoach()
        {
            return Db.Query(@"SELECT k.MaKH, p.TenPB, k.TuNgay, k.DenNgay, t.HoTen AS NguoiLap, k.NgayLap,
                                     (SELECT COUNT(*) FROM ChiTietPhanCong c WHERE c.MaKH = k.MaKH) AS SoDong
                              FROM KeHoachTuan k JOIN PhongBan p ON p.MaPB = k.MaPB JOIN NhanVien t ON t.MaNV = k.NguoiLap
                              ORDER BY k.TuNgay DESC, p.TenPB");
        }
        public DataTable LayChiTiet(string maKH)
        {
            return Db.Query(@"SELECT c.MaNV, n.HoTen, c.Ngay, ca.TenCa, c.GioPhuTroiKH
                              FROM ChiTietPhanCong c JOIN NhanVien n ON n.MaNV = c.MaNV JOIN CaLamViec ca ON ca.MaCa = c.MaCa
                              WHERE c.MaKH = @k ORDER BY c.Ngay, c.MaNV", Db.P("@k", maKH));
        }
        public string LayTruongPhong(string maPB)
        {
            if (string.IsNullOrEmpty(maPB)) return "";
            object o = Db.Scalar("SELECT t.MaNV + N' - ' + t.HoTen FROM PhongBan p JOIN NhanVien t ON t.MaNV = p.MaTruongPhong WHERE p.MaPB = @p", Db.P("@p", maPB));
            return o == null ? "" : Convert.ToString(o);
        }

        public KetQuaXuLy LapKeHoach(string maPB, DateTime tuNgay, List<DongPhanCong> dong)
        {
            if (string.IsNullOrWhiteSpace(maPB)) return KetQuaXuLy.Fail("Hãy chọn phòng ban.");
            tuNgay = tuNgay.Date;
            if (tuNgay.DayOfWeek != DayOfWeek.Monday) return KetQuaXuLy.Fail("Kế hoạch tuần phải bắt đầu từ thứ Hai.");
            if (tuNgay < QuyDinh.ThuHaiCuaTuan(DateTime.Today)) return KetQuaXuLy.Fail("Không lập kế hoạch cho tuần đã qua.");
            if (dong == null || dong.Count == 0) return KetQuaXuLy.Fail("Kế hoạch phải có ít nhất một dòng phân công.");
            DateTime denNgay = tuNgay.AddDays(6);
            try
            {
                object tp = Db.Scalar("SELECT MaTruongPhong FROM PhongBan WHERE MaPB = @p", Db.P("@p", maPB));
                if (tp == null) return KetQuaXuLy.Fail("Phòng ban không tồn tại.");
                if (tp == DBNull.Value) return KetQuaXuLy.Fail("Phòng ban chưa có trưởng phòng, hãy đặt trưởng phòng trong Danh mục.");
                string maTP = Convert.ToString(tp);
                if (Db.Scalar("SELECT 1 FROM KeHoachTuan WHERE MaPB = @p AND TuNgay = @t", Db.P("@p", maPB), Db.P("@t", tuNgay)) != null)
                    return KetQuaXuLy.Fail("Phòng ban này đã có kế hoạch cho tuần bắt đầu " + tuNgay.ToString("dd/MM/yyyy") + ".");

                var nvOk = new HashSet<string>();
                foreach (DataRow r in Db.Query("SELECT MaNV FROM NhanVien WHERE MaPB = @p AND TrangThai <> 'NGHIVIEC'", Db.P("@p", maPB)).Rows) nvOk.Add(Convert.ToString(r[0]));
                var caOk = new HashSet<string>();
                foreach (DataRow r in Db.Query("SELECT MaCa FROM CaLamViec").Rows) caOk.Add(Convert.ToString(r[0]));
                var trung = new HashSet<string>();
                foreach (var d in dong)
                {
                    if (!nvOk.Contains(d.MaNV)) return KetQuaXuLy.Fail("Nhân viên " + d.MaNV + " không thuộc phòng ban này hoặc đã nghỉ việc.");
                    if (!caOk.Contains(d.MaCa)) return KetQuaXuLy.Fail("Ca làm việc " + d.MaCa + " không tồn tại.");
                    if (d.Ngay.Date < tuNgay || d.Ngay.Date > denNgay) return KetQuaXuLy.Fail("Ngày " + d.Ngay.ToString("dd/MM/yyyy") + " nằm ngoài tuần " + tuNgay.ToString("dd/MM") + " - " + denNgay.ToString("dd/MM/yyyy") + ".");
                    if (d.GioPhuTroi < 0 || d.GioPhuTroi > 4) return KetQuaXuLy.Fail("Giờ phụ trội dự kiến mỗi ngày từ 0 đến 4 giờ.");
                    if (!trung.Add(d.MaNV + "|" + d.Ngay.ToString("yyyyMMdd"))) return KetQuaXuLy.Fail("Nhân viên " + d.MaNV + " bị phân công hai lần trong ngày " + d.Ngay.ToString("dd/MM/yyyy") + ".");
                }

                using (var cn = Db.OpenConnection())
                using (var tr = cn.BeginTransaction())
                {
                    try
                    {
                        int maKH;
                        using (var cmd = new SqlCommand(@"INSERT INTO KeHoachTuan(MaPB, TuNgay, DenNgay, NguoiLap, NgayLap) OUTPUT INSERTED.MaKH
                                                          VALUES(@p, @t, @d, @n, SYSDATETIME())", cn, tr))
                        {
                            cmd.Parameters.Add(Db.P("@p", maPB)); cmd.Parameters.Add(Db.P("@t", tuNgay)); cmd.Parameters.Add(Db.P("@d", denNgay)); cmd.Parameters.Add(Db.P("@n", maTP));
                            maKH = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                        foreach (var d in dong)
                            Db.Exec(cn, tr, "INSERT INTO ChiTietPhanCong(MaKH, MaNV, Ngay, MaCa, GioPhuTroiKH) VALUES(@k, @m, @n, @c, @g)",
                                Db.P("@k", maKH), Db.P("@m", d.MaNV), Db.P("@n", d.Ngay.Date), Db.P("@c", d.MaCa), Db.P("@g", d.GioPhuTroi));
                        tr.Commit();
                        return KetQuaXuLy.Ok("Đã lập kế hoạch tuần " + tuNgay.ToString("dd/MM") + " - " + denNgay.ToString("dd/MM/yyyy") + " gồm " + dong.Count + " dòng phân công, chuyển ban chấm công theo dõi.");
                    }
                    catch (Exception ex) { try { tr.Rollback(); } catch { } return KetQuaXuLy.TuLoi(ex); }
                }
            }
            catch (Exception ex) { return KetQuaXuLy.TuLoi(ex); }
        }
    }
}
