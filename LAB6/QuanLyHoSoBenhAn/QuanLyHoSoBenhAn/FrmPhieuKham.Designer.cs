
namespace QuanLyHoSoBenhAn
{
    partial class FrmPhieuKham
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox
            txtMaPhieu, txtTrieuChung, txtHuyetAp,
            txtChanDoan, txtHuongDieuTri, txtGhiChu,
            txtTimKiem;

        private System.Windows.Forms.ComboBox
            cboHoSo, cboKhoa, cboBacSi,
            cboLoaiKham, cboTrangThai;

        private System.Windows.Forms.DateTimePicker
            dtNgayGio, dtNgayXuatVien;

        private System.Windows.Forms.NumericUpDown nudNhietDo;
        private System.Windows.Forms.CheckBox chkNhietDo;

        private System.Windows.Forms.Button
            btnThem, btnSua, btnXoa,
            btnLamMoi, btnTimKiem, btnDong;

        private System.Windows.Forms.DataGridView dgvPhieuKham;
        private System.Windows.Forms.Label lblSoLuong, lblHuongDan;

        private System.Windows.Forms.GroupBox
            grpThongTin, grpDanhSach;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        // =====================================
        // CÁC HÀM TẠO CONTROL
        // =====================================

        private System.Windows.Forms.Label Nhan(
            string text, int x, int y, int w)
        {
            return new System.Windows.Forms.Label
            {
                Text = text,
                Location = new System.Drawing.Point(x, y),
                Size = new System.Drawing.Size(w, 25)
            };
        }

        private System.Windows.Forms.TextBox ONhap(
            int x, int y, int w,
            bool nhieuDong = false)
        {
            return new System.Windows.Forms.TextBox
            {
                Location = new System.Drawing.Point(x, y),
                Size = new System.Drawing.Size(
                    w, nhieuDong ? 50 : 28),
                Multiline = nhieuDong,
                MaxLength = nhieuDong ? 1000 : 200
            };
        }

        private System.Windows.Forms.ComboBox LuaChon(
            int x, int y, int w)
        {
            return new System.Windows.Forms.ComboBox
            {
                Location = new System.Drawing.Point(x, y),
                Size = new System.Drawing.Size(w, 28),
                DropDownStyle =
                    System.Windows.Forms.ComboBoxStyle.DropDownList
            };
        }

        private System.Windows.Forms.Button Nut(
            string text, int x, int y, int w)
        {
            return new System.Windows.Forms.Button
            {
                Text = text,
                Location = new System.Drawing.Point(x, y),
                Size = new System.Drawing.Size(w, 38)
            };
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // =====================================
            // FORM
            // =====================================

            this.Name = "FrmPhieuKham";
            this.Text = "Quản lý phiếu khám bệnh";

            this.ClientSize =
                new System.Drawing.Size(1160, 820);

            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;

            this.BackColor =
                System.Drawing.Color.FromArgb(245, 245, 245);

            this.Font =
                new System.Drawing.Font("Segoe UI", 10F);

            this.AutoScaleMode =
                System.Windows.Forms.AutoScaleMode.Font;

            this.MinimumSize =
                new System.Drawing.Size(1000, 750);

            // =====================================
            // TIÊU ĐỀ
            // =====================================

            System.Windows.Forms.Label title =
                Nhan("QUẢN LÝ PHIẾU KHÁM BỆNH",
                    20, 8, 1115);

            title.Font = new System.Drawing.Font(
                "Segoe UI", 17F,
                System.Drawing.FontStyle.Bold);

            title.ForeColor =
                System.Drawing.Color.FromArgb(35, 75, 125);

            title.TextAlign =
                System.Drawing.ContentAlignment.MiddleCenter;

            title.Height = 47;

            this.Controls.Add(title);

            // =====================================
            // GROUP THÔNG TIN
            // =====================================

            grpThongTin = new System.Windows.Forms.GroupBox
            {
                Text = "Thông tin phiếu khám",
                Location = new System.Drawing.Point(20, 60),
                Size = new System.Drawing.Size(1120, 350),

                Anchor =
                    System.Windows.Forms.AnchorStyles.Top |
                    System.Windows.Forms.AnchorStyles.Left |
                    System.Windows.Forms.AnchorStyles.Right
            };

            this.Controls.Add(grpThongTin);

            // MÃ PHIẾU KHÁM
            grpThongTin.Controls.Add(
                Nhan("Mã phiếu khám:", 20, 35, 135));

            txtMaPhieu = ONhap(155, 32, 260);
            txtMaPhieu.MaxLength = 20;
            grpThongTin.Controls.Add(txtMaPhieu);

            // HỒ SƠ BỆNH ÁN
            grpThongTin.Controls.Add(
                Nhan("Hồ sơ bệnh án:", 445, 35, 135));

            cboHoSo = LuaChon(585, 32, 500);
            grpThongTin.Controls.Add(cboHoSo);

            // KHOA ĐIỀU TRỊ
            grpThongTin.Controls.Add(
                Nhan("Khoa điều trị:", 20, 78, 135));

            cboKhoa = LuaChon(155, 75, 260);

            cboKhoa.SelectedIndexChanged +=
                cboKhoa_SelectedIndexChanged;

            grpThongTin.Controls.Add(cboKhoa);

            // BÁC SĨ
            grpThongTin.Controls.Add(
                Nhan("Bác sĩ:", 445, 78, 135));

            cboBacSi = LuaChon(585, 75, 500);
            grpThongTin.Controls.Add(cboBacSi);

            // NGÀY GIỜ KHÁM
            grpThongTin.Controls.Add(
                Nhan("Ngày giờ khám:", 20, 121, 135));

            dtNgayGio = new System.Windows.Forms.DateTimePicker
            {
                Location = new System.Drawing.Point(155, 118),
                Size = new System.Drawing.Size(260, 28),

                Format =
                    System.Windows.Forms.DateTimePickerFormat.Custom,

                CustomFormat = "dd/MM/yyyy HH:mm",
                ShowUpDown = true
            };

            grpThongTin.Controls.Add(dtNgayGio);

            // LOẠI KHÁM
            grpThongTin.Controls.Add(
                Nhan("Loại khám:", 445, 121, 135));

            cboLoaiKham = LuaChon(585, 118, 200);
            grpThongTin.Controls.Add(cboLoaiKham);

            // TRẠNG THÁI
            grpThongTin.Controls.Add(
                Nhan("Trạng thái:", 795, 121, 100));

            cboTrangThai = LuaChon(895, 118, 190);
            grpThongTin.Controls.Add(cboTrangThai);

            // NHIỆT ĐỘ
            grpThongTin.Controls.Add(
                Nhan("Nhiệt độ (°C):", 20, 164, 135));

            nudNhietDo = new System.Windows.Forms.NumericUpDown
            {
                Location = new System.Drawing.Point(155, 161),
                Size = new System.Drawing.Size(110, 28),
                DecimalPlaces = 1,
                Minimum = 25,
                Maximum = 45,
                Increment = 0.1M,
                Value = 37
            };

            grpThongTin.Controls.Add(nudNhietDo);

            chkNhietDo = new System.Windows.Forms.CheckBox
            {
                Text = "Có đo",
                Location = new System.Drawing.Point(275, 162),
                Size = new System.Drawing.Size(105, 27),
                Checked = false
            };

            grpThongTin.Controls.Add(chkNhietDo);

            // HUYẾT ÁP
            grpThongTin.Controls.Add(
                Nhan("Huyết áp:", 445, 164, 135));

            txtHuyetAp = ONhap(585, 161, 200);
            grpThongTin.Controls.Add(txtHuyetAp);

            // NGÀY XUẤT VIỆN
            grpThongTin.Controls.Add(
                Nhan("Ngày xuất viện:", 795, 164, 115));

            dtNgayXuatVien =
                new System.Windows.Forms.DateTimePicker
                {
                    Location = new System.Drawing.Point(915, 161),
                    Size = new System.Drawing.Size(170, 28),

                    Format =
                        System.Windows.Forms.DateTimePickerFormat.Custom,

                    CustomFormat = "dd/MM/yyyy",
                    ShowCheckBox = true,
                    Checked = false
                };

            grpThongTin.Controls.Add(dtNgayXuatVien);

            // TRIỆU CHỨNG
            grpThongTin.Controls.Add(
                Nhan("Triệu chứng:", 20, 208, 135));

            txtTrieuChung = ONhap(155, 205, 260, true);
            grpThongTin.Controls.Add(txtTrieuChung);

            // CHẨN ĐOÁN
            grpThongTin.Controls.Add(
                Nhan("Chẩn đoán:", 445, 208, 135));

            txtChanDoan = ONhap(585, 205, 500, true);
            grpThongTin.Controls.Add(txtChanDoan);

            // HƯỚNG ĐIỀU TRỊ
            grpThongTin.Controls.Add(
                Nhan("Hướng điều trị:", 20, 273, 135));

            txtHuongDieuTri = ONhap(155, 270, 400, true);
            grpThongTin.Controls.Add(txtHuongDieuTri);

            // GHI CHÚ
            grpThongTin.Controls.Add(
                Nhan("Ghi chú:", 580, 273, 80));

            txtGhiChu = ONhap(665, 270, 420, true);
            grpThongTin.Controls.Add(txtGhiChu);

            // =====================================
            // BUTTON
            // =====================================

            btnThem = Nut("Thêm", 240, 427, 130);
            btnSua = Nut("Sửa", 385, 427, 130);
            btnXoa = Nut("Xóa", 530, 427, 130);
            btnLamMoi = Nut("Làm mới", 675, 427, 130);
            btnDong = Nut("Đóng", 820, 427, 130);

            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnLamMoi.Click += btnLamMoi_Click;
            btnDong.Click += btnDong_Click;

            this.Controls.AddRange(
                new System.Windows.Forms.Control[]
                {
                    btnThem,
                    btnSua,
                    btnXoa,
                    btnLamMoi,
                    btnDong
                });

            // =====================================
            // GROUP DANH SÁCH
            // =====================================

            grpDanhSach = new System.Windows.Forms.GroupBox
            {
                Text = "Danh sách phiếu khám",
                Location = new System.Drawing.Point(20, 480),
                Size = new System.Drawing.Size(1120, 320),

                Anchor =
                    System.Windows.Forms.AnchorStyles.Top |
                    System.Windows.Forms.AnchorStyles.Bottom |
                    System.Windows.Forms.AnchorStyles.Left |
                    System.Windows.Forms.AnchorStyles.Right
            };

            this.Controls.Add(grpDanhSach);

            // TÌM KIẾM
            grpDanhSach.Controls.Add(
                Nhan("Tìm kiếm:", 18, 34, 90));

            txtTimKiem = ONhap(110, 31, 340);
            grpDanhSach.Controls.Add(txtTimKiem);

            btnTimKiem = Nut("Tìm kiếm", 465, 29, 120);

            btnTimKiem.Click += btnTimKiem_Click;
            grpDanhSach.Controls.Add(btnTimKiem);

            // TỔNG SỐ PHIẾU
            lblSoLuong =
                Nhan("Số phiếu khám: 0", 760, 35, 330);

            lblSoLuong.TextAlign =
                System.Drawing.ContentAlignment.MiddleRight;

            grpDanhSach.Controls.Add(lblSoLuong);

            // HƯỚNG DẪN
            lblHuongDan = Nhan(
                "Chọn hồ sơ, khoa và bác sĩ để tạo phiếu khám.",
                18, 68, 1050);

            lblHuongDan.ForeColor =
                System.Drawing.Color.DarkSlateGray;

            grpDanhSach.Controls.Add(lblHuongDan);

            // =====================================
            // DATAGRIDVIEW
            // =====================================

            dgvPhieuKham =
                new System.Windows.Forms.DataGridView
                {
                    Location =
                        new System.Drawing.Point(18, 98),

                    Size =
                        new System.Drawing.Size(1080, 205),

                    Anchor =
                        System.Windows.Forms.AnchorStyles.Top |
                        System.Windows.Forms.AnchorStyles.Bottom |
                        System.Windows.Forms.AnchorStyles.Left |
                        System.Windows.Forms.AnchorStyles.Right,

                    BackgroundColor =
                        System.Drawing.Color.White,

                    ReadOnly = true,
                    AllowUserToAddRows = false,
                    AllowUserToDeleteRows = false,
                    RowHeadersVisible = false,
                    MultiSelect = false,

                    SelectionMode =
                        System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect,

                    AutoSizeColumnsMode =
                        System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill,

                    EnableHeadersVisualStyles = false
                };

            dgvPhieuKham.ColumnHeadersDefaultCellStyle
                .BackColor =
                    System.Drawing.Color.FromArgb(219, 230, 241);

            dgvPhieuKham.SelectionChanged +=
                dgvPhieuKham_SelectionChanged;

            grpDanhSach.Controls.Add(dgvPhieuKham);

            // =====================================
            // LOAD FORM
            // =====================================

            this.Load += FrmPhieuKham_Load;

            this.ResumeLayout(false);
        }
    }
}
