
namespace QuanLyCongTyDuLich.Forms
{
    partial class FrmDanhMuc
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TabControl tabDanhMuc;
        private System.Windows.Forms.TabPage tabPT;
        private System.Windows.Forms.TabPage tabDB;
        private System.Windows.Forms.TabPage tabHDV;
        private System.Windows.Forms.TabPage tabDTQ;

        private System.Windows.Forms.DataGridView dgvPT;
        private System.Windows.Forms.DataGridView dgvDB;
        private System.Windows.Forms.DataGridView dgvHDV;
        private System.Windows.Forms.DataGridView dgvDTQ;

        private System.Windows.Forms.TextBox txtPTMa;
        private System.Windows.Forms.TextBox txtPTTen;
        private System.Windows.Forms.TextBox txtPTGhiChu;

        private System.Windows.Forms.TextBox txtDBMa;
        private System.Windows.Forms.TextBox txtDBTen;
        private System.Windows.Forms.TextBox txtDBDiaChi;
        private System.Windows.Forms.TextBox txtDBDT;

        private System.Windows.Forms.TextBox txtHDVMa;
        private System.Windows.Forms.TextBox txtHDVTen;
        private System.Windows.Forms.TextBox txtHDVDT;
        private System.Windows.Forms.NumericUpDown numLuong;

        private System.Windows.Forms.TextBox txtDTQMa;
        private System.Windows.Forms.TextBox txtDTQTen;
        private System.Windows.Forms.TextBox txtDTQDiaDiem;
        private System.Windows.Forms.TextBox txtDTQNoiDung;
        private System.Windows.Forms.TextBox txtDTQYNghia;

        private System.Windows.Forms.Button btnThemPT;
        private System.Windows.Forms.Button btnThemDB;
        private System.Windows.Forms.Button btnThemHDV;
        private System.Windows.Forms.Button btnThemDTQ;
        private System.Windows.Forms.Button btnDong;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabDanhMuc = new System.Windows.Forms.TabControl();

            this.tabPT = new System.Windows.Forms.TabPage();
            this.tabDB = new System.Windows.Forms.TabPage();
            this.tabHDV = new System.Windows.Forms.TabPage();
            this.tabDTQ = new System.Windows.Forms.TabPage();

            this.dgvPT = new System.Windows.Forms.DataGridView();
            this.dgvDB = new System.Windows.Forms.DataGridView();
            this.dgvHDV = new System.Windows.Forms.DataGridView();
            this.dgvDTQ = new System.Windows.Forms.DataGridView();

            this.txtPTMa = new System.Windows.Forms.TextBox();
            this.txtPTTen = new System.Windows.Forms.TextBox();
            this.txtPTGhiChu = new System.Windows.Forms.TextBox();

            this.txtDBMa = new System.Windows.Forms.TextBox();
            this.txtDBTen = new System.Windows.Forms.TextBox();
            this.txtDBDiaChi = new System.Windows.Forms.TextBox();
            this.txtDBDT = new System.Windows.Forms.TextBox();

            this.txtHDVMa = new System.Windows.Forms.TextBox();
            this.txtHDVTen = new System.Windows.Forms.TextBox();
            this.txtHDVDT = new System.Windows.Forms.TextBox();
            this.numLuong = new System.Windows.Forms.NumericUpDown();

            this.txtDTQMa = new System.Windows.Forms.TextBox();
            this.txtDTQTen = new System.Windows.Forms.TextBox();
            this.txtDTQDiaDiem = new System.Windows.Forms.TextBox();
            this.txtDTQNoiDung = new System.Windows.Forms.TextBox();
            this.txtDTQYNghia = new System.Windows.Forms.TextBox();

            this.btnThemPT = new System.Windows.Forms.Button();
            this.btnThemDB = new System.Windows.Forms.Button();
            this.btnThemHDV = new System.Windows.Forms.Button();
            this.btnThemDTQ = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();

            this.SuspendLayout();
            this.tabDanhMuc.SuspendLayout();
            this.tabPT.SuspendLayout();
            this.tabDB.SuspendLayout();
            this.tabHDV.SuspendLayout();
            this.tabDTQ.SuspendLayout();

            // ==========================
            // FORM CHÍNH
            // ==========================

            this.Name = "FrmDanhMuc";
            this.Text = "Danh mục";

            this.ClientSize =
                new System.Drawing.Size(940, 460);

            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterParent;

            this.FormBorderStyle =
                System.Windows.Forms.FormBorderStyle.FixedSingle;

            this.MaximizeBox = false;

            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;

            this.Font = new System.Drawing.Font(
                "Segoe UI", 9F);

            this.BackColor =
                System.Drawing.Color.FromArgb(240, 240, 240);

            // ==========================
            // TAB CONTROL
            // ==========================

            this.tabDanhMuc.Name = "tabDanhMuc";

            this.tabDanhMuc.Location =
                new System.Drawing.Point(15, 12);

            this.tabDanhMuc.Size =
                new System.Drawing.Size(910, 397);

            this.tabDanhMuc.TabPages.Add(this.tabPT);
            this.tabDanhMuc.TabPages.Add(this.tabDB);
            this.tabDanhMuc.TabPages.Add(this.tabHDV);
            this.tabDanhMuc.TabPages.Add(this.tabDTQ);

            this.tabPT.Text = "Phương tiện";
            this.tabDB.Text = "Điểm bán vé";
            this.tabHDV.Text = "Hướng dẫn viên";
            this.tabDTQ.Text = "Điểm tham quan";

            this.tabPT.BackColor =
                System.Drawing.Color.FromArgb(245, 245, 245);

            this.tabDB.BackColor = this.tabPT.BackColor;
            this.tabHDV.BackColor = this.tabPT.BackColor;
            this.tabDTQ.BackColor = this.tabPT.BackColor;

            // ==========================
            // CÁC BẢNG DỮ LIỆU
            // ==========================

            TaoBang(this.dgvPT);
            TaoBang(this.dgvDB);
            TaoBang(this.dgvHDV);
            TaoBang(this.dgvDTQ);

            this.dgvPT.Name = "dgvPT";
            this.dgvDB.Name = "dgvDB";
            this.dgvHDV.Name = "dgvHDV";
            this.dgvDTQ.Name = "dgvDTQ";

            // ==========================
            // TAB 1: PHƯƠNG TIỆN
            // ==========================

            TaoNhan(this.tabPT, "Mã PT", 28, 292);
            TaoNhan(this.tabPT, "Tên PT", 360, 292);
            TaoNhan(this.tabPT, "Ghi chú", 28, 329);

            TaoO(this.txtPTMa, "txtPTMa", 100, 289, 205);
            TaoO(this.txtPTTen, "txtPTTen", 430, 289, 285);
            TaoO(this.txtPTGhiChu, "txtPTGhiChu", 100, 326, 460);

            TaoNut(this.btnThemPT, "Thêm", 685, 325);

            this.tabPT.Controls.Add(this.dgvPT);
            this.tabPT.Controls.Add(this.txtPTMa);
            this.tabPT.Controls.Add(this.txtPTTen);
            this.tabPT.Controls.Add(this.txtPTGhiChu);
            this.tabPT.Controls.Add(this.btnThemPT);

            // ==========================
            // TAB 2: ĐIỂM BÁN VÉ
            // ==========================

            TaoNhan(this.tabDB, "Mã điểm bán", 28, 290);
            TaoNhan(this.tabDB, "Tên điểm bán", 360, 290);
            TaoNhan(this.tabDB, "Địa chỉ", 28, 329);
            TaoNhan(this.tabDB, "Điện thoại", 490, 329);

            TaoO(this.txtDBMa, "txtDBMa", 125, 287, 180);
            TaoO(this.txtDBTen, "txtDBTen", 465, 287, 315);
            TaoO(this.txtDBDiaChi, "txtDBDiaChi", 125, 326, 300);
            TaoO(this.txtDBDT, "txtDBDT", 575, 326, 180);

            TaoNut(this.btnThemDB, "Thêm", 790, 325);

            this.tabDB.Controls.Add(this.dgvDB);
            this.tabDB.Controls.Add(this.txtDBMa);
            this.tabDB.Controls.Add(this.txtDBTen);
            this.tabDB.Controls.Add(this.txtDBDiaChi);
            this.tabDB.Controls.Add(this.txtDBDT);
            this.tabDB.Controls.Add(this.btnThemDB);

            // ==========================
            // TAB 3: HƯỚNG DẪN VIÊN
            // ==========================

            TaoNhan(this.tabHDV, "Mã HDV", 28, 290);
            TaoNhan(this.tabHDV, "Họ tên", 360, 290);
            TaoNhan(this.tabHDV, "Điện thoại", 28, 329);
            TaoNhan(this.tabHDV, "Lương căn bản", 365, 329);

            TaoO(this.txtHDVMa, "txtHDVMa", 120, 287, 185);
            TaoO(this.txtHDVTen, "txtHDVTen", 430, 287, 315);
            TaoO(this.txtHDVDT, "txtHDVDT", 120, 326, 185);

            this.numLuong.Name = "numLuong";
            this.numLuong.Location =
                new System.Drawing.Point(480, 326);

            this.numLuong.Size =
                new System.Drawing.Size(210, 25);

            this.numLuong.Maximum = 1000000000M;
            this.numLuong.Minimum = 0M;
            this.numLuong.DecimalPlaces = 0;
            this.numLuong.ThousandsSeparator = true;

            TaoNut(this.btnThemHDV, "Thêm", 750, 325);

            this.tabHDV.Controls.Add(this.dgvHDV);
            this.tabHDV.Controls.Add(this.txtHDVMa);
            this.tabHDV.Controls.Add(this.txtHDVTen);
            this.tabHDV.Controls.Add(this.txtHDVDT);
            this.tabHDV.Controls.Add(this.numLuong);
            this.tabHDV.Controls.Add(this.btnThemHDV);

            // ==========================
            // TAB 4: ĐIỂM THAM QUAN
            // ==========================

            TaoNhan(this.tabDTQ, "Mã điểm TQ", 28, 289);
            TaoNhan(this.tabDTQ, "Tên điểm TQ", 360, 289);

            TaoNhan(this.tabDTQ, "Địa điểm", 28, 325);
            TaoNhan(this.tabDTQ, "Nội dung", 360, 325);
            TaoNhan(this.tabDTQ, "Ý nghĩa", 610, 325);

            TaoO(this.txtDTQMa, "txtDTQMa", 120, 286, 190);
            TaoO(this.txtDTQTen, "txtDTQTen", 460, 286, 350);

            TaoO(this.txtDTQDiaDiem,
                "txtDTQDiaDiem", 120, 322, 190);

            TaoO(this.txtDTQNoiDung,
                "txtDTQNoiDung", 430, 322, 155);

            TaoO(this.txtDTQYNghia,
                "txtDTQYNghia", 675, 322, 115);

            TaoNut(this.btnThemDTQ, "Thêm", 807, 321);
            this.btnThemDTQ.Size =
                new System.Drawing.Size(65, 29);

            this.tabDTQ.Controls.Add(this.dgvDTQ);
            this.tabDTQ.Controls.Add(this.txtDTQMa);
            this.tabDTQ.Controls.Add(this.txtDTQTen);
            this.tabDTQ.Controls.Add(this.txtDTQDiaDiem);
            this.tabDTQ.Controls.Add(this.txtDTQNoiDung);
            this.tabDTQ.Controls.Add(this.txtDTQYNghia);
            this.tabDTQ.Controls.Add(this.btnThemDTQ);

            // ==========================
            // NÚT ĐÓNG
            // ==========================

            TaoNut(this.btnDong, "Đóng", 800, 418);

            this.btnDong.Name = "btnDong";
            this.btnDong.Size =
                new System.Drawing.Size(120, 29);

            // ==========================
            // GẮN SỰ KIỆN
            // ==========================

            this.Load +=
                new System.EventHandler(
                    this.FrmDanhMuc_Load);

            this.btnThemPT.Click +=
                new System.EventHandler(
                    this.btnThemPT_Click);

            this.btnThemDB.Click +=
                new System.EventHandler(
                    this.btnThemDB_Click);

            this.btnThemHDV.Click +=
                new System.EventHandler(
                    this.btnThemHDV_Click);

            this.btnThemDTQ.Click +=
                new System.EventHandler(
                    this.btnThemDTQ_Click);

            this.btnDong.Click +=
                new System.EventHandler(
                    this.btnDong_Click);

            // ==========================
            // HOÀN THÀNH GIAO DIỆN
            // ==========================

            this.Controls.Add(this.tabDanhMuc);
            this.Controls.Add(this.btnDong);

            this.tabPT.ResumeLayout(false);
            this.tabPT.PerformLayout();

            this.tabDB.ResumeLayout(false);
            this.tabDB.PerformLayout();

            this.tabHDV.ResumeLayout(false);
            this.tabHDV.PerformLayout();

            this.tabDTQ.ResumeLayout(false);
            this.tabDTQ.PerformLayout();

            this.tabDanhMuc.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // ==========================
        // HÀM THIẾT KẾ DATAGRIDVIEW
        // ==========================

        private void TaoBang(
            System.Windows.Forms.DataGridView dgv)
        {
            dgv.Location =
                new System.Drawing.Point(15, 15);

            dgv.Size =
                new System.Drawing.Size(872, 260);

            dgv.BackgroundColor =
                System.Drawing.Color.White;

            dgv.BorderStyle =
                System.Windows.Forms.BorderStyle.FixedSingle;

            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;

            dgv.RowHeadersVisible = true;

            dgv.SelectionMode =
                System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            dgv.MultiSelect = false;
            dgv.AutoGenerateColumns = true;

            dgv.AutoSizeColumnsMode =
                System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            dgv.ColumnHeadersHeightSizeMode =
                System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        }

        // ==========================
        // HÀM TẠO LABEL
        // ==========================

        private void TaoNhan(
            System.Windows.Forms.Control cha,
            string noiDung,
            int x,
            int y)
        {
            System.Windows.Forms.Label lbl =
                new System.Windows.Forms.Label();

            lbl.Text = noiDung;
            lbl.AutoSize = true;

            lbl.Location =
                new System.Drawing.Point(x, y + 4);

            lbl.Font =
                new System.Drawing.Font("Segoe UI", 9F);

            cha.Controls.Add(lbl);
        }

        // ==========================
        // HÀM TẠO TEXTBOX
        // ==========================

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

            txt.Font =
                new System.Drawing.Font("Segoe UI", 9F);
        }

        // ==========================
        // HÀM TẠO BUTTON
        // ==========================

        private void TaoNut(
            System.Windows.Forms.Button btn,
            string noiDung,
            int x,
            int y)
        {
            btn.Text = noiDung;

            btn.Location =
                new System.Drawing.Point(x, y);

            btn.Size =
                new System.Drawing.Size(100, 29);

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

            btn.Font =
                new System.Drawing.Font("Segoe UI", 9F);
        }
    }
}
