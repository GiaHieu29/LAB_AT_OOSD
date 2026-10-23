
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Services;

namespace QuanLyHoSoBenhAn
{
    public partial class FrmBenhNhan : Form
    {
        private readonly BenhNhanService svc =
            new BenhNhanService();

        private string duongDanAnh = "";
        private bool dangNapDuLieu = false;

        public FrmBenhNhan()
        {
            InitializeComponent();
        }

        // ======================================
        // 1. KHI MỞ FORM
        // ======================================

        private void FrmBenhNhan_Load(
            object sender, EventArgs e)
        {
            try
            {
                cboGioiTinh.Items.Clear();
                cboGioiTinh.Items.AddRange(
                    new object[]
                    {
                        "Nam",
                        "Nữ",
                        "Khác"
                    });

                cboGioiTinh.SelectedIndex = 0;
                dtNgaySinh.MaxDate = DateTime.Today;

                NapDanhSach();
                XoaTrang();
            }
            catch (Exception ex)
            {
                HienLoi(ex);
            }
        }

        // ======================================
        // 2. HIỂN THỊ DANH SÁCH
        // ======================================

        private void NapDanhSach()
        {
            dangNapDuLieu = true;

            try
            {
                DataTable dt = svc.LayDanhSach();

                dgvBenhNhan.DataSource = dt;

                DinhDangBang();
                lblSoLuong.Text =
                    "Tổng số bệnh nhân: " + dt.Rows.Count;

                dgvBenhNhan.ClearSelection();
            }
            finally
            {
                dangNapDuLieu = false;
            }
        }

        private void DinhDangBang()
        {
            if (dgvBenhNhan.Columns.Count == 0)
                return;

            dgvBenhNhan.Columns["MaBenhNhan"]
                .HeaderText = "Mã bệnh nhân";

            dgvBenhNhan.Columns["HoTen"]
                .HeaderText = "Họ và tên";

            dgvBenhNhan.Columns["NgaySinh"]
                .HeaderText = "Ngày sinh";

            dgvBenhNhan.Columns["GioiTinh"]
                .HeaderText = "Giới tính";

            dgvBenhNhan.Columns["DiaChi"]
                .HeaderText = "Địa chỉ";

            dgvBenhNhan.Columns["DienThoai"]
                .HeaderText = "Điện thoại";

            dgvBenhNhan.Columns["Anh3x4"]
                .HeaderText = "Đường dẫn ảnh";

            dgvBenhNhan.Columns["Anh3x4"].Visible = false;

            dgvBenhNhan.Columns["NgaySinh"]
                .DefaultCellStyle.Format = "dd/MM/yyyy";

            dgvBenhNhan.Columns["HoTen"].FillWeight = 140;
            dgvBenhNhan.Columns["DiaChi"].FillWeight = 170;

            dgvBenhNhan.ColumnHeadersDefaultCellStyle
                .BackColor = Color.FromArgb(219, 230, 241);

            dgvBenhNhan.ColumnHeadersDefaultCellStyle
                .Font = new Font(
                    "Segoe UI", 9F, FontStyle.Bold);

            dgvBenhNhan.EnableHeadersVisualStyles = false;

            dgvBenhNhan.DefaultCellStyle
                .SelectionBackColor =
                    Color.FromArgb(205, 225, 245);

            dgvBenhNhan.DefaultCellStyle
                .SelectionForeColor = Color.Black;
        }

        // ======================================
        // 3. CHỌN BỆNH NHÂN TRONG BẢNG
        // ======================================

        private void dgvBenhNhan_SelectionChanged(
            object sender, EventArgs e)
        {
            if (dangNapDuLieu ||
                dgvBenhNhan.CurrentRow == null ||
                dgvBenhNhan.CurrentRow.Selected == false)
            {
                return;
            }

            try
            {
                DataGridViewRow row =
                    dgvBenhNhan.CurrentRow;

                txtMaBenhNhan.Text =
                    GiaTri(row, "MaBenhNhan");

                txtHoTen.Text =
                    GiaTri(row, "HoTen");

                txtDiaChi.Text =
                    GiaTri(row, "DiaChi");

                txtDienThoai.Text =
                    GiaTri(row, "DienThoai");

                string gioiTinh =
                    GiaTri(row, "GioiTinh");

                if (cboGioiTinh.Items.Contains(gioiTinh))
                    cboGioiTinh.SelectedItem = gioiTinh;
                else
                    cboGioiTinh.SelectedIndex = -1;

                object ngaySinh =
                    row.Cells["NgaySinh"].Value;

                if (ngaySinh != null &&
                    ngaySinh != DBNull.Value)
                {
                    DateTime ngay =
                        Convert.ToDateTime(ngaySinh);

                    if (ngay >= dtNgaySinh.MinDate &&
                        ngay <= dtNgaySinh.MaxDate)
                    {
                        dtNgaySinh.Value = ngay;
                    }
                }

                duongDanAnh = GiaTri(row, "Anh3x4");
                HienThiAnh(duongDanAnh);

                // Khóa mã khi chỉnh sửa.
                txtMaBenhNhan.ReadOnly = true;
                txtMaBenhNhan.BackColor =
                    Color.FromArgb(235, 235, 235);
            }
            catch (Exception ex)
            {
                HienLoi(ex);
            }
        }

        private string GiaTri(
            DataGridViewRow row, string tenCot)
        {
            object value = row.Cells[tenCot].Value;

            return value == null || value == DBNull.Value
                ? ""
                : Convert.ToString(value);
        }

        // ======================================
        // 4. THÊM BỆNH NHÂN
        // ======================================

        private void btnThem_Click(
            object sender, EventArgs e)
        {
            try
            {
                if (txtMaBenhNhan.ReadOnly)
                {
                    MessageBox.Show(
                        "Bạn đang chọn bệnh nhân có sẵn.\n" +
                        "Nhấn Làm mới để nhập bệnh nhân mới.");
                    return;
                }

                svc.Them(
                    txtMaBenhNhan.Text,
                    txtHoTen.Text,
                    dtNgaySinh.Value,
                    cboGioiTinh.Text,
                    txtDiaChi.Text,
                    txtDienThoai.Text,
                    duongDanAnh);

                MessageBox.Show(
                    "Thêm bệnh nhân thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                NapDanhSach();
                XoaTrang();
            }
            catch (Exception ex)
            {
                HienLoi(ex);
            }
        }

        // ======================================
        // 5. SỬA BỆNH NHÂN
        // ======================================

        private void btnSua_Click(
            object sender, EventArgs e)
        {
            if (!txtMaBenhNhan.ReadOnly)
            {
                MessageBox.Show(
                    "Vui lòng chọn bệnh nhân trong bảng " +
                    "trước khi sửa.");
                return;
            }

            try
            {
                svc.Sua(
                    txtMaBenhNhan.Text,
                    txtHoTen.Text,
                    dtNgaySinh.Value,
                    cboGioiTinh.Text,
                    txtDiaChi.Text,
                    txtDienThoai.Text,
                    duongDanAnh);

                MessageBox.Show(
                    "Cập nhật bệnh nhân thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                NapDanhSach();
                XoaTrang();
            }
            catch (Exception ex)
            {
                HienLoi(ex);
            }
        }

        // ======================================
        // 6. XÓA BỆNH NHÂN
        // ======================================

        private void btnXoa_Click(
            object sender, EventArgs e)
        {
            if (!txtMaBenhNhan.ReadOnly)
            {
                MessageBox.Show(
                    "Vui lòng chọn bệnh nhân cần xóa.");
                return;
            }

            DialogResult kq = MessageBox.Show(
                "Bạn có chắc muốn xóa bệnh nhân " +
                txtMaBenhNhan.Text + "?\n" +
                "Bệnh nhân có hồ sơ bệnh án sẽ " +
                "không thể xóa vì ràng buộc dữ liệu.",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (kq != DialogResult.Yes)
                return;

            try
            {
                svc.Xoa(txtMaBenhNhan.Text);

                MessageBox.Show(
                    "Xóa bệnh nhân thành công!");

                NapDanhSach();
                XoaTrang();
            }
            catch (Exception ex)
            {
                HienLoi(ex);
            }
        }

        // ======================================
        // 7. TÌM KIẾM
        // ======================================

        private void btnTimKiem_Click(
            object sender, EventArgs e)
        {
            try
            {
                dangNapDuLieu = true;

                DataTable dt =
                    svc.TimKiem(txtTimKiem.Text);

                dgvBenhNhan.DataSource = dt;

                DinhDangBang();

                lblSoLuong.Text =
                    "Kết quả tìm kiếm: " + dt.Rows.Count;

                dgvBenhNhan.ClearSelection();
                XoaTrangNhap();
            }
            catch (Exception ex)
            {
                HienLoi(ex);
            }
            finally
            {
                dangNapDuLieu = false;
            }
        }

        // ======================================
        // 8. LÀM MỚI
        // ======================================

        private void btnLamMoi_Click(
            object sender, EventArgs e)
        {
            try
            {
                txtTimKiem.Clear();
                NapDanhSach();
                XoaTrang();
            }
            catch (Exception ex)
            {
                HienLoi(ex);
            }
        }

        private void XoaTrang()
        {
            XoaTrangNhap();

            txtTimKiem.Clear();

            if (dgvBenhNhan.Rows.Count > 0)
                dgvBenhNhan.ClearSelection();
        }

        private void XoaTrangNhap()
        {
            txtMaBenhNhan.ReadOnly = false;
            txtMaBenhNhan.BackColor = Color.White;

            txtMaBenhNhan.Clear();
            txtHoTen.Clear();
            txtDiaChi.Clear();
            txtDienThoai.Clear();

            if (cboGioiTinh.Items.Count > 0)
                cboGioiTinh.SelectedIndex = 0;

            dtNgaySinh.Value =
                DateTime.Today.AddYears(-18);

            duongDanAnh = "";
            HienThiAnh("");
        }

        // ======================================
        // 9. CHỌN ẢNH 3x4
        // ======================================

        private void btnChonAnh_Click(
            object sender, EventArgs e)
        {
            using (OpenFileDialog dlg =
                new OpenFileDialog())
            {
                dlg.Title = "Chọn ảnh bệnh nhân";

                dlg.Filter =
                    "Ảnh (*.jpg;*.jpeg;*.png;*.bmp)|" +
                    "*.jpg;*.jpeg;*.png;*.bmp";

                if (dlg.ShowDialog(this) ==
                    DialogResult.OK)
                {
                    try
                    {
                        duongDanAnh = dlg.FileName;
                        HienThiAnh(duongDanAnh);
                    }
                    catch (Exception ex)
                    {
                        HienLoi(ex);
                    }
                }
            }
        }

        private void HienThiAnh(string duongDan)
        {
            if (picAnh.Image != null)
            {
                Image anhCu = picAnh.Image;
                picAnh.Image = null;
                anhCu.Dispose();
            }

            lblAnh.Text = "Chưa chọn ảnh";

            if (string.IsNullOrWhiteSpace(duongDan) ||
                !File.Exists(duongDan))
            {
                return;
            }

            // Đọc ảnh rồi sao chép vào bộ nhớ,
            // tránh khóa file gốc.
            using (Image anhGoc =
                Image.FromFile(duongDan))
            {
                picAnh.Image = new Bitmap(anhGoc);
            }

            lblAnh.Text =
                Path.GetFileName(duongDan);
        }

        // ======================================
        // 10. ĐÓNG FORM
        // ======================================

        private void btnDong_Click(
            object sender, EventArgs e)
        {
            Close();
        }

        private void HienLoi(Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Lỗi quản lý bệnh nhân",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}
