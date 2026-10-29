using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using QuanLyNhanSu.Services;

namespace QuanLyNhanSu.Forms
{
    public partial class FrmDanhMuc : Form
    {
        private readonly DanhMucService dm = new DanhMucService();
        private readonly NhanVienService nv = new NhanVienService();

        public FrmDanhMuc() { InitializeComponent(); }
        private void FrmDanhMuc_Load(object sender, EventArgs e) { Tai(); }

        private void Tai()
        {
            dgvPB.DataSource = dm.LayPhongBan();
            dgvCV.DataSource = dm.LayChucVu();
            dgvCa.DataSource = dm.LayCa();
        }

        private void dgvPB_SelectionChanged(object sender, EventArgs e)
        {
            string ma = FormHelper.O(dgvPB, "MaPB");
            FormHelper.Nap(cboPBTruong, nv.LayCombo(ma == "" ? null : ma), "HienThi", "MaNV");
            if (dgvPB.CurrentRow != null)
            {
                decimal n = Convert.ToDecimal(dgvPB.CurrentRow.Cells["NhuCauTuyen"].Value);
                numPBNhuCau.Value = Math.Max(numPBNhuCau.Minimum, Math.Min(numPBNhuCau.Maximum, n));
            }
        }

        private void btnThemPB_Click(object sender, EventArgs e)
        {
            if (FormHelper.Bao(dm.ThemPhongBan(txtPBMa.Text, txtPBTen.Text, txtPBMoTa.Text, (int)numPBNhuCau.Value))) { txtPBMa.Clear(); txtPBTen.Clear(); txtPBMoTa.Clear(); Tai(); }
        }
        private void btnCapNhatNhuCau_Click(object sender, EventArgs e)
        {
            if (FormHelper.Bao(dm.CapNhatNhuCau(FormHelper.O(dgvPB, "MaPB"), (int)numPBNhuCau.Value))) Tai();
        }
        private void btnDatTruong_Click(object sender, EventArgs e)
        {
            if (FormHelper.Bao(dm.DatTruongPhong(FormHelper.O(dgvPB, "MaPB"), FormHelper.Gia(cboPBTruong)))) Tai();
        }
        private void btnThemCV_Click(object sender, EventArgs e)
        {
            if (FormHelper.Bao(dm.ThemChucVu(txtCVMa.Text, txtCVTen.Text, numCVPhuCap.Value))) { txtCVMa.Clear(); txtCVTen.Clear(); numCVPhuCap.Value = 0; Tai(); }
        }
        private void btnThemCa_Click(object sender, EventArgs e)
        {
            if (FormHelper.Bao(dm.ThemCa(txtCaMa.Text, txtCaTen.Text, txtCaBD.Text, txtCaKT.Text))) { txtCaMa.Clear(); txtCaTen.Clear(); txtCaBD.Clear(); txtCaKT.Clear(); Tai(); }
        }
        private void btnDong_Click(object sender, EventArgs e) { Close(); }
    }
}
