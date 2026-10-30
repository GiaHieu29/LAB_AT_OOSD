
using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyChoThueVanPhong.Data;

namespace QuanLyChoThueVanPhong.Services
{
    public class DichVuService
    {
        private static SqlParameter P(string name, object value)
        {
            return new SqlParameter(name, value ?? DBNull.Value);
        }

        private static object Scalar(
            SqlConnection c, SqlTransaction t,
            string sql, params SqlParameter[] ps)
        {
            using (var cmd = new SqlCommand(sql, c, t))
            {
                cmd.Parameters.AddRange(ps);
                return cmd.ExecuteScalar();
            }
        }

        private static int Execute(
            SqlConnection c, SqlTransaction t,
            string sql, params SqlParameter[] ps)
        {
            using (var cmd = new SqlCommand(sql, c, t))
            {
                cmd.Parameters.AddRange(ps);
                return cmd.ExecuteNonQuery();
            }
        }

        private static DataTable Query(
            SqlConnection c, SqlTransaction t,
            string sql, params SqlParameter[] ps)
        {
            using (var cmd = new SqlCommand(sql, c, t))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddRange(ps);
                var table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        // ==========================================
        // 1. DANH SÁCH HỢP ĐỒNG
        // ==========================================

        public DataTable LayHopDong()
        {
            return Db.Query(@"
                SELECT h.MaHopDong,
                       h.MaHopDong + N' - ' +
                       k.TenKhachHang AS HienThi
                FROM dbo.HopDong h
                INNER JOIN dbo.KhachHang k
                    ON h.MaKhachHang = k.MaKhachHang
                WHERE h.TrangThai <> N'Dự thảo'
                ORDER BY h.MaHopDong");
        }

        // ==========================================
        // 2. PHÒNG THUÊ THEO HỢP ĐỒNG
        // ==========================================

        public DataTable LayPhong(string maHD)
        {
            return Db.Query(@"
                SELECT ct.MaChiTietHD,
                       ct.MaPhong + N' - ' +
                       p.TenPhong AS HienThi
                FROM dbo.ChiTietHopDong ct
                JOIN dbo.Phong p
                    ON p.MaPhong = ct.MaPhong
                WHERE ct.MaHopDong = @HD
                ORDER BY ct.MaPhong",
                P("@HD", maHD));
        }

        // ==========================================
        // 3. DANH MỤC LOẠI DỊCH VỤ
        // ==========================================

        public DataTable LayLoaiDichVu()
        {
            return Db.Query(@"
                SELECT MaDichVu,
                       MaDichVu + N' - ' +
                       TenDichVu AS HienThi,
                       DonGia,
                       DonViTinh
                FROM dbo.LoaiDichVu
                WHERE DangSuDung = 1
                ORDER BY MaDichVu");
        }

        // ==========================================
        // 4. DANH SÁCH SỬ DỤNG DỊCH VỤ
        // ==========================================

        public DataTable LaySuDung(
            string maHD, int thang, int nam)
        {
            return Db.Query(@"
                SELECT
                    s.MaSuDung,
                    ct.MaHopDong,
                    ct.MaPhong,
                    d.TenDichVu,
                    s.Thang,
                    s.Nam,
                    s.SoLuong,
                    s.DonGiaApDung,
                    CAST(
                        s.SoLuong * s.DonGiaApDung
                        AS DECIMAL(18,2)
                    ) AS ThanhTien,
                    CASE
                        WHEN hd.MaHoaDon IS NULL
                        THEN N'Chưa lập hóa đơn'
                        ELSE N'Đã lập hóa đơn'
                    END AS TrangThai
                FROM dbo.SuDungDichVu s
                JOIN dbo.ChiTietHopDong ct
                    ON ct.MaChiTietHD = s.MaChiTietHD
                JOIN dbo.LoaiDichVu d
                    ON d.MaDichVu = s.MaDichVu
                LEFT JOIN dbo.ChiTietHoaDonDichVu hd
                    ON hd.MaSuDung = s.MaSuDung
                WHERE ct.MaHopDong = @HD
                  AND s.Thang = @Thang
                  AND s.Nam = @Nam
                ORDER BY ct.MaPhong, d.TenDichVu",
                P("@HD", maHD),
                P("@Thang", thang),
                P("@Nam", nam));
        }

        // ==========================================
        // 5. GHI NHẬN SỬ DỤNG DỊCH VỤ
        // ==========================================

        public void GhiSuDung(
            string maSuDung,
            string maCT,
            string maDV,
            int thang,
            int nam,
            decimal soLuong)
        {
            if (string.IsNullOrWhiteSpace(maSuDung) ||
                maSuDung.Trim().Length > 20)
                throw new Exception(
                    "Mã sử dụng phải từ 1 đến 20 ký tự.");

            if (string.IsNullOrWhiteSpace(maCT) ||
                string.IsNullOrWhiteSpace(maDV))
                throw new Exception(
                    "Hãy chọn phòng và loại dịch vụ.");

            if (thang < 1 || thang > 12 ||
                nam < 2000 || nam > 2100)
                throw new Exception(
                    "Tháng/năm không hợp lệ.");

            if (soLuong <= 0)
                throw new Exception(
                    "Số lượng phải lớn hơn 0.");

            DateTime dau = new DateTime(nam, thang, 1);
            DateTime cuoi = dau.AddMonths(1).AddDays(-1);

            if (dau > DateTime.Today)
                throw new Exception(
                    "Không thể ghi dịch vụ cho tháng tương lai.");

            using (var c = Db.OpenConnection())
            using (var t = c.BeginTransaction(
                IsolationLevel.Serializable))
            {
                try
                {
                    DataTable ct = Query(c, t, @"
                        SELECT h.MaHopDong,
                               h.NgayBatDau,
                               h.NgayKetThuc,
                               h.TrangThai,
                               p.MaPhong,
                               ct.NgayNhanPhong,
                               ct.NgayTraPhong
                        FROM dbo.ChiTietHopDong ct
                        JOIN dbo.HopDong h
                            ON h.MaHopDong = ct.MaHopDong
                        JOIN dbo.Phong p
                            WITH (UPDLOCK, HOLDLOCK)
                            ON p.MaPhong = ct.MaPhong
                        WHERE ct.MaChiTietHD = @CT",
                        P("@CT", maCT));

                    if (ct.Rows.Count == 0)
                        throw new Exception(
                            "Phòng thuê không tồn tại.");

                    DataRow r = ct.Rows[0];

                    if (Convert.ToString(r["TrangThai"]) ==
                        "Dự thảo")
                        throw new Exception(
                            "Không ghi dịch vụ cho hợp đồng dự thảo.");

                    DateTime batDau = Convert.ToDateTime(
                        r["NgayNhanPhong"]).Date;

                    DateTime ketThuc = Convert.ToDateTime(
                        r["NgayKetThuc"]).Date;

                    if (r["NgayTraPhong"] != DBNull.Value)
                    {
                        DateTime ngayTra = Convert.ToDateTime(
                            r["NgayTraPhong"]).Date;

                        if (ngayTra < ketThuc)
                            ketThuc = ngayTra;
                    }
                    else
                    {
                        object giaHan = Scalar(c, t, @"
                            SELECT MAX(DenNgay)
                            FROM dbo.PhieuGiaHan
                            WHERE MaChiTietHD = @CT
                              AND TrangThai = N'Đã duyệt'",
                            P("@CT", maCT));

                        if (giaHan != null &&
                            giaHan != DBNull.Value)
                        {
                            DateTime denGiaHan =
                                Convert.ToDateTime(giaHan);

                            if (denGiaHan > ketThuc)
                                ketThuc = denGiaHan;
                        }
                    }

                    if (dau > ketThuc || cuoi < batDau)
                        throw new Exception(
                            "Tháng ghi dịch vụ nằm ngoài " +
                            "thời gian thuê của phòng.");

                    // Không ghi bổ sung khi hóa đơn
                    // của hợp đồng/tháng đã lập.
                    int daLap = Convert.ToInt32(
                        Scalar(c, t, @"
                            SELECT COUNT(*)
                            FROM dbo.HoaDonDichVu
                                WITH (UPDLOCK, HOLDLOCK)
                            WHERE MaHopDong = @HD
                              AND Thang = @Thang
                              AND Nam = @Nam",
                            P("@HD",
                                Convert.ToString(r["MaHopDong"])),
                            P("@Thang", thang),
                            P("@Nam", nam)));

                    if (daLap > 0)
                        throw new Exception(
                            "Tháng này đã lập hóa đơn. " +
                            "Không thể thêm dịch vụ sau khi chốt.");

                    DataTable dv = Query(c, t, @"
                        SELECT DonGia
                        FROM dbo.LoaiDichVu
                        WHERE MaDichVu = @DV
                          AND DangSuDung = 1",
                        P("@DV", maDV));

                    if (dv.Rows.Count == 0)
                        throw new Exception(
                            "Dịch vụ không còn được sử dụng.");

                    decimal gia = Convert.ToDecimal(
                        dv.Rows[0]["DonGia"]);

                    if (soLuong * gia > 9999999999999999m)
                        throw new Exception(
                            "Thành tiền vượt giới hạn cho phép.");

                    Execute(c, t, @"
                        INSERT INTO dbo.SuDungDichVu
                        (
                            MaSuDung,
                            MaChiTietHD,
                            MaDichVu,
                            Thang,
                            Nam,
                            SoLuong,
                            DonGiaApDung
                        )
                        VALUES
                        (
                            @Ma, @CT, @DV,
                            @Thang, @Nam, @SL, @Gia
                        )",
                        P("@Ma", maSuDung.Trim()),
                        P("@CT", maCT),
                        P("@DV", maDV),
                        P("@Thang", thang),
                        P("@Nam", nam),
                        P("@SL", soLuong),
                        P("@Gia", gia));

                    t.Commit();
                }
                catch
                {
                    if (t.Connection != null)
                        t.Rollback();

                    throw;
                }
            }
        }

        // ==========================================
        // 6. LẬP HÓA ĐƠN DỊCH VỤ
        // ==========================================

        public void LapHoaDon(
            string maHoaDon,
            string maHD,
            int thang,
            int nam)
        {
            if (string.IsNullOrWhiteSpace(maHoaDon) ||
                maHoaDon.Trim().Length > 20)
                throw new Exception(
                    "Mã hóa đơn phải từ 1 đến 20 ký tự.");

            if (string.IsNullOrWhiteSpace(maHD))
                throw new Exception(
                    "Chưa chọn hợp đồng.");

            if (thang < 1 || thang > 12 ||
                nam < 2000 || nam > 2100)
                throw new Exception(
                    "Tháng/năm không hợp lệ.");

            DateTime ngayCuoi =
                new DateTime(nam, thang, 1)
                .AddMonths(1).AddDays(-1);

            if (DateTime.Today < ngayCuoi)
                throw new Exception(
                    "Chỉ lập hóa đơn vào cuối tháng " +
                    "hoặc sau khi tháng đã kết thúc.");

            using (var c = Db.OpenConnection())
            using (var t = c.BeginTransaction(
                IsolationLevel.Serializable))
            {
                try
                {
                    int tonTaiHD = Convert.ToInt32(
                        Scalar(c, t, @"
                            SELECT COUNT(*)
                            FROM dbo.HopDong
                                WITH (UPDLOCK, HOLDLOCK)
                            WHERE MaHopDong = @HD
                              AND TrangThai <> N'Dự thảo'",
                            P("@HD", maHD)));

                    if (tonTaiHD == 0)
                        throw new Exception(
                            "Hợp đồng không hợp lệ.");

                    int daLap = Convert.ToInt32(
                        Scalar(c, t, @"
                            SELECT COUNT(*)
                            FROM dbo.HoaDonDichVu
                                WITH (UPDLOCK, HOLDLOCK)
                            WHERE MaHopDong = @HD
                              AND Thang = @Thang
                              AND Nam = @Nam",
                            P("@HD", maHD),
                            P("@Thang", thang),
                            P("@Nam", nam)));

                    if (daLap > 0)
                        throw new Exception(
                            "Hợp đồng đã có hóa đơn tháng này.");

                    DataTable sd = Query(c, t, @"
                        SELECT
                            s.MaSuDung,
                            CAST(
                                s.SoLuong * s.DonGiaApDung
                                AS DECIMAL(18,2)
                            ) AS ThanhTien
                        FROM dbo.SuDungDichVu s
                            WITH (UPDLOCK, HOLDLOCK)
                        JOIN dbo.ChiTietHopDong ct
                            ON ct.MaChiTietHD = s.MaChiTietHD
                        WHERE ct.MaHopDong = @HD
                          AND s.Thang = @Thang
                          AND s.Nam = @Nam
                          AND NOT EXISTS
                          (
                              SELECT 1
                              FROM dbo.ChiTietHoaDonDichVu x
                              WHERE x.MaSuDung = s.MaSuDung
                          )",
                        P("@HD", maHD),
                        P("@Thang", thang),
                        P("@Nam", nam));

                    if (sd.Rows.Count == 0)
                        throw new Exception(
                            "Chưa có dịch vụ sử dụng " +
                            "để lập hóa đơn.");

                    decimal tong = 0;

                    foreach (DataRow row in sd.Rows)
                        tong += Convert.ToDecimal(
                            row["ThanhTien"]);

                    DateTime han = ngayCuoi.AddDays(5);

                    Execute(c, t, @"
                        INSERT INTO dbo.HoaDonDichVu
                        (
                            MaHoaDon, MaHopDong,
                            Thang, Nam, NgayLap,
                            HanThanhToan, TongTien,
                            TrangThai
                        )
                        VALUES
                        (
                            @Ma, @HD, @Thang, @Nam,
                            @Ngay, @Han, @Tong,
                            N'Chưa thanh toán'
                        )",
                        P("@Ma", maHoaDon.Trim()),
                        P("@HD", maHD),
                        P("@Thang", thang),
                        P("@Nam", nam),
                        P("@Ngay", ngayCuoi),
                        P("@Han", han),
                        P("@Tong", tong));

                    foreach (DataRow row in sd.Rows)
                    {
                        Execute(c, t, @"
                            INSERT INTO dbo.ChiTietHoaDonDichVu
                            (MaHoaDon, MaSuDung, SoTien)
                            VALUES (@Ma, @SD, @Tien)",
                            P("@Ma", maHoaDon.Trim()),
                            P("@SD", row["MaSuDung"]),
                            P("@Tien", row["ThanhTien"]));
                    }

                    t.Commit();
                }
                catch
                {
                    if (t.Connection != null)
                        t.Rollback();

                    throw;
                }
            }
        }

        // ==========================================
        // 7. DANH SÁCH HÓA ĐƠN VÀ CÔNG NỢ
        // ==========================================

        public DataTable LayHoaDon(string maHD = "")
        {
            return Db.Query(@"
                SELECT
                    h.MaHoaDon,
                    h.MaHopDong,
                    h.Thang,
                    h.Nam,
                    h.NgayLap,
                    h.HanThanhToan,
                    h.TongTien,
                    ISNULL(x.DaThu, 0) AS DaThu,
                    h.TongTien - ISNULL(x.DaThu, 0)
                        AS ConNo,
                    CASE
                        WHEN ISNULL(x.DaThu, 0) >= h.TongTien
                        THEN N'Đã thanh toán'
                        WHEN ISNULL(x.DaThu, 0) > 0
                        THEN N'Thanh toán một phần'
                        ELSE N'Chưa thanh toán'
                    END AS TrangThai
                FROM dbo.HoaDonDichVu h
                OUTER APPLY
                (
                    SELECT SUM(SoTien) AS DaThu
                    FROM dbo.ThanhToanDichVu
                    WHERE MaHoaDon = h.MaHoaDon
                ) x
                WHERE (@HD = ''
                       OR h.MaHopDong = @HD)
                ORDER BY
                    h.Nam DESC,
                    h.Thang DESC,
                    h.MaHoaDon DESC",
                P("@HD", maHD ?? ""));
        }

        // ==========================================
        // 8. CHI TIẾT HÓA ĐƠN
        // ==========================================

        public DataTable LayChiTietHoaDon(string maHoaDon)
        {
            return Db.Query(@"
                SELECT
                    ct.MaPhong,
                    d.TenDichVu,
                    d.DonViTinh,
                    s.SoLuong,
                    s.DonGiaApDung,
                    detail.SoTien
                FROM dbo.ChiTietHoaDonDichVu detail
                JOIN dbo.SuDungDichVu s
                    ON s.MaSuDung = detail.MaSuDung
                JOIN dbo.ChiTietHopDong ct
                    ON ct.MaChiTietHD = s.MaChiTietHD
                JOIN dbo.LoaiDichVu d
                    ON d.MaDichVu = s.MaDichVu
                WHERE detail.MaHoaDon = @Ma
                ORDER BY ct.MaPhong, d.TenDichVu",
                P("@Ma", maHoaDon));
        }

        // ==========================================
        // 9. LỊCH SỬ THANH TOÁN DỊCH VỤ
        // ==========================================

        public DataTable LayLichSuThanhToan(string maHoaDon)
        {
            return Db.Query(@"
                SELECT
                    MaThanhToanDV,
                    MaHoaDon,
                    NgayThanhToan,
                    SoTien,
                    PhuongThuc,
                    GhiChu
                FROM dbo.ThanhToanDichVu
                WHERE MaHoaDon = @Ma
                ORDER BY
                    NgayThanhToan DESC,
                    MaThanhToanDV",
                P("@Ma", maHoaDon));
        }

        // ==========================================
        // 10. THANH TOÁN HÓA ĐƠN DỊCH VỤ
        // ==========================================

        public void ThanhToan(
            string maThu,
            string maHoaDon,
            DateTime ngay,
            decimal soTien,
            string phuongThuc,
            string ghiChu)
        {
            if (string.IsNullOrWhiteSpace(maThu) ||
                maThu.Trim().Length > 20)
                throw new Exception(
                    "Mã thanh toán phải từ 1 đến 20 ký tự.");

            if (string.IsNullOrWhiteSpace(maHoaDon))
                throw new Exception(
                    "Chưa chọn hóa đơn.");

            if (soTien <= 0)
                throw new Exception(
                    "Số tiền phải lớn hơn 0.");

            if (phuongThuc != "Tiền mặt" &&
                phuongThuc != "Chuyển khoản")
                throw new Exception(
                    "Phương thức thanh toán không hợp lệ.");

            if (ngay.Date > DateTime.Today)
                throw new Exception(
                    "Không ghi nhận ngày thu trong tương lai.");

            using (var c = Db.OpenConnection())
            using (var t = c.BeginTransaction(
                IsolationLevel.Serializable))
            {
                try
                {
                    DataTable hd = Query(c, t, @"
                        SELECT TongTien, NgayLap
                        FROM dbo.HoaDonDichVu
                            WITH (UPDLOCK, HOLDLOCK)
                        WHERE MaHoaDon = @Ma",
                        P("@Ma", maHoaDon));

                    if (hd.Rows.Count == 0)
                        throw new Exception(
                            "Không tìm thấy hóa đơn.");

                    if (ngay.Date <
                        Convert.ToDateTime(
                            hd.Rows[0]["NgayLap"]).Date)
                        throw new Exception(
                            "Ngày thu không được trước " +
                            "ngày lập hóa đơn.");

                    decimal tong = Convert.ToDecimal(
                        hd.Rows[0]["TongTien"]);

                    decimal daThu = Convert.ToDecimal(
                        Scalar(c, t, @"
                            SELECT ISNULL(SUM(SoTien), 0)
                            FROM dbo.ThanhToanDichVu
                            WHERE MaHoaDon = @Ma",
                            P("@Ma", maHoaDon)));

                    decimal conLai = tong - daThu;

                    if (conLai <= 0)
                        throw new Exception(
                            "Hóa đơn đã thanh toán đủ.");

                    if (soTien > conLai)
                        throw new Exception(
                            "Số tiền vượt công nợ còn lại: " +
                            conLai.ToString("N0") + " đ.");

                    Execute(c, t, @"
                        INSERT INTO dbo.ThanhToanDichVu
                        (
                            MaThanhToanDV,
                            MaHoaDon,
                            NgayThanhToan,
                            SoTien,
                            PhuongThuc,
                            GhiChu
                        )
                        VALUES
                        (
                            @Ma, @HD, @Ngay,
                            @Tien, @PT, @GhiChu
                        )",
                        P("@Ma", maThu.Trim()),
                        P("@HD", maHoaDon),
                        P("@Ngay", ngay.Date),
                        P("@Tien", soTien),
                        P("@PT", phuongThuc),
                        P("@GhiChu",
                            string.IsNullOrWhiteSpace(ghiChu)
                            ? (object)DBNull.Value
                            : ghiChu.Trim()));

                    decimal sauThu = daThu + soTien;

                    string trangThai =
                        sauThu >= tong
                        ? "Đã thanh toán"
                        : "Thanh toán một phần";

                    Execute(c, t, @"
                        UPDATE dbo.HoaDonDichVu
                        SET TrangThai = @TT
                        WHERE MaHoaDon = @Ma",
                        P("@TT", trangThai),
                        P("@Ma", maHoaDon));

                    t.Commit();
                }
                catch
                {
                    if (t.Connection != null)
                        t.Rollback();

                    throw;
                }
            }
        }
    }
}
