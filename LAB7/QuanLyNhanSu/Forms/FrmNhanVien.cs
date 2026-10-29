using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using QuanLyNhanSu.Services;

namespace QuanLyNhanSu.Forms
{
    public partial class FrmNhanVien : Form
    {
        private readonly NhanVienService svc = new NhanVienService();
        private readonly DanhMucService dm = new DanhMucService();
        private static readonly string[] MaTT = { null, QuyDinh.ThuViec, QuyDinh.ChinhThuc, QuyDinh.NghiViec };

        public FrmNhanVien() { InitializeComponent(); }

        private void FrmNhanVien_Load(object sender, EventArgs e)
        {
            FormHelper.Nap(cboLocPB, FormHelper.ThemDongTatCa(dm.LayPhongBanCombo(), "MaPB", "TenPB"), "TenPB", "MaPB");
            cboLocTT.Items.AddRange(new object[] { "(Tất cả)", "Thử việc", "Chính thức", "Nghỉ việc" });
            cboLocTT.SelectedIndex = 0;
            cboGT.Items.AddRange(new object[] { "Nam", "Nữ" });
            cboGT.SelectedIndex = 0;
            FormHelper.Nap(cboPB, dm.LayPhongBanCombo(), "TenPB", "MaPB");
            dtNghi.Value = DateTime.Today;
            Tim();
        }

        private void Tim()
        {
            string pb = FormHelper.Gia(cboLocPB);
            dgv.DataSource = svc.LayNhanVien(txtTim.Text, pb == "" ? null : pb, MaTT[Math.Max(0, cboLocTT.SelectedIndex)]);
        }

        private void btnTim_Click(object sender, EventArgs e) { Tim(); }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            var r = dgv.CurrentRow;
            if (r == null) { dgvLichSu.DataSource = null; return; }
            txtMaNV.Text = Convert.ToString(r.Cells["MaNV"].Value);
            txtHoTen.Text = Convert.ToString(r.Cells["HoTen"].Value);
            dtNgaySinh.Value = Convert.ToDateTime(r.Cells["NgaySinh"].Value);
            cboGT.SelectedItem = Convert.ToString(r.Cells["GioiTinh"].Value);
            txtDiaChi.Text = Convert.ToString(r.Cells["DiaChi"].Value);
            txtDT.Text = Convert.ToString(r.Cells["DienThoai"].Value);
            txtCCCD.Text = Convert.ToString(r.Cells["CCCD"].Value);
            cboPB.SelectedValue = Convert.ToString(r.Cells["MaPB"].Value);
            dgvLichSu.DataSource = svc.LayLichSu(txtMaNV.Text);
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (FormHelper.Bao(svc.CapNhat(txtMaNV.Text, txtHoTen.Text, dtNgaySinh.Value, cboGT.Text, txtDiaChi.Text, txtDT.Text, txtCCCD.Text, FormHelper.Gia(cboPB)))) Tim();
        }

        private void btnNghiViec_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Ghi nhận nhân viên " + txtMaNV.Text + " nghỉ việc?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            if (FormHelper.Bao(svc.NghiViec(txtMaNV.Text, dtNghi.Value, txtLyDo.Text))) { txtLyDo.Clear(); Tim(); }
        }

        private void btnDong_Click(object sender, EventArgs e) { Close(); }
    }
}
