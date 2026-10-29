using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using QuanLyNhanSu.Services;

namespace QuanLyNhanSu.Forms
{
    public partial class FrmThongKe : Form
    {
        private readonly ThongKeService svc = new ThongKeService();

        public FrmThongKe() { InitializeComponent(); }

        private void FrmThongKe_Load(object sender, EventArgs e)
        {
            numNam.Value = DateTime.Today.Year;
            btnXemPB_Click(null, null); btnXemHan_Click(null, null); btnXemTP_Click(null, null);
        }

        private void btnXemPB_Click(object sender, EventArgs e) { dgvPB.DataSource = svc.NhanSuTheoPhongBan(); }
        private void btnXemHan_Click(object sender, EventArgs e) { dgvHan.DataSource = svc.SapHetHan((int)numNgay.Value); }
        private void btnXemTP_Click(object sender, EventArgs e) { dgvTP.DataSource = svc.ThuongPhat((int)numNam.Value); }
        private void btnDong_Click(object sender, EventArgs e) { Close(); }
    }
}
