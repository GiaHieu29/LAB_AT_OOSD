
namespace QuanLyHoSoBenhAn
{
    partial class FrmHoSoBenhAn
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.GroupBox grpThongTin;
        private System.Windows.Forms.GroupBox grpDanhSach;

        private System.Windows.Forms.TextBox txtMaHoSo;
        private System.Windows.Forms.TextBox txtMaBenhNhan;
        private System.Windows.Forms.TextBox txtTenBenhNhan;
        private System.Windows.Forms.TextBox txtMaPhieuDK;
        private System.Windows.Forms.TextBox txtTinhTrang;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.TextBox txtTimKiem;

        private System.Windows.Forms.ComboBox cboBenhNhan;
        private System.Windows.Forms.ComboBox cboTrangThai;

        private System.Windows.Forms.DateTimePicker dtNgayLap;

        private System.Windows.Forms.Button btnTaoHoSo;
        private System.Windows.Forms.Button btnCapNhat;
        private System.Windows.Forms.Button btnLichSu;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnDong;

        private System.Windows.Forms.DataGridView dgvHoSo;
        private System.Windows.Forms.Label lblSoLuong;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.grpThongTin = new System.Windows.Forms.GroupBox();
            this.grpDanhSach = new System.Windows.Forms.GroupBox();

            this.txtMaHoSo = new System.Windows.Forms.TextBox();
            this.txtMaBenhNhan = new System.Windows.Forms.TextBox();
            this.txtTenBenhNhan = new System.Windows.Forms.TextBox();
            this.txtMaPhieuDK = new System.Windows.Forms.TextBox();
            this.txtTinhTrang = new System.Windows.Forms.TextBox();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.txtTimKiem = new System.Windows.Forms.TextBox();

            this.cboBenhNhan = new System.Windows.Forms.ComboBox();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();

            this.dtNgayLap =
                new System.Windows.Forms.DateTimePicker();

            this.btnTaoHoSo = new System.Windows.Forms.Button();
            this.btnCapNhat = new System.Windows.Forms.Button();
            this.btnLichSu = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();

            this.dgvHoSo = new System.Windows.Forms.DataGridView();
            this.lblSoLuong = new System.Windows.Forms.Label();

            System.Windows.Forms.Label lblTieuDe =
                new System.Windows.Forms.Label();

            System.Windows.Forms.Label lblMa =
                new System.Windows.Forms.Label();

            System.Windows.Forms.Label lblBN =
                new System.Windows.Forms.Label();

            System.Windows.Forms.Label lblMaBN =
                new System.Windows.Forms.Label();

            System.Windows.Forms.Label lblTenBN =
                new System.Windows.Forms.Label();

            System.Windows.Forms.Label lblPhieu =
                new System.Windows.Forms.Label();

            System.Windows.Forms.Label lblNgay =
                new System.Windows.Forms.Label();

            System.Windows.Forms.Label lblTrangThai =
                new System.Windows.Forms.Label();

            System.Windows.Forms.Label lblTinhTrang =
                new System.Windows.Forms.Label();

            System.Windows.Forms.Label lblGhiChu =
                new System.Windows.Forms.Label();

            System.Windows.Forms.Label lblTim =
                new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvHoSo)).BeginInit();

            this.grpThongTin.SuspendLayout();
            this.grpDanhSach.SuspendLayout();
            this.SuspendLayout();

            // FORM
            this.Name = "FrmHoSoBenhAn";
            this.Text = "Quản lý hồ sơ bệnh án";
            this.ClientSize = new System.Drawing.Size(1120, 790);

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

            // TIÊU ĐỀ
            lblTieuDe.Text = "QUẢN LÝ HỒ SƠ BỆNH ÁN";
            lblTieuDe.Font =
                new System.Drawing.Font(
                    "Segoe UI", 17F,
                    System.Drawing.FontStyle.Bold);

            lblTieuDe.ForeColor =
                System.Drawing.Color.FromArgb(35, 75, 125);

            lblTieuDe.Location = new System.Drawing.Point(20, 10);
            lblTieuDe.Size = new System.Drawing.Size(1080, 45);

            lblTieuDe.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;

            // GROUP THÔNG TIN
            this.grpThongTin.Text = "Thông tin hồ sơ bệnh án";
            this.grpThongTin.Location =
                new System.Drawing.Point(20, 65);

            this.grpThongTin.Size =
                new System.Drawing.Size(1080, 300);

            // MÃ HỒ SƠ
            lblMa.Text = "Mã hồ sơ:";
            lblMa.Location = new System.Drawing.Point(20, 38);
            lblMa.Size = new System.Drawing.Size(125, 28);

            this.txtMaHoSo.Location =
                new System.Drawing.Point(150, 35);
            this.txtMaHoSo.Size =
                new System.Drawing.Size(280, 27);
            this.txtMaHoSo.MaxLength = 20;

            // CHỌN BỆNH NHÂN
            lblBN.Text = "Chọn bệnh nhân:";
            lblBN.Location = new System.Drawing.Point(20, 80);
            lblBN.Size = new System.Drawing.Size(130, 28);

            this.cboBenhNhan.Location =
                new System.Drawing.Point(150, 77);
            this.cboBenhNhan.Size =
                new System.Drawing.Size(280, 27);

            this.cboBenhNhan.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.cboBenhNhan.SelectedIndexChanged +=
                new System.EventHandler(
                    this.cboBenhNhan_SelectedIndexChanged);

            // MÃ BỆNH NHÂN
            lblMaBN.Text = "Mã bệnh nhân:";
            lblMaBN.Location =
                new System.Drawing.Point(460, 38);
            lblMaBN.Size = new System.Drawing.Size(140, 28);

            this.txtMaBenhNhan.Location =
                new System.Drawing.Point(605, 35);
            this.txtMaBenhNhan.Size =
                new System.Drawing.Size(220, 27);
            this.txtMaBenhNhan.ReadOnly = true;

            // TÊN BỆNH NHÂN
            lblTenBN.Text = "Tên bệnh nhân:";
            lblTenBN.Location =
                new System.Drawing.Point(460, 80);
            lblTenBN.Size = new System.Drawing.Size(140, 28);

            this.txtTenBenhNhan.Location =
                new System.Drawing.Point(605, 77);
            this.txtTenBenhNhan.Size =
                new System.Drawing.Size(430, 27);
            this.txtTenBenhNhan.ReadOnly = true;

            // MÃ PHIẾU ĐĂNG KÝ
            lblPhieu.Text = "Phiếu đăng ký:";
            lblPhieu.Location =
                new System.Drawing.Point(20, 122);
            lblPhieu.Size = new System.Drawing.Size(130, 28);

            this.txtMaPhieuDK.Location =
                new System.Drawing.Point(150, 119);
            this.txtMaPhieuDK.Size =
                new System.Drawing.Size(280, 27);
            this.txtMaPhieuDK.ReadOnly = true;

            // NGÀY LẬP
            lblNgay.Text = "Ngày lập:";
            lblNgay.Location =
                new System.Drawing.Point(460, 122);
            lblNgay.Size = new System.Drawing.Size(140, 28);

            this.dtNgayLap.Location =
                new System.Drawing.Point(605, 119);
            this.dtNgayLap.Size =
                new System.Drawing.Size(220, 27);
            this.dtNgayLap.Format =
                System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtNgayLap.CustomFormat = "dd/MM/yyyy";

            this.dtNgayLap.ValueChanged +=
                new System.EventHandler(
                    this.dtNgayLap_ValueChanged);

            // TRẠNG THÁI
            lblTrangThai.Text = "Trạng thái:";
            lblTrangThai.Location =
                new System.Drawing.Point(20, 165);
            lblTrangThai.Size = new System.Drawing.Size(130, 28);

            this.cboTrangThai.Location =
                new System.Drawing.Point(150, 162);
            this.cboTrangThai.Size =
                new System.Drawing.Size(280, 27);

            this.cboTrangThai.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            // TÌNH TRẠNG SỨC KHỎE
            lblTinhTrang.Text = "Tình trạng sức khỏe:";
            lblTinhTrang.Location =
                new System.Drawing.Point(460, 165);
            lblTinhTrang.Size = new System.Drawing.Size(170, 28);

            this.txtTinhTrang.Location =
                new System.Drawing.Point(635, 162);
            this.txtTinhTrang.Size =
                new System.Drawing.Size(400, 52);
            this.txtTinhTrang.Multiline = true;
            this.txtTinhTrang.MaxLength = 1000;

            // GHI CHÚ
            lblGhiChu.Text = "Ghi chú:";
            lblGhiChu.Location =
                new System.Drawing.Point(20, 225);
            lblGhiChu.Size = new System.Drawing.Size(125, 28);

            this.txtGhiChu.Location =
                new System.Drawing.Point(150, 222);
            this.txtGhiChu.Size =
                new System.Drawing.Size(885, 55);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.MaxLength = 1000;

            this.grpThongTin.Controls.Add(lblMa);
            this.grpThongTin.Controls.Add(txtMaHoSo);

            this.grpThongTin.Controls.Add(lblBN);
            this.grpThongTin.Controls.Add(cboBenhNhan);

            this.grpThongTin.Controls.Add(lblMaBN);
            this.grpThongTin.Controls.Add(txtMaBenhNhan);

            this.grpThongTin.Controls.Add(lblTenBN);
            this.grpThongTin.Controls.Add(txtTenBenhNhan);

            this.grpThongTin.Controls.Add(lblPhieu);
            this.grpThongTin.Controls.Add(txtMaPhieuDK);

            this.grpThongTin.Controls.Add(lblNgay);
            this.grpThongTin.Controls.Add(dtNgayLap);

            this.grpThongTin.Controls.Add(lblTrangThai);
            this.grpThongTin.Controls.Add(cboTrangThai);

            this.grpThongTin.Controls.Add(lblTinhTrang);
            this.grpThongTin.Controls.Add(txtTinhTrang);

            this.grpThongTin.Controls.Add(lblGhiChu);
            this.grpThongTin.Controls.Add(txtGhiChu);

            // BUTTON
            this.btnTaoHoSo.Text = "Tạo hồ sơ";
            this.btnTaoHoSo.Location =
                new System.Drawing.Point(210, 385);
            this.btnTaoHoSo.Size =
                new System.Drawing.Size(140, 40);

            this.btnTaoHoSo.Click +=
                new System.EventHandler(
                    this.btnTaoHoSo_Click);

            this.btnCapNhat.Text = "Cập nhật";
            this.btnCapNhat.Location =
                new System.Drawing.Point(365, 385);
            this.btnCapNhat.Size =
                new System.Drawing.Size(140, 40);

            this.btnCapNhat.Click +=
                new System.EventHandler(
                    this.btnCapNhat_Click);

            this.btnLichSu.Text = "Lịch sử khám";
            this.btnLichSu.Location =
                new System.Drawing.Point(520, 385);
            this.btnLichSu.Size =
                new System.Drawing.Size(140, 40);

            this.btnLichSu.Click +=
                new System.EventHandler(
                    this.btnLichSu_Click);

            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.Location =
                new System.Drawing.Point(675, 385);
            this.btnLamMoi.Size =
                new System.Drawing.Size(140, 40);

            this.btnLamMoi.Click +=
                new System.EventHandler(
                    this.btnLamMoi_Click);

            this.btnDong.Text = "Đóng";
            this.btnDong.Location =
                new System.Drawing.Point(830, 385);
            this.btnDong.Size =
                new System.Drawing.Size(140, 40);

            this.btnDong.Click +=
                new System.EventHandler(
                    this.btnDong_Click);

            // GROUP DANH SÁCH
            this.grpDanhSach.Text = "Danh sách hồ sơ bệnh án";
            this.grpDanhSach.Location =
                new System.Drawing.Point(20, 445);
            this.grpDanhSach.Size =
                new System.Drawing.Size(1080, 325);

            // TÌM KIẾM
            lblTim.Text = "Tìm kiếm:";
            lblTim.Location =
                new System.Drawing.Point(20, 35);
            lblTim.Size =
                new System.Drawing.Size(100, 27);

            this.txtTimKiem.Location =
                new System.Drawing.Point(120, 32);
            this.txtTimKiem.Size =
                new System.Drawing.Size(320, 27);

            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.Location =
                new System.Drawing.Point(455, 30);
            this.btnTimKiem.Size =
                new System.Drawing.Size(125, 34);

            this.btnTimKiem.Click +=
                new System.EventHandler(
                    this.btnTimKiem_Click);

            this.lblSoLuong.Text = "Tổng số hồ sơ: 0";
            this.lblSoLuong.Location =
                new System.Drawing.Point(690, 35);
            this.lblSoLuong.Size =
                new System.Drawing.Size(365, 27);

            this.lblSoLuong.TextAlign =
                System.Drawing.ContentAlignment.MiddleRight;

            // BẢNG HỒ SƠ
            this.dgvHoSo.Location =
                new System.Drawing.Point(20, 75);
            this.dgvHoSo.Size =
                new System.Drawing.Size(1040, 235);

            this.dgvHoSo.ReadOnly = true;
            this.dgvHoSo.AllowUserToAddRows = false;
            this.dgvHoSo.AllowUserToDeleteRows = false;
            this.dgvHoSo.RowHeadersVisible = false;
            this.dgvHoSo.MultiSelect = false;

            this.dgvHoSo.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            this.dgvHoSo.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.dgvHoSo.BackgroundColor =
                System.Drawing.Color.White;

            this.dgvHoSo.SelectionChanged +=
                new System.EventHandler(
                    this.dgvHoSo_SelectionChanged);

            this.grpDanhSach.Controls.Add(lblTim);
            this.grpDanhSach.Controls.Add(txtTimKiem);
            this.grpDanhSach.Controls.Add(btnTimKiem);
            this.grpDanhSach.Controls.Add(lblSoLuong);
            this.grpDanhSach.Controls.Add(dgvHoSo);

            // THÊM CONTROL VÀO FORM
            this.Controls.Add(lblTieuDe);
            this.Controls.Add(grpThongTin);
            this.Controls.Add(btnTaoHoSo);
            this.Controls.Add(btnCapNhat);
            this.Controls.Add(btnLichSu);
            this.Controls.Add(btnLamMoi);
            this.Controls.Add(btnDong);
            this.Controls.Add(grpDanhSach);

            this.Load +=
                new System.EventHandler(
                    this.FrmHoSoBenhAn_Load);

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvHoSo)).EndInit();

            this.grpThongTin.ResumeLayout(false);
            this.grpThongTin.PerformLayout();

            this.grpDanhSach.ResumeLayout(false);
            this.grpDanhSach.PerformLayout();

            this.ResumeLayout(false);
        }
    }
}
