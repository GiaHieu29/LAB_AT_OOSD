
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Services;

namespace QuanLyHoSoBenhAn
{
    public partial class FrmHoSoBenhAn : Form
    {
        private readonly HoSoBenhAnService svc =
            new HoSoBenhAnService();

        private bool dangNap = false;
        private bool dangSua = false;

        private Button btnThemBenhNhan;

        public FrmHoSoBenhAn()
        {
            InitializeComponent();
            TaoNutThemBenhNhan();
        }

        // TẠO NÚT BỔ SUNG MÀ KHÔNG CẦN SỬA DESIGNER
        private void TaoNutThemBenhNhan()
        {
            btnThemBenhNhan = new Button();

            btnThemBenhNhan.Text = "Thêm bệnh nhân";
            btnThemBenhNhan.Location =
                new Point(25, 385);
            btnThemBenhNhan.Size =
                new Size(170, 40);

            btnThemBenhNhan.Click +=
                btnThemBenhNhan_Click;

            Controls.Add(btnThemBenhNhan);
        }

        // ==================================
        // LOAD
        // ==================================
        private void FrmHoSoBenhAn_Load(
            object sender, EventArgs e)
        {
            try
            {
                dangNap = true;

                cboTrangThai.Items.Clear();
                cboTrangThai.Items.Add("Đang theo dõi");
                cboTrangThai.Items.Add("Ngừng theo dõi");

                dtNgayLap.MaxDate = DateTime.Today;

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

        // ==================================
        // DANH SÁCH HỒ SƠ
        // ==================================
        private void NapDanhSach(string key = "")
        {
            dangNap = true;

            try
            {
                DataTable dt = svc.LayDanhSach(key);
                dgvHoSo.DataSource = dt;

                lblSoLuong.Text =
                    "Tổng số hồ sơ: " + dt.Rows.Count;

                DinhDangBang(dgvHoSo);

                DoiTieuDe("MaHoSo", "Mã hồ sơ");
                DoiTieuDe("MaBenhNhan", "Mã bệnh nhân");
                DoiTieuDe("HoTen", "Họ tên");
                DoiTieuDe("MaPhieuDK", "Phiếu đăng ký");
                DoiTieuDe("NgayLap", "Ngày lập");
                DoiTieuDe("TinhTrangSucKhoe",
                    "Tình trạng sức khỏe");
                DoiTieuDe("GhiChu", "Ghi chú");
                DoiTieuDe("TrangThai", "Trạng thái");

                if (dgvHoSo.Columns.Contains("NgayLap"))
                {
                    dgvHoSo.Columns["NgayLap"]
                        .DefaultCellStyle.Format = "dd/MM/yyyy";
                }

                dgvHoSo.ClearSelection();
            }
            finally
            {
                dangNap = false;
            }
        }

        private void DoiTieuDe(string cot, string ten)
        {
            if (dgvHoSo.Columns.Contains(cot))
                dgvHoSo.Columns[cot].HeaderText = ten;
        }

        private void NapBenhNhan()
        {
            dangNap = true;

            try
            {
                DataTable dt =
                    svc.LayBenhNhanChuaCoHoSo();

                cboBenhNhan.DataSource = null;
                cboBenhNhan.DisplayMember = "HoTen";
                cboBenhNhan.ValueMember = "MaBenhNhan";
                cboBenhNhan.DataSource = dt;

                if (dt.Rows.Count > 0)
                    cboBenhNhan.SelectedIndex = 0;

                HienThiBenhNhan();
            }
            finally
            {
                dangNap = false;
            }
        }

        private void cboBenhNhan_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            if (dangNap || dangSua)
                return;

            HienThiBenhNhan();
        }

        private void HienThiBenhNhan()
        {
            txtMaBenhNhan.Clear();
            txtTenBenhNhan.Clear();
            txtMaPhieuDK.Clear();

            DataRowView row =
                cboBenhNhan.SelectedItem as DataRowView;

            if (row == null)
                return;

            txtMaBenhNhan.Text =
                Convert.ToString(row["MaBenhNhan"]);

            txtTenBenhNhan.Text =
                Convert.ToString(row["HoTen"]);

            string maPDK =
                Convert.ToString(row["MaPhieuDK"]);

            txtMaPhieuDK.Text =
                string.IsNullOrWhiteSpace(maPDK)
                    ? "Tự tạo khi lưu"
                    : maPDK;
        }

        // ==================================
        // THÊM BỆNH NHÂN NGAY TRÊN FORM
        // ==================================
        private void btnThemBenhNhan_Click(
            object sender, EventArgs e)
        {
            try
            {
                using (FrmBenhNhan frm = new FrmBenhNhan())
                {
                    frm.ShowDialog(this);
                }

                if (!dangSua)
                    NapBenhNhan();
                else
                    LamMoiNhap();
            }
            catch (Exception ex)
            {
                HienLoi(ex);
            }
        }

        // ==================================
        // CHỌN HỒ SƠ TRONG BẢNG
        // ==================================
        private void dgvHoSo_SelectionChanged(
            object sender, EventArgs e)
        {
            if (dangNap ||
                dgvHoSo.CurrentRow == null ||
                !dgvHoSo.CurrentRow.Selected)
                return;

            try
            {
                DataGridViewRow row =
                    dgvHoSo.CurrentRow;

                dangSua = true;

                txtMaHoSo.Text =
                    GiaTri(row, "MaHoSo");

                txtMaBenhNhan.Text =
                    GiaTri(row, "MaBenhNhan");

                txtTenBenhNhan.Text =
                    GiaTri(row, "HoTen");

                txtMaPhieuDK.Text =
                    GiaTri(row, "MaPhieuDK");

                txtTinhTrang.Text =
                    GiaTri(row, "TinhTrangSucKhoe");

                txtGhiChu.Text =
                    GiaTri(row, "GhiChu");

                string trangThai =
                    GiaTri(row, "TrangThai");

                if (!cboTrangThai.Items.Contains(trangThai) &&
                    !string.IsNullOrWhiteSpace(trangThai))
                {
                    cboTrangThai.Items.Add(trangThai);
                }

                cboTrangThai.SelectedItem = trangThai;

                object ngay = row.Cells["NgayLap"].Value;

                if (ngay != null && ngay != DBNull.Value)
                {
                    DateTime d = Convert.ToDateTime(ngay);

                    if (d >= dtNgayLap.MinDate &&
                        d <= dtNgayLap.MaxDate)
                        dtNgayLap.Value = d;
                }

                txtMaHoSo.ReadOnly = true;
                txtMaHoSo.BackColor =
                    Color.FromArgb(235, 235, 235);

                cboBenhNhan.Enabled = false;
                dtNgayLap.Enabled = false;

                btnTaoHoSo.Enabled = false;
                btnCapNhat.Enabled = true;
                btnLichSu.Enabled = true;
            }
            catch (Exception ex)
            {
                HienLoi(ex);
            }
        }

        private string GiaTri(
            DataGridViewRow row, string cot)
        {
            object v = row.Cells[cot].Value;

            return v == null || v == DBNull.Value
                ? ""
                : Convert.ToString(v);
        }

        // ==================================
        // TẠO HỒ SƠ MỚI
        // ==================================
        private void btnTaoHoSo_Click(
            object sender, EventArgs e)
        {
            if (dangSua)
            {
                MessageBox.Show(
                    "Nhấn Làm mới để tạo hồ sơ mới.");
                return;
            }

            if (string.IsNullOrWhiteSpace(
                txtMaBenhNhan.Text))
            {
                MessageBox.Show(
                    "Chưa có bệnh nhân để chọn.\n" +
                    "Nhấn Thêm bệnh nhân trước.");
                return;
            }

            // Xác nhận việc ghi nhận lệ phí giả lập
            DialogResult kq = MessageBox.Show(
                "Tạo hồ sơ cho bệnh nhân " +
                txtTenBenhNhan.Text + "?\n\n" +
                "Nếu chưa có phiếu đăng ký, chương trình " +
                "sẽ tự tạo phiếu đăng ký.\n\n" +
                "Nếu chưa có lệ phí năm " +
                dtNgayLap.Value.Year +
                ", chương trình sẽ ghi nhận khoản " +
                "500.000 VNĐ ĐÃ THANH TOÁN GIẢ LẬP.\n\n" +
                "Chỉ sử dụng trên database bài thực hành.",
                "Xác nhận tạo hồ sơ mẫu",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (kq != DialogResult.Yes)
                return;

            try
            {
                svc.TaoHoSo(
                    txtMaHoSo.Text,
                    txtMaBenhNhan.Text,
                    dtNgayLap.Value,
                    txtTinhTrang.Text,
                    txtGhiChu.Text);

                MessageBox.Show(
                    "Tạo hồ sơ thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                NapDanhSach();
                LamMoiNhap();
            }
            catch (Exception ex)
            {
                HienLoi(ex);
            }
        }

        // ==================================
        // CẬP NHẬT HỒ SƠ
        // ==================================
        private void btnCapNhat_Click(
            object sender, EventArgs e)
        {
            if (!dangSua)
            {
                MessageBox.Show(
                    "Hãy chọn một hồ sơ trong bảng để sửa.");
                return;
            }

            try
            {
                svc.CapNhat(
                    txtMaHoSo.Text,
                    txtTinhTrang.Text,
                    txtGhiChu.Text,
                    cboTrangThai.Text);

                MessageBox.Show(
                    "Cập nhật hồ sơ thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                NapDanhSach(txtTimKiem.Text);
                LamMoiNhap();
            }
            catch (Exception ex)
            {
                HienLoi(ex);
            }
        }

        // ==================================
        // TÌM KIẾM
        // ==================================
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

        // ==================================
        // XEM LỊCH SỬ KHÁM
        // ==================================
        private void btnLichSu_Click(
            object sender, EventArgs e)
        {
            if (!dangSua)
            {
                MessageBox.Show(
                    "Hãy chọn hồ sơ cần xem lịch sử khám.");
                return;
            }

            try
            {
                DataTable dt =
                    svc.LayLichSuKham(txtMaHoSo.Text);

                using (Form frm = new Form())
                {
                    frm.Text =
                        "Lịch sử khám - " + txtMaHoSo.Text;

                    frm.Size = new Size(1050, 520);
                    frm.StartPosition =
                        FormStartPosition.CenterParent;

                    DataGridView dgv = new DataGridView();
                    dgv.Dock = DockStyle.Fill;
                    dgv.ReadOnly = true;
                    dgv.AllowUserToAddRows = false;
                    dgv.AllowUserToDeleteRows = false;
                    dgv.RowHeadersVisible = false;
                    dgv.AutoSizeColumnsMode =
                        DataGridViewAutoSizeColumnsMode.Fill;

                    dgv.DataSource = dt;

                    if (dgv.Columns.Contains("Ngày khám"))
                    {
                        dgv.Columns["Ngày khám"]
                            .DefaultCellStyle.Format =
                                "dd/MM/yyyy HH:mm";
                    }

                    Label lbl = new Label();
                    lbl.Dock = DockStyle.Top;
                    lbl.Height = 45;
                    lbl.Padding = new Padding(12);
                    lbl.Text =
                        "Số lần khám: " + dt.Rows.Count;

                    frm.Controls.Add(dgv);
                    frm.Controls.Add(lbl);
                    frm.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                HienLoi(ex);
            }
        }

        // ==================================
        // LÀM MỚI
        // ==================================
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

            txtMaHoSo.ReadOnly = false;
            txtMaHoSo.BackColor = Color.White;
            txtMaHoSo.Clear();

            txtMaBenhNhan.Clear();
            txtTenBenhNhan.Clear();
            txtMaPhieuDK.Clear();
            txtTinhTrang.Clear();
            txtGhiChu.Clear();

            cboBenhNhan.Enabled = true;
            dtNgayLap.Enabled = true;

            dtNgayLap.Value = DateTime.Today;

            if (cboTrangThai.Items.Count > 0)
                cboTrangThai.SelectedIndex = 0;

            btnTaoHoSo.Enabled = true;
            btnCapNhat.Enabled = false;
            btnLichSu.Enabled = false;

            NapBenhNhan();

            dgvHoSo.ClearSelection();
            txtMaHoSo.Focus();
        }

        private void dtNgayLap_ValueChanged(
            object sender, EventArgs e)
        {
            // Không cần nạp lại danh sách vì
            // bệnh nhân chưa có hồ sơ không phụ
            // thuộc năm đóng phí.
        }

        // ==================================
        // ĐỊNH DẠNG BẢNG
        // ==================================
        private void DinhDangBang(DataGridView dgv)
        {
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.RowHeadersVisible = false;
            dgv.MultiSelect = false;

            dgv.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgv.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgv.BackgroundColor = Color.White;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersHeight = 38;

            dgv.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(219, 230, 241);

            dgv.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 9F, FontStyle.Bold);

            dgv.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(205, 225, 245);

            dgv.DefaultCellStyle.SelectionForeColor =
                Color.Black;
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
                "Lỗi quản lý hồ sơ bệnh án",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}
