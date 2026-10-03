# Lab 04 — Hệ thống cửa hàng online e-SHOPPING

- **Sinh viên:** Phạm Gia Hiếu
- **MSSV:** 1250080053
- **Lớp:** ĐH_K12_CNPM1
- **Công nghệ:** C# WinForms (.NET Framework 4.7.2), SQL Server, ADO.NET
- **Công cụ vẽ UML:** draw.io


## 1. Cấu trúc thư mục nộp

```
Lab04/
├── README.md
├── BaoCao_Lab4.docx (và .pdf)        Báo cáo
├── UML/
│   └── eShopping_UML.drawio          5 sơ đồ: use case tổng quát, use case phân rã,
│                                     activity UC07, sequence UC07, sequence UC05
├── Database/
│   └── [ĐIỀN: tên file script .sql hoặc .bak]
└── Source/
    ├── EShopping.sln
    ├── EShopping.Domain/             Entity, enum, interface
    ├── EShopping.Data/               Repository (ADO.NET, SQL Server)
    ├── EShopping.Services/           Service, Adapter/Mock hệ thống ngoài
    └── EShopping.UI/                 Các Form WinForms
```

## 2. Kiến trúc

Hệ thống chia 4 project theo kiến trúc **UI → Service/Adapter → Data**:

| Project | Tầng | Nội dung |
|---|---|---|
| EShopping.Domain | Domain | Entity, interface |
| EShopping.Data | Data | Truy cập SQL Server |
| EShopping.Services | Service/Adapter | Nghiệp vụ (giỏ hàng, tính phí, kiểm tra thẻ, đặt hàng) và Adapter giả lập 3 hệ thống ngoài |
| EShopping.UI | UI | Form WinForms; chỉ gọi Service, không viết SQL |

Ba hệ thống/dịch vụ bên ngoài được giả lập: Hệ thống quản lý sản phẩm (chỉ đọc), Dịch vụ thanh toán trực tuyến, Dịch vụ email.

## 3. Yêu cầu môi trường

- Windows, Visual Studio có workload **.NET desktop development**
- .NET Framework 4.7.2
- SQL Server (Express/LocalDB/Developer) và SQL Server Management Studio

## 4. Hướng dẫn chạy

**Bước 1. Tạo cơ sở dữ liệu**
1. Mở SSMS, kết nối vào SQL Server của bạn.
2. Mở file `Database/[ĐIỀN]` và chạy toàn bộ (F5).
   (Nếu nộp file `.bak`: chuột phải Databases → Restore Database → Device → chọn file.)
3. Kiểm tra database tên **[ĐIỀN: tên database]** đã có đủ bảng và dữ liệu mẫu.

**Bước 2. Sửa chuỗi kết nối**

Mở `Source/EShopping.UI/App.config`, sửa phần `connectionStrings` cho đúng máy của bạn:

```xml
<connectionStrings>
  <add name="[ĐIỀN: tên connection string trong App.config]"
       connectionString="Server=[ĐIỀN: tên server];Database=[ĐIỀN: tên database];Integrated Security=true;"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

**Bước 3. Build và chạy**
1. Mở `Source/EShopping.sln` bằng Visual Studio.
2. Chuột phải project **EShopping.UI** → Set as Startup Project.
3. Nhấn **Ctrl+Shift+B** để build, rồi **F5** để chạy.

## 5. Tài khoản và dữ liệu để thử

| Tài khoản | Mật khẩu | Ghi chú |
|---|---|---|
| [ĐIỀN] | [ĐIỀN] | Khách **có** email (nhận email xác nhận) |
| [ĐIỀN] | [ĐIỀN] | Khách **không có** email |

Có thể bấm **Đăng ký** để tạo tài khoản mới.

**Thẻ tín dụng để thử** (quy tắc theo đề):

| Loại thẻ | Số thẻ | CSV |
|---|---|---|
| Visa / Master / Discover | 16 chữ số | 3 chữ số |
| American Express | 15 chữ số | 4 chữ số |

Ngày hết hạn nhập dạng `MM/yy`, phải còn hạn.
Thẻ thử trường hợp **thanh toán bị từ chối**: [ĐIỀN: số thẻ/quy tắc giả lập từ chối trong code, nếu có].

## 6. Chức năng đã hiện thực

- Xem danh sách sản phẩm theo nhóm, xem chi tiết sản phẩm
- Thêm sản phẩm vào giỏ (từ chối sản phẩm hết hàng, cộng dồn số lượng), cập nhật/xóa trong giỏ
- Đăng ký, đăng nhập (đăng nhập được yêu cầu khi tính tiền)
- Đặt hàng và tính tiền: chọn loại phiếu (thường / chuyển phát nhanh / nhanh trong ngày), nhập người nhận (có thể khác người mua), tính phí giao hàng theo khu vực và ngưỡng miễn phí, lệ phí thẻ, tổng cộng
- Kiểm tra định dạng thẻ, thanh toán qua dịch vụ giả lập, chỉ ghi đơn khi thanh toán thành công
- Gửi email xác nhận (giả lập) khi khách có email, không chứa thông tin thẻ

## 7. Quy tắc nghiệp vụ chính

- Đơn từ 1.000.000đ: miễn phí chuyển phát nhanh. Đơn từ 5.000.000đ: miễn phí chuyển phát nhanh trong ngày.
- Đơn lưu đơn giá tại thời điểm đặt.
- Không lưu CSV và số thẻ đầy đủ; mật khẩu không lưu văn bản gốc.

## 8. Các lỗi thường gặp

| Lỗi | Cách xử lý |
|---|---|
| Không kết nối được CSDL | Kiểm tra tên server, tên database trong `App.config`; SQL Server đã chạy chưa |
| Không có dữ liệu sản phẩm | Chưa chạy script/restore ở Bước 1 |
| Lỗi build do tham chiếu project | Chuột phải solution → Restore NuGet Packages / Rebuild Solution |
| Form không hiện, báo thiếu `System.Configuration` | Thêm Reference `System.Configuration` cho EShopping.UI |
