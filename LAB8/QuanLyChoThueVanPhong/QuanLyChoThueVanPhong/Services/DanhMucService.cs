
using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyChoThueVanPhong.Data;

namespace QuanLyChoThueVanPhong.Services
{
    public class DanhMucService
    {
        // ========================================
        // DANH MỤC TẦNG
        // ========================================

        public DataTable LayTang(string key = "")
        {
            return Db.Query(@"
                SELECT MaTang, TenTang, SoThuTu, MoTa
                FROM dbo.Tang
                WHERE MaTang LIKE @Key
                   OR TenTang LIKE @Key
                ORDER BY SoThuTu",
                Db.P("@Key", "%" + key.Trim() + "%"));
        }

        public void ThemTang(
            string ma, string ten, int so, string moTa)
        {
            KiemTraMaTen(ma, ten);

            Db.Execute(@"
                INSERT INTO dbo.Tang
                    (MaTang, TenTang, SoThuTu, MoTa)
                VALUES
                    (@Ma, @Ten, @So, @MoTa)",
                Db.P("@Ma", ma.Trim()),
                Db.P("@Ten", ten.Trim()),
                Db.P("@So", so),
                Db.P("@MoTa", NullNeuRong(moTa)));
        }

        public void SuaTang(
            string ma, string ten, int so, string moTa)
        {
            KiemTraMaTen(ma, ten);

            int n = Db.Execute(@"
                UPDATE dbo.Tang
                SET TenTang = @Ten,
                    SoThuTu = @So,
                    MoTa = @MoTa
                WHERE MaTang = @Ma",
                Db.P("@Ma", ma.Trim()),
                Db.P("@Ten", ten.Trim()),
                Db.P("@So", so),
                Db.P("@MoTa", NullNeuRong(moTa)));

            if (n == 0)
                throw new Exception("Không tìm thấy tầng.");
        }

        public void XoaTang(string ma)
        {
            if (string.IsNullOrWhiteSpace(ma))
                throw new Exception("Chưa chọn tầng để xóa.");

            int n = Db.Execute(@"
                DELETE FROM dbo.Tang
                WHERE MaTang = @Ma",
                Db.P("@Ma", ma.Trim()));

            if (n == 0)
                throw new Exception("Không tìm thấy tầng.");
        }

        // ========================================
        // DANH MỤC PHÒNG
        // ========================================

        public DataTable LayPhong(string key = "")
        {
            return Db.Query(@"
                SELECT
                    p.MaPhong,
                    p.MaTang,
                    t.TenTang,
                    p.TenPhong,
                    p.DienTich,
                    p.HuongPhong,
                    p.DonGiaThang,
                    p.TrangThai,
                    p.MoTa
                FROM dbo.Phong p
                INNER JOIN dbo.Tang t
                    ON p.MaTang = t.MaTang
                WHERE p.MaPhong LIKE @Key
                   OR p.TenPhong LIKE @Key
                   OR t.TenTang LIKE @Key
                   OR p.TrangThai LIKE @Key
                ORDER BY t.SoThuTu, p.MaPhong",
                Db.P("@Key", "%" + key.Trim() + "%"));
        }

        public void ThemPhong(
            string ma, string maTang,
            string ten, decimal dienTich,
            string huong, decimal gia,
            string trangThai, string moTa)
        {
            KiemTraPhong(
                ma, maTang, ten, dienTich,
                huong, gia, trangThai);

            Db.Execute(@"
                INSERT INTO dbo.Phong
                (
                    MaPhong, MaTang, TenPhong,
                    DienTich, HuongPhong,
                    DonGiaThang, TrangThai, MoTa
                )
                VALUES
                (
                    @Ma, @MaTang, @Ten,
                    @DienTich, @Huong,
                    @Gia, @TrangThai, @MoTa
                )",
                Db.P("@Ma", ma.Trim()),
                Db.P("@MaTang", maTang),
                Db.P("@Ten", ten.Trim()),
                Db.P("@DienTich", dienTich),
                Db.P("@Huong", huong),
                Db.P("@Gia", gia),
                Db.P("@TrangThai", trangThai),
                Db.P("@MoTa", NullNeuRong(moTa)));
        }

        public void SuaPhong(
            string ma, string maTang,
            string ten, decimal dienTich,
            string huong, decimal gia,
            string trangThai, string moTa)
        {
            KiemTraPhong(
                ma, maTang, ten, dienTich,
                huong, gia, trangThai);

            // Không chuyển phòng đã có hợp đồng
            // sang trạng thái "Trống" một cách tùy ý.
            int dangThue = Convert.ToInt32(Db.Scalar(@"
                SELECT COUNT(*)
                FROM dbo.ChiTietHopDong ct
                INNER JOIN dbo.HopDong hd
                    ON ct.MaHopDong = hd.MaHopDong
                WHERE ct.MaPhong = @Ma
                  AND hd.TrangThai = N'Có hiệu lực'
                  AND CAST(GETDATE() AS DATE)
                      BETWEEN hd.NgayBatDau AND hd.NgayKetThuc
                  AND
                  (
                      ct.NgayTraPhong IS NULL
                      OR ct.NgayTraPhong >=
                         CAST(GETDATE() AS DATE)
                  )",
                Db.P("@Ma", ma.Trim())));

            if (dangThue > 0 && trangThai != "Đang thuê")
                throw new Exception(
                    "Phòng đang có hợp đồng thuê hiệu lực. " +
                    "Không thể đổi trạng thái trực tiếp.");

            int n = Db.Execute(@"
                UPDATE dbo.Phong
                SET MaTang = @MaTang,
                    TenPhong = @Ten,
                    DienTich = @DienTich,
                    HuongPhong = @Huong,
                    DonGiaThang = @Gia,
                    TrangThai = @TrangThai,
                    MoTa = @MoTa
                WHERE MaPhong = @Ma",
                Db.P("@Ma", ma.Trim()),
                Db.P("@MaTang", maTang),
                Db.P("@Ten", ten.Trim()),
                Db.P("@DienTich", dienTich),
                Db.P("@Huong", huong),
                Db.P("@Gia", gia),
                Db.P("@TrangThai", trangThai),
                Db.P("@MoTa", NullNeuRong(moTa)));

            if (n == 0)
                throw new Exception("Không tìm thấy phòng.");
        }

        public void XoaPhong(string ma)
        {
            if (string.IsNullOrWhiteSpace(ma))
                throw new Exception("Chưa chọn phòng để xóa.");

            int n = Db.Execute(@"
                DELETE FROM dbo.Phong
                WHERE MaPhong = @Ma",
                Db.P("@Ma", ma.Trim()));

            if (n == 0)
                throw new Exception("Không tìm thấy phòng.");
        }

        // ========================================
        // DANH MỤC DỊCH VỤ
        // ========================================

        public DataTable LayDichVu(string key = "")
        {
            return Db.Query(@"
                SELECT
                    MaDichVu,
                    TenDichVu,
                    DonViTinh,
                    DonGia,
                    DangSuDung,
                    MoTa
                FROM dbo.LoaiDichVu
                WHERE MaDichVu LIKE @Key
                   OR TenDichVu LIKE @Key
                ORDER BY MaDichVu",
                Db.P("@Key", "%" + key.Trim() + "%"));
        }

        public void ThemDichVu(
            string ma, string ten, string donVi,
            decimal gia, bool dangDung, string moTa)
        {
            KiemTraDichVu(ma, ten, donVi, gia);

            Db.Execute(@"
                INSERT INTO dbo.LoaiDichVu
                (
                    MaDichVu, TenDichVu,
                    DonViTinh, DonGia,
                    DangSuDung, MoTa
                )
                VALUES
                (
                    @Ma, @Ten, @DonVi,
                    @Gia, @DangDung, @MoTa
                )",
                Db.P("@Ma", ma.Trim()),
                Db.P("@Ten", ten.Trim()),
                Db.P("@DonVi", donVi.Trim()),
                Db.P("@Gia", gia),
                Db.P("@DangDung", dangDung),
                Db.P("@MoTa", NullNeuRong(moTa)));
        }

        public void SuaDichVu(
            string ma, string ten, string donVi,
            decimal gia, bool dangDung, string moTa)
        {
            KiemTraDichVu(ma, ten, donVi, gia);

            int n = Db.Execute(@"
                UPDATE dbo.LoaiDichVu
                SET TenDichVu = @Ten,
                    DonViTinh = @DonVi,
                    DonGia = @Gia,
                    DangSuDung = @DangDung,
                    MoTa = @MoTa
                WHERE MaDichVu = @Ma",
                Db.P("@Ma", ma.Trim()),
                Db.P("@Ten", ten.Trim()),
                Db.P("@DonVi", donVi.Trim()),
                Db.P("@Gia", gia),
                Db.P("@DangDung", dangDung),
                Db.P("@MoTa", NullNeuRong(moTa)));

            if (n == 0)
                throw new Exception("Không tìm thấy dịch vụ.");
        }

        public void XoaDichVu(string ma)
        {
            if (string.IsNullOrWhiteSpace(ma))
                throw new Exception("Chưa chọn dịch vụ để xóa.");

            int n = Db.Execute(@"
                DELETE FROM dbo.LoaiDichVu
                WHERE MaDichVu = @Ma",
                Db.P("@Ma", ma.Trim()));

            if (n == 0)
                throw new Exception("Không tìm thấy dịch vụ.");
        }

        // ========================================
        // HÀM DÙNG CHUNG
        // ========================================

        private static void KiemTraMaTen(
            string ma, string ten)
        {
            if (string.IsNullOrWhiteSpace(ma) ||
                ma.Trim().Length > 20)
                throw new Exception(
                    "Mã phải có từ 1 đến 20 ký tự.");

            if (string.IsNullOrWhiteSpace(ten))
                throw new Exception("Tên không được để trống.");
        }

        private static void KiemTraPhong(
            string ma, string maTang, string ten,
            decimal dt, string huong, decimal gia,
            string trangThai)
        {
            KiemTraMaTen(ma, ten);

            if (string.IsNullOrWhiteSpace(maTang))
                throw new Exception("Hãy chọn tầng.");

            if (dt <= 0)
                throw new Exception("Diện tích phải lớn hơn 0.");

            if (gia < 0)
                throw new Exception("Giá thuê không được âm.");

            if (string.IsNullOrWhiteSpace(huong))
                throw new Exception("Hãy chọn hướng phòng.");

            if (trangThai != "Trống" &&
                trangThai != "Đang thuê" &&
                trangThai != "Bảo trì" &&
                trangThai != "Ngừng sử dụng")
                throw new Exception("Trạng thái không hợp lệ.");
        }

        private static void KiemTraDichVu(
            string ma, string ten,
            string donVi, decimal gia)
        {
            KiemTraMaTen(ma, ten);

            if (string.IsNullOrWhiteSpace(donVi))
                throw new Exception("Chưa nhập đơn vị tính.");

            if (gia < 0)
                throw new Exception("Đơn giá không được âm.");
        }

        private static object NullNeuRong(string value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? (object)DBNull.Value
                : value.Trim();
        }
    }
}
