using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using QuanLyNhanSu.Services;

namespace QuanLyNhanSu.Forms
{
    public partial class FrmBangLuong : Form
    {
        private readonly BangLuongService svc = new BangLuongService();

        public FrmBangLuong() { InitializeComponent(); }

        private void FrmBangLuong_Load(object sender, EventArgs e)
        {
            numThang.Value = DateTime.Today.Month; numNam.Value = DateTime.Today.Year;
            txtNguoiLap.Text = "Kế toán tiền lương";
            Tai();
        }

        private void Tai() { dgvBL.DataSource = svc.LayBangLuong(); }

        private void btnXemTruoc_Click(object sender, EventArgs e)
        {
            var kq = svc.Tinh((int)numThang.Value, (int)numNam.Value);
            if (kq.Loi != null) { FormHelper.Canh(kq.Loi); return; }
            dgvCT.DataSource = BangLuongService.ToBang(kq.Dong);
        }

        private void btnLap_Click(object sender, EventArgs e)
        {
            if (FormHelper.Bao(svc.Lap((int)numThang.Value, (int)numNam.Value, txtNguoiLap.Text))) Tai();
        }

        private void dgvBL_SelectionChanged(object sender, EventArgs e)
        {
            string ma = FormHelper.O(dgvBL, "MaBL");
            dgvCT.DataSource = ma == "" ? null : svc.LayChiTiet(ma);
        }

        private void btnDong_Click(object sender, EventArgs e) { Close(); }
    }
}
