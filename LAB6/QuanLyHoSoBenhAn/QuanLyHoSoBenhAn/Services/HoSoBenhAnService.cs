
using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyHoSoBenhAn.Data;

namespace QuanLyHoSoBenhAn.Services
{
    public class HoSoBenhAnService
    {
        // DANH SÁCH HỒ SƠ
        public DataTable LayDanhSach(string key = "")
        {
            return Db.Query(@"
                SELECT
                    hs.MaHoSo,
                    hs.MaBenhNhan,
                    bn.HoTen,
                    hs.MaPhieuDK,
                    hs.NgayLap,
                    hs.TinhTrangSucKhoe,
                    hs.GhiChu,
                    hs.TrangThai
                FROM dbo.HoSoBenhAn hs
                INNER JOIN dbo.BenhNhan bn
                    ON hs.MaBenhNhan = bn.MaBenhNhan
                WHERE hs.MaHoSo LIKE @Key
                   OR hs.MaBenhNhan LIKE @Key
                   OR bn.HoTen LIKE @Key
                ORDER BY hs.NgayLap DESC, hs.MaHoSo",
                Db.P("@Key", "%" + (key ?? "").Trim() + "%"));
        }

        public DataTable TimKiem(string key)
        {
            return LayDanhSach(key);
        }

        // TẤT CẢ BỆNH NHÂN CHƯA CÓ HỒ SƠ
        // KHÔNG YÊU CẦU ĐĂNG KÝ VÀ LỆ PHÍ TỪ TRƯỚC
        public DataTable LayBenhNhanChuaCoHoSo()
        {
            return Db.Query(@"
                SELECT
                    bn.MaBenhNhan,
                    bn.HoTen,
                    ISNULL(p.MaPhieuDK, '') AS MaPhieuDK
                FROM dbo.BenhNhan bn
                LEFT JOIN dbo.PhieuDangKy p
                    ON p.MaBenhNhan = bn.MaBenhNhan
                WHERE NOT EXISTS
                (
                    SELECT 1
                    FROM dbo.HoSoBenhAn hs
                    WHERE hs.MaBenhNhan = bn.MaBenhNhan
                )
                ORDER BY bn.HoTen");
        }

        // TẠO HỒ SƠ VỚI ĐĂNG KÝ VÀ LỆ PHÍ MẪU
        // Chỉ sử dụng trong database thực hành.
        public void TaoHoSo(
            string maHoSo,
            string maBenhNhan,
            DateTime ngayLap,
            string tinhTrang,
            string ghiChu)
        {
            maHoSo = (maHoSo ?? "").Trim();
            maBenhNhan = (maBenhNhan ?? "").Trim();

            if (maHoSo.Length == 0 || maHoSo.Length > 20)
                throw new Exception(
                    "Mã hồ sơ phải từ 1 đến 20 ký tự.");

            if (maBenhNhan.Length == 0)
                throw new Exception(
                    "Bạn chưa chọn bệnh nhân.");

            if (ngayLap.Date > DateTime.Today)
                throw new Exception(
                    "Ngày lập không được ở tương lai.");

            using (SqlConnection conn = Db.OpenConnection())
            using (SqlTransaction tran = conn.BeginTransaction(
                System.Data.IsolationLevel.Serializable))
            {
                try
                {
                    // Kiểm tra bệnh nhân có tồn tại không
                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT COUNT(*)
                        FROM dbo.BenhNhan
                        WHERE MaBenhNhan = @MaBN",
                        conn, tran))
                    {
                        cmd.Parameters.AddWithValue(
                            "@MaBN", maBenhNhan);

                        if (Convert.ToInt32(
                            cmd.ExecuteScalar()) == 0)
                        {
                            throw new Exception(
                                "Bệnh nhân không tồn tại.");
                        }
                    }

                    // Mỗi bệnh nhân chỉ có một hồ sơ
                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT COUNT(*)
                        FROM dbo.HoSoBenhAn
                        WHERE MaBenhNhan = @MaBN",
                        conn, tran))
                    {
                        cmd.Parameters.AddWithValue(
                            "@MaBN", maBenhNhan);

                        if (Convert.ToInt32(
                            cmd.ExecuteScalar()) > 0)
                        {
                            throw new Exception(
                                "Bệnh nhân đã có hồ sơ bệnh án.");
                        }
                    }

                    // Kiểm tra mã hồ sơ trùng
                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT COUNT(*)
                        FROM dbo.HoSoBenhAn
                        WHERE MaHoSo = @MaHS",
                        conn, tran))
                    {
                        cmd.Parameters.AddWithValue(
                            "@MaHS", maHoSo);

                        if (Convert.ToInt32(
                            cmd.ExecuteScalar()) > 0)
                        {
                            throw new Exception(
                                "Mã hồ sơ đã tồn tại.");
                        }
                    }

                    // 1. TẠO PHIẾU ĐĂNG KÝ NẾU CHƯA CÓ
                    string maPhieuDK = "";

                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT MaPhieuDK
                        FROM dbo.PhieuDangKy
                        WHERE MaBenhNhan = @MaBN",
                        conn, tran))
                    {
                        cmd.Parameters.AddWithValue(
                            "@MaBN", maBenhNhan);

                        object value = cmd.ExecuteScalar();

                        if (value != null && value != DBNull.Value)
                            maPhieuDK = Convert.ToString(value);
                    }

                    if (maPhieuDK == "")
                    {
                        maPhieuDK = TaoMa("PDK");

                        using (SqlCommand cmd = new SqlCommand(@"
                            INSERT INTO dbo.PhieuDangKy
                            (
                                MaPhieuDK,
                                MaBenhNhan,
                                NgayDangKy,
                                NoiDungDangKy,
                                TrangThai
                            )
                            VALUES
                            (
                                @MaPDK,
                                @MaBN,
                                @Ngay,
                                N'Đăng ký hồ sơ bệnh án - dữ liệu mẫu',
                                N'Đã đăng ký'
                            )", conn, tran))
                        {
                            cmd.Parameters.AddWithValue(
                                "@MaPDK", maPhieuDK);

                            cmd.Parameters.AddWithValue(
                                "@MaBN", maBenhNhan);

                            cmd.Parameters.AddWithValue(
                                "@Ngay", ngayLap.Date);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    // 2. GHI NHẬN LỆ PHÍ MẪU NẾU CHƯA CÓ
                    string trangThaiLePhi = null;

                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT TrangThai
                        FROM dbo.LePhiHangNam
                        WHERE MaBenhNhan = @MaBN
                          AND NamDong = @Nam",
                        conn, tran))
                    {
                        cmd.Parameters.AddWithValue(
                            "@MaBN", maBenhNhan);
                        cmd.Parameters.AddWithValue(
                            "@Nam", ngayLap.Year);

                        object value = cmd.ExecuteScalar();

                        if (value != null && value != DBNull.Value)
                            trangThaiLePhi = Convert.ToString(value);
                    }

                    if (trangThaiLePhi == null)
                    {
                        using (SqlCommand cmd = new SqlCommand(@"
                            INSERT INTO dbo.LePhiHangNam
                            (
                                MaLePhi,
                                MaBenhNhan,
                                NamDong,
                                SoTien,
                                NgayDong,
                                TrangThai,
                                GhiChu
                            )
                            VALUES
                            (
                                @MaLP,
                                @MaBN,
                                @Nam,
                                500000,
                                @Ngay,
                                N'Đã thanh toán',
                                N'Lệ phí giả lập phục vụ bài thực hành'
                            )", conn, tran))
                        {
                            cmd.Parameters.AddWithValue(
                                "@MaLP", TaoMa("LP"));

                            cmd.Parameters.AddWithValue(
                                "@MaBN", maBenhNhan);

                            cmd.Parameters.AddWithValue(
                                "@Nam", ngayLap.Year);

                            cmd.Parameters.AddWithValue(
                                "@Ngay", ngayLap.Date);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    else if (trangThaiLePhi != "Đã thanh toán")
                    {
                        throw new Exception(
                            "Bệnh nhân đã có khoản lệ phí " +
                            "chưa được ghi nhận thanh toán. " +
                            "Không tự động đổi trạng thái khoản phí.");
                    }

                    // 3. TẠO HỒ SƠ BỆNH ÁN
                    using (SqlCommand cmd = new SqlCommand(@"
                        INSERT INTO dbo.HoSoBenhAn
                        (
                            MaHoSo,
                            MaBenhNhan,
                            MaPhieuDK,
                            NgayLap,
                            TinhTrangSucKhoe,
                            GhiChu,
                            TrangThai
                        )
                        VALUES
                        (
                            @MaHS,
                            @MaBN,
                            @MaPDK,
                            @Ngay,
                            @TinhTrang,
                            @GhiChu,
                            N'Đang theo dõi'
                        )", conn, tran))
                    {
                        cmd.Parameters.AddWithValue(
                            "@MaHS", maHoSo);

                        cmd.Parameters.AddWithValue(
                            "@MaBN", maBenhNhan);

                        cmd.Parameters.AddWithValue(
                            "@MaPDK", maPhieuDK);

                        cmd.Parameters.AddWithValue(
                            "@Ngay", ngayLap.Date);

                        cmd.Parameters.AddWithValue(
                            "@TinhTrang", NullNeuRong(tinhTrang));

                        cmd.Parameters.AddWithValue(
                            "@GhiChu", NullNeuRong(ghiChu));

                        cmd.ExecuteNonQuery();
                    }

                    // 4. CẬP NHẬT PHIẾU ĐĂNG KÝ
                    using (SqlCommand cmd = new SqlCommand(@"
                        UPDATE dbo.PhieuDangKy
                        SET TrangThai = N'Đã cấp hồ sơ'
                        WHERE MaPhieuDK = @MaPDK",
                        conn, tran))
                    {
                        cmd.Parameters.AddWithValue(
                            "@MaPDK", maPhieuDK);

                        cmd.ExecuteNonQuery();
                    }

                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        // CẬP NHẬT THÔNG TIN HỒ SƠ
        public void CapNhat(
            string maHoSo,
            string tinhTrang,
            string ghiChu,
            string trangThai)
        {
            if (string.IsNullOrWhiteSpace(maHoSo))
                throw new Exception(
                    "Vui lòng chọn hồ sơ cần sửa.");

            if (trangThai != "Đang theo dõi" &&
                trangThai != "Ngừng theo dõi")
            {
                throw new Exception(
                    "Trạng thái hồ sơ không hợp lệ.");
            }

            int n = Db.Execute(@"
                UPDATE dbo.HoSoBenhAn
                SET TinhTrangSucKhoe = @TinhTrang,
                    GhiChu = @GhiChu,
                    TrangThai = @TrangThai
                WHERE MaHoSo = @MaHS",
                Db.P("@TinhTrang", NullNeuRong(tinhTrang)),
                Db.P("@GhiChu", NullNeuRong(ghiChu)),
                Db.P("@TrangThai", trangThai),
                Db.P("@MaHS", maHoSo.Trim()));

            if (n == 0)
                throw new Exception(
                    "Không tìm thấy hồ sơ cần cập nhật.");
        }

        // LỊCH SỬ KHÁM
        public DataTable LayLichSuKham(string maHoSo)
        {
            return Db.Query(@"
                SELECT
                    pk.MaPhieuKham AS [Mã phiếu khám],
                    pk.NgayGioKham AS [Ngày khám],
                    bs.HoTen AS [Bác sĩ],
                    k.TenKhoa AS [Khoa],
                    pk.LoaiKham AS [Loại khám],
                    pk.TrieuChung AS [Triệu chứng],
                    pk.ChanDoan AS [Chẩn đoán],
                    pk.TrangThai AS [Trạng thái]
                FROM dbo.PhieuKham pk
                INNER JOIN dbo.BacSi bs
                    ON pk.MaBacSi = bs.MaBacSi
                INNER JOIN dbo.KhoaDieuTri k
                    ON pk.MaKhoa = k.MaKhoa
                WHERE pk.MaHoSo = @MaHS
                ORDER BY pk.NgayGioKham DESC",
                Db.P("@MaHS", maHoSo));
        }

        private static string TaoMa(string tienTo)
        {
            return tienTo +
                Guid.NewGuid().ToString("N")
                    .Substring(0, 12).ToUpperInvariant();
        }

        private static object NullNeuRong(string text)
        {
            return string.IsNullOrWhiteSpace(text)
                ? (object)DBNull.Value : text.Trim();
        }
    }
}
