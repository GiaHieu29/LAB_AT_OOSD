using System;
using System.Collections.Generic;
using System.Linq;

public class Cart
{
    private readonly List<CartItem> _items = new List<CartItem>();
    public IReadOnlyList<CartItem> Items => _items;
    public decimal TienHang => _items.Sum(i => i.ThanhTien);

    public void Add(SanPham sp, int qty = 1)
    {
        if (!sp.ConHang) throw new InvalidOperationException("Sản phẩm đã hết hàng."); // BR03
        var it = _items.FirstOrDefault(i => i.SP.MaSP == sp.MaSP);
        if (it == null) _items.Add(new CartItem { SP = sp, SoLuong = qty });
        else it.SoLuong += qty;
    }
    public void Remove(string maSP) { _items.RemoveAll(i => i.SP.MaSP == maSP); }
    public void SetQty(string maSP, int qty)
    {
        if (qty <= 0) { Remove(maSP); return; }
        _items.First(i => i.SP.MaSP == maSP).SoLuong = qty;
    }
    public void Clear() { _items.Clear(); }
}