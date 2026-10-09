
using System;
using System.Windows.Forms;

namespace QuanLyCongTyDuLich.Forms
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void MoChucNang(string ten)
        {
            MessageBox.Show(
                "Chức năng " + ten +
                " sẽ được hoàn thiện ở bước tiếp theo.",
                "Quản lý công ty du lịch",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }


        private void btnDanhMuc_Click(object sender, EventArgs e)
        {
            using (FrmDanhMuc frm = new FrmDanhMuc())
            {
                frm.ShowDialog(this);
            }
        }



        private void btnTour_Click(object sender, EventArgs e)
        {
            using (FrmTour frm = new FrmTour())
            {
                frm.ShowDialog(this);
            }
        }



        private void btnChuyenLe_Click(object sender, EventArgs e)
        {
            using (FrmChuyenLe frm = new FrmChuyenLe())
            {
                frm.ShowDialog(this);
            }
        }



        private void btnDangKyLe_Click(object sender, EventArgs e)
        {
            using (FrmDangKyLe frm = new FrmDangKyLe())
            {
                frm.ShowDialog(this);
            }
        }



        private void btnDangKyDoan_Click(object sender, EventArgs e)
        {
            using (FrmDangKyDoan frm = new FrmDangKyDoan())
            {
                frm.ShowDialog(this);
            }
        }



        private void btnPhanCong_Click(object sender, EventArgs e)
        {
            using (FrmPhanCongHDV frm = new FrmPhanCongHDV())
            {
                frm.ShowDialog(this);
            }
        }



        private void btnKetThuc_Click(object sender, EventArgs e)
        {
            using (FrmKetThucKhaoSat frm =
                new FrmKetThucKhaoSat())
            {
                frm.ShowDialog(this);
            }
        }



        private void btnThongKe_Click(object sender, EventArgs e)
        {
            using (FrmLuongThongKe frm = new FrmLuongThongKe())
            {
                frm.ShowDialog(this);
            }
        }


        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "Bạn có thực sự muốn thoát?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Close();
            }
        }
    }
}
