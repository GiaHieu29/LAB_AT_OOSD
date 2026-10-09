
namespace QuanLyCongTyDuLich.Forms
{
    partial class FrmTour
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTour;
        private System.Windows.Forms.ComboBox cboTour;
        private System.Windows.Forms.TabControl tabTour;

        private System.Windows.Forms.TabPage tabTrangTour;
        private System.Windows.Forms.TabPage tabDiemDung;
        private System.Windows.Forms.TabPage tabPhuongTien;
        private System.Windows.Forms.TabPage tabThamQuan;

        private System.Windows.Forms.DataGridView dgvTour;
        private System.Windows.Forms.DataGridView dgvDiemDung;
        private System.Windows.Forms.DataGridView dgvChang;
        private System.Windows.Forms.DataGridView dgvTQ;

        private System.Windows.Forms.TextBox txtMa;
        private System.Windows.Forms.TextBox txtTen;
        private System.Windows.Forms.TextBox txtMoTa;

        private System.Windows.Forms.NumericUpDown numNgay;
        private System.Windows.Forms.NumericUpDown numDem;
        private System.Windows.Forms.NumericUpDown numGia;

        private System.Windows.Forms.Button btnThemTour;

        private System.Windows.Forms.NumericUpDown numThuTu;
        private System.Windows.Forms.TextBox txtDiemDung;
        private System.Windows.Forms.CheckBox chkDoiPT;
        private System.Windows.Forms.CheckBox chkAn;
        private System.Windows.Forms.CheckBox chkKS;
        private System.Windows.Forms.NumericUpDown numSao;
        private System.Windows.Forms.TextBox txtGhiChuDD;
        private System.Windows.Forms.Button btnThemDD;

        private System.Windows.Forms.NumericUpDown numChang;
        private System.Windows.Forms.ComboBox cboPT;
        private System.Windows.Forms.TextBox txtGhiChuPT;
        private System.Windows.Forms.Button btnThemChang;

        private System.Windows.Forms.ComboBox cboDTQ;
        private System.Windows.Forms.NumericUpDown numThuTuTQ;
        private System.Windows.Forms.Button btnThemTQ;

        private System.Windows.Forms.Button btnDong;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTour = new System.Windows.Forms.Label();
            this.cboTour = new System.Windows.Forms.ComboBox();
            this.tabTour = new System.Windows.Forms.TabControl();

            this.tabTrangTour = new System.Windows.Forms.TabPage();
            this.tabDiemDung = new System.Windows.Forms.TabPage();
            this.tabPhuongTien = new System.Windows.Forms.TabPage();
            this.tabThamQuan = new System.Windows.Forms.TabPage();

            this.dgvTour = new System.Windows.Forms.DataGridView();
            this.dgvDiemDung = new System.Windows.Forms.DataGridView();
            this.dgvChang = new System.Windows.Forms.DataGridView();
            this.dgvTQ = new System.Windows.Forms.DataGridView();

            this.txtMa = new System.Windows.Forms.TextBox();
            this.txtTen = new System.Windows.Forms.TextBox();
            this.txtMoTa = new System.Windows.Forms.TextBox();

            this.numNgay = new System.Windows.Forms.NumericUpDown();
            this.numDem = new System.Windows.Forms.NumericUpDown();
            this.numGia = new System.Windows.Forms.NumericUpDown();

            this.btnThemTour = new System.Windows.Forms.Button();

            this.numThuTu = new System.Windows.Forms.NumericUpDown();
            this.txtDiemDung = new System.Windows.Forms.TextBox();

            this.chkDoiPT = new System.Windows.Forms.CheckBox();
            this.chkAn = new System.Windows.Forms.CheckBox();
            this.chkKS = new System.Windows.Forms.CheckBox();

            this.numSao = new System.Windows.Forms.NumericUpDown();
            this.txtGhiChuDD = new System.Windows.Forms.TextBox();
            this.btnThemDD = new System.Windows.Forms.Button();

            this.numChang = new System.Windows.Forms.NumericUpDown();
            this.cboPT = new System.Windows.Forms.ComboBox();
            this.txtGhiChuPT = new System.Windows.Forms.TextBox();
            this.btnThemChang = new System.Windows.Forms.Button();

            this.cboDTQ = new System.Windows.Forms.ComboBox();
            this.numThuTuTQ = new System.Windows.Forms.NumericUpDown();
            this.btnThemTQ = new System.Windows.Forms.Button();

            this.btnDong = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // =============================
            // CẤU HÌNH FORM
            // =============================

            this.Name = "FrmTour";
            this.Text = "Tour - hành trình";

            this.ClientSize = new System.Drawing.Size(930, 465);

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

            // =============================
            // TOUR ĐANG CHỌN
            // =============================

            TaoNhan(this, "Tour đang chọn (cho các tab hành trình)",
                20, 17, 265);

            this.cboTour.Name = "cboTour";

            this.cboTour.SetBounds(295, 15, 310, 26);

            this.cboTour.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.cboTour.SelectedIndexChanged +=
                new System.EventHandler(
                    this.cboTour_SelectedIndexChanged);

            this.Controls.Add(this.cboTour);

            // =============================
            // TAB CONTROL
            // =============================

            this.tabTour.Name = "tabTour";
            this.tabTour.SetBounds(15, 53, 900, 363);

            this.tabTrangTour.Text = "Tour";
            this.tabDiemDung.Text = "Điểm dừng";
            this.tabPhuongTien.Text = "Phương tiện theo chặng";
            this.tabThamQuan.Text = "Điểm tham quan";

            this.tabTrangTour.BackColor =
                System.Drawing.Color.FromArgb(245, 245, 245);

            this.tabDiemDung.BackColor =
                this.tabTrangTour.BackColor;

            this.tabPhuongTien.BackColor =
                this.tabTrangTour.BackColor;

            this.tabThamQuan.BackColor =
                this.tabTrangTour.BackColor;

            this.tabTour.TabPages.Add(this.tabTrangTour);
            this.tabTour.TabPages.Add(this.tabDiemDung);
            this.tabTour.TabPages.Add(this.tabPhuongTien);
            this.tabTour.TabPages.Add(this.tabThamQuan);

            this.Controls.Add(this.tabTour);

            // =============================
            // DATA GRID VIEW
            // =============================

            TaoBang(this.dgvTour, "dgvTour");
            TaoBang(this.dgvDiemDung, "dgvDiemDung");
            TaoBang(this.dgvChang, "dgvChang");
            TaoBang(this.dgvTQ, "dgvTQ");

            this.tabTrangTour.Controls.Add(this.dgvTour);
            this.tabDiemDung.Controls.Add(this.dgvDiemDung);
            this.tabPhuongTien.Controls.Add(this.dgvChang);
            this.tabThamQuan.Controls.Add(this.dgvTQ);

            // =============================
            // TAB 1: TOUR
            // =============================

            TaoNhan(this.tabTrangTour, "Mã tour", 17, 243, 65);
            TaoNhan(this.tabTrangTour, "Tên tour", 306, 243, 65);

            TaoO(this.txtMa, "txtMa", 90, 240, 195);
            TaoO(this.txtTen, "txtTen", 378, 240, 330);

            TaoNhan(this.tabTrangTour, "Số ngày", 17, 278, 65);
            TaoNhan(this.tabTrangTour, "Số đêm", 195, 278, 65);
            TaoNhan(this.tabTrangTour, "Đơn giá / khách", 385, 278, 110);

            TaoSo(this.numNgay, "numNgay",
                90, 275, 80, 1, 365, 3);

            TaoSo(this.numDem, "numDem",
                260, 275, 80, 0, 365, 2);

            TaoSo(this.numGia, "numGia",
                500, 275, 210, 0, 1000000000, 0);

            this.numGia.ThousandsSeparator = true;

            TaoNhan(this.tabTrangTour, "Mô tả", 17, 313, 65);

            TaoO(this.txtMoTa, "txtMoTa",
                90, 309, 610);

            TaoNut(this.btnThemTour,
                "Thêm tour", 750, 307, 120);

            this.tabTrangTour.Controls.Add(this.txtMa);
            this.tabTrangTour.Controls.Add(this.txtTen);
            this.tabTrangTour.Controls.Add(this.numNgay);
            this.tabTrangTour.Controls.Add(this.numDem);
            this.tabTrangTour.Controls.Add(this.numGia);
            this.tabTrangTour.Controls.Add(this.txtMoTa);
            this.tabTrangTour.Controls.Add(this.btnThemTour);

            // =============================
            // TAB 2: ĐIỂM DỪNG
            // =============================

            TaoNhan(this.tabDiemDung, "Thứ tự",
                17, 245, 60);

            TaoSo(this.numThuTu, "numThuTu",
                76, 242, 85, 1, 999, 1);

            TaoNhan(this.tabDiemDung,
                "Tên điểm dừng", 190, 245, 105);

            TaoO(this.txtDiemDung, "txtDiemDung",
                300, 242, 355);

            TaoCheck(this.chkDoiPT, "chkDoiPT",
                "Đổi phương tiện", 17, 277);

            TaoCheck(this.chkAn, "chkAn",
                "Có nơi ăn", 202, 277);

            TaoCheck(this.chkKS, "chkKS",
                "Có khách sạn", 362, 277);

            TaoNhan(this.tabDiemDung, "Hạng sao",
                548, 278, 75);

            TaoSo(this.numSao, "numSao",
                625, 274, 85, 2, 5, 2);

            this.numSao.Enabled = false;

            TaoNhan(this.tabDiemDung, "Ghi chú",
                17, 314, 65);

            TaoO(this.txtGhiChuDD, "txtGhiChuDD",
                90, 309, 590);

            TaoNut(this.btnThemDD,
                "Thêm điểm dừng", 718, 307, 151);

            this.tabDiemDung.Controls.Add(this.numThuTu);
            this.tabDiemDung.Controls.Add(this.txtDiemDung);
            this.tabDiemDung.Controls.Add(this.chkDoiPT);
            this.tabDiemDung.Controls.Add(this.chkAn);
            this.tabDiemDung.Controls.Add(this.chkKS);
            this.tabDiemDung.Controls.Add(this.numSao);
            this.tabDiemDung.Controls.Add(this.txtGhiChuDD);
            this.tabDiemDung.Controls.Add(this.btnThemDD);

            // =============================
            // TAB 3: PHƯƠNG TIỆN
            // =============================

            TaoNhan(this.tabPhuongTien, "Chặng thứ",
                17, 251, 85);

            TaoSo(this.numChang, "numChang",
                110, 247, 85, 1, 999, 1);

            TaoNhan(this.tabPhuongTien, "Phương tiện",
                225, 251, 95);

            TaoCombo(this.cboPT, "cboPT",
                322, 247, 335);

            TaoNhan(this.tabPhuongTien, "Ghi chú",
                17, 296, 65);

            TaoO(this.txtGhiChuPT, "txtGhiChuPT",
                110, 292, 550);

            TaoNut(this.btnThemChang,
                "Gắn phương tiện", 710, 290, 158);

            this.tabPhuongTien.Controls.Add(this.numChang);
            this.tabPhuongTien.Controls.Add(this.cboPT);
            this.tabPhuongTien.Controls.Add(this.txtGhiChuPT);
            this.tabPhuongTien.Controls.Add(this.btnThemChang);

            // =============================
            // TAB 4: ĐIỂM THAM QUAN
            // =============================

            TaoNhan(this.tabThamQuan, "Điểm tham quan",
                17, 254, 125);

            TaoCombo(this.cboDTQ, "cboDTQ",
                150, 249, 350);

            TaoNhan(this.tabThamQuan, "Thứ tự",
                520, 254, 55);

            TaoSo(this.numThuTuTQ, "numThuTuTQ",
                590, 249, 85, 1, 999, 1);

            TaoNut(this.btnThemTQ,
                "Gắn điểm TQ", 715, 247, 151);

            this.tabThamQuan.Controls.Add(this.cboDTQ);
            this.tabThamQuan.Controls.Add(this.numThuTuTQ);
            this.tabThamQuan.Controls.Add(this.btnThemTQ);

            // =============================
            // NÚT ĐÓNG
            // =============================

            TaoNut(this.btnDong, "Đóng",
                789, 426, 125);

            this.Controls.Add(this.btnDong);

            // =============================
            // GẮN SỰ KIỆN
            // =============================

            this.Load +=
                new System.EventHandler(this.FrmTour_Load);

            this.chkKS.CheckedChanged +=
                new System.EventHandler(
                    this.chkKS_CheckedChanged);

            this.btnThemTour.Click +=
                new System.EventHandler(
                    this.btnThemTour_Click);

            this.btnThemDD.Click +=
                new System.EventHandler(
                    this.btnThemDD_Click);

            this.btnThemChang.Click +=
                new System.EventHandler(
                    this.btnThemChang_Click);

            this.btnThemTQ.Click +=
                new System.EventHandler(
                    this.btnThemTQ_Click);

            this.btnDong.Click +=
                new System.EventHandler(
                    this.btnDong_Click);

            this.ResumeLayout(false);
        }

        // =============================
        // HÀM TẠO DATAGRIDVIEW
        // =============================

        private void TaoBang(
            System.Windows.Forms.DataGridView dgv,
            string ten)
        {
            dgv.Name = ten;

            dgv.Location = new System.Drawing.Point(12, 12);
            dgv.Size = new System.Drawing.Size(870, 218);

            dgv.BackgroundColor = System.Drawing.Color.White;

            dgv.BorderStyle =
                System.Windows.Forms.BorderStyle.FixedSingle;

            dgv.ReadOnly = true;

            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;

            dgv.AutoGenerateColumns = true;

            dgv.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            dgv.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            dgv.MultiSelect = false;
        }

        // =============================
        // HÀM TẠO LABEL
        // =============================

        private void TaoNhan(
            System.Windows.Forms.Control cha,
            string noiDung,
            int x,
            int y,
            int rong)
        {
            var lbl = new System.Windows.Forms.Label();

            lbl.Text = noiDung;
            lbl.AutoSize = false;
            lbl.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;

            lbl.Location = new System.Drawing.Point(x, y);
            lbl.Size = new System.Drawing.Size(rong, 24);

            cha.Controls.Add(lbl);
        }

        // =============================
        // HÀM TẠO TEXTBOX
        // =============================

        private void TaoO(
            System.Windows.Forms.TextBox txt,
            string ten,
            int x,
            int y,
            int rong)
        {
            txt.Name = ten;
            txt.Location = new System.Drawing.Point(x, y);
            txt.Size = new System.Drawing.Size(rong, 25);
        }

        // =============================
        // HÀM TẠO NUMERICUPDOWN
        // =============================

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
            num.Location = new System.Drawing.Point(x, y);
            num.Size = new System.Drawing.Size(rong, 25);
            num.Minimum = min;
            num.Maximum = max;
            num.Value = giaTri;
        }

        // =============================
        // HÀM TẠO COMBOBOX
        // =============================

        private void TaoCombo(
            System.Windows.Forms.ComboBox cbo,
            string ten,
            int x,
            int y,
            int rong)
        {
            cbo.Name = ten;
            cbo.Location = new System.Drawing.Point(x, y);
            cbo.Size = new System.Drawing.Size(rong, 26);

            cbo.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;
        }

        // =============================
        // HÀM TẠO CHECKBOX
        // =============================

        private void TaoCheck(
            System.Windows.Forms.CheckBox chk,
            string ten,
            string noiDung,
            int x,
            int y)
        {
            chk.Name = ten;
            chk.Text = noiDung;
            chk.AutoSize = true;
            chk.Location = new System.Drawing.Point(x, y);
        }

        // =============================
        // HÀM TẠO BUTTON
        // =============================

        private void TaoNut(
            System.Windows.Forms.Button btn,
            string noiDung,
            int x,
            int y,
            int rong)
        {
            btn.Text = noiDung;

            btn.Location = new System.Drawing.Point(x, y);
            btn.Size = new System.Drawing.Size(rong, 28);

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
