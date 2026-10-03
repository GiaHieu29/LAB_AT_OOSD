using System;
using System.Data.SqlClient;

public class CustomerRepository
{
    public bool Ton_Tai(string cot, string giaTri)
    {
        // cot chỉ nhận giá trị cố định do code truyền vào (không lấy từ người dùng)
        using (var cn = Db.Open())
        using (var cmd = new SqlCommand("SELECT COUNT(*) FROM KhachHang WHERE " + cot + "=@v", cn))
        {
            cmd.Parameters.AddWithValue("@v", giaTri);
            return (int)cmd.ExecuteScalar() > 0;
        }
    }

    public int Them(string hoTen, DateTime ngaySinh, string cmnd, string diaChi,
                    string dienThoai, string tenDangNhap, string matKhauHash, string email)
    {
        using (var cn = Db.Open())
        using (var cmd = new SqlCommand(
            @"INSERT INTO KhachHang (HoTen, NgaySinh, CMND_Passport, DiaChi, DienThoai, TenDangNhap, MatKhauHash, Email)
              OUTPUT INSERTED.MaKH
              VALUES (@ht, @ns, @cmnd, @dc, @dt, @tdn, @mk, @em)", cn))
        {
            cmd.Parameters.AddWithValue("@ht", hoTen);
            cmd.Parameters.AddWithValue("@ns", ngaySinh);
            cmd.Parameters.AddWithValue("@cmnd", cmnd);
            cmd.Parameters.AddWithValue("@dc", diaChi);
            cmd.Parameters.AddWithValue("@dt", dienThoai);
            cmd.Parameters.AddWithValue("@tdn", tenDangNhap);
            cmd.Parameters.AddWithValue("@mk", matKhauHash);
            cmd.Parameters.AddWithValue("@em", string.IsNullOrWhiteSpace(email) ? (object)DBNull.Value : email);
            return (int)cmd.ExecuteScalar();
        }
    }

    public KhachHang Lay(string tenDangNhap, out string hash)
    {
        hash = null;
        using (var cn = Db.Open())
        using (var cmd = new SqlCommand(
            "SELECT MaKH, HoTen, Email, MatKhauHash FROM KhachHang WHERE TenDangNhap=@u", cn))
        {
            cmd.Parameters.AddWithValue("@u", tenDangNhap);
            using (var r = cmd.ExecuteReader())
            {
                if (!r.Read()) return null;
                hash = r.GetString(3);
                return new KhachHang
                {
                    MaKH = r.GetInt32(0),
                    HoTen = r.GetString(1),
                    Email = r.IsDBNull(2) ? null : r.GetString(2)
                };
            }
        }
    }
}