
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using QuanLyChoThueVanPhong.Services;

namespace QuanLyChoThueVanPhong
{
    public partial class FrmViPhamSuaChua : Form
    {
        private readonly ViPhamSuaChuaService svc =
            new ViPhamSuaChuaService();

        private bool loadingVP = false;
        private bool loadingSC = false;
        private bool suaVP = false;
        private bool suaSC = false;

        public FrmViPhamSuaChua()
        {
            InitializeComponent();
        }

        private string Selected(ComboBox cb)
        {
            if (cb.SelectedValue == null ||
                cb.SelectedValue is DataRowView)
                return "";

            return Convert.ToString(cb.SelectedValue);
        }

        private string Cell(DataGridViewRow row, string col)
        {
            object value = row.Cells[col].Value;

            return value == null || value == DBNull.Value
                ? "" : Convert.ToString(value);
        }

        private void DoiCot(
            DataGridView g, string cot, string ten,
            string format = null)
        {
            if (!g.Columns.Contains(cot)) return;
            g.Columns[cot].HeaderText = ten;

            if (format != null)
                g.Columns[cot].DefaultCellStyle.Format =
                    format;
        }

        private void BaoLoi(Exception ex)
        {
            SqlException sql = ex as SqlException;
            string msg = ex.Message;

            if (sql != null)
            {
                if (sql.Number == 2627 ||
                    sql.Number == 2601)
                    msg = "Mã bản ghi đã tồn tại.";
                else if (sql.Number == 547)
                    msg = "Dữ liệu đang bị ràng buộc " +
                          "khóa ngoại hoặc liên kết không hợp lệ.";
                else if (sql.Number == 1205)
                    msg = "Có giao dịch khác đang xử lý. " +
                          "Hãy thử lại.";
            }

            MessageBox.Show(
                msg, "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        // ======================================
        // LOAD FORM
        // ======================================

        private void FrmViPhamSuaChua_Load(
            object sender, EventArgs e)
        {
            try
            {
                cboTrangThaiVP.Items.AddRange(new object[]
                {
                    "Chưa xử lý", "Đã xử lý"
                });

                cboTrangThaiSC.Items.AddRange(new object[]
                {
                    "Chờ xử lý", "Đang sửa", "Hoàn thành"
                });

                NapHopDong();
                NapPhongSC();
                NapViPham();
                NapSuaChua();

                LamMoiVP();
                LamMoiSC();
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        // ======================================
        // TAB 1 - VI PHẠM
        // ======================================

        private void NapHopDong()
        {
            loadingVP = true;
            try
            {
                cboHopDong.DataSource = null;
                cboHopDong.DisplayMember = "HienThi";
                cboHopDong.ValueMember = "MaHopDong";
                cboHopDong.DataSource = svc.LayHopDong();
            }
            finally { loadingVP = false; }

            NapPhongThue();
        }

        private void NapPhongThue()
        {
            if (loadingVP) return;

            loadingVP = true;
            try
            {
                cboPhongThue.DataSource = null;
                cboPhongThue.DisplayMember = "HienThi";
                cboPhongThue.ValueMember = "MaChiTietHD";
                cboPhongThue.DataSource =
                    svc.LayPhongThue(Selected(cboHopDong));
            }
            finally { loadingVP = false; }
        }

        private void cboHopDong_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            if (loadingVP) return;

            try { NapPhongThue(); }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void NapViPham(string key = "")
        {
            loadingVP = true;
            try
            {
                DataTable dt = svc.LayViPham(key);
                dgvViPham.DataSource = dt;

                DoiCot(dgvViPham, "MaViPham", "Mã vi phạm");
                DoiCot(dgvViPham, "MaHopDong", "Hợp đồng");
                DoiCot(dgvViPham, "MaPhong", "Phòng");
                DoiCot(dgvViPham, "TenKhachHang", "Khách hàng");
                DoiCot(dgvViPham, "NgayPhatHien",
                    "Ngày phát hiện", "dd/MM/yyyy");
                DoiCot(dgvViPham, "NoiDung", "Nội dung");
                DoiCot(dgvViPham, "SoTienPhat",
                    "Tiền phạt", "N0");
                DoiCot(dgvViPham, "TrangThai", "Trạng thái");
                DoiCot(dgvViPham, "NgayXuLy",
                    "Ngày xử lý", "dd/MM/yyyy");
                DoiCot(dgvViPham, "GhiChu", "Ghi chú");

                if (dgvViPham.Columns.Contains("MaChiTietHD"))
                    dgvViPham.Columns["MaChiTietHD"]
                        .Visible = false;

                lblSoVP.Text = "Số vi phạm: " + dt.Rows.Count;
                dgvViPham.ClearSelection();
            }
            finally { loadingVP = false; }
        }

        private void dgvViPham_SelectionChanged(
            object sender, EventArgs e)
        {
            if (loadingVP || dgvViPham.CurrentRow == null ||
                !dgvViPham.CurrentRow.Selected) return;

            try
            {
                loadingVP = true;

                DataGridViewRow r = dgvViPham.CurrentRow;

                txtMaVP.Text = Cell(r, "MaViPham");

                cboHopDong.SelectedValue =
                    Cell(r, "MaHopDong");

                loadingVP = false;
                NapPhongThue();
                loadingVP = true;

                cboPhongThue.SelectedValue =
                    Cell(r, "MaChiTietHD");

                dtPhatHien.Value = Convert.ToDateTime(
                    r.Cells["NgayPhatHien"].Value);

                txtNoiDungVP.Text = Cell(r, "NoiDung");

                nudTienPhat.Value = Math.Min(
                    nudTienPhat.Maximum,
                    Convert.ToDecimal(r.Cells["SoTienPhat"].Value));

                cboTrangThaiVP.SelectedItem =
                    Cell(r, "TrangThai");

                dtXuLy.Checked =
                    r.Cells["NgayXuLy"].Value != DBNull.Value;

                if (dtXuLy.Checked)
                    dtXuLy.Value = Convert.ToDateTime(
                        r.Cells["NgayXuLy"].Value);

                txtGhiChuVP.Text = Cell(r, "GhiChu");

                suaVP = true;
                txtMaVP.ReadOnly = true;
                txtMaVP.BackColor = Color.Gainsboro;
            }
            catch (Exception ex) { BaoLoi(ex); }
            finally { loadingVP = false; }
        }

        private void LamMoiVP()
        {
            suaVP = false;
            txtMaVP.Clear();
            txtNoiDungVP.Clear();
            txtGhiChuVP.Clear();

            txtMaVP.ReadOnly = false;
            txtMaVP.BackColor = Color.White;

            dtPhatHien.Value = DateTime.Today;
            dtXuLy.Value = DateTime.Today;
            dtXuLy.Checked = false;
            nudTienPhat.Value = 0;

            if (cboTrangThaiVP.Items.Count > 0)
                cboTrangThaiVP.SelectedIndex = 0;

            if (cboHopDong.Items.Count > 0)
                cboHopDong.SelectedIndex = 0;

            NapPhongThue();
            dgvViPham.ClearSelection();
        }

        private void LuuVP(bool sua)
        {
            svc.LuuViPham(
                txtMaVP.Text,
                Selected(cboPhongThue),
                dtPhatHien.Value.Date,
                txtNoiDungVP.Text,
                nudTienPhat.Value,
                cboTrangThaiVP.Text,
                dtXuLy.Checked
                    ? (DateTime?)dtXuLy.Value.Date : null,
                txtGhiChuVP.Text,
                sua);

            NapViPham(txtTimVP.Text);
            LamMoiVP();

            MessageBox.Show(
                sua ? "Sửa vi phạm thành công!"
                    : "Thêm vi phạm thành công!");
        }

        private void btnThemVP_Click(object s, EventArgs e)
        {
            if (suaVP)
            {
                MessageBox.Show("Nhấn Làm mới trước khi thêm.");
                return;
            }

            try { LuuVP(false); }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void btnSuaVP_Click(object s, EventArgs e)
        {
            if (!suaVP)
            {
                MessageBox.Show("Chọn vi phạm cần sửa.");
                return;
            }

            try { LuuVP(true); }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void btnXoaVP_Click(object s, EventArgs e)
        {
            if (!suaVP)
            {
                MessageBox.Show("Chọn vi phạm cần xóa.");
                return;
            }

            if (MessageBox.Show(
                "Xóa vi phạm " + txtMaVP.Text + "?",
                "Xác nhận", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                svc.XoaViPham(txtMaVP.Text);
                NapViPham(txtTimVP.Text);
                LamMoiVP();
                MessageBox.Show("Đã xóa vi phạm.");
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void btnTimVP_Click(object s, EventArgs e)
        {
            try
            {
                NapViPham(txtTimVP.Text);
                LamMoiVP();
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void btnMoiVP_Click(object s, EventArgs e)
        {
            try
            {
                txtTimVP.Clear();
                NapHopDong();
                NapViPham();
                LamMoiVP();
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        // ======================================
        // TAB 2 - SỬA CHỮA
        // ======================================

        private void NapPhongSC()
        {
            loadingSC = true;
            try
            {
                cboPhongSC.DataSource = null;
                cboPhongSC.DisplayMember = "HienThi";
                cboPhongSC.ValueMember = "MaPhong";
                cboPhongSC.DataSource = svc.LayPhong();
            }
            finally { loadingSC = false; }

            NapKiemTra();
        }

        private void NapKiemTra()
        {
            if (loadingSC) return;

            loadingSC = true;
            try
            {
                cboKiemTra.DataSource = null;
                cboKiemTra.DisplayMember = "HienThi";
                cboKiemTra.ValueMember = "MaKiemTra";
                cboKiemTra.DataSource =
                    svc.LayPhieuKiemTra(Selected(cboPhongSC));
            }
            finally { loadingSC = false; }
        }

        private void cboPhongSC_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            if (loadingSC) return;

            try { NapKiemTra(); }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void NapSuaChua(string key = "")
        {
            loadingSC = true;
            try
            {
                DataTable dt = svc.LaySuaChua(key);
                dgvSuaChua.DataSource = dt;

                DoiCot(dgvSuaChua, "MaSuaChua", "Mã sửa chữa");
                DoiCot(dgvSuaChua, "MaPhong", "Mã phòng");
                DoiCot(dgvSuaChua, "TenPhong", "Tên phòng");
                DoiCot(dgvSuaChua, "TenTang", "Tầng");
                DoiCot(dgvSuaChua, "MaKiemTra", "Phiếu kiểm tra");
                DoiCot(dgvSuaChua, "NgayBatDau",
                    "Ngày bắt đầu", "dd/MM/yyyy");
                DoiCot(dgvSuaChua, "NgayHoanThanh",
                    "Ngày hoàn thành", "dd/MM/yyyy");
                DoiCot(dgvSuaChua, "NoiDung", "Nội dung");
                DoiCot(dgvSuaChua, "ChiPhi", "Chi phí", "N0");
                DoiCot(dgvSuaChua, "TrangThai", "Trạng thái");

                lblSoSC.Text = "Số sửa chữa: " + dt.Rows.Count;
                dgvSuaChua.ClearSelection();
            }
            finally { loadingSC = false; }
        }

        private void dgvSuaChua_SelectionChanged(
            object sender, EventArgs e)
        {
            if (loadingSC || dgvSuaChua.CurrentRow == null ||
                !dgvSuaChua.CurrentRow.Selected) return;

            try
            {
                loadingSC = true;
                DataGridViewRow r = dgvSuaChua.CurrentRow;

                txtMaSC.Text = Cell(r, "MaSuaChua");
                cboPhongSC.SelectedValue = Cell(r, "MaPhong");

                loadingSC = false;
                NapKiemTra();
                loadingSC = true;

                cboKiemTra.SelectedValue =
                    Cell(r, "MaKiemTra");

                dtBatDauSC.Value = Convert.ToDateTime(
                    r.Cells["NgayBatDau"].Value);

                dtHoanThanh.Checked =
                    r.Cells["NgayHoanThanh"].Value != DBNull.Value;

                if (dtHoanThanh.Checked)
                    dtHoanThanh.Value = Convert.ToDateTime(
                        r.Cells["NgayHoanThanh"].Value);

                txtNoiDungSC.Text = Cell(r, "NoiDung");

                nudChiPhi.Value = Math.Min(
                    nudChiPhi.Maximum,
                    Convert.ToDecimal(r.Cells["ChiPhi"].Value));

                cboTrangThaiSC.SelectedItem =
                    Cell(r, "TrangThai");

                suaSC = true;
                txtMaSC.ReadOnly = true;
                txtMaSC.BackColor = Color.Gainsboro;
            }
            catch (Exception ex) { BaoLoi(ex); }
            finally { loadingSC = false; }
        }

        private void LamMoiSC()
        {
            suaSC = false;
            txtMaSC.ReadOnly = false;
            txtMaSC.BackColor = Color.White;

            txtMaSC.Clear();
            txtNoiDungSC.Clear();
            nudChiPhi.Value = 0;

            dtBatDauSC.Value = DateTime.Today;
            dtHoanThanh.Value = DateTime.Today;
            dtHoanThanh.Checked = false;

            if (cboTrangThaiSC.Items.Count > 0)
                cboTrangThaiSC.SelectedIndex = 0;

            if (cboPhongSC.Items.Count > 0)
                cboPhongSC.SelectedIndex = 0;

            NapKiemTra();
            dgvSuaChua.ClearSelection();
        }

        private void LuuSC(bool sua)
        {
            svc.LuuSuaChua(
                txtMaSC.Text,
                Selected(cboPhongSC),
                Selected(cboKiemTra),
                dtBatDauSC.Value.Date,
                dtHoanThanh.Checked
                    ? (DateTime?)dtHoanThanh.Value.Date : null,
                txtNoiDungSC.Text,
                nudChiPhi.Value,
                cboTrangThaiSC.Text,
                sua);

            NapSuaChua(txtTimSC.Text);
            LamMoiSC();

            MessageBox.Show(
                sua ? "Sửa chữa được cập nhật!"
                    : "Đã thêm công việc sửa chữa!");
        }

        private void btnThemSC_Click(object s, EventArgs e)
        {
            if (suaSC)
            {
                MessageBox.Show("Nhấn Làm mới trước khi thêm.");
                return;
            }

            try { LuuSC(false); }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void btnSuaSC_Click(object s, EventArgs e)
        {
            if (!suaSC)
            {
                MessageBox.Show("Chọn sửa chữa cần sửa.");
                return;
            }

            try { LuuSC(true); }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void btnXoaSC_Click(object s, EventArgs e)
        {
            if (!suaSC)
            {
                MessageBox.Show("Chọn sửa chữa cần xóa.");
                return;
            }

            if (MessageBox.Show(
                "Xóa sửa chữa " + txtMaSC.Text + "?",
                "Xác nhận", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                svc.XoaSuaChua(txtMaSC.Text);
                NapSuaChua(txtTimSC.Text);
                LamMoiSC();
                MessageBox.Show("Đã xóa sửa chữa.");
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void btnTimSC_Click(object s, EventArgs e)
        {
            try
            {
                NapSuaChua(txtTimSC.Text);
                LamMoiSC();
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void btnMoiSC_Click(object s, EventArgs e)
        {
            try
            {
                txtTimSC.Clear();
                NapPhongSC();
                NapSuaChua();
                LamMoiSC();
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void btnDong_Click(object s, EventArgs e)
        {
            Close();
        }
    }
}
