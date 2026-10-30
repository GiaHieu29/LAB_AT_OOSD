
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChoThueVanPhong.Services;

namespace QuanLyChoThueVanPhong
{
    public partial class FrmKhachHang : Form
    {
        private readonly KhachHangService svc =
            new KhachHangService();

        private bool dangNap = false;
        private bool dangSua = false;

        public FrmKhachHang()
        {
            InitializeComponent();
        }

        // ======================================
        // 1. LOAD FORM
        // ======================================

        private void FrmKhachHang_Load(
            object sender, EventArgs e)
        {
            try
            {
                cboLoaiKhachHang.Items.Clear();

                cboLoaiKhachHang.Items.AddRange(
                    new object[]
                    {
                        "Công ty",
                        "Cá nhân"
                    });

                cboLoaiKhachHang.SelectedIndex = 0;

                NapDanhSach();
                LamMoiNhap();
            }
            catch (Exception ex)
            {
                HienLoi(ex);
            }
        }

        // ======================================
        // 2. HIỂN THỊ DANH SÁCH
        // ======================================

        private void NapDanhSach(string tuKhoa = "")
        {
            dangNap = true;

            try
            {
                DataTable dt =
                    svc.LayDanhSach(tuKhoa);

                dgvKhachHang.DataSource = dt;

                lblSoLuong.Text =
                    "Số khách hàng: " + dt.Rows.Count;

                DoiTieuDe("MaKhachHang", "Mã khách hàng");
                DoiTieuDe("TenKhachHang", "Khách hàng / Công ty");
                DoiTieuDe("LoaiKhachHang", "Loại khách hàng");
                DoiTieuDe("MaSoThue", "Mã số thuế");
                DoiTieuDe("NguoiDaiDien", "Người đại diện");
                DoiTieuDe("DienThoai", "Điện thoại");
                DoiTieuDe("Email", "Email");
                DoiTieuDe("DiaChi", "Địa chỉ");
                DoiTieuDe("GhiChu", "Ghi chú");

                dgvKhachHang.ClearSelection();
            }
            finally
            {
                dangNap = false;
            }
        }

        private void DoiTieuDe(
            string tenCot, string tieuDe)
        {
            if (dgvKhachHang.Columns.Contains(tenCot))
            {
                dgvKhachHang.Columns[tenCot]
                    .HeaderText = tieuDe;
            }
        }

        // ======================================
        // 3. CHỌN KHÁCH HÀNG
        // ======================================

        private string LayChuoi(
            DataGridViewRow r, string tenCot)
        {
            object giaTri = r.Cells[tenCot].Value;

            if (giaTri == null ||
                giaTri == DBNull.Value)
            {
                return "";
            }

            return Convert.ToString(giaTri);
        }

        private void dgvKhachHang_SelectionChanged(
            object sender, EventArgs e)
        {
            if (dangNap ||
                dgvKhachHang.CurrentRow == null ||
                !dgvKhachHang.CurrentRow.Selected)
            {
                return;
            }

            try
            {
                DataGridViewRow r =
                    dgvKhachHang.CurrentRow;

                txtMaKhachHang.Text =
                    LayChuoi(r, "MaKhachHang");

                txtTenKhachHang.Text =
                    LayChuoi(r, "TenKhachHang");

                cboLoaiKhachHang.SelectedItem =
                    LayChuoi(r, "LoaiKhachHang");

                txtMaSoThue.Text =
                    LayChuoi(r, "MaSoThue");

                txtNguoiDaiDien.Text =
                    LayChuoi(r, "NguoiDaiDien");

                txtDienThoai.Text =
                    LayChuoi(r, "DienThoai");

                txtEmail.Text =
                    LayChuoi(r, "Email");

                txtDiaChi.Text =
                    LayChuoi(r, "DiaChi");

                txtGhiChu.Text =
                    LayChuoi(r, "GhiChu");

                dangSua = true;

                txtMaKhachHang.ReadOnly = true;

                txtMaKhachHang.BackColor =
                    Color.FromArgb(235, 235, 235);

                lblHuongDan.Text =
                    "Đang sửa khách hàng " +
                    txtMaKhachHang.Text +
                    ". Mã khách hàng không được thay đổi.";
            }
            catch (Exception ex)
            {
                HienLoi(ex);
            }
        }

        // ======================================
        // 4. LÀM MỚI Ô NHẬP
        // ======================================

        private void LamMoiNhap()
        {
            dangSua = false;

            txtMaKhachHang.ReadOnly = false;
            txtMaKhachHang.BackColor = Color.White;

            txtMaKhachHang.Clear();
            txtTenKhachHang.Clear();
            txtMaSoThue.Clear();
            txtNguoiDaiDien.Clear();
            txtDienThoai.Clear();
            txtEmail.Clear();
            txtDiaChi.Clear();
            txtGhiChu.Clear();

            if (cboLoaiKhachHang.Items.Count > 0)
            {
                cboLoaiKhachHang.SelectedIndex = 0;
            }

            lblHuongDan.Text =
                "Nhập thông tin khách hàng mới, " +
                "sau đó nhấn Thêm.";

            dgvKhachHang.ClearSelection();

            txtMaKhachHang.Focus();
        }

        // ======================================
        // 5. THÊM / SỬA
        // ======================================

        private void Luu(bool sua)
        {
            if (sua)
            {
                svc.Sua(
                    txtMaKhachHang.Text,
                    txtTenKhachHang.Text,
                    cboLoaiKhachHang.Text,
                    txtMaSoThue.Text,
                    txtNguoiDaiDien.Text,
                    txtDienThoai.Text,
                    txtEmail.Text,
                    txtDiaChi.Text,
                    txtGhiChu.Text);
            }
            else
            {
                svc.Them(
                    txtMaKhachHang.Text,
                    txtTenKhachHang.Text,
                    cboLoaiKhachHang.Text,
                    txtMaSoThue.Text,
                    txtNguoiDaiDien.Text,
                    txtDienThoai.Text,
                    txtEmail.Text,
                    txtDiaChi.Text,
                    txtGhiChu.Text);
            }

            MessageBox.Show(
                sua
                    ? "Cập nhật khách hàng thành công!"
                    : "Thêm khách hàng thành công!",
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
                    "Bạn đang chọn khách hàng cũ.\n" +
                    "Nhấn Làm mới để thêm khách hàng mới.");
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
                    "Hãy chọn khách hàng trong bảng để sửa.");
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

        // ======================================
        // 6. XÓA
        // ======================================

        private void btnXoa_Click(
            object sender, EventArgs e)
        {
            if (!dangSua)
            {
                MessageBox.Show(
                    "Hãy chọn khách hàng cần xóa.");
                return;
            }

            DialogResult kq = MessageBox.Show(
                "Bạn có muốn xóa khách hàng " +
                txtMaKhachHang.Text + "?\n\n" +
                "Không thể xóa khách hàng đã có " +
                "hợp đồng hoặc nhân viên liên quan.",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (kq != DialogResult.Yes)
            {
                return;
            }

            try
            {
                svc.Xoa(txtMaKhachHang.Text);

                NapDanhSach(txtTimKiem.Text);
                LamMoiNhap();

                MessageBox.Show(
                    "Xóa khách hàng thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
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
                btnTimKiem_Click(sender, EventArgs.Empty);
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
                LamMoiNhap();
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        // ======================================
        // 9. ĐÓNG FORM
        // ======================================

        private void btnDong_Click(
            object sender, EventArgs e)
        {
            Close();
        }

        // ======================================
        // 10. XỬ LÝ LỖI SQL SERVER
        // ======================================

        private void BaoLoi(Exception ex)
        {
            SqlException sql = ex as SqlException;

            if (sql != null)
            {
                if (sql.Number == 2627 ||
                    sql.Number == 2601)
                {
                    MessageBox.Show(
                        "Mã khách hàng hoặc mã số thuế " +
                        "đã tồn tại.\n" +
                        "Vui lòng nhập giá trị khác.",
                        "Trùng dữ liệu",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                if (sql.Number == 547)
                {
                    MessageBox.Show(
                        "Dữ liệu đang được sử dụng " +
                        "trong hợp đồng hoặc bảng khác.\n" +
                        "Không thể xóa hay thay đổi " +
                        "trái với ràng buộc SQL Server.",
                        "Ràng buộc khóa ngoại",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                if (sql.Number == 2628 ||
                    sql.Number == 8152)
                {
                    MessageBox.Show(
                        "Một hoặc nhiều nội dung " +
                        "vượt quá độ dài cột SQL Server.",
                        "Dữ liệu quá dài",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
            }

            HienLoi(ex);
        }

        private void HienLoi(Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Lỗi quản lý khách hàng",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}
