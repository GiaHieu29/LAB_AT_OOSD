using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.Services
{
    /// <summary>
    /// Bảng lương hằng tháng, tính từ quyết định lương, quyết định chức vụ, bảng chấm công, phiếu phụ trội,
    /// hồ sơ bảo hiểm và phiếu thu nộp phạt trừ lương.
    /// </summary>
    public class BangLuongService
    {
        public class KetQuaTinh { public List<DongLuong> Dong = new List<DongLuong>(); public string Loi; }

        private static decimal Lam(decimal v) { return Math.Round(v, 0, MidpointRounding.AwayFromZero); }

        public DataTable LayBangLuong()
        {
            return Db.Query(@"SELECT b.MaBL, b.Thang, b.Nam, b.NgayLap, b.NguoiLap, COUNT(c.MaNV) AS SoNhanVien, ISNULL(SUM(c.ThucLanh), 0) AS TongThucLanh
                              FROM BangLuong b LEFT JOIN ChiTietLuong c ON c.MaBL = b.MaBL
                              GROUP BY b.MaBL, b.Thang, b.Nam, b.NgayLap, b.NguoiLap ORDER BY b.Nam DESC, b.Thang DESC");
        }

        public DataTable LayChiTiet(string maBL)
        {
            return Db.Query(@"SELECT c.MaNV, n.HoTen, c.LuongCoBan, c.PhuCapLuong, c.PhuCapDocHai, c.PhuCapChucVu, c.NgayCong, c.LuongNgayCong,
                                     c.GioPhuTroi, c.TienPhuTroi, c.TongThuNhap, c.TruBHXH, c.TruBHYT, c.TruPhat, c.ThucLanh
                              FROM ChiTietLuong c JOIN NhanVien n ON n.MaNV = c.MaNV WHERE c.MaBL = @b ORDER BY c.MaNV", Db.P("@b", maBL));
        }

        public static DataTable ToBang(List<DongLuong> ds)
        {
            var dt = new DataTable();
            foreach (var c in new[] { "MaNV", "HoTen" }) dt.Columns.Add(c, typeof(string));
            foreach (var c in new[] { "LuongCoBan", "PhuCapLuong", "PhuCapDocHai", "PhuCapChucVu", "NgayCong", "LuongNgayCong", "GioPhuTroi", "TienPhuTroi", "TongThuNhap", "TruBHXH", "TruBHYT", "TruPhat", "ThucLanh" })
                dt.Columns.Add(c, typeof(decimal));
            foreach (var d in ds)
                dt.Rows.Add(d.MaNV, d.HoTen, d.LuongCoBan, d.PhuCapLuong, d.PhuCapDocHai, d.PhuCapChucVu, d.NgayCong, d.LuongNgayCong, d.GioPhuTroi, d.TienPhuTroi, d.TongThuNhap, d.TruBHXH, d.TruBHYT, d.TruPhat, d.ThucLanh);
            return dt;
        }

        /// <summary>Tính bảng lương nhưng chưa ghi vào CSDL (dùng cho xem trước và cho lập bảng lương).</summary>
        public KetQuaTinh Tinh(int thang, int nam)
        {
            var kq = new KetQuaTinh();
            if (thang < 1 || thang > 12 || nam < 2000 || nam > 2100) { kq.Loi = "Tháng hoặc năm không hợp lệ."; return kq; }
            DateTime dauThang = new DateTime(nam, thang, 1), cuoiThang = dauThang.AddMonths(1).AddDays(-1);
            if (dauThang > new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)) { kq.Loi = "Không lập bảng lương cho tháng chưa tới."; return kq; }
            try
            {
                var nvs = Db.Query(@"SELECT DISTINCT n.MaNV, n.HoTen, n.TrangThai, n.NgayChinhThuc
                                     FROM NhanVien n JOIN ChamCong c ON c.MaNV = n.MaNV WHERE YEAR(c.Ngay) = @n AND MONTH(c.Ngay) = @t ORDER BY n.MaNV",
                    Db.P("@n", nam), Db.P("@t", thang));
                if (nvs.Rows.Count == 0) { kq.Loi = "Tháng " + thang + "/" + nam + " chưa có dữ liệu chấm công."; return kq; }
                var thieu = new List<string>();
                foreach (DataRow r in nvs.Rows)
                {
                    string ma = Convert.ToString(r["MaNV"]);
                    var d = new DongLuong { MaNV = ma, HoTen = Convert.ToString(r["HoTen"]) };
                    bool chinhThuc = Convert.ToString(r["TrangThai"]) == QuyDinh.ChinhThuc && r["NgayChinhThuc"] != DBNull.Value && Convert.ToDateTime(r["NgayChinhThuc"]).Date <= cuoiThang;

                    var l = Db.Query("SELECT TOP 1 LuongCoBan, PhuCapLuong, PhuCapDocHai FROM QuyetDinhLuong WHERE MaNV = @m AND NgayHieuLuc <= @c ORDER BY NgayHieuLuc DESC",
                        Db.P("@m", ma), Db.P("@c", cuoiThang));
                    if (l.Rows.Count > 0)
                    {
                        d.LuongCoBan = Convert.ToDecimal(l.Rows[0]["LuongCoBan"]); d.PhuCapLuong = Convert.ToDecimal(l.Rows[0]["PhuCapLuong"]); d.PhuCapDocHai = Convert.ToDecimal(l.Rows[0]["PhuCapDocHai"]);
                    }
                    else
                    {
                        object tv = Db.Scalar("SELECT LuongThuViec FROM QuyetDinhTuyenDung WHERE MaNV = @m", Db.P("@m", ma));
                        if (chinhThuc || tv == null) { thieu.Add(ma + " - " + d.HoTen); continue; }
                        d.LuongCoBan = Convert.ToDecimal(tv);   // đang thử việc: tính theo lương thử việc
                    }
                    object pc = Db.Scalar("SELECT TOP 1 PhuCapChucVu FROM QuyetDinhChucVu WHERE MaNV = @m AND NgayHieuLuc <= @c ORDER BY NgayHieuLuc DESC", Db.P("@m", ma), Db.P("@c", cuoiThang));
                    d.PhuCapChucVu = pc == null ? 0 : Convert.ToDecimal(pc);

                    int ngay = Convert.ToInt32(Db.Scalar(@"SELECT ISNULL(SUM(CASE WHEN TrangThai IN ('DILAM', 'NGHIPHEP') THEN 1 ELSE 0 END), 0) FROM ChamCong
                                                            WHERE MaNV = @m AND YEAR(Ngay) = @n AND MONTH(Ngay) = @t", Db.P("@m", ma), Db.P("@n", nam), Db.P("@t", thang)));
                    d.NgayCong = Math.Min(ngay, QuyDinh.NgayCongChuan);
                    d.LuongNgayCong = Lam(d.LuongCoBan * d.NgayCong / QuyDinh.NgayCongChuan);

                    d.GioPhuTroi = Convert.ToDecimal(Db.Scalar(@"SELECT ISNULL(SUM(c.SoGio), 0) FROM ChiTietPhuTroi c JOIN PhieuPhuTroi s ON s.SoPhieu = c.SoPhieu
                                                                  WHERE c.MaNV = @m AND YEAR(s.NgayPhuTroi) = @n AND MONTH(s.NgayPhuTroi) = @t", Db.P("@m", ma), Db.P("@n", nam), Db.P("@t", thang)));
                    decimal donGiaGio = d.LuongCoBan / (QuyDinh.NgayCongChuan * QuyDinh.GioMotNgay);
                    d.TienPhuTroi = Lam(d.GioPhuTroi * donGiaGio * QuyDinh.HeSoPhuTroi);

                    d.TongThuNhap = d.LuongNgayCong + d.PhuCapLuong + d.PhuCapDocHai + d.PhuCapChucVu + d.TienPhuTroi;

                    foreach (DataRow b in Db.Query("SELECT LoaiBH, MucDong FROM BaoHiem WHERE MaNV = @m AND NgayThamGia <= @c", Db.P("@m", ma), Db.P("@c", cuoiThang)).Rows)
                    {
                        decimal tien = Lam(d.LuongCoBan * Convert.ToDecimal(b["MucDong"]) / 100m);
                        if (Convert.ToString(b["LoaiBH"]) == "BHXH") d.TruBHXH = tien; else d.TruBHYT = tien;
                    }

                    decimal conLai = d.TongThuNhap - d.TruBHXH - d.TruBHYT;
                    foreach (DataRow p in Db.Query(@"SELECT p.SoPhieu, p.SoTien FROM PhieuThuNopPhat p JOIN QuyetDinhKyLuat q ON q.SoQD = p.SoQD
                                                      WHERE p.MaNV = @m AND p.CachThu = 'TRULUONG' AND p.TrangThai = 'CHUATHU' AND q.NgayQD <= @c ORDER BY p.SoPhieu",
                        Db.P("@m", ma), Db.P("@c", cuoiThang)).Rows)
                    {
                        decimal tien = Convert.ToDecimal(p["SoTien"]);
                        if (tien <= conLai) { d.TruPhat += tien; conLai -= tien; d.PhieuPhat.Add(Convert.ToInt32(p["SoPhieu"])); }   // không đủ thu nhập thì để tháng sau
                    }
                    d.ThucLanh = d.TongThuNhap - d.TruBHXH - d.TruBHYT - d.TruPhat;
                    kq.Dong.Add(d);
                }
                if (thieu.Count > 0) { kq.Loi = "Các nhân viên chính thức sau chưa có quyết định lương: " + string.Join("; ", thieu) + "."; kq.Dong.Clear(); }
            }
            catch (Exception ex) { kq.Loi = ex.Message; kq.Dong.Clear(); }
            return kq;
        }

        public KetQuaXuLy Lap(int thang, int nam, string nguoiLap)
        {
            if (string.IsNullOrWhiteSpace(nguoiLap)) return KetQuaXuLy.Fail("Phải ghi người lập bảng lương.");
            try
            {
                if (Db.Scalar("SELECT 1 FROM BangLuong WHERE Thang = @t AND Nam = @n", Db.P("@t", thang), Db.P("@n", nam)) != null)
                    return KetQuaXuLy.Fail("Đã có bảng lương tháng " + thang + "/" + nam + ".");
                var kq = Tinh(thang, nam);
                if (kq.Loi != null) return KetQuaXuLy.Fail(kq.Loi);
                using (var cn = Db.OpenConnection())
                using (var tr = cn.BeginTransaction())
                {
                    try
                    {
                        int maBL;
                        using (var cmd = new SqlCommand("INSERT INTO BangLuong(Thang, Nam, NgayLap, NguoiLap) OUTPUT INSERTED.MaBL VALUES(@t, @n, SYSDATETIME(), @l)", cn, tr))
                        {
                            cmd.Parameters.Add(Db.P("@t", thang)); cmd.Parameters.Add(Db.P("@n", nam)); cmd.Parameters.Add(Db.P("@l", nguoiLap));
                            maBL = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                        foreach (var d in kq.Dong)
                        {
                            Db.Exec(cn, tr, @"INSERT INTO ChiTietLuong(MaBL, MaNV, LuongCoBan, PhuCapLuong, PhuCapDocHai, PhuCapChucVu, NgayCong, LuongNgayCong, GioPhuTroi, TienPhuTroi, TongThuNhap, TruBHXH, TruBHYT, TruPhat, ThucLanh)
                                              VALUES(@b, @m, @l, @pl, @dh, @cv, @nc, @lnc, @gp, @tp, @tn, @xh, @yt, @ph, @tl)",
                                Db.P("@b", maBL), Db.P("@m", d.MaNV), Db.P("@l", d.LuongCoBan), Db.P("@pl", d.PhuCapLuong), Db.P("@dh", d.PhuCapDocHai), Db.P("@cv", d.PhuCapChucVu),
                                Db.P("@nc", d.NgayCong), Db.P("@lnc", d.LuongNgayCong), Db.P("@gp", d.GioPhuTroi), Db.P("@tp", d.TienPhuTroi), Db.P("@tn", d.TongThuNhap),
                                Db.P("@xh", d.TruBHXH), Db.P("@yt", d.TruBHYT), Db.P("@ph", d.TruPhat), Db.P("@tl", d.ThucLanh));
                            foreach (int so in d.PhieuPhat)
                                Db.Exec(cn, tr, "UPDATE PhieuThuNopPhat SET TrangThai = 'DATHU', NgayThu = @d, ThangTru = @t, NamTru = @n WHERE SoPhieu = @s",
                                    Db.P("@d", DateTime.Today), Db.P("@t", thang), Db.P("@n", nam), Db.P("@s", so));
                        }
                        tr.Commit();
                        decimal tong = 0; foreach (var d in kq.Dong) tong += d.ThucLanh;
                        return KetQuaXuLy.Ok("Đã lập bảng lương tháng " + thang + "/" + nam + " cho " + kq.Dong.Count + " nhân viên, tổng thực lãnh " + tong.ToString("N0") + " đ.");
                    }
                    catch (Exception ex) { try { tr.Rollback(); } catch { } return KetQuaXuLy.TuLoi(ex); }
                }
            }
            catch (Exception ex) { return KetQuaXuLy.TuLoi(ex); }
        }
    }
}
