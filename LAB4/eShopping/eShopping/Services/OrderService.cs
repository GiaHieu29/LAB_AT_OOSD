using System.Text;

public class OrderService
{
    private readonly IOrderRepository _orders;
    private readonly IPaymentGateway _gateway;
    private readonly IEmailSender _email;
    private readonly ShippingService _shipping;
    private readonly IConfigRepository _cfg;

    public OrderService(IOrderRepository orders, IPaymentGateway gateway,
                        IEmailSender email, ShippingService shipping, IConfigRepository cfg)
    {
        _orders = orders; _gateway = gateway; _email = email;
        _shipping = shipping; _cfg = cfg;
    }

    public OrderResult DatHang(KhachHang kh, Cart cart, string maLoaiPhieu,
                               NguoiNhan nn, CardInfo card)
    {
        if (kh == null) return OrderResult.Fail("Cần đăng nhập.");                 // BR13
        if (cart.Items.Count == 0) return OrderResult.Fail("Giỏ hàng trống.");

        var loi = CardValidator.Validate(card);                                    // BR09
        if (loi != null) return OrderResult.Fail(loi);

        decimal tienHang = cart.TienHang;
        decimal phiGiao = _shipping.TinhPhi(tienHang, maLoaiPhieu, nn.MaKhuVuc);   // BR05-07
        decimal lePhi = _cfg.LayLePhiThe(card.MaLoaiThe);                          // BR10
        decimal tong = tienHang + phiGiao + lePhi;

        var pay = _gateway.Authorize(card, tong);                                  // hệ thống ngoài
        if (!pay.ThanhCong) return OrderResult.Fail(pay.LyDo);                     // BR11

        int maDon = _orders.Save(kh, cart, maLoaiPhieu, nn, card, pay,
                                 tienHang, phiGiao, lePhi, tong);

        string body = null;
        if (!string.IsNullOrEmpty(kh.Email))                                       // BR12
            body = BuildEmailBody(maDon, cart, nn, tienHang, phiGiao, lePhi, tong);

        cart.Clear();

        if (body != null)
            _email.Send(kh.Email, "Xác nhận đơn hàng #" + maDon, body);

        return OrderResult.Ok(maDon, tong);
    }

    // Không đưa bất kỳ thông tin thẻ nào vào email (BR12)
    private static string BuildEmailBody(int maDon, Cart cart, NguoiNhan nn,
                                         decimal tienHang, decimal phiGiao, decimal lePhi, decimal tong)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Đơn hàng #" + maDon + " đã được ghi nhận.");
        sb.AppendLine("Người nhận: " + nn.HoTen + " | " + nn.DiaChi + " | " + nn.DienThoai);
        sb.AppendLine("Sản phẩm:");
        foreach (var i in cart.Items)
            sb.AppendLine("- " + i.SP.TenSP + " x" + i.SoLuong + " x " +
                          i.SP.GiaHienHanh.ToString("N0") + " = " + i.ThanhTien.ToString("N0"));
        sb.AppendLine("Tiền hàng: " + tienHang.ToString("N0"));
        sb.AppendLine("Phí giao hàng: " + phiGiao.ToString("N0"));
        sb.AppendLine("Lệ phí thẻ: " + lePhi.ToString("N0"));
        sb.AppendLine("TỔNG: " + tong.ToString("N0") + " đ");
        return sb.ToString();
    }
}