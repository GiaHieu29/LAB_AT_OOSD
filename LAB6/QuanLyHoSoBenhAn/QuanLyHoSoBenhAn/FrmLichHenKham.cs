
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Services;

namespace QuanLyHoSoBenhAn
{
    public partial class FrmLichHenKham : Form
    {
        private readonly LichHenKhamService svc =
            new LichHenKhamService();

        private bool dangNap = false;
        private bool dangSua = false;

        public FrmLichHenKham()
        {
            InitializeComponent();
        }

        private void FrmLichHenKham_Load(
            object sender, EventArgs e)
        {
            try
            {
                dangNap = true;

                cboLoaiHen.Items.AddRange(new object[]
                {
                    "Khám bệnh",
                    "Khám định kỳ",
                    "Tái khám"
                });

                cboTrangThai.Items.AddRange(new object[]
                {
                    "Đã đặt lịch",
                    "Đã khám",
                    "Đã hủy"
                });

                GanCombo(cboHoSo, svc.LayHoSo(),
                    "HienThi", "MaHoSo");

                GanCombo(cboKhoa, svc.LayKhoa(),
                    "TenKhoa", "MaKhoa");

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

        private void GanCombo(
            ComboBox cbo, DataTable dt,
            string hienThi, string giaTri)
        {
            cbo.DisplayMember = hienThi;
            cbo.ValueMember = giaTri;
            cbo.DataSource = dt;
        }

        private string MaChon(ComboBox cbo)
        {
            object value = cbo.SelectedValue;

            if (value == null || value is DataRowView)
                return "";

            return Convert.ToString(value);
        }

        // ===================================
        // DANH SÁCH
        // ===================================

        private void NapDanhSach(string key = "")
        {
            dangNap = true;

            try
            {
                DataTable dt = svc.LayDanhSach(key);
                dgvLichHen.DataSource = dt;

                string[] tieuDe =
                {
                    "Mã lịch hẹn",
                    "Mã hồ sơ",
                    "Bệnh nhân",
                    "Mã khoa",
                    "Khoa điều trị",
                    "Ngày giờ hẹn",
                    "Loại hẹn",
                    "Nội dung",
                    "Trạng thái"
                };

                for (int i = 0;
                    i < dgvLichHen.Columns.Count &&
                    i < tieuDe.Length; i++)
                {
                    dgvLichHen.Columns[i].HeaderText =
                        tieuDe[i];
                }

                if (dgvLichHen.Columns
                    .Contains("MaKhoa"))
                {
                    dgvLichHen.Columns["MaKhoa"]
                        .Visible = false;
                }

                if (dgvLichHen.Columns
                    .Contains("NgayGioHen"))
                {
                    dgvLichHen.Columns["NgayGioHen"]
                        .DefaultCellStyle.Format =
                            "dd/MM/yyyy HH:mm";
                }

                lblSoLuong.Text =
                    "Số lịch hẹn: " + dt.Rows.Count;

                dgvLichHen.ClearSelection();
            }
            finally
            {
                dangNap = false;
            }
        }

        // ===================================
        // CHỌN LỊCH HẸN
        // ===================================

        private string GiaTri(
            DataGridViewRow row, string cot)
        {
            object value = row.Cells[cot].Value;

            return value == null || value == DBNull.Value
                ? ""
                : Convert.ToString(value);
        }

        private void dgvLichHen_SelectionChanged(
            object sender, EventArgs e)
        {
            if (dangNap ||
                dgvLichHen.CurrentRow == null ||
                !dgvLichHen.CurrentRow.Selected)
                return;

            try
            {
                dangNap = true;

                DataGridViewRow row =
                    dgvLichHen.CurrentRow;

                txtMaLichHen.Text =
                    GiaTri(row, "MaLichHen");

                cboHoSo.SelectedValue =
                    GiaTri(row, "MaHoSo");

                cboKhoa.SelectedValue =
                    GiaTri(row, "MaKhoa");

                dtNgayHen.Value =
                    Convert.ToDateTime(
                        row.Cells["NgayGioHen"].Value);

                cboLoaiHen.SelectedItem =
                    GiaTri(row, "LoaiHen");

                cboTrangThai.SelectedItem =
                    GiaTri(row, "TrangThai");

                txtNoiDung.Text =
                    GiaTri(row, "NoiDung");

                dangSua = true;
                txtMaLichHen.ReadOnly = true;
                txtMaLichHen.BackColor =
                    Color.FromArgb(235, 235, 235);
            }
            catch (Exception ex)
            {
                HienLoi(ex);
            }
            finally
            {
                dangNap = false;
            }
        }

        // ===================================
        // THÊM / SỬA
        // ===================================

        private void Luu(bool sua)
        {
            string maHoSo = MaChon(cboHoSo);
            string maKhoa = MaChon(cboKhoa);

            if (sua)
            {
                svc.Sua(
                    txtMaLichHen.Text,
                    maHoSo,
                    maKhoa,
                    dtNgayHen.Value,
                    cboLoaiHen.Text,
                    txtNoiDung.Text,
                    cboTrangThai.Text);
            }
            else
            {
                svc.Them(
                    txtMaLichHen.Text,
                    maHoSo,
                    maKhoa,
                    dtNgayHen.Value,
                    cboLoaiHen.Text,
                    txtNoiDung.Text,
                    cboTrangThai.Text);
            }

            MessageBox.Show(
                sua
                    ? "Cập nhật lịch hẹn thành công!"
                    : "Tạo lịch hẹn thành công!",
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
                    "Nhấn Làm mới để tạo lịch hẹn mới.");
                return;
            }

            try
            {
                Luu(false);
            }
            catch (Exception ex)
            {
                HienLoi(ex);
            }
        }

        private void btnSua_Click(
            object sender, EventArgs e)
        {
            if (!dangSua)
            {
                MessageBox.Show(
                    "Hãy chọn lịch hẹn cần sửa.");
                return;
            }

            try
            {
                Luu(true);
            }
            catch (Exception ex)
            {
                HienLoi(ex);
            }
        }

        // ===================================
        // XÓA
        // ===================================

        private void btnXoa_Click(
            object sender, EventArgs e)
        {
            if (!dangSua)
            {
                MessageBox.Show(
                    "Hãy chọn lịch hẹn cần xóa.");
                return;
            }

            DialogResult kq = MessageBox.Show(
                "Bạn có chắc muốn xóa lịch hẹn " +
                    txtMaLichHen.Text + "?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (kq != DialogResult.Yes)
                return;

            try
            {
                svc.Xoa(txtMaLichHen.Text);

                NapDanhSach(txtTimKiem.Text);
                LamMoiNhap();

                MessageBox.Show(
                    "Xóa lịch hẹn thành công.");
            }
            catch (Exception ex)
            {
                HienLoi(ex);
            }
        }

        // ===================================
        // TÌM KIẾM VÀ LÀM MỚI
        // ===================================

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
                HienLoi(ex);
            }
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
            catch (Exception ex)
            {
                HienLoi(ex);
            }
        }

        private void LamMoiNhap()
        {
            dangSua = false;

            txtMaLichHen.ReadOnly = false;
            txtMaLichHen.BackColor = Color.White;
            txtMaLichHen.Clear();

            if (cboHoSo.Items.Count > 0)
                cboHoSo.SelectedIndex = 0;

            if (cboKhoa.Items.Count > 0)
                cboKhoa.SelectedIndex = 0;

            cboLoaiHen.SelectedIndex = 0;
            cboTrangThai.SelectedIndex = 0;

            dtNgayHen.Value =
                DateTime.Now.AddDays(1);

            txtNoiDung.Clear();

            dgvLichHen.ClearSelection();
            txtMaLichHen.Focus();
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
                "Lỗi quản lý lịch hẹn",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}
