using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using QuanLyNhanSu.Services;

namespace QuanLyNhanSu.Forms
{
    public partial class FrmHopDong : Form
    {
        private readonly HopDongService svc = new HopDongService();

        public FrmHopDong() { InitializeComponent(); }

        private void FrmHopDong_Load(object sender, EventArgs e)
        {
            cboLoai.Items.AddRange(new object[] { "Dài hạn", "Ngắn hạn" });
            cboLoai.SelectedIndex = 0;
            dtKT.Value = DateTime.Today.AddYears(1);
            Tai();
        }

        private void Tai()
        {
            FormHelper.Nap(cboNV, svc.LayNhanVienCoTheKy(), "HienThi", "MaNV");
            FormHelper.Nap(cboNVBH, svc.LayNhanVienChuaCoBaoHiem(), "HienThi", "MaNV");
            dgvHD.DataSource = svc.LayHopDong();
            dgvBH.DataSource = svc.LayBaoHiem();
        }

        private void cboNV_SelectedIndexChanged(object sender, EventArgs e) { lblThongTin.Text = svc.MoTaNhanVien(FormHelper.Gia(cboNV)); }
        private void cboLoai_SelectedIndexChanged(object sender, EventArgs e) { dtKT.Enabled = cboLoai.SelectedIndex == 1; }

        private void btnKy_Click(object sender, EventArgs e)
        {
            string loai = cboLoai.SelectedIndex == 0 ? QuyDinh.DaiHan : QuyDinh.NganHan;
            if (FormHelper.Bao(svc.KyHopDong(txtSoHD.Text.Trim(), FormHelper.Gia(cboNV), loai, dtKy.Value, dtBD.Value, dtKT.Value, txtGhiChu.Text))) { txtSoHD.Clear(); txtGhiChu.Clear(); Tai(); }
        }

        private void btnLapBH_Click(object sender, EventArgs e)
        {
            if (FormHelper.Bao(svc.LapHoSoBaoHiem(FormHelper.Gia(cboNVBH), txtBHXH.Text, numBHXH.Value, txtBHYT.Text, numBHYT.Value, dtThamGia.Value))) { txtBHXH.Clear(); txtBHYT.Clear(); Tai(); }
        }

        private void btnDong_Click(object sender, EventArgs e) { Close(); }
    }
}
