
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using QuanLyChoThueVanPhong.Data;

namespace QuanLyChoThueVanPhong.Services
{
    public class GiaHanService
    {
        private class KyThue
        {
            public DateTime TuNgay;
            public DateTime DenNgay;
            public DateTime HanTra;
            public decimal SoTien;
            public decimal DaTra;
        }

        private static SqlParameter P(string name, object value)
        {
            return new SqlParameter(
                name, value ?? DBNull.Value);
        }

        private static object Scalar(
            SqlConnection c, SqlTransaction t,
            string sql, params SqlParameter[] p)
        {
            using (SqlCommand cmd = new SqlCommand(sql, c, t))
            {
                cmd.Parameters.AddRange(p);
                return cmd.ExecuteScalar();
            }
        }

        private static int Execute(
            SqlConnection c, SqlTransaction t,
            string sql, params SqlParameter[] p)
        {
            using (SqlCommand cmd = new SqlCommand(sql, c, t))
            {
                cmd.Parameters.AddRange(p);
                return cmd.ExecuteNonQuery();
            }
        }

        private static DataTable Query(
            SqlConnection c, SqlTransaction t,
            string sql, params SqlParameter[] p)
        {
            using (SqlCommand cmd = new SqlCommand(sql, c, t))
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddRange(p);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable LayHopDong()
        {
            return Db.Query(@"
                SELECT h.MaHopDong,
                       h.MaHopDong + N' - ' +
                       k.TenKhachHang AS HienThi
                FROM dbo.HopDong h
                JOIN dbo.KhachHang k
                    ON h.MaKhachHang = k.MaKhachHang
                WHERE h.TrangThai = N'Có hiệu lực'
                ORDER BY h.MaHopDong");
        }

        public DataTable LayPhongTheoHopDong(string maHD)
        {
            return Db.Query(@"
                SELECT ct.MaChiTietHD,
                       ct.MaPhong,
                       ct.MaPhong + N' - ' +
                       p.TenPhong AS HienThi,
                       ct.DonGiaThue
                FROM dbo.ChiTietHopDong ct
                JOIN dbo.Phong p ON p.MaPhong = ct.MaPhong
                WHERE ct.MaHopDong = @Ma
                ORDER BY ct.MaPhong",
                P("@Ma", maHD));
        }

        public DataTable LayDanhSach(string key = "")
        {
            return Db.Query(@"
                SELECT gh.MaPhieuGiaHan,
                       gh.MaChiTietHD,
                       ct.MaHopDong,
                       ct.MaPhong,
                       gh.NgayLap,
                       gh.TuNgay,
                       gh.DenNgay,
                       gh.SoThangGiaHan,
                       gh.DonGiaGiaHan,
                       gh.TrangThai,
                       gh.GhiChu
                FROM dbo.PhieuGiaHan gh
                JOIN dbo.ChiTietHopDong ct
                    ON ct.MaChiTietHD = gh.MaChiTietHD
                WHERE gh.MaPhieuGiaHan LIKE @Key
                   OR ct.MaHopDong LIKE @Key
                   OR ct.MaPhong LIKE @Key
                   OR gh.TrangThai LIKE @Key
                ORDER BY gh.NgayLap DESC, gh.MaPhieuGiaHan",
                P("@Key", "%" + (key ?? "").Trim() + "%"));
        }

        // Lấy ngày gia hạn tiếp theo và giới hạn còn lại.
        public DataTable LayThongTinPhong(string maCT)
        {
            return Db.Query(@"
                SELECT h.SoThangThue,
                       h.NgayKetThuc,
                       ct.DonGiaThue,
                       ISNULL(
                          (SELECT SUM(g.SoThangGiaHan)
                           FROM dbo.PhieuGiaHan g
                           WHERE g.MaChiTietHD = ct.MaChiTietHD
                             AND g.TrangThai = N'Đã duyệt'), 0)
                             AS ThangDaGiaHan,
                       ISNULL(
                          (SELECT MAX(g.DenNgay)
                           FROM dbo.PhieuGiaHan g
                           WHERE g.MaChiTietHD = ct.MaChiTietHD
                             AND g.TrangThai = N'Đã duyệt'),
                          h.NgayKetThuc) AS NgayKetThucHienTai
                FROM dbo.ChiTietHopDong ct
                JOIN dbo.HopDong h
                    ON h.MaHopDong = ct.MaHopDong
                WHERE ct.MaChiTietHD = @Ma",
                P("@Ma", maCT));
        }

        private static void KiemTraMa(string ma)
        {
            if (string.IsNullOrWhiteSpace(ma) ||
                ma.Trim().Length > 20)
                throw new Exception(
                    "Mã phiếu phải có từ 1 đến 20 ký tự.");
        }

        // Tạo phiếu Chờ duyệt, chưa gia hạn thực tế.
        public void LapPhieu(
            string ma, string maCT, int soThang,
            decimal donGia, string ghiChu)
        {
            KiemTraMa(ma);

            if (string.IsNullOrWhiteSpace(maCT))
                throw new Exception("Hãy chọn phòng thuê.");

            if (soThang <= 0 || donGia < 0)
                throw new Exception(
                    "Số tháng hoặc đơn giá không hợp lệ.");

            using (SqlConnection c = Db.OpenConnection())
            using (SqlTransaction t =
                c.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                    DataTable dt = Query(c, t, @"
                        SELECT h.SoThangThue, h.NgayKetThuc,
                               h.TrangThai,
                               ct.MaPhong
                        FROM dbo.ChiTietHopDong ct
                        JOIN dbo.HopDong h
                            ON h.MaHopDong = ct.MaHopDong
                        WHERE ct.MaChiTietHD = @Ma",
                        P("@Ma", maCT));

                    if (dt.Rows.Count == 0)
                        throw new Exception(
                            "Không tìm thấy phòng trong hợp đồng.");

                    DataRow r = dt.Rows[0];

                    if (Convert.ToString(r["TrangThai"]) !=
                        "Có hiệu lực")
                        throw new Exception(
                            "Hợp đồng không ở trạng thái Có hiệu lực.");

                    // Khóa phòng nhằm đồng bộ các thao tác duyệt.
                    Scalar(c, t, @"
                        SELECT MaPhong
                        FROM dbo.Phong WITH (UPDLOCK, HOLDLOCK)
                        WHERE MaPhong = @Ma",
                        P("@Ma", Convert.ToString(r["MaPhong"])));

                    int thangGoc =
                        Convert.ToInt32(r["SoThangThue"]);

                    DataTable daDuyet = Query(c, t, @"
                        SELECT SoThangGiaHan, DenNgay
                        FROM dbo.PhieuGiaHan WITH (UPDLOCK, HOLDLOCK)
                        WHERE MaChiTietHD = @Ma
                          AND TrangThai = N'Đã duyệt'
                        ORDER BY TuNgay",
                        P("@Ma", maCT));

                    int tongDaDuyet = 0;
                    DateTime ngayCuoi =
                        Convert.ToDateTime(r["NgayKetThuc"]);

                    foreach (DataRow x in daDuyet.Rows)
                    {
                        tongDaDuyet +=
                            Convert.ToInt32(x["SoThangGiaHan"]);

                        DateTime ngay =
                            Convert.ToDateTime(x["DenNgay"]);

                        if (ngay > ngayCuoi)
                            ngayCuoi = ngay;
                    }

                    if (tongDaDuyet + soThang > thangGoc / 2)
                        throw new Exception(
                            "Tổng số tháng gia hạn vượt quá " +
                            "một nửa thời hạn thuê ban đầu.");

                    int choDuyet = Convert.ToInt32(
                        Scalar(c, t, @"
                            SELECT COUNT(*)
                            FROM dbo.PhieuGiaHan
                            WHERE MaChiTietHD = @Ma
                              AND TrangThai = N'Chờ duyệt'",
                            P("@Ma", maCT)));

                    if (choDuyet > 0)
                        throw new Exception(
                            "Phòng đã có phiếu chờ duyệt. " +
                            "Hãy xử lý phiếu cũ trước.");

                    DateTime tuNgay = ngayCuoi.AddDays(1);
                    DateTime denNgay =
                        tuNgay.AddMonths(soThang).AddDays(-1);

                    Execute(c, t, @"
                        INSERT INTO dbo.PhieuGiaHan
                        (MaPhieuGiaHan, MaChiTietHD,
                         NgayLap, TuNgay, DenNgay,
                         SoThangGiaHan, DonGiaGiaHan,
                         TrangThai, GhiChu)
                        VALUES
                        (@Ma, @CT, @NgayLap, @Tu, @Den,
                         @Thang, @Gia, N'Chờ duyệt', @GhiChu)",
                        P("@Ma", ma.Trim()),
                        P("@CT", maCT),
                        P("@NgayLap", DateTime.Today),
                        P("@Tu", tuNgay),
                        P("@Den", denNgay),
                        P("@Thang", soThang),
                        P("@Gia", donGia),
                        P("@GhiChu",
                            string.IsNullOrWhiteSpace(ghiChu)
                              ? (object)DBNull.Value
                              : ghiChu.Trim()));

                    t.Commit();
                }
                catch
                {
                    if (t.Connection != null) t.Rollback();
                    throw;
                }
            }
        }

        // Tạo các kỳ thuê theo ngày bắt đầu của từng giai đoạn.
        private static void ThemCacKy(
            List<KyThue> ds,
            DateTime batDau, int soThang,
            decimal gia, bool laKyGoc)
        {
            for (int i = 0; i < soThang; i++)
            {
                DateTime tu = batDau.AddMonths(i);
                DateTime den =
                    batDau.AddMonths(i + 1).AddDays(-1);

                ds.Add(new KyThue
                {
                    TuNgay = tu,
                    DenNgay = den,
                    SoTien = gia,
                    HanTra = laKyGoc && i < 6
                        ? batDau
                        : tu
                });
            }
        }

        // Kiểm tra nợ thuê cho một chi tiết hợp đồng
        // tính đến ngày hiện tại.
        private static decimal NoTienThue(
            SqlConnection c, SqlTransaction t,
            string maCT, DateTime ngayXet)
        {
            DataTable thongTin = Query(c, t, @"
                SELECT h.NgayBatDau, h.SoThangThue,
                       ct.DonGiaThue
                FROM dbo.ChiTietHopDong ct
                JOIN dbo.HopDong h
                    ON h.MaHopDong = ct.MaHopDong
                WHERE ct.MaChiTietHD = @Ma",
                P("@Ma", maCT));

            if (thongTin.Rows.Count == 0)
                throw new Exception("Không tìm thấy chi tiết thuê.");

            DataRow r = thongTin.Rows[0];

            List<KyThue> ds = new List<KyThue>();

            ThemCacKy(
                ds,
                Convert.ToDateTime(r["NgayBatDau"]),
                Convert.ToInt32(r["SoThangThue"]),
                Convert.ToDecimal(r["DonGiaThue"]),
                true);

            DataTable giaHan = Query(c, t, @"
                SELECT TuNgay, SoThangGiaHan, DonGiaGiaHan
                FROM dbo.PhieuGiaHan
                WHERE MaChiTietHD = @Ma
                  AND TrangThai = N'Đã duyệt'
                ORDER BY TuNgay",
                P("@Ma", maCT));

            foreach (DataRow x in giaHan.Rows)
            {
                ThemCacKy(
                    ds,
                    Convert.ToDateTime(x["TuNgay"]),
                    Convert.ToInt32(x["SoThangGiaHan"]),
                    Convert.ToDecimal(x["DonGiaGiaHan"]),
                    false);
            }

            // Phân bổ thanh toán vào các kỳ mà phiếu bao phủ.
            DataTable tra = Query(c, t, @"
                SELECT TuNgay, DenNgay,
                       SoThangThanhToan, SoTien
                FROM dbo.ThanhToanTienThue
                WHERE MaChiTietHD = @Ma",
                P("@Ma", maCT));

            foreach (DataRow x in tra.Rows)
            {
                DateTime tu =
                    Convert.ToDateTime(x["TuNgay"]).Date;
                DateTime den =
                    Convert.ToDateTime(x["DenNgay"]).Date;

                int soThang =
                    Convert.ToInt32(x["SoThangThanhToan"]);

                List<KyThue> phuHop = ds
                    .Where(k => k.TuNgay >= tu &&
                                k.DenNgay <= den)
                    .ToList();

                if (phuHop.Count != soThang ||
                    phuHop.Count == 0 ||
                    phuHop.Min(k => k.TuNgay) != tu ||
                    phuHop.Max(k => k.DenNgay) != den)
                {
                    throw new Exception(
                        "Có khoản thanh toán tiền thuê " +
                        "không khớp kỳ thuê. Cần kiểm tra " +
                        "TuNgay, DenNgay và SoThangThanhToan.");
                }

                decimal moiKy =
                    Convert.ToDecimal(x["SoTien"]) / soThang;

                foreach (KyThue k in phuHop)
                    k.DaTra += moiKy;
            }

            // Không cho khoản trả dư của một kỳ tự động
            // bù sang kỳ khác nếu chưa có nghiệp vụ phân bổ.
            return ds.Where(k => k.HanTra <= ngayXet)
                     .Sum(k => Math.Max(0, k.SoTien - k.DaTra));
        }

        private static void KiemTraCongNo(
            SqlConnection c, SqlTransaction t,
            string maCT, string maHD)
        {
            DateTime ngayXet = DateTime.Today;

            decimal noThue =
                NoTienThue(c, t, maCT, ngayXet);

            if (noThue > 0.01m)
                throw new Exception(
                    "Phòng còn nợ tiền thuê đến hạn: " +
                    noThue.ToString("N0") + " đ.\n" +
                    "Chưa thể duyệt gia hạn.");

            // Kiểm tra tất cả hóa đơn dịch vụ đã đến hạn
            // của hợp đồng, không chỉ hóa đơn của một phòng.
            object noDV = Scalar(c, t, @"
                SELECT ISNULL(SUM(
                    CASE
                        WHEN hd.TongTien > ISNULL(p.DaTra, 0)
                        THEN hd.TongTien - ISNULL(p.DaTra, 0)
                        ELSE 0
                    END), 0)
                FROM dbo.HoaDonDichVu hd
                OUTER APPLY
                (
                    SELECT SUM(tt.SoTien) AS DaTra
                    FROM dbo.ThanhToanDichVu tt
                    WHERE tt.MaHoaDon = hd.MaHoaDon
                ) p
                WHERE hd.MaHopDong = @MaHD
                  AND hd.HanThanhToan <= @Ngay",
                P("@MaHD", maHD),
                P("@Ngay", ngayXet));

            if (Convert.ToDecimal(noDV) > 0.01m)
                throw new Exception(
                    "Hợp đồng còn hóa đơn dịch vụ " +
                    "đến hạn chưa thanh toán đủ.");

            // Dịch vụ của những tháng đã kết thúc
            // phải được lập hóa đơn trước khi gia hạn.
            int chuaLapHoaDon = Convert.ToInt32(
                Scalar(c, t, @"
                SELECT COUNT(*)
                FROM dbo.SuDungDichVu sd
                JOIN dbo.ChiTietHopDong ct
                    ON ct.MaChiTietHD = sd.MaChiTietHD
                WHERE ct.MaHopDong = @MaHD
                  AND
                  (
                      sd.Nam < YEAR(@Ngay)
                      OR
                      (sd.Nam = YEAR(@Ngay)
                       AND sd.Thang < MONTH(@Ngay))
                  )
                  AND NOT EXISTS
                  (
                      SELECT 1
                      FROM dbo.ChiTietHoaDonDichVu cthd
                      WHERE cthd.MaSuDung = sd.MaSuDung
                  )",
                P("@MaHD", maHD),
                P("@Ngay", ngayXet)));

            if (chuaLapHoaDon > 0)
                throw new Exception(
                    "Còn dịch vụ đã sử dụng nhưng chưa " +
                    "lập hóa đơn. Cần lập hóa đơn trước.");
        }

        public string KiemTraNo(string maPhieu)
        {
            using (SqlConnection c = Db.OpenConnection())
            using (SqlTransaction t =
                c.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                    DataTable dt = Query(c, t, @"
                        SELECT gh.MaChiTietHD, ct.MaHopDong,
                               ct.MaPhong
                        FROM dbo.PhieuGiaHan gh
                        JOIN dbo.ChiTietHopDong ct
                            ON ct.MaChiTietHD = gh.MaChiTietHD
                        WHERE gh.MaPhieuGiaHan = @Ma",
                        P("@Ma", maPhieu));

                    if (dt.Rows.Count == 0)
                        throw new Exception("Không tìm thấy phiếu.");

                    string maHD =
                        Convert.ToString(dt.Rows[0]["MaHopDong"]);
                    string maCT =
                        Convert.ToString(dt.Rows[0]["MaChiTietHD"]);

                    // Khóa phòng nhất quán với bước duyệt.
                    Scalar(c, t, @"
                        SELECT MaPhong FROM dbo.Phong
                        WITH (UPDLOCK, HOLDLOCK)
                        WHERE MaPhong = @Ma",
                        P("@Ma",
                            Convert.ToString(dt.Rows[0]["MaPhong"])));

                    KiemTraCongNo(c, t, maCT, maHD);
                    t.Commit();

                    return "Không phát hiện nợ tiền thuê đến hạn " +
                           "hoặc nợ dịch vụ đã ghi nhận. " +
                           "Có thể tiếp tục kiểm tra gia hạn.";
                }
                catch
                {
                    if (t.Connection != null) t.Rollback();
                    throw;
                }
            }
        }

        // Chỉ duyệt khi đáp ứng tất cả điều kiện.
        public void Duyet(string maPhieu)
        {
            using (SqlConnection c = Db.OpenConnection())
            using (SqlTransaction t =
                c.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                    DataTable dt = Query(c, t, @"
                        SELECT gh.MaChiTietHD,
                               gh.TuNgay, gh.DenNgay,
                               gh.SoThangGiaHan, gh.TrangThai,
                               ct.MaHopDong, ct.MaPhong,
                               h.SoThangThue, h.NgayKetThuc,
                               h.TrangThai AS TrangThaiHD
                        FROM dbo.PhieuGiaHan gh
                        JOIN dbo.ChiTietHopDong ct
                            ON ct.MaChiTietHD = gh.MaChiTietHD
                        JOIN dbo.HopDong h
                            ON h.MaHopDong = ct.MaHopDong
                        WHERE gh.MaPhieuGiaHan = @Ma",
                        P("@Ma", maPhieu));

                    if (dt.Rows.Count == 0)
                        throw new Exception("Không tìm thấy phiếu gia hạn.");

                    DataRow r = dt.Rows[0];

                    string maCT = Convert.ToString(r["MaChiTietHD"]);
                    string maHD = Convert.ToString(r["MaHopDong"]);
                    string maPhong = Convert.ToString(r["MaPhong"]);

                    Scalar(c, t, @"
                        SELECT MaPhong
                        FROM dbo.Phong WITH (UPDLOCK, HOLDLOCK)
                        WHERE MaPhong = @Ma",
                        P("@Ma", maPhong));

                    if (Convert.ToString(r["TrangThai"]) !=
                        "Chờ duyệt")
                        throw new Exception(
                            "Chỉ duyệt phiếu đang Chờ duyệt.");

                    if (Convert.ToString(r["TrangThaiHD"]) !=
                        "Có hiệu lực")
                        throw new Exception(
                            "Hợp đồng không còn hiệu lực.");

                    // Kiểm tra lại giới hạn và thứ tự gia hạn.
                    DataTable daDuyet = Query(c, t, @"
                        SELECT TuNgay, DenNgay, SoThangGiaHan
                        FROM dbo.PhieuGiaHan
                        WHERE MaChiTietHD = @Ma
                          AND TrangThai = N'Đã duyệt'
                        ORDER BY TuNgay",
                        P("@Ma", maCT));

                    int tongThang = 0;
                    DateTime ngayCuoi =
                        Convert.ToDateTime(r["NgayKetThuc"]);

                    foreach (DataRow x in daDuyet.Rows)
                    {
                        tongThang +=
                            Convert.ToInt32(x["SoThangGiaHan"]);

                        DateTime cuoi =
                            Convert.ToDateTime(x["DenNgay"]);

                        if (cuoi > ngayCuoi)
                            ngayCuoi = cuoi;
                    }

                    int thangThem =
                        Convert.ToInt32(r["SoThangGiaHan"]);

                    int thangGoc =
                        Convert.ToInt32(r["SoThangThue"]);

                    if (tongThang + thangThem > thangGoc / 2)
                        throw new Exception(
                            "Thời gian gia hạn vượt giới hạn.");

                    DateTime tuNgay =
                        Convert.ToDateTime(r["TuNgay"]);
                    DateTime denNgay =
                        Convert.ToDateTime(r["DenNgay"]);

                    if (tuNgay != ngayCuoi.AddDays(1) ||
                        denNgay !=
                        tuNgay.AddMonths(thangThem).AddDays(-1))
                        throw new Exception(
                            "Kỳ gia hạn không còn liên tiếp " +
                            "với thời hạn thuê hiện tại.");

                    KiemTraCongNo(c, t, maCT, maHD);

                    // Kiểm tra phòng có bị hợp đồng khác
                    // giữ trong khoảng gia hạn hay không.
                    int trung = Convert.ToInt32(
                        Scalar(c, t, @"
                        SELECT COUNT(*)
                        FROM dbo.ChiTietHopDong ct
                        JOIN dbo.HopDong h
                            ON h.MaHopDong = ct.MaHopDong
                        OUTER APPLY
                        (
                            SELECT MAX(g.DenNgay) AS DenGiaHan
                            FROM dbo.PhieuGiaHan g
                            WHERE g.MaChiTietHD = ct.MaChiTietHD
                              AND g.TrangThai = N'Đã duyệt'
                        ) gh
                        WHERE ct.MaPhong = @Phong
                          AND h.MaHopDong <> @HD
                          AND h.TrangThai <> N'Dự thảo'
                          AND ct.NgayNhanPhong <= @Den
                          AND
                          (
                              CASE
                                  WHEN ct.NgayTraPhong IS NOT NULL
                                  THEN ct.NgayTraPhong
                                  WHEN gh.DenGiaHan > h.NgayKetThuc
                                  THEN gh.DenGiaHan
                                  ELSE h.NgayKetThuc
                              END
                          ) >= @Tu",
                        P("@Phong", maPhong),
                        P("@HD", maHD),
                        P("@Tu", tuNgay),
                        P("@Den", denNgay)));

                    if (trung > 0)
                        throw new Exception(
                            "Phòng đã có hợp đồng khác trong " +
                            "thời gian yêu cầu gia hạn.");

                    int n = Execute(c, t, @"
                        UPDATE dbo.PhieuGiaHan
                        SET TrangThai = N'Đã duyệt'
                        WHERE MaPhieuGiaHan = @Ma
                          AND TrangThai = N'Chờ duyệt'",
                        P("@Ma", maPhieu));

                    if (n != 1)
                        throw new Exception(
                            "Không cập nhật được trạng thái phiếu.");

                    t.Commit();
                }
                catch
                {
                    if (t.Connection != null) t.Rollback();
                    throw;
                }
            }
        }

        public void DoiTrangThai(
            string maPhieu, string trangThai)
        {
            if (trangThai != "Từ chối" &&
                trangThai != "Đã hủy")
                throw new Exception(
                    "Chỉ cho phép Từ chối hoặc Đã hủy.");

            int n = Db.Execute(@"
                UPDATE dbo.PhieuGiaHan
                SET TrangThai = @TrangThai
                WHERE MaPhieuGiaHan = @Ma
                  AND TrangThai = N'Chờ duyệt'",
                P("@TrangThai", trangThai),
                P("@Ma", maPhieu));

            if (n == 0)
                throw new Exception(
                    "Không tìm thấy phiếu chờ duyệt. " +
                    "Phiếu đã duyệt không thể hủy trực tiếp.");
        }
    }
}
