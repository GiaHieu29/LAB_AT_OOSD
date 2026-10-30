
using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyChoThueVanPhong.Data;

namespace QuanLyChoThueVanPhong.Services
{
    public class KiemTraPhongService
    {
        // ========================================
        // 1. DANH SÁCH PHÒNG
        // ========================================
        public DataTable LayPhong()
        {
            return Db.Query(@"
                SELECT
                    p.MaPhong,
                    p.MaPhong + N' - ' + p.TenPhong
                    + N' (' + t.TenTang + N')' AS HienThi
                FROM dbo.Phong p
                INNER JOIN dbo.Tang t
                    ON p.MaTang = t.MaTang
                ORDER BY t.SoThuTu, p.MaPhong");
        }

        // ========================================
        // 2. DANH SÁCH PHIẾU / TÌM KIẾM
        // ========================================
        public DataTable LayDanhSach(string tuKhoa = "")
        {
            return Db.Query(@"
                SELECT
                    kt.MaKiemTra,
                    kt.MaPhong,
                    p.TenPhong,
                    t.TenTang,
                    kt.NgayKiemTra,
                    kt.TinhTrang,
                    kt.NoiDung,
                    kt.NguoiKiemTra,
                    kt.DeXuatSuaChua,
                    (
                        SELECT COUNT(*)
                        FROM dbo.SuaChuaPhong sc
                        WHERE sc.MaKiemTra = kt.MaKiemTra
                    ) AS SoLanSuaChua
                FROM dbo.KiemTraPhong kt
                INNER JOIN dbo.Phong p
                    ON kt.MaPhong = p.MaPhong
                INNER JOIN dbo.Tang t
                    ON p.MaTang = t.MaTang
                WHERE kt.MaKiemTra LIKE @Key
                   OR kt.MaPhong LIKE @Key
                   OR p.TenPhong LIKE @Key
                   OR t.TenTang LIKE @Key
                   OR kt.TinhTrang LIKE @Key
                   OR kt.NguoiKiemTra LIKE @Key
                   OR kt.NoiDung LIKE @Key
                ORDER BY kt.NgayKiemTra DESC,
                         kt.MaKiemTra DESC",
                Db.P("@Key",
                    "%" + (tuKhoa ?? "").Trim() + "%"));
        }

        // ========================================
        // 3. ĐẾM PHIẾU TRONG NĂM
        // ========================================
        public int DemKiemTraTrongNam(
            string maPhong, int nam,
            string maKiemTraBoQua = "")
        {
            return Convert.ToInt32(Db.Scalar(@"
                SELECT COUNT(*)
                FROM dbo.KiemTraPhong
                WHERE MaPhong = @Phong
                  AND NgayKiemTra >= @DauNam
                  AND NgayKiemTra < @DauNamSau
                  AND MaKiemTra <> @BoQua",
                Db.P("@Phong", maPhong),
                Db.P("@DauNam",
                    new DateTime(nam, 1, 1)),
                Db.P("@DauNamSau",
                    new DateTime(nam + 1, 1, 1)),
                Db.P("@BoQua",
                    maKiemTraBoQua ?? "")));
        }

        // ========================================
        // 4. KIỂM TRA THÔNG TIN
        // ========================================
        private static void KiemTraDuLieu(
            string ma, string phong,
            DateTime ngay, string tinhTrang,
            string noiDung, string nguoi,
            string deXuat)
        {
            if (string.IsNullOrWhiteSpace(ma) ||
                ma.Trim().Length > 20)
                throw new Exception(
                    "Mã kiểm tra phải từ 1 đến 20 ký tự.");

            if (string.IsNullOrWhiteSpace(phong))
                throw new Exception(
                    "Hãy chọn phòng cần kiểm tra.");

            if (ngay.Date > DateTime.Today)
                throw new Exception(
                    "Không được lập phiếu kiểm tra " +
                    "với ngày trong tương lai.");

            if (string.IsNullOrWhiteSpace(tinhTrang))
                throw new Exception(
                    "Hãy chọn tình trạng phòng.");

            if (string.IsNullOrWhiteSpace(noiDung))
                throw new Exception(
                    "Hãy nhập nội dung kiểm tra.");

            if (string.IsNullOrWhiteSpace(nguoi))
                throw new Exception(
                    "Hãy nhập người kiểm tra.");

            if (noiDung.Length > 2000 ||
                deXuat.Length > 2000)
                throw new Exception(
                    "Nội dung hoặc đề xuất quá dài.");
        }

        private static object NullNeuRong(string value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? (object)DBNull.Value
                : value.Trim();
        }

        // ========================================
        // 5. THÊM PHIẾU KIỂM TRA
        // ========================================
        public void Them(
            string ma, string phong,
            DateTime ngay, string tinhTrang,
            string noiDung, string nguoi,
            string deXuat)
        {
            KiemTraDuLieu(ma, phong, ngay,
                tinhTrang, noiDung, nguoi, deXuat);

            Db.Execute(@"
                INSERT INTO dbo.KiemTraPhong
                (
                    MaKiemTra,
                    MaPhong,
                    NgayKiemTra,
                    TinhTrang,
                    NoiDung,
                    NguoiKiemTra,
                    DeXuatSuaChua
                )
                VALUES
                (
                    @Ma,
                    @Phong,
                    @Ngay,
                    @TinhTrang,
                    @NoiDung,
                    @Nguoi,
                    @DeXuat
                )",
                Db.P("@Ma", ma.Trim()),
                Db.P("@Phong", phong.Trim()),
                Db.P("@Ngay", ngay.Date),
                Db.P("@TinhTrang", tinhTrang.Trim()),
                Db.P("@NoiDung", noiDung.Trim()),
                Db.P("@Nguoi", nguoi.Trim()),
                Db.P("@DeXuat", NullNeuRong(deXuat)));
        }

        // ========================================
        // 6. SỬA PHIẾU KIỂM TRA
        // ========================================
        public void Sua(
            string ma, string phong,
            DateTime ngay, string tinhTrang,
            string noiDung, string nguoi,
            string deXuat)
        {
            KiemTraDuLieu(ma, phong, ngay,
                tinhTrang, noiDung, nguoi, deXuat);

            using (SqlConnection conn = Db.OpenConnection())
            using (SqlTransaction tran =
                conn.BeginTransaction(
                    IsolationLevel.Serializable))
            {
                try
                {
                    // Nếu phiếu đã được sử dụng để
                    // lập sửa chữa thì không cho đổi phòng.
                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT MaPhong
                        FROM dbo.KiemTraPhong
                            WITH (UPDLOCK, HOLDLOCK)
                        WHERE MaKiemTra = @Ma",
                        conn, tran))
                    {
                        cmd.Parameters.AddWithValue(
                            "@Ma", ma.Trim());

                        object cu = cmd.ExecuteScalar();

                        if (cu == null)
                            throw new Exception(
                                "Không tìm thấy phiếu cần sửa.");

                        if (Convert.ToString(cu) !=
                            phong.Trim())
                        {
                            using (SqlCommand check =
                                new SqlCommand(@"
                                SELECT COUNT(*)
                                FROM dbo.SuaChuaPhong
                                WHERE MaKiemTra = @Ma",
                                conn, tran))
                            {
                                check.Parameters.AddWithValue(
                                    "@Ma", ma.Trim());

                                if (Convert.ToInt32(
                                    check.ExecuteScalar()) > 0)
                                    throw new Exception(
                                        "Phiếu đã liên kết " +
                                        "với sửa chữa. " +
                                        "Không thể đổi phòng.");
                            }
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand(@"
                        UPDATE dbo.KiemTraPhong
                        SET MaPhong = @Phong,
                            NgayKiemTra = @Ngay,
                            TinhTrang = @TinhTrang,
                            NoiDung = @NoiDung,
                            NguoiKiemTra = @Nguoi,
                            DeXuatSuaChua = @DeXuat
                        WHERE MaKiemTra = @Ma",
                        conn, tran))
                    {
                        cmd.Parameters.AddWithValue(
                            "@Ma", ma.Trim());
                        cmd.Parameters.AddWithValue(
                            "@Phong", phong.Trim());
                        cmd.Parameters.AddWithValue(
                            "@Ngay", ngay.Date);
                        cmd.Parameters.AddWithValue(
                            "@TinhTrang", tinhTrang.Trim());
                        cmd.Parameters.AddWithValue(
                            "@NoiDung", noiDung.Trim());
                        cmd.Parameters.AddWithValue(
                            "@Nguoi", nguoi.Trim());
                        cmd.Parameters.AddWithValue(
                            "@DeXuat", NullNeuRong(deXuat));

                        int n = cmd.ExecuteNonQuery();

                        if (n == 0)
                            throw new Exception(
                                "Không tìm thấy phiếu.");
                    }

                    tran.Commit();
                }
                catch
                {
                    if (tran.Connection != null)
                        tran.Rollback();

                    throw;
                }
            }
        }

        // ========================================
        // 7. XÓA PHIẾU KIỂM TRA
        // ========================================
        public void Xoa(string ma)
        {
            if (string.IsNullOrWhiteSpace(ma))
                throw new Exception(
                    "Hãy chọn phiếu cần xóa.");

            using (SqlConnection conn = Db.OpenConnection())
            using (SqlTransaction tran =
                conn.BeginTransaction(
                    IsolationLevel.Serializable))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT MaKiemTra
                        FROM dbo.KiemTraPhong
                            WITH (UPDLOCK, HOLDLOCK)
                        WHERE MaKiemTra = @Ma",
                        conn, tran))
                    {
                        cmd.Parameters.AddWithValue(
                            "@Ma", ma.Trim());

                        if (cmd.ExecuteScalar() == null)
                            throw new Exception(
                                "Phiếu không tồn tại.");
                    }

                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT COUNT(*)
                        FROM dbo.SuaChuaPhong
                        WHERE MaKiemTra = @Ma",
                        conn, tran))
                    {
                        cmd.Parameters.AddWithValue(
                            "@Ma", ma.Trim());

                        int count = Convert.ToInt32(
                            cmd.ExecuteScalar());

                        if (count > 0)
                            throw new Exception(
                                "Phiếu đã được liên kết với " +
                                "công việc sửa chữa.\n" +
                                "Không thể xóa phiếu này.");
                    }

                    using (SqlCommand cmd = new SqlCommand(@"
                        DELETE FROM dbo.KiemTraPhong
                        WHERE MaKiemTra = @Ma",
                        conn, tran))
                    {
                        cmd.Parameters.AddWithValue(
                            "@Ma", ma.Trim());

                        if (cmd.ExecuteNonQuery() != 1)
                            throw new Exception(
                                "Không xóa được phiếu.");
                    }

                    tran.Commit();
                }
                catch
                {
                    if (tran.Connection != null)
                        tran.Rollback();

                    throw;
                }
            }
        }
    }
}
