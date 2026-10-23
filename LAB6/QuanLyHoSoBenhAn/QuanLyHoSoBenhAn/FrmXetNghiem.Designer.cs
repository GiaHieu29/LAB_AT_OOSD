
namespace QuanLyHoSoBenhAn
{
    partial class FrmXetNghiem
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox
            txtMaXetNghiem, txtTenXetNghiem,
            txtKetQua, txtGhiChu, txtTimKiem;

        private System.Windows.Forms.ComboBox
            cboPhieuKham, cboTrangThai;

        private System.Windows.Forms.DateTimePicker
            dtNgayChiDinh, dtNgayKetQua;

        private System.Windows.Forms.Button
            btnThem, btnSua, btnXoa,
            btnTimKiem, btnLamMoi, btnDong;

        private System.Windows.Forms.DataGridView dgvXetNghiem;
        private System.Windows.Forms.Label lblSoLuong;

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
            this.txtMaXetNghiem = new System.Windows.Forms.TextBox();
            this.txtTenXetNghiem = new System.Windows.Forms.TextBox();
            this.txtKetQua = new System.Windows.Forms.TextBox();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.txtTimKiem = new System.Windows.Forms.TextBox();

            this.cboPhieuKham = new System.Windows.Forms.ComboBox();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();

            this.dtNgayChiDinh =
                new System.Windows.Forms.DateTimePicker();
            this.dtNgayKetQua =
                new System.Windows.Forms.DateTimePicker();

            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();

            this.dgvXetNghiem =
                new System.Windows.Forms.DataGridView();
            this.lblSoLuong = new System.Windows.Forms.Label();

            this.grpThongTin = new System.Windows.Forms.GroupBox();
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
                (this.dgvXetNghiem)).BeginInit();

            this.grpThongTin.SuspendLayout();
            this.grpDanhSach.SuspendLayout();
            this.SuspendLayout();

            // =================================
            // FORM
            // =================================
            this.Name = "FrmXetNghiem";
            this.Text = "Quản lý xét nghiệm";
            this.ClientSize =
                new System.Drawing.Size(1100, 760);
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
            title.Text = "QUẢN LÝ XÉT NGHIỆM";
            title.Font = new System.Drawing.Font(
                "Segoe UI", 17F,
                System.Drawing.FontStyle.Bold);
            title.ForeColor =
                System.Drawing.Color.FromArgb(35, 75, 125);
            title.Location =
                new System.Drawing.Point(20, 10);
            title.Size =
                new System.Drawing.Size(1060, 45);
            title.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;

            // =================================
            // GROUP THÔNG TIN
            // =================================
            grpThongTin.Text = "Thông tin xét nghiệm";
            grpThongTin.Location =
                new System.Drawing.Point(20, 65);
            grpThongTin.Size =
                new System.Drawing.Size(1060, 285);

            l1.Text = "Mã xét nghiệm:";
            l1.Location = new System.Drawing.Point(20, 40);
            l1.Size = new System.Drawing.Size(140, 27);
            txtMaXetNghiem.Location =
                new System.Drawing.Point(165, 37);
            txtMaXetNghiem.Size =
                new System.Drawing.Size(300, 27);
            txtMaXetNghiem.MaxLength = 20;

            l2.Text = "Phiếu khám:";
            l2.Location = new System.Drawing.Point(500, 40);
            l2.Size = new System.Drawing.Size(120, 27);
            cboPhieuKham.Location =
                new System.Drawing.Point(630, 37);
            cboPhieuKham.Size =
                new System.Drawing.Size(400, 27);
            cboPhieuKham.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            l3.Text = "Tên xét nghiệm:";
            l3.Location = new System.Drawing.Point(20, 87);
            l3.Size = new System.Drawing.Size(140, 27);
            txtTenXetNghiem.Location =
                new System.Drawing.Point(165, 84);
            txtTenXetNghiem.Size =
                new System.Drawing.Size(300, 27);
            txtTenXetNghiem.MaxLength = 200;

            l4.Text = "Trạng thái:";
            l4.Location = new System.Drawing.Point(500, 87);
            l4.Size = new System.Drawing.Size(120, 27);
            cboTrangThai.Location =
                new System.Drawing.Point(630, 84);
            cboTrangThai.Size =
                new System.Drawing.Size(400, 27);
            cboTrangThai.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;
            cboTrangThai.SelectedIndexChanged +=
                new System.EventHandler(
                    this.cboTrangThai_SelectedIndexChanged);

            l5.Text = "Ngày chỉ định:";
            l5.Location = new System.Drawing.Point(20, 134);
            l5.Size = new System.Drawing.Size(140, 27);
            dtNgayChiDinh.Location =
                new System.Drawing.Point(165, 131);
            dtNgayChiDinh.Size =
                new System.Drawing.Size(300, 27);
            dtNgayChiDinh.Format =
                System.Windows.Forms.DateTimePickerFormat.Custom;
            dtNgayChiDinh.CustomFormat = "dd/MM/yyyy";

            l6.Text = "Ngày có kết quả:";
            l6.Location = new System.Drawing.Point(500, 134);
            l6.Size = new System.Drawing.Size(125, 27);
            dtNgayKetQua.Location =
                new System.Drawing.Point(630, 131);
            dtNgayKetQua.Size =
                new System.Drawing.Size(400, 27);
            dtNgayKetQua.Format =
                System.Windows.Forms.DateTimePickerFormat.Custom;
            dtNgayKetQua.CustomFormat = "dd/MM/yyyy";
            dtNgayKetQua.ShowCheckBox = true;
            dtNgayKetQua.Checked = false;

            l7.Text = "Kết quả:";
            l7.Location = new System.Drawing.Point(20, 180);
            l7.Size = new System.Drawing.Size(140, 27);
            txtKetQua.Location =
                new System.Drawing.Point(165, 177);
            txtKetQua.Size =
                new System.Drawing.Size(865, 42);
            txtKetQua.Multiline = true;
            txtKetQua.MaxLength = 2000;

            l8.Text = "Ghi chú:";
            l8.Location = new System.Drawing.Point(20, 234);
            l8.Size = new System.Drawing.Size(140, 27);
            txtGhiChu.Location =
                new System.Drawing.Point(165, 231);
            txtGhiChu.Size =
                new System.Drawing.Size(865, 40);
            txtGhiChu.Multiline = true;
            txtGhiChu.MaxLength = 1000;

            grpThongTin.Controls.AddRange(
                new System.Windows.Forms.Control[]
                {
                    l1, txtMaXetNghiem,
                    l2, cboPhieuKham,
                    l3, txtTenXetNghiem,
                    l4, cboTrangThai,
                    l5, dtNgayChiDinh,
                    l6, dtNgayKetQua,
                    l7, txtKetQua,
                    l8, txtGhiChu
                });

            // =================================
            // NÚT CHỨC NĂNG
            // =================================
            btnThem.Text = "Thêm";
            btnSua.Text = "Sửa";
            btnXoa.Text = "Xóa";
            btnLamMoi.Text = "Làm mới";
            btnDong.Text = "Đóng";

            btnThem.Location =
                new System.Drawing.Point(220, 366);
            btnSua.Location =
                new System.Drawing.Point(365, 366);
            btnXoa.Location =
                new System.Drawing.Point(510, 366);
            btnLamMoi.Location =
                new System.Drawing.Point(655, 366);
            btnDong.Location =
                new System.Drawing.Point(800, 366);

            foreach (System.Windows.Forms.Button b
                in new System.Windows.Forms.Button[]
                {
                    btnThem, btnSua, btnXoa, btnLamMoi, btnDong
                })
            {
                b.Size = new System.Drawing.Size(130, 38);
                this.Controls.Add(b);
            }

            btnThem.Click += new System.EventHandler(
                this.btnThem_Click);
            btnSua.Click += new System.EventHandler(
                this.btnSua_Click);
            btnXoa.Click += new System.EventHandler(
                this.btnXoa_Click);
            btnLamMoi.Click += new System.EventHandler(
                this.btnLamMoi_Click);
            btnDong.Click += new System.EventHandler(
                this.btnDong_Click);

            // =================================
            // DANH SÁCH
            // =================================
            grpDanhSach.Text = "Danh sách xét nghiệm";
            grpDanhSach.Location =
                new System.Drawing.Point(20, 420);
            grpDanhSach.Size =
                new System.Drawing.Size(1060, 320);

            l9.Text = "Tìm kiếm:";
            l9.Location = new System.Drawing.Point(20, 34);
            l9.Size = new System.Drawing.Size(100, 27);

            txtTimKiem.Location =
                new System.Drawing.Point(120, 31);
            txtTimKiem.Size =
                new System.Drawing.Size(340, 27);

            btnTimKiem.Text = "Tìm kiếm";
            btnTimKiem.Location =
                new System.Drawing.Point(475, 29);
            btnTimKiem.Size =
                new System.Drawing.Size(125, 34);
            btnTimKiem.Click += new System.EventHandler(
                this.btnTimKiem_Click);

            lblSoLuong.Text = "Số xét nghiệm: 0";
            lblSoLuong.Location =
                new System.Drawing.Point(745, 33);
            lblSoLuong.Size =
                new System.Drawing.Size(290, 27);
            lblSoLuong.TextAlign =
                System.Drawing.ContentAlignment.MiddleRight;

            dgvXetNghiem.Location =
                new System.Drawing.Point(20, 78);
            dgvXetNghiem.Size =
                new System.Drawing.Size(1020, 225);
            dgvXetNghiem.BackgroundColor =
                System.Drawing.Color.White;
            dgvXetNghiem.ReadOnly = true;
            dgvXetNghiem.AllowUserToAddRows = false;
            dgvXetNghiem.AllowUserToDeleteRows = false;
            dgvXetNghiem.RowHeadersVisible = false;
            dgvXetNghiem.MultiSelect = false;
            dgvXetNghiem.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvXetNghiem.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvXetNghiem.EnableHeadersVisualStyles = false;
            dgvXetNghiem.ColumnHeadersDefaultCellStyle.BackColor =
                System.Drawing.Color.FromArgb(219, 230, 241);

            dgvXetNghiem.SelectionChanged +=
                new System.EventHandler(
                    this.dgvXetNghiem_SelectionChanged);

            grpDanhSach.Controls.AddRange(
                new System.Windows.Forms.Control[]
                {
                    l9, txtTimKiem, btnTimKiem,
                    lblSoLuong, dgvXetNghiem
                });

            this.Controls.Add(title);
            this.Controls.Add(grpThongTin);
            this.Controls.Add(grpDanhSach);

            this.Load += new System.EventHandler(
                this.FrmXetNghiem_Load);

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvXetNghiem)).EndInit();

            grpThongTin.ResumeLayout(false);
            grpThongTin.PerformLayout();
            grpDanhSach.ResumeLayout(false);
            grpDanhSach.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
