using System;
using System.Data;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.Services
{
    /// <summary>Ban chấm công: chấm công từng ngày và bảng chấm công theo tháng.</summary>
    public class ChamCongService
    {
        public DataTable LayChamCongThang(string maNV, int thang, int nam)
        {
            return Db.Query(@"SELECT c.Ngay,
                                     CASE c.TrangThai WHEN 'DILAM' THEN N'Đi làm' WHEN 'NGHIPHEP' THEN N'Nghỉ phép' ELSE N'Nghỉ không phép' END AS TrangThai,
                                     c.SoGioLam, ca.TenCa, c.GhiChu
                              FROM ChamCong c LEFT JOIN CaLamViec ca ON ca.MaCa = c.MaCa
                              WHERE c.MaNV = @m AND YEAR(c.Ngay) = @n AND MONTH(c.Ngay) = @t ORDER BY c.Ngay",
                Db.P("@m", maNV), Db.P("@n", nam), Db.P("@t", thang));
        }

        /// <summary>Kế hoạch phân công của nhân viên trong ngày, để ban chấm công đối chiếu.</summary>
        public string LayKeHoach(string maNV, DateTime ngay)
        {
            if (string.IsNullOrEmpty(maNV)) return "";
            var dt = Db.Query(@"SELECT ca.TenCa, CONVERT(varchar(5), ca.GioBatDau, 108) AS BD, CONVERT(varchar(5), ca.GioKetThuc, 108) AS KT, c.GioPhuTroiKH
                                FROM ChiTietPhanCong c JOIN CaLamViec ca ON ca.MaCa = c.MaCa WHERE c.MaNV = @m AND c.Ngay = @d",
                Db.P("@m", maNV), Db.P("@d", ngay.Date));
            if (dt.Rows.Count == 0) return "Chưa có kế hoạch phân công cho ngày này";
            var r = dt.Rows[0];
            return "Kế hoạch: " + r["TenCa"] + " (" + r["BD"] + " - " + r["KT"] + ")" +
                   (Convert.ToDecimal(r["GioPhuTroiKH"]) > 0 ? ", phụ trội dự kiến " + Convert.ToDecimal(r["GioPhuTroiKH"]).ToString("0.#") + " giờ" : "");
        }

        public KetQuaXuLy Ghi(string maNV, DateTime ngay, string trangThai, decimal soGio, string maCa, string ghiChu)
        {
            if (string.IsNullOrWhiteSpace(maNV)) return KetQuaXuLy.Fail("Hãy chọn nhân viên.");
            if (trangThai != QuyDinh.DiLam && trangThai != QuyDinh.NghiPhep && trangThai != QuyDinh.NghiKhongPhep) return KetQuaXuLy.Fail("Trạng thái chấm công không hợp lệ.");
            ngay = ngay.Date;
            if (ngay > DateTime.Today) return KetQuaXuLy.Fail("Không chấm công cho ngày chưa tới.");
            if (trangThai == QuyDinh.DiLam) { if (soGio <= 0 || soGio > 12) return KetQuaXuLy.Fail("Số giờ làm của ngày đi làm phải từ 0,5 đến 12 giờ."); }
            else soGio = 0;
            try
            {
                var nv = Db.Query(@"SELECT n.TrangThai, n.NgayNghi, COALESCE(n.NgayChinhThuc, q.NgayBatDau) AS NgayVao
                                    FROM NhanVien n LEFT JOIN QuyetDinhTuyenDung q ON q.MaNV = n.MaNV WHERE n.MaNV = @m", Db.P("@m", maNV));
                if (nv.Rows.Count == 0) return KetQuaXuLy.Fail("Nhân viên không tồn tại.");
                var r = nv.Rows[0];
                if (r["NgayVao"] != DBNull.Value && ngay < Convert.ToDateTime(r["NgayVao"]).Date)
                    return KetQuaXuLy.Fail("Ngày chấm công trước ngày nhân viên vào làm (" + Convert.ToDateTime(r["NgayVao"]).ToString("dd/MM/yyyy") + ").");
                if (r["NgayNghi"] != DBNull.Value && ngay > Convert.ToDateTime(r["NgayNghi"]).Date) return KetQuaXuLy.Fail("Nhân viên đã nghỉ việc từ " + Convert.ToDateTime(r["NgayNghi"]).ToString("dd/MM/yyyy") + ".");
                bool co = Db.Scalar("SELECT 1 FROM ChamCong WHERE MaNV = @m AND Ngay = @d", Db.P("@m", maNV), Db.P("@d", ngay)) != null;
                if (co)
                    Db.Execute("UPDATE ChamCong SET TrangThai = @t, SoGioLam = @g, MaCa = @c, GhiChu = @n WHERE MaNV = @m AND Ngay = @d",
                        Db.P("@t", trangThai), Db.P("@g", soGio), Db.P("@c", maCa), Db.P("@n", ghiChu), Db.P("@m", maNV), Db.P("@d", ngay));
                else
                    Db.Execute("INSERT INTO ChamCong(MaNV, Ngay, TrangThai, SoGioLam, MaCa, GhiChu) VALUES(@m, @d, @t, @g, @c, @n)",
                        Db.P("@m", maNV), Db.P("@d", ngay), Db.P("@t", trangThai), Db.P("@g", soGio), Db.P("@c", maCa), Db.P("@n", ghiChu));
                return KetQuaXuLy.Ok((co ? "Đã cập nhật" : "Đã ghi") + " chấm công ngày " + ngay.ToString("dd/MM/yyyy") + ".");
            }
            catch (Exception ex) { return KetQuaXuLy.TuLoi(ex); }
        }

        public DataTable BangChamCong(int thang, int nam)
        {
            return Db.Query(@"SELECT n.MaNV, n.HoTen, p.TenPB,
                                     SUM(CASE WHEN c.TrangThai = 'DILAM' THEN 1 ELSE 0 END) AS NgayDiLam,
                                     SUM(CASE WHEN c.TrangThai = 'NGHIPHEP' THEN 1 ELSE 0 END) AS NghiPhep,
                                     SUM(CASE WHEN c.TrangThai = 'NGHIKHONGPHEP' THEN 1 ELSE 0 END) AS NghiKhongPhep,
                                     ISNULL(SUM(c.SoGioLam), 0) AS TongGio
                              FROM NhanVien n JOIN PhongBan p ON p.MaPB = n.MaPB
                                   LEFT JOIN ChamCong c ON c.MaNV = n.MaNV AND YEAR(c.Ngay) = @n AND MONTH(c.Ngay) = @t
                              GROUP BY n.MaNV, n.HoTen, p.TenPB HAVING COUNT(c.MaCC) > 0 ORDER BY n.MaNV",
                Db.P("@n", nam), Db.P("@t", thang));
        }
    }
}
