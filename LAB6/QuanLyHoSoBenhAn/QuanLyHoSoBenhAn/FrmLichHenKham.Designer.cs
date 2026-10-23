
namespace QuanLyHoSoBenhAn
{
    partial class FrmLichHenKham
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox
            txtMaLichHen, txtNoiDung, txtTimKiem;

        private System.Windows.Forms.ComboBox
            cboHoSo, cboKhoa, cboLoaiHen, cboTrangThai;

        private System.Windows.Forms.DateTimePicker dtNgayHen;

        private System.Windows.Forms.Button
            btnThem, btnSua, btnXoa, btnLamMoi,
            btnTimKiem, btnDong;

        private System.Windows.Forms.DataGridView dgvLichHen;
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
            this.txtMaLichHen =
                new System.Windows.Forms.TextBox();
            this.txtNoiDung =
                new System.Windows.Forms.TextBox();
            this.txtTimKiem =
                new System.Windows.Forms.TextBox();

            this.cboHoSo =
                new System.Windows.Forms.ComboBox();
            this.cboKhoa =
                new System.Windows.Forms.ComboBox();
            this.cboLoaiHen =
                new System.Windows.Forms.ComboBox();
            this.cboTrangThai =
                new System.Windows.Forms.ComboBox();

            this.dtNgayHen =
                new System.Windows.Forms.DateTimePicker();

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

            this.dgvLichHen =
                new System.Windows.Forms.DataGridView();

            this.lblSoLuong =
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
            System.Windows.Forms.Label l7 =
                new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvLichHen)).BeginInit();

            this.grpThongTin.SuspendLayout();
            this.grpDanhSach.SuspendLayout();
            this.SuspendLayout();

            // =================================
            // FORM
            // =================================
            this.Name = "FrmLichHenKham";
            this.Text = "Quản lý lịch hẹn khám";

            this.ClientSize =
                new System.Drawing.Size(1100, 750);

            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;

            this.Font =
                new System.Drawing.Font("Segoe UI", 10F);

            this.BackColor =
                System.Drawing.Color.FromArgb(245, 245, 245);

            this.FormBorderStyle =
                System.Windows.Forms.FormBorderStyle.FixedSingle;

            this.MaximizeBox = false;

            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;

            // =================================
            // TIÊU ĐỀ
            // =================================
            lblTieuDe.Text = "QUẢN LÝ LỊCH HẸN KHÁM";

            lblTieuDe.Location =
                new System.Drawing.Point(20, 12);

            lblTieuDe.Size =
                new System.Drawing.Size(1060, 45);

            lblTieuDe.Font =
                new System.Drawing.Font(
                    "Segoe UI", 17F,
                    System.Drawing.FontStyle.Bold);

            lblTieuDe.ForeColor =
                System.Drawing.Color.FromArgb(35, 75, 125);

            lblTieuDe.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;

            // =================================
            // THÔNG TIN LỊCH HẸN
            // =================================
            grpThongTin.Text = "Thông tin lịch hẹn";

            grpThongTin.Location =
                new System.Drawing.Point(20, 70);

            grpThongTin.Size =
                new System.Drawing.Size(1060, 265);

            // MÃ LỊCH HẸN
            l1.Text = "Mã lịch hẹn:";
            l1.Location =
                new System.Drawing.Point(20, 40);
            l1.Size =
                new System.Drawing.Size(130, 27);

            txtMaLichHen.Location =
                new System.Drawing.Point(150, 37);
            txtMaLichHen.Size =
                new System.Drawing.Size(300, 27);
            txtMaLichHen.MaxLength = 20;

            // HỒ SƠ
            l2.Text = "Hồ sơ bệnh án:";
            l2.Location =
                new System.Drawing.Point(485, 40);
            l2.Size =
                new System.Drawing.Size(135, 27);

            cboHoSo.Location =
                new System.Drawing.Point(625, 37);
            cboHoSo.Size =
                new System.Drawing.Size(405, 27);
            cboHoSo.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            // KHOA
            l3.Text = "Khoa điều trị:";
            l3.Location =
                new System.Drawing.Point(20, 87);
            l3.Size =
                new System.Drawing.Size(130, 27);

            cboKhoa.Location =
                new System.Drawing.Point(150, 84);
            cboKhoa.Size =
                new System.Drawing.Size(300, 27);
            cboKhoa.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            // NGÀY HẸN
            l4.Text = "Ngày giờ hẹn:";
            l4.Location =
                new System.Drawing.Point(485, 87);
            l4.Size =
                new System.Drawing.Size(135, 27);

            dtNgayHen.Location =
                new System.Drawing.Point(625, 84);
            dtNgayHen.Size =
                new System.Drawing.Size(300, 27);

            dtNgayHen.Format =
                System.Windows.Forms.DateTimePickerFormat.Custom;

            dtNgayHen.CustomFormat =
                "dd/MM/yyyy HH:mm";

            dtNgayHen.ShowUpDown = true;

            // LOẠI HẸN
            l5.Text = "Loại hẹn:";
            l5.Location =
                new System.Drawing.Point(20, 134);
            l5.Size =
                new System.Drawing.Size(130, 27);

            cboLoaiHen.Location =
                new System.Drawing.Point(150, 131);
            cboLoaiHen.Size =
                new System.Drawing.Size(300, 27);
            cboLoaiHen.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            // TRẠNG THÁI
            l6.Text = "Trạng thái:";
            l6.Location =
                new System.Drawing.Point(485, 134);
            l6.Size =
                new System.Drawing.Size(135, 27);

            cboTrangThai.Location =
                new System.Drawing.Point(625, 131);
            cboTrangThai.Size =
                new System.Drawing.Size(300, 27);
            cboTrangThai.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            // NỘI DUNG
            l7.Text = "Nội dung:";
            l7.Location =
                new System.Drawing.Point(20, 187);
            l7.Size =
                new System.Drawing.Size(130, 27);

            txtNoiDung.Location =
                new System.Drawing.Point(150, 184);
            txtNoiDung.Size =
                new System.Drawing.Size(880, 55);
            txtNoiDung.Multiline = true;
            txtNoiDung.MaxLength = 1000;

            grpThongTin.Controls.AddRange(
                new System.Windows.Forms.Control[]
                {
                    l1, txtMaLichHen,
                    l2, cboHoSo,
                    l3, cboKhoa,
                    l4, dtNgayHen,
                    l5, cboLoaiHen,
                    l6, cboTrangThai,
                    l7, txtNoiDung
                });

            // =================================
            // NÚT CHỨC NĂNG
            // =================================
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
                        210 + i * 145, 355);

                nut[i].Size =
                    new System.Drawing.Size(130, 38);

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

            // =================================
            // DANH SÁCH LỊCH HẸN
            // =================================
            grpDanhSach.Text = "Danh sách lịch hẹn khám";

            grpDanhSach.Location =
                new System.Drawing.Point(20, 415);

            grpDanhSach.Size =
                new System.Drawing.Size(1060, 315);

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

            lblSoLuong.Text = "Số lịch hẹn: 0";
            lblSoLuong.Location =
                new System.Drawing.Point(745, 35);
            lblSoLuong.Size =
                new System.Drawing.Size(290, 27);

            lblSoLuong.TextAlign =
                System.Drawing.ContentAlignment.MiddleRight;

            // DATAGRIDVIEW
            dgvLichHen.Location =
                new System.Drawing.Point(20, 80);
            dgvLichHen.Size =
                new System.Drawing.Size(1020, 215);

            dgvLichHen.BackgroundColor =
                System.Drawing.Color.White;

            dgvLichHen.ReadOnly = true;
            dgvLichHen.AllowUserToAddRows = false;
            dgvLichHen.AllowUserToDeleteRows = false;
            dgvLichHen.RowHeadersVisible = false;
            dgvLichHen.MultiSelect = false;

            dgvLichHen.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            dgvLichHen.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            dgvLichHen.EnableHeadersVisualStyles = false;

            dgvLichHen.ColumnHeadersDefaultCellStyle.BackColor =
                System.Drawing.Color.FromArgb(219, 230, 241);

            dgvLichHen.SelectionChanged +=
                new System.EventHandler(
                    this.dgvLichHen_SelectionChanged);

            grpDanhSach.Controls.AddRange(
                new System.Windows.Forms.Control[]
                {
                    lblTim, txtTimKiem, btnTimKiem,
                    lblSoLuong, dgvLichHen
                });

            this.Controls.Add(lblTieuDe);
            this.Controls.Add(grpThongTin);
            this.Controls.Add(grpDanhSach);

            this.Load +=
                new System.EventHandler(
                    this.FrmLichHenKham_Load);

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvLichHen)).EndInit();

            grpThongTin.ResumeLayout(false);
            grpThongTin.PerformLayout();

            grpDanhSach.ResumeLayout(false);
            grpDanhSach.PerformLayout();

            this.ResumeLayout(false);
        }
    }
}
