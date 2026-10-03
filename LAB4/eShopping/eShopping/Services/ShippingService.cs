public class ShippingService
{
    private readonly IConfigRepository _cfg;
    public ShippingService(IConfigRepository cfg) { _cfg = cfg; }

    public decimal TinhPhi(decimal tienHang, string maLoaiPhieu, string maKhuVuc)
    {
        if (maLoaiPhieu == "NHANH_NGAY" && tienHang >= 5000000) return 0;   // BR06
        if (maLoaiPhieu == "NHANH" && tienHang >= 1000000) return 0;        // BR05
        return _cfg.LayPhi(maKhuVuc, maLoaiPhieu);                          // BR07
    }
}