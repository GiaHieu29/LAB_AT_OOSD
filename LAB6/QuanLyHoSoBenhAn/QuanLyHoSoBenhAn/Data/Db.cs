using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyHoSoBenhAn.Data
{
    public static class Db
    {
        // Đọc chuỗi kết nối từ App.config
        private static string ConnectionString
        {
            get
            {
                var config =
                    ConfigurationManager.ConnectionStrings[
                        "QuanLyHoSoBenhAn"];

                if (config == null)
                {
                    throw new Exception(
                        "Không tìm thấy chuỗi kết nối " +
                        "'QuanLyHoSoBenhAn' trong App.config.");
                }

                return config.ConnectionString;
            }
        }

        // Tạo kết nối SQL Server
        public static SqlConnection OpenConnection()
        {
            SqlConnection conn =
                new SqlConnection(ConnectionString);

            conn.Open();

            return conn;
        }

        // Tạo tham số SQL
        public static SqlParameter P(
            string name,
            object value)
        {
            return new SqlParameter(
                name,
                value ?? DBNull.Value);
        }

        // SELECT: trả về DataTable
        public static DataTable Query(
            string sql,
            params SqlParameter[] parameters)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn =
                OpenConnection())
            {
                using (SqlCommand cmd =
                    new SqlCommand(sql, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    using (SqlDataAdapter da =
                        new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        // INSERT, UPDATE, DELETE
        // Trả về số dòng bị ảnh hưởng
        public static int Execute(
            string sql,
            params SqlParameter[] parameters)
        {
            using (SqlConnection conn =
                OpenConnection())
            {
                using (SqlCommand cmd =
                    new SqlCommand(sql, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        // Lấy một giá trị duy nhất
        // Ví dụ: COUNT(*), SUM(...)
        public static object Scalar(
            string sql,
            params SqlParameter[] parameters)
        {
            using (SqlConnection conn =
                OpenConnection())
            {
                using (SqlCommand cmd =
                    new SqlCommand(sql, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    return cmd.ExecuteScalar();
                }
            }
        }

        // Kiểm tra kết nối SQL Server
        public static bool TestConnection(
            out string message)
        {
            try
            {
                using (SqlConnection conn =
                    OpenConnection())
                {
                    message =
                        "Kết nối SQL Server thành công!\n" +
                        "Server: " + conn.DataSource + "\n" +
                        "Database: " + conn.Database;

                    return true;
                }
            }
            catch (Exception ex)
            {
                message =
                    "Kết nối SQL Server thất bại!\n\n" +
                    ex.Message;

                return false;
            }
        }
    }
}
