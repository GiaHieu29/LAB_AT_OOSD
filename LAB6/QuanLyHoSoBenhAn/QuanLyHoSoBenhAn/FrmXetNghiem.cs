
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Services;

namespace QuanLyHoSoBenhAn
{
    public partial class FrmXetNghiem : Form
    {
        private readonly XetNghiemService svc =
            new XetNghiemService();

        private bool dangNap = false;
        private bool dangSua = false;

        public FrmXetNghiem()
        {
            InitializeComponent();
        }

        private void FrmXetNghiem_Load(
            object sender, EventArgs e)
        {
            try
            {
                cboTrangThai.Items.AddRange(new object[]
                {
                    "Đã chỉ định",
                    "Đang thực hiện",
                    "Đã có kết quả"
                });

                cboTrangThai.SelectedIndex = 0;

                dangNap = true;
                cboPhieuKham.DisplayMember = "HienThi";
                cboPhieuKham.ValueMember = "MaPhieuKham";
                cboPhieuKham.DataSource = svc.LayPhieuKham();
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

        private string MaPhieuDaChon()
        {
            if (cboPhieuKham.SelectedValue == null ||
                cboPhieuKham.SelectedValue is DataRowView)
                return "";

            return Convert.ToString(
                cboPhieuKham.SelectedValue);
        }

        private void NapDanhSach(string key = "")
        {
            dangNap = true;

            try
            {
                DataTable dt = svc.LayDanhSach(key);
                dgvXetNghiem.DataSource = dt;

                lblSoLuong.Text =
                    "Số xét nghiệm: " + dt.Rows.Count;

                string[] tenCot =
                {
                    "Mã xét nghiệm",
                    "Mã phiếu khám",
                    "Bệnh nhân",
                    "Tên xét nghiệm",
                    "Ngày chỉ định",
                    "Ngày có kết quả",
                    "Kết quả",
                    "Ghi chú",
                    "Trạng thái"
                };

                for (int i = 0;
                    i < dgvXetNghiem.Columns.Count &&
                    i < tenCot.Length; i++)
                {
                    dgvXetNghiem.Columns[i].HeaderText =
                        tenCot[i];
                }

                foreach (string cot in new[]
                    { "NgayChiDinh", "NgayCoKetQua" })
                {
                    if (dgvXetNghiem.Columns.Contains(cot))
                        dgvXetNghiem.Columns[cot]
                            .DefaultCellStyle.Format =
                                "dd/MM/yyyy";
                }

                dgvXetNghiem.ClearSelection();
            }
            finally
            {
                dangNap = false;
            }
        }

        private string LayChuoi(
            DataGridViewRow row, string tenCot)
        {
            object value = row.Cells[tenCot].Value;
            return value == null || value == DBNull.Value
                ? "" : Convert.ToString(value);
        }

        private void dgvXetNghiem_SelectionChanged(
            object sender, EventArgs e)
        {
            if (dangNap ||
                dgvXetNghiem.CurrentRow == null ||
                !dgvXetNghiem.CurrentRow.Selected)
                return;

            try
            {
                DataGridViewRow r =
                    dgvXetNghiem.CurrentRow;

                txtMaXetNghiem.Text =
                    LayChuoi(r, "MaXetNghiem");

                cboPhieuKham.SelectedValue =
                    LayChuoi(r, "MaPhieuKham");

                txtTenXetNghiem.Text =
                    LayChuoi(r, "TenXetNghiem");

                txtKetQua.Text =
                    LayChuoi(r, "KetQua");

                txtGhiChu.Text =
                    LayChuoi(r, "GhiChu");

                cboTrangThai.SelectedItem =
                    LayChuoi(r, "TrangThai");

                dtNgayChiDinh.Value =
                    Convert.ToDateTime(
                        r.Cells["NgayChiDinh"].Value);

                object ngayKQ =
                    r.Cells["NgayCoKetQua"].Value;

                dtNgayKetQua.Checked =
                    ngayKQ != null &&
                    ngayKQ != DBNull.Value;

                if (dtNgayKetQua.Checked)
                    dtNgayKetQua.Value =
                        Convert.ToDateTime(ngayKQ);

                dangSua = true;
                txtMaXetNghiem.ReadOnly = true;
                txtMaXetNghiem.BackColor =
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

            txtMaXetNghiem.ReadOnly = false;
            txtMaXetNghiem.BackColor = Color.White;
            txtMaXetNghiem.Clear();
            txtTenXetNghiem.Clear();
            txtKetQua.Clear();
            txtGhiChu.Clear();

            if (cboPhieuKham.Items.Count > 0)
                cboPhieuKham.SelectedIndex = 0;

            cboTrangThai.SelectedIndex = 0;
            dtNgayChiDinh.Value = DateTime.Today;
            dtNgayKetQua.Value = DateTime.Today;
            dtNgayKetQua.Checked = false;

            if (dgvXetNghiem.Rows.Count > 0)
                dgvXetNghiem.ClearSelection();
        }

        private void Luu(bool sua)
        {
            DateTime? ngayKQ =
                dtNgayKetQua.Checked
                    ? (DateTime?)dtNgayKetQua.Value.Date
                    : null;

            if (sua)
            {
                svc.Sua(
                    txtMaXetNghiem.Text,
                    MaPhieuDaChon(),
                    txtTenXetNghiem.Text,
                    dtNgayChiDinh.Value.Date,
                    ngayKQ,
                    txtKetQua.Text,
                    txtGhiChu.Text,
                    cboTrangThai.Text);
            }
            else
            {
                svc.Them(
                    txtMaXetNghiem.Text,
                    MaPhieuDaChon(),
                    txtTenXetNghiem.Text,
                    dtNgayChiDinh.Value.Date,
                    ngayKQ,
                    txtKetQua.Text,
                    txtGhiChu.Text,
                    cboTrangThai.Text);
            }

            MessageBox.Show(
                sua ? "Cập nhật xét nghiệm thành công!"
                    : "Thêm xét nghiệm thành công!",
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
                    "Nhấn Làm mới trước khi thêm xét nghiệm.");
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
                    "Hãy chọn xét nghiệm cần sửa.");
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
                    "Hãy chọn xét nghiệm cần xóa.");
                return;
            }

            if (MessageBox.Show(
                "Xóa xét nghiệm " +
                    txtMaXetNghiem.Text + "?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)
                != DialogResult.Yes)
                return;

            try
            {
                svc.Xoa(txtMaXetNghiem.Text);
                NapDanhSach(txtTimKiem.Text);
                LamMoiNhap();
                MessageBox.Show(
                    "Đã xóa xét nghiệm thành công.");
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

        private void cboTrangThai_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            bool daCoKetQua =
                cboTrangThai.Text == "Đã có kết quả";

            txtKetQua.ReadOnly = !daCoKetQua;
            dtNgayKetQua.Enabled = daCoKetQua;

            if (!daCoKetQua)
            {
                txtKetQua.Clear();
                dtNgayKetQua.Checked = false;
            }
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
                "Lỗi quản lý xét nghiệm",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}
