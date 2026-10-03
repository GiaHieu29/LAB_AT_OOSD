using System;
using System.Drawing;
using System.Windows.Forms;

public partial class frmDangKy : Form
{
    private readonly TextBox txtHoTen = new TextBox { Width = 250 };
    private readonly DateTimePicker dtpNgaySinh = new DateTimePicker
    {
        Width = 250,
        Format = DateTimePickerFormat.Short,
        Value = new DateTime(2000, 1, 1),
        MaxDate = DateTime.Today
    };
    private readonly TextBox txtCMND = new TextBox { Width = 250 };
    private readonly TextBox txtDiaChi = new TextBox { Width = 250 };
    private readonly TextBox txtDienThoai = new TextBox { Width = 250 };
    private readonly TextBox txtUser = new TextBox { Width = 250 };
    private readonly TextBox txtPwd = new TextBox { Width = 250, UseSystemPasswordChar = true };
    private readonly TextBox txtEmail = new TextBox { Width = 250 };

    public frmDangKy()
    {
        Text = "Đăng ký tài khoản";
        ClientSize = new Size(440, 370);
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false; MinimizeBox = false;

        int y = 15;
        UiHelper.Row(this, "Họ tên (*)", txtHoTen, y); y += 35;
        UiHelper.Row(this, "Ngày sinh (*)", dtpNgaySinh, y); y += 35;
        UiHelper.Row(this, "CMND/Passport (*)", txtCMND, y); y += 35;
        UiHelper.Row(this, "Địa chỉ (*)", txtDiaChi, y); y += 35;
        UiHelper.Row(this, "Điện thoại (*)", txtDienThoai, y); y += 35;
        UiHelper.Row(this, "Tên đăng nhập (*)", txtUser, y); y += 35;
        UiHelper.Row(this, "Mật khẩu (*, ≥ 6)", txtPwd, y); y += 35;
        UiHelper.Row(this, "Email", txtEmail, y); y += 45;

        UiHelper.Btn(this, "Đăng ký", 150, y, 110, BtnDangKy_Click);
        UiHelper.Btn(this, "Hủy", 270, y, 110, delegate { Close(); });
    }

    private void BtnDangKy_Click(object s, EventArgs e)
    {
        string loi = AppSession.Auth.DangKy(txtHoTen.Text, dtpNgaySinh.Value.Date, txtCMND.Text,
            txtDiaChi.Text, txtDienThoai.Text, txtUser.Text, txtPwd.Text, txtEmail.Text);
        if (loi != null) { UiHelper.Error(loi); return; }
        UiHelper.Info("Đăng ký thành công. Hãy đăng nhập.");
        Close();
    }
}