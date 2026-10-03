using System.Collections.Generic;
using System.Data.SqlClient;

public class ProductRepository : IProductCatalogAdapter
{
    private const string Cols =
        "MaSP, TenSP, NhaSX, MoTa, ThongSoKT, GiaHienHanh, ConHang, MaNhom";

    public List<KeyValuePair<string, string>> LayNhom()
    {
        var ds = new List<KeyValuePair<string, string>>();
        using (var cn = Db.Open())
        using (var cmd = new SqlCommand("SELECT MaNhom, TenNhom FROM NhomSanPham ORDER BY TenNhom", cn))
        using (var r = cmd.ExecuteReader())
            while (r.Read())
                ds.Add(new KeyValuePair<string, string>(r.GetString(0), r.GetString(1)));
        return ds;
    }

    public List<SanPham> LayTheoNhom(string maNhom)
    {
        var ds = new List<SanPham>();
        using (var cn = Db.Open())
        using (var cmd = new SqlCommand("SELECT " + Cols + " FROM SanPham WHERE MaNhom=@n ORDER BY TenSP", cn))
        {
            cmd.Parameters.AddWithValue("@n", maNhom);
            using (var r = cmd.ExecuteReader())
                while (r.Read()) ds.Add(Map(r));
        }
        return ds;
    }

    public SanPham LayChiTiet(string maSP)
    {
        using (var cn = Db.Open())
        using (var cmd = new SqlCommand("SELECT " + Cols + " FROM SanPham WHERE MaSP=@m", cn))
        {
            cmd.Parameters.AddWithValue("@m", maSP);
            using (var r = cmd.ExecuteReader())
                return r.Read() ? Map(r) : null;
        }
    }

    private static string S(SqlDataReader r, int i)
    {
        return r.IsDBNull(i) ? "" : r.GetString(i);
    }

    private static SanPham Map(SqlDataReader r)
    {
        return new SanPham
        {
            MaSP = r.GetString(0),
            TenSP = r.GetString(1),
            NhaSX = S(r, 2),
            MoTa = S(r, 3),
            ThongSoKT = S(r, 4),
            GiaHienHanh = r.GetDecimal(5),
            ConHang = r.GetBoolean(6),
            MaNhom = r.GetString(7)
        };
    }
}