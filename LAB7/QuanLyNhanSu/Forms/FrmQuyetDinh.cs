using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using QuanLyNhanSu.Services;

namespace QuanLyNhanSu.Forms
{
    public partial class FrmQuyetDinh : Form
    {
        private readonly QuyetDinhService svc = new QuyetDinhService();
        private readonly NhanVienService nv = new NhanVienService();
        private readonly DanhMucService dm = new DanhMucService();

        public FrmQuyetDinh() { InitializeComponent(); }

        private void FrmQuyetDinh_Load(object sender, EventArgs e)
        {
            FormHelper.Nap(cboNVCV, nv.LayComboTheoTrangThai(QuyDinh.ChinhThuc), "HienThi", "MaNV");
            FormHelper.Nap(cboNVL, nv.LayComboTheoTrangThai(QuyDinh.ChinhThuc), "HienThi", "MaNV");
            FormHelper.Nap(cboCV, dm.LayChucVuCombo(), "TenCV", "MaCV");
            TaiLuoi();
        }

        private void TaiLuoi() { dgvCV.DataSource = svc.LayChucVu(); dgvL.DataSource = svc.LayLuong(); }

        private void cboNVCV_SelectedIndexChanged(object sender, EventArgs e) { lblHienTaiCV.Text = svc.LayChucVuHienTai(FormHelper.Gia(cboNVCV)); }
        private void cboNVL_SelectedIndexChanged(object sender, EventArgs e) { lblHienTaiL.Text = svc.LayLuongHienTai(FormHelper.Gia(cboNVL)); }

        private void cboCV_SelectedIndexChanged(object sender, EventArgs e)
        {
            var r = cboCV.SelectedItem as DataRowView;
            if (r != null) numPCCV.Value = Math.Min(numPCCV.Maximum, Convert.ToDecimal(r["PhuCapMacDinh"]));
        }

        private void btnLapCV_Click(object sender, EventArgs e)
        {
            if (FormHelper.Bao(svc.LapQuyetDinhChucVu(txtSoQDCV.Text.Trim(), FormHelper.Gia(cboNVCV), FormHelper.Gia(cboCV), dtCV.Value, numPCCV.Value, txtGhiChuCV.Text)))
            { txtSoQDCV.Clear(); txtGhiChuCV.Clear(); TaiLuoi(); lblHienTaiCV.Text = svc.LayChucVuHienTai(FormHelper.Gia(cboNVCV)); }
        }

        private void btnLapL_Click(object sender, EventArgs e)
        {
            if (FormHelper.Bao(svc.LapQuyetDinhLuong(txtSoQDL.Text.Trim(), FormHelper.Gia(cboNVL), dtL.Value, numLCB.Value, numPCL.Value, numPCDH.Value, txtLyDo.Text)))
            { txtSoQDL.Clear(); txtLyDo.Clear(); TaiLuoi(); lblHienTaiL.Text = svc.LayLuongHienTai(FormHelper.Gia(cboNVL)); }
        }

        private void btnDong_Click(object sender, EventArgs e) { Close(); }
    }
}
