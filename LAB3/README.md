# LAB 03 - HỆ THỐNG QUẢN LÝ KHÁCH SẠN (QuanLyKhachSan)

## Thông tin sinh viên
- **Họ và tên:** Phạm Gia Hiếu
- **MSSV:** 1250080053
- **Lớp:** K12_CNPM1
- **Môn học:** Phân tích và Thiết kế Hướng đối tượng (OOSD)

---

## 1. Mục tiêu
Xây dựng và hoàn thiện quy trình phân tích, thiết kế hướng đối tượng cho **Hệ thống Quản lý Khách sạn**, chuyển đổi từ mô hình thiết kế sang cài đặt ứng dụng thực tế trên nền tảng C# WinForms kết hợp hệ quản trị cơ sở dữ liệu SQL Server:
- Khảo sát bài toán thực tế, xác định các quy định nghiệp vụ (Business Rules) và yêu cầu chức năng trong quản lý lưu trú, phòng, tiện nghi và dịch vụ khách sạn.
- Thiết kế hệ thống bằng ngôn ngữ mô hình hóa thống nhất (UML) và thiết kế lược đồ cơ sở dữ liệu quan hệ (ERD).
- Cài đặt phần mềm theo kiến trúc phân lớp (Layered Architecture: `Data` - `Services` - `Forms`) đảm bảo tính toàn vẹn dữ liệu và dễ bảo trì.

---

## 2. Nội dung thực hiện
- **Khảo sát & Phân tích yêu cầu:** Xác định các Actor, phân loại nghiệp vụ và đặc tả chi tiết các Use Case cốt lõi (Quản lý danh mục phòng/loại tiện nghi/quy định, Đặt phòng, Nhận/Trả phòng, Thanh toán và Thống kê).
- **Thiết kế hệ thống (UML & CSDL):**
  - Sơ đồ Use Case (Use Case Diagram).
  - Sơ đồ Lớp (Class Diagram).
  - Sơ đồ Hoạt động (Activity Diagram) & Sơ đồ Tuần tự (Sequence Diagram).
  - Sơ đồ Thực thể kết hợp (ERD) và kịch bản khởi tạo CSDL SQL Server (`Database`).
- **Cài đặt chương trình (`QuanLyKhachSan`):**
  - **Công nghệ:** C# (.NET WinForms), ADO.NET, SQL Server.
  - **Kiến trúc mã nguồn:**
    - `Data/` (`Db.cs`): Quản lý kết nối và thực thi truy vấn CSDL an toàn với `SqlParameter`.
    - `Services/`: Xử lý logic nghiệp vụ và kiểm tra các ràng buộc dữ liệu.
    - `Forms/` (`FrmMain`, `FrmDanhMuc`, ...): Giao diện người dùng trực quan, tương tác thông qua tầng Service.

---

## 3. Cấu trúc thư mục
```text
LAB3/
├── QuanLyKhachSan/
│   ├── Database/               # Chứa script SQL khởi tạo CSDL và dữ liệu mẫu
│   ├── QuanLyKhachSan/         # Source code chính (Data, Services, Forms, App.config)
│   └── QuanLyKhachSan.sln      # File Solution mở bằng Visual Studio
└── README.md                   # Tài liệu giới thiệu dự án Lab 3
