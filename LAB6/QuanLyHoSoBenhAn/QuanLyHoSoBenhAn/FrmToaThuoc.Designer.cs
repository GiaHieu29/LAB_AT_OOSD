
namespace QuanLyHoSoBenhAn
{
    partial class FrmToaThuoc
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox
            txtMaToa, txtBacSi, txtLoiDan,
            txtLieuDung, txtCachDung, txtTimKiem;

        private System.Windows.Forms.ComboBox
            cboPhieuKham, cboThuoc;

        private System.Windows.Forms.DateTimePicker dtNgayLap;

        private System.Windows.Forms.NumericUpDown
            nudSoLuong, nudSoNgay;

        private System.Windows.Forms.CheckBox chkSoNgay;

        private System.Windows.Forms.Button
            btnThemThuoc, btnXoaThuoc,
            btnLuu, btnCapNhat, btnXoa,
            btnLamMoi, btnTimKiem, btnDong;

        private System.Windows.Forms.DataGridView
            dgvChiTiet, dgvToaThuoc;

        private System.Windows.Forms.Label lblSoLuong;

        private System.Windows.Forms.GroupBox
            grpThongTin, grpChiTiet, grpDanhSach;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtMaToa = new System.Windows.Forms.TextBox();
            this.txtBacSi = new System.Windows.Forms.TextBox();
            this.txtLoiDan = new System.Windows.Forms.TextBox();
            this.txtLieuDung = new System.Windows.Forms.TextBox();
            this.txtCachDung = new System.Windows.Forms.TextBox();
            this.txtTimKiem = new System.Windows.Forms.TextBox();

            this.cboPhieuKham = new System.Windows.Forms.ComboBox();
            this.cboThuoc = new System.Windows.Forms.ComboBox();

            this.dtNgayLap =
                new System.Windows.Forms.DateTimePicker();

            this.nudSoLuong =
                new System.Windows.Forms.NumericUpDown();

            this.nudSoNgay =
                new System.Windows.Forms.NumericUpDown();

            this.chkSoNgay =
                new System.Windows.Forms.CheckBox();

            this.btnThemThuoc = new System.Windows.Forms.Button();
            this.btnXoaThuoc = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnCapNhat = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();

            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.dgvToaThuoc = new System.Windows.Forms.DataGridView();

            this.lblSoLuong = new System.Windows.Forms.Label();

            this.grpThongTin = new System.Windows.Forms.GroupBox();
            this.grpChiTiet = new System.Windows.Forms.GroupBox();
            this.grpDanhSach = new System.Windows.Forms.GroupBox();

            System.Windows.Forms.Label title =
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
            System.Windows.Forms.Label l7 =
                new System.Windows.Forms.Label();
            System.Windows.Forms.Label l8 =
                new System.Windows.Forms.Label();
            System.Windows.Forms.Label l9 =
                new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)
                (this.nudSoLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)
                (this.nudSoNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)
                (this.dgvChiTiet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)
                (this.dgvToaThuoc)).BeginInit();

            this.grpThongTin.SuspendLayout();
            this.grpChiTiet.SuspendLayout();
            this.grpDanhSach.SuspendLayout();
            this.SuspendLayout();

            // ===================================
            // FORM
            // ===================================
            this.Name = "FrmToaThuoc";
            this.Text = "Quản lý toa thuốc";
            this.ClientSize =
                new System.Drawing.Size(1140, 870);

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

            // ===================================
            // TIÊU ĐỀ
            // ===================================
            title.Text = "QUẢN LÝ TOA THUỐC";
            title.Location =
                new System.Drawing.Point(20, 8);
            title.Size =
                new System.Drawing.Size(1100, 45);

            title.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;

            title.Font = new System.Drawing.Font(
                "Segoe UI", 17F,
                System.Drawing.FontStyle.Bold);

            title.ForeColor =
                System.Drawing.Color.FromArgb(35, 75, 125);

            // ===================================
            // THÔNG TIN TOA
            // ===================================
            grpThongTin.Text = "Thông tin toa thuốc";
            grpThongTin.Location =
                new System.Drawing.Point(20, 60);
            grpThongTin.Size =
                new System.Drawing.Size(1100, 180);

            l1.Text = "Mã toa thuốc:";
            l1.Location = new System.Drawing.Point(20, 38);
            l1.Size = new System.Drawing.Size(130, 27);

            txtMaToa.Location =
                new System.Drawing.Point(150, 35);
            txtMaToa.Size =
                new System.Drawing.Size(290, 27);
            txtMaToa.MaxLength = 20;

            l2.Text = "Phiếu khám:";
            l2.Location = new System.Drawing.Point(460, 38);
            l2.Size = new System.Drawing.Size(115, 27);

            cboPhieuKham.Location =
                new System.Drawing.Point(580, 35);
            cboPhieuKham.Size =
                new System.Drawing.Size(490, 27);
            cboPhieuKham.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            cboPhieuKham.SelectedIndexChanged +=
                new System.EventHandler(
                    this.cboPhieuKham_SelectedIndexChanged);

            l3.Text = "Bác sĩ:";
            l3.Location = new System.Drawing.Point(20, 82);
            l3.Size = new System.Drawing.Size(130, 27);

            txtBacSi.Location =
                new System.Drawing.Point(150, 79);
            txtBacSi.Size =
                new System.Drawing.Size(290, 27);
            txtBacSi.ReadOnly = true;

            l4.Text = "Ngày lập:";
            l4.Location = new System.Drawing.Point(460, 82);
            l4.Size = new System.Drawing.Size(115, 27);

            dtNgayLap.Location =
                new System.Drawing.Point(580, 79);
            dtNgayLap.Size =
                new System.Drawing.Size(250, 27);
            dtNgayLap.Format =
                System.Windows.Forms.DateTimePickerFormat.Custom;
            dtNgayLap.CustomFormat = "dd/MM/yyyy";

            l5.Text = "Lời dặn:";
            l5.Location = new System.Drawing.Point(20, 128);
            l5.Size = new System.Drawing.Size(130, 27);

            txtLoiDan.Location =
                new System.Drawing.Point(150, 125);
            txtLoiDan.Size =
                new System.Drawing.Size(920, 43);
            txtLoiDan.Multiline = true;
            txtLoiDan.MaxLength = 1000;

            grpThongTin.Controls.AddRange(
                new System.Windows.Forms.Control[]
                {
                    l1, txtMaToa, l2, cboPhieuKham,
                    l3, txtBacSi, l4, dtNgayLap,
                    l5, txtLoiDan
                });

            // ===================================
            // CHI TIẾT THUỐC
            // ===================================
            grpChiTiet.Text = "Chi tiết thuốc trong toa";
            grpChiTiet.Location =
                new System.Drawing.Point(20, 250);
            grpChiTiet.Size =
                new System.Drawing.Size(1100, 255);

            l6.Text = "Thuốc:";
            l6.Location = new System.Drawing.Point(18, 35);
            l6.Size = new System.Drawing.Size(60, 27);

            cboThuoc.Location =
                new System.Drawing.Point(80, 32);
            cboThuoc.Size =
                new System.Drawing.Size(265, 27);
            cboThuoc.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            l7.Text = "Số lượng:";
            l7.Location = new System.Drawing.Point(365, 35);
            l7.Size = new System.Drawing.Size(90, 27);

            nudSoLuong.Location =
                new System.Drawing.Point(455, 32);
            nudSoLuong.Size =
                new System.Drawing.Size(95, 27);
            nudSoLuong.Minimum = 1;
            nudSoLuong.Maximum = 100000;
            nudSoLuong.Value = 1;

            chkSoNgay.Text = "Số ngày:";
            chkSoNgay.Location =
                new System.Drawing.Point(575, 33);
            chkSoNgay.Size =
                new System.Drawing.Size(105, 27);

            nudSoNgay.Location =
                new System.Drawing.Point(685, 32);
            nudSoNgay.Size =
                new System.Drawing.Size(85, 27);
            nudSoNgay.Minimum = 1;
            nudSoNgay.Maximum = 365;
            nudSoNgay.Value = 1;

            l8.Text = "Liều dùng:";
            l8.Location = new System.Drawing.Point(18, 75);
            l8.Size = new System.Drawing.Size(105, 27);

            txtLieuDung.Location =
                new System.Drawing.Point(120, 72);
            txtLieuDung.Size =
                new System.Drawing.Size(425, 27);
            txtLieuDung.MaxLength = 200;

            l9.Text = "Cách dùng:";
            l9.Location = new System.Drawing.Point(565, 75);
            l9.Size = new System.Drawing.Size(100, 27);

            txtCachDung.Location =
                new System.Drawing.Point(665, 72);
            txtCachDung.Size =
                new System.Drawing.Size(405, 27);
            txtCachDung.MaxLength = 200;

            btnThemThuoc.Text = "Thêm thuốc";
            btnThemThuoc.Location =
                new System.Drawing.Point(795, 30);
            btnThemThuoc.Size =
                new System.Drawing.Size(130, 32);

            btnThemThuoc.Click +=
                new System.EventHandler(
                    this.btnThemThuoc_Click);

            btnXoaThuoc.Text = "Bỏ dòng";
            btnXoaThuoc.Location =
                new System.Drawing.Point(940, 30);
            btnXoaThuoc.Size =
                new System.Drawing.Size(130, 32);

            btnXoaThuoc.Click +=
                new System.EventHandler(
                    this.btnXoaThuoc_Click);

            dgvChiTiet.Location =
                new System.Drawing.Point(18, 115);
            dgvChiTiet.Size =
                new System.Drawing.Size(1052, 122);
            dgvChiTiet.BackgroundColor =
                System.Drawing.Color.White;
            dgvChiTiet.ReadOnly = true;
            dgvChiTiet.AllowUserToAddRows = false;
            dgvChiTiet.AllowUserToDeleteRows = false;
            dgvChiTiet.RowHeadersVisible = false;
            dgvChiTiet.MultiSelect = false;
            dgvChiTiet.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvChiTiet.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            grpChiTiet.Controls.AddRange(
                new System.Windows.Forms.Control[]
                {
                    l6, cboThuoc, l7, nudSoLuong,
                    chkSoNgay, nudSoNgay,
                    l8, txtLieuDung, l9, txtCachDung,
                    btnThemThuoc, btnXoaThuoc,
                    dgvChiTiet
                });

            // ===================================
            // CÁC NÚT CHÍNH
            // ===================================
            btnLuu.Text = "Lưu toa";
            btnCapNhat.Text = "Cập nhật";
            btnXoa.Text = "Xóa toa";
            btnLamMoi.Text = "Làm mới";
            btnDong.Text = "Đóng";

            System.Windows.Forms.Button[] nut =
            {
                btnLuu, btnCapNhat, btnXoa,
                btnLamMoi, btnDong
            };

            for (int i = 0; i < nut.Length; i++)
            {
                nut[i].Location =
                    new System.Drawing.Point(
                        225 + i * 145, 520);

                nut[i].Size =
                    new System.Drawing.Size(130, 38);

                this.Controls.Add(nut[i]);
            }

            btnLuu.Click +=
                new System.EventHandler(this.btnLuu_Click);
            btnCapNhat.Click +=
                new System.EventHandler(this.btnCapNhat_Click);
            btnXoa.Click +=
                new System.EventHandler(this.btnXoa_Click);
            btnLamMoi.Click +=
                new System.EventHandler(this.btnLamMoi_Click);
            btnDong.Click +=
                new System.EventHandler(this.btnDong_Click);

            // ===================================
            // DANH SÁCH TOA THUỐC
            // ===================================
            grpDanhSach.Text = "Danh sách toa thuốc";
            grpDanhSach.Location =
                new System.Drawing.Point(20, 575);
            grpDanhSach.Size =
                new System.Drawing.Size(1100, 275);

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
                new System.Drawing.Size(350, 27);

            btnTimKiem.Text = "Tìm kiếm";
            btnTimKiem.Location =
                new System.Drawing.Point(485, 30);
            btnTimKiem.Size =
                new System.Drawing.Size(125, 34);

            btnTimKiem.Click +=
                new System.EventHandler(
                    this.btnTimKiem_Click);

            lblSoLuong.Text = "Số toa thuốc: 0";
            lblSoLuong.Location =
                new System.Drawing.Point(770, 35);
            lblSoLuong.Size =
                new System.Drawing.Size(300, 27);

            lblSoLuong.TextAlign =
                System.Drawing.ContentAlignment.MiddleRight;

            dgvToaThuoc.Location =
                new System.Drawing.Point(20, 76);
            dgvToaThuoc.Size =
                new System.Drawing.Size(1060, 180);

            dgvToaThuoc.BackgroundColor =
                System.Drawing.Color.White;
            dgvToaThuoc.ReadOnly = true;
            dgvToaThuoc.AllowUserToAddRows = false;
            dgvToaThuoc.AllowUserToDeleteRows = false;
            dgvToaThuoc.RowHeadersVisible = false;
            dgvToaThuoc.MultiSelect = false;
            dgvToaThuoc.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvToaThuoc.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            dgvToaThuoc.SelectionChanged +=
                new System.EventHandler(
                    this.dgvToaThuoc_SelectionChanged);

            grpDanhSach.Controls.AddRange(
                new System.Windows.Forms.Control[]
                {
                    lblTim, txtTimKiem, btnTimKiem,
                    lblSoLuong, dgvToaThuoc
                });

            this.Controls.Add(title);
            this.Controls.Add(grpThongTin);
            this.Controls.Add(grpChiTiet);
            this.Controls.Add(grpDanhSach);

            this.Load +=
                new System.EventHandler(
                    this.FrmToaThuoc_Load);

            ((System.ComponentModel.ISupportInitialize)
                (this.nudSoLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)
                (this.nudSoNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)
                (this.dgvChiTiet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)
                (this.dgvToaThuoc)).EndInit();

            grpThongTin.ResumeLayout(false);
            grpThongTin.PerformLayout();

            grpChiTiet.ResumeLayout(false);
            grpChiTiet.PerformLayout();

            grpDanhSach.ResumeLayout(false);
            grpDanhSach.PerformLayout();

            this.ResumeLayout(false);
        }
    }
}
