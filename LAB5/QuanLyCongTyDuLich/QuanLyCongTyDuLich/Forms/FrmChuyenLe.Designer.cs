
namespace QuanLyCongTyDuLich.Forms
{
    partial class FrmChuyenLe
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.TextBox txtMa;
        private System.Windows.Forms.ComboBox cboTour;
        private System.Windows.Forms.DateTimePicker dtDi;
        private System.Windows.Forms.Label lblNgayVe;
        private System.Windows.Forms.TextBox txtDon;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnDongDK;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.DataGridView dgv;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlMain = new System.Windows.Forms.Panel();
            this.txtMa = new System.Windows.Forms.TextBox();
            this.cboTour = new System.Windows.Forms.ComboBox();
            this.dtDi = new System.Windows.Forms.DateTimePicker();
            this.lblNgayVe = new System.Windows.Forms.Label();
            this.txtDon = new System.Windows.Forms.TextBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnDongDK = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();

            ((System.ComponentModel.ISupportInitialize)
                (this.dgv)).BeginInit();

            this.pnlMain.SuspendLayout();
            this.SuspendLayout();

            // FORM
            this.Name = "FrmChuyenLe";
            this.Text = "Lịch chuyến khách lẻ";
            this.ClientSize = new System.Drawing.Size(940, 455);
            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormBorderStyle =
                System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.BackColor = System.Drawing.Color.White;

            // PANEL CHÍNH
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.SetBounds(12, 10, 916, 435);
            this.pnlMain.BackColor =
                System.Drawing.Color.FromArgb(240, 240, 240);
            this.pnlMain.BorderStyle =
                System.Windows.Forms.BorderStyle.FixedSingle;

            // MÃ CHUYẾN
            TaoNhan("Mã chuyến", 20, 22, 100);
            TaoTextBox(this.txtMa, "txtMa",
                125, 19, 200);

            // TOUR
            TaoNhan("Tour", 370, 22, 65);
            this.cboTour.Name = "cboTour";
            this.cboTour.SetBounds(440, 19, 440, 27);
            this.cboTour.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            // NGÀY ĐI
            TaoNhan("Ngày đi", 20, 62, 100);
            this.dtDi.Name = "dtDi";
            this.dtDi.SetBounds(125, 59, 200, 27);
            this.dtDi.Format =
                System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDi.CustomFormat = "dd/MM/yyyy";

            // NGÀY VỀ
            TaoNhan("Ngày về", 370, 62, 65);
            this.lblNgayVe.Name = "lblNgayVe";
            this.lblNgayVe.Text = "-";
            this.lblNgayVe.SetBounds(440, 59, 200, 27);
            this.lblNgayVe.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;
            this.lblNgayVe.Font = new System.Drawing.Font(
                "Segoe UI", 9F,
                System.Drawing.FontStyle.Bold);

            // ĐỊA ĐIỂM ĐÓN
            TaoNhan("Địa điểm đón", 20, 103, 100);
            TaoTextBox(this.txtDon, "txtDon",
                125, 100, 465);

            // NÚT TẠO CHUYẾN
            TaoNut(this.btnThem, "btnThem",
                "Tạo chuyến", 615, 98, 125);

            // NÚT ĐÓNG ĐĂNG KÝ
            TaoNut(this.btnDongDK, "btnDongDK",
                "Đóng đăng ký", 750, 98, 130);

            // BẢNG DANH SÁCH CHUYẾN
            this.dgv.Name = "dgv";
            this.dgv.SetBounds(20, 145, 860, 233);
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
                System.Drawing.Color.FromArgb(194, 222, 244);
            this.dgv.DefaultCellStyle.SelectionForeColor =
                System.Drawing.Color.Black;

            // NÚT ĐÓNG FORM
            TaoNut(this.btnDong, "btnDong",
                "Đóng", 765, 390, 115);

            // GẮN SỰ KIỆN
            this.Load +=
                new System.EventHandler(this.FrmChuyenLe_Load);

            this.cboTour.SelectedIndexChanged +=
                new System.EventHandler(this.TinhNgayVe);

            this.dtDi.ValueChanged +=
                new System.EventHandler(this.TinhNgayVe);

            this.btnThem.Click +=
                new System.EventHandler(this.btnThem_Click);

            this.btnDongDK.Click +=
                new System.EventHandler(this.btnDongDK_Click);

            this.btnDong.Click +=
                new System.EventHandler(this.btnDong_Click);

            // THÊM CONTROL
            this.pnlMain.Controls.Add(this.txtMa);
            this.pnlMain.Controls.Add(this.cboTour);
            this.pnlMain.Controls.Add(this.dtDi);
            this.pnlMain.Controls.Add(this.lblNgayVe);
            this.pnlMain.Controls.Add(this.txtDon);
            this.pnlMain.Controls.Add(this.btnThem);
            this.pnlMain.Controls.Add(this.btnDongDK);
            this.pnlMain.Controls.Add(this.dgv);
            this.pnlMain.Controls.Add(this.btnDong);

            this.Controls.Add(this.pnlMain);

            ((System.ComponentModel.ISupportInitialize)
                (this.dgv)).EndInit();

            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);
        }

        private void TaoNhan(
            string noiDung, int x, int y, int rong)
        {
            var lbl = new System.Windows.Forms.Label();
            lbl.Text = noiDung;
            lbl.SetBounds(x, y, rong, 25);
            lbl.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;
            this.pnlMain.Controls.Add(lbl);
        }

        private void TaoTextBox(
            System.Windows.Forms.TextBox txt,
            string ten, int x, int y, int rong)
        {
            txt.Name = ten;
            txt.SetBounds(x, y, rong, 27);
        }

        private void TaoNut(
            System.Windows.Forms.Button btn,
            string ten, string noiDung,
            int x, int y, int rong)
        {
            btn.Name = ten;
            btn.Text = noiDung;
            btn.SetBounds(x, y, rong, 28);
            btn.BackColor =
                System.Drawing.Color.FromArgb(225, 225, 225);
            btn.ForeColor = System.Drawing.Color.Black;
            btn.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor =
                System.Drawing.Color.LightGray;
            btn.UseVisualStyleBackColor = false;
        }
    }
}
