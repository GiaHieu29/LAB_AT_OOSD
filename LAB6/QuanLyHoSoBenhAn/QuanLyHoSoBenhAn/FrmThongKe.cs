
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Data;

namespace QuanLyHoSoBenhAn
{
    public partial class FrmThongKe : Form
    {
        private DataTable bangTongQuan;
        private DataTable bangChiTiet;

        public FrmThongKe()
        {
            InitializeComponent();
        }

        private void FrmThongKe_Load(
            object sender, EventArgs e)
        {
            try
            {
                NapBoLoc();
                DatLaiBoLoc();
                ThucHienThongKe();
            }
            catch (Exception ex)
            {
                HienLoi(ex);
            }
        }

        // ============================================
        // 1. NẠP BỘ LỌC
        // ============================================

        private void NapBoLoc()
        {
            cboLoaiKham.Items.Clear();
            cboLoaiKham.Items.Add("Tất cả");
            cboLoaiKham.Items.Add("Khám bệnh");
            cboLoaiKham.Items.Add("Khám định kỳ");

            cboTrangThai.Items.Clear();
            cboTrangThai.Items.Add("Tất cả");
            cboTrangThai.Items.Add("Đang khám");
            cboTrangThai.Items.Add("Đang điều trị");
            cboTrangThai.Items.Add("Đã hoàn tất");

            cboNhomThongKe.Items.Clear();
            cboNhomThongKe.Items.Add("Theo trạng thái");
            cboNhomThongKe.Items.Add("Theo khoa");
            cboNhomThongKe.Items.Add("Theo loại khám");

            DataTable khoa = Db.Query(
                "SELECT CAST('' AS VARCHAR(20)) AS MaKhoa, " +
                "N'Tất cả' AS TenKhoa " +
                "UNION ALL " +
                "SELECT MaKhoa, TenKhoa " +
                "FROM dbo.KhoaDieuTri " +
                "ORDER BY TenKhoa");

            cboKhoa.DisplayMember = "TenKhoa";
            cboKhoa.ValueMember = "MaKhoa";
            cboKhoa.DataSource = khoa;
        }

        private void DatLaiBoLoc()
        {
            dtTuNgay.Value =
                new DateTime(DateTime.Today.Year, 1, 1);

            dtDenNgay.Value = DateTime.Today;

            cboLoaiKham.SelectedIndex = 0;
            cboTrangThai.SelectedIndex = 0;
            cboNhomThongKe.SelectedIndex = 0;

            if (cboKhoa.Items.Count > 0)
                cboKhoa.SelectedIndex = 0;
        }

        // ============================================
        // 2. ĐIỀU KIỆN LỌC
        // ============================================

        private string LayDieuKien()
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(
                " WHERE pk.NgayGioKham >= @TuNgay " +
                "AND pk.NgayGioKham < @DenNgay ");

            if (cboLoaiKham.SelectedIndex > 0)
                sql.Append(" AND pk.LoaiKham = @LoaiKham ");

            if (cboTrangThai.SelectedIndex > 0)
                sql.Append(" AND pk.TrangThai = @TrangThai ");

            if (cboKhoa.SelectedValue != null &&
                cboKhoa.SelectedValue.ToString() != "")
            {
                sql.Append(" AND pk.MaKhoa = @MaKhoa ");
            }

            return sql.ToString();
        }

        private SqlParameter[] TaoThamSo()
        {
            return new SqlParameter[]
            {
                Db.P("@TuNgay", dtTuNgay.Value.Date),

                Db.P("@DenNgay",
                    dtDenNgay.Value.Date.AddDays(1)),

                Db.P("@LoaiKham",
                    cboLoaiKham.Text),

                Db.P("@TrangThai",
                    cboTrangThai.Text),

                Db.P("@MaKhoa",
                    cboKhoa.SelectedValue == null
                        ? ""
                        : cboKhoa.SelectedValue.ToString())
            };
        }

        // ============================================
        // 3. THỐNG KÊ
        // ============================================

        private void ThucHienThongKe()
        {
            if (dtTuNgay.Value.Date > dtDenNgay.Value.Date)
            {
                MessageBox.Show(
                    "Từ ngày không được lớn hơn đến ngày.",
                    "Kiểm tra dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            string dk = LayDieuKien();

            string sqlTongQuan = @"
                SELECT
                    COUNT(*) AS TongLuotKham,

                    COUNT(CASE
                        WHEN pk.TrangThai = N'Đang khám'
                        THEN 1 END) AS DangKham,

                    COUNT(CASE
                        WHEN pk.TrangThai = N'Đang điều trị'
                        THEN 1 END) AS DangDieuTri,

                    COUNT(CASE
                        WHEN pk.TrangThai = N'Đã hoàn tất'
                        THEN 1 END) AS HoanTat,

                    COUNT(CASE WHEN xnFlag.MaPhieuKham IS NOT NULL
                        THEN 1 END) AS CoXetNghiem

                FROM dbo.PhieuKham pk

                OUTER APPLY
                (
                    SELECT TOP (1) xn.MaPhieuKham
                    FROM dbo.XetNghiem xn
                    WHERE xn.MaPhieuKham = pk.MaPhieuKham
                ) AS xnFlag
            " + dk;

            bangTongQuan = Db.Query(
                sqlTongQuan, TaoThamSo());

            dgvTongQuan.DataSource = bangTongQuan;

            string truongNhom;

            switch (cboNhomThongKe.SelectedIndex)
            {
                case 1:
                    truongNhom = "k.TenKhoa";
                    break;

                case 2:
                    truongNhom = "pk.LoaiKham";
                    break;

                default:
                    truongNhom = "pk.TrangThai";
                    break;
            }

            string sqlChiTiet = @"
                SELECT
                    " + truongNhom + @" AS Nhom,

                    COUNT(*) AS SoLuong,

                    COUNT(CASE WHEN xnFlag.MaPhieuKham IS NOT NULL
                        THEN 1 END) AS CoXetNghiem

                FROM dbo.PhieuKham pk

                INNER JOIN dbo.KhoaDieuTri k
                    ON pk.MaKhoa = k.MaKhoa

                OUTER APPLY
                (
                    SELECT TOP (1) xn.MaPhieuKham
                    FROM dbo.XetNghiem xn
                    WHERE xn.MaPhieuKham = pk.MaPhieuKham
                ) AS xnFlag
            " + dk + @"

                GROUP BY " + truongNhom + @"

                ORDER BY " + truongNhom;

            bangChiTiet = Db.Query(
                sqlChiTiet, TaoThamSo());

            dgvChiTiet.DataSource = bangChiTiet;

            DinhDangBang();

            lblTieuChi.Text =
                "Tiêu chí: Thống kê theo ngày giờ khám, " +
                "mỗi phiếu khám được đếm một lần.";

            lblGhiChu.Text =
                "Nguồn: SQL Server - QuanLyHoSoBenhAn. " +
                "Số phiếu có xét nghiệm là một phần " +
                "của tổng lượt khám.";

            lblTrangThaiDuLieu.Text =
                "Đã tải dữ liệu lúc " +
                DateTime.Now.ToString("HH:mm:ss");
        }

        // ============================================
        // 4. ĐỊNH DẠNG BẢNG
        // ============================================

        private void DinhDangBang()
        {
            if (dgvTongQuan.Columns.Count == 5)
            {
                dgvTongQuan.Columns["TongLuotKham"]
                    .HeaderText = "Tổng lượt khám";

                dgvTongQuan.Columns["DangKham"]
                    .HeaderText = "Đang khám";

                dgvTongQuan.Columns["DangDieuTri"]
                    .HeaderText = "Đang điều trị";

                dgvTongQuan.Columns["HoanTat"]
                    .HeaderText = "Hoàn tất";

                dgvTongQuan.Columns["CoXetNghiem"]
                    .HeaderText = "Có xét nghiệm";
            }

            if (dgvChiTiet.Columns.Count == 3)
            {
                dgvChiTiet.Columns["Nhom"]
                    .HeaderText = "Nhóm / Trạng thái";

                dgvChiTiet.Columns["SoLuong"]
                    .HeaderText = "Số lượt khám";

                dgvChiTiet.Columns["CoXetNghiem"]
                    .HeaderText = "Có xét nghiệm";

                dgvChiTiet.Columns["Nhom"]
                    .FillWeight = 180;
            }

            DinhDangGrid(dgvTongQuan);
            DinhDangGrid(dgvChiTiet);

            if (dgvTongQuan.Rows.Count > 0)
                dgvTongQuan.ClearSelection();

            if (dgvChiTiet.Rows.Count > 0)
                dgvChiTiet.ClearSelection();
        }

        private void DinhDangGrid(DataGridView dgv)
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
            dgv.BorderStyle = BorderStyle.FixedSingle;
            dgv.EnableHeadersVisualStyles = false;

            dgv.ColumnHeadersHeight = 38;
            dgv.RowTemplate.Height = 30;

            dgv.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(219, 230, 241);

            dgv.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.Black;

            dgv.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 9F, FontStyle.Bold);

            dgv.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(205, 225, 245);

            dgv.DefaultCellStyle.SelectionForeColor =
                Color.Black;
        }

        // ============================================
        // 5. CÁC NÚT CHỨC NĂNG
        // ============================================

        private void btnThongKe_Click(
            object sender, EventArgs e)
        {
            try
            {
                ThucHienThongKe();
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
                DatLaiBoLoc();
                ThucHienThongKe();
            }
            catch (Exception ex)
            {
                HienLoi(ex);
            }
        }

        private void btnXemChiTiet_Click(
            object sender, EventArgs e)
        {
            if (dgvChiTiet.CurrentRow == null)
            {
                MessageBox.Show(
                    "Bạn hãy chọn một nhóm trong bảng " +
                    "thống kê chi tiết.");
                return;
            }

            string nhom = Convert.ToString(
                dgvChiTiet.CurrentRow.Cells["Nhom"].Value);

            if (string.IsNullOrWhiteSpace(nhom))
                return;

            try
            {
                HienThiChiTietNhom(nhom);
            }
            catch (Exception ex)
            {
                HienLoi(ex);
            }
        }

        // ============================================
        // 6. XEM DANH SÁCH PHIẾU KHÁM
        // ============================================

        private void HienThiChiTietNhom(string nhom)
        {
            string dk = LayDieuKien();
            string dkNhom;

            switch (cboNhomThongKe.SelectedIndex)
            {
                case 1:
                    dkNhom = " AND k.TenKhoa = @Nhom ";
                    break;

                case 2:
                    dkNhom = " AND pk.LoaiKham = @Nhom ";
                    break;

                default:
                    dkNhom = " AND pk.TrangThai = @Nhom ";
                    break;
            }

            string sql = @"
                SELECT
                    pk.MaPhieuKham AS [Mã phiếu khám],
                    bn.MaBenhNhan AS [Mã bệnh nhân],
                    bn.HoTen AS [Bệnh nhân],
                    pk.NgayGioKham AS [Ngày giờ khám],
                    bs.HoTen AS [Bác sĩ],
                    k.TenKhoa AS [Khoa],
                    pk.LoaiKham AS [Loại khám],
                    pk.TrangThai AS [Trạng thái]

                FROM dbo.PhieuKham pk

                INNER JOIN dbo.HoSoBenhAn hs
                    ON pk.MaHoSo = hs.MaHoSo

                INNER JOIN dbo.BenhNhan bn
                    ON hs.MaBenhNhan = bn.MaBenhNhan

                INNER JOIN dbo.BacSi bs
                    ON pk.MaBacSi = bs.MaBacSi

                INNER JOIN dbo.KhoaDieuTri k
                    ON pk.MaKhoa = k.MaKhoa

            " + dk + dkNhom + @"

                ORDER BY pk.NgayGioKham DESC";

            SqlParameter[] goc = TaoThamSo();

            SqlParameter[] thamSo =
                new SqlParameter[goc.Length + 1];

            Array.Copy(goc, thamSo, goc.Length);

            thamSo[goc.Length] = Db.P("@Nhom", nhom);

            DataTable ds = Db.Query(sql, thamSo);

            using (Form frm = new Form())
            {
                frm.Text = "Chi tiết nhóm: " + nhom;
                frm.StartPosition =
                    FormStartPosition.CenterParent;
                frm.Size = new Size(1100, 530);
                frm.Font = new Font("Segoe UI", 9F);

                DataGridView dgv = new DataGridView();
                dgv.Dock = DockStyle.Fill;
                dgv.DataSource = ds;

                DinhDangGrid(dgv);

                if (dgv.Columns.Contains("Ngày giờ khám"))
                {
                    dgv.Columns["Ngày giờ khám"]
                        .DefaultCellStyle.Format =
                            "dd/MM/yyyy HH:mm";
                }

                Panel panel = new Panel();
                panel.Dock = DockStyle.Bottom;
                panel.Height = 55;

                Button btnDong = new Button();
                btnDong.Text = "Đóng";
                btnDong.Size = new Size(110, 32);
                btnDong.Anchor =
                    AnchorStyles.Right | AnchorStyles.Top;
                btnDong.Location =
                    new Point(frm.ClientSize.Width - 130, 10);

                btnDong.Click += (s, e) => frm.Close();

                panel.Controls.Add(btnDong);

                frm.Controls.Add(dgv);
                frm.Controls.Add(panel);

                frm.ShowDialog(this);
            }
        }

        // ============================================
        // 7. XUẤT BẢNG RA FILE CSV
        // ============================================

        private void btnXuatBang_Click(
            object sender, EventArgs e)
        {
            if (bangTongQuan == null ||
                bangChiTiet == null)
            {
                MessageBox.Show(
                    "Chưa có dữ liệu thống kê để xuất.");
                return;
            }

            using (SaveFileDialog dlg =
                new SaveFileDialog())
            {
                dlg.Title = "Xuất báo cáo thống kê";
                dlg.Filter =
                    "CSV (*.csv)|*.csv";

                dlg.FileName =
                    "ThongKeBenhAn_" +
                    DateTime.Now.ToString("yyyyMMdd") +
                    ".csv";

                if (dlg.ShowDialog(this) !=
                    DialogResult.OK)
                    return;

                try
                {
                    using (StreamWriter sw =
                        new StreamWriter(
                            dlg.FileName,
                            false,
                            new UTF8Encoding(true)))
                    {
                        sw.WriteLine(
                            "THỐNG KÊ HỒ SƠ BỆNH ÁN");

                        sw.WriteLine(
                            "Từ ngày," +
                            CSV(dtTuNgay.Value.ToString(
                                "dd/MM/yyyy")));

                        sw.WriteLine(
                            "Đến ngày," +
                            CSV(dtDenNgay.Value.ToString(
                                "dd/MM/yyyy")));

                        sw.WriteLine();

                        sw.WriteLine("TỔNG QUAN");
                        GhiBangCSV(sw, bangTongQuan);

                        sw.WriteLine();

                        sw.WriteLine("THỐNG KÊ CHI TIẾT");
                        GhiBangCSV(sw, bangChiTiet);
                    }

                    MessageBox.Show(
                        "Đã xuất báo cáo:\n" +
                        dlg.FileName,
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    HienLoi(ex);
                }
            }
        }

        private void GhiBangCSV(
            StreamWriter sw, DataTable dt)
        {
            for (int c = 0; c < dt.Columns.Count; c++)
            {
                if (c > 0) sw.Write(",");

                sw.Write(CSV(dt.Columns[c].ColumnName));
            }

            sw.WriteLine();

            foreach (DataRow r in dt.Rows)
            {
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    if (c > 0) sw.Write(",");

                    sw.Write(CSV(
                        Convert.ToString(r[c])));
                }

                sw.WriteLine();
            }
        }

        private string CSV(string value)
        {
            if (value == null)
                value = "";

            // Tránh Excel hiểu dữ liệu nhập từ CSDL
            // thành công thức.
            if (value.Length > 0 &&
                "=+-@".IndexOf(value[0]) >= 0)
            {
                value = "'" + value;
            }

            return "\"" + value.Replace(
                "\"", "\"\"") + "\"";
        }

        private void HienLoi(Exception ex)
        {
            MessageBox.Show(
                "Có lỗi khi truy vấn SQL Server:\n\n" +
                ex.Message,
                "Lỗi thống kê",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}
