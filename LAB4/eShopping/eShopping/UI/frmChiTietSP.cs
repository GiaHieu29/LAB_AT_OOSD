using System;
using System.Drawing;
using System.Windows.Forms;

public partial class frmChiTietSP : Form
{
    private readonly SanPham _sp;

    public frmChiTietSP(string maSP)
    {
        _sp = AppSession.Catalog.LayChiTiet(maSP);
        Text = "Chi tiết sản phẩm";
        ClientSize = new Size(480, 400);
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false; MinimizeBox = false;

        var lbl = new Label { Left = 15, Top = 15, Width = 450, Height = 320 };
        lbl.Text =
            "Mã SP: " + _sp.MaSP + "\r\n" +
            "Tên: " + _sp.TenSP + "\r\n" +
            "Nhà sản xuất: " + _sp.NhaSX + "\r\n" +
            "Giá: " + _sp.GiaHienHanh.ToString("N0") + " đ\r\n" +
            "Tình trạng: " + (_sp.ConHang ? "Còn hàng" : "HẾT HÀNG") + "\r\n\r\n" +
            "Mô tả:\r\n" + _sp.MoTa + "\r\n\r\n" +
            "Thông số kỹ thuật:\r\n" + _sp.ThongSoKT;
        Controls.Add(lbl);

        UiHelper.Btn(this, "Thêm vào giỏ", 250, 350, 110, delegate
        {
            try { AppSession.Cart.Add(_sp); UiHelper.Info("Đã thêm vào giỏ hàng."); }
            catch (InvalidOperationException ex) { UiHelper.Error(ex.Message); }
        });
        UiHelper.Btn(this, "Đóng", 370, 350, 95, delegate { Close(); });
    }
}