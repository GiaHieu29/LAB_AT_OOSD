using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

public class AuthService
{
    private readonly CustomerRepository _repo;
    public AuthService(CustomerRepository repo) { _repo = repo; }

    public static string Hash(string pwd)
    {
        var salt = new byte[16];
        using (var rng = new RNGCryptoServiceProvider()) rng.GetBytes(salt);
        var hash = new Rfc2898DeriveBytes(pwd, salt, 10000).GetBytes(20);
        return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
    }

    public static bool Verify(string pwd, string stored)
    {
        var parts = stored.Split(':');
        if (parts.Length != 2) return false;
        var salt = Convert.FromBase64String(parts[0]);
        var expected = Convert.FromBase64String(parts[1]);
        var actual = new Rfc2898DeriveBytes(pwd, salt, 10000).GetBytes(expected.Length);
        int diff = 0;
        for (int i = 0; i < expected.Length; i++) diff |= expected[i] ^ actual[i];
        return diff == 0;
    }

    // Trả null nếu đăng ký thành công, ngược lại trả thông báo lỗi
    public string DangKy(string hoTen, DateTime ngaySinh, string cmnd, string diaChi,
                         string dienThoai, string tenDangNhap, string matKhau, string email)
    {
        if (string.IsNullOrWhiteSpace(hoTen) || string.IsNullOrWhiteSpace(cmnd) ||
            string.IsNullOrWhiteSpace(diaChi) || string.IsNullOrWhiteSpace(dienThoai) ||
            string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrEmpty(matKhau))
            return "Vui lòng nhập đủ các thông tin bắt buộc.";
        if (matKhau.Length < 6) return "Mật khẩu phải từ 6 ký tự.";
        if (!Regex.IsMatch(dienThoai, @"^\d{8,15}$")) return "Số điện thoại không hợp lệ.";
        if (!string.IsNullOrWhiteSpace(email) && !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            return "Email không hợp lệ.";
        if (ngaySinh > DateTime.Today) return "Ngày sinh không hợp lệ.";
        if (_repo.Ton_Tai("TenDangNhap", tenDangNhap)) return "Tên đăng nhập đã tồn tại.";
        if (_repo.Ton_Tai("CMND_Passport", cmnd)) return "Số CMND/Passport đã được đăng ký.";

        _repo.Them(hoTen.Trim(), ngaySinh, cmnd.Trim(), diaChi.Trim(), dienThoai.Trim(),
                   tenDangNhap.Trim(), Hash(matKhau), email == null ? null : email.Trim());
        return null;
    }

    public KhachHang DangNhap(string tenDangNhap, string matKhau)
    {
        string hash;
        var kh = _repo.Lay(tenDangNhap.Trim(), out hash);
        if (kh == null || !Verify(matKhau, hash)) return null;
        return kh;
    }
}