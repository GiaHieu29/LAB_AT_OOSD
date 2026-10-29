using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using QuanLyNhanSu.Services;

namespace QuanLyNhanSu.Forms
{
    public partial class FrmTuyenDung : Form
    {
        private readonly TuyenDungService svc = new TuyenDungService();
        private readonly DanhMucService dm = new DanhMucService();

        public FrmTuyenDung() { InitializeComponent(); }

        private void FrmTuyenDung_Load(object sender, EventArgs e)
        {
            cboGT.Items.AddRange(new object[] { "Nam", "Nữ" });
            cboGT.SelectedIndex = 0;
            dtNgaySinh.Value = new DateTime(1995, 1, 1);
            Tai();
            TinhKetThuc(null, null);
        }

        private void Tai()
        {
            FormHelper.Nap(cboPB, dm.LayPhongBanCoNhuCau(), "HienThi", "MaPB");
            dgv.DataSource = svc.LayTheoDoiThuViec();
        }

        private void TinhKetThuc(object sender, EventArgs e)
        {
            if (lblKetThuc == null) return;
            lblKetThuc.Text = dtBD.Value.Date.AddMonths((int)numThang.Value).AddDays(-1).ToString("dd/MM/yyyy");
        }

        private void btnLap_Click(object sender, EventArgs e)
        {
            var uv = new HoSoUngVien
            {
                MaNV = txtMaNV.Text.Trim(), HoTen = txtHoTen.Text, NgaySinh = dtNgaySinh.Value, GioiTinh = cboGT.Text,
                DiaChi = txtDiaChi.Text, DienThoai = txtDT.Text, CCCD = txtCCCD.Text, MaPB = FormHelper.Gia(cboPB)
            };
            var kq = svc.LapQuyetDinh(uv, txtSoQD.Text.Trim(), dtQD.Value, dtBD.Value, (int)numThang.Value, numLuong.Value, txtDKCT.Text, txtDKNV.Text, txtNguoiKy.Text);
            if (FormHelper.Bao(kq)) { LamMoi(); Tai(); }
        }

        private void LamMoi()
        {
            txtMaNV.Clear(); txtHoTen.Clear(); txtDiaChi.Clear(); txtDT.Clear(); txtCCCD.Clear(); txtSoQD.Clear();
            txtDKCT.Clear(); txtDKNV.Clear(); txtNguoiKy.Clear(); numLuong.Value = 0;
            dtNgaySinh.Value = new DateTime(1995, 1, 1);
        }

        private void btnLamMoi_Click(object sender, EventArgs e) { LamMoi(); }
        private void btnDong_Click(object sender, EventArgs e) { Close(); }
    }
}
