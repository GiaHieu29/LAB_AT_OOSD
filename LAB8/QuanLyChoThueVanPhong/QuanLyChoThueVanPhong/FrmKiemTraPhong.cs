
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChoThueVanPhong.Services;

namespace QuanLyChoThueVanPhong
{
    public partial class FrmKiemTraPhong : Form
    {
        private readonly KiemTraPhongService svc =
            new KiemTraPhongService();

        private bool dangNap = false;
        private bool dangSua = false;

        public FrmKiemTraPhong()
        {
            InitializeComponent();
        }

        private string MaPhong()
        {
            if (cboPhong.SelectedValue == null ||
                cboPhong.SelectedValue is DataRowView)
                return "";

            return Convert.ToString(
                cboPhong.SelectedValue);
        }

        // ==========================================
        // 1. LOAD FORM
        // ==========================================
        private void FrmKiemTraPhong_Load(
            object sender, EventArgs e)
        {
            try
            {
                cboTinhTrang.Items.AddRange(new object[]
                {
                    "Tốt",
                    "Bình thường",
                    "Cần bảo trì",
                    "Hư hỏng",
                    "Không sử dụng được"
                });

                NapPhong();
                NapDanhSach();
                LamMoiNhap();
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        // ==========================================
        // 2. NẠP DANH SÁCH PHÒNG
        // ==========================================
        private void NapPhong()
        {
            dangNap = true;

            try
            {
                cboPhong.DataSource = null;
                cboPhong.DisplayMember = "HienThi";
                cboPhong.ValueMember = "MaPhong";
                cboPhong.DataSource = svc.LayPhong();
            }
            finally
            {
                dangNap = false;
            }
        }

        // ==========================================
        // 3. NẠP DANH SÁCH PHIẾU
        // ==========================================
        private void NapDanhSach(string key = "")
        {
            dangNap = true;

            try
            {
                DataTable dt = svc.LayDanhSach(key);
                dgvKiemTra.DataSource = dt;

                DoiTieuDe("MaKiemTra", "Mã kiểm tra");
                DoiTieuDe("MaPhong", "Mã phòng");
                DoiTieuDe("TenPhong", "Tên phòng");
                DoiTieuDe("TenTang", "Tầng");
                DoiTieuDe("NgayKiemTra", "Ngày kiểm tra");
                DoiTieuDe("TinhTrang", "Tình trạng");
                DoiTieuDe("NoiDung", "Nội dung");
                DoiTieuDe("NguoiKiemTra", "Người kiểm tra");
                DoiTieuDe("DeXuatSuaChua", "Đề xuất sửa chữa");
                DoiTieuDe("SoLanSuaChua", "Số lần sửa chữa");

                if (dgvKiemTra.Columns.Contains(
                    "NgayKiemTra"))
                {
                    dgvKiemTra.Columns["NgayKiemTra"]
                        .DefaultCellStyle.Format =
                        "dd/MM/yyyy";
                }

                lblSoLuong.Text =
                    "Số phiếu: " + dt.Rows.Count;

                dgvKiemTra.ClearSelection();
            }
            finally
            {
                dangNap = false;
            }
        }

        private void DoiTieuDe(
            string col, string text)
        {
            if (dgvKiemTra.Columns.Contains(col))
                dgvKiemTra.Columns[col].HeaderText = text;
        }

        private string Cell(
            DataGridViewRow row, string col)
        {
            object value = row.Cells[col].Value;

            if (value == null || value == DBNull.Value)
                return "";

            return Convert.ToString(value);
        }

        // ==========================================
        // 4. CHỌN PHIẾU KIỂM TRA
        // ==========================================
        private void dgvKiemTra_SelectionChanged(
            object sender, EventArgs e)
        {
            if (dangNap ||
                dgvKiemTra.CurrentRow == null ||
                !dgvKiemTra.CurrentRow.Selected)
                return;

            try
            {
                DataGridViewRow row =
                    dgvKiemTra.CurrentRow;

                txtMaKiemTra.Text =
                    Cell(row, "MaKiemTra");

                cboPhong.SelectedValue =
                    Cell(row, "MaPhong");

                dtNgayKiemTra.Value =
                    Convert.ToDateTime(
                        row.Cells["NgayKiemTra"].Value);

                string trangThai = Cell(row, "TinhTrang");

                if (!cboTinhTrang.Items.Contains(trangThai))
                    cboTinhTrang.Items.Add(trangThai);

                cboTinhTrang.SelectedItem = trangThai;

                txtNoiDung.Text =
                    Cell(row, "NoiDung");

                txtNguoiKiemTra.Text =
                    Cell(row, "NguoiKiemTra");

                txtDeXuat.Text =
                    Cell(row, "DeXuatSuaChua");

                int soLan = Convert.ToInt32(
                    row.Cells["SoLanSuaChua"].Value);

                dangSua = true;
                txtMaKiemTra.ReadOnly = true;
                txtMaKiemTra.BackColor = Color.Gainsboro;

                // Không cho đổi phòng nếu phiếu
                // đã được dùng trong nghiệp vụ sửa chữa.
                cboPhong.Enabled = soLan == 0;

                lblHuongDan.Text =
                    "Đang sửa " + txtMaKiemTra.Text +
                    (soLan > 0
                        ? " | Đã liên kết sửa chữa, không đổi phòng."
                        : " | Có thể chỉnh sửa thông tin.");
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        // ==========================================
        // 5. LÀM MỚI Ô NHẬP
        // ==========================================
        private void LamMoiNhap()
        {
            dangSua = false;

            txtMaKiemTra.ReadOnly = false;
            txtMaKiemTra.BackColor = Color.White;

            txtMaKiemTra.Clear();
            txtNoiDung.Clear();
            txtNguoiKiemTra.Clear();
            txtDeXuat.Clear();

            cboPhong.Enabled = true;

            if (cboPhong.Items.Count > 0)
                cboPhong.SelectedIndex = 0;

            if (cboTinhTrang.Items.Count > 0)
                cboTinhTrang.SelectedIndex = 0;

            dtNgayKiemTra.Value = DateTime.Today;

            dgvKiemTra.ClearSelection();

            lblHuongDan.Text =
                "Nhập thông tin kiểm tra mới, sau đó nhấn Thêm.";

            txtMaKiemTra.Focus();
        }

        // ==========================================
        // 6. CẢNH BÁO KIỂM TRA CÙNG NĂM
        // ==========================================
        private bool XacNhanKiemTraTrongNam()
        {
            int soPhieu = svc.DemKiemTraTrongNam(
                MaPhong(),
                dtNgayKiemTra.Value.Year,
                dangSua ? txtMaKiemTra.Text : "");

            if (soPhieu == 0)
                return true;

            return MessageBox.Show(
                "Phòng này đã có " + soPhieu +
                " phiếu kiểm tra trong năm " +
                dtNgayKiemTra.Value.Year + ".\n\n" +
                "Bạn có muốn ghi thêm một đợt " +
                "kiểm tra khác không?",
                "Kiểm tra định kỳ",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes;
        }

        // ==========================================
        // 7. THÊM / SỬA PHIẾU
        // ==========================================
        private void Luu(bool sua)
        {
            if (!XacNhanKiemTraTrongNam())
                return;

            if (sua)
            {
                svc.Sua(
                    txtMaKiemTra.Text,
                    MaPhong(),
                    dtNgayKiemTra.Value,
                    cboTinhTrang.Text,
                    txtNoiDung.Text,
                    txtNguoiKiemTra.Text,
                    txtDeXuat.Text);
            }
            else
            {
                svc.Them(
                    txtMaKiemTra.Text,
                    MaPhong(),
                    dtNgayKiemTra.Value,
                    cboTinhTrang.Text,
                    txtNoiDung.Text,
                    txtNguoiKiemTra.Text,
                    txtDeXuat.Text);
            }

            NapDanhSach(txtTimKiem.Text);
            LamMoiNhap();

            MessageBox.Show(
                sua
                    ? "Cập nhật phiếu kiểm tra thành công!"
                    : "Thêm phiếu kiểm tra thành công!",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void btnThem_Click(
            object sender, EventArgs e)
        {
            if (dangSua)
            {
                MessageBox.Show(
                    "Nhấn Làm mới để thêm phiếu mới.");
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
                    "Hãy chọn phiếu cần sửa.");
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

        // ==========================================
        // 8. XÓA PHIẾU
        // ==========================================
        private void btnXoa_Click(
            object sender, EventArgs e)
        {
            if (!dangSua)
            {
                MessageBox.Show(
                    "Hãy chọn phiếu cần xóa.");
                return;
            }

            if (MessageBox.Show(
                "Bạn muốn xóa phiếu " +
                txtMaKiemTra.Text + "?\n\n" +
                "Phiếu đã liên kết sửa chữa " +
                "không thể xóa.",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)
                != DialogResult.Yes)
                return;

            try
            {
                svc.Xoa(txtMaKiemTra.Text);

                NapDanhSach(txtTimKiem.Text);
                LamMoiNhap();

                MessageBox.Show(
                    "Xóa phiếu kiểm tra thành công!");
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        // ==========================================
        // 9. TÌM KIẾM
        // ==========================================
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

        // ==========================================
        // 10. LÀM MỚI
        // ==========================================
        private void btnLamMoi_Click(
            object sender, EventArgs e)
        {
            try
            {
                txtTimKiem.Clear();
                NapPhong();
                NapDanhSach();
                LamMoiNhap();
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

        // ==========================================
        // 11. THÔNG BÁO LỖI SQL SERVER
        // ==========================================
        private void BaoLoi(Exception ex)
        {
            SqlException sql = ex as SqlException;

            if (sql != null)
            {
                if (sql.Number == 2627 ||
                    sql.Number == 2601)
                {
                    MessageBox.Show(
                        "Mã kiểm tra đã tồn tại.",
                        "Trùng dữ liệu",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                if (sql.Number == 547)
                {
                    MessageBox.Show(
                        "Phiếu hoặc phòng đang liên kết " +
                        "với dữ liệu khác.\n" +
                        "Không thể thực hiện.",
                        "Ràng buộc SQL Server",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                if (sql.Number == 1205)
                {
                    MessageBox.Show(
                        "Có giao dịch khác đang xử lý. " +
                        "Hãy tải lại dữ liệu.",
                        "Xung đột giao dịch",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
            }

            MessageBox.Show(
                ex.Message,
                "Lỗi quản lý kiểm tra phòng",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }
}
