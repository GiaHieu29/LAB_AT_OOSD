
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using QuanLyChoThueVanPhong.Data;

namespace QuanLyChoThueVanPhong.Services
{
    public class KyThanhToan
    {
        public DateTime TuNgay { get; set; }
        public DateTime DenNgay { get; set; }
        public DateTime HanThu { get; set; }
        public int SoThang { get; set; }
        public decimal SoTien { get; set; }
        public decimal DaThu { get; set; }
        public string LoaiKy { get; set; }
    }

    public class ThanhToanTienThueService
    {
        // =========================================
        // 1. LẤY DANH SÁCH HỢP ĐỒNG
        // =========================================

        public DataTable LayHopDong()
        {
            return Db.Query(@"
                SELECT
                    h.MaHopDong,
                    h.MaHopDong + N' - ' +
                    k.TenKhachHang AS HienThi
                FROM dbo.HopDong h
                INNER JOIN dbo.KhachHang k
                    ON h.MaKhachHang = k.MaKhachHang
                WHERE h.TrangThai <> N'Dự thảo'
                ORDER BY h.MaHopDong");
        }

        // =========================================
        // 2. LẤY PHÒNG THEO HỢP ĐỒNG
        // =========================================

        public DataTable LayPhong(string maHD)
        {
            return Db.Query(@"
                SELECT
                    ct.MaChiTietHD,
                    ct.MaPhong + N' - ' +
                    p.TenPhong AS HienThi
                FROM dbo.ChiTietHopDong ct
                JOIN dbo.Phong p
                    ON p.MaPhong = ct.MaPhong
                WHERE ct.MaHopDong = @MaHD
                ORDER BY ct.MaPhong",
                Db.P("@MaHD", maHD));
        }

        // =========================================
        // 3. LỊCH SỬ THANH TOÁN
        // =========================================

        public DataTable LayLichSu(
            string maCT = "", string key = "")
        {
            return Db.Query(@"
                SELECT
                    tt.MaThanhToan,
                    ct.MaHopDong,
                    ct.MaPhong,
                    tt.MaChiTietHD,
                    tt.NgayThanhToan,
                    tt.TuNgay,
                    tt.DenNgay,
                    tt.SoThangThanhToan,
                    tt.SoTien,
                    tt.PhuongThuc,
                    tt.GhiChu
                FROM dbo.ThanhToanTienThue tt
                INNER JOIN dbo.ChiTietHopDong ct
                    ON tt.MaChiTietHD = ct.MaChiTietHD
                WHERE (@MaCT = ''
                       OR tt.MaChiTietHD = @MaCT)
                  AND
                  (
                      tt.MaThanhToan LIKE @Key
                      OR ct.MaHopDong LIKE @Key
                      OR ct.MaPhong LIKE @Key
                      OR tt.PhuongThuc LIKE @Key
                  )
                ORDER BY
                    tt.NgayThanhToan DESC,
                    tt.MaThanhToan",
                Db.P("@MaCT", maCT ?? ""),
                Db.P("@Key", "%" + (key ?? "").Trim() + "%"));
        }

        // =========================================
        // 4. LẤY THÔNG TIN THUÊ VÀ GIA HẠN
        // =========================================

        private static DataTable Query(
            SqlConnection c, SqlTransaction t,
            string sql, params SqlParameter[] ps)
        {
            using (SqlCommand cmd =
                new SqlCommand(sql, c, t))
            using (SqlDataAdapter da =
                new SqlDataAdapter(cmd))
            {
                cmd.Parameters.AddRange(ps);

                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }

        private static int Execute(
            SqlConnection c, SqlTransaction t,
            string sql, params SqlParameter[] ps)
        {
            using (SqlCommand cmd =
                new SqlCommand(sql, c, t))
            {
                cmd.Parameters.AddRange(ps);
                return cmd.ExecuteNonQuery();
            }
        }

        private static SqlParameter P(
            string name, object value)
        {
            return new SqlParameter(
                name, value ?? DBNull.Value);
        }

        // =========================================
        // 5. SINH CÁC KỲ THANH TOÁN
        // =========================================

        private static List<KyThanhToan> TaoCacKy(
            DateTime batDau,
            int soThang,
            decimal donGia,
            bool laThueGoc)
        {
            List<KyThanhToan> ds =
                new List<KyThanhToan>();

            // 6 tháng đầu của hợp đồng gốc:
            // thu một lần cho cả 6 tháng.
            if (laThueGoc)
            {
                ds.Add(new KyThanhToan
                {
                    TuNgay = batDau.Date,
                    DenNgay = batDau.AddMonths(6)
                        .AddDays(-1).Date,
                    HanThu = batDau.Date,
                    SoThang = 6,
                    SoTien = donGia * 6,
                    LoaiKy = "Trả trước 6 tháng"
                });

                for (int i = 6; i < soThang; i++)
                {
                    DateTime tu = batDau.AddMonths(i);
                    DateTime den = batDau
                        .AddMonths(i + 1).AddDays(-1);

                    ds.Add(new KyThanhToan
                    {
                        TuNgay = tu.Date,
                        DenNgay = den.Date,
                        HanThu = tu.Date,
                        SoThang = 1,
                        SoTien = donGia,
                        LoaiKy = "Thu hàng tháng"
                    });
                }
            }
            else
            {
                // Kỳ gia hạn đã được duyệt:
                // thanh toán từng tháng.
                for (int i = 0; i < soThang; i++)
                {
                    DateTime tu = batDau.AddMonths(i);
                    DateTime den = batDau
                        .AddMonths(i + 1).AddDays(-1);

                    ds.Add(new KyThanhToan
                    {
                        TuNgay = tu.Date,
                        DenNgay = den.Date,
                        HanThu = tu.Date,
                        SoThang = 1,
                        SoTien = donGia,
                        LoaiKy = "Thu tháng gia hạn"
                    });
                }
            }

            return ds;
        }

        private List<KyThanhToan> LayCacKy(
            SqlConnection c,
            SqlTransaction t,
            string maCT)
        {
            DataTable thongTin = Query(c, t, @"
                SELECT
                    h.NgayBatDau,
                    h.SoThangThue,
                    h.TrangThai,
                    ct.DonGiaThue,
                    ct.MaPhong
                FROM dbo.ChiTietHopDong ct
                JOIN dbo.HopDong h
                    ON h.MaHopDong = ct.MaHopDong
                WHERE ct.MaChiTietHD = @MaCT",
                P("@MaCT", maCT));

            if (thongTin.Rows.Count == 0)
                throw new Exception(
                    "Không tìm thấy chi tiết hợp đồng.");

            DataRow r = thongTin.Rows[0];

            if (Convert.ToString(r["TrangThai"]) ==
                "Dự thảo")
                throw new Exception(
                    "Hợp đồng Dự thảo chưa được thu tiền.");

            DateTime batDau =
                Convert.ToDateTime(r["NgayBatDau"]).Date;

            int soThang =
                Convert.ToInt32(r["SoThangThue"]);

            decimal donGia =
                Convert.ToDecimal(r["DonGiaThue"]);

            List<KyThanhToan> ds =
                TaoCacKy(batDau, soThang, donGia, true);

            // Các kỳ gia hạn đã được duyệt.
            DataTable giaHan = Query(c, t, @"
                SELECT TuNgay, SoThangGiaHan,
                       DonGiaGiaHan
                FROM dbo.PhieuGiaHan
                WHERE MaChiTietHD = @MaCT
                  AND TrangThai = N'Đã duyệt'
                ORDER BY TuNgay",
                P("@MaCT", maCT));

            foreach (DataRow gh in giaHan.Rows)
            {
                ds.AddRange(TaoCacKy(
                    Convert.ToDateTime(gh["TuNgay"]),
                    Convert.ToInt32(gh["SoThangGiaHan"]),
                    Convert.ToDecimal(gh["DonGiaGiaHan"]),
                    false));
            }

            // Đối chiếu các khoản đã thu.
            DataTable daThu = Query(c, t, @"
                SELECT MaThanhToan, TuNgay, DenNgay,
                       SoThangThanhToan, SoTien
                FROM dbo.ThanhToanTienThue
                    WITH (UPDLOCK, HOLDLOCK)
                WHERE MaChiTietHD = @MaCT
                ORDER BY TuNgay",
                P("@MaCT", maCT));

            foreach (DataRow tt in daThu.Rows)
            {
                DateTime tu =
                    Convert.ToDateTime(tt["TuNgay"]).Date;

                DateTime den =
                    Convert.ToDateTime(tt["DenNgay"]).Date;

                int thang = Convert.ToInt32(
                    tt["SoThangThanhToan"]);

                decimal tien =
                    Convert.ToDecimal(tt["SoTien"]);

                KyThanhToan ky = ds.FirstOrDefault(
                    x => x.TuNgay == tu &&
                         x.DenNgay == den &&
                         x.SoThang == thang);

                if (ky == null)
                {
                    throw new Exception(
                        "Phiếu " +
                        Convert.ToString(tt["MaThanhToan"]) +
                        " không khớp kỳ thanh toán chuẩn. " +
                        "Hãy kiểm tra dữ liệu SQL.");
                }

                ky.DaThu += tien;
            }

            if (ds.Any(x => x.DaThu > x.SoTien + 0.01m))
            {
                throw new Exception(
                    "Phát hiện kỳ thuê được thu vượt số tiền " +
                    "phải thu. Cần đối chiếu dữ liệu thanh toán.");
            }

            return ds.OrderBy(x => x.TuNgay).ToList();
        }

        // =========================================
        // 6. LẤY BẢNG CÁC KỲ THANH TOÁN
        // =========================================

        public DataTable LayBangKy(string maCT)
        {
            using (SqlConnection c = Db.OpenConnection())
            using (SqlTransaction t =
                c.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                    List<KyThanhToan> ds =
                        LayCacKy(c, t, maCT);

                    DataTable bang = new DataTable();

                    bang.Columns.Add(
                        "TuNgay", typeof(DateTime));
                    bang.Columns.Add(
                        "DenNgay", typeof(DateTime));
                    bang.Columns.Add(
                        "HanThu", typeof(DateTime));
                    bang.Columns.Add(
                        "SoThang", typeof(int));
                    bang.Columns.Add(
                        "LoaiKy", typeof(string));
                    bang.Columns.Add(
                        "PhaiThu", typeof(decimal));
                    bang.Columns.Add(
                        "DaThu", typeof(decimal));
                    bang.Columns.Add(
                        "ConLai", typeof(decimal));
                    bang.Columns.Add(
                        "TrangThai", typeof(string));

                    foreach (KyThanhToan k in ds)
                    {
                        decimal conLai =
                            Math.Max(0, k.SoTien - k.DaThu);

                        string trangThai =
                            conLai <= 0.01m
                            ? "Đã thanh toán"
                            : k.HanThu <= DateTime.Today
                                ? "Đến hạn / Còn nợ"
                                : "Chưa đến hạn";

                        bang.Rows.Add(
                            k.TuNgay, k.DenNgay,
                            k.HanThu, k.SoThang,
                            k.LoaiKy, k.SoTien,
                            k.DaThu, conLai, trangThai);
                    }

                    t.Commit();
                    return bang;
                }
                catch
                {
                    if (t.Connection != null)
                        t.Rollback();
                    throw;
                }
            }
        }

        // =========================================
        // 7. TỔNG HỢP CÔNG NỢ
        // =========================================

        public DataTable LayCongNo(string maCT)
        {
            DataTable ky = LayBangKy(maCT);

            decimal phaiThu = 0;
            decimal daThu = 0;
            decimal noDenHan = 0;

            foreach (DataRow r in ky.Rows)
            {
                decimal phai =
                    Convert.ToDecimal(r["PhaiThu"]);

                decimal da =
                    Convert.ToDecimal(r["DaThu"]);

                decimal con =
                    Convert.ToDecimal(r["ConLai"]);

                DateTime han =
                    Convert.ToDateTime(r["HanThu"]);

                phaiThu += phai;
                daThu += da;

                if (han.Date <= DateTime.Today)
                    noDenHan += con;
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("TongPhaiThu", typeof(decimal));
            dt.Columns.Add("TongDaThu", typeof(decimal));
            dt.Columns.Add("NoDenHan", typeof(decimal));

            dt.Rows.Add(phaiThu, daThu, noDenHan);
            return dt;
        }

        // =========================================
        // 8. GHI NHẬN THANH TOÁN
        // =========================================

        public void ThuTien(
            string maThanhToan,
            string maCT,
            DateTime ngayThanhToan,
            DateTime tuNgay,
            DateTime denNgay,
            int soThang,
            decimal soTien,
            string phuongThuc,
            string ghiChu)
        {
            if (string.IsNullOrWhiteSpace(maThanhToan) ||
                maThanhToan.Trim().Length > 20)
                throw new Exception(
                    "Mã thanh toán phải từ 1 đến 20 ký tự.");

            if (string.IsNullOrWhiteSpace(maCT))
                throw new Exception("Hãy chọn phòng thuê.");

            if (phuongThuc != "Tiền mặt" &&
                phuongThuc != "Chuyển khoản")
                throw new Exception(
                    "Phương thức thanh toán không hợp lệ.");

            if (soTien <= 0 || soThang <= 0)
                throw new Exception(
                    "Số tiền và số tháng phải lớn hơn 0.");

            using (SqlConnection c = Db.OpenConnection())
            using (SqlTransaction t =
                c.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                    // Khóa phòng trước khi kiểm tra và thu tiền.
                    object maPhong = null;

                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT MaPhong
                        FROM dbo.ChiTietHopDong
                        WHERE MaChiTietHD = @MaCT", c, t))
                    {
                        cmd.Parameters.AddWithValue("@MaCT", maCT);
                        maPhong = cmd.ExecuteScalar();
                    }

                    if (maPhong == null)
                        throw new Exception(
                            "Không tìm thấy phòng thuê.");

                    using (SqlCommand cmd = new SqlCommand(@"
                        SELECT MaPhong
                        FROM dbo.Phong WITH (UPDLOCK, HOLDLOCK)
                        WHERE MaPhong = @MaPhong", c, t))
                    {
                        cmd.Parameters.AddWithValue(
                            "@MaPhong", Convert.ToString(maPhong));

                        if (cmd.ExecuteScalar() == null)
                            throw new Exception(
                                "Không tìm thấy phòng.");
                    }

                    List<KyThanhToan> ds =
                        LayCacKy(c, t, maCT);

                    KyThanhToan ky = ds.FirstOrDefault(
                        x => x.TuNgay == tuNgay.Date &&
                             x.DenNgay == denNgay.Date &&
                             x.SoThang == soThang);

                    if (ky == null)
                        throw new Exception(
                            "Kỳ thanh toán không hợp lệ.");

                    if (ky.DaThu > 0)
                        throw new Exception(
                            "Kỳ này đã phát sinh thanh toán. " +
                            "Không được tạo phiếu thu trùng.");

                    if (Math.Abs(soTien - ky.SoTien) > 0.01m)
                        throw new Exception(
                            "Số tiền phải bằng " +
                            ky.SoTien.ToString("N0") + " đ.");

                    // Không cho thu khi đã đến kỳ kế tiếp
                    // mà bỏ qua kỳ trước chưa trả.
                    bool chuaTraKyTruoc = ds.Any(x =>
                        x.TuNgay < ky.TuNgay &&
                        x.DaThu < x.SoTien - 0.01m);

                    if (chuaTraKyTruoc)
                        throw new Exception(
                            "Cần thanh toán các kỳ trước " +
                            "còn nợ trước khi thu kỳ này.");

                    int tonTai = Convert.ToInt32(
                        Scalar(c, t, @"
                            SELECT COUNT(*)
                            FROM dbo.ThanhToanTienThue
                            WHERE MaThanhToan = @Ma",
                            P("@Ma", maThanhToan.Trim())));

                    if (tonTai > 0)
                        throw new Exception(
                            "Mã thanh toán đã tồn tại.");

                    Execute(c, t, @"
                        INSERT INTO dbo.ThanhToanTienThue
                        (
                            MaThanhToan,
                            MaChiTietHD,
                            NgayThanhToan,
                            TuNgay,
                            DenNgay,
                            SoThangThanhToan,
                            SoTien,
                            PhuongThuc,
                            GhiChu
                        )
                        VALUES
                        (
                            @Ma, @CT, @Ngay,
                            @Tu, @Den, @Thang,
                            @Tien, @PhuongThuc, @GhiChu
                        )",
                        P("@Ma", maThanhToan.Trim()),
                        P("@CT", maCT),
                        P("@Ngay", ngayThanhToan.Date),
                        P("@Tu", tuNgay.Date),
                        P("@Den", denNgay.Date),
                        P("@Thang", soThang),
                        P("@Tien", soTien),
                        P("@PhuongThuc", phuongThuc),
                        P("@GhiChu",
                            string.IsNullOrWhiteSpace(ghiChu)
                            ? (object)DBNull.Value
                            : ghiChu.Trim()));

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

        private static object Scalar(
            SqlConnection c, SqlTransaction t,
            string sql, params SqlParameter[] ps)
        {
            using (SqlCommand cmd =
                new SqlCommand(sql, c, t))
            {
                cmd.Parameters.AddRange(ps);
                return cmd.ExecuteScalar();
            }
        }
    }
}
