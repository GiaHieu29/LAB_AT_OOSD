
namespace QuanLyCongTyDuLich.Forms
{
    partial class FrmPhanCongHDV
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlMain;

        private System.Windows.Forms.TextBox txtMaPC;
        private System.Windows.Forms.ComboBox cboHDV;
        private System.Windows.Forms.ComboBox cboLoai;
        private System.Windows.Forms.ComboBox cboDoiTuong;

        private System.Windows.Forms.NumericUpDown numThuLao;

        private System.Windows.Forms.Button btnPhanCong;
        private System.Windows.Forms.Button btnDong;

        private System.Windows.Forms.DataGridView dgv;

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
            this.pnlMain = new System.Windows.Forms.Panel();

            this.txtMaPC = new System.Windows.Forms.TextBox();
            this.cboHDV = new System.Windows.Forms.ComboBox();
            this.cboLoai = new System.Windows.Forms.ComboBox();
            this.cboDoiTuong = new System.Windows.Forms.ComboBox();

            this.numThuLao = new System.Windows.Forms.NumericUpDown();

            this.btnPhanCong = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();

            this.dgv = new System.Windows.Forms.DataGridView();

            this.SuspendLayout();
            this.pnlMain.SuspendLayout();

            // =================================
            // CẤU HÌNH FORM CHÍNH
            // =================================

            this.Name = "FrmPhanCongHDV";
            this.Text = "Phân công hướng dẫn viên";

            this.ClientSize =
                new System.Drawing.Size(940, 455);

            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterParent;

            this.FormBorderStyle =
                System.Windows.Forms.FormBorderStyle.FixedSingle;

            this.MaximizeBox = false;

            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;

            this.Font =
                new System.Drawing.Font("Segoe UI", 9F);

            this.BackColor = System.Drawing.Color.White;

            // =================================
            // PANEL NỀN
            // =================================

            this.pnlMain.Name = "pnlMain";

            this.pnlMain.Location =
                new System.Drawing.Point(12, 10);

            this.pnlMain.Size =
                new System.Drawing.Size(916, 435);

            this.pnlMain.BackColor =
                System.Drawing.Color.FromArgb(240, 240, 240);

            this.pnlMain.BorderStyle =
                System.Windows.Forms.BorderStyle.FixedSingle;

            // =================================
            // DÒNG 1: MÃ PHÂN CÔNG, HƯỚNG DẪN VIÊN
            // =================================

            TaoNhan(
                "Mã phân công",
                20, 22, 110);

            TaoO(
                this.txtMaPC,
                "txtMaPC",
                135, 19, 190);

            TaoNhan(
                "Hướng dẫn viên",
                355, 22, 110);

            TaoCombo(
                this.cboHDV,
                "cboHDV",
                470, 19, 405);

            // =================================
            // DÒNG 2: LOẠI KHÁCH, CHUYẾN / ĐOÀN
            // =================================

            TaoNhan(
                "Loại",
                20, 63, 110);

            TaoCombo(
                this.cboLoai,
                "cboLoai",
                135, 60, 190);

            TaoNhan(
                "Chuyến / đoàn",
                355, 63, 110);

            TaoCombo(
                this.cboDoiTuong,
                "cboDoiTuong",
                470, 60, 405);

            // =================================
            // DÒNG 3: THÙ LAO TOUR
            // =================================

            TaoNhan(
                "Thù lao tour",
                20, 104, 110);

            this.numThuLao.Name = "numThuLao";

            this.numThuLao.Location =
                new System.Drawing.Point(135, 101);

            this.numThuLao.Size =
                new System.Drawing.Size(190, 27);

            this.numThuLao.Minimum = 0;
            this.numThuLao.Maximum = 1000000000M;

            this.numThuLao.DecimalPlaces = 0;
            this.numThuLao.ThousandsSeparator = true;

            // =================================
            // NÚT PHÂN CÔNG
            // =================================

            TaoNut(
                this.btnPhanCong,
                "Phân công",
                745, 100, 130);

            this.btnPhanCong.Name = "btnPhanCong";

            // =================================
            // BẢNG DANH SÁCH PHÂN CÔNG
            // =================================

            this.dgv.Name = "dgv";

            this.dgv.Location =
                new System.Drawing.Point(20, 145);

            this.dgv.Size =
                new System.Drawing.Size(855, 240);

            this.dgv.BackgroundColor =
                System.Drawing.Color.White;

            this.dgv.BorderStyle =
                System.Windows.Forms.BorderStyle.FixedSingle;

            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;

            this.dgv.ReadOnly = true;
            this.dgv.MultiSelect = false;

            this.dgv.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            this.dgv.AutoGenerateColumns = true;

            this.dgv.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.dgv.RowHeadersVisible = true;

            this.dgv.EnableHeadersVisualStyles = false;

            this.dgv.ColumnHeadersDefaultCellStyle.BackColor =
                System.Drawing.Color.FromArgb(230, 230, 230);

            this.dgv.DefaultCellStyle.SelectionBackColor =
                System.Drawing.Color.FromArgb(195, 222, 244);

            this.dgv.DefaultCellStyle.SelectionForeColor =
                System.Drawing.Color.Black;

            // =================================
            // NÚT ĐÓNG
            // =================================

            TaoNut(
                this.btnDong,
                "Đóng",
                760, 395, 115);

            this.btnDong.Name = "btnDong";

            // =================================
            // GẮN SỰ KIỆN
            // =================================

            this.Load +=
                new System.EventHandler(
                    this.FrmPhanCongHDV_Load);

            this.cboLoai.SelectedIndexChanged +=
                new System.EventHandler(
                    this.cboLoai_SelectedIndexChanged);

            this.btnPhanCong.Click +=
                new System.EventHandler(
                    this.btnPhanCong_Click);

            this.btnDong.Click +=
                new System.EventHandler(
                    this.btnDong_Click);

            // =================================
            // THÊM CONTROL VÀO PANEL
            // =================================

            this.pnlMain.Controls.Add(this.txtMaPC);
            this.pnlMain.Controls.Add(this.cboHDV);
            this.pnlMain.Controls.Add(this.cboLoai);
            this.pnlMain.Controls.Add(this.cboDoiTuong);
            this.pnlMain.Controls.Add(this.numThuLao);

            this.pnlMain.Controls.Add(this.btnPhanCong);
            this.pnlMain.Controls.Add(this.dgv);
            this.pnlMain.Controls.Add(this.btnDong);

            this.Controls.Add(this.pnlMain);

            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();

            this.ResumeLayout(false);
        }

        // =================================
        // HÀM TẠO LABEL
        // =================================

        private void TaoNhan(
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

            this.pnlMain.Controls.Add(lbl);
        }

        // =================================
        // HÀM TẠO TEXTBOX
        // =================================

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
                new System.Drawing.Size(rong, 27);

            txt.Font =
                new System.Drawing.Font("Segoe UI", 9F);
        }

        // =================================
        // HÀM TẠO COMBOBOX
        // =================================

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
                new System.Drawing.Size(rong, 27);

            cbo.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;
        }

        // =================================
        // HÀM TẠO BUTTON
        // =================================

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
    }
}
