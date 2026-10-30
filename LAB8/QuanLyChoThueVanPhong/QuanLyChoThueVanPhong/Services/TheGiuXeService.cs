
using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyChoThueVanPhong.Data;

namespace QuanLyChoThueVanPhong.Services
{
    public class TheGiuXeService
    {
        // =========================================
        // 1. DANH SÁCH THẺ VÀ TÌM KIẾM
        // =========================================
        public DataTable LayDanhSach(string tuKhoa = "")
        {
            string key = (tuKhoa ?? "").Trim();

            return Db.Query(@"
                SELECT
                    tx.MaThe,
                    tx.MaNhanVien,
                    nv.HoTen,
                    kh.TenKhachHang,
                    tx.BienSoXe,
                    tx.LoaiXe,
                    tx.MauXe,
                    tx.NgayCap,
                    tx.TrangThai
                FROM dbo.TheGiuXe tx
                INNER JOIN dbo.NhanVienVanPhong nv
                    ON tx.MaNhanVien = nv.MaNhanVien
                INNER JOIN dbo.KhachHang kh
                    ON nv.MaKhachHang = kh.MaKhachHang
                WHERE tx.MaThe LIKE @Key
                   OR tx.MaNhanVien LIKE @Key
                   OR nv.HoTen LIKE @Key
                   OR kh.TenKhachHang LIKE @Key
                   OR tx.BienSoXe LIKE @Key
                   OR tx.LoaiXe LIKE @Key
                   OR tx.TrangThai LIKE @Key
                ORDER BY tx.NgayCap DESC, tx.MaThe",
                Db.P("@Key", "%" + key + "%"));
        }

        // =========================================
        // 2. DANH SÁCH NHÂN VIÊN
        // =========================================
        public DataTable LayNhanVien()
        {
            return Db.Query(@"
                SELECT
                    nv.MaNhanVien,
                    nv.MaNhanVien + N' - ' +
                    nv.HoTen + N' (' +
                    kh.TenKhachHang + N')' AS HienThi
                FROM dbo.NhanVienVanPhong nv
                INNER JOIN dbo.KhachHang kh
                    ON nv.MaKhachHang = kh.MaKhachHang
                ORDER BY nv.HoTen, nv.MaNhanVien");
        }

        // =========================================
        // 3. KIỂM TRA ĐẦU VÀO
        // =========================================
        private static void KiemTra(
            string maThe,
            string maNhanVien,
            string bienSo,
            string loaiXe,
            string mauXe,
            DateTime ngayCap,
            string trangThai)
        {
            if (string.IsNullOrWhiteSpace(maThe) ||
                maThe.Trim().Length > 20)
                throw new Exception(
                    "Mã thẻ phải từ 1 đến 20 ký tự.");

            if (string.IsNullOrWhiteSpace(maNhanVien))
                throw new Exception(
                    "Hãy chọn nhân viên được cấp thẻ.");

            if (string.IsNullOrWhiteSpace(bienSo))
                throw new Exception(
                    "Vui lòng nhập biển số xe.");

            if (bienSo.Trim().Length > 30)
                throw new Exception(
                    "Biển số xe không được quá 30 ký tự.");

            if (string.IsNullOrWhiteSpace(loaiXe))
                throw new Exception(
                    "Hãy chọn loại xe.");

            if (loaiXe.Trim().Length > 100)
                throw new Exception(
                    "Loại xe không được quá 100 ký tự.");

            if (!string.IsNullOrWhiteSpace(mauXe) &&
                mauXe.Trim().Length > 100)
                throw new Exception(
                    "Màu xe không được quá 100 ký tự.");

            if (ngayCap.Date > DateTime.Today)
                throw new Exception(
                    "Ngày cấp không được ở tương lai.");

            if (trangThai != "Đang sử dụng" &&
                trangThai != "Đã thu hồi" &&
                trangThai != "Bị mất")
                throw new Exception(
                    "Trạng thái thẻ không hợp lệ.");
        }

        // =========================================
        // 4. HELPER SQL TRANSACTION
        // =========================================
        private static SqlParameter P(
            string name, object value)
        {
            return new SqlParameter(
                name, value ?? DBNull.Value);
        }

        private static object NullNeuRong(string text)
        {
            return string.IsNullOrWhiteSpace(text)
                ? (object)DBNull.Value
                : text.Trim();
        }

        private static object Scalar(
            SqlConnection conn,
            SqlTransaction tran,
            string sql,
            params SqlParameter[] ps)
        {
            using (SqlCommand cmd =
                new SqlCommand(sql, conn, tran))
            {
                cmd.Parameters.AddRange(ps);
                return cmd.ExecuteScalar();
            }
        }

        private static int Execute(
            SqlConnection conn,
            SqlTransaction tran,
            string sql,
            params SqlParameter[] ps)
        {
            using (SqlCommand cmd =
                new SqlCommand(sql, conn, tran))
            {
                cmd.Parameters.AddRange(ps);
                return cmd.ExecuteNonQuery();
            }
        }

        // =========================================
        // 5. THÊM THẺ
        // =========================================
        public void Them(
            string maThe,
            string maNhanVien,
            string bienSo,
            string loaiXe,
            string mauXe,
            DateTime ngayCap,
            string trangThai)
        {
            Luu(
                maThe, maNhanVien, bienSo,
                loaiXe, mauXe, ngayCap,
                trangThai, false);
        }

        // =========================================
        // 6. SỬA THẺ
        // =========================================
        public void Sua(
            string maThe,
            string maNhanVien,
            string bienSo,
            string loaiXe,
            string mauXe,
            DateTime ngayCap,
            string trangThai)
        {
            Luu(
                maThe, maNhanVien, bienSo,
                loaiXe, mauXe, ngayCap,
                trangThai, true);
        }

        // =========================================
        // 7. LƯU THẺ CÓ KIỂM TRA NGHIỆP VỤ
        // =========================================
        private void Luu(
            string maThe,
            string maNhanVien,
            string bienSo,
            string loaiXe,
            string mauXe,
            DateTime ngayCap,
            string trangThai,
            bool sua)
        {
            KiemTra(
                maThe, maNhanVien, bienSo,
                loaiXe, mauXe, ngayCap, trangThai);

            maThe = maThe.Trim();
            maNhanVien = maNhanVien.Trim();
            bienSo = bienSo.Trim().ToUpperInvariant();

            using (SqlConnection conn = Db.OpenConnection())
            using (SqlTransaction tran =
                conn.BeginTransaction(
                    IsolationLevel.Serializable))
            {
                try
                {
                    // Kiểm tra nhân viên và trạng thái.
                    object ttNhanVien = Scalar(
                        conn, tran, @"
                        SELECT TrangThai
                        FROM dbo.NhanVienVanPhong
                            WITH (UPDLOCK, HOLDLOCK)
                        WHERE MaNhanVien = @MaNV",
                        P("@MaNV", maNhanVien));

                    if (ttNhanVien == null)
                        throw new Exception(
                            "Không tìm thấy nhân viên.");

                    if (trangThai == "Đang sử dụng" &&
                        Convert.ToString(ttNhanVien) !=
                        "Đang làm việc")
                        throw new Exception(
                            "Không thể cấp thẻ đang sử dụng " +
                            "cho nhân viên đã nghỉ việc.");

                    // Kiểm tra mã thẻ khi thêm/sửa.
                    int soThe = Convert.ToInt32(
                        Scalar(conn, tran, @"
                        SELECT COUNT(*)
                        FROM dbo.TheGiuXe
                            WITH (UPDLOCK, HOLDLOCK)
                        WHERE MaThe = @MaThe",
                        P("@MaThe", maThe)));

                    if (!sua && soThe > 0)
                        throw new Exception(
                            "Mã thẻ đã tồn tại.");

                    if (sua && soThe == 0)
                        throw new Exception(
                            "Không tìm thấy thẻ cần sửa.");

                    // Chỉ một thẻ Đang sử dụng
                    // cho mỗi biển số tại một thời điểm.
                    if (trangThai == "Đang sử dụng")
                    {
                        int trung = Convert.ToInt32(
                            Scalar(conn, tran, @"
                            SELECT COUNT(*)
                            FROM dbo.TheGiuXe
                                WITH (UPDLOCK, HOLDLOCK)
                            WHERE UPPER(
                                REPLACE(BienSoXe, ' ', '')
                            ) = @BienSo
                              AND TrangThai = N'Đang sử dụng'
                              AND MaThe <> @MaThe",
                            P("@BienSo",
                                bienSo.Replace(" ", "")),
                            P("@MaThe", maThe)));

                        if (trung > 0)
                            throw new Exception(
                                "Biển số xe này đang có " +
                                "một thẻ giữ xe hoạt động. " +
                                "Hãy thu hồi thẻ cũ trước.");
                    }

                    if (sua)
                    {
                        int n = Execute(
                            conn, tran, @"
                            UPDATE dbo.TheGiuXe
                            SET MaNhanVien = @MaNV,
                                BienSoXe = @BienSo,
                                LoaiXe = @LoaiXe,
                                MauXe = @MauXe,
                                NgayCap = @NgayCap,
                                TrangThai = @TrangThai
                            WHERE MaThe = @MaThe",
                            P("@MaThe", maThe),
                            P("@MaNV", maNhanVien),
                            P("@BienSo", bienSo),
                            P("@LoaiXe", loaiXe.Trim()),
                            P("@MauXe", NullNeuRong(mauXe)),
                            P("@NgayCap", ngayCap.Date),
                            P("@TrangThai", trangThai));

                        if (n != 1)
                            throw new Exception(
                                "Không cập nhật được thẻ.");
                    }
                    else
                    {
                        Execute(conn, tran, @"
                            INSERT INTO dbo.TheGiuXe
                            (
                                MaThe,
                                MaNhanVien,
                                BienSoXe,
                                LoaiXe,
                                MauXe,
                                NgayCap,
                                TrangThai
                            )
                            VALUES
                            (
                                @MaThe,
                                @MaNV,
                                @BienSo,
                                @LoaiXe,
                                @MauXe,
                                @NgayCap,
                                @TrangThai
                            )",
                            P("@MaThe", maThe),
                            P("@MaNV", maNhanVien),
                            P("@BienSo", bienSo),
                            P("@LoaiXe", loaiXe.Trim()),
                            P("@MauXe", NullNeuRong(mauXe)),
                            P("@NgayCap", ngayCap.Date),
                            P("@TrangThai", trangThai));
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

        // =========================================
        // 8. XÓA THẺ
        // =========================================
        public void Xoa(string maThe)
        {
            if (string.IsNullOrWhiteSpace(maThe))
                throw new Exception(
                    "Hãy chọn thẻ cần xóa.");

            // Không xóa thẻ đang sử dụng:
            // phải chuyển sang Đã thu hồi hoặc Bị mất.
            using (SqlConnection conn = Db.OpenConnection())
            using (SqlTransaction tran =
                conn.BeginTransaction(
                    IsolationLevel.Serializable))
            {
                try
                {
                    object trangThai = Scalar(
                        conn, tran, @"
                        SELECT TrangThai
                        FROM dbo.TheGiuXe
                            WITH (UPDLOCK, HOLDLOCK)
                        WHERE MaThe = @Ma",
                        P("@Ma", maThe.Trim()));

                    if (trangThai == null)
                        throw new Exception(
                            "Không tìm thấy thẻ.");

                    if (Convert.ToString(trangThai) ==
                        "Đang sử dụng")
                        throw new Exception(
                            "Thẻ vẫn đang sử dụng. " +
                            "Hãy đổi sang Đã thu hồi " +
                            "hoặc Bị mất trước khi xóa.");

                    int n = Execute(conn, tran, @"
                        DELETE FROM dbo.TheGiuXe
                        WHERE MaThe = @Ma",
                        P("@Ma", maThe.Trim()));

                    if (n != 1)
                        throw new Exception(
                            "Không xóa được thẻ.");

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
