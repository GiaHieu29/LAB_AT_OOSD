using System.Collections.Generic;

namespace QuanLyKhachSan
{
    /// <summary>
    /// Kết quả trả về chuẩn cho mọi thao tác nghiệp vụ (Service) gọi từ Form.
    /// Dùng để Form chỉ cần hiển thị ThongBao, không cần biết chi tiết xử lý.
    /// </summary>
    public class KetQuaXuLy
    {
        public bool ThanhCong { get; set; }
        public string ThongBao { get; set; }

        public static KetQuaXuLy Ok(string thongBao)
        {
            return new KetQuaXuLy { ThanhCong = true, ThongBao = thongBao };
        }

        public static KetQuaXuLy Fail(string thongBao)
        {
            return new KetQuaXuLy { ThanhCong = false, ThongBao = thongBao };
        }
    }

    /// <summary>
    /// Một dòng phòng được chọn khi lập phiếu đặt phòng (FrmDatPhong).
    /// Dùng làm nguồn dữ liệu cho BindingList hiển thị trong DataGridView "Phòng chọn".
    /// </summary>
    public class PhongDatItem
    {
        public string SoPhong { get; set; }
        public int SoNguoi { get; set; }
        public decimal DonGiaNgay { get; set; }
    }

    /// <summary>
    /// Một dòng tiện nghi bị hư hỏng/mất khi lập phiếu đền bù (FrmTraPhong).
    /// </summary>
    public class DenBuItem
    {
        public string MaTienNghi { get; set; }
        public string TenLoaiTN { get; set; }
        public string MucDoThietHai { get; set; }
        public decimal SoTien { get; set; }
    }
}
