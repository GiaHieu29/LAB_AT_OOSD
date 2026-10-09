
namespace QuanLyCongTyDuLich.Forms
{
    partial class FrmDangKyDoan
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.GroupBox grpDoan;
        private System.Windows.Forms.GroupBox grpDangKy;

        private System.Windows.Forms.TextBox txtMaDoan;
        private System.Windows.Forms.TextBox txtTenCQ;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.TextBox txtDT;
        private System.Windows.Forms.TextBox txtDaiDien;

        private System.Windows.Forms.TextBox txtSo;
        private System.Windows.Forms.ComboBox cboTour;
        private System.Windows.Forms.DateTimePicker dtDi;
        private System.Windows.Forms.NumericUpDown numNguoi;
        private System.Windows.Forms.TextBox txtDon;
        private System.Windows.Forms.NumericUpDown numCoc;
        private System.Windows.Forms.CheckBox chkBH;

        private System.Windows.Forms.Label lblKetThuc;
        private System.Windows.Forms.Label lblTong;

        private System.Windows.Forms.DataGridView dgvThanhVien;
        private System.Windows.Forms.DataGridView dgv;

        private System.Windows.Forms.Button btnDangKy;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnDong;

        private System.Windows.Forms.Label lblDS;
        private System.Windows.Forms.Label lblPhieu;

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
            this.grpDoan = new System.Windows.Forms.GroupBox();
            this.grpDangKy = new System.Windows.Forms.GroupBox();

            this.txtMaDoan = new System.Windows.Forms.TextBox();
            this.txtTenCQ = new System.Windows.Forms.TextBox();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.txtDT = new System.Windows.Forms.TextBox();
            this.txtDaiDien = new System.Windows.Forms.TextBox();

            this.txtSo = new System.Windows.Forms.TextBox();
            this.cboTour = new System.Windows.Forms.ComboBox();
            this.dtDi = new System.Windows.Forms.DateTimePicker();
            this.numNguoi = new System.Windows.Forms.NumericUpDown();
            this.txtDon = new System.Windows.Forms.TextBox();
            this.numCoc = new System.Windows.Forms.NumericUpDown();
            this.chkBH = new System.Windows.Forms.CheckBox();

            this.lblKetThuc = new System.Windows.Forms.Label();
            this.lblTong = new System.Windows.Forms.Label();

            this.dgvThanhVien = new System.Windows.Forms.DataGridView();
            this.dgv = new System.Windows.Forms.DataGridView();

            this.btnDangKy = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();

            this.lblDS = new System.Windows.Forms.Label();
            this.lblPhieu = new System.Windows.Forms.Label();

            this.SuspendLayout();
            this.grpDoan.SuspendLayout();
            this.grpDangKy.SuspendLayout();

            // ========================================
            // FORM ĐĂNG KÝ ĐOÀN
            // ========================================

            this.Name = "FrmDangKyDoan";

            this.Text =
                "Phiếu đăng ký đoàn, danh sách người đi, hủy - mất cọc";

            this.ClientSize = new System.Drawing.Size(1000, 650);

            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterParent;

            this.FormBorderStyle =
                System.Windows.Forms.FormBorderStyle.FixedSingle;

            this.MaximizeBox = false;

            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;

            this.Font = new System.Drawing.Font("Segoe UI", 9F);

            this.BackColor =
                System.Drawing.Color.FromArgb(240, 240, 240);

            // ========================================
            // NHÓM 1: THÔNG TIN ĐOÀN KHÁCH
            // ========================================

            this.grpDoan.Name = "grpDoan";
            this.grpDoan.Text = "Thông tin đoàn khách";

            this.grpDoan.Location =
                new System.Drawing.Point(15, 12);

            this.grpDoan.Size =
                new System.Drawing.Size(475, 215);

            TaoNhan(this.grpDoan, "Mã đoàn", 15, 30, 110);
            TaoO(this.txtMaDoan, "txtMaDoan", 130, 27, 320);

            TaoNhan(this.grpDoan, "Cơ quan / gia đình",
                15, 66, 115);

            TaoO(this.txtTenCQ, "txtTenCQ", 130, 63, 320);

            TaoNhan(this.grpDoan, "Địa chỉ", 15, 102, 110);
            TaoO(this.txtDiaChi, "txtDiaChi", 130, 99, 320);

            TaoNhan(this.grpDoan, "Điện thoại", 15, 138, 110);
            TaoO(this.txtDT, "txtDT", 130, 135, 320);

            TaoNhan(this.grpDoan, "Người đại diện",
                15, 174, 110);

            TaoO(this.txtDaiDien, "txtDaiDien", 130, 171, 320);

            this.grpDoan.Controls.Add(this.txtMaDoan);
            this.grpDoan.Controls.Add(this.txtTenCQ);
            this.grpDoan.Controls.Add(this.txtDiaChi);
            this.grpDoan.Controls.Add(this.txtDT);
            this.grpDoan.Controls.Add(this.txtDaiDien);

            // ========================================
            // NHÓM 2: ĐĂNG KÝ TOUR
            // ========================================

            this.grpDangKy.Name = "grpDangKy";
            this.grpDangKy.Text = "Đăng ký tour";

            this.grpDangKy.Location =
                new System.Drawing.Point(505, 12);

            this.grpDangKy.Size =
                new System.Drawing.Size(480, 215);

            TaoNhan(this.grpDangKy, "Số phiếu", 15, 30, 75);
            TaoO(this.txtSo, "txtSo", 100, 27, 125);

            TaoNhan(this.grpDangKy, "Tour", 235, 30, 40);

            TaoCombo(this.cboTour, "cboTour",
                278, 27, 187);

            TaoNhan(this.grpDangKy, "Ngày đi", 15, 66, 75);

            this.dtDi.Name = "dtDi";
            this.dtDi.Location =
                new System.Drawing.Point(100, 63);

            this.dtDi.Size =
                new System.Drawing.Size(125, 25);

            this.dtDi.Format =
                System.Windows.Forms.DateTimePickerFormat.Custom;

            this.dtDi.CustomFormat = "dd/MM/yyyy";

            TaoNhan(this.grpDangKy, "Số người", 235, 66, 65);

            TaoSo(this.numNguoi, "numNguoi",
                305, 63, 160, 13, 10000, 13);

            TaoNhan(this.grpDangKy, "Địa điểm đón",
                15, 102, 85);

            TaoO(this.txtDon, "txtDon", 100, 99, 365);

            TaoNhan(this.grpDangKy, "Tiền cọc", 15, 138, 75);

            TaoSo(this.numCoc, "numCoc",
                100, 135, 170, 0, 1000000000, 0);

            this.numCoc.ThousandsSeparator = true;

            this.chkBH.Name = "chkBH";
            this.chkBH.Text = "Mua bảo hiểm";

            this.chkBH.Location =
                new System.Drawing.Point(300, 135);

            this.chkBH.Size =
                new System.Drawing.Size(155, 25);

            TaoNhan(this.grpDangKy, "Kết thúc DK",
                15, 177, 85);

            this.lblKetThuc.Name = "lblKetThuc";
            this.lblKetThuc.Text = "-";

            this.lblKetThuc.Location =
                new System.Drawing.Point(100, 174);

            this.lblKetThuc.Size =
                new System.Drawing.Size(130, 25);

            this.lblKetThuc.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;

            TaoNhan(this.grpDangKy, "Tổng dự kiến",
                235, 177, 100);

            this.lblTong.Name = "lblTong";
            this.lblTong.Text = "0 đ";

            this.lblTong.Location =
                new System.Drawing.Point(337, 174);

            this.lblTong.Size =
                new System.Drawing.Size(128, 25);

            this.lblTong.ForeColor =
                System.Drawing.Color.DarkRed;

            this.lblTong.Font = new System.Drawing.Font(
                "Segoe UI", 10F,
                System.Drawing.FontStyle.Bold);

            this.lblTong.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;

            this.grpDangKy.Controls.Add(this.txtSo);
            this.grpDangKy.Controls.Add(this.cboTour);
            this.grpDangKy.Controls.Add(this.dtDi);
            this.grpDangKy.Controls.Add(this.numNguoi);
            this.grpDangKy.Controls.Add(this.txtDon);
            this.grpDangKy.Controls.Add(this.numCoc);
            this.grpDangKy.Controls.Add(this.chkBH);
            this.grpDangKy.Controls.Add(this.lblKetThuc);
            this.grpDangKy.Controls.Add(this.lblTong);

            // ========================================
            // DANH SÁCH NGƯỜI CÙNG ĐI
            // ========================================

            this.lblDS.Name = "lblDS";

            this.lblDS.Text =
                "Danh sách người cùng đi (nhập đủ số người nếu mua bảo hiểm)";

            this.lblDS.Location =
                new System.Drawing.Point(15, 238);

            this.lblDS.Size =
                new System.Drawing.Size(650, 24);

            TaoBang(
                this.dgvThanhVien,
                "dgvThanhVien",
                15, 263, 970, 125);

            this.dgvThanhVien.ReadOnly = false;
            this.dgvThanhVien.AllowUserToAddRows = true;
            this.dgvThanhVien.AllowUserToDeleteRows = true;

            this.dgvThanhVien.EditMode =
                System.Windows.Forms.DataGridViewEditMode.EditOnKeystrokeOrF2;

            this.dgvThanhVien.Enabled = false;

            // ========================================
            // NÚT LẬP PHIẾU VÀ HỦY
            // ========================================

            TaoNut(
                this.btnDangKy,
                "Lập phiếu đăng ký",
                650, 397, 155);

            this.btnDangKy.Name = "btnDangKy";

            TaoNut(
                this.btnHuy,
                "Hủy phiếu (mất cọc)",
                818, 397, 167);

            this.btnHuy.Name = "btnHuy";

            // ========================================
            // DANH SÁCH PHIẾU ĐĂNG KÝ ĐOÀN
            // ========================================

            this.lblPhieu.Name = "lblPhieu";
            this.lblPhieu.Text = "Các phiếu đăng ký đoàn";

            this.lblPhieu.Location =
                new System.Drawing.Point(15, 403);

            this.lblPhieu.Size =
                new System.Drawing.Size(270, 25);

            TaoBang(
                this.dgv,
                "dgv",
                15, 435, 970, 158);

            this.dgv.ReadOnly = true;
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;

            // ========================================
            // NÚT ĐÓNG
            // ========================================

            TaoNut(this.btnDong, "Đóng", 870, 609, 115);
            this.btnDong.Name = "btnDong";

            // ========================================
            // GẮN SỰ KIỆN
            // ========================================

            this.Load +=
                new System.EventHandler(
                    this.FrmDangKyDoan_Load);

            this.cboTour.SelectedIndexChanged +=
                new System.EventHandler(
                    this.TinhTong);

            this.dtDi.ValueChanged +=
                new System.EventHandler(
                    this.TinhTong);

            this.numNguoi.ValueChanged +=
                new System.EventHandler(
                    this.TinhTong);

            this.chkBH.CheckedChanged +=
                new System.EventHandler(
                    this.chkBH_CheckedChanged);

            this.btnDangKy.Click +=
                new System.EventHandler(
                    this.btnDangKy_Click);

            this.btnHuy.Click +=
                new System.EventHandler(
                    this.btnHuy_Click);

            this.btnDong.Click +=
                new System.EventHandler(
                    this.btnDong_Click);

            // ========================================
            // THÊM ĐIỀU KHIỂN VÀO FORM
            // ========================================

            this.Controls.Add(this.grpDoan);
            this.Controls.Add(this.grpDangKy);

            this.Controls.Add(this.lblDS);
            this.Controls.Add(this.dgvThanhVien);

            this.Controls.Add(this.btnDangKy);
            this.Controls.Add(this.btnHuy);

            this.Controls.Add(this.lblPhieu);
            this.Controls.Add(this.dgv);

            this.Controls.Add(this.btnDong);

            this.grpDoan.ResumeLayout(false);
            this.grpDoan.PerformLayout();

            this.grpDangKy.ResumeLayout(false);
            this.grpDangKy.PerformLayout();

            this.ResumeLayout(false);
        }

        // ========================================
        // HÀM TẠO LABEL
        // ========================================

        private void TaoNhan(
            System.Windows.Forms.Control cha,
            string noiDung,
            int x,
            int y,
            int rong)
        {
            var lbl = new System.Windows.Forms.Label();

            lbl.Text = noiDung;

            lbl.Location =
                new System.Drawing.Point(x, y);

            lbl.Size =
                new System.Drawing.Size(rong, 25);

            lbl.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;

            cha.Controls.Add(lbl);
        }

        // ========================================
        // HÀM TẠO TEXTBOX
        // ========================================

        private void TaoO(
            System.Windows.Forms.TextBox txt,
            string ten,
            int x,
            int y,
            int rong)
        {
            txt.Name = ten;

            txt.Location =
                new System.Drawing.Point(x, y);

            txt.Size =
                new System.Drawing.Size(rong, 25);
        }

        // ========================================
        // HÀM TẠO COMBOBOX
        // ========================================

        private void TaoCombo(
            System.Windows.Forms.ComboBox cbo,
            string ten,
            int x,
            int y,
            int rong)
        {
            cbo.Name = ten;

            cbo.Location =
                new System.Drawing.Point(x, y);

            cbo.Size =
                new System.Drawing.Size(rong, 25);

            cbo.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;
        }

        // ========================================
        // HÀM TẠO NUMERICUPDOWN
        // ========================================

        private void TaoSo(
            System.Windows.Forms.NumericUpDown num,
            string ten,
            int x,
            int y,
            int rong,
            decimal min,
            decimal max,
            decimal giaTri)
        {
            num.Name = ten;

            num.Location =
                new System.Drawing.Point(x, y);

            num.Size =
                new System.Drawing.Size(rong, 25);

            num.Minimum = min;
            num.Maximum = max;
            num.Value = giaTri;
        }

        // ========================================
        // HÀM TẠO DATAGRIDVIEW
        // ========================================

        private void TaoBang(
            System.Windows.Forms.DataGridView dgv,
            string ten,
            int x,
            int y,
            int rong,
            int cao)
        {
            dgv.Name = ten;

            dgv.Location =
                new System.Drawing.Point(x, y);

            dgv.Size =
                new System.Drawing.Size(rong, cao);

            dgv.BackgroundColor =
                System.Drawing.Color.White;

            dgv.BorderStyle =
                System.Windows.Forms.BorderStyle.FixedSingle;

            dgv.AutoGenerateColumns = true;

            dgv.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;

            dgv.ReadOnly = true;
            dgv.MultiSelect = false;

            dgv.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            dgv.RowHeadersVisible = true;

            dgv.EnableHeadersVisualStyles = false;

            dgv.ColumnHeadersDefaultCellStyle.BackColor =
                System.Drawing.Color.FromArgb(230, 230, 230);

            dgv.DefaultCellStyle.SelectionBackColor =
                System.Drawing.Color.FromArgb(195, 222, 244);

            dgv.DefaultCellStyle.SelectionForeColor =
                System.Drawing.Color.Black;
        }

        // ========================================
        // HÀM TẠO BUTTON
        // ========================================

        private void TaoNut(
            System.Windows.Forms.Button btn,
            string noiDung,
            int x,
            int y,
            int rong)
        {
            btn.Text = noiDung;

            btn.Location =
                new System.Drawing.Point(x, y);

            btn.Size =
                new System.Drawing.Size(rong, 29);

            btn.BackColor =
                System.Drawing.Color.FromArgb(225, 225, 225);

            btn.ForeColor = System.Drawing.Color.Black;

            btn.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            btn.FlatAppearance.BorderColor =
                System.Drawing.Color.LightGray;

            btn.FlatAppearance.BorderSize = 1;

            btn.UseVisualStyleBackColor = false;

            btn.Cursor =
                System.Windows.Forms.Cursors.Hand;
        }
    }
}
