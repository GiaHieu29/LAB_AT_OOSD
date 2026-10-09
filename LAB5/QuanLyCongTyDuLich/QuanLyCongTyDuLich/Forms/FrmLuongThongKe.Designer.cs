
namespace QuanLyCongTyDuLich.Forms
{
    partial class FrmLuongThongKe
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TabControl tabTK;
        private System.Windows.Forms.TabPage tabLuong;
        private System.Windows.Forms.TabPage tabTongHop;

        private System.Windows.Forms.NumericUpDown numThang;
        private System.Windows.Forms.NumericUpDown numNam;
        private System.Windows.Forms.Button btnLuong;
        private System.Windows.Forms.DataGridView dgvLuong;

        private System.Windows.Forms.DateTimePicker dtTu;
        private System.Windows.Forms.DateTimePicker dtDen;
        private System.Windows.Forms.Button btnTongHop;
        private System.Windows.Forms.DataGridView dgvTongHop;

        private System.Windows.Forms.Button btnDong;

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
            this.tabTK = new System.Windows.Forms.TabControl();
            this.tabLuong = new System.Windows.Forms.TabPage();
            this.tabTongHop = new System.Windows.Forms.TabPage();

            this.numThang = new System.Windows.Forms.NumericUpDown();
            this.numNam = new System.Windows.Forms.NumericUpDown();
            this.btnLuong = new System.Windows.Forms.Button();
            this.dgvLuong = new System.Windows.Forms.DataGridView();

            this.dtTu = new System.Windows.Forms.DateTimePicker();
            this.dtDen = new System.Windows.Forms.DateTimePicker();
            this.btnTongHop = new System.Windows.Forms.Button();
            this.dgvTongHop = new System.Windows.Forms.DataGridView();

            this.btnDong = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)
                (this.numThang)).BeginInit();

            ((System.ComponentModel.ISupportInitialize)
                (this.numNam)).BeginInit();

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvLuong)).BeginInit();

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvTongHop)).BeginInit();

            this.tabTK.SuspendLayout();
            this.tabLuong.SuspendLayout();
            this.tabTongHop.SuspendLayout();
            this.SuspendLayout();

            // =====================================
            // CẤU HÌNH FORM
            // =====================================

            this.Name = "FrmLuongThongKe";
            this.Text = "Lương hướng dẫn viên - thống kê";

            this.ClientSize =
                new System.Drawing.Size(940, 465);

            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterParent;

            this.FormBorderStyle =
                System.Windows.Forms.FormBorderStyle.FixedSingle;

            this.MaximizeBox = false;

            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;

            this.Font =
                new System.Drawing.Font("Segoe UI", 9F);

            this.BackColor =
                System.Drawing.Color.FromArgb(240, 240, 240);

            // =====================================
            // TAB CONTROL
            // =====================================

            this.tabTK.Name = "tabTK";

            this.tabTK.Location =
                new System.Drawing.Point(15, 12);

            this.tabTK.Size =
                new System.Drawing.Size(910, 397);

            this.tabLuong.Name = "tabLuong";
            this.tabLuong.Text = "Lương hướng dẫn viên";

            this.tabTongHop.Name = "tabTongHop";
            this.tabTongHop.Text = "Thống kê tổng hợp";

            this.tabLuong.BackColor =
                System.Drawing.Color.FromArgb(245, 245, 245);

            this.tabTongHop.BackColor =
                System.Drawing.Color.FromArgb(245, 245, 245);

            this.tabTK.TabPages.Add(this.tabLuong);
            this.tabTK.TabPages.Add(this.tabTongHop);

            // =====================================
            // TAB 1: LƯƠNG HƯỚNG DẪN VIÊN
            // =====================================

            TaoNhan(
                this.tabLuong,
                "Tháng",
                20, 20, 65);

            this.numThang.Name = "numThang";

            this.numThang.Location =
                new System.Drawing.Point(85, 17);

            this.numThang.Size =
                new System.Drawing.Size(110, 27);

            this.numThang.Minimum = 1;
            this.numThang.Maximum = 12;
            this.numThang.Value = 1;

            TaoNhan(
                this.tabLuong,
                "Năm",
                225, 20, 50);

            this.numNam.Name = "numNam";

            this.numNam.Location =
                new System.Drawing.Point(280, 17);

            this.numNam.Size =
                new System.Drawing.Size(125, 27);

            this.numNam.Minimum = 2000;
            this.numNam.Maximum = 2100;
            this.numNam.Value = 2026;

            TaoNut(
                this.btnLuong,
                "Tính lương",
                445, 16, 145);

            this.btnLuong.Name = "btnLuong";

            TaoNhan(
                this.tabLuong,
                "Lương tháng = lương căn bản + tổng thù lao các tour kết thúc trong tháng",
                610, 20, 275);

            // BẢNG LƯƠNG
            TaoBang(
                this.dgvLuong,
                "dgvLuong",
                15, 60, 870, 300);

            this.tabLuong.Controls.Add(this.numThang);
            this.tabLuong.Controls.Add(this.numNam);
            this.tabLuong.Controls.Add(this.btnLuong);
            this.tabLuong.Controls.Add(this.dgvLuong);

            // =====================================
            // TAB 2: THỐNG KÊ TỔNG HỢP
            // =====================================

            TaoNhan(
                this.tabTongHop,
                "Từ ngày",
                20, 20, 70);

            this.dtTu.Name = "dtTu";

            this.dtTu.Location =
                new System.Drawing.Point(95, 17);

            this.dtTu.Size =
                new System.Drawing.Size(185, 27);

            this.dtTu.Format =
                System.Windows.Forms.DateTimePickerFormat.Custom;

            this.dtTu.CustomFormat = "dd/MM/yyyy";

            TaoNhan(
                this.tabTongHop,
                "Đến ngày",
                310, 20, 75);

            this.dtDen.Name = "dtDen";

            this.dtDen.Location =
                new System.Drawing.Point(390, 17);

            this.dtDen.Size =
                new System.Drawing.Size(185, 27);

            this.dtDen.Format =
                System.Windows.Forms.DateTimePickerFormat.Custom;

            this.dtDen.CustomFormat = "dd/MM/yyyy";

            TaoNut(
                this.btnTongHop,
                "Thống kê",
                615, 16, 145);

            this.btnTongHop.Name = "btnTongHop";

            // BẢNG THỐNG KÊ
            TaoBang(
                this.dgvTongHop,
                "dgvTongHop",
                15, 60, 870, 300);

            this.tabTongHop.Controls.Add(this.dtTu);
            this.tabTongHop.Controls.Add(this.dtDen);
            this.tabTongHop.Controls.Add(this.btnTongHop);
            this.tabTongHop.Controls.Add(this.dgvTongHop);

            // =====================================
            // NÚT ĐÓNG
            // =====================================

            TaoNut(
                this.btnDong,
                "Đóng",
                795, 420, 125);

            this.btnDong.Name = "btnDong";

            // =====================================
            // GẮN SỰ KIỆN
            // =====================================

            this.Load +=
                new System.EventHandler(
                    this.FrmLuongThongKe_Load);

            this.btnLuong.Click +=
                new System.EventHandler(
                    this.btnLuong_Click);

            this.btnTongHop.Click +=
                new System.EventHandler(
                    this.btnTongHop_Click);

            this.btnDong.Click +=
                new System.EventHandler(
                    this.btnDong_Click);

            // =====================================
            // THÊM CÁC CONTROL VÀO FORM
            // =====================================

            this.Controls.Add(this.tabTK);
            this.Controls.Add(this.btnDong);

            ((System.ComponentModel.ISupportInitialize)
                (this.numThang)).EndInit();

            ((System.ComponentModel.ISupportInitialize)
                (this.numNam)).EndInit();

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvLuong)).EndInit();

            ((System.ComponentModel.ISupportInitialize)
                (this.dgvTongHop)).EndInit();

            this.tabLuong.ResumeLayout(false);
            this.tabTongHop.ResumeLayout(false);
            this.tabTK.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // =====================================
        // HÀM TẠO LABEL
        // =====================================

        private void TaoNhan(
            System.Windows.Forms.Control cha,
            string noiDung,
            int x,
            int y,
            int rong)
        {
            System.Windows.Forms.Label lbl =
                new System.Windows.Forms.Label();

            lbl.Text = noiDung;

            lbl.Location =
                new System.Drawing.Point(x, y);

            lbl.Size =
                new System.Drawing.Size(rong, 25);

            lbl.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;

            cha.Controls.Add(lbl);
        }

        // =====================================
        // HÀM TẠO BUTTON
        // =====================================

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

            btn.ForeColor =
                System.Drawing.Color.Black;

            btn.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;

            btn.FlatAppearance.BorderColor =
                System.Drawing.Color.LightGray;

            btn.FlatAppearance.BorderSize = 1;

            btn.UseVisualStyleBackColor = false;

            btn.Cursor =
                System.Windows.Forms.Cursors.Hand;
        }

        // =====================================
        // HÀM TẠO DATAGRIDVIEW
        // =====================================

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

            dgv.ReadOnly = true;

            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;

            dgv.MultiSelect = false;

            dgv.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            dgv.AutoGenerateColumns = true;

            dgv.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            dgv.EnableHeadersVisualStyles = false;

            dgv.ColumnHeadersDefaultCellStyle.BackColor =
                System.Drawing.Color.FromArgb(230, 230, 230);

            dgv.DefaultCellStyle.SelectionBackColor =
                System.Drawing.Color.FromArgb(195, 222, 244);

            dgv.DefaultCellStyle.SelectionForeColor =
                System.Drawing.Color.Black;
        }
    }
}
