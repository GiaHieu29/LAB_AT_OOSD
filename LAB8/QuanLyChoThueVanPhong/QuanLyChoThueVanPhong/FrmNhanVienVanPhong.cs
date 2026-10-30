
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using QuanLyChoThueVanPhong.Services;

namespace QuanLyChoThueVanPhong
{
    public partial class FrmNhanVienVanPhong : Form
    {
        private readonly NhanVienVanPhongService svc =
            new NhanVienVanPhongService();

        private bool dangNap = false;
        private bool dangSua = false;
        private string anhHienTai = "";
        private string anhMoiChon = "";

        public FrmNhanVienVanPhong()
        {
            InitializeComponent();
        }

        private void FrmNhanVienVanPhong_Load(
            object sender, EventArgs e)
        {
            try
            {
                cboGioiTinh.Items.AddRange(
                    new object[] { "Nam", "Nữ", "Khác" });

                cboTrangThai.Items.AddRange(
                    new object[]
                    {
                        "Đang làm việc", "Đã nghỉ"
                    });

                NapCongTy();
                NapDanhSach();
                LamMoiNhap();
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        private string GiaTriCombo(ComboBox cbo)
        {
            if (cbo.SelectedValue == null ||
                cbo.SelectedValue is DataRowView)
                return "";

            return Convert.ToString(cbo.SelectedValue);
        }

        private void NapCongTy()
        {
            dangNap = true;
            try
            {
                cboCongTy.DataSource = null;
                cboCongTy.DisplayMember = "TenKhachHang";
                cboCongTy.ValueMember = "MaKhachHang";
                cboCongTy.DataSource = svc.LayCongTy();
            }
            finally
            {
                dangNap = false;
            }

            NapPhong();
        }

        private void NapPhong()
        {
            if (dangNap) return;

            dangNap = true;

            try
            {
                DataTable dt = svc.LayPhongTheoCongTy(
                    GiaTriCombo(cboCongTy));

                // Cho phép nhân viên chưa phân phòng.
                DataRow rong = dt.NewRow();
                rong["MaPhong"] = "";
                rong["HienThi"] = "-- Chưa phân phòng --";
                dt.Rows.InsertAt(rong, 0);

                cboPhong.DataSource = null;
                cboPhong.DisplayMember = "HienThi";
                cboPhong.ValueMember = "MaPhong";
                cboPhong.DataSource = dt;
            }
            finally
            {
                dangNap = false;
            }
        }

        private void cboCongTy_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            if (!dangNap)
            {
                try { NapPhong(); }
                catch (Exception ex) { BaoLoi(ex); }
            }
        }

        private void NapDanhSach(string key = "")
        {
            dangNap = true;

            try
            {
                DataTable dt = svc.LayDanhSach(key);
                dgvNhanVien.DataSource = dt;

                string[] cot =
                {
                    "MaNhanVien", "TenKhachHang",
                    "TenPhong", "TenTang", "HoTen",
                    "NgaySinh", "GioiTinh", "CCCD",
                    "ChucVu", "DienThoai", "TrangThai"
                };

                string[] ten =
                {
                    "Mã NV", "Công ty",
                    "Phòng", "Tầng", "Họ tên",
                    "Ngày sinh", "Giới tính", "CCCD",
                    "Chức vụ", "Điện thoại", "Trạng thái"
                };

                for (int i = 0; i < cot.Length; i++)
                {
                    if (dgvNhanVien.Columns.Contains(cot[i]))
                        dgvNhanVien.Columns[cot[i]]
                            .HeaderText = ten[i];
                }

                foreach (string col in new[]
                {
                    "MaKhachHang", "MaPhong", "AnhNhanVien"
                })
                {
                    if (dgvNhanVien.Columns.Contains(col))
                        dgvNhanVien.Columns[col].Visible = false;
                }

                if (dgvNhanVien.Columns.Contains("NgaySinh"))
                    dgvNhanVien.Columns["NgaySinh"]
                        .DefaultCellStyle.Format = "dd/MM/yyyy";

                lblSoLuong.Text =
                    "Số nhân viên: " + dt.Rows.Count;

                dgvNhanVien.ClearSelection();
            }
            finally
            {
                dangNap = false;
            }
        }

        private string Cell(DataGridViewRow r, string c)
        {
            object v = r.Cells[c].Value;

            return v == null || v == DBNull.Value
                ? "" : Convert.ToString(v);
        }

        private void dgvNhanVien_SelectionChanged(
            object sender, EventArgs e)
        {
            if (dangNap ||
                dgvNhanVien.CurrentRow == null ||
                !dgvNhanVien.CurrentRow.Selected)
                return;

            try
            {
                dangNap = true;
                DataGridViewRow r = dgvNhanVien.CurrentRow;

                txtMaNhanVien.Text = Cell(r, "MaNhanVien");
                txtHoTen.Text = Cell(r, "HoTen");

                cboCongTy.SelectedValue =
                    Cell(r, "MaKhachHang");

                dangNap = false;
                NapPhong();
                dangNap = true;

                cboPhong.SelectedValue = Cell(r, "MaPhong");

                dtNgaySinh.Checked =
                    r.Cells["NgaySinh"].Value != DBNull.Value;

                if (dtNgaySinh.Checked)
                    dtNgaySinh.Value = Convert.ToDateTime(
                        r.Cells["NgaySinh"].Value);
                else
                    dtNgaySinh.Value = DateTime.Today;

                cboGioiTinh.SelectedItem =
                    Cell(r, "GioiTinh");

                txtCCCD.Text = Cell(r, "CCCD");
                txtChucVu.Text = Cell(r, "ChucVu");
                txtDienThoai.Text = Cell(r, "DienThoai");

                cboTrangThai.SelectedItem =
                    Cell(r, "TrangThai");

                anhHienTai = Cell(r, "AnhNhanVien");
                anhMoiChon = "";

                txtDuongDanAnh.Text = anhHienTai;
                HienAnh(anhHienTai);

                dangSua = true;
                txtMaNhanVien.ReadOnly = true;
                txtMaNhanVien.BackColor = Color.Gainsboro;

                lblHuongDan.Text =
                    "Đang sửa " + txtMaNhanVien.Text +
                    ". Mã nhân viên không được thay đổi.";
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
            finally
            {
                dangNap = false;
            }
        }

        private string ThuMucAnh()
        {
            return Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Images", "NhanVien");
        }

        private string DuongDanTuyetDoi(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return "";

            // Chỉ nhận đường dẫn tương đối do ứng dụng lưu.
            string root = Path.GetFullPath(
                AppDomain.CurrentDomain.BaseDirectory);

            string file = Path.GetFullPath(
                Path.Combine(root, path));

            if (!file.StartsWith(root,
                StringComparison.OrdinalIgnoreCase))
                throw new Exception(
                    "Đường dẫn ảnh nằm ngoài thư mục ứng dụng.");

            return file;
        }

        private void HienAnh(string path)
        {
            if (picAnh.Image != null)
            {
                Image old = picAnh.Image;
                picAnh.Image = null;
                old.Dispose();
            }

            if (string.IsNullOrWhiteSpace(path))
                return;

            string file = Path.IsPathRooted(path)
                ? path
                : DuongDanTuyetDoi(path);

            if (!File.Exists(file))
                return;

            // Đọc vào bộ nhớ để không khóa file ảnh.
            using (FileStream fs = new FileStream(
                file, FileMode.Open, FileAccess.Read))
            using (Image image = Image.FromStream(fs))
            {
                picAnh.Image = new Bitmap(image);
            }
        }

        private void btnChonAnh_Click(
            object sender, EventArgs e)
        {
            using (OpenFileDialog dialog =
                new OpenFileDialog())
            {
                dialog.Title = "Chọn ảnh nhân viên";
                dialog.Filter =
                    "Ảnh JPG, PNG (*.jpg;*.jpeg;*.png)" +
                    "|*.jpg;*.jpeg;*.png";
                dialog.CheckFileExists = true;

                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return;

                try
                {
                    anhMoiChon = dialog.FileName;
                    txtDuongDanAnh.Text =
                        Path.GetFileName(anhMoiChon);

                    HienAnh(anhMoiChon);
                }
                catch (Exception ex)
                {
                    BaoLoi(ex);
                }
            }
        }

        private void btnXoaAnh_Click(
            object sender, EventArgs e)
        {
            anhMoiChon = "";
            anhHienTai = "";
            txtDuongDanAnh.Clear();
            HienAnh("");
        }

        private string LuuAnhDaChon(string ma)
        {
            if (string.IsNullOrEmpty(anhMoiChon))
                return anhHienTai;

            Directory.CreateDirectory(ThuMucAnh());

            string duoi =
                Path.GetExtension(anhMoiChon).ToLowerInvariant();

            if (duoi != ".jpg" && duoi != ".jpeg" &&
                duoi != ".png")
                throw new Exception("Định dạng ảnh không hỗ trợ.");

            // Không ghi đè ảnh cũ. Tạo tên duy nhất.
            string tenFile =
                Guid.NewGuid().ToString("N") + duoi;

            string dich =
                Path.Combine(ThuMucAnh(), tenFile);

            File.Copy(anhMoiChon, dich, false);

            return Path.Combine(
                "Images", "NhanVien", tenFile);
        }

        private void LamMoiNhap()
        {
            dangSua = false;
            anhHienTai = "";
            anhMoiChon = "";

            txtMaNhanVien.ReadOnly = false;
            txtMaNhanVien.BackColor = Color.White;

            txtMaNhanVien.Clear();
            txtHoTen.Clear();
            txtCCCD.Clear();
            txtChucVu.Clear();
            txtDienThoai.Clear();
            txtDuongDanAnh.Clear();

            dtNgaySinh.Checked = false;
            dtNgaySinh.Value = DateTime.Today;

            if (cboCongTy.Items.Count > 0)
                cboCongTy.SelectedIndex = 0;

            NapPhong();

            if (cboGioiTinh.Items.Count > 0)
                cboGioiTinh.SelectedIndex = 0;

            if (cboTrangThai.Items.Count > 0)
                cboTrangThai.SelectedIndex = 0;

            HienAnh("");
            dgvNhanVien.ClearSelection();

            lblHuongDan.Text =
                "Nhập thông tin nhân viên mới rồi nhấn Thêm.";

            txtMaNhanVien.Focus();
        }

        private void Luu(bool sua)
        {
            string pathMoi = "";
            bool daCopy = false;

            try
            {
                pathMoi = LuuAnhDaChon(
                    txtMaNhanVien.Text.Trim());

                daCopy = !string.IsNullOrEmpty(anhMoiChon);

                DateTime? ngaySinh =
                    dtNgaySinh.Checked
                        ? (DateTime?)dtNgaySinh.Value.Date
                        : null;

                string phong = GiaTriCombo(cboPhong);

                if (sua)
                {
                    svc.Sua(
                        txtMaNhanVien.Text,
                        GiaTriCombo(cboCongTy),
                        phong,
                        txtHoTen.Text,
                        ngaySinh,
                        cboGioiTinh.Text,
                        txtCCCD.Text,
                        txtChucVu.Text,
                        txtDienThoai.Text,
                        pathMoi,
                        cboTrangThai.Text);
                }
                else
                {
                    svc.Them(
                        txtMaNhanVien.Text,
                        GiaTriCombo(cboCongTy),
                        phong,
                        txtHoTen.Text,
                        ngaySinh,
                        cboGioiTinh.Text,
                        txtCCCD.Text,
                        txtChucVu.Text,
                        txtDienThoai.Text,
                        pathMoi,
                        cboTrangThai.Text);
                }

                MessageBox.Show(
                    sua
                        ? "Cập nhật nhân viên thành công!"
                        : "Thêm nhân viên thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                NapDanhSach(txtTimKiem.Text);
                LamMoiNhap();
            }
            catch
            {
                // Dọn file mới sao chép nếu SQL không lưu được.
                if (daCopy && !string.IsNullOrEmpty(pathMoi))
                {
                    try
                    {
                        string file = DuongDanTuyetDoi(pathMoi);
                        if (File.Exists(file)) File.Delete(file);
                    }
                    catch { }
                }
                throw;
            }
        }

        private void btnThem_Click(
            object sender, EventArgs e)
        {
            if (dangSua)
            {
                MessageBox.Show(
                    "Nhấn Làm mới để thêm nhân viên mới.");
                return;
            }

            try { Luu(false); }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void btnSua_Click(
            object sender, EventArgs e)
        {
            if (!dangSua)
            {
                MessageBox.Show(
                    "Chọn nhân viên trong bảng để sửa.");
                return;
            }

            try { Luu(true); }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void btnXoa_Click(
            object sender, EventArgs e)
        {
            if (!dangSua)
            {
                MessageBox.Show(
                    "Chọn nhân viên cần xóa.");
                return;
            }

            if (MessageBox.Show(
                "Xóa nhân viên " + txtMaNhanVien.Text + "?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                svc.Xoa(txtMaNhanVien.Text);

                // Giữ file ảnh trên đĩa để tránh làm mất
                // ảnh khi dữ liệu đang được kiểm tra.
                NapDanhSach(txtTimKiem.Text);
                LamMoiNhap();

                MessageBox.Show("Đã xóa nhân viên.");
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void btnTimKiem_Click(
            object sender, EventArgs e)
        {
            try
            {
                NapDanhSach(txtTimKiem.Text);
                LamMoiNhap();
            }
            catch (Exception ex) { BaoLoi(ex); }
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

        private void btnLamMoi_Click(
            object sender, EventArgs e)
        {
            try
            {
                txtTimKiem.Clear();
                NapCongTy();
                NapDanhSach();
                LamMoiNhap();
            }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void btnDong_Click(
            object sender, EventArgs e)
        {
            Close();
        }

        private void BaoLoi(Exception ex)
        {
            SqlException sql = ex as SqlException;

            if (sql != null)
            {
                if (sql.Number == 2627 ||
                    sql.Number == 2601)
                {
                    MessageBox.Show(
                        "Mã nhân viên hoặc CCCD đã tồn tại.",
                        "Trùng dữ liệu",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                if (sql.Number == 547)
                {
                    MessageBox.Show(
                        "Nhân viên đang liên kết với dữ liệu " +
                        "khác hoặc phòng/công ty không hợp lệ.",
                        "Ràng buộc dữ liệu",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
            }

            MessageBox.Show(
                ex.Message,
                "Lỗi quản lý nhân viên",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }
}
