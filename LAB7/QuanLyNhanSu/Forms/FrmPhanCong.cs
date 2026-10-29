using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using QuanLyNhanSu.Services;

namespace QuanLyNhanSu.Forms
{
    public partial class FrmPhanCong : Form
    {
        private readonly PhanCongService svc = new PhanCongService();
        private readonly NhanVienService nv = new NhanVienService();
        private readonly DanhMucService dm = new DanhMucService();
        private readonly DataTable dtSoan = TaoBang();

        private static DataTable TaoBang()
        {
            var t = new DataTable();
            t.Columns.Add("MaNV", typeof(string)); t.Columns.Add("HoTen", typeof(string)); t.Columns.Add("Ngay", typeof(DateTime));
            t.Columns.Add("MaCa", typeof(string)); t.Columns.Add("TenCa", typeof(string)); t.Columns.Add("GioPhuTroi", typeof(decimal));
            return t;
        }

        public FrmPhanCong() { InitializeComponent(); }

        private void FrmPhanCong_Load(object sender, EventArgs e)
        {
            dgvSoan.DataSource = dtSoan;
            FormHelper.Nap(cboCa, dm.LayCaCombo(), "HienThi", "MaCa");
            FormHelper.Nap(cboPB, dm.LayPhongBanCombo(), "TenPB", "MaPB");
            dtTuan.Value = QuyDinh.ThuHaiCuaTuan(DateTime.Today).AddDays(7);   // kế hoạch lập cho tuần sau
            dtNgay.Value = dtTuan.Value;
            TaiKeHoach();
        }

        private void TaiKeHoach() { dgvKH.DataSource = svc.LayKeHoach(); }

        private void cboPB_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pb = FormHelper.Gia(cboPB);
            FormHelper.Nap(cboNV, nv.LayCombo(pb == "" ? null : pb), "HienThi", "MaNV");
            string tp = svc.LayTruongPhong(pb);
            lblTruongPhong.Text = tp == "" ? "Phòng ban chưa có trưởng phòng" : "Người lập kế hoạch (trưởng phòng): " + tp;
            dtSoan.Rows.Clear();
        }

        private void btnThemDong_Click(object sender, EventArgs e)
        {
            string ma = FormHelper.Gia(cboNV), ca = FormHelper.Gia(cboCa);
            if (ma == "" || ca == "") { FormHelper.Canh("Hãy chọn nhân viên và ca làm việc."); return; }
            DateTime ngay = dtNgay.Value.Date, tuan = dtTuan.Value.Date;
            if (ngay < tuan || ngay > tuan.AddDays(6)) { FormHelper.Canh("Ngày phân công phải nằm trong tuần bắt đầu " + tuan.ToString("dd/MM/yyyy") + "."); return; }
            foreach (DataRow r in dtSoan.Rows)
                if (Convert.ToString(r["MaNV"]) == ma && Convert.ToDateTime(r["Ngay"]).Date == ngay) { FormHelper.Canh("Nhân viên đã được phân công trong ngày này."); return; }
            string hoTen = cboNV.Text.Contains(" - ") ? cboNV.Text.Substring(cboNV.Text.IndexOf(" - ") + 3) : cboNV.Text;
            dtSoan.Rows.Add(ma, hoTen, ngay, ca, cboCa.Text, numGio.Value);
        }

        private void btnXoaDong_Click(object sender, EventArgs e)
        {
            if (dgvSoan.CurrentRow != null) dtSoan.Rows.RemoveAt(dgvSoan.CurrentRow.Index);
        }

        private void btnLap_Click(object sender, EventArgs e)
        {
            var ds = new List<DongPhanCong>();
            foreach (DataRow r in dtSoan.Rows)
                ds.Add(new DongPhanCong { MaNV = Convert.ToString(r["MaNV"]), Ngay = Convert.ToDateTime(r["Ngay"]), MaCa = Convert.ToString(r["MaCa"]), GioPhuTroi = Convert.ToDecimal(r["GioPhuTroi"]) });
            if (FormHelper.Bao(svc.LapKeHoach(FormHelper.Gia(cboPB), dtTuan.Value, ds))) { dtSoan.Rows.Clear(); TaiKeHoach(); }
        }

        private void dgvKH_SelectionChanged(object sender, EventArgs e)
        {
            string ma = FormHelper.O(dgvKH, "MaKH");
            dgvCT.DataSource = ma == "" ? null : svc.LayChiTiet(ma);
        }

        private void btnDong_Click(object sender, EventArgs e) { Close(); }
    }
}
