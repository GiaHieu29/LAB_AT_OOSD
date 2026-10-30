
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChoThueVanPhong.Services;

namespace QuanLyChoThueVanPhong
{
    public partial class FrmGiaHan : Form
    {
        private readonly GiaHanService svc =
            new GiaHanService();

        private bool dangNap = false;
        private string maPhieuDangChon = "";

        public FrmGiaHan()
        {
            InitializeComponent();
        }

        private void FrmGiaHan_Load(object sender, EventArgs e)
        {
            try
            {
                dtNgayLap.Value = DateTime.Today;

                NapHopDong();
                NapDanhSach();
                LamMoiNhap();
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        private void NapHopDong()
        {
            dangNap = true;
            try
            {
                cboHopDong.DataSource = null;
                cboHopDong.DisplayMember = "HienThi";
                cboHopDong.ValueMember = "MaHopDong";
                cboHopDong.DataSource = svc.LayHopDong();
            }
            finally
            {
                dangNap = false;
            }

            NapPhong();
        }

        private string GiaTriCombo(ComboBox cbo)
        {
            if (cbo.SelectedValue == null ||
                cbo.SelectedValue is DataRowView)
                return "";

            return Convert.ToString(cbo.SelectedValue);
        }

        private void NapPhong()
        {
            if (dangNap) return;

            dangNap = true;
            try
            {
                cboPhong.DataSource = null;
                cboPhong.DisplayMember = "HienThi";
                cboPhong.ValueMember = "MaChiTietHD";
                cboPhong.DataSource = svc.LayPhongTheoHopDong(
                    GiaTriCombo(cboHopDong));
            }
            finally
            {
                dangNap = false;
            }

            CapNhatThongTin();
        }

        private void cboHopDong_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            if (!dangNap) NapPhong();
        }

        private void cboPhong_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            if (!dangNap) CapNhatThongTin();
        }

        private void nudSoThang_ValueChanged(
            object sender, EventArgs e)
        {
            CapNhatNgay();
        }

        private void CapNhatThongTin()
        {
            if (dangNap) return;

            string maCT = GiaTriCombo(cboPhong);
            if (maCT == "")
            {
                lblGioiHan.Text = "Chưa chọn phòng thuê.";
                return;
            }

            try
            {
                DataTable dt = svc.LayThongTinPhong(maCT);

                if (dt.Rows.Count == 0) return;

                DataRow r = dt.Rows[0];
                int thangGoc =
                    Convert.ToInt32(r["SoThangThue"]);

                int daGiaHan =
                    Convert.ToInt32(r["ThangDaGiaHan"]);

                int conLai =
                    Math.Max(0, thangGoc / 2 - daGiaHan);

                ngayCuoiHienTai =
                    Convert.ToDateTime(
                        r["NgayKetThucHienTai"]);

                nudDonGia.Value = Math.Min(
                    nudDonGia.Maximum,
                    Convert.ToDecimal(r["DonGiaThue"]));

                nudSoThang.Maximum =
                    Math.Max(1, conLai);

                nudSoThang.Value = 1;

                lblGioiHan.Text =
                    "Thuê gốc: " + thangGoc +
                    " tháng | Đã gia hạn: " + daGiaHan +
                    " tháng | Còn được gia hạn: " +
                    conLai + " tháng";

                btnLapPhieu.Enabled =
                    conLai > 0 && maPhieuDangChon == "";

                CapNhatNgay();
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        private DateTime ngayCuoiHienTai =
            DateTime.Today;

        private void CapNhatNgay()
        {
            if (dtTuNgay == null ||
                dtDenNgay == null ||
                nudSoThang == null)
                return;

            try
            {
                DateTime tu = ngayCuoiHienTai.AddDays(1);
                DateTime den = tu.AddMonths(
                    (int)nudSoThang.Value).AddDays(-1);

                dtTuNgay.Value = tu;
                dtDenNgay.Value = den;
            }
            catch (ArgumentOutOfRangeException)
            {
                // Ngày nằm ngoài giới hạn DateTimePicker.
            }
        }

        private void NapDanhSach(string key = "")
        {
            dangNap = true;
            try
            {
                DataTable dt = svc.LayDanhSach(key);
                dgvGiaHan.DataSource = dt;

                string[] cot =
                {
                    "MaPhieuGiaHan", "MaHopDong",
                    "MaPhong", "NgayLap", "TuNgay",
                    "DenNgay", "SoThangGiaHan",
                    "DonGiaGiaHan", "TrangThai"
                };

                string[] ten =
                {
                    "Mã phiếu", "Hợp đồng",
                    "Phòng", "Ngày lập", "Từ ngày",
                    "Đến ngày", "Số tháng",
                    "Giá gia hạn", "Trạng thái"
                };

                for (int i = 0; i < cot.Length; i++)
                {
                    if (dgvGiaHan.Columns.Contains(cot[i]))
                        dgvGiaHan.Columns[cot[i]]
                            .HeaderText = ten[i];
                }

                foreach (string c in new[]
                {
                    "NgayLap", "TuNgay", "DenNgay"
                })
                {
                    if (dgvGiaHan.Columns.Contains(c))
                        dgvGiaHan.Columns[c]
                            .DefaultCellStyle.Format = "dd/MM/yyyy";
                }

                if (dgvGiaHan.Columns.Contains("DonGiaGiaHan"))
                    dgvGiaHan.Columns["DonGiaGiaHan"]
                        .DefaultCellStyle.Format = "N0";

                if (dgvGiaHan.Columns.Contains("MaChiTietHD"))
                    dgvGiaHan.Columns["MaChiTietHD"]
                        .Visible = false;

                if (dgvGiaHan.Columns.Contains("GhiChu"))
                    dgvGiaHan.Columns["GhiChu"]
                        .Visible = false;

                lblSoLuong.Text =
                    "Số phiếu: " + dt.Rows.Count;

                dgvGiaHan.ClearSelection();
            }
            finally
            {
                dangNap = false;
            }
        }

        private string Cell(
            DataGridViewRow r, string col)
        {
            object value = r.Cells[col].Value;

            return value == null || value == DBNull.Value
                ? "" : Convert.ToString(value);
        }

        private void dgvGiaHan_SelectionChanged(
            object sender, EventArgs e)
        {
            if (dangNap ||
                dgvGiaHan.CurrentRow == null ||
                !dgvGiaHan.CurrentRow.Selected)
                return;

            try
            {
                DataGridViewRow r = dgvGiaHan.CurrentRow;

                maPhieuDangChon =
                    Cell(r, "MaPhieuGiaHan");

                txtMaPhieu.Text = maPhieuDangChon;
                txtMaPhieu.ReadOnly = true;
                txtMaPhieu.BackColor = Color.Gainsboro;

                string maHD = Cell(r, "MaHopDong");
                cboHopDong.SelectedValue = maHD;

                // NapPhong() thực hiện qua sự kiện thay đổi
                // của combobox hợp đồng.
                cboPhong.SelectedValue =
                    Cell(r, "MaChiTietHD");

                dtNgayLap.Value = Convert.ToDateTime(
                    r.Cells["NgayLap"].Value);

                dtTuNgay.Value = Convert.ToDateTime(
                    r.Cells["TuNgay"].Value);

                dtDenNgay.Value = Convert.ToDateTime(
                    r.Cells["DenNgay"].Value);

                nudDonGia.Value = Math.Min(
                    nudDonGia.Maximum,
                    Convert.ToDecimal(
                        r.Cells["DonGiaGiaHan"].Value));

                txtGhiChu.Text = Cell(r, "GhiChu");

                lblTrangThai.Text =
                    "Trạng thái: " + Cell(r, "TrangThai");

                bool choDuyet =
                    Cell(r, "TrangThai") == "Chờ duyệt";

                btnLapPhieu.Enabled = false;
                btnDuyet.Enabled = choDuyet;
                btnTuChoi.Enabled = choDuyet;
                btnHuy.Enabled = choDuyet;
                btnKiemTraNo.Enabled = choDuyet;

                cboHopDong.Enabled = false;
                cboPhong.Enabled = false;
                nudSoThang.Enabled = false;
                nudDonGia.Enabled = false;
                txtGhiChu.ReadOnly = true;
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        private void LamMoiNhap()
        {
            maPhieuDangChon = "";

            txtMaPhieu.ReadOnly = false;
            txtMaPhieu.BackColor = Color.White;
            txtMaPhieu.Clear();
            txtGhiChu.Clear();
            txtGhiChu.ReadOnly = false;

            cboHopDong.Enabled = true;
            cboPhong.Enabled = true;
            nudSoThang.Enabled = true;
            nudDonGia.Enabled = true;

            dtNgayLap.Value = DateTime.Today;

            btnDuyet.Enabled = false;
            btnTuChoi.Enabled = false;
            btnHuy.Enabled = false;
            btnKiemTraNo.Enabled = false;

            lblTrangThai.Text =
                "Trạng thái phiếu mới: Chờ duyệt";

            dgvGiaHan.ClearSelection();
            CapNhatThongTin();

            txtMaPhieu.Focus();
        }

        private void btnLapPhieu_Click(
            object sender, EventArgs e)
        {
            if (maPhieuDangChon != "")
            {
                MessageBox.Show(
                    "Nhấn Làm mới để lập phiếu mới.");
                return;
            }

            try
            {
                svc.LapPhieu(
                    txtMaPhieu.Text,
                    GiaTriCombo(cboPhong),
                    (int)nudSoThang.Value,
                    nudDonGia.Value,
                    txtGhiChu.Text);

                MessageBox.Show(
                    "Đã lập phiếu Chờ duyệt thành công.\n" +
                    "Chưa thay đổi thời hạn thuê thực tế.");

                NapDanhSach(txtTimKiem.Text);
                LamMoiNhap();
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void btnKiemTraNo_Click(
            object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show(
                    svc.KiemTraNo(maPhieuDangChon),
                    "Kiểm tra công nợ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void btnDuyet_Click(
            object sender, EventArgs e)
        {
            if (maPhieuDangChon == "") return;

            if (MessageBox.Show(
                "Duyệt phiếu gia hạn " +
                maPhieuDangChon + "?\n" +
                "Hệ thống sẽ kiểm tra lại công nợ, " +
                "giới hạn gia hạn và lịch thuê.",
                "Xác nhận duyệt",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                svc.Duyet(maPhieuDangChon);
                MessageBox.Show(
                    "Đã duyệt gia hạn thành công!");
                NapDanhSach(txtTimKiem.Text);
                LamMoiNhap();
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void XuLyPhieu(string trangThai)
        {
            if (maPhieuDangChon == "") return;

            if (MessageBox.Show(
                "Chuyển phiếu thành '" +
                trangThai + "'?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                svc.DoiTrangThai(
                    maPhieuDangChon, trangThai);

                MessageBox.Show("Cập nhật thành công.");
                NapDanhSach(txtTimKiem.Text);
                LamMoiNhap();
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void btnTuChoi_Click(
            object sender, EventArgs e)
        {
            XuLyPhieu("Từ chối");
        }

        private void btnHuy_Click(
            object sender, EventArgs e)
        {
            XuLyPhieu("Đã hủy");
        }

        private void btnTimKiem_Click(
            object sender, EventArgs e)
        {
            try
            {
                NapDanhSach(txtTimKiem.Text);
                LamMoiNhap();
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void btnLamMoi_Click(
            object sender, EventArgs e)
        {
            try
            {
                txtTimKiem.Clear();
                NapHopDong();
                NapDanhSach();
                LamMoiNhap();
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void btnDong_Click(
            object sender, EventArgs e)
        {
            Close();
        }

        private void BaoLoi(Exception ex)
        {
            SqlException sql = ex as SqlException;

            if (sql != null &&
                (sql.Number == 2627 || sql.Number == 2601))
            {
                MessageBox.Show(
                    "Mã phiếu gia hạn đã tồn tại.",
                    "Trùng dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show(
                ex.Message,
                "Thông báo nghiệp vụ gia hạn",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }
}
