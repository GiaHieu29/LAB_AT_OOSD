using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using QuanLyNhanSu.Services;

namespace QuanLyNhanSu.Forms
{
    public partial class FrmKhenThuong : Form
    {
        private readonly KhenThuongService svc = new KhenThuongService();
        private readonly NhanVienService nv = new NhanVienService();
        private readonly DanhMucService dm = new DanhMucService();
        private static readonly string[] MaLoai = { QuyDinh.ThanhTich, QuyDinh.NgayLe, QuyDinh.CuoiNam, QuyDinh.Luong13 };

        public FrmKhenThuong() { InitializeComponent(); }

        private void FrmKhenThuong_Load(object sender, EventArgs e)
        {
            cboDT.Items.AddRange(new object[] { "Cá nhân", "Tập thể phòng ban" });
            cboLoai.Items.AddRange(new object[] { "Thành tích", "Ngày lễ", "Thưởng cuối năm", "Lương tháng 13" });
            cboHTChi.Items.AddRange(new object[] { "Tiền mặt", "Chuyển khoản" });
            cboLoai.SelectedIndex = 0; cboHTChi.SelectedIndex = 0;
            FormHelper.Nap(cboNV, nv.LayCombo(null), "HienThi", "MaNV");
            FormHelper.Nap(cboPB, dm.LayPhongBanCombo(), "TenPB", "MaPB");
            FormHelper.Nap(cboKy, svc.LayGiamDoc(), "HienThi", "MaNV");
            dtQD.Value = DateTime.Today;
            cboDT.SelectedIndex = 0;
            TaiQD();
        }

        private void TaiQD() { dgvQD.DataSource = svc.LayQuyetDinh(); }

        private void cboDT_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool caNhan = cboDT.SelectedIndex == 0;
            cboNV.Enabled = caNhan; cboPB.Enabled = !caNhan;
        }

        private void btnLap_Click(object sender, EventArgs e)
        {
            string dt = cboDT.SelectedIndex == 0 ? QuyDinh.CaNhan : QuyDinh.TapThe;
            var kq = svc.LapQuyetDinh(txtSoQD.Text.Trim(), dtQD.Value, dt, FormHelper.Gia(cboNV), FormHelper.Gia(cboPB), MaLoai[Math.Max(0, cboLoai.SelectedIndex)],
                txtLyDo.Text, numTien.Value, FormHelper.Gia(cboKy));
            if (FormHelper.Bao(kq)) { txtSoQD.Clear(); txtLyDo.Clear(); numTien.Value = 0; TaiQD(); }
        }

        private void dgvQD_SelectionChanged(object sender, EventArgs e)
        {
            string so = FormHelper.O(dgvQD, "SoQD");
            dgvPhieu.DataSource = so == "" ? null : svc.LayPhieuChi(so);
        }

        private void btnChi_Click(object sender, EventArgs e)
        {
            string hinhThuc = cboHTChi.SelectedIndex == 0 ? "TIENMAT" : "CHUYENKHOAN";
            if (FormHelper.Bao(svc.ChiPhieu(FormHelper.O(dgvPhieu, "SoPhieu"), hinhThuc)))
                dgvPhieu.DataSource = svc.LayPhieuChi(FormHelper.O(dgvQD, "SoQD"));
        }

        private void btnDong_Click(object sender, EventArgs e) { Close(); }
    }
}
