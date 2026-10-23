
using System;
using System.Windows.Forms;

namespace QuanLyHoSoBenhAn
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnBenhNhan_Click(object sender, EventArgs e)
        {
            using (FrmBenhNhan frm = new FrmBenhNhan())
                frm.ShowDialog(this);
        }

        private void btnHoSo_Click(object sender, EventArgs e)
        {
            using (FrmHoSoBenhAn frm = new FrmHoSoBenhAn())
                frm.ShowDialog(this);
        }

        private void btnPhieuKham_Click(object sender, EventArgs e)
        {
            using (FrmPhieuKham frm = new FrmPhieuKham())
                frm.ShowDialog(this);
        }

        private void btnXetNghiem_Click(object sender, EventArgs e)
        {
            using (FrmXetNghiem frm = new FrmXetNghiem())
                frm.ShowDialog(this);
        }

        private void btnToaThuoc_Click(object sender, EventArgs e)
        {
            using (FrmToaThuoc frm = new FrmToaThuoc())
                frm.ShowDialog(this);
        }

        private void btnLichHen_Click(object sender, EventArgs e)
        {
            using (FrmLichHenKham frm = new FrmLichHenKham())
                frm.ShowDialog(this);
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            using (FrmThanhToanVienPhi frm =
                new FrmThanhToanVienPhi())
            {
                frm.ShowDialog(this);
            }
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            using (FrmThongKe frm = new FrmThongKe())
                frm.ShowDialog(this);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "Bạn có muốn thoát chương trình?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
