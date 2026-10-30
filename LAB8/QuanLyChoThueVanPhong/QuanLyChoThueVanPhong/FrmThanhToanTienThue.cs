
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChoThueVanPhong.Services;

namespace QuanLyChoThueVanPhong
{
    public partial class FrmThanhToanTienThue : Form
    {
        private readonly ThanhToanTienThueService svc =
            new ThanhToanTienThueService();

        private bool dangNap = false;
        private bool dangChonKy = false;

        public FrmThanhToanTienThue()
        {
            InitializeComponent();
        }

        private void FrmThanhToanTienThue_Load(
            object sender, EventArgs e)
        {
            try
            {
                cboPhuongThuc.Items.AddRange(
                    new object[]
                    {
                        "Tiền mặt",
                        "Chuyển khoản"
                    });

                cboPhuongThuc.SelectedIndex = 0;

                dtNgayThu.Value = DateTime.Today;

                NapHopDong();
                NapDuLieuPhong();
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        // ===================================
        // COMBOBOX HỢP ĐỒNG - PHÒNG
        // ===================================

        private string GiaTriCombo(ComboBox cbo)
        {
            if (cbo.SelectedValue == null ||
                cbo.SelectedValue is DataRowView)
                return "";

            return Convert.ToString(cbo.SelectedValue);
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

        private void NapPhong()
        {
            if (dangNap) return;

            dangNap = true;

            try
            {
                cboPhong.DataSource = null;
                cboPhong.DisplayMember = "HienThi";
                cboPhong.ValueMember = "MaChiTietHD";

                cboPhong.DataSource = svc.LayPhong(
                    GiaTriCombo(cboHopDong));
            }
            finally
            {
                dangNap = false;
            }

            NapDuLieuPhong();
        }

        private void cboHopDong_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            if (!dangNap) NapPhong();
        }

        private void cboPhong_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            if (!dangNap) NapDuLieuPhong();
        }

        // ===================================
        // NẠP KỲ VÀ LỊCH SỬ THANH TOÁN
        // ===================================

        private void NapDuLieuPhong()
        {
            if (dangNap) return;

            string maCT = GiaTriCombo(cboPhong);

            if (maCT == "")
            {
                dgvKy.DataSource = null;
                dgvLichSu.DataSource = null;
                XoaThongTinKy();
                HienTong(0, 0, 0);
                return;
            }

            try
            {
                dangNap = true;
                dangChonKy = false;

                DataTable ky = svc.LayBangKy(maCT);
                dgvKy.DataSource = ky;

                DoiTieuDe(dgvKy, "TuNgay", "Từ ngày");
                DoiTieuDe(dgvKy, "DenNgay", "Đến ngày");
                DoiTieuDe(dgvKy, "HanThu", "Hạn thu");
                DoiTieuDe(dgvKy, "SoThang", "Số tháng");
                DoiTieuDe(dgvKy, "LoaiKy", "Loại kỳ");
                DoiTieuDe(dgvKy, "PhaiThu", "Phải thu");
                DoiTieuDe(dgvKy, "DaThu", "Đã thu");
                DoiTieuDe(dgvKy, "ConLai", "Còn lại");
                DoiTieuDe(dgvKy, "TrangThai", "Trạng thái");

                DinhDangNgay(
                    dgvKy, "TuNgay", "DenNgay", "HanThu");

                DinhDangTien(
                    dgvKy, "PhaiThu", "DaThu", "ConLai");

                dgvKy.ClearSelection();

                NapLichSu();

                DataTable congNo = svc.LayCongNo(maCT);

                if (congNo.Rows.Count > 0)
                {
                    DataRow r = congNo.Rows[0];

                    HienTong(
                        Convert.ToDecimal(r["TongPhaiThu"]),
                        Convert.ToDecimal(r["TongDaThu"]),
                        Convert.ToDecimal(r["NoDenHan"]));
                }

                XoaThongTinKy();
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
            finally
            {
                dangNap = false;
            }
        }

        private void NapLichSu()
        {
            DataTable dt = svc.LayLichSu(
                GiaTriCombo(cboPhong),
                txtTimKiem.Text);

            dgvLichSu.DataSource = dt;

            DoiTieuDe(dgvLichSu,
                "MaThanhToan", "Mã thanh toán");
            DoiTieuDe(dgvLichSu,
                "MaHopDong", "Hợp đồng");
            DoiTieuDe(dgvLichSu,
                "MaPhong", "Phòng");
            DoiTieuDe(dgvLichSu,
                "NgayThanhToan", "Ngày thu");
            DoiTieuDe(dgvLichSu,
                "TuNgay", "Từ ngày");
            DoiTieuDe(dgvLichSu,
                "DenNgay", "Đến ngày");
            DoiTieuDe(dgvLichSu,
                "SoThangThanhToan", "Số tháng");
            DoiTieuDe(dgvLichSu,
                "SoTien", "Số tiền");
            DoiTieuDe(dgvLichSu,
                "PhuongThuc", "Phương thức");

            if (dgvLichSu.Columns.Contains(
                "MaChiTietHD"))
                dgvLichSu.Columns["MaChiTietHD"]
                    .Visible = false;

            DinhDangNgay(dgvLichSu,
                "NgayThanhToan", "TuNgay", "DenNgay");

            DinhDangTien(dgvLichSu, "SoTien");

            lblSoPhieu.Text =
                "Lịch sử: " + dt.Rows.Count + " phiếu";

            dgvLichSu.ClearSelection();
        }

        private void DoiTieuDe(
            DataGridView dgv, string cot, string ten)
        {
            if (dgv.Columns.Contains(cot))
                dgv.Columns[cot].HeaderText = ten;
        }

        private void DinhDangNgay(
            DataGridView dgv, params string[] cot)
        {
            foreach (string c in cot)
                if (dgv.Columns.Contains(c))
                    dgv.Columns[c].DefaultCellStyle.Format =
                        "dd/MM/yyyy";
        }

        private void DinhDangTien(
            DataGridView dgv, params string[] cot)
        {
            foreach (string c in cot)
                if (dgv.Columns.Contains(c))
                    dgv.Columns[c].DefaultCellStyle.Format =
                        "N0";
        }

        private void HienTong(
            decimal phai, decimal da, decimal no)
        {
            lblPhaiThu.Text =
                "Tổng phải thu: " + phai.ToString("N0") + " đ";

            lblDaThu.Text =
                "Đã thu: " + da.ToString("N0") + " đ";

            lblConNo.Text =
                "Nợ đến hạn: " + no.ToString("N0") + " đ";

            lblConNo.ForeColor =
                no > 0 ? Color.Firebrick : Color.DarkGreen;
        }

        // ===================================
        // CHỌN KỲ THANH TOÁN
        // ===================================

        private void dgvKy_SelectionChanged(
            object sender, EventArgs e)
        {
            if (dangNap ||
                dgvKy.CurrentRow == null ||
                !dgvKy.CurrentRow.Selected)
                return;

            try
            {
                DataGridViewRow r = dgvKy.CurrentRow;

                dtTuNgay.Value = Convert.ToDateTime(
                    r.Cells["TuNgay"].Value);

                dtDenNgay.Value = Convert.ToDateTime(
                    r.Cells["DenNgay"].Value);

                nudSoThang.Value = Convert.ToDecimal(
                    r.Cells["SoThang"].Value);

                decimal tien = Convert.ToDecimal(
                    r.Cells["PhaiThu"].Value);

                nudSoTien.Value = Math.Min(
                    nudSoTien.Maximum, tien);

                decimal conLai = Convert.ToDecimal(
                    r.Cells["ConLai"].Value);

                bool chuaThu = Math.Abs(
                    conLai - tien) <= 0.01m;

                dangChonKy = chuaThu;

                btnThuTien.Enabled = chuaThu;

                lblThongBao.Text = chuaThu
                    ? "Đã chọn kỳ chưa thanh toán. " +
                      "Nhập mã phiếu để thu tiền."
                    : "Kỳ này đã có thanh toán. " +
                      "Không thể thu trùng.";
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        private void XoaThongTinKy()
        {
            dangChonKy = false;

            dtTuNgay.Value = DateTime.Today;
            dtDenNgay.Value = DateTime.Today;

            nudSoThang.Value = 1;
            nudSoTien.Value = 0;

            btnThuTien.Enabled = false;

            lblThongBao.Text =
                "Chọn một kỳ chưa thanh toán trong bảng.";
        }

        // ===================================
        // THU TIỀN
        // ===================================

        private void btnThuTien_Click(
            object sender, EventArgs e)
        {
            if (!dangChonKy)
            {
                MessageBox.Show(
                    "Hãy chọn kỳ chưa thanh toán.");
                return;
            }

            if (string.IsNullOrWhiteSpace(
                txtMaThanhToan.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập mã thanh toán.");
                return;
            }

            string noiDung =
                "Thu tiền thuê phòng, kỳ " +
                dtTuNgay.Value.ToString("dd/MM/yyyy") +
                " - " +
                dtDenNgay.Value.ToString("dd/MM/yyyy");

            if (MessageBox.Show(
                "Xác nhận ghi nhận đã thu " +
                nudSoTien.Value.ToString("N0") +
                " đ?\n\n" +
                "Chỉ xác nhận sau khi đã thực sự " +
                "nhận tiền hoặc kiểm tra chuyển khoản.",
                "Xác nhận thu tiền",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                svc.ThuTien(
                    txtMaThanhToan.Text,
                    GiaTriCombo(cboPhong),
                    dtNgayThu.Value.Date,
                    dtTuNgay.Value.Date,
                    dtDenNgay.Value.Date,
                    (int)nudSoThang.Value,
                    nudSoTien.Value,
                    cboPhuongThuc.Text,
                    noiDung);

                MessageBox.Show(
                    "Đã ghi nhận thanh toán thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                txtMaThanhToan.Clear();
                NapDuLieuPhong();
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        // ===================================
        // TÌM KIẾM, LÀM MỚI VÀ ĐÓNG
        // ===================================

        private void btnTimKiem_Click(
            object sender, EventArgs e)
        {
            try
            {
                NapLichSu();
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        private void txtTimKiem_KeyDown(
            object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnTimKiem_Click(sender, EventArgs.Empty);
            }
        }

        private void btnLamMoi_Click(
            object sender, EventArgs e)
        {
            try
            {
                txtMaThanhToan.Clear();
                txtTimKiem.Clear();
                dtNgayThu.Value = DateTime.Today;

                NapHopDong();
                NapDuLieuPhong();
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        private void btnDong_Click(
            object sender, EventArgs e)
        {
            Close();
        }

        private void BaoLoi(Exception ex)
        {
            SqlException sql = ex as SqlException;

            if (sql != null)
            {
                if (sql.Number == 2627 ||
                    sql.Number == 2601)
                {
                    MessageBox.Show(
                        "Mã thanh toán đã tồn tại.",
                        "Trùng dữ liệu",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                if (sql.Number == 1205)
                {
                    MessageBox.Show(
                        "Có giao dịch khác đang xử lý " +
                        "dữ liệu. Hãy tải lại và thử lại.",
                        "Xung đột giao dịch");
                    return;
                }
            }

            MessageBox.Show(
                ex.Message,
                "Lỗi thanh toán tiền thuê",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }
}
