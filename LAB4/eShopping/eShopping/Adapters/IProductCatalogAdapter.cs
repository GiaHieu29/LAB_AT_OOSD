using System.Collections.Generic;

public interface IProductCatalogAdapter
{
    List<KeyValuePair<string, string>> LayNhom();   // (MaNhom, TenNhom)
    List<SanPham> LayTheoNhom(string maNhom);
    SanPham LayChiTiet(string maSP);
}