using System;
using System.Drawing;
using System.Windows.Forms;

public partial class frmDatHang : Form
{
    private readonly RadioButton rbThuong = new RadioButton { Text = "Thường", Left = 10, Top = 22, Width = 90, Checked = true };
    private readonly RadioButton rbNhanh = new RadioButton { Text = "Chuyển phát nhanh", Left = 105, Top = 22, Width = 150 };
    private readonly RadioButton rbNgay = new RadioButton { Text = "Nhanh trong ngày", Left = 260, Top = 22, Width = 150 };
    private readonly ComboBox cboKhuVuc = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 250 };
    private readonly TextBox txtTen = new TextBox { Width = 250 };
    private readonly TextBox txtDiaChi = new TextBox { Width = 250 };
    private readonly TextBox txtDT = new TextBox { Width = 250 };
    private readonly ComboBox cboLoaiThe = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 250 };
    private readonly TextBox txtSoThe = new TextBox { Width = 250, MaxLength = 16 };
    private readonly TextBox txtCSV = new TextBox { Width = 80, MaxLength = 4, UseSystemPasswordChar = true };
    private readonly TextBox txtHetHan = new TextBox { Width = 80, MaxLength = 5 };
    private readonly TextBox txtChuThe = new TextBox { Width = 250 };
    private readonly Label lblTH = new Label { AutoSize = true };
    private readonly Label lblPG = new Label { AutoSize = true };
    private readonly Label lblLP = new Label { AutoSize = true };
    private readonly Label lblTong = new Label { AutoSize = true, Font = new Font("Segoe UI", 11, FontStyle.Bold) };
    private bool _ready;

    public frmDatHang()
    {
        Text = "Đặt hàng và tính tiền";
        ClientSize = new Size(500, 640);
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false; MinimizeBox = false;

        var gb = new GroupBox { Text = "Loại phiếu đặt hàng", Left = 15, Top = 10, Width = 470, Height = 55 };
        gb.Controls.AddRange(new Control[] { rbThuong, rbNhanh, rbNgay });
        Controls.Add(gb);

        int y = 80;
        Controls.Add(new Label { Text = "THÔNG TIN NGƯỜI NHẬN", Left = 15, Top = y, AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold) }); y += 25;
        UiHelper.Row(this, "Họ tên", txtTen, y); y += 32;
        UiHelper.Row(this, "Địa chỉ", txtDiaChi, y); y += 32;
        UiHelper.Row(this, "Điện thoại", txtDT, y); y += 32;
        UiHelper.Row(this, "Khu vực giao hàng", cboKhuVuc, y); y += 42;

        Controls.Add(new Label { Text = "THANH TOÁN BẰNG THẺ TÍN DỤNG", Left = 15, Top = y, AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold) }); y += 25;
        UiHelper.Row(this, "Loại thẻ", cboLoaiThe, y); y += 32;
        UiHelper.Row(this, "Số thẻ", txtSoThe, y); y += 32;
        UiHelper.Row(this, "CSV", txtCSV, y); y += 32;
        UiHelper.Row(this, "Hết hạn (MM/yy)", txtHetHan, y); y += 32;
        UiHelper.Row(this, "Tên chủ thẻ", txtChuThe, y); y += 45;

        UiHelper.Row(this, "Tiền hàng", lblTH, y); y += 25;
        UiHelper.Row(this, "Phí giao hàng", lblPG, y); y += 25;
        UiHelper.Row(this, "Lệ phí thẻ", lblLP, y); y += 25;
        UiHelper.Row(this, "TỔNG CỘNG", lblTong, y); y += 40;

        UiHelper.Btn(this, "Đặt hàng", 270, y, 110, BtnDatHang_Click);
        UiHelper.Btn(this, "Hủy", 390, y, 95, delegate { Close(); });

        Load += delegate
        {
            cboKhuVuc.DisplayMember = "Value"; cboKhuVuc.ValueMember = "Key";
            cboKhuVuc.DataSource = AppSession.Config.LayKhuVuc();
            cboLoaiThe.DisplayMember = "Value"; cboLoaiThe.ValueMember = "Key";
            cboLoaiThe.DataSource = AppSession.Config.LayLoaiThe();
            if (AppSession.KhachHienTai != null) txtTen.Text = AppSession.KhachHienTai.HoTen;  // sửa được (BR08)

            // Đổi lựa chọn nào cũng tính lại tiền ngay
            rbThuong.CheckedChanged += delegate { CapNhatTien(); };
            rbNhanh.CheckedChanged += delegate { CapNhatTien(); };
            rbNgay.CheckedChanged += delegate { CapNhatTien(); };
            cboKhuVuc.SelectedIndexChanged += delegate { CapNhatTien(); };
            cboLoaiThe.SelectedIndexChanged += delegate { CapNhatTien(); };
            _ready = true;
            CapNhatTien();
        };
    }

    private string LoaiPhieu()
    {
        return rbNgay.Checked ? "NHANH_NGAY" : rbNhanh.Checked ? "NHANH" : "THUONG";
    }

    private void CapNhatTien()
    {
        if (!_ready) return;
        string kv = cboKhuVuc.SelectedValue as string;
        string lt = cboLoaiThe.SelectedValue as string;
        if (kv == null || lt == null) return;

        decimal th = AppSession.Cart.TienHang;
        decimal pg = AppSession.Shipping.TinhPhi(th, LoaiPhieu(), kv);
        decimal lp = AppSession.Config.LayLePhiThe(lt);

        lblTH.Text = th.ToString("N0") + " đ";
        lblPG.Text = pg.ToString("N0") + " đ" + (pg == 0 && LoaiPhieu() != "THUONG" ? "  (miễn phí)" : "");
        lblLP.Text = lp.ToString("N0") + " đ";
        lblTong.Text = (th + pg + lp).ToString("N0") + " đ";
    }

    private void BtnDatHang_Click(object s, EventArgs e)
    {
        var nn = new NguoiNhan
        {
            HoTen = txtTen.Text.Trim(),
            DiaChi = txtDiaChi.Text.Trim(),
            DienThoai = txtDT.Text.Trim(),
            MaKhuVuc = cboKhuVuc.SelectedValue as string
        };
        if (nn.HoTen == "" || nn.DiaChi == "" || nn.DienThoai == "")
        {
            UiHelper.Error("Vui lòng nhập đủ thông tin người nhận.");
            return;
        }

        var card = new CardInfo
        {
            MaLoaiThe = cboLoaiThe.SelectedValue as string,
            SoThe = txtSoThe.Text.Trim(),
            CSV = txtCSV.Text.Trim(),
            NgayHetHan = txtHetHan.Text.Trim(),
            TenChuThe = txtChuThe.Text.Trim()
        };

        var rs = AppSession.Orders.DatHang(AppSession.KhachHienTai, AppSession.Cart, LoaiPhieu(), nn, card);
        if (!rs.ThanhCong) { UiHelper.Error(rs.Loi); return; }

        string tb = "Đặt hàng thành công! Mã đơn: #" + rs.MaDon + "\r\nTổng tiền: " + rs.TongTien.ToString("N0") + " đ";
        if (AppSession.Email.Sent.Count > 0 && !string.IsNullOrEmpty(AppSession.KhachHienTai.Email))
            tb += "\r\n\r\n[Mock email đã gửi]\r\n" + AppSession.Email.Sent[AppSession.Email.Sent.Count - 1];
        UiHelper.Info(tb);
        DialogResult = DialogResult.OK;
    }
}