using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace eShopping.UI
{
    public partial class frmGioHang : Form
    {
        private readonly DataGridView dgv = new DataGridView();
        private readonly NumericUpDown nud = new NumericUpDown { Minimum = 1, Maximum = 999, Width = 60 };
        private readonly Label lblTong = new Label { AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };

        public frmGioHang()
        {
            Text = "Giỏ hàng";
            ClientSize = new Size(760, 450);
            StartPosition = FormStartPosition.CenterParent;

            dgv.SetBounds(15, 15, 730, 300);
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.SelectionChanged += delegate { DongDuocChon(); };
            Controls.Add(dgv);

            Controls.Add(new Label { Text = "Số lượng:", Left = 15, Top = 330, Width = 70 });
            nud.Left = 90; nud.Top = 326; Controls.Add(nud);
            UiHelper.Btn(this, "Cập nhật", 165, 322, 90, BtnCapNhat_Click);
            UiHelper.Btn(this, "Xóa", 265, 322, 70, BtnXoa_Click);
            lblTong.Left = 15; lblTong.Top = 375; Controls.Add(lblTong);
            UiHelper.Btn(this, "Tính tiền", 560, 395, 90, BtnTinhTien_Click);
            UiHelper.Btn(this, "Đóng", 660, 395, 85, delegate { Close(); });

            Nap();
        }

        private void Nap()
        {
            dgv.DataSource = AppSession.Cart.Items.Select(i => new
            {
                MaSP = i.SP.MaSP,
                TenSP = i.SP.TenSP,
                DonGia = i.SP.GiaHienHanh,
                SoLuong = i.SoLuong,
                ThanhTien = i.ThanhTien
            }).ToList();

            if (dgv.Columns.Count == 5)
            {
                dgv.Columns[0].HeaderText = "Mã SP";
                dgv.Columns[1].HeaderText = "Tên sản phẩm"; dgv.Columns[1].Width = 280;
                dgv.Columns[2].HeaderText = "Đơn giá"; dgv.Columns[2].DefaultCellStyle.Format = "N0";
                dgv.Columns[3].HeaderText = "Số lượng";
                dgv.Columns[4].HeaderText = "Thành tiền"; dgv.Columns[4].DefaultCellStyle.Format = "N0";
            }
            lblTong.Text = "Tiền hàng: " + AppSession.Cart.TienHang.ToString("N0") + " đ";
        }

        private string MaDangChon()
        {
            return dgv.CurrentRow == null ? null : dgv.CurrentRow.Cells[0].Value.ToString();
        }

        private void DongDuocChon()
        {
            var ma = MaDangChon(); if (ma == null) return;
            var it = AppSession.Cart.Items.FirstOrDefault(i => i.SP.MaSP == ma);
            if (it != null) nud.Value = Math.Min(Math.Max(it.SoLuong, 1), 999);
        }

        private void BtnCapNhat_Click(object s, EventArgs e)
        {
            var ma = MaDangChon(); if (ma == null) return;
            AppSession.Cart.SetQty(ma, (int)nud.Value);
            Nap();
        }

        private void BtnXoa_Click(object s, EventArgs e)
        {
            var ma = MaDangChon(); if (ma == null) return;
            AppSession.Cart.Remove(ma);
            Nap();
        }

        private void BtnTinhTien_Click(object s, EventArgs e)
        {
            if (AppSession.Cart.Items.Count == 0) { UiHelper.Info("Giỏ hàng trống."); return; }

            if (AppSession.KhachHienTai == null)                          // BR13
            {
                using (var f = new frmDangNhap()) f.ShowDialog(this);
                if (AppSession.KhachHienTai == null) return;
            }
            using (var f = new frmDatHang()) f.ShowDialog(this);
            Nap();
        }
    }
}