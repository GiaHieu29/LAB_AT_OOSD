using System;
using System.Data.SqlClient;
using System.Dynamic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

public interface IOrderRepository
{
    int Save(KhachHang kh, Cart cart, string maLoaiPhieu, NguoiNhan nn,
             CardInfo card, PaymentResult pay,
             decimal tienHang, decimal phiGiao, decimal lePhi, decimal tong);
}

public class OrderRepository : IOrderRepository
{
    public int Save(KhachHang kh, Cart cart, string maLoaiPhieu, NguoiNhan nn,
                    CardInfo card, PaymentResult pay,
                    decimal tienHang, decimal phiGiao, decimal lePhi, decimal tong)
    {
        using (var cn = Db.Open())
        using (var tx = cn.BeginTransaction())
        {
            try
            {
                // 1. Người nhận (BR08: bản ghi riêng, khác khách hàng)
                int maNguoiNhan;
                using (var cmd = new SqlCommand(
                    @"INSERT INTO NguoiNhan (HoTen, DiaChi, DienThoai, MaKhuVuc)
                      OUTPUT INSERTED.MaNguoiNhan
                      VALUES (@ht, @dc, @dt, @kv)", cn, tx))
                {
                    cmd.Parameters.AddWithValue("@ht", nn.HoTen);
                    cmd.Parameters.AddWithValue("@dc", nn.DiaChi);
                    cmd.Parameters.AddWithValue("@dt", nn.DienThoai);
                    cmd.Parameters.AddWithValue("@kv", nn.MaKhuVuc);
                    maNguoiNhan = (int)cmd.ExecuteScalar();
                }

                // 2. Thẻ tín dụng: CHỈ lưu 4 số cuối, không lưu số đầy đủ và CSV
                int maThe;
                using (var cmd = new SqlCommand(
                    @"INSERT INTO TheTinDung (MaLoaiThe, BonSoCuoi, NgayHetHan, TenChuThe)
                      OUTPUT INSERTED.MaThe
                      VALUES (@lt, @cuoi, @hh, @ten)", cn, tx))
                {
                    cmd.Parameters.AddWithValue("@lt", card.MaLoaiThe);
                    cmd.Parameters.AddWithValue("@cuoi", card.SoThe.Substring(card.SoThe.Length - 4));
                    cmd.Parameters.AddWithValue("@hh", card.NgayHetHan);
                    cmd.Parameters.AddWithValue("@ten", card.TenChuThe);
                    maThe = (int)cmd.ExecuteScalar();
                }

                // 3. Giao dịch thanh toán (mã do dịch vụ thanh toán trả về)
                using (var cmd = new SqlCommand(
                    @"INSERT INTO GiaoDichThanhToan (MaGiaoDich, MaThe, KetQua)
                      VALUES (@gd, @the, 'THANHCONG')", cn, tx))
                {
                    cmd.Parameters.AddWithValue("@gd", pay.MaGiaoDich);
                    cmd.Parameters.AddWithValue("@the", maThe);
                    cmd.ExecuteNonQuery();
                }

                // 4. Đơn đặt hàng
                int maDon;
                using (var cmd = new SqlCommand(
                    @"INSERT INTO DonDatHang
                        (MaKH, MaNguoiNhan, MaLoaiPhieu, MaGiaoDich,
                         TienHang, PhiGiao, LePhiThe, TongHoaDon)
                      OUTPUT INSERTED.MaDon
                      VALUES (@kh, @nn, @lp, @gd, @th, @pg, @lephi, @tong)", cn, tx))
                {
                    cmd.Parameters.AddWithValue("@kh", kh.MaKH);
                    cmd.Parameters.AddWithValue("@nn", maNguoiNhan);
                    cmd.Parameters.AddWithValue("@lp", maLoaiPhieu);
                    cmd.Parameters.AddWithValue("@gd", pay.MaGiaoDich);
                    cmd.Parameters.AddWithValue("@th", tienHang);
                    cmd.Parameters.AddWithValue("@pg", phiGiao);
                    cmd.Parameters.AddWithValue("@lephi", lePhi);
                    cmd.Parameters.AddWithValue("@tong", tong);
                    maDon = (int)cmd.ExecuteScalar();
                }

                // 5. Chi tiết đơn: DonGia = giá chốt tại thời điểm đặt (BR02)
                foreach (var item in cart.Items)
                {
                    using (var cmd = new SqlCommand(
                        @"INSERT INTO ChiTietDonHang (MaDon, MaSP, SoLuong, DonGia)
                          VALUES (@don, @sp, @sl, @gia)", cn, tx))
                    {
                        cmd.Parameters.AddWithValue("@don", maDon);
                        cmd.Parameters.AddWithValue("@sp", item.SP.MaSP);
                        cmd.Parameters.AddWithValue("@sl", item.SoLuong);
                        cmd.Parameters.AddWithValue("@gia", item.SP.GiaHienHanh);
                        cmd.ExecuteNonQuery();
                    }
                }

                tx.Commit();
                return maDon;
            }
            catch
            {
                tx.Rollback();   // lỗi giữa chừng thì không lưu gì cả
                throw;
            }
        }
    }
}