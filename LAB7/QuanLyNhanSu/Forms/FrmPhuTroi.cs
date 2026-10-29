using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using QuanLyNhanSu.Services;

namespace QuanLyNhanSu.Forms
{
    public partial class FrmPhuTroi : Form
    {
        private readonly PhuTroiService svc = new PhuTroiService();
        private readonly NhanVienService nv = new NhanVienService();
        private readonly DataTable dtSoan = TaoBang();
        private static readonly string[] MaHT = { QuyDinh.TangCa, QuyDinh.NgayLe, QuyDinh.NgayNghi };

        private static DataTable TaoBang()
        {
            var t = new DataTable();
            t.Columns.Add("MaNV", typeof(string)); t.Columns.Add("HoTen", typeof(string)); t.Columns.Add("SoGio", typeof(decimal));
            return t;
        }

        public FrmPhuTroi() { InitializeComponent(); }

        private void FrmPhuTroi_Load(object sender, EventArgs e)
        {
            cboHT.Items.AddRange(new object[] { "Tăng ca", "Ngày lễ", "Ngày nghỉ" });
            cboHT.SelectedIndex = 0;
            FormHelper.Nap(cboNV, nv.LayCombo(null), "HienThi", "MaNV");
            dgvSoan.DataSource = dtSoan;
            dtNgay.Value = DateTime.Today;
            txtNguoiLap.Text = "Ban chấm công";
            TaiPhieu();
        }

        private void TaiPhieu() { dgvPhieu.DataSource = svc.LayPhieu(); }

        private void btnThemNV_Click(object sender, EventArgs e)
        {
            string ma = FormHelper.Gia(cboNV);
            if (ma == "") { FormHelper.Canh("Hãy chọn nhân viên."); return; }
            if (numGioNV.Value > numGio.Value) { FormHelper.Canh("Giờ của nhân viên không được vượt số giờ của phiếu."); return; }
            foreach (DataRow r in dtSoan.Rows)
                if (Convert.ToString(r["MaNV"]) == ma) { FormHelper.Canh("Nhân viên đã có trong danh sách."); return; }
            string hoTen = cboNV.Text.Contains(" - ") ? cboNV.Text.Substring(cboNV.Text.IndexOf(" - ") + 3) : cboNV.Text;
            dtSoan.Rows.Add(ma, hoTen, numGioNV.Value);
        }

        private void btnXoaNV_Click(object sender, EventArgs e)
        {
            if (dgvSoan.CurrentRow != null) dtSoan.Rows.RemoveAt(dgvSoan.CurrentRow.Index);
        }

        private void btnLap_Click(object sender, EventArgs e)
        {
            var ds = new List<DongPhuTroi>();
            foreach (DataRow r in dtSoan.Rows) ds.Add(new DongPhuTroi { MaNV = Convert.ToString(r["MaNV"]), SoGio = Convert.ToDecimal(r["SoGio"]) });
            if (FormHelper.Bao(svc.LapPhieu(txtSo.Text.Trim(), dtNgay.Value, MaHT[Math.Max(0, cboHT.SelectedIndex)], numGio.Value, txtNguoiLap.Text, ds)))
            { dtSoan.Rows.Clear(); txtSo.Clear(); TaiPhieu(); }
        }

        private void dgvPhieu_SelectionChanged(object sender, EventArgs e)
        {
            string so = FormHelper.O(dgvPhieu, "SoPhieu");
            dgvCT.DataSource = so == "" ? null : svc.LayChiTiet(so);
        }

        private void btnDong_Click(object sender, EventArgs e) { Close(); }
    }
}
