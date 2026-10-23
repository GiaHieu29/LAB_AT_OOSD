
namespace QuanLyHoSoBenhAn
{
    partial class FrmBenhNhan
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.GroupBox grpThongTin;
        private System.Windows.Forms.GroupBox grpDanhSach;

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblMa;
        private System.Windows.Forms.Label lblHoTen;
        private System.Windows.Forms.Label lblNgaySinh;
        private System.Windows.Forms.Label lblGioiTinh;
        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.Label lblDienThoai;
        private System.Windows.Forms.Label lblAnh;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.Label lblSoLuong;

        private System.Windows.Forms.TextBox txtMaBenhNhan;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.TextBox txtDienThoai;
        private System.Windows.Forms.TextBox txtTimKiem;

        private System.Windows.Forms.DateTimePicker dtNgaySinh;
        private System.Windows.Forms.ComboBox cboGioiTinh;
        private System.Windows.Forms.PictureBox picAnh;

        private System.Windows.Forms.Button btnChonAnh;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnDong;

        private System.Windows.Forms.DataGridView dgvBenhNhan;

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
            this.grpThongTin =
                new System.Windows.Forms.GroupBox();
            this.grpDanhSach =
                new System.Windows.Forms.GroupBox();

            this.lblTieuDe =
                new System.Windows.Forms.Label();
            this.lblMa =
                new System.Windows.Forms.Label();
            this.lblHoTen =
                new System.Windows.Forms.Label();
            this.lblNgaySinh =
                new System.Windows.Forms.Label();
            this.lblGioiTinh =
                new System.Windows.Forms.Label();
            this.lblDiaChi =
                new System.Windows.Forms.Label();
            this.lblDienThoai =
                new System.Windows.Forms.Label();
            this.lblAnh =
                new System.Windows.Forms.Label();
            this.lblTimKiem =
                new System.Windows.Forms.Label();
            this.lblSoLuong =
                new System.Windows.Forms.Label();

            this.txtMaBenhNhan =
                new System.Windows.Forms.TextBox();
            this.txtHoTen =
                new System.Windows.Forms.TextBox();
            this.txtDiaChi =
                new System.Windows.Forms.TextBox();
            this.txtDienThoai =
                new System.Windows.Forms.TextBox();
            this.txtTimKiem =
                new System.Windows.Forms.TextBox();

            this.dtNgaySinh =
                new System.Windows.Forms.DateTimePicker();
            this.cboGioiTinh =
                new System.Windows.Forms.ComboBox();

            this.picAnh =
                new System.Windows.Forms.PictureBox();

            this.btnChonAnh =
                new System.Windows.Forms.Button();
            this.btnThem =
                new System.Windows.Forms.Button();
            this.btnSua =
                new System.Windows.Forms.Button();
            this.btnXoa =
                new System.Windows.Forms.Button();
            this.btnLamMoi =
                new System.Windows.Forms.Button();
            this.btnTimKiem =
                new System.Windows.Forms.Button();
            this.btnDong =
                new System.Windows.Forms.Button();

            this.dgvBenhNhan =
                new System.Windows.Forms.DataGridView();

            ((System.ComponentModel.ISupportInitialize)
                (this.picAnh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)
                (this.dgvBenhNhan)).BeginInit();

            this.grpThongTin.SuspendLayout();
            this.grpDanhSach.SuspendLayout();
            this.SuspendLayout();

            // ==========================================
            // FORM
            // ==========================================

            this.Name = "FrmBenhNhan";
            this.Text = "Quản lý bệnh nhân";

            this.ClientSize =
                new System.Drawing.Size(1120, 780);

            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;

            this.FormBorderStyle =
                System.Windows.Forms.FormBorderStyle.FixedSingle;

            this.MaximizeBox = false;

            this.BackColor =
                System.Drawing.Color.FromArgb(245, 245, 245);

            this.Font =
                new System.Drawing.Font("Segoe UI", 10F);

            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;

            // ==========================================
            // TIÊU ĐỀ
            // ==========================================

            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Text = "QUẢN LÝ BỆNH NHÂN";
            this.lblTieuDe.Font =
                new System.Drawing.Font(
                    "Segoe UI", 17F,
                    System.Drawing.FontStyle.Bold);

            this.lblTieuDe.ForeColor =
                System.Drawing.Color.FromArgb(35, 75, 125);

            this.lblTieuDe.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;

            this.lblTieuDe.Location =
                new System.Drawing.Point(20, 12);

            this.lblTieuDe.Size =
                new System.Drawing.Size(1080, 43);

            // ==========================================
            // GROUP THÔNG TIN
            // ==========================================

            this.grpThongTin.Name = "grpThongTin";
            this.grpThongTin.Text = "Thông tin bệnh nhân";

            this.grpThongTin.Location =
                new System.Drawing.Point(20, 65);

            this.grpThongTin.Size =
                new System.Drawing.Size(1080, 280);

            this.grpThongTin.BackColor =
                System.Drawing.Color.FromArgb(250, 250, 250);

            // MÃ BỆNH NHÂN

            this.lblMa.Text = "Mã bệnh nhân:";
            this.lblMa.Location =
                new System.Drawing.Point(25, 42);
            this.lblMa.Size =
                new System.Drawing.Size(120, 27);

            this.txtMaBenhNhan.Name = "txtMaBenhNhan";
            this.txtMaBenhNhan.Location =
                new System.Drawing.Point(150, 39);
            this.txtMaBenhNhan.Size =
                new System.Drawing.Size(250, 27);
            this.txtMaBenhNhan.MaxLength = 20;

            // HỌ TÊN

            this.lblHoTen.Text = "Họ và tên:";
            this.lblHoTen.Location =
                new System.Drawing.Point(25, 85);
            this.lblHoTen.Size =
                new System.Drawing.Size(120, 27);

            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Location =
                new System.Drawing.Point(150, 82);
            this.txtHoTen.Size =
                new System.Drawing.Size(250, 27);
            this.txtHoTen.MaxLength = 120;

            // NGÀY SINH

            this.lblNgaySinh.Text = "Ngày sinh:";
            this.lblNgaySinh.Location =
                new System.Drawing.Point(25, 128);
            this.lblNgaySinh.Size =
                new System.Drawing.Size(120, 27);

            this.dtNgaySinh.Name = "dtNgaySinh";
            this.dtNgaySinh.Location =
                new System.Drawing.Point(150, 125);
            this.dtNgaySinh.Size =
                new System.Drawing.Size(250, 27);

            this.dtNgaySinh.Format =
                System.Windows.Forms.DateTimePickerFormat.Custom;

            this.dtNgaySinh.CustomFormat = "dd/MM/yyyy";

            // GIỚI TÍNH

            this.lblGioiTinh.Text = "Giới tính:";
            this.lblGioiTinh.Location =
                new System.Drawing.Point(25, 171);
            this.lblGioiTinh.Size =
                new System.Drawing.Size(120, 27);

            this.cboGioiTinh.Name = "cboGioiTinh";
            this.cboGioiTinh.Location =
                new System.Drawing.Point(150, 168);
            this.cboGioiTinh.Size =
                new System.Drawing.Size(250, 27);

            this.cboGioiTinh.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            // ĐỊA CHỈ

            this.lblDiaChi.Text = "Địa chỉ:";
            this.lblDiaChi.Location =
                new System.Drawing.Point(430, 42);
            this.lblDiaChi.Size =
                new System.Drawing.Size(95, 27);

            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Location =
                new System.Drawing.Point(530, 39);
            this.txtDiaChi.Size =
                new System.Drawing.Size(310, 27);
            this.txtDiaChi.MaxLength = 250;

            // ĐIỆN THOẠI

            this.lblDienThoai.Text = "Điện thoại:";
            this.lblDienThoai.Location =
                new System.Drawing.Point(430, 85);
            this.lblDienThoai.Size =
                new System.Drawing.Size(95, 27);

            this.txtDienThoai.Name = "txtDienThoai";
            this.txtDienThoai.Location =
                new System.Drawing.Point(530, 82);
            this.txtDienThoai.Size =
                new System.Drawing.Size(310, 27);
            this.txtDienThoai.MaxLength = 20;

            // ẢNH 3x4

            this.picAnh.Name = "picAnh";
            this.picAnh.Location =
                new System.Drawing.Point(875, 35);
            this.picAnh.Size =
                new System.Drawing.Size(165, 180);

            this.picAnh.BorderStyle =
                System.Windows.Forms.BorderStyle.FixedSingle;

            this.picAnh.BackColor =
                System.Drawing.Color.White;

            this.picAnh.SizeMode =
                System.Windows.Forms.PictureBoxSizeMode.Zoom;

            this.lblAnh.Name = "lblAnh";
            this.lblAnh.Text = "Chưa chọn ảnh";
            this.lblAnh.Location =
                new System.Drawing.Point(855, 218);
            this.lblAnh.Size =
                new System.Drawing.Size(205, 22);

            this.lblAnh.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;

            this.lblAnh.AutoEllipsis = true;

            this.btnChonAnh.Name = "btnChonAnh";
            this.btnChonAnh.Text = "Chọn ảnh 3x4";

            this.btnChonAnh.Location =
                new System.Drawing.Point(530, 125);

            this.btnChonAnh.Size =
                new System.Drawing.Size(145, 35);

            this.btnChonAnh.Click +=
                new System.EventHandler(
                    this.btnChonAnh_Click);

            // THÊM CONTROL VÀO GROUP

            this.grpThongTin.Controls.Add(this.lblMa);
            this.grpThongTin.Controls.Add(this.txtMaBenhNhan);

            this.grpThongTin.Controls.Add(this.lblHoTen);
            this.grpThongTin.Controls.Add(this.txtHoTen);

            this.grpThongTin.Controls.Add(this.lblNgaySinh);
            this.grpThongTin.Controls.Add(this.dtNgaySinh);

            this.grpThongTin.Controls.Add(this.lblGioiTinh);
            this.grpThongTin.Controls.Add(this.cboGioiTinh);

            this.grpThongTin.Controls.Add(this.lblDiaChi);
            this.grpThongTin.Controls.Add(this.txtDiaChi);

            this.grpThongTin.Controls.Add(this.lblDienThoai);
            this.grpThongTin.Controls.Add(this.txtDienThoai);

            this.grpThongTin.Controls.Add(this.picAnh);
            this.grpThongTin.Controls.Add(this.lblAnh);
            this.grpThongTin.Controls.Add(this.btnChonAnh);

            // ==========================================
            // CÁC NÚT CHỨC NĂNG
            // ==========================================

            this.btnThem.Name = "btnThem";
            this.btnThem.Text = "Thêm";
            this.btnThem.Location =
                new System.Drawing.Point(215, 360);
            this.btnThem.Size =
                new System.Drawing.Size(125, 38);

            this.btnThem.Click +=
                new System.EventHandler(this.btnThem_Click);

            this.btnSua.Name = "btnSua";
            this.btnSua.Text = "Sửa";
            this.btnSua.Location =
                new System.Drawing.Point(355, 360);
            this.btnSua.Size =
                new System.Drawing.Size(125, 38);

            this.btnSua.Click +=
                new System.EventHandler(this.btnSua_Click);

            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Text = "Xóa";
            this.btnXoa.Location =
                new System.Drawing.Point(495, 360);
            this.btnXoa.Size =
                new System.Drawing.Size(125, 38);

            this.btnXoa.Click +=
                new System.EventHandler(this.btnXoa_Click);

            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.Location =
                new System.Drawing.Point(635, 360);
            this.btnLamMoi.Size =
                new System.Drawing.Size(125, 38);

            this.btnLamMoi.Click +=
                new System.EventHandler(this.btnLamMoi_Click);

            this.btnDong.Name = "btnDong";
            this.btnDong.Text = "Đóng";
            this.btnDong.Location =
                new System.Drawing.Point(775, 360);
            this.btnDong.Size =
                new System.Drawing.Size(125, 38);

            this.btnDong.Click +=
                new System.EventHandler(this.btnDong_Click);

            // ==========================================
            // GROUP DANH SÁCH
            // ==========================================

            this.grpDanhSach.Name = "grpDanhSach";
            this.grpDanhSach.Text = "Danh sách bệnh nhân";

            this.grpDanhSach.Location =
                new System.Drawing.Point(20, 420);

            this.grpDanhSach.Size =
                new System.Drawing.Size(1080, 340);

            this.grpDanhSach.BackColor =
                System.Drawing.Color.FromArgb(250, 250, 250);

            // TÌM KIẾM

            this.lblTimKiem.Text = "Tìm kiếm:";
            this.lblTimKiem.Location =
                new System.Drawing.Point(20, 35);
            this.lblTimKiem.Size =
                new System.Drawing.Size(95, 27);

            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Location =
                new System.Drawing.Point(115, 32);
            this.txtTimKiem.Size =
                new System.Drawing.Size(350, 27);

            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.Location =
                new System.Drawing.Point(480, 30);
            this.btnTimKiem.Size =
                new System.Drawing.Size(120, 34);

            this.btnTimKiem.Click +=
                new System.EventHandler(
                    this.btnTimKiem_Click);

            // SỐ LƯỢNG

            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Text = "Tổng số bệnh nhân: 0";
            this.lblSoLuong.Location =
                new System.Drawing.Point(735, 35);
            this.lblSoLuong.Size =
                new System.Drawing.Size(310, 27);

            this.lblSoLuong.TextAlign =
                System.Drawing.ContentAlignment.MiddleRight;

            // DATAGRIDVIEW

            this.dgvBenhNhan.Name = "dgvBenhNhan";
            this.dgvBenhNhan.Location =
                new System.Drawing.Point(20, 78);
            this.dgvBenhNhan.Size =
                new System.Drawing.Size(1040, 245);

            this.dgvBenhNhan.ReadOnly = true;
            this.dgvBenhNhan.AllowUserToAddRows = false;
            this.dgvBenhNhan.AllowUserToDeleteRows = false;

            this.dgvBenhNhan.RowHeadersVisible = false;
            this.dgvBenhNhan.MultiSelect = false;

            this.dgvBenhNhan.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            this.dgvBenhNhan.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.dgvBenhNhan.BackgroundColor =
                System.Drawing.Color.White;

            this.dgvBenhNhan.EnableHeadersVisualStyles = false;

            this.dgvBenhNhan.SelectionChanged +=
                new System.EventHandler(
                    this.dgvBenhNhan_SelectionChanged);

            this.grpDanhSach.Controls.Add(this.lblTimKiem);
            this.grpDanhSach.Controls.Add(this.txtTimKiem);
            this.grpDanhSach.Controls.Add(this.btnTimKiem);
            this.grpDanhSach.Controls.Add(this.lblSoLuong);
            this.grpDanhSach.Controls.Add(this.dgvBenhNhan);

            // ==========================================
            // THÊM CONTROL VÀO FORM
            // ==========================================

            this.Controls.Add(this.lblTieuDe);
            this.Controls.Add(this.grpThongTin);

            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.btnDong);

            this.Controls.Add(this.grpDanhSach);

            // SỰ KIỆN LOAD
            this.Load += new System.EventHandler(
                this.FrmBenhNhan_Load);

            ((System.ComponentModel.ISupportInitialize)
                (this.picAnh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)
                (this.dgvBenhNhan)).EndInit();

            this.grpThongTin.ResumeLayout(false);
            this.grpThongTin.PerformLayout();

            this.grpDanhSach.ResumeLayout(false);
            this.grpDanhSach.PerformLayout();

            this.ResumeLayout(false);
        }
    }
}
