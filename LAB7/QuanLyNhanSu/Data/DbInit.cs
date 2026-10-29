using System;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;

namespace QuanLyNhanSu.Data
{
    /// <summary>
    /// Lần chạy đầu: nếu chưa có CSDL QuanLyNhanSu thì tự chạy script
    /// Database\QuanLyNhanSu.sql (đã được copy cạnh file .exe). Có rồi thì bỏ qua.
    /// </summary>
    public static class DbInit
    {
        public static void DamBaoCoCSDL()
        {
            var b = new SqlConnectionStringBuilder(Db.ConnectionString);
            string tenDb = b.InitialCatalog;
            b.InitialCatalog = "master";

            using (var cn = new SqlConnection(b.ConnectionString))
            {
                cn.Open();
                object id;
                using (var cmd = new SqlCommand("SELECT DB_ID(@n)", cn))
                {
                    cmd.Parameters.AddWithValue("@n", tenDb);
                    id = cmd.ExecuteScalar();
                }
                if (id != null && id != DBNull.Value) return;

                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database", "QuanLyNhanSu.sql");
                if (!File.Exists(path)) throw new FileNotFoundException("Không tìm thấy file script: " + path);

                string sql = File.ReadAllText(path);
                foreach (string batch in Regex.Split(sql, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase))
                {
                    if (string.IsNullOrWhiteSpace(batch)) continue;
                    using (var cmd = new SqlCommand(batch, cn))
                    {
                        cmd.CommandTimeout = 120;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
