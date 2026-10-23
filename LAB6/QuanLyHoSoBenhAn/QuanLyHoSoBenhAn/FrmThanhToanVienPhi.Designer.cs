
namespace QuanLyHoSoBenhAn
{
    partial class FrmThanhToanVienPhi
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox
            txtMaThanhToan, txtGhiChu, txtTimKiem;

        private System.Windows.Forms.ComboBox
            cboPhieuKham, cboPhuongThuc;

        private System.Windows.Forms.DateTimePicker dtNgayThanhToan;

        private System.Windows.Forms.NumericUpDown nudSoTien;

        private System.Windows.Forms.Button
            btnThem, btnSua, btnXoa,
            btnLamMoi, btnTimKiem, btnDong;

        private System.Windows.Forms.DataGridView dgvThanhToan;

        private System.Windows.Forms.Label
            lblSoLuong, lblTongTien;

        private System.Windows.Forms.GroupBox
            grpThongTin, grpDanhSach;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtMaThanhToan =
                new System.Windows.Forms.TextBox();
            this.txtGhiChu =
                new System.Windows.Forms.TextBox();
            this.txtTimKiem =
                new System.Windows.Forms.TextBox();

            this.cboPhieuKham =
                new System.Windows.Forms.ComboBox();
            this.cboPhuongThuc =
                new System.Windows.Forms.ComboBox();

            this.dtNgayThanhToan =
                new System.Windows.Forms.DateTimePicker();

            this.nudSoTien =
                new System.Windows.Forms.NumericUpDown();

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

            this.dgvThanhToan =
                new System.Windows.Forms.DataGridView();

            this.lblSoLuong =
                new System.Windows.Forms.Label();
            this.lblTongTien =
                new System.Windows.Forms.Label();

            this.grpThongTin =
                new System.Windows.Forms.GroupBox();
            this.grpDanhSach =
                new System.Windows.Forms.GroupBox();

            System.Windows.Forms.Label lblTieuDe =
                new System.Windows.Forms.Label();
            System.Windows.Forms.Label l1 =
                new System.Windows.Forms.Label();
            System.Windows.Forms.Label l2 =
                new System.Windows.Forms.Label();
            System.Windows.Forms.Label l3 =
                new System.Windows.Forms.Label();
            System.Windows.Forms.Label l4 =
                new System.Windows.Forms.Label();
            System.Windows.Forms.Label l5 =
                new System.Windows.Forms.Label();
            System.Windows.Forms.Label l6 =
                new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)
                (this.nudSoTien)).BeginInit();

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvThanhToan)).BeginInit();

            this.grpThongTin.SuspendLayout();
            this.grpDanhSach.SuspendLayout();
            this.SuspendLayout();

            // FORM
            this.Name = "FrmThanhToanVienPhi";
            this.Text = "Quản lý thanh toán viện phí";
            this.ClientSize =
                new System.Drawing.Size(1100, 750);
            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Font =
                new System.Drawing.Font("Segoe UI", 10F);
            this.BackColor =
                System.Drawing.Color.FromArgb(245, 245, 245);
            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;
            this.FormBorderStyle =
                System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // TIÊU ĐỀ
            lblTieuDe.Text =
                "QUẢN LÝ THANH TOÁN VIỆN PHÍ";
            lblTieuDe.Location =
                new System.Drawing.Point(20, 12);
            lblTieuDe.Size =
                new System.Drawing.Size(1060, 45);
            lblTieuDe.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;
            lblTieuDe.Font =
                new System.Drawing.Font(
                    "Segoe UI", 17F,
                    System.Drawing.FontStyle.Bold);
            lblTieuDe.ForeColor =
                System.Drawing.Color.FromArgb(35, 75, 125);

            // GROUP THÔNG TIN
            grpThongTin.Text = "Thông tin thanh toán";
            grpThongTin.Location =
                new System.Drawing.Point(20, 70);
            grpThongTin.Size =
                new System.Drawing.Size(1060, 255);

            // MÃ THANH TOÁN
            l1.Text = "Mã thanh toán:";
            l1.Location =
                new System.Drawing.Point(20, 40);
            l1.Size =
                new System.Drawing.Size(140, 27);
            txtMaThanhToan.Location =
                new System.Drawing.Point(165, 37);
            txtMaThanhToan.Size =
                new System.Drawing.Size(300, 27);
            txtMaThanhToan.MaxLength = 20;

            // PHIẾU KHÁM
            l2.Text = "Phiếu khám:";
            l2.Location =
                new System.Drawing.Point(500, 40);
            l2.Size =
                new System.Drawing.Size(125, 27);
            cboPhieuKham.Location =
                new System.Drawing.Point(630, 37);
            cboPhieuKham.Size =
                new System.Drawing.Size(400, 27);
            cboPhieuKham.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            // NGÀY THANH TOÁN
            l3.Text = "Ngày thanh toán:";
            l3.Location =
                new System.Drawing.Point(20, 90);
            l3.Size =
                new System.Drawing.Size(145, 27);
            dtNgayThanhToan.Location =
                new System.Drawing.Point(165, 87);
            dtNgayThanhToan.Size =
                new System.Drawing.Size(300, 27);
            dtNgayThanhToan.Format =
                System.Windows.Forms.DateTimePickerFormat.Custom;
            dtNgayThanhToan.CustomFormat = "dd/MM/yyyy";

            // PHƯƠNG THỨC
            l4.Text = "Phương thức:";
            l4.Location =
                new System.Drawing.Point(500, 90);
            l4.Size =
                new System.Drawing.Size(125, 27);
            cboPhuongThuc.Location =
                new System.Drawing.Point(630, 87);
            cboPhuongThuc.Size =
                new System.Drawing.Size(400, 27);
            cboPhuongThuc.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            // SỐ TIỀN
            l5.Text = "Số tiền (VNĐ):";
            l5.Location =
                new System.Drawing.Point(20, 140);
            l5.Size =
                new System.Drawing.Size(140, 27);
            nudSoTien.Location =
                new System.Drawing.Point(165, 137);
            nudSoTien.Size =
                new System.Drawing.Size(300, 27);
            nudSoTien.Minimum = 0;
            nudSoTien.Maximum = 100000000000M;
            nudSoTien.DecimalPlaces = 0;
            nudSoTien.Increment = 10000;
            nudSoTien.ThousandsSeparator = true;

            // GHI CHÚ
            l6.Text = "Ghi chú:";
            l6.Location =
                new System.Drawing.Point(20, 190);
            l6.Size =
                new System.Drawing.Size(140, 27);
            txtGhiChu.Location =
                new System.Drawing.Point(165, 187);
            txtGhiChu.Size =
                new System.Drawing.Size(865, 48);
            txtGhiChu.Multiline = true;
            txtGhiChu.MaxLength = 1000;

            grpThongTin.Controls.AddRange(
                new System.Windows.Forms.Control[]
                {
                    l1, txtMaThanhToan,
                    l2, cboPhieuKham,
                    l3, dtNgayThanhToan,
                    l4, cboPhuongThuc,
                    l5, nudSoTien,
                    l6, txtGhiChu
                });

            // NÚT CHỨC NĂNG
            btnThem.Text = "Thêm";
            btnSua.Text = "Sửa";
            btnXoa.Text = "Xóa";
            btnLamMoi.Text = "Làm mới";
            btnDong.Text = "Đóng";

            System.Windows.Forms.Button[] nut =
            {
                btnThem, btnSua, btnXoa,
                btnLamMoi, btnDong
            };

            for (int i = 0; i < nut.Length; i++)
            {
                nut[i].Location =
                    new System.Drawing.Point(
                        210 + i * 145, 345);
                nut[i].Size =
                    new System.Drawing.Size(130, 40);
                this.Controls.Add(nut[i]);
            }

            btnThem.Click +=
                new System.EventHandler(this.btnThem_Click);
            btnSua.Click +=
                new System.EventHandler(this.btnSua_Click);
            btnXoa.Click +=
                new System.EventHandler(this.btnXoa_Click);
            btnLamMoi.Click +=
                new System.EventHandler(this.btnLamMoi_Click);
            btnDong.Click +=
                new System.EventHandler(this.btnDong_Click);

            // GROUP DANH SÁCH
            grpDanhSach.Text = "Danh sách thanh toán viện phí";
            grpDanhSach.Location =
                new System.Drawing.Point(20, 405);
            grpDanhSach.Size =
                new System.Drawing.Size(1060, 325);

            System.Windows.Forms.Label lblTim =
                new System.Windows.Forms.Label();

            lblTim.Text = "Tìm kiếm:";
            lblTim.Location =
                new System.Drawing.Point(20, 35);
            lblTim.Size =
                new System.Drawing.Size(100, 27);

            txtTimKiem.Location =
                new System.Drawing.Point(120, 32);
            txtTimKiem.Size =
                new System.Drawing.Size(310, 27);

            btnTimKiem.Text = "Tìm kiếm";
            btnTimKiem.Location =
                new System.Drawing.Point(445, 30);
            btnTimKiem.Size =
                new System.Drawing.Size(120, 34);
            btnTimKiem.Click +=
                new System.EventHandler(
                    this.btnTimKiem_Click);

            lblSoLuong.Text = "Số khoản thanh toán: 0";
            lblSoLuong.Location =
                new System.Drawing.Point(600, 35);
            lblSoLuong.Size =
                new System.Drawing.Size(435, 27);
            lblSoLuong.TextAlign =
                System.Drawing.ContentAlignment.MiddleRight;

            // DATAGRIDVIEW
            dgvThanhToan.Location =
                new System.Drawing.Point(20, 82);
            dgvThanhToan.Size =
                new System.Drawing.Size(1020, 175);
            dgvThanhToan.BackgroundColor =
                System.Drawing.Color.White;
            dgvThanhToan.ReadOnly = true;
            dgvThanhToan.AllowUserToAddRows = false;
            dgvThanhToan.AllowUserToDeleteRows = false;
            dgvThanhToan.RowHeadersVisible = false;
            dgvThanhToan.MultiSelect = false;
            dgvThanhToan.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvThanhToan.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvThanhToan.EnableHeadersVisualStyles = false;
            dgvThanhToan.ColumnHeadersDefaultCellStyle.BackColor =
                System.Drawing.Color.FromArgb(219, 230, 241);

            dgvThanhToan.SelectionChanged +=
                new System.EventHandler(
                    this.dgvThanhToan_SelectionChanged);

            // TỔNG TIỀN
            lblTongTien.Text = "Tổng tiền: 0 VNĐ";
            lblTongTien.Location =
                new System.Drawing.Point(520, 270);
            lblTongTien.Size =
                new System.Drawing.Size(515, 30);
            lblTongTien.TextAlign =
                System.Drawing.ContentAlignment.MiddleRight;
            lblTongTien.Font =
                new System.Drawing.Font(
                    "Segoe UI", 11F,
                    System.Drawing.FontStyle.Bold);
            lblTongTien.ForeColor =
                System.Drawing.Color.FromArgb(35, 75, 125);

            grpDanhSach.Controls.AddRange(
                new System.Windows.Forms.Control[]
                {
                    lblTim, txtTimKiem, btnTimKiem,
                    lblSoLuong, dgvThanhToan, lblTongTien
                });

            this.Controls.Add(lblTieuDe);
            this.Controls.Add(grpThongTin);
            this.Controls.Add(grpDanhSach);

            this.Load +=
                new System.EventHandler(
                    this.FrmThanhToanVienPhi_Load);

            ((System.ComponentModel.ISupportInitialize)
                (this.nudSoTien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)
                (this.dgvThanhToan)).EndInit();

            grpThongTin.ResumeLayout(false);
            grpThongTin.PerformLayout();
            grpDanhSach.ResumeLayout(false);
            grpDanhSach.PerformLayout();

            this.ResumeLayout(false);
        }
    }
}
