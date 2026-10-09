# LAB 05 – QUẢN LÝ CÔNG TY DU LỊCH

## 1. Thông tin sinh viên

- **Họ và tên:** Phạm Gia Hiếu
- **Lớp:** 12_CNPM1
- **Môn học:** Phân tích thiết kế hướng đối tượng (OOSD)
- **Trường:** Đại học Tài nguyên và Môi Trường

## 2. Giới thiệu đề tài

**Tên đề tài:** Quản lý công ty du lịch Văn Hóa Việt

Bài thực hành xây dựng hệ thống quản lý công ty du lịch, hỗ trợ quản lý tour, lịch trình, đăng ký khách lẻ, đăng ký theo đoàn, phân công hướng dẫn viên, thanh toán và thống kê.

Hệ thống được xây dựng bằng **C# Windows Forms**, kết nối cơ sở dữ liệu **SQL Server**.

## 3. Công nghệ sử dụng

- **Ngôn ngữ lập trình:** C#
- **Giao diện:** Windows Forms (WinForms)
- **Môi trường phát triển:** Visual Studio 2026
- **Hệ quản trị cơ sở dữ liệu:** Microsoft SQL Server
- **Công cụ quản lý CSDL:** SQL Server Management Studio (SSMS)
- **Thiết kế hệ thống:** UML, Use Case, Class Diagram, Sequence Diagram, Activity Diagram, ERD

## 4. Các chức năng chính

1. Quản lý danh mục phương tiện, điểm bán vé, hướng dẫn viên và điểm tham quan.
2. Quản lý tour du lịch và hành trình.
3. Quản lý lịch chuyến khách lẻ.
4. Đăng ký và thanh toán vé cho khách lẻ.
5. Đăng ký tour theo đoàn, quản lý tiền cọc và hủy đăng ký.
6. Phân công hướng dẫn viên cho chuyến hoặc đoàn.
7. Thanh toán sau tour và khảo sát khách hàng.
8. Tính lương hướng dẫn viên và thống kê tổng hợp.

## 5. Cấu trúc thư mục

```text
LAB5/
│
├── QuanLyCongTyDuLich/
│   ├── Data/
│   ├── Services/
│   ├── Forms/
│   ├── Program.cs
│   └── App.config
│
├── QuanLyCongTyDuLich.sql
├── BaoCao_Lab05_PhamGiaHieu.docx
└── README.md
```

## 6. Hướng dẫn chạy chương trình

**Bước 1:** Mở SQL Server Management Studio (SSMS).

**Bước 2:** Chạy file `QuanLyCongTyDuLich.sql` để tạo cơ sở dữ liệu và các bảng.

**Bước 3:** Mở project `QuanLyCongTyDuLich` bằng Visual Studio 2026.

**Bước 4:** Kiểm tra và cấu hình chuỗi kết nối SQL Server trong `App.config` phù hợp với máy đang sử dụng.

**Bước 5:** Nhấn `Ctrl + Shift + B` để Build Solution.

**Bước 6:** Nhấn `F5` để chạy chương trình.

## 7. Nội dung báo cáo

Báo cáo bao gồm:

- Phân tích yêu cầu hệ thống.
- Xác định tác nhân và Use Case.
- Thiết kế sơ đồ UML.
- Thiết kế cơ sở dữ liệu SQL Server.
- Thiết kế giao diện Windows Forms.
- Xây dựng các lớp xử lý nghiệp vụ (Service).
- Kiểm thử chức năng theo các test case.

## 8. Tác giả

**Phạm Gia Hiếu**

Lớp: **12_CNPM1**

Trường Đại học Tài nguyên và Môi trường 

---

*Bài thực hành môn Phân tích thiết kế hướng đối tượng (OOSD).* 
