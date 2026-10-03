using System.Linq;
using System.Text.RegularExpressions;

public static class CardValidator
{
    // Trả null nếu hợp lệ, ngược lại trả thông báo lỗi (BR09)
    public static string Validate(CardInfo c)
    {
        bool amex = c.MaLoaiThe == "AMEX";
        int lenSo = amex ? 15 : 16, lenCsv = amex ? 4 : 3;

        if (c.SoThe == null || c.SoThe.Length != lenSo || !c.SoThe.All(char.IsDigit))
            return "Số thẻ phải gồm " + lenSo + " chữ số.";
        if (c.CSV == null || c.CSV.Length != lenCsv || !c.CSV.All(char.IsDigit))
            return "CSV phải gồm " + lenCsv + " chữ số.";
        if (c.NgayHetHan == null || !Regex.IsMatch(c.NgayHetHan, @"^(0[1-9]|1[0-2])/\d{2}$"))
            return "Ngày hết hạn phải có dạng MM/yy.";
        if (string.IsNullOrWhiteSpace(c.TenChuThe))
            return "Thiếu tên chủ thẻ.";
        return null;
    }
}