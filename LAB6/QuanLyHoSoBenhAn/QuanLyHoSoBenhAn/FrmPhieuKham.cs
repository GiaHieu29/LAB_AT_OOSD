
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Services;

namespace QuanLyHoSoBenhAn
{
    public partial class FrmPhieuKham : Form
    {
        private readonly PhieuKhamService svc =
            new PhieuKhamService();

        private bool dangNap = false;
        private bool dangSua = false;

        public FrmPhieuKham()
        {
            InitializeComponent();
        }

        // =====================================
        // 1. LOAD FORM
        // =====================================

        private void FrmPhieuKham_Load(
            object sender, EventArgs e)
        {
            try
            {
                cboLoaiKham.Items.AddRange(
                    new object[]
                    {
                        "Khám bệnh",
                        "Khám định kỳ"
                    });

                cboTrangThai.Items.AddRange(
                    new object[]
                    {
                        "Đang khám",
                        "Đang điều trị",
                        "Đã hoàn tất"
                    });

                cboLoaiKham.SelectedIndex = 0;
                cboTrangThai.SelectedIndex = 0;

                dangNap = true;

                GanNguon(
                    cboHoSo,
                    svc.LayHoSo(),
                    "HienThi", "MaHoSo");

                GanNguon(
                    cboKhoa,
                    svc.LayKhoa(),
                    "TenKhoa", "MaKhoa");

                dangNap = false;

                NapBacSi();
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
        // 2. NẠP DỮ LIỆU COMBOBOX
        // =====================================

        private void GanNguon(
            ComboBox cbo,
            DataTable dt,
            string hienThi,
            string giaTri)
        {
            cbo.DataSource = null;
            cbo.DisplayMember = hienThi;
            cbo.ValueMember = giaTri;
            cbo.DataSource = dt;
        }

        private string MaChon(ComboBox cbo)
        {
            if (cbo.SelectedValue == null ||
                cbo.SelectedValue is DataRowView)
                return "";

            return Convert.ToString(cbo.SelectedValue);
        }

        private void cboKhoa_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            if (dangNap)
                return;

            try
            {
                NapBacSi();
            }
            catch (Exception ex)
            {
                HienLoi(ex);
            }
        }

        private void NapBacSi()
        {
            string maKhoa = MaChon(cboKhoa);

            GanNguon(
                cboBacSi,
                svc.LayBacSiTheoKhoa(maKhoa),
                "HoTen", "MaBacSi");
        }

        // =====================================
        // 3. HIỂN THỊ DANH SÁCH PHIẾU KHÁM
        // =====================================

        private void NapDanhSach(string tuKhoa = "")
        {
            dangNap = true;

            try
            {
                DataTable dt =
                    svc.LayDanhSach(tuKhoa);

                dgvPhieuKham.DataSource = dt;

                lblSoLuong.Text =
                    "Số phiếu khám: " + dt.Rows.Count;

                if (dgvPhieuKham.Columns
                    .Contains("NgayGioKham"))
                {
                    dgvPhieuKham.Columns["NgayGioKham"]
                        .DefaultCellStyle.Format =
                            "dd/MM/yyyy HH:mm";
                }

                if (dgvPhieuKham.Columns
                    .Contains("NgayXuatVien"))
                {
                    dgvPhieuKham.Columns["NgayXuatVien"]
                        .DefaultCellStyle.Format =
                            "dd/MM/yyyy";
                }

                foreach (DataGridViewColumn c
                    in dgvPhieuKham.Columns)
                {
                    switch (c.Name)
                    {
                        case "MaPhieuKham":
                            c.HeaderText = "Mã phiếu";
                            break;

                        case "MaHoSo":
                            c.HeaderText = "Mã hồ sơ";
                            break;

                        case "TenBenhNhan":
                            c.HeaderText = "Bệnh nhân";
                            break;

                        case "MaKhoa":
                            c.Visible = false;
                            break;

                        case "TenKhoa":
                            c.HeaderText = "Khoa";
                            break;

                        case "MaBacSi":
                            c.Visible = false;
                            break;

                        case "TenBacSi":
                            c.HeaderText = "Bác sĩ";
                            break;

                        case "NgayGioKham":
                            c.HeaderText = "Ngày khám";
                            break;

                        case "LoaiKham":
                            c.HeaderText = "Loại khám";
                            break;

                        case "TrieuChung":
                            c.HeaderText = "Triệu chứng";
                            break;

                        case "NhietDo":
                            c.HeaderText = "Nhiệt độ";
                            break;

                        case "HuyetAp":
                            c.HeaderText = "Huyết áp";
                            break;

                        case "ChanDoan":
                            c.HeaderText = "Chẩn đoán";
                            break;

                        case "HuongDieuTri":
                            c.HeaderText = "Hướng điều trị";
                            break;

                        case "NgayXuatVien":
                            c.HeaderText = "Ngày xuất viện";
                            break;

                        case "GhiChu":
                            c.HeaderText = "Ghi chú";
                            break;

                        case "TrangThai":
                            c.HeaderText = "Trạng thái";
                            break;
                    }
                }

                dgvPhieuKham.ClearSelection();
            }
            finally
            {
                dangNap = false;
            }
        }

        // =====================================
        // 4. CHỌN MỘT PHIẾU KHÁM
        // =====================================

        private object O(
            DataGridViewRow r, string cot)
        {
            return r.Cells[cot].Value;
        }

        private string Chuoi(
            DataGridViewRow r, string cot)
        {
            object value = O(r, cot);

            if (value == null ||
                value == DBNull.Value)
                return "";

            return Convert.ToString(value);
        }

        private void dgvPhieuKham_SelectionChanged(
            object sender, EventArgs e)
        {
            if (dangNap ||
                dgvPhieuKham.CurrentRow == null ||
                !dgvPhieuKham.CurrentRow.Selected)
                return;

            try
            {
                DataGridViewRow r =
                    dgvPhieuKham.CurrentRow;

                dangNap = true;

                txtMaPhieu.Text =
                    Chuoi(r, "MaPhieuKham");

                cboHoSo.SelectedValue =
                    Chuoi(r, "MaHoSo");

                cboKhoa.SelectedValue =
                    Chuoi(r, "MaKhoa");

                dangNap = false;

                NapBacSi();

                cboBacSi.SelectedValue =
                    Chuoi(r, "MaBacSi");

                if (MaChon(cboBacSi) !=
                    Chuoi(r, "MaBacSi"))
                {
                    lblHuongDan.Text =
                        "Bác sĩ của phiếu cũ không còn " +
                        "hoạt động. Hãy chọn bác sĩ khác " +
                        "nếu cập nhật.";
                }
                else
                {
                    lblHuongDan.Text =
                        "Đang chỉnh sửa phiếu khám đã chọn.";
                }

                dtNgayGio.Value =
                    Convert.ToDateTime(
                        O(r, "NgayGioKham"));

                cboLoaiKham.SelectedItem =
                    Chuoi(r, "LoaiKham");

                cboTrangThai.SelectedItem =
                    Chuoi(r, "TrangThai");

                txtTrieuChung.Text =
                    Chuoi(r, "TrieuChung");

                txtHuyetAp.Text =
                    Chuoi(r, "HuyetAp");

                txtChanDoan.Text =
                    Chuoi(r, "ChanDoan");

                txtHuongDieuTri.Text =
                    Chuoi(r, "HuongDieuTri");

                txtGhiChu.Text =
                    Chuoi(r, "GhiChu");

                object temp = O(r, "NhietDo");

                chkNhietDo.Checked =
                    temp != DBNull.Value &&
                    temp != null;

                if (chkNhietDo.Checked)
                {
                    nudNhietDo.Value =
                        Math.Max(
                            nudNhietDo.Minimum,
                            Math.Min(
                                nudNhietDo.Maximum,
                                Convert.ToDecimal(temp)));
                }

                object xuatVien =
                    O(r, "NgayXuatVien");

                dtNgayXuatVien.Checked =
                    xuatVien != DBNull.Value &&
                    xuatVien != null;

                if (dtNgayXuatVien.Checked)
                {
                    dtNgayXuatVien.Value =
                        Convert.ToDateTime(xuatVien);
                }

                dangSua = true;
                txtMaPhieu.ReadOnly = true;

                txtMaPhieu.BackColor =
                    Color.FromArgb(235, 235, 235);
            }
            catch (Exception ex)
            {
                dangNap = false;
                HienLoi(ex);
            }
        }

        // =====================================
        // 5. LÀM MỚI Ô NHẬP
        // =====================================

        private void LamMoiNhap()
        {
            dangSua = false;

            txtMaPhieu.ReadOnly = false;
            txtMaPhieu.BackColor = Color.White;
            txtMaPhieu.Clear();

            if (cboHoSo.Items.Count > 0)
                cboHoSo.SelectedIndex = 0;

            if (cboKhoa.Items.Count > 0)
                cboKhoa.SelectedIndex = 0;

            NapBacSi();

            dtNgayGio.Value = DateTime.Now;
            cboLoaiKham.SelectedIndex = 0;
            cboTrangThai.SelectedIndex = 0;

            txtTrieuChung.Clear();
            txtHuyetAp.Clear();
            txtChanDoan.Clear();
            txtHuongDieuTri.Clear();
            txtGhiChu.Clear();

            chkNhietDo.Checked = false;
            dtNgayXuatVien.Checked = false;

            lblHuongDan.Text =
                "Nhập mã phiếu khám mới, chọn hồ sơ, " +
                "khoa và bác sĩ.";

            dgvPhieuKham.ClearSelection();
        }

        // =====================================
        // 6. LƯU THÔNG TIN
        // =====================================

        private void Luu(bool sua)
        {
            string maHoSo = MaChon(cboHoSo);
            string maKhoa = MaChon(cboKhoa);
            string maBacSi = MaChon(cboBacSi);

            decimal? nhietDo =
                chkNhietDo.Checked
                    ? (decimal?)nudNhietDo.Value
                    : null;

            DateTime? xuatVien =
                dtNgayXuatVien.Checked
                    ? (DateTime?)dtNgayXuatVien.Value.Date
                    : null;

            if (sua)
            {
                svc.Sua(
                    txtMaPhieu.Text,
                    maHoSo, maKhoa, maBacSi,
                    dtNgayGio.Value,
                    cboLoaiKham.Text,
                    txtTrieuChung.Text,
                    nhietDo,
                    txtHuyetAp.Text,
                    txtChanDoan.Text,
                    txtHuongDieuTri.Text,
                    xuatVien,
                    txtGhiChu.Text,
                    cboTrangThai.Text);
            }
            else
            {
                svc.Them(
                    txtMaPhieu.Text,
                    maHoSo, maKhoa, maBacSi,
                    dtNgayGio.Value,
                    cboLoaiKham.Text,
                    txtTrieuChung.Text,
                    nhietDo,
                    txtHuyetAp.Text,
                    txtChanDoan.Text,
                    txtHuongDieuTri.Text,
                    xuatVien,
                    txtGhiChu.Text,
                    cboTrangThai.Text);
            }

            MessageBox.Show(
                sua
                    ? "Cập nhật phiếu khám thành công!"
                    : "Thêm phiếu khám thành công!",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            NapDanhSach(txtTimKiem.Text);
            LamMoiNhap();
        }

        // =====================================
        // 7. CÁC NÚT CHỨC NĂNG
        // =====================================

        private void btnThem_Click(
            object sender, EventArgs e)
        {
            if (dangSua)
            {
                MessageBox.Show(
                    "Nhấn Làm mới để tạo phiếu khám mới.");
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
                    "Chọn một phiếu khám trong bảng để sửa.");
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

        private void btnXoa_Click(
            object sender, EventArgs e)
        {
            if (!dangSua)
            {
                MessageBox.Show(
                    "Chọn phiếu khám cần xóa.");
                return;
            }

            DialogResult kq = MessageBox.Show(
                "Xóa phiếu " + txtMaPhieu.Text + "?\n" +
                "Phiếu có xét nghiệm, toa thuốc hoặc " +
                "thanh toán sẽ không xóa được.",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (kq != DialogResult.Yes)
                return;

            try
            {
                svc.Xoa(txtMaPhieu.Text);

                NapDanhSach(txtTimKiem.Text);
                LamMoiNhap();

                MessageBox.Show(
                    "Xóa phiếu khám thành công.");
            }
            catch (SqlException ex)
                when (ex.Number == 547)
            {
                MessageBox.Show(
                    "Phiếu khám có dữ liệu liên quan " +
                    "(xét nghiệm, toa thuốc hoặc thanh toán), " +
                    "không thể xóa.");
            }
            catch (Exception ex)
            {
                HienLoi(ex);
            }
        }

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

        private void btnDong_Click(
            object sender, EventArgs e)
        {
            Close();
        }

        private void HienLoi(Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Lỗi quản lý phiếu khám",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}
