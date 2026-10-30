
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChoThueVanPhong.Services;

namespace QuanLyChoThueVanPhong
{
    public partial class FrmDanhMuc : Form
    {
        private readonly DanhMucService svc =
            new DanhMucService();

        private bool dangNap = false;
        private bool suaTang = false;
        private bool suaPhong = false;
        private bool suaDichVu = false;

        public FrmDanhMuc()
        {
            InitializeComponent();
        }

        private void FrmDanhMuc_Load(object sender, EventArgs e)
        {
            try
            {
                cboHuong.Items.AddRange(new object[]
                {
                    "Đông", "Tây", "Nam", "Bắc",
                    "Đông Nam", "Đông Bắc",
                    "Tây Nam", "Tây Bắc"
                });

                cboTrangThaiPhong.Items.AddRange(new object[]
                {
                    "Trống", "Đang thuê",
                    "Bảo trì", "Ngừng sử dụng"
                });

                NapTang();
                NapComboTang();
                NapPhong();
                NapDichVu();

                LamMoiTang();
                LamMoiPhong();
                LamMoiDichVu();
            }
            catch (Exception ex)
            {
                HienLoi(ex);
            }
        }

        // ===================================
        // HÀM DÙNG CHUNG
        // ===================================

        private string GiaTri(
            DataGridViewRow r, string cot)
        {
            object x = r.Cells[cot].Value;
            return x == null || x == DBNull.Value
                ? "" : Convert.ToString(x);
        }

        private void DinhDang(
            DataGridView dgv,
            string[] cot,
            string[] tieuDe)
        {
            for (int i = 0; i < cot.Length; i++)
            {
                if (dgv.Columns.Contains(cot[i]))
                    dgv.Columns[cot[i]].HeaderText =
                        tieuDe[i];
            }

            dgv.ClearSelection();
        }

        private string MaTangDaChon()
        {
            if (cboTangPhong.SelectedValue == null ||
                cboTangPhong.SelectedValue is DataRowView)
                return "";

            return Convert.ToString(
                cboTangPhong.SelectedValue);
        }

        private void BaoLoi(Exception ex)
        {
            SqlException sql = ex as SqlException;

            if (sql != null)
            {
                if (sql.Number == 547)
                {
                    MessageBox.Show(
                        "Không thể thực hiện vì dữ liệu " +
                        "đang được bảng khác sử dụng hoặc " +
                        "vi phạm ràng buộc SQL Server.",
                        "Ràng buộc dữ liệu",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                if (sql.Number == 2627 ||
                    sql.Number == 2601)
                {
                    MessageBox.Show(
                        "Mã hoặc giá trị duy nhất đã tồn tại. " +
                        "Hãy nhập giá trị khác.",
                        "Trùng dữ liệu",
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
                "Lỗi quản lý danh mục",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        // ===================================
        // TAB 1 - TẦNG
        // ===================================

        private void NapTang(string key = "")
        {
            dangNap = true;
            try
            {
                DataTable dt = svc.LayTang(key);
                dgvTang.DataSource = dt;

                DinhDang(dgvTang,
                    new[] { "MaTang", "TenTang",
                            "SoThuTu", "MoTa" },
                    new[] { "Mã tầng", "Tên tầng",
                            "Số thứ tự", "Mô tả" });

                lblTongTang.Text =
                    "Số tầng: " + dt.Rows.Count;
            }
            finally
            {
                dangNap = false;
            }
        }

        private void NapComboTang()
        {
            DataTable dt = svc.LayTang();

            cboTangPhong.DisplayMember = "TenTang";
            cboTangPhong.ValueMember = "MaTang";
            cboTangPhong.DataSource = dt;
        }

        private void LamMoiTang()
        {
            suaTang = false;
            txtMaTang.ReadOnly = false;
            txtMaTang.BackColor = Color.White;
            txtMaTang.Clear();
            txtTenTang.Clear();
            nudSoTang.Value = 0;
            txtMoTaTang.Clear();
            dgvTang.ClearSelection();
        }

        private void dgvTang_SelectionChanged(
            object sender, EventArgs e)
        {
            if (dangNap ||
                dgvTang.CurrentRow == null ||
                !dgvTang.CurrentRow.Selected)
                return;

            DataGridViewRow r = dgvTang.CurrentRow;

            txtMaTang.Text = GiaTri(r, "MaTang");
            txtTenTang.Text = GiaTri(r, "TenTang");

            decimal so = Convert.ToDecimal(
                r.Cells["SoThuTu"].Value);

            nudSoTang.Value = Math.Max(
                nudSoTang.Minimum,
                Math.Min(nudSoTang.Maximum, so));

            txtMoTaTang.Text = GiaTri(r, "MoTa");

            suaTang = true;
            txtMaTang.ReadOnly = true;
            txtMaTang.BackColor = Color.Gainsboro;
        }

        private void btnThemTang_Click(
            object sender, EventArgs e)
        {
            if (suaTang)
            {
                MessageBox.Show(
                    "Nhấn Làm mới để thêm tầng mới.");
                return;
            }

            try
            {
                svc.ThemTang(
                    txtMaTang.Text,
                    txtTenTang.Text,
                    Convert.ToInt32(nudSoTang.Value),
                    txtMoTaTang.Text);

                NapTang();
                NapComboTang();
                LamMoiTang();

                MessageBox.Show("Thêm tầng thành công!");
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void btnSuaTang_Click(
            object sender, EventArgs e)
        {
            if (!suaTang)
            {
                MessageBox.Show("Hãy chọn tầng cần sửa.");
                return;
            }

            try
            {
                svc.SuaTang(
                    txtMaTang.Text,
                    txtTenTang.Text,
                    Convert.ToInt32(nudSoTang.Value),
                    txtMoTaTang.Text);

                NapTang(txtTimTang.Text);
                NapComboTang();
                LamMoiTang();

                MessageBox.Show("Sửa tầng thành công!");
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void btnXoaTang_Click(
            object sender, EventArgs e)
        {
            if (!suaTang)
            {
                MessageBox.Show("Hãy chọn tầng cần xóa.");
                return;
            }

            if (MessageBox.Show(
                "Xóa tầng " + txtMaTang.Text + "?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                svc.XoaTang(txtMaTang.Text);
                NapTang();
                NapComboTang();
                LamMoiTang();

                MessageBox.Show("Đã xóa tầng.");
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void btnTimTang_Click(
            object sender, EventArgs e)
        {
            try
            {
                NapTang(txtTimTang.Text);
                LamMoiTang();
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void btnLamMoiTang_Click(
            object sender, EventArgs e)
        {
            try
            {
                txtTimTang.Clear();
                NapTang();
                NapComboTang();
                LamMoiTang();
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        // ===================================
        // TAB 2 - PHÒNG
        // ===================================

        private void NapPhong(string key = "")
        {
            dangNap = true;
            try
            {
                DataTable dt = svc.LayPhong(key);
                dgvPhong.DataSource = dt;

                DinhDang(dgvPhong,
                    new[]
                    {
                        "MaPhong", "MaTang", "TenTang",
                        "TenPhong", "DienTich",
                        "HuongPhong", "DonGiaThang",
                        "TrangThai", "MoTa"
                    },
                    new[]
                    {
                        "Mã phòng", "Mã tầng", "Tầng",
                        "Tên phòng", "Diện tích (m²)",
                        "Hướng", "Giá thuê/tháng",
                        "Trạng thái", "Mô tả"
                    });

                dgvPhong.Columns["MaTang"].Visible = false;

                dgvPhong.Columns["DonGiaThang"]
                    .DefaultCellStyle.Format = "N0";

                dgvPhong.Columns["DienTich"]
                    .DefaultCellStyle.Format = "N2";

                lblTongPhong.Text =
                    "Số phòng: " + dt.Rows.Count;
            }
            finally
            {
                dangNap = false;
            }
        }

        private void LamMoiPhong()
        {
            suaPhong = false;
            txtMaPhong.ReadOnly = false;
            txtMaPhong.BackColor = Color.White;

            txtMaPhong.Clear();
            txtTenPhong.Clear();
            txtMoTaPhong.Clear();

            nudDienTich.Value = 50;
            nudGiaThue.Value = 0;

            if (cboTangPhong.Items.Count > 0)
                cboTangPhong.SelectedIndex = 0;

            if (cboHuong.Items.Count > 0)
                cboHuong.SelectedIndex = 0;

            if (cboTrangThaiPhong.Items.Count > 0)
                cboTrangThaiPhong.SelectedIndex = 0;

            dgvPhong.ClearSelection();
        }

        private void dgvPhong_SelectionChanged(
            object sender, EventArgs e)
        {
            if (dangNap ||
                dgvPhong.CurrentRow == null ||
                !dgvPhong.CurrentRow.Selected)
                return;

            DataGridViewRow r = dgvPhong.CurrentRow;

            txtMaPhong.Text = GiaTri(r, "MaPhong");
            txtTenPhong.Text = GiaTri(r, "TenPhong");

            cboTangPhong.SelectedValue =
                GiaTri(r, "MaTang");

            cboHuong.SelectedItem =
                GiaTri(r, "HuongPhong");

            cboTrangThaiPhong.SelectedItem =
                GiaTri(r, "TrangThai");

            decimal dt = Convert.ToDecimal(
                r.Cells["DienTich"].Value);

            decimal gia = Convert.ToDecimal(
                r.Cells["DonGiaThang"].Value);

            nudDienTich.Value = Math.Max(
                nudDienTich.Minimum,
                Math.Min(nudDienTich.Maximum, dt));

            nudGiaThue.Value = Math.Max(
                nudGiaThue.Minimum,
                Math.Min(nudGiaThue.Maximum, gia));

            txtMoTaPhong.Text = GiaTri(r, "MoTa");

            suaPhong = true;
            txtMaPhong.ReadOnly = true;
            txtMaPhong.BackColor = Color.Gainsboro;
        }

        private void LuuPhong(bool sua)
        {
            if (sua)
            {
                svc.SuaPhong(
                    txtMaPhong.Text, MaTangDaChon(),
                    txtTenPhong.Text, nudDienTich.Value,
                    cboHuong.Text, nudGiaThue.Value,
                    cboTrangThaiPhong.Text,
                    txtMoTaPhong.Text);
            }
            else
            {
                svc.ThemPhong(
                    txtMaPhong.Text, MaTangDaChon(),
                    txtTenPhong.Text, nudDienTich.Value,
                    cboHuong.Text, nudGiaThue.Value,
                    cboTrangThaiPhong.Text,
                    txtMoTaPhong.Text);
            }

            NapPhong(txtTimPhong.Text);
            LamMoiPhong();

            MessageBox.Show(
                sua ? "Sửa phòng thành công!"
                    : "Thêm phòng thành công!");
        }

        private void btnThemPhong_Click(
            object sender, EventArgs e)
        {
            if (suaPhong)
            {
                MessageBox.Show(
                    "Nhấn Làm mới để thêm phòng.");
                return;
            }

            try { LuuPhong(false); }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void btnSuaPhong_Click(
            object sender, EventArgs e)
        {
            if (!suaPhong)
            {
                MessageBox.Show("Hãy chọn phòng cần sửa.");
                return;
            }

            try { LuuPhong(true); }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void btnXoaPhong_Click(
            object sender, EventArgs e)
        {
            if (!suaPhong)
            {
                MessageBox.Show("Hãy chọn phòng cần xóa.");
                return;
            }

            if (MessageBox.Show(
                "Xóa phòng " + txtMaPhong.Text + "?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                svc.XoaPhong(txtMaPhong.Text);
                NapPhong();
                LamMoiPhong();
                MessageBox.Show("Đã xóa phòng.");
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void btnTimPhong_Click(
            object sender, EventArgs e)
        {
            try
            {
                NapPhong(txtTimPhong.Text);
                LamMoiPhong();
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void btnLamMoiPhong_Click(
            object sender, EventArgs e)
        {
            try
            {
                txtTimPhong.Clear();
                NapComboTang();
                NapPhong();
                LamMoiPhong();
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        // ===================================
        // TAB 3 - DỊCH VỤ
        // ===================================

        private void NapDichVu(string key = "")
        {
            dangNap = true;
            try
            {
                DataTable dt = svc.LayDichVu(key);
                dgvDichVu.DataSource = dt;

                DinhDang(dgvDichVu,
                    new[]
                    {
                        "MaDichVu", "TenDichVu",
                        "DonViTinh", "DonGia",
                        "DangSuDung", "MoTa"
                    },
                    new[]
                    {
                        "Mã dịch vụ", "Tên dịch vụ",
                        "Đơn vị tính", "Đơn giá",
                        "Đang sử dụng", "Mô tả"
                    });

                dgvDichVu.Columns["DonGia"]
                    .DefaultCellStyle.Format = "N0";

                lblTongDichVu.Text =
                    "Số dịch vụ: " + dt.Rows.Count;
            }
            finally
            {
                dangNap = false;
            }
        }

        private void LamMoiDichVu()
        {
            suaDichVu = false;
            txtMaDichVu.ReadOnly = false;
            txtMaDichVu.BackColor = Color.White;

            txtMaDichVu.Clear();
            txtTenDichVu.Clear();
            txtDonViTinh.Clear();
            txtMoTaDichVu.Clear();

            nudDonGiaDV.Value = 0;
            chkDangSuDung.Checked = true;

            dgvDichVu.ClearSelection();
        }

        private void dgvDichVu_SelectionChanged(
            object sender, EventArgs e)
        {
            if (dangNap ||
                dgvDichVu.CurrentRow == null ||
                !dgvDichVu.CurrentRow.Selected)
                return;

            DataGridViewRow r = dgvDichVu.CurrentRow;

            txtMaDichVu.Text = GiaTri(r, "MaDichVu");
            txtTenDichVu.Text = GiaTri(r, "TenDichVu");
            txtDonViTinh.Text = GiaTri(r, "DonViTinh");
            txtMoTaDichVu.Text = GiaTri(r, "MoTa");

            decimal gia = Convert.ToDecimal(
                r.Cells["DonGia"].Value);

            nudDonGiaDV.Value = Math.Max(
                nudDonGiaDV.Minimum,
                Math.Min(nudDonGiaDV.Maximum, gia));

            chkDangSuDung.Checked = Convert.ToBoolean(
                r.Cells["DangSuDung"].Value);

            suaDichVu = true;
            txtMaDichVu.ReadOnly = true;
            txtMaDichVu.BackColor = Color.Gainsboro;
        }

        private void LuuDichVu(bool sua)
        {
            if (sua)
            {
                svc.SuaDichVu(
                    txtMaDichVu.Text,
                    txtTenDichVu.Text,
                    txtDonViTinh.Text,
                    nudDonGiaDV.Value,
                    chkDangSuDung.Checked,
                    txtMoTaDichVu.Text);
            }
            else
            {
                svc.ThemDichVu(
                    txtMaDichVu.Text,
                    txtTenDichVu.Text,
                    txtDonViTinh.Text,
                    nudDonGiaDV.Value,
                    chkDangSuDung.Checked,
                    txtMoTaDichVu.Text);
            }

            NapDichVu(txtTimDichVu.Text);
            LamMoiDichVu();

            MessageBox.Show(
                sua ? "Sửa dịch vụ thành công!"
                    : "Thêm dịch vụ thành công!");
        }

        private void btnThemDichVu_Click(
            object sender, EventArgs e)
        {
            if (suaDichVu)
            {
                MessageBox.Show(
                    "Nhấn Làm mới để thêm dịch vụ.");
                return;
            }

            try { LuuDichVu(false); }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void btnSuaDichVu_Click(
            object sender, EventArgs e)
        {
            if (!suaDichVu)
            {
                MessageBox.Show("Hãy chọn dịch vụ cần sửa.");
                return;
            }

            try { LuuDichVu(true); }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void btnXoaDichVu_Click(
            object sender, EventArgs e)
        {
            if (!suaDichVu)
            {
                MessageBox.Show("Hãy chọn dịch vụ cần xóa.");
                return;
            }

            if (MessageBox.Show(
                "Xóa dịch vụ " + txtMaDichVu.Text + "?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                svc.XoaDichVu(txtMaDichVu.Text);
                NapDichVu();
                LamMoiDichVu();
                MessageBox.Show("Đã xóa dịch vụ.");
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void btnTimDichVu_Click(
            object sender, EventArgs e)
        {
            try
            {
                NapDichVu(txtTimDichVu.Text);
                LamMoiDichVu();
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void btnLamMoiDichVu_Click(
            object sender, EventArgs e)
        {
            try
            {
                txtTimDichVu.Clear();
                NapDichVu();
                LamMoiDichVu();
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
