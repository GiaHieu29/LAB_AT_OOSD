using System;
using System.Data;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.Services
{
    /// <summary>Danh mục: phòng ban, chức vụ, ca làm việc.</summary>
    public class DanhMucService
    {
        public DataTable LayPhongBan()
        {
            return Db.Query(@"SELECT p.MaPB, p.TenPB, p.MoTa, p.NhuCauTuyen, t.HoTen AS TruongPhong,
                                     (SELECT COUNT(*) FROM NhanVien n WHERE n.MaPB = p.MaPB AND n.TrangThai <> 'NGHIVIEC') AS SoNhanVien
                              FROM PhongBan p LEFT JOIN NhanVien t ON t.MaNV = p.MaTruongPhong ORDER BY p.MaPB");
        }
        public DataTable LayPhongBanCombo() { return Db.Query("SELECT MaPB, TenPB FROM PhongBan ORDER BY TenPB"); }
        public DataTable LayPhongBanCoNhuCau()
        {
            return Db.Query(@"SELECT MaPB, TenPB + N' (cần ' + CAST(NhuCauTuyen AS nvarchar(10)) + N' người)' AS HienThi
                              FROM PhongBan WHERE NhuCauTuyen > 0 ORDER BY TenPB");
        }
        public DataTable LayChucVu() { return Db.Query("SELECT MaCV, TenCV, PhuCapMacDinh FROM ChucVu ORDER BY MaCV"); }
        public DataTable LayChucVuCombo() { return Db.Query("SELECT MaCV, TenCV, PhuCapMacDinh FROM ChucVu ORDER BY TenCV"); }
        public DataTable LayCa()
        {
            return Db.Query(@"SELECT MaCa, TenCa, CONVERT(varchar(5), GioBatDau, 108) AS GioBatDau, CONVERT(varchar(5), GioKetThuc, 108) AS GioKetThuc
                              FROM CaLamViec ORDER BY GioBatDau");
        }
        public DataTable LayCaCombo()
        {
            return Db.Query(@"SELECT MaCa, TenCa + N' (' + CONVERT(varchar(5), GioBatDau, 108) + N' - ' + CONVERT(varchar(5), GioKetThuc, 108) + N')' AS HienThi
                              FROM CaLamViec ORDER BY GioBatDau");
        }

        public KetQuaXuLy ThemPhongBan(string ma, string ten, string moTa, int nhuCau)
        {
            if (string.IsNullOrWhiteSpace(ma) || string.IsNullOrWhiteSpace(ten)) return KetQuaXuLy.Fail("Mã phòng ban và tên phòng ban không được để trống.");
            return Chay("INSERT INTO PhongBan(MaPB, TenPB, MoTa, NhuCauTuyen) VALUES(@m, @t, @d, @n)", "Đã thêm phòng ban.",
                Db.P("@m", ma), Db.P("@t", ten), Db.P("@d", moTa), Db.P("@n", nhuCau));
        }

        public KetQuaXuLy CapNhatNhuCau(string maPB, int soLuong)
        {
            if (string.IsNullOrWhiteSpace(maPB)) return KetQuaXuLy.Fail("Hãy chọn phòng ban trên lưới.");
            if (soLuong < 0) return KetQuaXuLy.Fail("Nhu cầu tuyển dụng không được âm.");
            return Chay("UPDATE PhongBan SET NhuCauTuyen = @n WHERE MaPB = @p", "Đã cập nhật nhu cầu tuyển dụng.", Db.P("@n", soLuong), Db.P("@p", maPB));
        }

        public KetQuaXuLy DatTruongPhong(string maPB, string maNV)
        {
            if (string.IsNullOrWhiteSpace(maPB) || string.IsNullOrWhiteSpace(maNV)) return KetQuaXuLy.Fail("Hãy chọn phòng ban và nhân viên.");
            try
            {
                var dt = Db.Query("SELECT MaPB, TrangThai FROM NhanVien WHERE MaNV = @m", Db.P("@m", maNV));
                if (dt.Rows.Count == 0) return KetQuaXuLy.Fail("Nhân viên không tồn tại.");
                if (Convert.ToString(dt.Rows[0]["MaPB"]) != maPB) return KetQuaXuLy.Fail("Trưởng phòng phải là nhân viên thuộc chính phòng ban đó.");
                if (Convert.ToString(dt.Rows[0]["TrangThai"]) != QuyDinh.ChinhThuc) return KetQuaXuLy.Fail("Chỉ nhân viên chính thức mới được làm trưởng phòng.");
                Db.Execute("UPDATE PhongBan SET MaTruongPhong = @m WHERE MaPB = @p", Db.P("@m", maNV), Db.P("@p", maPB));
                return KetQuaXuLy.Ok("Đã đặt trưởng phòng.");
            }
            catch (Exception ex) { return KetQuaXuLy.TuLoi(ex); }
        }

        public KetQuaXuLy ThemChucVu(string ma, string ten, decimal phuCap)
        {
            if (string.IsNullOrWhiteSpace(ma) || string.IsNullOrWhiteSpace(ten)) return KetQuaXuLy.Fail("Mã và tên chức vụ không được để trống.");
            if (phuCap < 0) return KetQuaXuLy.Fail("Phụ cấp không được âm.");
            return Chay("INSERT INTO ChucVu(MaCV, TenCV, PhuCapMacDinh) VALUES(@m, @t, @p)", "Đã thêm chức vụ.", Db.P("@m", ma), Db.P("@t", ten), Db.P("@p", phuCap));
        }

        public KetQuaXuLy ThemCa(string ma, string ten, string gioBD, string gioKT)
        {
            if (string.IsNullOrWhiteSpace(ma) || string.IsNullOrWhiteSpace(ten)) return KetQuaXuLy.Fail("Mã ca và tên ca không được để trống.");
            TimeSpan a, b;
            if (!TimeSpan.TryParse((gioBD ?? "").Trim(), out a) || !TimeSpan.TryParse((gioKT ?? "").Trim(), out b) || a.TotalHours >= 24 || b.TotalHours >= 24)
                return KetQuaXuLy.Fail("Giờ bắt đầu và giờ kết thúc nhập dạng HH:mm, ví dụ 07:00.");
            if (a == b) return KetQuaXuLy.Fail("Giờ bắt đầu và giờ kết thúc không được trùng nhau.");
            return Chay("INSERT INTO CaLamViec(MaCa, TenCa, GioBatDau, GioKetThuc) VALUES(@m, @t, @a, @b)", "Đã thêm ca làm việc.",
                Db.P("@m", ma), Db.P("@t", ten), new System.Data.SqlClient.SqlParameter("@a", SqlDbType.Time) { Value = a }, new System.Data.SqlClient.SqlParameter("@b", SqlDbType.Time) { Value = b });
        }

        private static KetQuaXuLy Chay(string sql, string thongBao, params System.Data.SqlClient.SqlParameter[] ps)
        {
            try { Db.Execute(sql, ps); return KetQuaXuLy.Ok(thongBao); }
            catch (Exception ex) { return KetQuaXuLy.TuLoi(ex); }
        }
    }
}
