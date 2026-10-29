using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QuanLyNhanSu.Services
{
    /// <summary>Kết quả của mọi thao tác nghiệp vụ; Form chỉ đọc ThanhCong và ThongBao.</summary>
    public class KetQuaXuLy
    {
        public bool ThanhCong { get; private set; }
        public string ThongBao { get; private set; }
        public static KetQuaXuLy Ok(string thongBao) { return new KetQuaXuLy { ThanhCong = true, ThongBao = thongBao }; }
        public static KetQuaXuLy Fail(string thongBao) { return new KetQuaXuLy { ThanhCong = false, ThongBao = thongBao }; }

        /// <summary>Đổi lỗi SQL thành thông báo dễ hiểu.</summary>
        public static KetQuaXuLy TuLoi(Exception ex)
        {
            var sql = ex as SqlException;
            if (sql != null)
            {
                if (sql.Number == 2627 || sql.Number == 2601) return Fail("Dữ liệu bị trùng (mã, số quyết định hoặc khóa duy nhất đã tồn tại).");
                if (sql.Number == 547) return Fail("Dữ liệu vi phạm ràng buộc (khóa ngoại hoặc giá trị không hợp lệ).");
            }
            return Fail(ex.Message);
        }
    }

    public class HoSoUngVien
    {
        public string MaNV, HoTen, GioiTinh, DiaChi, DienThoai, CCCD, MaPB;
        public DateTime NgaySinh;
    }
    public class DongPhanCong { public string MaNV, MaCa; public DateTime Ngay; public decimal GioPhuTroi; }
    public class DongPhuTroi { public string MaNV; public decimal SoGio; }

    public class DongLuong
    {
        public string MaNV, HoTen;
        public decimal LuongCoBan, PhuCapLuong, PhuCapDocHai, PhuCapChucVu, NgayCong, LuongNgayCong,
                       GioPhuTroi, TienPhuTroi, TongThuNhap, TruBHXH, TruBHYT, TruPhat, ThucLanh;
        public List<int> PhieuPhat = new List<int>();
    }

    /// <summary>Hằng số và quy định nghiệp vụ của công ty.</summary>
    public static class QuyDinh
    {
        public const string ThuViec = "THUVIEC", ChinhThuc = "CHINHTHUC", NghiViec = "NGHIVIEC";
        public const string DaiHan = "DAIHAN", NganHan = "NGANHAN";
        public const string DiLam = "DILAM", NghiPhep = "NGHIPHEP", NghiKhongPhep = "NGHIKHONGPHEP";
        public const string TangCa = "TANGCA", NgayLe = "NGAYLE", NgayNghi = "NGAYNGHI";
        public const string CaNhan = "CANHAN", TapThe = "TAPTHE";
        public const string ThanhTich = "THANHTICH", CuoiNam = "CUOINAM", Luong13 = "LUONG13";
        public const string PhatTien = "PHATTIEN", SaThai = "SATHAI", KhienTrach = "KHIENTRACH", CanhCao = "CANHCAO";
        public const string NopPhat = "NOPPHAT", TruLuong = "TRULUONG";
        public const string MaChucVuGiamDoc = "GD";
        public const int NgayCongChuan = 26;
        public const decimal GioMotNgay = 8m;
        public const decimal HeSoPhuTroi = 1.5m;          // giờ phụ trội tính bằng 1,5 lần giờ lao động bình thường
        public const decimal LuongToiThieu = 4960000m;    // lương tối thiểu vùng I
        public const decimal MucBHXH = 8m, MucBHYT = 1.5m;
        public static DateTime ThuHaiCuaTuan(DateTime d) { return d.Date.AddDays(-(((int)d.DayOfWeek + 6) % 7)); }
    }
}
