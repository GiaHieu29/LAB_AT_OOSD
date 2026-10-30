
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChoThueVanPhong.Services;

namespace QuanLyChoThueVanPhong
{
    public partial class FrmTheGiuXe : Form
    {
        private readonly TheGiuXeService svc =
            new TheGiuXeService();

        private bool dangNap = false;
        private bool dangSua = false;

        public FrmTheGiuXe()
        {
            InitializeComponent();
        }

        // ====================================
        // 1. LOAD FORM
        // ====================================
        private void FrmTheGiuXe_Load(
            object sender, EventArgs e)
        {
            try
            {
                cboLoaiXe.Items.AddRange(new object[]
                {
                    "Xe máy",
                    "Ô tô",
                    "Xe đạp",
                    "Xe máy điện",
                    "Xe đạp điện",
                    "Khác"
                });

                cboTrangThai.Items.AddRange(new object[]
                {
                    "Đang sử dụng",
                    "Đã thu hồi",
                    "Bị mất"
                });

                NapNhanVien();
                NapDanhSach();
                LamMoiNhap();
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        // ====================================
        // 2. NẠP NHÂN VIÊN
        // ====================================
        private void NapNhanVien()
        {
            dangNap = true;

            try
            {
                cboNhanVien.DataSource = null;
                cboNhanVien.DisplayMember = "HienThi";
                cboNhanVien.ValueMember = "MaNhanVien";
                cboNhanVien.DataSource = svc.LayNhanVien();
            }
            finally
            {
                dangNap = false;
            }
        }

        private string MaNhanVienDaChon()
        {
            if (cboNhanVien.SelectedValue == null ||
                cboNhanVien.SelectedValue is DataRowView)
                return "";

            return Convert.ToString(
                cboNhanVien.SelectedValue);
        }

        // ====================================
        // 3. NẠP DANH SÁCH THẺ
        // ====================================
        private void NapDanhSach(string tuKhoa = "")
        {
            dangNap = true;

            try
            {
                DataTable dt =
                    svc.LayDanhSach(tuKhoa);

                dgvTheGiuXe.DataSource = dt;

                DoiTieuDe("MaThe", "Mã thẻ");
                DoiTieuDe("MaNhanVien", "Mã nhân viên");
                DoiTieuDe("HoTen", "Họ tên");
                DoiTieuDe("TenKhachHang", "Công ty");
                DoiTieuDe("BienSoXe", "Biển số xe");
                DoiTieuDe("LoaiXe", "Loại xe");
                DoiTieuDe("MauXe", "Màu xe");
                DoiTieuDe("NgayCap", "Ngày cấp");
                DoiTieuDe("TrangThai", "Trạng thái");

                if (dgvTheGiuXe.Columns.Contains("NgayCap"))
                {
                    dgvTheGiuXe.Columns["NgayCap"]
                        .DefaultCellStyle.Format =
                        "dd/MM/yyyy";
                }

                lblSoLuong.Text =
                    "Số thẻ: " + dt.Rows.Count;

                dgvTheGiuXe.ClearSelection();
            }
            finally
            {
                dangNap = false;
            }
        }

        private void DoiTieuDe(
            string cot, string tieuDe)
        {
            if (dgvTheGiuXe.Columns.Contains(cot))
                dgvTheGiuXe.Columns[cot]
                    .HeaderText = tieuDe;
        }

        private string GiaTri(
            DataGridViewRow r, string cot)
        {
            object value = r.Cells[cot].Value;

            if (value == null || value == DBNull.Value)
                return "";

            return Convert.ToString(value);
        }

        // ====================================
        // 4. CHỌN THẺ TRONG DATAGRIDVIEW
        // ====================================
        private void dgvTheGiuXe_SelectionChanged(
            object sender, EventArgs e)
        {
            if (dangNap ||
                dgvTheGiuXe.CurrentRow == null ||
                !dgvTheGiuXe.CurrentRow.Selected)
                return;

            try
            {
                DataGridViewRow r =
                    dgvTheGiuXe.CurrentRow;

                txtMaThe.Text =
                    GiaTri(r, "MaThe");

                cboNhanVien.SelectedValue =
                    GiaTri(r, "MaNhanVien");

                txtBienSo.Text =
                    GiaTri(r, "BienSoXe");

                string loai = GiaTri(r, "LoaiXe");

                if (!cboLoaiXe.Items.Contains(loai) &&
                    !string.IsNullOrEmpty(loai))
                {
                    cboLoaiXe.Items.Add(loai);
                }

                cboLoaiXe.SelectedItem = loai;

                txtMauXe.Text =
                    GiaTri(r, "MauXe");

                dtNgayCap.Value =
                    Convert.ToDateTime(
                        r.Cells["NgayCap"].Value);

                cboTrangThai.SelectedItem =
                    GiaTri(r, "TrangThai");

                dangSua = true;

                txtMaThe.ReadOnly = true;
                txtMaThe.BackColor = Color.Gainsboro;

                lblHuongDan.Text =
                    "Đang sửa thẻ " +
                    txtMaThe.Text +
                    ". Mã thẻ không được thay đổi.";
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        // ====================================
        // 5. LÀM MỚI THÔNG TIN
        // ====================================
        private void LamMoiNhap()
        {
            dangSua = false;

            txtMaThe.ReadOnly = false;
            txtMaThe.BackColor = Color.White;

            txtMaThe.Clear();
            txtBienSo.Clear();
            txtMauXe.Clear();

            dtNgayCap.Value = DateTime.Today;

            if (cboNhanVien.Items.Count > 0)
                cboNhanVien.SelectedIndex = 0;

            if (cboLoaiXe.Items.Count > 0)
                cboLoaiXe.SelectedIndex = 0;

            if (cboTrangThai.Items.Count > 0)
                cboTrangThai.SelectedIndex = 0;

            dgvTheGiuXe.ClearSelection();

            lblHuongDan.Text =
                "Nhập thông tin thẻ mới, " +
                "sau đó nhấn Thêm.";

            txtMaThe.Focus();
        }

        // ====================================
        // 6. LƯU THÊM / SỬA
        // ====================================
        private void Luu(bool sua)
        {
            if (sua)
            {
                svc.Sua(
                    txtMaThe.Text,
                    MaNhanVienDaChon(),
                    txtBienSo.Text,
                    cboLoaiXe.Text,
                    txtMauXe.Text,
                    dtNgayCap.Value.Date,
                    cboTrangThai.Text);
            }
            else
            {
                svc.Them(
                    txtMaThe.Text,
                    MaNhanVienDaChon(),
                    txtBienSo.Text,
                    cboLoaiXe.Text,
                    txtMauXe.Text,
                    dtNgayCap.Value.Date,
                    cboTrangThai.Text);
            }

            MessageBox.Show(
                sua
                    ? "Cập nhật thẻ giữ xe thành công!"
                    : "Cấp thẻ giữ xe thành công!",
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
                    "Bạn đang chọn một thẻ cũ.\n" +
                    "Nhấn Làm mới để cấp thẻ mới.");
                return;
            }

            try
            {
                Luu(false);
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        private void btnSua_Click(
            object sender, EventArgs e)
        {
            if (!dangSua)
            {
                MessageBox.Show(
                    "Hãy chọn thẻ cần sửa trong bảng.");
                return;
            }

            try
            {
                Luu(true);
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        // ====================================
        // 7. XÓA THẺ
        // ====================================
        private void btnXoa_Click(
            object sender, EventArgs e)
        {
            if (!dangSua)
            {
                MessageBox.Show(
                    "Hãy chọn thẻ cần xóa.");
                return;
            }

            if (MessageBox.Show(
                "Bạn có muốn xóa thẻ " +
                txtMaThe.Text + "?\n\n" +
                "Thẻ đang sử dụng phải được " +
                "thu hồi trước khi xóa.",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)
                != DialogResult.Yes)
                return;

            try
            {
                svc.Xoa(txtMaThe.Text);

                NapDanhSach(txtTimKiem.Text);
                LamMoiNhap();

                MessageBox.Show(
                    "Xóa thẻ giữ xe thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        // ====================================
        // 8. TÌM KIẾM
        // ====================================
        private void btnTimKiem_Click(
            object sender, EventArgs e)
        {
            try
            {
                NapDanhSach(txtTimKiem.Text);
                LamMoiNhap();
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

                btnTimKiem_Click(
                    sender, EventArgs.Empty);
            }
        }

        // ====================================
        // 9. LÀM MỚI
        // ====================================
        private void btnLamMoi_Click(
            object sender, EventArgs e)
        {
            try
            {
                txtTimKiem.Clear();

                NapNhanVien();
                NapDanhSach();
                LamMoiNhap();
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        // ====================================
        // 10. ĐÓNG FORM
        // ====================================
        private void btnDong_Click(
            object sender, EventArgs e)
        {
            Close();
        }

        // ====================================
        // 11. THÔNG BÁO LỖI
        // ====================================
        private void BaoLoi(Exception ex)
        {
            SqlException sql = ex as SqlException;

            if (sql != null)
            {
                if (sql.Number == 2627 ||
                    sql.Number == 2601)
                {
                    MessageBox.Show(
                        "Mã thẻ đã tồn tại. " +
                        "Vui lòng nhập mã khác.",
                        "Trùng dữ liệu",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                if (sql.Number == 547)
                {
                    MessageBox.Show(
                        "Không thể thực hiện vì nhân viên " +
                        "hoặc dữ liệu liên kết không hợp lệ.",
                        "Lỗi ràng buộc SQL Server",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                if (sql.Number == 1205)
                {
                    MessageBox.Show(
                        "Dữ liệu đang được giao dịch khác " +
                        "xử lý. Hãy tải lại và thử lại.",
                        "Xung đột giao dịch",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
            }

            MessageBox.Show(
                ex.Message,
                "Lỗi quản lý thẻ giữ xe",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }
}
