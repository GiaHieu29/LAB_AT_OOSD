using System;
using System.Collections.Generic;
using System.Data;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.Services
{
    /// <summary>Phiếu ghi nhận giờ phụ trội (tăng ca, ngày lễ, ngày nghỉ) do ban chấm công lập.</summary>
    public class PhuTroiService
    {
        public DataTable LayPhieu()
        {
            return Db.Query(@"SELECT s.SoPhieu, s.NgayPhuTroi,
                                     CASE s.HinhThuc WHEN 'TANGCA' THEN N'Tăng ca' WHEN 'NGAYLE' THEN N'Ngày lễ' ELSE N'Ngày nghỉ' END AS HinhThuc,
                                     s.SoGio, s.NguoiLap, (SELECT COUNT(*) FROM ChiTietPhuTroi c WHERE c.SoPhieu = s.SoPhieu) AS SoNguoi
                              FROM PhieuPhuTroi s ORDER BY s.NgayPhuTroi DESC, s.SoPhieu DESC");
        }
        public DataTable LayChiTiet(string soPhieu)
        {
            return Db.Query(@"SELECT c.MaNV, n.HoTen, c.SoGio FROM ChiTietPhuTroi c JOIN NhanVien n ON n.MaNV = c.MaNV WHERE c.SoPhieu = @s ORDER BY c.MaNV", Db.P("@s", soPhieu));
        }

        public KetQuaXuLy LapPhieu(string soPhieu, DateTime ngay, string hinhThuc, decimal soGio, string nguoiLap, List<DongPhuTroi> dong)
        {
            if (string.IsNullOrWhiteSpace(soPhieu) || string.IsNullOrWhiteSpace(nguoiLap)) return KetQuaXuLy.Fail("Số phiếu và người lập không được để trống.");
            if (hinhThuc != QuyDinh.TangCa && hinhThuc != QuyDinh.NgayLe && hinhThuc != QuyDinh.NgayNghi) return KetQuaXuLy.Fail("Hình thức phụ trội phải là tăng ca, ngày lễ hoặc ngày nghỉ.");
            if (ngay.Date > DateTime.Today) return KetQuaXuLy.Fail("Ngày phụ trội không được ở tương lai (phiếu ghi nhận việc đã thực hiện).");
            if (soGio <= 0 || soGio > 12) return KetQuaXuLy.Fail("Số giờ phụ trội của phiếu từ 0,5 đến 12 giờ.");
            if (dong == null || dong.Count == 0) return KetQuaXuLy.Fail("Phiếu phụ trội phải ghi rõ ít nhất một nhân viên tham gia.");
            var da = new HashSet<string>();
            foreach (var d in dong)
            {
                if (d.SoGio <= 0 || d.SoGio > soGio) return KetQuaXuLy.Fail("Giờ phụ trội của từng nhân viên phải lớn hơn 0 và không vượt số giờ của phiếu.");
                if (!da.Add(d.MaNV)) return KetQuaXuLy.Fail("Nhân viên " + d.MaNV + " bị ghi hai lần trong cùng phiếu.");
            }
            try
            {
                if (Db.Scalar("SELECT 1 FROM PhieuPhuTroi WHERE SoPhieu = @s", Db.P("@s", soPhieu)) != null) return KetQuaXuLy.Fail("Số phiếu đã tồn tại.");
                foreach (var d in dong)
                {
                    var nv = Db.Query("SELECT TrangThai FROM NhanVien WHERE MaNV = @m", Db.P("@m", d.MaNV));
                    if (nv.Rows.Count == 0) return KetQuaXuLy.Fail("Nhân viên " + d.MaNV + " không tồn tại.");
                    if (Convert.ToString(nv.Rows[0]["TrangThai"]) == QuyDinh.NghiViec) return KetQuaXuLy.Fail("Nhân viên " + d.MaNV + " đã nghỉ việc.");
                    decimal daCo = Convert.ToDecimal(Db.Scalar(@"SELECT ISNULL(SUM(c.SoGio), 0) FROM ChiTietPhuTroi c JOIN PhieuPhuTroi s ON s.SoPhieu = c.SoPhieu
                                                                  WHERE c.MaNV = @m AND s.NgayPhuTroi = @d", Db.P("@m", d.MaNV), Db.P("@d", ngay.Date)));
                    if (daCo + d.SoGio > 12) return KetQuaXuLy.Fail("Nhân viên " + d.MaNV + " đã có " + daCo.ToString("0.#") + " giờ phụ trội trong ngày, tổng không được quá 12 giờ.");
                }
                using (var cn = Db.OpenConnection())
                using (var tr = cn.BeginTransaction())
                {
                    try
                    {
                        Db.Exec(cn, tr, "INSERT INTO PhieuPhuTroi(SoPhieu, NgayPhuTroi, SoGio, HinhThuc, NguoiLap) VALUES(@s, @n, @g, @h, @l)",
                            Db.P("@s", soPhieu), Db.P("@n", ngay.Date), Db.P("@g", soGio), Db.P("@h", hinhThuc), Db.P("@l", nguoiLap));
                        foreach (var d in dong)
                            Db.Exec(cn, tr, "INSERT INTO ChiTietPhuTroi(SoPhieu, MaNV, SoGio) VALUES(@s, @m, @g)", Db.P("@s", soPhieu), Db.P("@m", d.MaNV), Db.P("@g", d.SoGio));
                        tr.Commit();
                        return KetQuaXuLy.Ok("Đã lập phiếu phụ trội " + soPhieu + " cho " + dong.Count + " nhân viên (tính bằng " + QuyDinh.HeSoPhuTroi.ToString("0.0") + " lần giờ bình thường).");
                    }
                    catch (Exception ex) { try { tr.Rollback(); } catch { } return KetQuaXuLy.TuLoi(ex); }
                }
            }
            catch (Exception ex) { return KetQuaXuLy.TuLoi(ex); }
        }
    }
}
