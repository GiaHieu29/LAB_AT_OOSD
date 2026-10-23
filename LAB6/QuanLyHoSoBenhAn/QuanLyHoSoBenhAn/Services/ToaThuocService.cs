
using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyHoSoBenhAn.Data;

namespace QuanLyHoSoBenhAn.Services
{
    public class ToaThuocService
    {
        // 1. Danh sách toa thuốc
        public DataTable LayDanhSach(string tuKhoa = "")
        {
            return Db.Query(@"
                SELECT
                    tt.MaToaThuoc,
                    tt.MaPhieuKham,
                    bn.HoTen AS TenBenhNhan,
                    tt.MaBacSi,
                    bs.HoTen AS TenBacSi,
                    tt.NgayLap,
                    tt.LoiDan,
                    (SELECT COUNT(*)
                     FROM dbo.ChiTietToaThuoc ct
                     WHERE ct.MaToaThuoc = tt.MaToaThuoc)
                        AS SoLoaiThuoc
                FROM dbo.ToaThuoc tt
                INNER JOIN dbo.PhieuKham pk
                    ON tt.MaPhieuKham = pk.MaPhieuKham
                INNER JOIN dbo.HoSoBenhAn hs
                    ON pk.MaHoSo = hs.MaHoSo
                INNER JOIN dbo.BenhNhan bn
                    ON hs.MaBenhNhan = bn.MaBenhNhan
                INNER JOIN dbo.BacSi bs
                    ON tt.MaBacSi = bs.MaBacSi
                WHERE tt.MaToaThuoc LIKE @Key
                   OR tt.MaPhieuKham LIKE @Key
                   OR bn.HoTen LIKE @Key
                ORDER BY tt.NgayLap DESC, tt.MaToaThuoc",
                Db.P("@Key",
                    "%" + (tuKhoa ?? "").Trim() + "%"));
        }

        // 2. Phiếu khám để chọn
        public DataTable LayPhieuKham()
        {
            return Db.Query(@"
                SELECT
                    pk.MaPhieuKham,
                    pk.MaBacSi,
                    bs.HoTen AS TenBacSi,
                    pk.NgayGioKham,
                    pk.MaPhieuKham + N' - ' + bn.HoTen
                        AS HienThi
                FROM dbo.PhieuKham pk
                INNER JOIN dbo.HoSoBenhAn hs
                    ON pk.MaHoSo = hs.MaHoSo
                INNER JOIN dbo.BenhNhan bn
                    ON hs.MaBenhNhan = bn.MaBenhNhan
                INNER JOIN dbo.BacSi bs
                    ON pk.MaBacSi = bs.MaBacSi
                ORDER BY pk.NgayGioKham DESC");
        }

        // 3. Danh mục thuốc
        public DataTable LayThuoc()
        {
            return Db.Query(@"
                SELECT
                    MaThuoc,
                    TenThuoc,
                    DonViTinh,
                    DangSuDung
                FROM dbo.Thuoc
                WHERE DangSuDung = 1
                ORDER BY TenThuoc");
        }

        // 4. Chi tiết toa thuốc
        public DataTable LayChiTiet(string maToa)
        {
            return Db.Query(@"
                SELECT
                    ct.MaThuoc,
                    t.TenThuoc,
                    t.DonViTinh,
                    ct.SoLuong,
                    ct.LieuDung,
                    ct.CachDung,
                    ct.SoNgayDung
                FROM dbo.ChiTietToaThuoc ct
                INNER JOIN dbo.Thuoc t
                    ON ct.MaThuoc = t.MaThuoc
                WHERE ct.MaToaThuoc = @MaToa
                ORDER BY t.TenThuoc",
                Db.P("@MaToa", maToa));
        }

        // 5. Thêm toa và toàn bộ chi tiết
        public void Them(
            string maToa,
            string maPhieu,
            DateTime ngayLap,
            string loiDan,
            DataTable chiTiet)
        {
            KiemTra(maToa, maPhieu, ngayLap, chiTiet);

            using (SqlConnection conn = Db.OpenConnection())
            using (SqlTransaction tran = conn.BeginTransaction())
            {
                try
                {
                    string maBacSi =
                        LayBacSiPhieuKham(
                            conn, tran, maPhieu);

                    using (SqlCommand cmd = new SqlCommand(@"
                        INSERT INTO dbo.ToaThuoc
                        (
                            MaToaThuoc, MaPhieuKham,
                            MaBacSi, NgayLap, LoiDan
                        )
                        VALUES
                        (
                            @MaToa, @MaPhieu,
                            @MaBS, @NgayLap, @LoiDan
                        )", conn, tran))
                    {
                        cmd.Parameters.AddWithValue(
                            "@MaToa", maToa.Trim());

                        cmd.Parameters.AddWithValue(
                            "@MaPhieu", maPhieu);

                        cmd.Parameters.AddWithValue(
                            "@MaBS", maBacSi);

                        cmd.Parameters.AddWithValue(
                            "@NgayLap", ngayLap.Date);

                        cmd.Parameters.AddWithValue(
                            "@LoiDan", NullNeuRong(loiDan));

                        cmd.ExecuteNonQuery();
                    }

                    LuuChiTiet(conn, tran,
                        maToa.Trim(), chiTiet);

                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        // 6. Cập nhật toa và thay danh sách chi tiết
        public void Sua(
            string maToa,
            string maPhieu,
            DateTime ngayLap,
            string loiDan,
            DataTable chiTiet)
        {
            KiemTra(maToa, maPhieu, ngayLap, chiTiet);

            using (SqlConnection conn = Db.OpenConnection())
            using (SqlTransaction tran = conn.BeginTransaction())
            {
                try
                {
                    string maBacSi =
                        LayBacSiPhieuKham(
                            conn, tran, maPhieu);

                    using (SqlCommand cmd = new SqlCommand(@"
                        UPDATE dbo.ToaThuoc
                        SET MaPhieuKham = @MaPhieu,
                            MaBacSi = @MaBS,
                            NgayLap = @NgayLap,
                            LoiDan = @LoiDan
                        WHERE MaToaThuoc = @MaToa",
                        conn, tran))
                    {
                        cmd.Parameters.AddWithValue(
                            "@MaToa", maToa.Trim());

                        cmd.Parameters.AddWithValue(
                            "@MaPhieu", maPhieu);

                        cmd.Parameters.AddWithValue(
                            "@MaBS", maBacSi);

                        cmd.Parameters.AddWithValue(
                            "@NgayLap", ngayLap.Date);

                        cmd.Parameters.AddWithValue(
                            "@LoiDan", NullNeuRong(loiDan));

                        if (cmd.ExecuteNonQuery() == 0)
                            throw new Exception(
                                "Không tìm thấy toa thuốc.");
                    }

                    using (SqlCommand cmd = new SqlCommand(@"
                        DELETE FROM dbo.ChiTietToaThuoc
                        WHERE MaToaThuoc = @MaToa",
                        conn, tran))
                    {
                        cmd.Parameters.AddWithValue(
                            "@MaToa", maToa.Trim());
                        cmd.ExecuteNonQuery();
                    }

                    LuuChiTiet(conn, tran,
                        maToa.Trim(), chiTiet);

                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        // 7. Xóa toa thuốc
        public void Xoa(string maToa)
        {
            if (string.IsNullOrWhiteSpace(maToa))
                throw new Exception(
                    "Hãy chọn toa thuốc cần xóa.");

            using (SqlConnection conn = Db.OpenConnection())
            using (SqlTransaction tran = conn.BeginTransaction())
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand(@"
                        DELETE FROM dbo.ChiTietToaThuoc
                        WHERE MaToaThuoc = @MaToa",
                        conn, tran))
                    {
                        cmd.Parameters.AddWithValue(
                            "@MaToa", maToa.Trim());
                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = new SqlCommand(@"
                        DELETE FROM dbo.ToaThuoc
                        WHERE MaToaThuoc = @MaToa",
                        conn, tran))
                    {
                        cmd.Parameters.AddWithValue(
                            "@MaToa", maToa.Trim());

                        if (cmd.ExecuteNonQuery() == 0)
                            throw new Exception(
                                "Không tìm thấy toa thuốc.");
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

        // Lấy bác sĩ trực tiếp từ phiếu khám
        private string LayBacSiPhieuKham(
            SqlConnection conn,
            SqlTransaction tran,
            string maPhieu)
        {
            using (SqlCommand cmd = new SqlCommand(@"
                SELECT MaBacSi
                FROM dbo.PhieuKham
                WHERE MaPhieuKham = @MaPhieu",
                conn, tran))
            {
                cmd.Parameters.AddWithValue(
                    "@MaPhieu", maPhieu);

                object result = cmd.ExecuteScalar();

                if (result == null ||
                    result == DBNull.Value)
                    throw new Exception(
                        "Phiếu khám không tồn tại.");

                return Convert.ToString(result);
            }
        }

        // Lưu các dòng thuốc
        private void LuuChiTiet(
            SqlConnection conn,
            SqlTransaction tran,
            string maToa,
            DataTable chiTiet)
        {
            foreach (DataRow row in chiTiet.Rows)
            {
                using (SqlCommand cmd = new SqlCommand(@"
                    INSERT INTO dbo.ChiTietToaThuoc
                    (
                        MaToaThuoc, MaThuoc,
                        SoLuong, LieuDung,
                        CachDung, SoNgayDung
                    )
                    VALUES
                    (
                        @MaToa, @MaThuoc,
                        @SoLuong, @LieuDung,
                        @CachDung, @SoNgay
                    )", conn, tran))
                {
                    cmd.Parameters.AddWithValue(
                        "@MaToa", maToa);

                    cmd.Parameters.AddWithValue(
                        "@MaThuoc",
                        Convert.ToString(row["MaThuoc"]));

                    cmd.Parameters.AddWithValue(
                        "@SoLuong",
                        Convert.ToInt32(row["SoLuong"]));

                    cmd.Parameters.AddWithValue(
                        "@LieuDung",
                        NullNeuRong(Convert.ToString(
                            row["LieuDung"])));

                    cmd.Parameters.AddWithValue(
                        "@CachDung",
                        NullNeuRong(Convert.ToString(
                            row["CachDung"])));

                    object ngay = row["SoNgayDung"];

                    cmd.Parameters.AddWithValue(
                        "@SoNgay",
                        ngay == DBNull.Value
                            ? (object)DBNull.Value
                            : Convert.ToInt32(ngay));

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private static object NullNeuRong(string s)
        {
            return string.IsNullOrWhiteSpace(s)
                ? (object)DBNull.Value : s.Trim();
        }

        private static void KiemTra(
            string maToa,
            string maPhieu,
            DateTime ngayLap,
            DataTable chiTiet)
        {
            if (string.IsNullOrWhiteSpace(maToa) ||
                maToa.Trim().Length > 20)
                throw new Exception(
                    "Mã toa thuốc phải từ 1 đến 20 ký tự.");

            if (string.IsNullOrWhiteSpace(maPhieu))
                throw new Exception(
                    "Vui lòng chọn phiếu khám.");

            if (ngayLap.Date > DateTime.Today)
                throw new Exception(
                    "Ngày lập toa không được ở tương lai.");

            if (chiTiet == null || chiTiet.Rows.Count == 0)
                throw new Exception(
                    "Toa thuốc cần có ít nhất một dòng thuốc.");

            System.Collections.Generic.HashSet<string> daCo =
                new System.Collections.Generic.HashSet<string>(
                    StringComparer.OrdinalIgnoreCase);

            foreach (DataRow r in chiTiet.Rows)
            {
                string maThuoc =
                    Convert.ToString(r["MaThuoc"]);

                if (string.IsNullOrWhiteSpace(maThuoc))
                    throw new Exception(
                        "Mã thuốc không được để trống.");

                if (!daCo.Add(maThuoc))
                    throw new Exception(
                        "Thuốc bị trùng trong toa: " +
                        maThuoc);

                if (Convert.ToInt32(r["SoLuong"]) <= 0)
                    throw new Exception(
                        "Số lượng thuốc phải lớn hơn 0.");

                if (r["SoNgayDung"] != DBNull.Value &&
                    Convert.ToInt32(r["SoNgayDung"]) <= 0)
                    throw new Exception(
                        "Số ngày dùng phải lớn hơn 0.");
            }
        }
    }
}
