
namespace QuanLyCongTyDuLich.Forms
{
    partial class FrmDangKyLe
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.TextBox txtSo;
        private System.Windows.Forms.ComboBox cboChuyen;
        private System.Windows.Forms.ComboBox cboDiemBan;
        private System.Windows.Forms.TextBox txtTen;
        private System.Windows.Forms.TextBox txtDT;
        private System.Windows.Forms.NumericUpDown numNguoi;
        private System.Windows.Forms.Label lblThanhTien;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button btnDangKy;
        private System.Windows.Forms.Button btnDong;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlMain = new System.Windows.Forms.Panel();
            this.txtSo = new System.Windows.Forms.TextBox();
            this.cboChuyen = new System.Windows.Forms.ComboBox();
            this.cboDiemBan = new System.Windows.Forms.ComboBox();
            this.txtTen = new System.Windows.Forms.TextBox();
            this.txtDT = new System.Windows.Forms.TextBox();
            this.numNguoi =
                new System.Windows.Forms.NumericUpDown();
            this.lblThanhTien =
                new System.Windows.Forms.Label();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.btnDangKy = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)
                (this.numNguoi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)
                (this.dgv)).BeginInit();

            this.pnlMain.SuspendLayout();
            this.SuspendLayout();

            // FORM
            this.Name = "FrmDangKyLe";
            this.Text = "Đăng ký và thanh toán vé khách lẻ";
            this.ClientSize = new System.Drawing.Size(940, 465);
            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormBorderStyle =
                System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.BackColor = System.Drawing.Color.White;

            // PANEL
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.SetBounds(12, 10, 916, 445);
            this.pnlMain.BackColor =
                System.Drawing.Color.FromArgb(240, 240, 240);
            this.pnlMain.BorderStyle =
                System.Windows.Forms.BorderStyle.FixedSingle;

            // SỐ ĐĂNG KÝ
            TaoNhan("Số đăng ký", 20, 19, 100);
            TaoTextBox(this.txtSo, "txtSo",
                125, 16, 185);

            // CHUYẾN
            TaoNhan("Chuyến", 355, 19, 70);
            TaoCombo(this.cboChuyen, "cboChuyen",
                440, 16, 435);

            // ĐIỂM BÁN VÉ
            TaoNhan("Điểm bán vé", 20, 59, 100);
            TaoCombo(this.cboDiemBan, "cboDiemBan",
                125, 56, 185);

            // NGƯỜI ĐĂNG KÝ
            TaoNhan("Người đăng ký", 325, 59, 110);
            TaoTextBox(this.txtTen, "txtTen",
                440, 56, 210);

            // ĐIỆN THOẠI
            TaoNhan("Điện thoại", 665, 59, 75);
            TaoTextBox(this.txtDT, "txtDT",
                750, 56, 125);

            // SỐ NGƯỜI
            TaoNhan("Số người", 20, 101, 95);
            this.numNguoi.Name = "numNguoi";
            this.numNguoi.SetBounds(125, 97, 100, 27);
            this.numNguoi.Minimum = 1;
            this.numNguoi.Maximum = 11;
            this.numNguoi.Value = 1;

            // THÀNH TIỀN
            TaoNhan("Thành tiền", 265, 101, 95);

            this.lblThanhTien.Name = "lblThanhTien";
            this.lblThanhTien.Text = "0 đ";
            this.lblThanhTien.SetBounds(360, 96, 220, 30);
            this.lblThanhTien.TextAlign =
                System.Drawing.ContentAlignment.MiddleLeft;
            this.lblThanhTien.ForeColor =
                System.Drawing.Color.DarkRed;
            this.lblThanhTien.Font = new System.Drawing.Font(
                "Segoe UI", 11F,
                System.Drawing.FontStyle.Bold);

            // NÚT ĐĂNG KÝ
            TaoNut(
                this.btnDangKy,
                "btnDangKy",
                "Đăng ký và thanh toán vé",
                680, 96, 195, 32);

            // BẢNG DANH SÁCH
            this.dgv.Name = "dgv";
            this.dgv.SetBounds(20, 143, 855, 248);
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
                System.Drawing.Color.FromArgb(195, 221, 243);
            this.dgv.DefaultCellStyle.SelectionForeColor =
                System.Drawing.Color.Black;

            // NÚT ĐÓNG
            TaoNut(
                this.btnDong,
                "btnDong",
                "Đóng",
                760, 402, 115, 28);

            // GẮN SỰ KIỆN
            this.Load +=
                new System.EventHandler(
                    this.FrmDangKyLe_Load);

            this.cboChuyen.SelectedIndexChanged +=
                new System.EventHandler(this.TinhTien);

            this.numNguoi.ValueChanged +=
                new System.EventHandler(this.TinhTien);

            this.btnDangKy.Click +=
                new System.EventHandler(
                    this.btnDangKy_Click);

            this.btnDong.Click +=
                new System.EventHandler(
                    this.btnDong_Click);

            // THÊM CONTROL
            this.pnlMain.Controls.Add(this.txtSo);
            this.pnlMain.Controls.Add(this.cboChuyen);
            this.pnlMain.Controls.Add(this.cboDiemBan);
            this.pnlMain.Controls.Add(this.txtTen);
            this.pnlMain.Controls.Add(this.txtDT);
            this.pnlMain.Controls.Add(this.numNguoi);
            this.pnlMain.Controls.Add(this.lblThanhTien);
            this.pnlMain.Controls.Add(this.dgv);
            this.pnlMain.Controls.Add(this.btnDangKy);
            this.pnlMain.Controls.Add(this.btnDong);

            this.Controls.Add(this.pnlMain);

            ((System.ComponentModel.ISupportInitialize)
                (this.numNguoi)).EndInit();
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
            lbl.SetBounds(x, y, rong, 26);
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
            txt.Font =
                new System.Drawing.Font("Segoe UI", 9F);
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

        private void TaoNut(
            System.Windows.Forms.Button btn,
            string ten,
            string noiDung,
            int x, int y, int rong, int cao)
        {
            btn.Name = ten;
            btn.Text = noiDung;
            btn.SetBounds(x, y, rong, cao);
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
