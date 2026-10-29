using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using QuanLyNhanSu.Services;

namespace QuanLyNhanSu.Forms
{
    public partial class FrmKyLuat : Form
    {
        private readonly KyLuatService svc = new KyLuatService();
        private readonly KhenThuongService kt = new KhenThuongService();
        private readonly NhanVienService nv = new NhanVienService();
        private readonly DanhMucService dm = new DanhMucService();
        private static readonly string[] MaHT = { QuyDinh.KhienTrach, QuyDinh.CanhCao, QuyDinh.PhatTien, QuyDinh.SaThai };

        public FrmKyLuat() { InitializeComponent(); }

        private void FrmKyLuat_Load(object sender, EventArgs e)
        {
            cboDT.Items.AddRange(new object[] { "Cá nhân", "Tập thể phòng ban" });
            cboHT.Items.AddRange(new object[] { "Khiển trách", "Cảnh cáo", "Phạt tiền", "Sa thải" });
            cboCach.Items.AddRange(new object[] { "Nộp phạt", "Trừ lương" });
            cboCach.SelectedIndex = 0;
            FormHelper.Nap(cboNV, nv.LayCombo(null), "HienThi", "MaNV");
            FormHelper.Nap(cboPB, dm.LayPhongBanCombo(), "TenPB", "MaPB");
            FormHelper.Nap(cboKy, kt.LayGiamDoc(), "HienThi", "MaNV");
            dtQD.Value = DateTime.Today;
            cboDT.SelectedIndex = 0;
            cboHT.SelectedIndex = 0;
            TaiQD();
        }

        private void TaiQD() { dgvQD.DataSource = svc.LayQuyetDinh(); }

        private void cboDT_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool caNhan = cboDT.SelectedIndex == 0;
            cboNV.Enabled = caNhan; cboPB.Enabled = !caNhan;
        }

        private void cboHT_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool phat = cboHT.SelectedIndex == 2;
            numTien.Enabled = phat; cboCach.Enabled = phat;
            if (!phat) numTien.Value = 0;
        }

        private void btnLap_Click(object sender, EventArgs e)
        {
            string dt = cboDT.SelectedIndex == 0 ? QuyDinh.CaNhan : QuyDinh.TapThe;
            bool phat = cboHT.SelectedIndex == 2;
            string cach = phat ? (cboCach.SelectedIndex == 0 ? QuyDinh.NopPhat : QuyDinh.TruLuong) : null;
            var kq = svc.LapQuyetDinh(txtSoQD.Text.Trim(), dtQD.Value, dt, FormHelper.Gia(cboNV), FormHelper.Gia(cboPB), MaHT[Math.Max(0, cboHT.SelectedIndex)],
                txtLyDo.Text, numTien.Value, cach, FormHelper.Gia(cboKy));
            if (FormHelper.Bao(kq)) { txtSoQD.Clear(); txtLyDo.Clear(); numTien.Value = 0; TaiQD(); FormHelper.Nap(cboNV, nv.LayCombo(null), "HienThi", "MaNV"); }
        }

        private void dgvQD_SelectionChanged(object sender, EventArgs e)
        {
            string so = FormHelper.O(dgvQD, "SoQD");
            dgvPhieu.DataSource = so == "" ? null : svc.LayPhieuThu(so);
        }

        private void btnThuPhat_Click(object sender, EventArgs e)
        {
            if (FormHelper.Bao(svc.ThuPhat(FormHelper.O(dgvPhieu, "SoPhieu")))) dgvPhieu.DataSource = svc.LayPhieuThu(FormHelper.O(dgvQD, "SoQD"));
        }

        private void btnDong_Click(object sender, EventArgs e) { Close(); }
    }
}
