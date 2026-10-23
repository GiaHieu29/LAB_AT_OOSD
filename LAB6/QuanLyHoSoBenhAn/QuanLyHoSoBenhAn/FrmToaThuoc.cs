
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Services;

namespace QuanLyHoSoBenhAn
{
    public partial class FrmToaThuoc : Form
    {
        private readonly ToaThuocService svc =
            new ToaThuocService();

        private DataTable bangThuoc;
        private bool dangNap = false;
        private bool dangSua = false;

        public FrmToaThuoc()
        {
            InitializeComponent();
            TaoBangThuocTam();
        }

        // =====================================
        // 1. LOAD FORM
        // =====================================

        private void FrmToaThuoc_Load(
            object sender, EventArgs e)
        {
            try
            {
                dangNap = true;

                cboPhieuKham.DisplayMember = "HienThi";
                cboPhieuKham.ValueMember = "MaPhieuKham";
                cboPhieuKham.DataSource = svc.LayPhieuKham();

                cboThuoc.DisplayMember = "TenThuoc";
                cboThuoc.ValueMember = "MaThuoc";
                cboThuoc.DataSource = svc.LayThuoc();

                dangNap = false;

                cboPhieuKham_SelectedIndexChanged(
                    null, EventArgs.Empty);

                NapDanhSach();
                LamMoiNhap();
            }
            catch (Exception ex)
            {
                dangNap = false;
                HienLoi(ex);
            }
        }

        // =====================================
        // 2. TẠO BẢNG CHI TIẾT TẠM
        // =====================================

        private void TaoBangThuocTam()
        {
            bangThuoc = new DataTable();

            bangThuoc.Columns.Add(
                "MaThuoc", typeof(string));

            bangThuoc.Columns.Add(
                "TenThuoc", typeof(string));

            bangThuoc.Columns.Add(
                "DonViTinh", typeof(string));

            bangThuoc.Columns.Add(
                "SoLuong", typeof(int));

            bangThuoc.Columns.Add(
                "LieuDung", typeof(string));

            bangThuoc.Columns.Add(
                "CachDung", typeof(string));

            bangThuoc.Columns.Add(
                "SoNgayDung", typeof(int));

            dgvChiTiet.DataSource = bangThuoc;
            DinhDangChiTiet();
        }

        // =====================================
        // 3. NẠP DANH SÁCH TOA
        // =====================================

        private void NapDanhSach(string key = "")
        {
            dangNap = true;

            try
            {
                DataTable dt = svc.LayDanhSach(key);
                dgvToaThuoc.DataSource = dt;

                lblSoLuong.Text =
                    "Số toa thuốc: " + dt.Rows.Count;

                string[] cot =
                {
                    "Mã toa",
                    "Mã phiếu khám",
                    "Bệnh nhân",
                    "Mã bác sĩ",
                    "Bác sĩ",
                    "Ngày lập",
                    "Lời dặn",
                    "Số loại thuốc"
                };

                for (int i = 0;
                    i < dgvToaThuoc.Columns.Count &&
                    i < cot.Length; i++)
                {
                    dgvToaThuoc.Columns[i].HeaderText =
                        cot[i];
                }

                if (dgvToaThuoc.Columns
                    .Contains("NgayLap"))
                    dgvToaThuoc.Columns["NgayLap"]
                        .DefaultCellStyle.Format =
                            "dd/MM/yyyy";

                if (dgvToaThuoc.Columns
                    .Contains("MaBacSi"))
                    dgvToaThuoc.Columns["MaBacSi"]
                        .Visible = false;

                dgvToaThuoc.ClearSelection();
            }
            finally
            {
                dangNap = false;
            }
        }

        private void DinhDangChiTiet()
        {
            string[] ten =
            {
                "Mã thuốc",
                "Tên thuốc",
                "Đơn vị",
                "Số lượng",
                "Liều dùng",
                "Cách dùng",
                "Số ngày"
            };

            for (int i = 0;
                i < dgvChiTiet.Columns.Count &&
                i < ten.Length; i++)
            {
                dgvChiTiet.Columns[i].HeaderText = ten[i];
            }

            dgvChiTiet.ReadOnly = true;
            dgvChiTiet.AllowUserToAddRows = false;
            dgvChiTiet.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
        }

        // =====================================
        // 4. CHỌN PHIẾU KHÁM
        // =====================================

        private void cboPhieuKham_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            if (dangNap)
                return;

            DataRowView row =
                cboPhieuKham.SelectedItem as DataRowView;

            if (row == null)
            {
                txtBacSi.Clear();
                return;
            }

            txtBacSi.Text =
                Convert.ToString(row["TenBacSi"]);
        }

        private string MaPhieu()
        {
            object value = cboPhieuKham.SelectedValue;

            return value == null ||
                   value is DataRowView
                ? "" : Convert.ToString(value);
        }

        // =====================================
        // 5. THÊM THUỐC VÀO DANH SÁCH
        // =====================================

        private void btnThemThuoc_Click(
            object sender, EventArgs e)
        {
            DataRowView thuoc =
                cboThuoc.SelectedItem as DataRowView;

            if (thuoc == null)
            {
                MessageBox.Show(
                    "Vui lòng chọn thuốc trong danh mục.");
                return;
            }

            string maThuoc =
                Convert.ToString(thuoc["MaThuoc"]);

            foreach (DataRow row in bangThuoc.Rows)
            {
                if (Convert.ToString(row["MaThuoc"])
                    == maThuoc)
                {
                    MessageBox.Show(
                        "Thuốc này đã có trong toa.\n" +
                        "Hãy xóa dòng cũ nếu muốn nhập lại.");
                    return;
                }
            }

            DataRow moi = bangThuoc.NewRow();

            moi["MaThuoc"] = maThuoc;
            moi["TenThuoc"] =
                Convert.ToString(thuoc["TenThuoc"]);
            moi["DonViTinh"] =
                Convert.ToString(thuoc["DonViTinh"]);
            moi["SoLuong"] =
                Convert.ToInt32(nudSoLuong.Value);
            moi["LieuDung"] =
                txtLieuDung.Text.Trim();
            moi["CachDung"] =
                txtCachDung.Text.Trim();

            if (chkSoNgay.Checked)
                moi["SoNgayDung"] =
                    Convert.ToInt32(nudSoNgay.Value);
            else
                moi["SoNgayDung"] = DBNull.Value;

            bangThuoc.Rows.Add(moi);
            dgvChiTiet.ClearSelection();
        }

        // =====================================
        // 6. BỎ THUỐC KHỎI DANH SÁCH
        // =====================================

        private void btnXoaThuoc_Click(
            object sender, EventArgs e)
        {
            DataGridViewRow row =
                dgvChiTiet.CurrentRow;

            if (row == null || !row.Selected)
            {
                MessageBox.Show(
                    "Chọn một dòng thuốc cần bỏ.");
                return;
            }

            DataRowView data =
                row.DataBoundItem as DataRowView;

            if (data != null)
                data.Row.Delete();
        }

        // =====================================
        // 7. CHỌN TOA THUỐC ĐÃ LƯU
        // =====================================

        private void dgvToaThuoc_SelectionChanged(
            object sender, EventArgs e)
        {
            if (dangNap ||
                dgvToaThuoc.CurrentRow == null ||
                !dgvToaThuoc.CurrentRow.Selected)
                return;

            try
            {
                DataGridViewRow row =
                    dgvToaThuoc.CurrentRow;

                txtMaToa.Text =
                    GiaTri(row, "MaToaThuoc");

                dangNap = true;
                cboPhieuKham.SelectedValue =
                    GiaTri(row, "MaPhieuKham");
                dangNap = false;

                cboPhieuKham_SelectedIndexChanged(
                    null, EventArgs.Empty);

                dtNgayLap.Value =
                    Convert.ToDateTime(
                        row.Cells["NgayLap"].Value);

                txtLoiDan.Text =
                    GiaTri(row, "LoiDan");

                // Dùng bảng tạm mới để giữ nguyên
                // cấu trúc cột khi chỉnh sửa.
                bangThuoc.Clear();

                DataTable tuCSDL =
                    svc.LayChiTiet(txtMaToa.Text);

                foreach (DataRow r in tuCSDL.Rows)
                {
                    DataRow moi = bangThuoc.NewRow();

                    foreach (DataColumn c in
                        bangThuoc.Columns)
                    {
                        moi[c.ColumnName] =
                            r[c.ColumnName];
                    }

                    bangThuoc.Rows.Add(moi);
                }

                dangSua = true;
                txtMaToa.ReadOnly = true;
                txtMaToa.BackColor =
                    Color.FromArgb(235, 235, 235);

                // Không đổi phiếu khám của toa đã lập
                cboPhieuKham.Enabled = false;

                dgvChiTiet.ClearSelection();
            }
            catch (Exception ex)
            {
                dangNap = false;
                HienLoi(ex);
            }
        }

        private string GiaTri(
            DataGridViewRow row, string cot)
        {
            object value = row.Cells[cot].Value;

            return value == null || value == DBNull.Value
                ? "" : Convert.ToString(value);
        }

        // =====================================
        // 8. LƯU / SỬA TOA
        // =====================================

        private void Luu(bool sua)
        {
            // Copy các dòng hiện hành để service xử lý
            // và không mang theo dòng đã xóa.
            DataTable chiTiet = bangThuoc.Clone();

            foreach (DataRow row in bangThuoc.Rows)
            {
                if (row.RowState != DataRowState.Deleted)
                    chiTiet.ImportRow(row);
            }

            if (sua)
            {
                svc.Sua(
                    txtMaToa.Text,
                    MaPhieu(),
                    dtNgayLap.Value,
                    txtLoiDan.Text,
                    chiTiet);
            }
            else
            {
                svc.Them(
                    txtMaToa.Text,
                    MaPhieu(),
                    dtNgayLap.Value,
                    txtLoiDan.Text,
                    chiTiet);
            }

            MessageBox.Show(
                sua ? "Cập nhật toa thuốc thành công!"
                    : "Lập toa thuốc thành công!",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            NapDanhSach(txtTimKiem.Text);
            LamMoiNhap();
        }

        private void btnLuu_Click(
            object sender, EventArgs e)
        {
            if (dangSua)
            {
                MessageBox.Show(
                    "Đang chỉnh sửa toa cũ. " +
                    "Hãy sử dụng nút Cập nhật.");
                return;
            }

            try { Luu(false); }
            catch (Exception ex) { HienLoi(ex); }
        }

        private void btnCapNhat_Click(
            object sender, EventArgs e)
        {
            if (!dangSua)
            {
                MessageBox.Show(
                    "Chọn toa thuốc trong bảng để sửa.");
                return;
            }

            try { Luu(true); }
            catch (Exception ex) { HienLoi(ex); }
        }

        // =====================================
        // 9. XÓA TOA
        // =====================================

        private void btnXoa_Click(
            object sender, EventArgs e)
        {
            if (!dangSua)
            {
                MessageBox.Show(
                    "Chọn toa thuốc cần xóa.");
                return;
            }

            if (MessageBox.Show(
                "Bạn có muốn xóa toa " +
                    txtMaToa.Text + " và các dòng thuốc?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)
                != DialogResult.Yes)
                return;

            try
            {
                svc.Xoa(txtMaToa.Text);
                NapDanhSach(txtTimKiem.Text);
                LamMoiNhap();

                MessageBox.Show(
                    "Đã xóa toa thuốc thành công.");
            }
            catch (Exception ex) { HienLoi(ex); }
        }

        // =====================================
        // 10. TÌM KIẾM VÀ LÀM MỚI
        // =====================================

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

        private void LamMoiNhap()
        {
            dangSua = false;

            txtMaToa.ReadOnly = false;
            txtMaToa.BackColor = Color.White;
            txtMaToa.Clear();

            cboPhieuKham.Enabled = true;

            if (cboPhieuKham.Items.Count > 0)
                cboPhieuKham.SelectedIndex = 0;

            cboPhieuKham_SelectedIndexChanged(
                null, EventArgs.Empty);

            if (cboThuoc.Items.Count > 0)
                cboThuoc.SelectedIndex = 0;

            dtNgayLap.Value = DateTime.Today;
            txtLoiDan.Clear();
            txtLieuDung.Clear();
            txtCachDung.Clear();

            nudSoLuong.Value = 1;
            nudSoNgay.Value = 1;
            chkSoNgay.Checked = false;

            bangThuoc.Clear();

            dgvToaThuoc.ClearSelection();
            dgvChiTiet.ClearSelection();

            txtMaToa.Focus();
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
                "Lỗi quản lý toa thuốc",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}
