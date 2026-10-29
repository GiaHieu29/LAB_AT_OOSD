using System;
using System.Data;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.Services
{
    /// <summary>Ký hợp đồng chính thức (dài hạn, ngắn hạn) và hồ sơ bảo hiểm BHYT, BHXH.</summary>
    public class HopDongService
    {
        public DataTable LayHopDong()
        {
            return Db.Query(@"SELECT h.SoHD, h.MaNV, n.HoTen,
                                     CASE h.LoaiHD WHEN 'DAIHAN' THEN N'Dài hạn' ELSE N'Ngắn hạn' END AS LoaiHD,
                                     h.NgayKy, h.NgayBatDau, h.NgayKetThuc,
                                     CASE WHEN h.NgayKetThuc IS NULL OR h.NgayKetThuc >= CAST(GETDATE() AS date) THEN N'Còn hiệu lực' ELSE N'Hết hạn' END AS TinhTrang,
                                     h.GhiChu
                              FROM HopDong h JOIN NhanVien n ON n.MaNV = h.MaNV ORDER BY h.NgayKy DESC, h.SoHD DESC");
        }

        /// <summary>Nhân viên thử việc, hoặc chính thức nhưng chưa có hợp đồng còn hiệu lực.</summary>
        public DataTable LayNhanVienCoTheKy()
        {
            return Db.Query(@"SELECT n.MaNV,
                                     n.MaNV + N' - ' + n.HoTen + CASE n.TrangThai
                                         WHEN 'THUVIEC' THEN N' (thử việc đến ' + CONVERT(varchar(10), q.NgayKetThuc, 103) + N')'
                                         ELSE N' (chính thức, chưa có hợp đồng còn hiệu lực)' END AS HienThi
                              FROM NhanVien n LEFT JOIN QuyetDinhTuyenDung q ON q.MaNV = n.MaNV
                              WHERE n.TrangThai = 'THUVIEC'
                                 OR (n.TrangThai = 'CHINHTHUC' AND NOT EXISTS (SELECT 1 FROM HopDong h WHERE h.MaNV = n.MaNV
                                        AND (h.NgayKetThuc IS NULL OR h.NgayKetThuc >= CAST(GETDATE() AS date))))
                              ORDER BY n.MaNV");
        }

        public KetQuaXuLy KyHopDong(string soHD, string maNV, string loai, DateTime ngayKy, DateTime ngayBatDau, DateTime ngayKetThuc, string ghiChu)
        {
            if (string.IsNullOrWhiteSpace(soHD) || string.IsNullOrWhiteSpace(maNV)) return KetQuaXuLy.Fail("Số hợp đồng và nhân viên không được để trống.");
            if (loai != QuyDinh.DaiHan && loai != QuyDinh.NganHan) return KetQuaXuLy.Fail("Loại hợp đồng phải là dài hạn hoặc ngắn hạn.");
            if (ngayBatDau.Date < ngayKy.Date) return KetQuaXuLy.Fail("Ngày bắt đầu hợp đồng không được trước ngày ký.");
            if (loai == QuyDinh.NganHan)
            {
                if (ngayKetThuc.Date <= ngayBatDau.Date) return KetQuaXuLy.Fail("Hợp đồng ngắn hạn phải có ngày kết thúc sau ngày bắt đầu.");
                if (ngayKetThuc.Date > ngayBatDau.Date.AddMonths(36)) return KetQuaXuLy.Fail("Hợp đồng ngắn hạn không được quá 36 tháng.");
            }
            try
            {
                var nv = Db.Query("SELECT TrangThai FROM NhanVien WHERE MaNV = @m", Db.P("@m", maNV));
                if (nv.Rows.Count == 0) return KetQuaXuLy.Fail("Nhân viên không tồn tại.");
                string tt = Convert.ToString(nv.Rows[0]["TrangThai"]);
                if (tt == QuyDinh.NghiViec) return KetQuaXuLy.Fail("Nhân viên đã nghỉ việc, không thể ký hợp đồng.");
                bool thuViec = tt == QuyDinh.ThuViec;
                if (thuViec)
                {
                    object kt = Db.Scalar("SELECT NgayKetThuc FROM QuyetDinhTuyenDung WHERE MaNV = @m", Db.P("@m", maNV));
                    if (kt == null) return KetQuaXuLy.Fail("Nhân viên chưa có quyết định tuyển dụng.");
                    DateTime hetThuViec = Convert.ToDateTime(kt).Date;
                    if (ngayKy.Date < hetThuViec) return KetQuaXuLy.Fail("Nhân viên còn thử việc đến " + hetThuViec.ToString("dd/MM/yyyy") + ". Chỉ ký hợp đồng từ ngày đó trở đi.");
                }
                if (Db.Scalar("SELECT 1 FROM HopDong WHERE SoHD = @s", Db.P("@s", soHD)) != null) return KetQuaXuLy.Fail("Số hợp đồng đã tồn tại.");
                if (Db.Scalar("SELECT 1 FROM HopDong WHERE MaNV = @m AND (NgayKetThuc IS NULL OR NgayKetThuc >= @bd)", Db.P("@m", maNV), Db.P("@bd", ngayBatDau.Date)) != null)
                    return KetQuaXuLy.Fail("Nhân viên đã có hợp đồng còn hiệu lực trong khoảng thời gian này.");

                using (var cn = Db.OpenConnection())
                using (var tr = cn.BeginTransaction())
                {
                    try
                    {
                        Db.Exec(cn, tr, "INSERT INTO HopDong(SoHD, MaNV, LoaiHD, NgayKy, NgayBatDau, NgayKetThuc, GhiChu) VALUES(@s, @m, @l, @k, @bd, @kt, @g)",
                            Db.P("@s", soHD), Db.P("@m", maNV), Db.P("@l", loai), Db.P("@k", ngayKy.Date), Db.P("@bd", ngayBatDau.Date),
                            Db.P("@kt", loai == QuyDinh.NganHan ? (object)ngayKetThuc.Date : null), Db.P("@g", ghiChu));
                        if (thuViec)
                            Db.Exec(cn, tr, "UPDATE NhanVien SET TrangThai = 'CHINHTHUC', NgayChinhThuc = @d WHERE MaNV = @m", Db.P("@d", ngayBatDau.Date), Db.P("@m", maNV));
                        tr.Commit();
                        return KetQuaXuLy.Ok(thuViec
                            ? "Đã ký hợp đồng " + soHD + ". Nhân viên chuyển sang chính thức; hãy lập quyết định lương và chức vụ" + (loai == QuyDinh.DaiHan ? ", làm hồ sơ bảo hiểm." : ".")
                            : "Đã ký hợp đồng " + soHD + ".");
                    }
                    catch (Exception ex) { try { tr.Rollback(); } catch { } return KetQuaXuLy.TuLoi(ex); }
                }
            }
            catch (Exception ex) { return KetQuaXuLy.TuLoi(ex); }
        }

        public string MoTaNhanVien(string maNV)
        {
            if (string.IsNullOrEmpty(maNV)) return "";
            var dt = Db.Query(@"SELECT n.TrangThai, q.NgayBatDau, q.NgayKetThuc, q.LuongThuViec FROM NhanVien n
                                LEFT JOIN QuyetDinhTuyenDung q ON q.MaNV = n.MaNV WHERE n.MaNV = @m", Db.P("@m", maNV));
            if (dt.Rows.Count == 0) return "";
            var r = dt.Rows[0];
            if (Convert.ToString(r["TrangThai"]) == QuyDinh.ThuViec)
                return "Thử việc " + Convert.ToDateTime(r["NgayBatDau"]).ToString("dd/MM/yyyy") + " - " + Convert.ToDateTime(r["NgayKetThuc"]).ToString("dd/MM/yyyy") +
                       ", lương thử việc " + Convert.ToDecimal(r["LuongThuViec"]).ToString("N0") + " đ";
            return "Nhân viên chính thức, chưa có hợp đồng còn hiệu lực";
        }

        // ---------------- Bảo hiểm ----------------
        public DataTable LayBaoHiem()
        {
            return Db.Query(@"SELECT b.MaBH, b.MaNV, n.HoTen, b.LoaiBH, b.SoSo, b.NgayThamGia, b.MucDong
                              FROM BaoHiem b JOIN NhanVien n ON n.MaNV = b.MaNV ORDER BY b.MaNV, b.LoaiBH");
        }

        public DataTable LayNhanVienChuaCoBaoHiem()
        {
            return Db.Query(@"SELECT n.MaNV, n.MaNV + N' - ' + n.HoTen AS HienThi FROM NhanVien n
                              WHERE n.TrangThai = 'CHINHTHUC'
                                AND EXISTS (SELECT 1 FROM HopDong h WHERE h.MaNV = n.MaNV AND h.LoaiHD = 'DAIHAN')
                                AND NOT EXISTS (SELECT 1 FROM BaoHiem b WHERE b.MaNV = n.MaNV)
                              ORDER BY n.MaNV");
        }

        public KetQuaXuLy LapHoSoBaoHiem(string maNV, string soBHXH, decimal mucBHXH, string soBHYT, decimal mucBHYT, DateTime ngayThamGia)
        {
            if (string.IsNullOrWhiteSpace(maNV)) return KetQuaXuLy.Fail("Hãy chọn nhân viên.");
            if (string.IsNullOrWhiteSpace(soBHXH) || string.IsNullOrWhiteSpace(soBHYT)) return KetQuaXuLy.Fail("Phải nhập số sổ BHXH và số thẻ BHYT.");
            if (mucBHXH <= 0 || mucBHXH > 20 || mucBHYT <= 0 || mucBHYT > 20) return KetQuaXuLy.Fail("Mức đóng phải lớn hơn 0 và không quá 20%.");
            try
            {
                object bd = Db.Scalar("SELECT MIN(NgayBatDau) FROM HopDong WHERE MaNV = @m AND LoaiHD = 'DAIHAN'", Db.P("@m", maNV));
                if (bd == null || bd == DBNull.Value) return KetQuaXuLy.Fail("Chỉ nhân viên có hợp đồng dài hạn mới làm hồ sơ BHYT, BHXH.");
                if (ngayThamGia.Date < Convert.ToDateTime(bd).Date) return KetQuaXuLy.Fail("Ngày tham gia bảo hiểm không được trước ngày bắt đầu hợp đồng dài hạn (" + Convert.ToDateTime(bd).ToString("dd/MM/yyyy") + ").");
                if (Db.Scalar("SELECT 1 FROM BaoHiem WHERE MaNV = @m", Db.P("@m", maNV)) != null) return KetQuaXuLy.Fail("Nhân viên đã có hồ sơ bảo hiểm.");
                using (var cn = Db.OpenConnection())
                using (var tr = cn.BeginTransaction())
                {
                    try
                    {
                        Db.Exec(cn, tr, "INSERT INTO BaoHiem(MaNV, LoaiBH, SoSo, NgayThamGia, MucDong) VALUES(@m, 'BHXH', @s, @d, @c)", Db.P("@m", maNV), Db.P("@s", soBHXH), Db.P("@d", ngayThamGia.Date), Db.P("@c", mucBHXH));
                        Db.Exec(cn, tr, "INSERT INTO BaoHiem(MaNV, LoaiBH, SoSo, NgayThamGia, MucDong) VALUES(@m, 'BHYT', @s, @d, @c)", Db.P("@m", maNV), Db.P("@s", soBHYT), Db.P("@d", ngayThamGia.Date), Db.P("@c", mucBHYT));
                        tr.Commit();
                        return KetQuaXuLy.Ok("Đã lập hồ sơ BHXH và BHYT; hai khoản này sẽ được trích trừ trong bảng lương.");
                    }
                    catch (Exception ex) { try { tr.Rollback(); } catch { } return KetQuaXuLy.TuLoi(ex); }
                }
            }
            catch (Exception ex) { return KetQuaXuLy.TuLoi(ex); }
        }
    }
}
