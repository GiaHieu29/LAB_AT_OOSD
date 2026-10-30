
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChoThueVanPhong.Services;

namespace QuanLyChoThueVanPhong
{
    public partial class FrmDichVu : Form
    {
        private readonly DichVuService svc =
            new DichVuService();

        private bool loading = false;
        private string selectedInvoice = "";
        private decimal selectedDebt = 0;

        public FrmDichVu()
        {
            InitializeComponent();
        }

        private string Selected(ComboBox cbo)
        {
            if (cbo.SelectedValue == null ||
                cbo.SelectedValue is DataRowView)
                return "";

            return Convert.ToString(cbo.SelectedValue);
        }

        private static void SetColumn(
            DataGridView grid,
            string name,
            string title,
            string format = null)
        {
            if (!grid.Columns.Contains(name))
                return;

            grid.Columns[name].HeaderText = title;

            if (format != null)
                grid.Columns[name]
                    .DefaultCellStyle.Format = format;
        }

        private void Error(Exception ex)
        {
            var sql = ex as SqlException;
            string msg = ex.Message;

            if (sql != null &&
                (sql.Number == 2627 || sql.Number == 2601))
                msg = "Dữ liệu đã tồn tại. Mã hoặc " +
                      "dịch vụ theo phòng/tháng bị trùng.";

            else if (sql != null && sql.Number == 547)
                msg = "Dữ liệu liên quan không hợp lệ " +
                      "hoặc bị ràng buộc khóa ngoại.";

            else if (sql != null && sql.Number == 1205)
                msg = "Có giao dịch khác đang xử lý. " +
                      "Hãy tải lại và thử lại.";

            MessageBox.Show(
                msg,
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        // ============================================
        // 1. LOAD FORM
        // ============================================

        private void FrmDichVu_Load(
            object sender, EventArgs e)
        {
            try
            {
                loading = true;

                cboPhuongThuc.Items.AddRange(
                    new object[]
                    {
                        "Tiền mặt",
                        "Chuyển khoản"
                    });

                cboPhuongThuc.SelectedIndex = 0;

                numThang.Value = DateTime.Today.Month;
                numNam.Value = DateTime.Today.Year;
                dtNgayThu.Value = DateTime.Today;

                cboHopDong.DisplayMember = "HienThi";
                cboHopDong.ValueMember = "MaHopDong";
                cboHopDong.DataSource = svc.LayHopDong();

                cboLoaiDV.DisplayMember = "HienThi";
                cboLoaiDV.ValueMember = "MaDichVu";
                cboLoaiDV.DataSource = svc.LayLoaiDichVu();

                loading = false;

                cboLoaiDV_SelectedIndexChanged(
                    null, EventArgs.Empty);

                ReloadRoom();
                ReloadUsage();
                ReloadInvoices();
            }
            catch (Exception ex)
            {
                loading = false;
                Error(ex);
            }
        }

        // ============================================
        // 2. TẢI PHÒNG
        // ============================================

        private void ReloadRoom()
        {
            if (loading)
                return;

            try
            {
                loading = true;

                cboPhong.DataSource = null;
                cboPhong.DisplayMember = "HienThi";
                cboPhong.ValueMember = "MaChiTietHD";

                cboPhong.DataSource =
                    svc.LayPhong(Selected(cboHopDong));

                loading = false;
            }
            catch (Exception ex)
            {
                loading = false;
                Error(ex);
            }
        }

        // ============================================
        // 3. TẢI SỬ DỤNG DỊCH VỤ
        // ============================================

        private void ReloadUsage()
        {
            if (loading)
                return;

            try
            {
                dgvSuDung.DataSource = svc.LaySuDung(
                    Selected(cboHopDong),
                    (int)numThang.Value,
                    (int)numNam.Value);

                SetColumn(
                    dgvSuDung, "MaSuDung", "Mã sử dụng");

                SetColumn(
                    dgvSuDung, "MaPhong", "Phòng");

                SetColumn(
                    dgvSuDung, "TenDichVu", "Dịch vụ");

                SetColumn(
                    dgvSuDung, "SoLuong", "Số lượng", "N2");

                SetColumn(
                    dgvSuDung, "DonGiaApDung", "Đơn giá", "N0");

                SetColumn(
                    dgvSuDung, "ThanhTien", "Thành tiền", "N0");

                SetColumn(
                    dgvSuDung, "TrangThai", "Trạng thái");

                dgvSuDung.ClearSelection();
            }
            catch (Exception ex)
            {
                Error(ex);
            }
        }

        // ============================================
        // 4. TẢI HÓA ĐƠN
        // ============================================

        private void ReloadInvoices()
        {
            if (loading)
                return;

            try
            {
                selectedInvoice = "";
                selectedDebt = 0;

                btnThu.Enabled = false;
                txtHoaDonChon.Text = "";
                lblCongNo.Text = "Công nợ hóa đơn: 0 đ";

                dgvChiTiet.DataSource = null;
                dgvThanhToan.DataSource = null;

                dgvHoaDon.DataSource =
                    svc.LayHoaDon(Selected(cboHopDong));

                SetColumn(
                    dgvHoaDon, "MaHoaDon", "Mã hóa đơn");

                SetColumn(
                    dgvHoaDon, "Thang", "Tháng");

                SetColumn(
                    dgvHoaDon, "Nam", "Năm");

                SetColumn(
                    dgvHoaDon, "NgayLap",
                    "Ngày lập", "dd/MM/yyyy");

                SetColumn(
                    dgvHoaDon, "HanThanhToan",
                    "Hạn thanh toán", "dd/MM/yyyy");

                SetColumn(
                    dgvHoaDon, "TongTien",
                    "Tổng tiền", "N0");

                SetColumn(
                    dgvHoaDon, "DaThu",
                    "Đã thu", "N0");

                SetColumn(
                    dgvHoaDon, "ConNo",
                    "Còn nợ", "N0");

                SetColumn(
                    dgvHoaDon, "TrangThai", "Trạng thái");

                dgvHoaDon.ClearSelection();
            }
            catch (Exception ex)
            {
                Error(ex);
            }
        }

        // ============================================
        // 5. SỰ KIỆN COMBOBOX
        // ============================================

        private void cboHopDong_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            if (loading)
                return;

            ReloadRoom();
            ReloadUsage();
            ReloadInvoices();
        }

        private void cboLoaiDV_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            if (loading)
                return;

            var row = cboLoaiDV.SelectedItem as DataRowView;

            if (row != null)
            {
                txtDonGia.Text =
                    Convert.ToDecimal(row["DonGia"])
                        .ToString("N0") +
                    " đ / " +
                    Convert.ToString(row["DonViTinh"]);
            }
            else
            {
                txtDonGia.Clear();
            }
        }

        private void numKy_ValueChanged(
            object sender, EventArgs e)
        {
            if (loading)
                return;

            ReloadUsage();
        }

        // ============================================
        // 6. GHI SỬ DỤNG DỊCH VỤ
        // ============================================

        private void btnGhiSuDung_Click(
            object sender, EventArgs e)
        {
            try
            {
                svc.GhiSuDung(
                    txtMaSuDung.Text,
                    Selected(cboPhong),
                    Selected(cboLoaiDV),
                    (int)numThang.Value,
                    (int)numNam.Value,
                    numSoLuong.Value);

                txtMaSuDung.Clear();

                ReloadUsage();

                MessageBox.Show(
                    "Đã lưu sử dụng dịch vụ vào SQL Server.",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Error(ex);
            }
        }

        // ============================================
        // 7. LẬP HÓA ĐƠN
        // ============================================

        private void btnLapHoaDon_Click(
            object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "Lập hóa đơn và chốt dịch vụ " +
                "của hợp đồng trong tháng này?\n\n" +
                "Hãy chắc chắn đã nhập đầy đủ " +
                "điện, nước, Internet của mọi phòng.",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)
                != DialogResult.Yes)
            {
                return;
            }

            try
            {
                svc.LapHoaDon(
                    txtMaHoaDon.Text,
                    Selected(cboHopDong),
                    (int)numThang.Value,
                    (int)numNam.Value);

                txtMaHoaDon.Clear();

                ReloadUsage();
                ReloadInvoices();

                tabMain.SelectedTab = tabHoaDon;

                MessageBox.Show(
                    "Đã lập hóa đơn và chi tiết " +
                    "dịch vụ thành công.");
            }
            catch (Exception ex)
            {
                Error(ex);
            }
        }

        // ============================================
        // 8. CHỌN HÓA ĐƠN
        // ============================================

        private void dgvHoaDon_SelectionChanged(
            object sender, EventArgs e)
        {
            if (loading ||
                dgvHoaDon.CurrentRow == null ||
                !dgvHoaDon.CurrentRow.Selected)
                return;

            try
            {
                var r = dgvHoaDon.CurrentRow;

                selectedInvoice = Convert.ToString(
                    r.Cells["MaHoaDon"].Value);

                selectedDebt = Convert.ToDecimal(
                    r.Cells["ConNo"].Value);

                txtHoaDonChon.Text = selectedInvoice;

                lblCongNo.Text =
                    "Công nợ hóa đơn: " +
                    selectedDebt.ToString("N0") + " đ";

                lblCongNo.ForeColor =
                    selectedDebt > 0
                    ? Color.Firebrick
                    : Color.DarkGreen;

                numTienThu.Value = Math.Min(
                    numTienThu.Maximum,
                    Math.Max(numTienThu.Minimum, selectedDebt));

                btnThu.Enabled = selectedDebt > 0;

                dgvChiTiet.DataSource =
                    svc.LayChiTietHoaDon(selectedInvoice);

                dgvThanhToan.DataSource =
                    svc.LayLichSuThanhToan(selectedInvoice);

                SetColumn(
                    dgvChiTiet, "MaPhong", "Phòng");

                SetColumn(
                    dgvChiTiet, "TenDichVu", "Dịch vụ");

                SetColumn(
                    dgvChiTiet, "DonViTinh", "Đơn vị");

                SetColumn(
                    dgvChiTiet, "SoLuong", "Số lượng", "N2");

                SetColumn(
                    dgvChiTiet, "DonGiaApDung",
                    "Đơn giá", "N0");

                SetColumn(
                    dgvChiTiet, "SoTien",
                    "Thành tiền", "N0");

                SetColumn(
                    dgvThanhToan, "NgayThanhToan",
                    "Ngày thu", "dd/MM/yyyy");

                SetColumn(
                    dgvThanhToan, "SoTien",
                    "Số tiền", "N0");

                dgvChiTiet.ClearSelection();
                dgvThanhToan.ClearSelection();
            }
            catch (Exception ex)
            {
                Error(ex);
            }
        }

        // ============================================
        // 9. GHI NHẬN THANH TOÁN
        // ============================================

        private void btnThu_Click(
            object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(selectedInvoice))
            {
                MessageBox.Show(
                    "Hãy chọn hóa đơn trong bảng.");
                return;
            }

            if (MessageBox.Show(
                "Xác nhận đã thực nhận " +
                numTienThu.Value.ToString("N0") +
                " đ cho hóa đơn " +
                selectedInvoice + "?",
                "Xác nhận tiền thu",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)
                != DialogResult.Yes)
            {
                return;
            }

            try
            {
                svc.ThanhToan(
                    txtMaThu.Text,
                    selectedInvoice,
                    dtNgayThu.Value,
                    numTienThu.Value,
                    cboPhuongThuc.Text,
                    txtGhiChu.Text);

                txtMaThu.Clear();
                txtGhiChu.Clear();

                ReloadInvoices();

                tabMain.SelectedTab = tabThanhToan;

                MessageBox.Show(
                    "Đã ghi nhận tiền thu. " +
                    "Công nợ đã được cập nhật.");
            }
            catch (Exception ex)
            {
                Error(ex);
            }
        }

        // ============================================
        // 10. LÀM MỚI
        // ============================================

        private void btnLamMoi_Click(
            object sender, EventArgs e)
        {
            try
            {
                ReloadRoom();
                ReloadUsage();
                ReloadInvoices();

                MessageBox.Show(
                    "Đã tải lại dữ liệu từ SQL Server.");
            }
            catch (Exception ex)
            {
                Error(ex);
            }
        }

        // ============================================
        // 11. ĐÓNG FORM
        // ============================================

        private void btnDong_Click(
            object sender, EventArgs e)
        {
            Close();
        }
    }
}
