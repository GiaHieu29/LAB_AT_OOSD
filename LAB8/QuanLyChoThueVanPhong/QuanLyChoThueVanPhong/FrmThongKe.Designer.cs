
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyChoThueVanPhong
{
    partial class FrmThongKe
    {
        private System.ComponentModel.IContainer components = null;

        private DateTimePicker dtTuNgay, dtDenNgay;
        private Button btnThongKe, btnNamNay;
        private Button btnThangNay, btnDong;

        private TabControl tabMain;
        private TabPage tabTongQuan, tabDoanhThu;
        private TabPage tabCongNo, tabVanHanh;

        private Label lblTongPhong, lblDangThue;
        private Label lblPhongTrong, lblBaoTri;
        private Label lblTongHopDong, lblTongThu;
        private Label lblNoThue, lblNoDichVu;
        private Label lblKyBaoCao;

        private Label lblThuThue, lblThuDV, lblThuTong;
        private Label lblTongNo;
        private Label lblChiPhiSua, lblSoSuaChua;
        private Label lblSoViPham, lblTienPhat;

        private DataGridView dgvPhong, dgvHopDong;
        private DataGridView dgvDoanhThu, dgvChiTietThu;
        private DataGridView dgvNoThue, dgvNoDichVu;
        private DataGridView dgvSuaChua, dgvViPham;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        // ======================================
        // CÁC HÀM TẠO CONTROL
        // ======================================

        private Label TaoNhan(
            string text, int x, int y, int w = 130)
        {
            return new Label
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(w, 28),
                TextAlign = ContentAlignment.MiddleLeft
            };
        }

        private Button TaoNut(
            string text, int x, int y, int w = 135)
        {
            return new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(w, 36),
                Cursor = Cursors.Hand
            };
        }

        private DateTimePicker TaoNgay(
            int x, int y, int w)
        {
            return new DateTimePicker
            {
                Location = new Point(x, y),
                Size = new Size(w, 29),
                Format = DateTimePickerFormat.Short
            };
        }

        private DataGridView TaoBang()
        {
            return new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                MultiSelect = false,
                RowHeadersVisible = false,
                SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect,
                AutoGenerateColumns = true,
                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill,
                ColumnHeadersHeight = 35,
                EnableHeadersVisualStyles = false
            };
        }

        private GroupBox TaoNhom(
            string text, int x, int y, int w, int h)
        {
            return new GroupBox
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(w, h),
                Anchor = AnchorStyles.Top |
                         AnchorStyles.Left |
                         AnchorStyles.Right
            };
        }

        private Panel TaoPanelBang(
            int x, int y, int w, int h,
            DataGridView bang)
        {
            Panel p = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(w, h),
                Anchor = AnchorStyles.Top |
                         AnchorStyles.Bottom |
                         AnchorStyles.Left |
                         AnchorStyles.Right
            };

            p.Controls.Add(bang);
            return p;
        }

        private Label TaoChiSo(
            Control parent,
            string ten,
            int x, int y, int w)
        {
            Panel p = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(w, 84),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            Label tenChiSo = TaoNhan(
                ten, 12, 9, w - 24);

            tenChiSo.ForeColor = Color.DimGray;
            tenChiSo.Font = new Font(
                "Segoe UI", 9F);

            Label giaTri = TaoNhan(
                "0", 12, 37, w - 24);

            giaTri.Font = new Font(
                "Segoe UI", 15F, FontStyle.Bold);

            giaTri.ForeColor =
                Color.FromArgb(30, 80, 125);

            giaTri.Height = 36;

            p.Controls.Add(tenChiSo);
            p.Controls.Add(giaTri);
            parent.Controls.Add(p);

            return giaTri;
        }

        private void InitializeComponent()
        {
            SuspendLayout();

            // ======================================
            // FORM
            // ======================================

            Name = "FrmThongKe";
            Text = "Thống kê hoạt động cho thuê văn phòng";
            ClientSize = new Size(1200, 850);
            MinimumSize = new Size(1150, 800);

            StartPosition =
                FormStartPosition.CenterParent;

            BackColor = Color.WhiteSmoke;
            Font = new Font("Segoe UI", 10F);
            AutoScaleMode = AutoScaleMode.Font;

            Label title = TaoNhan(
                "THỐNG KÊ HOẠT ĐỘNG CHO THUÊ VĂN PHÒNG",
                20, 10, 1160);

            title.Height = 45;
            title.TextAlign =
                ContentAlignment.MiddleCenter;

            title.Font = new Font(
                "Segoe UI", 17F, FontStyle.Bold);

            title.ForeColor =
                Color.FromArgb(35, 75, 125);

            Controls.Add(title);

            // ======================================
            // BỘ LỌC THỜI GIAN
            // ======================================

            GroupBox grpLoc = TaoNhom(
                "Bộ lọc thống kê",
                20, 65, 1160, 92);

            Controls.Add(grpLoc);

            grpLoc.Controls.Add(
                TaoNhan("Từ ngày:", 20, 35, 85));

            dtTuNgay = TaoNgay(105, 33, 170);
            grpLoc.Controls.Add(dtTuNgay);

            grpLoc.Controls.Add(
                TaoNhan("Đến ngày:", 295, 35, 95));

            dtDenNgay = TaoNgay(390, 33, 170);
            grpLoc.Controls.Add(dtDenNgay);

            btnThongKe = TaoNut(
                "Thống kê", 580, 30, 130);

            btnNamNay = TaoNut(
                "Năm nay", 723, 30, 115);

            btnThangNay = TaoNut(
                "Tháng này", 850, 30, 125);

            btnDong = TaoNut(
                "Đóng", 990, 30, 145);

            grpLoc.Controls.AddRange(new Control[]
            {
                btnThongKe,
                btnNamNay,
                btnThangNay,
                btnDong
            });

            btnThongKe.Click += btnThongKe_Click;
            btnNamNay.Click += btnNamNay_Click;
            btnThangNay.Click += btnThangNay_Click;
            btnDong.Click += btnDong_Click;

            // ======================================
            // TAB CONTROL
            // ======================================

            tabMain = new TabControl
            {
                Location = new Point(20, 170),
                Size = new Size(1160, 660),
                Anchor = AnchorStyles.Top |
                         AnchorStyles.Bottom |
                         AnchorStyles.Left |
                         AnchorStyles.Right
            };

            tabTongQuan =
                new TabPage("1. Tổng quan");

            tabDoanhThu =
                new TabPage("2. Doanh thu");

            tabCongNo =
                new TabPage("3. Công nợ");

            tabVanHanh =
                new TabPage("4. Vận hành");

            tabMain.TabPages.AddRange(
                new TabPage[]
                {
                    tabTongQuan,
                    tabDoanhThu,
                    tabCongNo,
                    tabVanHanh
                });

            Controls.Add(tabMain);

            // ======================================
            // TAB 1 - TỔNG QUAN
            // ======================================

            lblTongPhong = TaoChiSo(
                tabTongQuan, "Tổng số phòng",
                15, 15, 265);

            lblDangThue = TaoChiSo(
                tabTongQuan, "Phòng đang thuê",
                295, 15, 265);

            lblPhongTrong = TaoChiSo(
                tabTongQuan, "Phòng trống",
                575, 15, 265);

            lblBaoTri = TaoChiSo(
                tabTongQuan, "Bảo trì / ngừng sử dụng",
                855, 15, 265);

            lblTongHopDong = TaoChiSo(
                tabTongQuan, "Hợp đồng ký trong kỳ",
                15, 110, 265);

            lblTongThu = TaoChiSo(
                tabTongQuan, "Tổng thực thu trong kỳ",
                295, 110, 265);

            lblNoThue = TaoChiSo(
                tabTongQuan, "Nợ tiền thuê đến hạn",
                575, 110, 265);

            lblNoDichVu = TaoChiSo(
                tabTongQuan, "Nợ dịch vụ đến hạn",
                855, 110, 265);

            lblKyBaoCao = TaoNhan(
                "Khoảng thời gian báo cáo",
                15, 205, 1110);

            lblKyBaoCao.ForeColor = Color.DarkBlue;
            tabTongQuan.Controls.Add(lblKyBaoCao);

            GroupBox grpPhong = TaoNhom(
                "Tình trạng phòng tại ngày kết thúc",
                15, 242, 1105, 175);

            dgvPhong = TaoBang();

            grpPhong.Controls.Add(
                TaoPanelBang(12, 25, 1080, 135, dgvPhong));

            tabTongQuan.Controls.Add(grpPhong);

            GroupBox grpHD = TaoNhom(
                "Hợp đồng ký trong khoảng thời gian",
                15, 427, 1105, 188);

            dgvHopDong = TaoBang();

            grpHD.Controls.Add(
                TaoPanelBang(12, 25, 1080, 148, dgvHopDong));

            tabTongQuan.Controls.Add(grpHD);

            // ======================================
            // TAB 2 - DOANH THU
            // ======================================

            lblThuThue = TaoNhan(
                "Đã thu tiền thuê: 0 đ",
                20, 20, 350);

            lblThuDV = TaoNhan(
                "Đã thu dịch vụ: 0 đ",
                385, 20, 350);

            lblThuTong = TaoNhan(
                "Tổng thực thu: 0 đ",
                760, 20, 350);

            foreach (Label label in new[]
            {
                lblThuThue, lblThuDV, lblThuTong
            })
            {
                label.Height = 38;
                label.Font = new Font(
                    "Segoe UI", 10F, FontStyle.Bold);
                label.ForeColor =
                    Color.FromArgb(35, 75, 125);
                tabDoanhThu.Controls.Add(label);
            }

            GroupBox grpDT = TaoNhom(
                "Doanh thu theo tháng",
                15, 70, 1105, 235);

            dgvDoanhThu = TaoBang();

            grpDT.Controls.Add(
                TaoPanelBang(12, 25, 1080, 195,
                             dgvDoanhThu));

            tabDoanhThu.Controls.Add(grpDT);

            GroupBox grpChiTietThu = TaoNhom(
                "Chi tiết giao dịch thu tiền",
                15, 320, 1105, 295);

            dgvChiTietThu = TaoBang();

            grpChiTietThu.Controls.Add(
                TaoPanelBang(12, 25, 1080, 255,
                             dgvChiTietThu));

            tabDoanhThu.Controls.Add(grpChiTietThu);

            // ======================================
            // TAB 3 - CÔNG NỢ
            // ======================================

            lblTongNo = TaoNhan(
                "Tổng công nợ đến hạn: 0 đ",
                20, 18, 1100);

            lblTongNo.Font = new Font(
                "Segoe UI", 12F, FontStyle.Bold);

            lblTongNo.ForeColor = Color.Firebrick;
            lblTongNo.Height = 40;

            tabCongNo.Controls.Add(lblTongNo);

            GroupBox grpNoThue = TaoNhom(
                "Công nợ tiền thuê theo phòng",
                15, 75, 1105, 250);

            dgvNoThue = TaoBang();

            grpNoThue.Controls.Add(
                TaoPanelBang(12, 25, 1080, 210,
                             dgvNoThue));

            tabCongNo.Controls.Add(grpNoThue);

            GroupBox grpNoDV = TaoNhom(
                "Công nợ hóa đơn dịch vụ đã đến hạn",
                15, 340, 1105, 275);

            dgvNoDichVu = TaoBang();

            grpNoDV.Controls.Add(
                TaoPanelBang(12, 25, 1080, 235,
                             dgvNoDichVu));

            tabCongNo.Controls.Add(grpNoDV);

            // ======================================
            // TAB 4 - VẬN HÀNH
            // ======================================

            lblChiPhiSua = TaoNhan(
                "Chi phí sửa chữa: 0 đ",
                20, 20, 375);

            lblSoSuaChua = TaoNhan(
                "Số công việc: 0",
                420, 20, 250);

            lblSoViPham = TaoNhan(
                "Số vi phạm: 0",
                700, 20, 200);

            lblTienPhat = TaoNhan(
                "Tiền phạt ghi nhận: 0 đ",
                20, 52, 1080);

            foreach (Label label in new[]
            {
                lblChiPhiSua, lblSoSuaChua,
                lblSoViPham, lblTienPhat
            })
            {
                label.Font = new Font(
                    "Segoe UI", 10F, FontStyle.Bold);

                label.ForeColor =
                    Color.FromArgb(35, 75, 125);

                tabVanHanh.Controls.Add(label);
            }

            GroupBox grpSC = TaoNhom(
                "Các công việc sửa chữa bắt đầu trong kỳ",
                15, 92, 1105, 245);

            dgvSuaChua = TaoBang();

            grpSC.Controls.Add(
                TaoPanelBang(12, 25, 1080, 205,
                             dgvSuaChua));

            tabVanHanh.Controls.Add(grpSC);

            GroupBox grpVP = TaoNhom(
                "Các vi phạm được phát hiện trong kỳ",
                15, 350, 1105, 265);

            dgvViPham = TaoBang();

            grpVP.Controls.Add(
                TaoPanelBang(12, 25, 1080, 225,
                             dgvViPham));

            tabVanHanh.Controls.Add(grpVP);

            // ======================================
            // LOAD
            // ======================================

            Load += FrmThongKe_Load;

            ResumeLayout(false);
        }
    }
}
