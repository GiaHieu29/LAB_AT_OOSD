
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Services;

namespace QuanLyHoSoBenhAn
{
    public partial class FrmThanhToanVienPhi : Form
    {
        private readonly ThanhToanVienPhiService svc =
            new ThanhToanVienPhiService();

        private bool dangNap = false;
        private bool dangSua = false;

        public FrmThanhToanVienPhi()
        {
            InitializeComponent();
        }

        private void FrmThanhToanVienPhi_Load(
            object sender, EventArgs e)
        {
            try
            {
                dangNap = true;

                cboPhuongThuc.Items.AddRange(
                    new object[]
                    {
                        "Tiền mặt",
                        "Chuyển khoản",
                        "Thẻ ngân hàng"
                    });

                cboPhieuKham.DisplayMember = "HienThi";
                cboPhieuKham.ValueMember = "MaPhieuKham";
                cboPhieuKham.DataSource =
                    svc.LayPhieuKham();

                dangNap = false;

                NapDanhSach();
                LamMoiNhap();
            }
            catch (Exception ex)
            {
                dangNap = false;
                HienLoi(ex);
            }
        }

        private string MaPhieu()
        {
            object value = cboPhieuKham.SelectedValue;

            return value == null ||
                   value is DataRowView
                ? ""
                : Convert.ToString(value);
        }

        private void NapDanhSach(string key = "")
        {
            dangNap = true;

            try
            {
                DataTable dt = svc.LayDanhSach(key);

                dgvThanhToan.DataSource = dt;

                lblSoLuong.Text =
                    "Số khoản thanh toán: " + dt.Rows.Count;

                decimal tongTien = 0;

                foreach (DataRow row in dt.Rows)
                {
                    if (row["SoTien"] != DBNull.Value)
                        tongTien +=
                            Convert.ToDecimal(row["SoTien"]);
                }

                lblTongTien.Text =
                    "Tổng tiền: " +
                    tongTien.ToString("N0") + " VNĐ";

                if (dgvThanhToan.Columns
                    .Contains("MaThanhToan"))
                    dgvThanhToan.Columns["MaThanhToan"]
                        .HeaderText = "Mã thanh toán";

                if (dgvThanhToan.Columns
                    .Contains("MaPhieuKham"))
                    dgvThanhToan.Columns["MaPhieuKham"]
                        .HeaderText = "Mã phiếu khám";

                if (dgvThanhToan.Columns
                    .Contains("TenBenhNhan"))
                    dgvThanhToan.Columns["TenBenhNhan"]
                        .HeaderText = "Bệnh nhân";

                if (dgvThanhToan.Columns
                    .Contains("NgayGioKham"))
                {
                    dgvThanhToan.Columns["NgayGioKham"]
                        .HeaderText = "Ngày khám";

                    dgvThanhToan.Columns["NgayGioKham"]
                        .DefaultCellStyle.Format =
                            "dd/MM/yyyy HH:mm";
                }

                if (dgvThanhToan.Columns
                    .Contains("NgayThanhToan"))
                {
                    dgvThanhToan.Columns["NgayThanhToan"]
                        .HeaderText = "Ngày thanh toán";

                    dgvThanhToan.Columns["NgayThanhToan"]
                        .DefaultCellStyle.Format =
                            "dd/MM/yyyy";
                }

                if (dgvThanhToan.Columns.Contains("SoTien"))
                {
                    dgvThanhToan.Columns["SoTien"]
                        .HeaderText = "Số tiền (VNĐ)";

                    dgvThanhToan.Columns["SoTien"]
                        .DefaultCellStyle.Format = "N0";

                    dgvThanhToan.Columns["SoTien"]
                        .DefaultCellStyle.Alignment =
                        DataGridViewContentAlignment.MiddleRight;
                }

                if (dgvThanhToan.Columns
                    .Contains("PhuongThuc"))
                    dgvThanhToan.Columns["PhuongThuc"]
                        .HeaderText = "Phương thức";

                if (dgvThanhToan.Columns.Contains("GhiChu"))
                    dgvThanhToan.Columns["GhiChu"]
                        .HeaderText = "Ghi chú";

                dgvThanhToan.ClearSelection();
            }
            finally
            {
                dangNap = false;
            }
        }

        private string GiaTri(
            DataGridViewRow row, string cot)
        {
            object value = row.Cells[cot].Value;

            return value == null || value == DBNull.Value
                ? ""
                : Convert.ToString(value);
        }

        private void dgvThanhToan_SelectionChanged(
            object sender, EventArgs e)
        {
            if (dangNap ||
                dgvThanhToan.CurrentRow == null ||
                !dgvThanhToan.CurrentRow.Selected)
                return;

            try
            {
                DataGridViewRow r =
                    dgvThanhToan.CurrentRow;

                txtMaThanhToan.Text =
                    GiaTri(r, "MaThanhToan");

                cboPhieuKham.SelectedValue =
                    GiaTri(r, "MaPhieuKham");

                dtNgayThanhToan.Value =
                    Convert.ToDateTime(
                        r.Cells["NgayThanhToan"].Value);

                decimal soTien = Convert.ToDecimal(
                    r.Cells["SoTien"].Value);

                nudSoTien.Value = Math.Min(
                    nudSoTien.Maximum,
                    Math.Max(nudSoTien.Minimum, soTien));

                string phuongThuc =
                    GiaTri(r, "PhuongThuc");

                if (!cboPhuongThuc.Items.Contains(phuongThuc)
                    && !string.IsNullOrWhiteSpace(phuongThuc))
                {
                    cboPhuongThuc.Items.Add(phuongThuc);
                }

                cboPhuongThuc.SelectedItem = phuongThuc;

                txtGhiChu.Text =
                    GiaTri(r, "GhiChu");

                dangSua = true;
                txtMaThanhToan.ReadOnly = true;
                txtMaThanhToan.BackColor =
                    Color.FromArgb(235, 235, 235);
            }
            catch (Exception ex)
            {
                HienLoi(ex);
            }
        }

        private void LamMoiNhap()
        {
            dangSua = false;

            txtMaThanhToan.ReadOnly = false;
            txtMaThanhToan.BackColor = Color.White;
            txtMaThanhToan.Clear();

            if (cboPhieuKham.Items.Count > 0)
                cboPhieuKham.SelectedIndex = 0;

            if (cboPhuongThuc.Items.Count > 0)
                cboPhuongThuc.SelectedIndex = 0;

            dtNgayThanhToan.Value = DateTime.Today;
            nudSoTien.Value = 0;
            txtGhiChu.Clear();

            dgvThanhToan.ClearSelection();
            txtMaThanhToan.Focus();
        }

        private void Luu(bool sua)
        {
            if (sua)
            {
                svc.Sua(
                    txtMaThanhToan.Text,
                    MaPhieu(),
                    dtNgayThanhToan.Value,
                    nudSoTien.Value,
                    cboPhuongThuc.Text,
                    txtGhiChu.Text);
            }
            else
            {
                svc.Them(
                    txtMaThanhToan.Text,
                    MaPhieu(),
                    dtNgayThanhToan.Value,
                    nudSoTien.Value,
                    cboPhuongThuc.Text,
                    txtGhiChu.Text);
            }

            MessageBox.Show(
                sua
                    ? "Cập nhật thanh toán thành công!"
                    : "Thêm thanh toán thành công!",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            NapDanhSach(txtTimKiem.Text);
            LamMoiNhap();
        }

        private void btnThem_Click(
            object sender, EventArgs e)
        {
            if (dangSua)
            {
                MessageBox.Show(
                    "Nhấn Làm mới trước khi thêm thanh toán.");
                return;
            }

            try { Luu(false); }
            catch (Exception ex) { HienLoi(ex); }
        }

        private void btnSua_Click(
            object sender, EventArgs e)
        {
            if (!dangSua)
            {
                MessageBox.Show(
                    "Vui lòng chọn khoản thanh toán cần sửa.");
                return;
            }

            try { Luu(true); }
            catch (Exception ex) { HienLoi(ex); }
        }

        private void btnXoa_Click(
            object sender, EventArgs e)
        {
            if (!dangSua)
            {
                MessageBox.Show(
                    "Vui lòng chọn khoản thanh toán cần xóa.");
                return;
            }

            DialogResult kq = MessageBox.Show(
                "Xóa khoản thanh toán " +
                    txtMaThanhToan.Text + "?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (kq != DialogResult.Yes)
                return;

            try
            {
                svc.Xoa(txtMaThanhToan.Text);
                NapDanhSach(txtTimKiem.Text);
                LamMoiNhap();

                MessageBox.Show(
                    "Đã xóa khoản thanh toán.");
            }
            catch (Exception ex) { HienLoi(ex); }
        }

        private void btnTimKiem_Click(
            object sender, EventArgs e)
        {
            try
            {
                NapDanhSach(txtTimKiem.Text);
                LamMoiNhap();
            }
            catch (Exception ex) { HienLoi(ex); }
        }

        private void btnLamMoi_Click(
            object sender, EventArgs e)
        {
            try
            {
                txtTimKiem.Clear();
                NapDanhSach();
                LamMoiNhap();
            }
            catch (Exception ex) { HienLoi(ex); }
        }

        private void btnDong_Click(
            object sender, EventArgs e)
        {
            Close();
        }

        private void HienLoi(Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Lỗi quản lý thanh toán viện phí",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}
