namespace QuanLyThuVien.Forms
{
    partial class FrmMuonTra
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabMuon = new System.Windows.Forms.TabPage();
            this.btnLapPhieu = new System.Windows.Forms.Button();
            this.dgvSachChon = new System.Windows.Forms.DataGridView();
            this.btnBoSach = new System.Windows.Forms.Button();
            this.btnThemSach = new System.Windows.Forms.Button();
            this.dgvSachCon = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.dtHenTra = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtNgayMuon = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.cboNhanVienMuon = new System.Windows.Forms.ComboBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.btnKiemTra = new System.Windows.Forms.Button();
            this.cboDocGia = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabTra = new System.Windows.Forms.TabPage();
            this.btnTraSach = new System.Windows.Forms.Button();
            this.numPhiPhat = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.cboTinhTrang = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dtNgayTra = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvDangMuon = new System.Windows.Forms.DataGridView();
            this.cboNhanVienTra = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnTaiSachMuon = new System.Windows.Forms.Button();
            this.cboDocGiaTra = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnDong = new System.Windows.Forms.Button();
            this.tabs.SuspendLayout();
            this.tabMuon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSachChon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSachCon)).BeginInit();
            this.tabTra.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPhiPhat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDangMuon)).BeginInit();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tabMuon);
            this.tabs.Controls.Add(this.tabTra);
            this.tabs.Location = new System.Drawing.Point(12, 12);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(1010, 650);
            this.tabs.TabIndex = 0;
            // 
            // tabMuon
            // 
            this.tabMuon.Controls.Add(this.btnLapPhieu);
            this.tabMuon.Controls.Add(this.dgvSachChon);
            this.tabMuon.Controls.Add(this.btnBoSach);
            this.tabMuon.Controls.Add(this.btnThemSach);
            this.tabMuon.Controls.Add(this.dgvSachCon);
            this.tabMuon.Controls.Add(this.label4);
            this.tabMuon.Controls.Add(this.dtHenTra);
            this.tabMuon.Controls.Add(this.label3);
            this.tabMuon.Controls.Add(this.dtNgayMuon);
            this.tabMuon.Controls.Add(this.label2);
            this.tabMuon.Controls.Add(this.cboNhanVienMuon);
            this.tabMuon.Controls.Add(this.lblTrangThai);
            this.tabMuon.Controls.Add(this.btnKiemTra);
            this.tabMuon.Controls.Add(this.cboDocGia);
            this.tabMuon.Controls.Add(this.label1);
            this.tabMuon.Location = new System.Drawing.Point(4, 32);
            this.tabMuon.Name = "tabMuon";
            this.tabMuon.Padding = new System.Windows.Forms.Padding(3);
            this.tabMuon.Size = new System.Drawing.Size(1002, 614);
            this.tabMuon.TabIndex = 0;
            this.tabMuon.Text = "Mượn sách";
            this.tabMuon.UseVisualStyleBackColor = true;
            // 
            // btnLapPhieu
            // 
            this.btnLapPhieu.Location = new System.Drawing.Point(20, 470);
            this.btnLapPhieu.Name = "btnLapPhieu";
            this.btnLapPhieu.Size = new System.Drawing.Size(180, 36);
            this.btnLapPhieu.TabIndex = 14;
            this.btnLapPhieu.Text = "Lập phiếu mượn";
            this.btnLapPhieu.UseVisualStyleBackColor = true;
            this.btnLapPhieu.Click += new System.EventHandler(this.btnLapPhieu_Click);
            // 
            // dgvSachChon
            // 
            this.dgvSachChon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSachChon.Location = new System.Drawing.Point(560, 140);
            this.dgvSachChon.Name = "dgvSachChon";
            this.dgvSachChon.RowHeadersWidth = 51;
            this.dgvSachChon.RowTemplate.Height = 24;
            this.dgvSachChon.Size = new System.Drawing.Size(420, 310);
            this.dgvSachChon.TabIndex = 13;
            // 
            // btnBoSach
            // 
            this.btnBoSach.Location = new System.Drawing.Point(460, 260);
            this.btnBoSach.Name = "btnBoSach";
            this.btnBoSach.Size = new System.Drawing.Size(90, 32);
            this.btnBoSach.TabIndex = 12;
            this.btnBoSach.Text = "<< Bỏ";
            this.btnBoSach.UseVisualStyleBackColor = true;
            this.btnBoSach.Click += new System.EventHandler(this.btnBoSach_Click);
            // 
            // btnThemSach
            // 
            this.btnThemSach.Location = new System.Drawing.Point(460, 220);
            this.btnThemSach.Name = "btnThemSach";
            this.btnThemSach.Size = new System.Drawing.Size(90, 32);
            this.btnThemSach.TabIndex = 11;
            this.btnThemSach.Text = "Thêm >>";
            this.btnThemSach.UseVisualStyleBackColor = true;
            this.btnThemSach.Click += new System.EventHandler(this.btnThemSach_Click);
            // 
            // dgvSachCon
            // 
            this.dgvSachCon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSachCon.Location = new System.Drawing.Point(20, 140);
            this.dgvSachCon.Name = "dgvSachCon";
            this.dgvSachCon.RowHeadersWidth = 51;
            this.dgvSachCon.RowTemplate.Height = 24;
            this.dgvSachCon.Size = new System.Drawing.Size(430, 310);
            this.dgvSachCon.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(630, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 23);
            this.label4.TabIndex = 9;
            this.label4.Text = "Ngày hẹn trả :";
            // 
            // dtHenTra
            // 
            this.dtHenTra.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtHenTra.Location = new System.Drawing.Point(750, 95);
            this.dtHenTra.Name = "dtHenTra";
            this.dtHenTra.Size = new System.Drawing.Size(150, 30);
            this.dtHenTra.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(390, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 7;
            this.label3.Text = "Ngày mượn :";
            // 
            // dtNgayMuon
            // 
            this.dtNgayMuon.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtNgayMuon.Location = new System.Drawing.Point(500, 95);
            this.dtNgayMuon.Name = "dtNgayMuon";
            this.dtNgayMuon.Size = new System.Drawing.Size(120, 30);
            this.dtNgayMuon.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 23);
            this.label2.TabIndex = 5;
            this.label2.Text = "Nhân viên lập phiếu :";
            // 
            // cboNhanVienMuon
            // 
            this.cboNhanVienMuon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhanVienMuon.FormattingEnabled = true;
            this.cboNhanVienMuon.Location = new System.Drawing.Point(190, 95);
            this.cboNhanVienMuon.Name = "cboNhanVienMuon";
            this.cboNhanVienMuon.Size = new System.Drawing.Size(180, 31);
            this.cboNhanVienMuon.TabIndex = 4;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Location = new System.Drawing.Point(20, 60);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(0, 23);
            this.lblTrangThai.TabIndex = 3;
            // 
            // btnKiemTra
            // 
            this.btnKiemTra.Location = new System.Drawing.Point(430, 18);
            this.btnKiemTra.Name = "btnKiemTra";
            this.btnKiemTra.Size = new System.Drawing.Size(160, 32);
            this.btnKiemTra.TabIndex = 2;
            this.btnKiemTra.Text = "Kiểm tra điều kiện";
            this.btnKiemTra.UseVisualStyleBackColor = true;
            this.btnKiemTra.Click += new System.EventHandler(this.btnKiemTra_Click);
            // 
            // cboDocGia
            // 
            this.cboDocGia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDocGia.FormattingEnabled = true;
            this.cboDocGia.Location = new System.Drawing.Point(150, 18);
            this.cboDocGia.Name = "cboDocGia";
            this.cboDocGia.Size = new System.Drawing.Size(260, 31);
            this.cboDocGia.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Độc giả :";
            // 
            // tabTra
            // 
            this.tabTra.Controls.Add(this.btnTraSach);
            this.tabTra.Controls.Add(this.numPhiPhat);
            this.tabTra.Controls.Add(this.label8);
            this.tabTra.Controls.Add(this.cboTinhTrang);
            this.tabTra.Controls.Add(this.label7);
            this.tabTra.Controls.Add(this.dtNgayTra);
            this.tabTra.Controls.Add(this.label6);
            this.tabTra.Controls.Add(this.dgvDangMuon);
            this.tabTra.Controls.Add(this.cboNhanVienTra);
            this.tabTra.Controls.Add(this.label5);
            this.tabTra.Controls.Add(this.btnTaiSachMuon);
            this.tabTra.Controls.Add(this.cboDocGiaTra);
            this.tabTra.Controls.Add(this.label9);
            this.tabTra.Location = new System.Drawing.Point(4, 32);
            this.tabTra.Name = "tabTra";
            this.tabTra.Padding = new System.Windows.Forms.Padding(3);
            this.tabTra.Size = new System.Drawing.Size(1002, 614);
            this.tabTra.TabIndex = 1;
            this.tabTra.Text = "Trả sách";
            this.tabTra.UseVisualStyleBackColor = true;
            // 
            // btnTraSach
            // 
            this.btnTraSach.Location = new System.Drawing.Point(820, 413);
            this.btnTraSach.Name = "btnTraSach";
            this.btnTraSach.Size = new System.Drawing.Size(160, 36);
            this.btnTraSach.TabIndex = 12;
            this.btnTraSach.Text = "Xác nhận trả sách";
            this.btnTraSach.UseVisualStyleBackColor = true;
            this.btnTraSach.Click += new System.EventHandler(this.btnTraSach_Click);
            // 
            // numPhiPhat
            // 
            this.numPhiPhat.Location = new System.Drawing.Point(650, 415);
            this.numPhiPhat.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            this.numPhiPhat.Name = "numPhiPhat";
            this.numPhiPhat.Size = new System.Drawing.Size(150, 30);
            this.numPhiPhat.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(560, 418);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 23);
            this.label8.TabIndex = 10;
            this.label8.Text = "Phí phạt :";
            // 
            // cboTinhTrang
            // 
            this.cboTinhTrang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTinhTrang.FormattingEnabled = true;
            this.cboTinhTrang.Location = new System.Drawing.Point(380, 415);
            this.cboTinhTrang.Name = "cboTinhTrang";
            this.cboTinhTrang.Size = new System.Drawing.Size(160, 31);
            this.cboTinhTrang.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(280, 418);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 23);
            this.label7.TabIndex = 8;
            this.label7.Text = "Tình trạng :";
            // 
            // dtNgayTra
            // 
            this.dtNgayTra.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtNgayTra.Location = new System.Drawing.Point(120, 415);
            this.dtNgayTra.Name = "dtNgayTra";
            this.dtNgayTra.Size = new System.Drawing.Size(140, 30);
            this.dtNgayTra.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 418);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 23);
            this.label6.TabIndex = 6;
            this.label6.Text = "Ngày trả :";
            // 
            // dgvDangMuon
            // 
            this.dgvDangMuon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDangMuon.Location = new System.Drawing.Point(20, 100);
            this.dgvDangMuon.Name = "dgvDangMuon";
            this.dgvDangMuon.RowHeadersWidth = 51;
            this.dgvDangMuon.RowTemplate.Height = 24;
            this.dgvDangMuon.Size = new System.Drawing.Size(960, 300);
            this.dgvDangMuon.TabIndex = 5;
            // 
            // cboNhanVienTra
            // 
            this.cboNhanVienTra.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhanVienTra.FormattingEnabled = true;
            this.cboNhanVienTra.Location = new System.Drawing.Point(190, 55);
            this.cboNhanVienTra.Name = "cboNhanVienTra";
            this.cboNhanVienTra.Size = new System.Drawing.Size(180, 31);
            this.cboNhanVienTra.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(160, 23);
            this.label5.TabIndex = 3;
            this.label5.Text = "Nhân viên nhận trả :";
            // 
            // btnTaiSachMuon
            // 
            this.btnTaiSachMuon.Location = new System.Drawing.Point(430, 18);
            this.btnTaiSachMuon.Name = "btnTaiSachMuon";
            this.btnTaiSachMuon.Size = new System.Drawing.Size(180, 32);
            this.btnTaiSachMuon.TabIndex = 2;
            this.btnTaiSachMuon.Text = "Tải sách đang mượn";
            this.btnTaiSachMuon.UseVisualStyleBackColor = true;
            this.btnTaiSachMuon.Click += new System.EventHandler(this.btnTaiSachMuon_Click);
            // 
            // cboDocGiaTra
            // 
            this.cboDocGiaTra.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDocGiaTra.FormattingEnabled = true;
            this.cboDocGiaTra.Location = new System.Drawing.Point(150, 18);
            this.cboDocGiaTra.Name = "cboDocGiaTra";
            this.cboDocGiaTra.Size = new System.Drawing.Size(260, 31);
            this.cboDocGiaTra.TabIndex = 1;
            this.cboDocGiaTra.SelectedIndexChanged += new System.EventHandler(this.cboDocGiaTra_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 23);
            this.label9.TabIndex = 0;
            this.label9.Text = "Độc giả :";
            // 
            // btnDong
            // 
            this.btnDong.Location = new System.Drawing.Point(920, 670);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(100, 30);
            this.btnDong.TabIndex = 1;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // FrmMuonTra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 712);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.tabs);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmMuonTra";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mượn - Trả sách";
            this.Load += new System.EventHandler(this.FrmMuonTra_Load);
            this.tabs.ResumeLayout(false);
            this.tabMuon.ResumeLayout(false);
            this.tabMuon.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSachChon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSachCon)).EndInit();
            this.tabTra.ResumeLayout(false);
            this.tabTra.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPhiPhat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDangMuon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabMuon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboDocGia;
        private System.Windows.Forms.Button btnKiemTra;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboNhanVienMuon;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtHenTra;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtNgayMuon;
        private System.Windows.Forms.DataGridView dgvSachCon;
        private System.Windows.Forms.Button btnBoSach;
        private System.Windows.Forms.Button btnThemSach;
        private System.Windows.Forms.DataGridView dgvSachChon;
        private System.Windows.Forms.Button btnLapPhieu;
        private System.Windows.Forms.TabPage tabTra;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cboDocGiaTra;
        private System.Windows.Forms.Button btnTaiSachMuon;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboNhanVienTra;
        private System.Windows.Forms.DataGridView dgvDangMuon;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtNgayTra;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboTinhTrang;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numPhiPhat;
        private System.Windows.Forms.Button btnTraSach;
        private System.Windows.Forms.Button btnDong;
    }
}