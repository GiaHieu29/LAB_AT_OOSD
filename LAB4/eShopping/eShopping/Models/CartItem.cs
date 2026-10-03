public class CartItem
{
    public SanPham SP;
    public int SoLuong;
    public decimal ThanhTien => SP.GiaHienHanh * SoLuong;
}