using System;
using System.Drawing;
using System.Windows.Forms;

namespace eShopping.UI
{
    public partial class frmDangNhap : Form
    {
        private readonly TextBox txtUser = new TextBox { Width = 220 };
        private readonly TextBox txtPwd = new TextBox { Width = 220, UseSystemPasswordChar = true };

        public frmDangNhap()
        {
            Text = "Đăng nhập";
            ClientSize = new Size(390, 150);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false; MinimizeBox = false;

            UiHelper.Row(this, "Tên đăng nhập", txtUser, 20);
            UiHelper.Row(this, "Mật khẩu", txtPwd, 55);
            var btnOk = UiHelper.Btn(this, "Đăng nhập", 150, 100, 100, BtnDangNhap_Click);
            UiHelper.Btn(this, "Đăng ký", 260, 100, 100, delegate
            {
                using (var f = new frmDangKy()) f.ShowDialog(this);
            });
            AcceptButton = btnOk;
        }

        private void BtnDangNhap_Click(object s, EventArgs e)
        {
            var kh = AppSession.Auth.DangNhap(txtUser.Text, txtPwd.Text);
            if (kh == null)
            {
                UiHelper.Error("Sai tên đăng nhập hoặc mật khẩu.");
                return;
            }
            AppSession.KhachHienTai = kh;
            DialogResult = DialogResult.OK;   // Tự đóng form
        }
    }
}