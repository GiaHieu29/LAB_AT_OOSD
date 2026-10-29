using System.Data;
using System.Windows.Forms;
using QuanLyNhanSu.Services;
 
namespace QuanLyNhanSu.Forms
{
    /// <summary>Tiện ích dùng chung cho các Form.</summary>
    internal static class FormHelper
    {
        public static void Nap(ComboBox cbo, DataTable dt, string display, string value)
        {
            cbo.DisplayMember = display;
            cbo.ValueMember = value;
            cbo.DataSource = dt;
        }
 
        public static string Gia(ComboBox cbo)
        {
            return cbo.SelectedValue == null ? "" : cbo.SelectedValue.ToString();
        }
 
        public static string O(DataGridView dgv, string cot)
        {
            return dgv.CurrentRow == null ? "" : System.Convert.ToString(dgv.CurrentRow.Cells[cot].Value);
        }
 
        /// <summary>Đổi mảng byte thành ảnh (bản sao, không giữ stream).</summary>
        public static System.Drawing.Image DocAnh(byte[] b)
        {
            if (b == null || b.Length == 0) return null;
            try
            {
                using (var ms = new System.IO.MemoryStream(b))
                using (var im = System.Drawing.Image.FromStream(ms))
                    return new System.Drawing.Bitmap(im);
            }
            catch { return null; }
        }

        /// <summary>Thêm dòng "(Tất cả)" lên đầu bảng dùng cho ComboBox lọc.</summary>
        public static System.Data.DataTable ThemDongTatCa(System.Data.DataTable dt, string cotMa, string cotHienThi)
        {
            var r = dt.NewRow(); r[cotMa] = ""; r[cotHienThi] = "(Tất cả)"; dt.Rows.InsertAt(r, 0); return dt;
        }

        /// <summary>Hộp thoại cảnh báo.</summary>
        public static void Canh(string noiDung)
        {
            MessageBox.Show(noiDung, "Không thực hiện được", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>Hiển thị kết quả; trả về true nếu thành công.</summary>
        public static bool Bao(KetQuaXuLy k)
        {
            MessageBox.Show(k.ThongBao, k.ThanhCong ? "Thông báo" : "Không thực hiện được",
                MessageBoxButtons.OK, k.ThanhCong ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            return k.ThanhCong;
        }
    }
}
