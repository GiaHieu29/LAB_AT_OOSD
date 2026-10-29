using System;
using System.Data;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.Services
{
    /// <summary>Quyết định chức vụ (phụ cấp chức vụ) và quyết định lương (lương cơ bản, phụ cấp, độc hại).</summary>
    public class QuyetDinhService
    {
        public DataTable LayChucVu()
        {
            return Db.Query(@"SELECT q.SoQD, q.MaNV, n.HoTen, c.TenCV, q.NgayHieuLuc, q.PhuCapChucVu, q.GhiChu
                              FROM QuyetDinhChucVu q JOIN NhanVien n ON n.MaNV = q.MaNV JOIN ChucVu c ON c.MaCV = q.MaCV
                              ORDER BY q.NgayHieuLuc DESC, q.SoQD DESC");
        }
        public DataTable LayLuong()
        {
            return Db.Query(@"SELECT q.SoQD, q.MaNV, n.HoTen, q.NgayHieuLuc, q.LuongCoBan, q.PhuCapLuong, q.PhuCapDocHai, q.LyDo
                              FROM QuyetDinhLuong q JOIN NhanVien n ON n.MaNV = q.MaNV ORDER BY q.NgayHieuLuc DESC, q.SoQD DESC");
        }

        public string LayChucVuHienTai(string maNV)
        {
            if (string.IsNullOrEmpty(maNV)) return "";
            var dt = Db.Query(@"SELECT TOP 1 c.TenCV, q.NgayHieuLuc, q.PhuCapChucVu FROM QuyetDinhChucVu q JOIN ChucVu c ON c.MaCV = q.MaCV
                                WHERE q.MaNV = @m ORDER BY q.NgayHieuLuc DESC", Db.P("@m", maNV));
            if (dt.Rows.Count == 0) return "Chưa có quyết định chức vụ";
            return "Hiện tại: " + dt.Rows[0]["TenCV"] + " từ " + Convert.ToDateTime(dt.Rows[0]["NgayHieuLuc"]).ToString("dd/MM/yyyy") +
                   ", phụ cấp " + Convert.ToDecimal(dt.Rows[0]["PhuCapChucVu"]).ToString("N0") + " đ";
        }
        public string LayLuongHienTai(string maNV)
        {
            if (string.IsNullOrEmpty(maNV)) return "";
            var dt = Db.Query("SELECT TOP 1 NgayHieuLuc, LuongCoBan, PhuCapLuong, PhuCapDocHai FROM QuyetDinhLuong WHERE MaNV = @m ORDER BY NgayHieuLuc DESC", Db.P("@m", maNV));
            if (dt.Rows.Count == 0) return "Chưa có quyết định lương";
            return "Hiện tại: lương cơ bản " + Convert.ToDecimal(dt.Rows[0]["LuongCoBan"]).ToString("N0") + " đ từ " + Convert.ToDateTime(dt.Rows[0]["NgayHieuLuc"]).ToString("dd/MM/yyyy");
        }

        private static DataTable NhanVienChinhThuc(string maNV)
        {
            return Db.Query("SELECT TrangThai, NgayChinhThuc FROM NhanVien WHERE MaNV = @m", Db.P("@m", maNV));
        }

        public KetQuaXuLy LapQuyetDinhChucVu(string soQD, string maNV, string maCV, DateTime ngayHieuLuc, decimal phuCap, string ghiChu)
        {
            if (string.IsNullOrWhiteSpace(soQD) || string.IsNullOrWhiteSpace(maNV) || string.IsNullOrWhiteSpace(maCV)) return KetQuaXuLy.Fail("Số quyết định, nhân viên và chức vụ không được để trống.");
            if (phuCap < 0) return KetQuaXuLy.Fail("Phụ cấp chức vụ không được âm.");
            try
            {
                var nv = NhanVienChinhThuc(maNV);
                if (nv.Rows.Count == 0) return KetQuaXuLy.Fail("Nhân viên không tồn tại.");
                if (Convert.ToString(nv.Rows[0]["TrangThai"]) != QuyDinh.ChinhThuc) return KetQuaXuLy.Fail("Chỉ nhân viên đã ký hợp đồng chính thức mới có quyết định chức vụ.");
                if (ngayHieuLuc.Date < Convert.ToDateTime(nv.Rows[0]["NgayChinhThuc"]).Date) return KetQuaXuLy.Fail("Ngày hiệu lực không được trước ngày nhân viên vào chính thức (" + Convert.ToDateTime(nv.Rows[0]["NgayChinhThuc"]).ToString("dd/MM/yyyy") + ").");
                var cur = Db.Query("SELECT TOP 1 MaCV, NgayHieuLuc, PhuCapChucVu FROM QuyetDinhChucVu WHERE MaNV = @m ORDER BY NgayHieuLuc DESC", Db.P("@m", maNV));
                if (cur.Rows.Count > 0)
                {
                    if (ngayHieuLuc.Date <= Convert.ToDateTime(cur.Rows[0]["NgayHieuLuc"]).Date) return KetQuaXuLy.Fail("Ngày hiệu lực phải sau quyết định chức vụ trước đó (" + Convert.ToDateTime(cur.Rows[0]["NgayHieuLuc"]).ToString("dd/MM/yyyy") + ").");
                    if (Convert.ToString(cur.Rows[0]["MaCV"]) == maCV && Convert.ToDecimal(cur.Rows[0]["PhuCapChucVu"]) == phuCap) return KetQuaXuLy.Fail("Chức vụ và phụ cấp không thay đổi so với quyết định hiện hành.");
                }
                if (Db.Scalar("SELECT 1 FROM QuyetDinhChucVu WHERE SoQD = @s", Db.P("@s", soQD)) != null) return KetQuaXuLy.Fail("Số quyết định đã tồn tại.");
                Db.Execute("INSERT INTO QuyetDinhChucVu(SoQD, MaNV, MaCV, NgayHieuLuc, PhuCapChucVu, GhiChu) VALUES(@s, @m, @c, @d, @p, @g)",
                    Db.P("@s", soQD), Db.P("@m", maNV), Db.P("@c", maCV), Db.P("@d", ngayHieuLuc.Date), Db.P("@p", phuCap), Db.P("@g", ghiChu));
                return KetQuaXuLy.Ok("Đã lập quyết định chức vụ " + soQD + ".");
            }
            catch (Exception ex) { return KetQuaXuLy.TuLoi(ex); }
        }

        public KetQuaXuLy LapQuyetDinhLuong(string soQD, string maNV, DateTime ngayHieuLuc, decimal luongCoBan, decimal phuCapLuong, decimal phuCapDocHai, string lyDo)
        {
            if (string.IsNullOrWhiteSpace(soQD) || string.IsNullOrWhiteSpace(maNV)) return KetQuaXuLy.Fail("Số quyết định và nhân viên không được để trống.");
            if (luongCoBan < QuyDinh.LuongToiThieu) return KetQuaXuLy.Fail("Lương cơ bản không được thấp hơn lương tối thiểu vùng (" + QuyDinh.LuongToiThieu.ToString("N0") + " đ).");
            if (phuCapLuong < 0 || phuCapDocHai < 0) return KetQuaXuLy.Fail("Các khoản phụ cấp không được âm.");
            try
            {
                var nv = NhanVienChinhThuc(maNV);
                if (nv.Rows.Count == 0) return KetQuaXuLy.Fail("Nhân viên không tồn tại.");
                if (Convert.ToString(nv.Rows[0]["TrangThai"]) != QuyDinh.ChinhThuc) return KetQuaXuLy.Fail("Chỉ nhân viên đã ký hợp đồng chính thức mới có quyết định lương (thử việc dùng lương thử việc).");
                if (ngayHieuLuc.Date < Convert.ToDateTime(nv.Rows[0]["NgayChinhThuc"]).Date) return KetQuaXuLy.Fail("Ngày hiệu lực không được trước ngày nhân viên vào chính thức (" + Convert.ToDateTime(nv.Rows[0]["NgayChinhThuc"]).ToString("dd/MM/yyyy") + ").");
                object prev = Db.Scalar("SELECT MAX(NgayHieuLuc) FROM QuyetDinhLuong WHERE MaNV = @m", Db.P("@m", maNV));
                if (prev != null && prev != DBNull.Value && ngayHieuLuc.Date <= Convert.ToDateTime(prev).Date)
                    return KetQuaXuLy.Fail("Ngày hiệu lực phải sau quyết định lương trước đó (" + Convert.ToDateTime(prev).ToString("dd/MM/yyyy") + ").");
                if (Db.Scalar("SELECT 1 FROM QuyetDinhLuong WHERE SoQD = @s", Db.P("@s", soQD)) != null) return KetQuaXuLy.Fail("Số quyết định đã tồn tại.");
                Db.Execute("INSERT INTO QuyetDinhLuong(SoQD, MaNV, NgayHieuLuc, LuongCoBan, PhuCapLuong, PhuCapDocHai, LyDo) VALUES(@s, @m, @d, @l, @pl, @dh, @ld)",
                    Db.P("@s", soQD), Db.P("@m", maNV), Db.P("@d", ngayHieuLuc.Date), Db.P("@l", luongCoBan), Db.P("@pl", phuCapLuong), Db.P("@dh", phuCapDocHai), Db.P("@ld", lyDo));
                return KetQuaXuLy.Ok("Đã lập quyết định lương " + soQD + ".");
            }
            catch (Exception ex) { return KetQuaXuLy.TuLoi(ex); }
        }
    }
}
