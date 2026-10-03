public class OrderResult
{
    public bool ThanhCong;
    public int MaDon;
    public decimal TongTien;
    public string Loi;

    public static OrderResult Ok(int maDon, decimal tong)
    {
        return new OrderResult { ThanhCong = true, MaDon = maDon, TongTien = tong };
    }
    public static OrderResult Fail(string loi)
    {
        return new OrderResult { ThanhCong = false, Loi = loi };
    }
}