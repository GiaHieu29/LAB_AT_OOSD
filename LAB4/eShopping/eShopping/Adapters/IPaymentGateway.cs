public class PaymentResult
{
    public bool ThanhCong;
    public string MaGiaoDich, LyDo;
}

public interface IPaymentGateway
{
    PaymentResult Authorize(CardInfo card, decimal soTien);
}

public class MockPaymentGateway : IPaymentGateway
{
    // Quy ước demo: số thẻ kết thúc 0000 bị từ chối; số tiền > 50 triệu không đủ khả năng thanh toán
    public PaymentResult Authorize(CardInfo c, decimal soTien)
    {
        if (c.SoThe.EndsWith("0000"))
            return new PaymentResult { ThanhCong = false, LyDo = "Thẻ không hợp lệ." };
        if (soTien > 50000000)
            return new PaymentResult { ThanhCong = false, LyDo = "Thẻ không đủ khả năng thanh toán." };
        return new PaymentResult
        {
            ThanhCong = true,
            MaGiaoDich = "TXN" + System.Guid.NewGuid().ToString("N").Substring(0, 12)
        };
    }
}