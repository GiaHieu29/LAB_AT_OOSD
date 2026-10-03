using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public interface IConfigRepository
{
    decimal LayPhi(string maKhuVuc, string maLoaiPhieu);
    decimal LayLePhiThe(string maLoaiThe);
    List<KeyValuePair<string, string>> LayKhuVuc();
    List<KeyValuePair<string, string>> LayLoaiThe();
}

public class ConfigRepository : IConfigRepository
{
    public decimal LayPhi(string maKhuVuc, string maLoaiPhieu)
    {
        using (var cn = Db.Open())
        using (var cmd = new SqlCommand(
            "SELECT Phi FROM BangPhiGiao WHERE MaKhuVuc=@kv AND MaLoaiPhieu=@lp", cn))
        {
            cmd.Parameters.AddWithValue("@kv", maKhuVuc);
            cmd.Parameters.AddWithValue("@lp", maLoaiPhieu);
            object o = cmd.ExecuteScalar();
            if (o == null) throw new InvalidOperationException("Chưa cấu hình phí giao hàng.");
            return Convert.ToDecimal(o);
        }
    }

    public decimal LayLePhiThe(string maLoaiThe)
    {
        using (var cn = Db.Open())
        using (var cmd = new SqlCommand("SELECT LePhi FROM LoaiThe WHERE MaLoaiThe=@m", cn))
        {
            cmd.Parameters.AddWithValue("@m", maLoaiThe);
            object o = cmd.ExecuteScalar();
            if (o == null) throw new InvalidOperationException("Loại thẻ không tồn tại.");
            return Convert.ToDecimal(o);
        }
    }

    public List<KeyValuePair<string, string>> LayKhuVuc()
    {
        return Pairs("SELECT MaKhuVuc, TenKhuVuc FROM KhuVuc");
    }

    public List<KeyValuePair<string, string>> LayLoaiThe()
    {
        return Pairs("SELECT MaLoaiThe, TenLoaiThe FROM LoaiThe");
    }

    private static List<KeyValuePair<string, string>> Pairs(string sql)
    {
        var ds = new List<KeyValuePair<string, string>>();
        using (var cn = Db.Open())
        using (var cmd = new SqlCommand(sql, cn))
        using (var r = cmd.ExecuteReader())
            while (r.Read())
                ds.Add(new KeyValuePair<string, string>(r.GetString(0), r.GetString(1)));
        return ds;
    }
}