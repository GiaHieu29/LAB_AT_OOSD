public static class AppSession
{
    public static KhachHang KhachHienTai;
    public static Cart Cart = new Cart();

    // Thứ tự khai báo quan trọng: cái sau dùng cái trước
    public static IProductCatalogAdapter Catalog = new ProductRepository();
    public static IConfigRepository Config = new ConfigRepository();
    public static AuthService Auth = new AuthService(new CustomerRepository());
    public static MockEmailSender Email = new MockEmailSender();
    public static ShippingService Shipping = new ShippingService(Config);
    public static OrderService Orders = new OrderService(
        new OrderRepository(), new MockPaymentGateway(), Email, Shipping, Config);
}