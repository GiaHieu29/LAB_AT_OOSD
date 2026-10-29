using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using QuanLyNhanSu.Services;

namespace QuanLyNhanSu.Forms
{
    public partial class FrmChamCong : Form
    {
        private readonly ChamCongService svc = new ChamCongService();
        private readonly NhanVienService nv = new NhanVienService();
        private readonly DanhMucService dm = new DanhMucService();
        private bool daTai;
        private static readonly string[] MaTT = { QuyDinh.DiLam, QuyDinh.NghiPhep, QuyDinh.NghiKhongPhep };

        public FrmChamCong() { InitializeComponent(); }

        private void FrmChamCong_Load(object sender, EventArgs e)
        {
            cboTT.Items.AddRange(new object[] { "Đi làm", "Nghỉ phép", "Nghỉ không phép" });
            cboTT.SelectedIndex = 0;
            FormHelper.Nap(cboCa, FormHelper.ThemDongTatCa(dm.LayCaCombo(), "MaCa", "HienThi"), "HienThi", "MaCa");
            dtNgay.Value = DateTime.Today;
            numThang.Value = DateTime.Today.Month; numNam.Value = DateTime.Today.Year;
            FormHelper.Nap(cboNV, nv.LayCombo(null), "HienThi", "MaNV");
            daTai = true;
            CapNhatNgay(null, null);
        }

        private void CapNhatNgay(object sender, EventArgs e)
        {
            if (!daTai) return;
            string ma = FormHelper.Gia(cboNV);
            lblKeHoach.Text = svc.LayKeHoach(ma, dtNgay.Value);
            dgv.DataSource = ma == "" ? null : svc.LayChamCongThang(ma, dtNgay.Value.Month, dtNgay.Value.Year);
        }

        private void cboTT_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool diLam = cboTT.SelectedIndex == 0;
            numGio.Enabled = diLam;
            if (!diLam) numGio.Value = 0; else if (numGio.Value == 0) numGio.Value = 8;
        }

        private void btnGhi_Click(object sender, EventArgs e)
        {
            string ca = FormHelper.Gia(cboCa);
            if (FormHelper.Bao(svc.Ghi(FormHelper.Gia(cboNV), dtNgay.Value, MaTT[Math.Max(0, cboTT.SelectedIndex)], numGio.Value, ca == "" ? null : ca, txtGhiChu.Text)))
            { txtGhiChu.Clear(); CapNhatNgay(null, null); }
        }

        private void btnXem_Click(object sender, EventArgs e) { dgvBang.DataSource = svc.BangChamCong((int)numThang.Value, (int)numNam.Value); }
        private void btnDong_Click(object sender, EventArgs e) { Close(); }
    }
}
