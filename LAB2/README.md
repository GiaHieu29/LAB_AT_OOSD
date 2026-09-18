# LAB2 - Hệ thống Quản lý Thư viện

Từ phân tích yêu cầu - UML - CSDL - Giao diện - đến code C# WinForms.

## Mục tiêu
Đi trọn chu trình từ mô tả nghiệp vụ đến một ứng dụng Windows Forms C# có cơ sở dữ liệu,
thay vì tách rời phần phân tích thiết kế và phần lập trình.

## Nội dung thực hiện
- Khảo sát và phân loại nghiệp vụ (đầu vào - xử lý - kết xuất; lưu trữ - tra cứu - tính toán - thống kê)
- Bảng yêu cầu chức năng nghiệp vụ, Business Rules (BR01-BR13)
- Actor, Use Case, đặc tả Use Case (Basic/Alternative/Exception Flow)
- Class Diagram, ERD, Activity Diagram (swimlane), Sequence Diagram
- Thiết kế Screen: Overview - Screen Image - Screen Items - Event/Validation
- Cài đặt SQL Server LocalDB, ADO.NET, lớp Service, giao diện WinForms
- Nghiệp vụ: mượn - trả - phạt - thống kê
- Kiểm thử Business Rule (TC01-TC14)

## Cấu trúc Solution
```
QuanLyThuVien/
├── Data/          (Db.cs)
├── Services/      (DanhMucService, SachService, DocGiaService, MuonTraService, ThongKeService)
├── Forms/         (FrmMain, FrmDanhMuc, FrmSach, FrmDocGia, FrmMuonTra, FrmThongKe)
├── Models.cs
├── Program.cs
└── App.config
```

## Sản phẩm nộp bài
- [ ] 01 báo cáo Word (Bai_1_He_thong_quan_ly_thu_vien_FULL_TU_PHAN_TICH_DEN_CODE.docx)
- [ ] 01 mô hình UML (UML/QuanLyThuVien_UML.drawio + PNG)
- [ ] 01 workbook đặc tả Use Case + Screen Design (.xlsx)
- [ ] 01 script SQL (Database/QuanLyThuVien.sql)
- [ ] Ảnh chụp kết quả chạy
- [ ] Bảng test case (TC01-TC14)
- [ ] Solution Visual Studio 2022 (.sln), .NET Framework 4.7.2

## Công nghệ sử dụng
- Windows Forms (.NET Framework 4.7.2)
- SQL Server LocalDB / SQL Server Express
- ADO.NET (System.Data.SqlClient)

## Cách chạy
1. Cài Visual Studio 2022 (workload .NET desktop development) + SQL Server LocalDB.
2. Chạy `Database/QuanLyThuVien.sql` để tạo CSDL `QuanLyThuVienDB`.
3. Mở `QuanLyThuVien.sln`, kiểm tra connection string trong `App.config`.
4. Build (Rebuild Solution) rồi nhấn F5.
5. Kiểm thử theo trình tự: Danh mục → Sách → Độc giả → Mượn/Trả → Thống kê.
