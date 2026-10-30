
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyChoThueVanPhong.Data
{
    public static class Db
    {
        // =====================================
        // 1. ĐỌC CHUỖI KẾT NỐI
        // =====================================

        private static string ConnectionString
        {
            get
            {
                var setting =
                    ConfigurationManager.ConnectionStrings[
                        "QuanLyChoThueVanPhong"];

                if (setting == null ||
                    string.IsNullOrWhiteSpace(
                        setting.ConnectionString))
                {
                    throw new Exception(
                        "Không tìm thấy chuỗi kết nối " +
                        "QuanLyChoThueVanPhong trong App.config.");
                }

                return setting.ConnectionString;
            }
        }

        // =====================================
        // 2. MỞ KẾT NỐI SQL SERVER
        // =====================================

        public static SqlConnection OpenConnection()
        {
            SqlConnection conn =
                new SqlConnection(ConnectionString);

            try
            {
                conn.Open();
                return conn;
            }
            catch
            {
                conn.Dispose();
                throw;
            }
        }

        // =====================================
        // 3. TẠO SQL PARAMETER
        // =====================================

        public static SqlParameter P(
            string name, object value)
        {
            return new SqlParameter(
                name,
                value == null
                    ? (object)DBNull.Value
                    : value);
        }

        // =====================================
        // 4. QUERY - TRẢ VỀ DATATABLE
        // =====================================

        public static DataTable Query(
            string sql,
            params SqlParameter[] parameters)
        {
            DataTable table = new DataTable();

            using (SqlConnection conn =
                OpenConnection())
            using (SqlCommand cmd =
                new SqlCommand(sql, conn))
            {
                cmd.CommandTimeout = 30;

                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                using (SqlDataAdapter adapter =
                    new SqlDataAdapter(cmd))
                {
                    adapter.Fill(table);
                }
            }

            return table;
        }

        // =====================================
        // 5. EXECUTE - INSERT / UPDATE / DELETE
        // =====================================

        public static int Execute(
            string sql,
            params SqlParameter[] parameters)
        {
            using (SqlConnection conn =
                OpenConnection())
            using (SqlCommand cmd =
                new SqlCommand(sql, conn))
            {
                cmd.CommandTimeout = 30;

                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                return cmd.ExecuteNonQuery();
            }
        }

        // =====================================
        // 6. SCALAR - LẤY MỘT GIÁ TRỊ
        // =====================================

        public static object Scalar(
            string sql,
            params SqlParameter[] parameters)
        {
            using (SqlConnection conn =
                OpenConnection())
            using (SqlCommand cmd =
                new SqlCommand(sql, conn))
            {
                cmd.CommandTimeout = 30;

                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                return cmd.ExecuteScalar();
            }
        }

        // =====================================
        // 7. KIỂM TRA KẾT NỐI
        // =====================================

        public static bool TestConnection(
            out string message)
        {
            try
            {
                using (SqlConnection conn =
                    OpenConnection())
                {
                    message =
                        "Kết nối SQL Server thành công!\n\n" +
                        "Server: " + conn.DataSource + "\n" +
                        "Database: " + conn.Database + "\n" +
                        "Trạng thái: " + conn.State;

                    return true;
                }
            }
            catch (Exception ex)
            {
                message =
                    "Không thể kết nối SQL Server.\n\n" +
                    ex.Message;

                return false;
            }
        }
    }
}
