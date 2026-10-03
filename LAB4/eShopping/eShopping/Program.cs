using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using eShopping.UI; // Bắt buộc thêm dòng này để nhận diện Form trong thư mục UI

namespace eShopping
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Đã đổi Form1 thành frmDanhSachSP để mở màn hình chính đầu tiên
            Application.Run(new frmDanhSachSP());
        }
    }
}