using System;
using System.Windows.Forms;
using QuanLyNhanSu.Data;
using QuanLyNhanSu.Forms;

namespace QuanLyNhanSu
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                DbInit.DamBaoCoCSDL();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không kết nối / tạo được CSDL QuanLyNhanSu.\n\n" + ex.Message +
                    "\n\nHãy chạy thủ công file Database\\QuanLyNhanSu.sql trong SSMS, " +
                    "rồi kiểm tra chuỗi kết nối trong App.config.",
                    "Lỗi CSDL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Application.Run(new FrmMain());
        }
    }
}
