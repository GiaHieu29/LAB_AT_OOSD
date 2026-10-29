using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using QuanLyNhanSu.Services;

namespace QuanLyNhanSu.Forms
{
    public partial class FrmMain : Form
    {
        public FrmMain() { InitializeComponent(); }
        private void Mo(Form f) { using (f) f.ShowDialog(this); }

        private void btnDanhMuc_Click(object sender, EventArgs e) { Mo(new FrmDanhMuc()); }
        private void btnNhanVien_Click(object sender, EventArgs e) { Mo(new FrmNhanVien()); }
        private void btnTuyenDung_Click(object sender, EventArgs e) { Mo(new FrmTuyenDung()); }
        private void btnHopDong_Click(object sender, EventArgs e) { Mo(new FrmHopDong()); }
        private void btnQuyetDinh_Click(object sender, EventArgs e) { Mo(new FrmQuyetDinh()); }
        private void btnPhanCong_Click(object sender, EventArgs e) { Mo(new FrmPhanCong()); }
        private void btnChamCong_Click(object sender, EventArgs e) { Mo(new FrmChamCong()); }
        private void btnPhuTroi_Click(object sender, EventArgs e) { Mo(new FrmPhuTroi()); }
        private void btnBangLuong_Click(object sender, EventArgs e) { Mo(new FrmBangLuong()); }
        private void btnKhenThuong_Click(object sender, EventArgs e) { Mo(new FrmKhenThuong()); }
        private void btnKyLuat_Click(object sender, EventArgs e) { Mo(new FrmKyLuat()); }
        private void btnThongKe_Click(object sender, EventArgs e) { Mo(new FrmThongKe()); }
        private void btnThoat_Click(object sender, EventArgs e) { Close(); }
    }
}
