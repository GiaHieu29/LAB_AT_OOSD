
namespace QuanLyHoSoBenhAn
{
    partial class FrmThongKe
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTuNgay;
        private System.Windows.Forms.Label lblDenNgay;
        private System.Windows.Forms.Label lblLoaiKham;
        private System.Windows.Forms.Label lblKhoa;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.Label lblNhomThongKe;
        private System.Windows.Forms.Label lblTieuChi;
        private System.Windows.Forms.Label lblBangChiTiet;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.Label lblTrangThaiDuLieu;

        private System.Windows.Forms.DateTimePicker dtTuNgay;
        private System.Windows.Forms.DateTimePicker dtDenNgay;

        private System.Windows.Forms.ComboBox cboLoaiKham;
        private System.Windows.Forms.ComboBox cboKhoa;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.ComboBox cboNhomThongKe;

        private System.Windows.Forms.Button btnThongKe;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnXuatBang;
        private System.Windows.Forms.Button btnXemChiTiet;

        private System.Windows.Forms.DataGridView dgvTongQuan;
        private System.Windows.Forms.DataGridView dgvChiTiet;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTuNgay = new System.Windows.Forms.Label();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.lblLoaiKham = new System.Windows.Forms.Label();
            this.lblKhoa = new System.Windows.Forms.Label();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.lblNhomThongKe = new System.Windows.Forms.Label();
            this.lblTieuChi = new System.Windows.Forms.Label();
            this.lblBangChiTiet = new System.Windows.Forms.Label();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.lblTrangThaiDuLieu = new System.Windows.Forms.Label();

            this.dtTuNgay =
                new System.Windows.Forms.DateTimePicker();
            this.dtDenNgay =
                new System.Windows.Forms.DateTimePicker();

            this.cboLoaiKham =
                new System.Windows.Forms.ComboBox();
            this.cboKhoa =
                new System.Windows.Forms.ComboBox();
            this.cboTrangThai =
                new System.Windows.Forms.ComboBox();
            this.cboNhomThongKe =
                new System.Windows.Forms.ComboBox();

            this.btnThongKe =
                new System.Windows.Forms.Button();
            this.btnLamMoi =
                new System.Windows.Forms.Button();
            this.btnXuatBang =
                new System.Windows.Forms.Button();
            this.btnXemChiTiet =
                new System.Windows.Forms.Button();

            this.dgvTongQuan =
                new System.Windows.Forms.DataGridView();
            this.dgvChiTiet =
                new System.Windows.Forms.DataGridView();

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvTongQuan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)
                (this.dgvChiTiet)).BeginInit();

            this.SuspendLayout();

            // ========================================
            // CẤU HÌNH FORM
            // ========================================

            this.Name = "FrmThongKe";
            this.Text =
                "Thống kê - Quản lý hồ sơ bệnh án";

            this.ClientSize =
                new System.Drawing.Size(1100, 770);

            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;

            this.BackColor =
                System.Drawing.Color.FromArgb(245, 245, 245);

            this.Font =
                new System.Drawing.Font("Segoe UI", 10F);

            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;

            this.FormBorderStyle =
                System.Windows.Forms.FormBorderStyle.FixedSingle;

            this.MaximizeBox = false;

            // ========================================
            // TỪ NGÀY
            // ========================================

            this.lblTuNgay.Name = "lblTuNgay";
            this.lblTuNgay.Text = "Từ ngày:";

            this.lblTuNgay.Location =
                new System.Drawing.Point(40, 45);

            this.lblTuNgay.Size =
                new System.Drawing.Size(100, 27);

            this.dtTuNgay.Name = "dtTuNgay";

            this.dtTuNgay.Location =
                new System.Drawing.Point(145, 42);

            this.dtTuNgay.Size =
                new System.Drawing.Size(165, 27);

            this.dtTuNgay.Format =
                System.Windows.Forms.DateTimePickerFormat.Custom;

            this.dtTuNgay.CustomFormat = "dd/MM/yyyy";

            // ========================================
            // ĐẾN NGÀY
            // ========================================

            this.lblDenNgay.Name = "lblDenNgay";
            this.lblDenNgay.Text = "Đến ngày:";

            this.lblDenNgay.Location =
                new System.Drawing.Point(340, 45);

            this.lblDenNgay.Size =
                new System.Drawing.Size(110, 27);

            this.dtDenNgay.Name = "dtDenNgay";

            this.dtDenNgay.Location =
                new System.Drawing.Point(455, 42);

            this.dtDenNgay.Size =
                new System.Drawing.Size(165, 27);

            this.dtDenNgay.Format =
                System.Windows.Forms.DateTimePickerFormat.Custom;

            this.dtDenNgay.CustomFormat = "dd/MM/yyyy";

            // ========================================
            // LOẠI KHÁM
            // ========================================

            this.lblLoaiKham.Name = "lblLoaiKham";
            this.lblLoaiKham.Text = "Loại khám:";

            this.lblLoaiKham.Location =
                new System.Drawing.Point(40, 90);

            this.lblLoaiKham.Size =
                new System.Drawing.Size(105, 27);

            this.cboLoaiKham.Name = "cboLoaiKham";

            this.cboLoaiKham.Location =
                new System.Drawing.Point(145, 87);

            this.cboLoaiKham.Size =
                new System.Drawing.Size(165, 27);

            this.cboLoaiKham.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            // ========================================
            // KHOA ĐIỀU TRỊ
            // ========================================

            this.lblKhoa.Name = "lblKhoa";
            this.lblKhoa.Text = "Khoa điều trị:";

            this.lblKhoa.Location =
                new System.Drawing.Point(340, 90);

            this.lblKhoa.Size =
                new System.Drawing.Size(110, 27);

            this.cboKhoa.Name = "cboKhoa";

            this.cboKhoa.Location =
                new System.Drawing.Point(455, 87);

            this.cboKhoa.Size =
                new System.Drawing.Size(230, 27);

            this.cboKhoa.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            // ========================================
            // TRẠNG THÁI
            // ========================================

            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Text = "Trạng thái:";

            this.lblTrangThai.Location =
                new System.Drawing.Point(40, 135);

            this.lblTrangThai.Size =
                new System.Drawing.Size(105, 27);

            this.cboTrangThai.Name = "cboTrangThai";

            this.cboTrangThai.Location =
                new System.Drawing.Point(145, 132);

            this.cboTrangThai.Size =
                new System.Drawing.Size(165, 27);

            this.cboTrangThai.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            // ========================================
            // NHÓM THỐNG KÊ
            // ========================================

            this.lblNhomThongKe.Name = "lblNhomThongKe";
            this.lblNhomThongKe.Text = "Nhóm thống kê:";

            this.lblNhomThongKe.Location =
                new System.Drawing.Point(340, 135);

            this.lblNhomThongKe.Size =
                new System.Drawing.Size(115, 27);

            this.cboNhomThongKe.Name = "cboNhomThongKe";

            this.cboNhomThongKe.Location =
                new System.Drawing.Point(455, 132);

            this.cboNhomThongKe.Size =
                new System.Drawing.Size(230, 27);

            this.cboNhomThongKe.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            // ========================================
            // NÚT THỐNG KÊ
            // ========================================

            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Text = "Thống kê";

            this.btnThongKe.Location =
                new System.Drawing.Point(420, 210);

            this.btnThongKe.Size =
                new System.Drawing.Size(165, 40);

            this.btnThongKe.Click +=
                new System.EventHandler(
                    this.btnThongKe_Click);

            // ========================================
            // NÚT LÀM MỚI
            // ========================================

            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Text = "Làm mới";

            this.btnLamMoi.Location =
                new System.Drawing.Point(605, 210);

            this.btnLamMoi.Size =
                new System.Drawing.Size(165, 40);

            this.btnLamMoi.Click +=
                new System.EventHandler(
                    this.btnLamMoi_Click);

            // ========================================
            // NÚT XUẤT BẢNG
            // ========================================

            this.btnXuatBang.Name = "btnXuatBang";
            this.btnXuatBang.Text = "Xuất bảng";

            this.btnXuatBang.Location =
                new System.Drawing.Point(790, 210);

            this.btnXuatBang.Size =
                new System.Drawing.Size(165, 40);

            this.btnXuatBang.Click +=
                new System.EventHandler(
                    this.btnXuatBang_Click);

            // ========================================
            // TIÊU CHÍ THỐNG KÊ
            // ========================================

            this.lblTieuChi.Name = "lblTieuChi";

            this.lblTieuChi.Text =
                "Tiêu chí: Thống kê theo ngày giờ khám, " +
                "mỗi phiếu khám được đếm một lần.";

            this.lblTieuChi.Location =
                new System.Drawing.Point(40, 290);

            this.lblTieuChi.Size =
                new System.Drawing.Size(740, 27);

            // ========================================
            // TRẠNG THÁI TẢI DỮ LIỆU
            // ========================================

            this.lblTrangThaiDuLieu.Name =
                "lblTrangThaiDuLieu";

            this.lblTrangThaiDuLieu.Text =
                "Chưa tải dữ liệu";

            this.lblTrangThaiDuLieu.TextAlign =
                System.Drawing.ContentAlignment.MiddleRight;

            this.lblTrangThaiDuLieu.Location =
                new System.Drawing.Point(790, 290);

            this.lblTrangThaiDuLieu.Size =
                new System.Drawing.Size(270, 27);

            this.lblTrangThaiDuLieu.Font =
                new System.Drawing.Font("Segoe UI", 8F);

            // ========================================
            // BẢNG THỐNG KÊ TỔNG QUAN
            // ========================================

            this.dgvTongQuan.Name = "dgvTongQuan";

            this.dgvTongQuan.Location =
                new System.Drawing.Point(40, 330);

            this.dgvTongQuan.Size =
                new System.Drawing.Size(1020, 110);

            this.dgvTongQuan.BackgroundColor =
                System.Drawing.Color.White;

            this.dgvTongQuan.ReadOnly = true;
            this.dgvTongQuan.RowHeadersVisible = false;

            this.dgvTongQuan.AllowUserToAddRows = false;
            this.dgvTongQuan.AllowUserToDeleteRows = false;

            this.dgvTongQuan.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            this.dgvTongQuan.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // ========================================
            // TIÊU ĐỀ BẢNG CHI TIẾT
            // ========================================

            this.lblBangChiTiet.Name = "lblBangChiTiet";

            this.lblBangChiTiet.Text =
                "BẢNG THỐNG KÊ THEO ĐIỀU KIỆN ĐÃ CHỌN";

            this.lblBangChiTiet.Location =
                new System.Drawing.Point(40, 475);

            this.lblBangChiTiet.Size =
                new System.Drawing.Size(650, 30);

            this.lblBangChiTiet.Font =
                new System.Drawing.Font(
                    "Segoe UI",
                    10F,
                    System.Drawing.FontStyle.Bold);

            // ========================================
            // BẢNG THỐNG KÊ CHI TIẾT
            // ========================================

            this.dgvChiTiet.Name = "dgvChiTiet";

            this.dgvChiTiet.Location =
                new System.Drawing.Point(40, 515);

            this.dgvChiTiet.Size =
                new System.Drawing.Size(1020, 175);

            this.dgvChiTiet.BackgroundColor =
                System.Drawing.Color.White;

            this.dgvChiTiet.ReadOnly = true;
            this.dgvChiTiet.RowHeadersVisible = false;

            this.dgvChiTiet.AllowUserToAddRows = false;
            this.dgvChiTiet.AllowUserToDeleteRows = false;

            this.dgvChiTiet.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            this.dgvChiTiet.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // ========================================
            // NÚT XEM CHI TIẾT
            // ========================================

            this.btnXemChiTiet.Name = "btnXemChiTiet";

            this.btnXemChiTiet.Text =
                "Xem chi tiết nhóm đã chọn";

            this.btnXemChiTiet.Location =
                new System.Drawing.Point(785, 705);

            this.btnXemChiTiet.Size =
                new System.Drawing.Size(275, 38);

            this.btnXemChiTiet.Click +=
                new System.EventHandler(
                    this.btnXemChiTiet_Click);

            // ========================================
            // GHI CHÚ
            // ========================================

            this.lblGhiChu.Name = "lblGhiChu";

            this.lblGhiChu.Text =
                "Dữ liệu được lấy trực tiếp từ SQL Server.";

            this.lblGhiChu.Location =
                new System.Drawing.Point(40, 744);

            this.lblGhiChu.Size =
                new System.Drawing.Size(790, 22);

            this.lblGhiChu.Font =
                new System.Drawing.Font("Segoe UI", 8F);

            // ========================================
            // THÊM CONTROL VÀO FORM
            // ========================================

            this.Controls.Add(this.lblTuNgay);
            this.Controls.Add(this.lblDenNgay);
            this.Controls.Add(this.lblLoaiKham);
            this.Controls.Add(this.lblKhoa);
            this.Controls.Add(this.lblTrangThai);
            this.Controls.Add(this.lblNhomThongKe);

            this.Controls.Add(this.dtTuNgay);
            this.Controls.Add(this.dtDenNgay);

            this.Controls.Add(this.cboLoaiKham);
            this.Controls.Add(this.cboKhoa);
            this.Controls.Add(this.cboTrangThai);
            this.Controls.Add(this.cboNhomThongKe);

            this.Controls.Add(this.btnThongKe);
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.btnXuatBang);

            this.Controls.Add(this.lblTieuChi);
            this.Controls.Add(this.lblTrangThaiDuLieu);
            this.Controls.Add(this.dgvTongQuan);

            this.Controls.Add(this.lblBangChiTiet);
            this.Controls.Add(this.dgvChiTiet);
            this.Controls.Add(this.btnXemChiTiet);

            this.Controls.Add(this.lblGhiChu);

            // ========================================
            // SỰ KIỆN LOAD
            // ========================================

            this.Load +=
                new System.EventHandler(
                    this.FrmThongKe_Load);

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvTongQuan)).EndInit();

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvChiTiet)).EndInit();

            this.ResumeLayout(false);
        }
    }
}
