
namespace QuanLyCongTyDuLich.Forms
{
    partial class FrmKetThucKhaoSat
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TabControl tabKT;
        private System.Windows.Forms.TabPage tabThanhToan;
        private System.Windows.Forms.TabPage tabKhaoSat;

        private System.Windows.Forms.DataGridView dgvDoan;
        private System.Windows.Forms.TextBox txtSoTT;
        private System.Windows.Forms.TextBox txtSoDK;
        private System.Windows.Forms.DateTimePicker dtTT;
        private System.Windows.Forms.NumericUpDown numTien;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Button btnThanhToan;

        private System.Windows.Forms.ComboBox cboLoaiKS;
        private System.Windows.Forms.ComboBox cboDangKy;
        private System.Windows.Forms.TextBox txtMaKS;
        private System.Windows.Forms.DateTimePicker dtGui;
        private System.Windows.Forms.Button btnGui;
        private System.Windows.Forms.DataGridView dgvKS;
        private System.Windows.Forms.TextBox txtKSChon;
        private System.Windows.Forms.DateTimePicker dtPH;
        private System.Windows.Forms.NumericUpDown numDiem;
        private System.Windows.Forms.TextBox txtGopY;
        private System.Windows.Forms.Button btnGhiPH;

        private System.Windows.Forms.Button btnDong;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabKT = new System.Windows.Forms.TabControl();
            this.tabThanhToan = new System.Windows.Forms.TabPage();
            this.tabKhaoSat = new System.Windows.Forms.TabPage();

            this.dgvDoan = new System.Windows.Forms.DataGridView();
            this.txtSoTT = new System.Windows.Forms.TextBox();
            this.txtSoDK = new System.Windows.Forms.TextBox();
            this.dtTT = new System.Windows.Forms.DateTimePicker();
            this.numTien = new System.Windows.Forms.NumericUpDown();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.btnThanhToan = new System.Windows.Forms.Button();

            this.cboLoaiKS = new System.Windows.Forms.ComboBox();
            this.cboDangKy = new System.Windows.Forms.ComboBox();
            this.txtMaKS = new System.Windows.Forms.TextBox();
            this.dtGui = new System.Windows.Forms.DateTimePicker();
            this.btnGui = new System.Windows.Forms.Button();
            this.dgvKS = new System.Windows.Forms.DataGridView();
            this.txtKSChon = new System.Windows.Forms.TextBox();
            this.dtPH = new System.Windows.Forms.DateTimePicker();
            this.numDiem = new System.Windows.Forms.NumericUpDown();
            this.txtGopY = new System.Windows.Forms.TextBox();
            this.btnGhiPH = new System.Windows.Forms.Button();

            this.btnDong = new System.Windows.Forms.Button();

            this.SuspendLayout();
            this.tabKT.SuspendLayout();
            this.tabThanhToan.SuspendLayout();
            this.tabKhaoSat.SuspendLayout();

            // CẤU HÌNH FORM
            this.Name = "FrmKetThucKhaoSat";
            this.Text = "Kết thúc tour - thanh toán đoàn - khảo sát";
            this.ClientSize = new System.Drawing.Size(960, 555);
            this.MinimumSize = new System.Drawing.Size(800, 550);
            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterParent;
            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.BackColor =
                System.Drawing.Color.FromArgb(240, 240, 240);

            // TAB CONTROL
            this.tabKT.Name = "tabKT";
            this.tabKT.SetBounds(15, 12, 930, 486);
            this.tabKT.Anchor =
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right;

            this.tabThanhToan.Text = "Thanh toán sau tour (đoàn)";
            this.tabKhaoSat.Text = "Khảo sát khách hàng";

            this.tabThanhToan.BackColor =
                System.Drawing.Color.FromArgb(245, 245, 245);
            this.tabKhaoSat.BackColor =
                System.Drawing.Color.FromArgb(245, 245, 245);

            this.tabKT.TabPages.Add(this.tabThanhToan);
            this.tabKT.TabPages.Add(this.tabKhaoSat);

            // ==================================
            // TAB 1 - THANH TOÁN ĐOÀN
            // ==================================

            TaoBang(this.dgvDoan, "dgvDoan",
                12, 12, 897, 310);

            this.dgvDoan.Anchor =
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right;

            this.tabThanhToan.Controls.Add(this.dgvDoan);

            TaoNhan(this.tabThanhToan,
                "Số thanh toán", 15, 334, 105);

            TaoO(this.txtSoTT, "txtSoTT",
                120, 331, 190);

            TaoNhan(this.tabThanhToan,
                "Phiếu đoàn", 335, 334, 85);

            TaoO(this.txtSoDK, "txtSoDK",
                420, 331, 180);

            this.txtSoDK.ReadOnly = true;

            TaoNhan(this.tabThanhToan,
                "Ngày thanh toán", 620, 334, 115);

            TaoNgay(this.dtTT, "dtTT",
                737, 331, 170);

            TaoNhan(this.tabThanhToan,
                "Số tiền", 15, 378, 105);

            TaoSo(this.numTien, "numTien",
                120, 375, 190, 0, 100000000000M, 0);

            this.numTien.ThousandsSeparator = true;

            TaoNhan(this.tabThanhToan,
                "Ghi chú", 335, 378, 85);

            TaoO(this.txtGhiChu, "txtGhiChu",
                420, 375, 265);

            TaoNut(this.btnThanhToan,
                "Ghi nhận thanh toán",
                712, 373, 195);

            this.tabThanhToan.Controls.Add(this.txtSoTT);
            this.tabThanhToan.Controls.Add(this.txtSoDK);
            this.tabThanhToan.Controls.Add(this.dtTT);
            this.tabThanhToan.Controls.Add(this.numTien);
            this.tabThanhToan.Controls.Add(this.txtGhiChu);
            this.tabThanhToan.Controls.Add(this.btnThanhToan);

            // ==================================
            // TAB 2 - KHẢO SÁT KHÁCH HÀNG
            // ==================================

            TaoNhan(this.tabKhaoSat,
                "Loại khách", 15, 18, 80);

            TaoCombo(this.cboLoaiKS, "cboLoaiKS",
                100, 15, 110);

            TaoNhan(this.tabKhaoSat,
                "Đăng ký đã kết thúc", 222, 18, 140);

            TaoCombo(this.cboDangKy, "cboDangKy",
                365, 15, 290);

            TaoNhan(this.tabKhaoSat,
                "Mã KS", 15, 58, 80);

            TaoO(this.txtMaKS, "txtMaKS",
                100, 55, 145);

            TaoNhan(this.tabKhaoSat,
                "Ngày gửi", 270, 58, 75);

            TaoNgay(this.dtGui, "dtGui",
                350, 55, 170);

            TaoNut(this.btnGui, "Gửi phiếu khảo sát",
                680, 53, 227);

            TaoBang(this.dgvKS, "dgvKS",
                12, 105, 897, 210);

            TaoNhan(this.tabKhaoSat,
                "Phiếu chọn", 15, 328, 80);

            TaoO(this.txtKSChon, "txtKSChon",
                100, 325, 145);

            this.txtKSChon.ReadOnly = true;

            TaoNhan(this.tabKhaoSat,
                "Ngày phản hồi", 265, 328, 110);

            TaoNgay(this.dtPH, "dtPH",
                378, 325, 175);

            TaoNhan(this.tabKhaoSat,
                "Điểm (1-5)", 577, 328, 90);

            TaoSo(this.numDiem, "numDiem",
                675, 325, 100, 1, 5, 5);

            TaoNhan(this.tabKhaoSat,
                "Góp ý", 15, 382, 80);

            TaoO(this.txtGopY, "txtGopY",
                100, 379, 555);

            TaoNut(this.btnGhiPH, "Ghi nhận góp ý",
                680, 377, 227);

            this.tabKhaoSat.Controls.Add(this.cboLoaiKS);
            this.tabKhaoSat.Controls.Add(this.cboDangKy);
            this.tabKhaoSat.Controls.Add(this.txtMaKS);
            this.tabKhaoSat.Controls.Add(this.dtGui);
            this.tabKhaoSat.Controls.Add(this.btnGui);
            this.tabKhaoSat.Controls.Add(this.dgvKS);
            this.tabKhaoSat.Controls.Add(this.txtKSChon);
            this.tabKhaoSat.Controls.Add(this.dtPH);
            this.tabKhaoSat.Controls.Add(this.numDiem);
            this.tabKhaoSat.Controls.Add(this.txtGopY);
            this.tabKhaoSat.Controls.Add(this.btnGhiPH);

            // NÚT ĐÓNG
            TaoNut(this.btnDong, "Đóng",
                813, 508, 130);

            this.btnDong.Anchor =
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Right;

            this.Controls.Add(this.tabKT);
            this.Controls.Add(this.btnDong);

            // ==================================
            // GẮN SỰ KIỆN
            // ==================================

            this.Load += new System.EventHandler(
                this.FrmKetThucKhaoSat_Load);

            this.dgvDoan.SelectionChanged +=
                new System.EventHandler(
                    this.dgvDoan_SelectionChanged);

            this.btnThanhToan.Click +=
                new System.EventHandler(
                    this.btnThanhToan_Click);

            this.cboLoaiKS.SelectedIndexChanged +=
                new System.EventHandler(
                    this.cboLoaiKS_SelectedIndexChanged);

            this.btnGui.Click +=
                new System.EventHandler(
                    this.btnGui_Click);

            this.dgvKS.SelectionChanged +=
                new System.EventHandler(
                    this.dgvKS_SelectionChanged);

            this.btnGhiPH.Click +=
                new System.EventHandler(
                    this.btnGhiPH_Click);

            this.btnDong.Click +=
                new System.EventHandler(
                    this.btnDong_Click);

            this.tabThanhToan.ResumeLayout(false);
            this.tabKhaoSat.ResumeLayout(false);
            this.tabKT.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void TaoNhan(
            System.Windows.Forms.Control cha,
            string noiDung, int x, int y, int rong)
        {
            var lbl = new System.Windows.Forms.Label();
            lbl.Text = noiDung;
            lbl.SetBounds(x, y, rong, 26);
            lbl.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;
            cha.Controls.Add(lbl);
        }

        private void TaoO(
            System.Windows.Forms.TextBox txt,
            string ten, int x, int y, int rong)
        {
            txt.Name = ten;
            txt.SetBounds(x, y, rong, 26);
        }

        private void TaoCombo(
            System.Windows.Forms.ComboBox cbo,
            string ten, int x, int y, int rong)
        {
            cbo.Name = ten;
            cbo.SetBounds(x, y, rong, 27);
            cbo.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;
        }

        private void TaoNgay(
            System.Windows.Forms.DateTimePicker dt,
            string ten, int x, int y, int rong)
        {
            dt.Name = ten;
            dt.SetBounds(x, y, rong, 27);
            dt.Format =
                System.Windows.Forms.DateTimePickerFormat.Custom;
            dt.CustomFormat = "dd/MM/yyyy";
        }

        private void TaoSo(
            System.Windows.Forms.NumericUpDown num,
            string ten, int x, int y, int rong,
            decimal min, decimal max, decimal giaTri)
        {
            num.Name = ten;
            num.SetBounds(x, y, rong, 27);
            num.Minimum = min;
            num.Maximum = max;
            num.Value = giaTri;
        }

        private void TaoBang(
            System.Windows.Forms.DataGridView dgv,
            string ten, int x, int y, int rong, int cao)
        {
            dgv.Name = ten;
            dgv.SetBounds(x, y, rong, cao);
            dgv.BackgroundColor = System.Drawing.Color.White;
            dgv.BorderStyle =
                System.Windows.Forms.BorderStyle.FixedSingle;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.MultiSelect = false;
            dgv.AutoGenerateColumns = true;
            dgv.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor =
                System.Drawing.Color.FromArgb(230, 230, 230);
            dgv.DefaultCellStyle.SelectionBackColor =
                System.Drawing.Color.FromArgb(195, 222, 244);
            dgv.DefaultCellStyle.SelectionForeColor =
                System.Drawing.Color.Black;
        }

        private void TaoNut(
            System.Windows.Forms.Button btn,
            string noiDung, int x, int y, int rong)
        {
            btn.Text = noiDung;
            btn.SetBounds(x, y, rong, 30);
            btn.BackColor =
                System.Drawing.Color.FromArgb(225, 225, 225);
            btn.ForeColor = System.Drawing.Color.Black;
            btn.FlatStyle =
                System.Windows.Forms.FlatStyle.Flat;
            btn.FlatAppearance.BorderColor =
                System.Drawing.Color.LightGray;
            btn.FlatAppearance.BorderSize = 1;
            btn.UseVisualStyleBackColor = false;
        }
    }
}
