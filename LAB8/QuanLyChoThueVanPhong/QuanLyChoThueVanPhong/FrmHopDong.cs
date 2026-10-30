
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QuanLyChoThueVanPhong.Services;

namespace QuanLyChoThueVanPhong
{
    public partial class FrmHopDong : Form
    {
        private readonly HopDongService svc =
            new HopDongService();

        private readonly DataTable bangPhongChon =
            new DataTable();

        private bool dangNap = false;
        private bool dangSua = false;

        public FrmHopDong()
        {
            InitializeComponent();

            bangPhongChon.Columns.Add(
                "MaPhong", typeof(string));

            bangPhongChon.Columns.Add(
                "TenPhong", typeof(string));

            bangPhongChon.Columns.Add(
                "DonGiaThue", typeof(decimal));

            dgvPhongChon.DataSource = bangPhongChon;

            DinhDangPhongChon();
        }

        private void FrmHopDong_Load(object sender, EventArgs e)
        {
            try
            {
                cboTrangThai.Items.AddRange(new object[]
                {
                    "Dự thảo",
                    "Có hiệu lực",
                    "Hết hạn",
                    "Đã chấm dứt"
                });

                NapKhachHang();
                NapDanhSach();
                LamMoiNhap();
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        // ===========================================
        // NẠP DỮ LIỆU
        // ===========================================

        private void NapKhachHang()
        {
            cboKhachHang.DisplayMember =
                "TenKhachHang";

            cboKhachHang.ValueMember =
                "MaKhachHang";

            cboKhachHang.DataSource =
                svc.LayKhachHang();
        }

        private string MaKhachHang()
        {
            if (cboKhachHang.SelectedValue == null ||
                cboKhachHang.SelectedValue is DataRowView)
                return "";

            return Convert.ToString(
                cboKhachHang.SelectedValue);
        }

        private void NapDanhSach(string key = "")
        {
            dangNap = true;
            try
            {
                DataTable dt = svc.LayDanhSach(key);
                dgvHopDong.DataSource = dt;

                DoiTieuDe("MaHopDong", "Mã hợp đồng");
                DoiTieuDe("TenKhachHang", "Khách hàng");
                DoiTieuDe("NgayKy", "Ngày ký");
                DoiTieuDe("NgayBatDau", "Bắt đầu");
                DoiTieuDe("NgayKetThuc", "Kết thúc");
                DoiTieuDe("SoThangThue", "Số tháng");
                DoiTieuDe("SoPhong", "Số phòng");
                DoiTieuDe("TongTienThang", "Tiền/tháng");
                DoiTieuDe("TrangThai", "Trạng thái");

                dgvHopDong.Columns["MaKhachHang"]
                    .Visible = false;

                dgvHopDong.Columns["NoiDung"]
                    .Visible = false;

                foreach (string c in new[]
                {
                    "NgayKy", "NgayBatDau", "NgayKetThuc"
                })
                {
                    dgvHopDong.Columns[c]
                        .DefaultCellStyle.Format = "dd/MM/yyyy";
                }

                dgvHopDong.Columns["TongTienThang"]
                    .DefaultCellStyle.Format = "N0";

                lblSoLuong.Text =
                    "Số hợp đồng: " + dt.Rows.Count;

                dgvHopDong.ClearSelection();
            }
            finally
            {
                dangNap = false;
            }
        }

        private void DoiTieuDe(string cot, string text)
        {
            if (dgvHopDong.Columns.Contains(cot))
                dgvHopDong.Columns[cot].HeaderText = text;
        }

        private DateTime NgayKetThuc()
        {
            return svc.TinhNgayKetThuc(
                dtBatDau.Value.Date,
                Convert.ToInt32(nudSoThang.Value));
        }

        private void CapNhatNgay()
        {
            try
            {
                dtKetThuc.Value = NgayKetThuc();
            }
            catch (Exception ex)
            {
                lblTongTien.Text = ex.Message;
            }
        }

        // ===========================================
        // PHÒNG KHẢ DỤNG
        // ===========================================

        private void NapPhongKhaDung()
        {
            string maHD = dangSua
                ? txtMaHopDong.Text.Trim()
                : "";

            DataTable dt = svc.LayPhongKhaDung(
                dtBatDau.Value.Date,
                NgayKetThuc(),
                maHD);

            // Không đưa phòng đã chọn vào danh sách.
            HashSet<string> daChon =
                new HashSet<string>(
                    bangPhongChon.AsEnumerable()
                        .Select(r =>
                            Convert.ToString(r["MaPhong"])));

            DataTable loc = dt.Clone();

            foreach (DataRow row in dt.Rows)
            {
                string ma = Convert.ToString(
                    row["MaPhong"]);

                if (!daChon.Contains(ma))
                    loc.ImportRow(row);
            }

            cboPhong.DisplayMember = "TenPhong";
            cboPhong.ValueMember = "MaPhong";
            cboPhong.DataSource = loc;

            btnThemPhong.Enabled = loc.Rows.Count > 0;
        }

        private void DinhDangPhongChon()
        {
            dgvPhongChon.Columns["MaPhong"].HeaderText =
                "Mã phòng";

            dgvPhongChon.Columns["TenPhong"].HeaderText =
                "Tên phòng";

            dgvPhongChon.Columns["DonGiaThue"].HeaderText =
                "Giá thuê/tháng";

            dgvPhongChon.Columns["DonGiaThue"]
                .DefaultCellStyle.Format = "N0";
        }

        private void btnThemPhong_Click(
            object sender, EventArgs e)
        {
            try
            {
                DataRowView row =
                    cboPhong.SelectedItem as DataRowView;

                if (row == null)
                {
                    MessageBox.Show(
                        "Không có phòng khả dụng để thêm.");
                    return;
                }

                string ma = Convert.ToString(row["MaPhong"]);

                if (bangPhongChon.AsEnumerable().Any(
                    r => Convert.ToString(r["MaPhong"]) == ma))
                {
                    MessageBox.Show(
                        "Phòng này đã được chọn.");
                    return;
                }

                decimal gia = nudGiaPhong.Value;

                bangPhongChon.Rows.Add(
                    ma,
                    Convert.ToString(row["TenPhong"]),
                    gia);

                TinhTien();
                NapPhongKhaDung();
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        private void cboPhong_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            DataRowView row =
                cboPhong.SelectedItem as DataRowView;

            if (row != null)
            {
                decimal gia = Convert.ToDecimal(
                    row["DonGiaThang"]);

                nudGiaPhong.Value = Math.Min(
                    nudGiaPhong.Maximum, gia);
            }
        }

        private void btnBoPhong_Click(
            object sender, EventArgs e)
        {
            if (dgvPhongChon.CurrentRow == null ||
                dgvPhongChon.CurrentRow.IsNewRow)
            {
                MessageBox.Show(
                    "Hãy chọn phòng cần bỏ.");
                return;
            }

            DataRowView rv =
                dgvPhongChon.CurrentRow.DataBoundItem
                    as DataRowView;

            if (rv != null)
            {
                bangPhongChon.Rows.Remove(rv.Row);

                TinhTien();
                NapPhongKhaDung();
            }
        }

        private void TinhTien()
        {
            decimal tong = bangPhongChon.AsEnumerable()
                .Sum(r => r.Field<decimal>("DonGiaThue"));

            lblTongTien.Text =
                "Tổng tiền/tháng: " +
                tong.ToString("N0") + " đ" +
                "     |     Trả trước 6 tháng: " +
                (tong * 6).ToString("N0") + " đ";
        }

        // ===========================================
        // CHỌN HỢP ĐỒNG
        // ===========================================

        private string GiaTri(
            DataGridViewRow r, string cot)
        {
            object value = r.Cells[cot].Value;

            return value == null || value == DBNull.Value
                ? "" : Convert.ToString(value);
        }

        private void dgvHopDong_SelectionChanged(
            object sender, EventArgs e)
        {
            if (dangNap ||
                dgvHopDong.CurrentRow == null ||
                !dgvHopDong.CurrentRow.Selected)
                return;

            try
            {
                DataGridViewRow r = dgvHopDong.CurrentRow;

                dangSua = true;

                txtMaHopDong.Text =
                    GiaTri(r, "MaHopDong");

                txtMaHopDong.ReadOnly = true;
                txtMaHopDong.BackColor = Color.Gainsboro;

                cboKhachHang.SelectedValue =
                    GiaTri(r, "MaKhachHang");

                dtNgayKy.Value = Convert.ToDateTime(
                    r.Cells["NgayKy"].Value);

                dtBatDau.Value = Convert.ToDateTime(
                    r.Cells["NgayBatDau"].Value);

                nudSoThang.Value = Convert.ToDecimal(
                    r.Cells["SoThangThue"].Value);

                cboTrangThai.SelectedItem =
                    GiaTri(r, "TrangThai");

                txtNoiDung.Text =
                    GiaTri(r, "NoiDung");

                CapNhatNgay();

                bangPhongChon.Clear();

                DataTable dt = svc.LayChiTiet(
                    txtMaHopDong.Text);

                foreach (DataRow p in dt.Rows)
                {
                    bangPhongChon.Rows.Add(
                        Convert.ToString(p["MaPhong"]),
                        Convert.ToString(p["TenPhong"]),
                        Convert.ToDecimal(p["DonGiaThue"]));
                }

                TinhTien();
                NapPhongKhaDung();

                lblHuongDan.Text =
                    "Đang xem hợp đồng " +
                    txtMaHopDong.Text +
                    ". Chỉ có thể sửa nếu chưa phát sinh giao dịch.";
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        // ===========================================
        // LÀM MỚI
        // ===========================================

        private void LamMoiNhap()
        {
            dangSua = false;

            txtMaHopDong.ReadOnly = false;
            txtMaHopDong.BackColor = Color.White;

            txtMaHopDong.Clear();
            txtNoiDung.Clear();

            dtNgayKy.Value = DateTime.Today;
            dtBatDau.Value = DateTime.Today;

            nudSoThang.Value = 6;

            if (cboTrangThai.Items.Count > 0)
                cboTrangThai.SelectedItem = "Dự thảo";

            if (cboKhachHang.Items.Count > 0)
                cboKhachHang.SelectedIndex = 0;

            bangPhongChon.Clear();

            CapNhatNgay();
            TinhTien();
            NapPhongKhaDung();

            dgvHopDong.ClearSelection();

            lblHuongDan.Text =
                "Nhập hợp đồng mới và chọn các phòng thuê.";

            txtMaHopDong.Focus();
        }

        // ===========================================
        // SỰ KIỆN NGÀY / THÁNG
        // ===========================================

        private void dtBatDau_ValueChanged(
            object sender, EventArgs e)
        {
            if (nudSoThang == null ||
                dtKetThuc == null) return;

            CapNhatNgay();

            if (!dangNap && cboPhong != null)
            {
                try { NapPhongKhaDung(); }
                catch { /* Xử lý khi nhấn lưu/tải */ }
            }
        }

        private void nudSoThang_ValueChanged(
            object sender, EventArgs e)
        {
            dtBatDau_ValueChanged(sender, e);
        }

        private List<PhongThueItem> DanhSachPhong()
        {
            return bangPhongChon.AsEnumerable()
                .Select(r => new PhongThueItem
                {
                    MaPhong =
                        Convert.ToString(r["MaPhong"]),

                    DonGiaThue =
                        Convert.ToDecimal(r["DonGiaThue"])
                })
                .ToList();
        }

        // ===========================================
        // THÊM / SỬA
        // ===========================================

        private void Luu(bool sua)
        {
            svc.Luu(
                txtMaHopDong.Text,
                MaKhachHang(),
                dtNgayKy.Value,
                dtBatDau.Value,
                Convert.ToInt32(nudSoThang.Value),
                txtNoiDung.Text,
                cboTrangThai.Text,
                DanhSachPhong(),
                sua);

            MessageBox.Show(
                sua
                    ? "Cập nhật hợp đồng thành công!"
                    : "Tạo hợp đồng thành công!\n\n" +
                      "Hãy lập thanh toán 6 tháng đầu " +
                      "trước khi bàn giao phòng.",
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
                    "Nhấn Làm mới để tạo hợp đồng mới.");
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
                    "Hãy chọn hợp đồng cần sửa.");
                return;
            }

            try { Luu(true); }
            catch (Exception ex) { BaoLoi(ex); }
        }

        // ===========================================
        // XÓA
        // ===========================================

        private void btnXoa_Click(
            object sender, EventArgs e)
        {
            if (!dangSua)
            {
                MessageBox.Show(
                    "Hãy chọn hợp đồng cần xóa.");
                return;
            }

            if (MessageBox.Show(
                "Xóa hợp đồng " +
                txtMaHopDong.Text + "?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                svc.Xoa(txtMaHopDong.Text);

                NapDanhSach(txtTimKiem.Text);
                LamMoiNhap();

                MessageBox.Show(
                    "Đã xóa hợp đồng.");
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        // ===========================================
        // TÌM KIẾM / LÀM MỚI / ĐÓNG
        // ===========================================

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

        private void btnLamMoi_Click(
            object sender, EventArgs e)
        {
            try
            {
                txtTimKiem.Clear();
                NapKhachHang();
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

        private void txtTimKiem_KeyDown(
            object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnTimKiem_Click(sender, EventArgs.Empty);
            }
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
                        "Mã hợp đồng hoặc chi tiết đã tồn tại.",
                        "Trùng dữ liệu");
                    return;
                }

                if (sql.Number == 547)
                {
                    MessageBox.Show(
                        "Không thể thực hiện vì dữ liệu " +
                        "đang liên kết với bảng khác.",
                        "Ràng buộc dữ liệu");
                    return;
                }

                if (sql.Number == 1205)
                {
                    MessageBox.Show(
                        "Có giao dịch khác đang cập nhật " +
                        "dữ liệu. Hãy thử lại.",
                        "Xung đột giao dịch");
                    return;
                }
            }

            MessageBox.Show(
                ex.Message,
                "Lỗi quản lý hợp đồng",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}
