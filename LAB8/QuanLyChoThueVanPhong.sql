-- LAB 08 - QUAN LY CHO THUE VAN PHONG CAO OC
-- Chay tren SQL Server Management Studio (SSMS)
-- Script tao 17 bang va du lieu mau, khong xoa du lieu hien co.
-- Neu bang da ton tai, script khong sua schema cua bang do.
USE master;
GO
IF DB_ID(N'QuanLyChoThueVanPhong') IS NULL CREATE DATABASE QuanLyChoThueVanPhong;
GO
USE QuanLyChoThueVanPhong;
GO
IF OBJECT_ID('dbo.Tang','U') IS NULL
CREATE TABLE dbo.Tang(MaTang varchar(20) NOT NULL PRIMARY KEY,TenTang nvarchar(100) NOT NULL,SoThuTu int NOT NULL UNIQUE,MoTa nvarchar(500) NULL,CONSTRAINT CK_Tang_SoThuTu CHECK(SoThuTu>=0));
GO
IF OBJECT_ID('dbo.Phong','U') IS NULL
CREATE TABLE dbo.Phong(MaPhong varchar(20) NOT NULL PRIMARY KEY,MaTang varchar(20) NOT NULL,TenPhong nvarchar(100) NOT NULL,DienTich decimal(10,2) NOT NULL,HuongPhong nvarchar(50) NOT NULL,DonGiaThang decimal(18,2) NOT NULL,TrangThai nvarchar(30) NOT NULL DEFAULT N'Trống',MoTa nvarchar(500) NULL,CONSTRAINT FK_Phong_Tang FOREIGN KEY(MaTang) REFERENCES dbo.Tang(MaTang),CONSTRAINT CK_Phong_DienTich CHECK(DienTich>0),CONSTRAINT CK_Phong_DonGia CHECK(DonGiaThang>=0),CONSTRAINT CK_Phong_TrangThai CHECK(TrangThai IN(N'Trống',N'Đang thuê',N'Bảo trì',N'Ngừng sử dụng')));
GO
IF OBJECT_ID('dbo.KhachHang','U') IS NULL
CREATE TABLE dbo.KhachHang(MaKhachHang varchar(20) NOT NULL PRIMARY KEY,TenKhachHang nvarchar(200) NOT NULL,LoaiKhachHang nvarchar(20) NOT NULL,MaSoThue varchar(30) NULL,NguoiDaiDien nvarchar(150) NULL,DienThoai varchar(20) NULL,Email varchar(150) NULL,DiaChi nvarchar(300) NULL,GhiChu nvarchar(500) NULL,CONSTRAINT CK_KH_Loai CHECK(LoaiKhachHang IN(N'Công ty',N'Cá nhân')));
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID('dbo.KhachHang') AND name='UX_KH_MaSoThue_NotNull')
CREATE UNIQUE INDEX UX_KH_MaSoThue_NotNull ON dbo.KhachHang(MaSoThue) WHERE MaSoThue IS NOT NULL;
GO
IF OBJECT_ID('dbo.HopDong','U') IS NULL
CREATE TABLE dbo.HopDong(MaHopDong varchar(20) NOT NULL PRIMARY KEY,MaKhachHang varchar(20) NOT NULL,NgayKy date NOT NULL,NgayBatDau date NOT NULL,NgayKetThuc date NOT NULL,SoThangThue int NOT NULL,NoiDung nvarchar(max) NULL,TrangThai nvarchar(30) NOT NULL DEFAULT N'Có hiệu lực',CONSTRAINT FK_HD_KhachHang FOREIGN KEY(MaKhachHang) REFERENCES dbo.KhachHang(MaKhachHang),CONSTRAINT CK_HD_SoThang CHECK(SoThangThue>=6),CONSTRAINT CK_HD_Ngay CHECK(NgayKetThuc>=NgayBatDau),CONSTRAINT CK_HD_TrangThai CHECK(TrangThai IN(N'Dự thảo',N'Có hiệu lực',N'Hết hạn',N'Đã chấm dứt')));
GO
IF OBJECT_ID('dbo.ChiTietHopDong','U') IS NULL
CREATE TABLE dbo.ChiTietHopDong(MaChiTietHD varchar(20) NOT NULL PRIMARY KEY,MaHopDong varchar(20) NOT NULL,MaPhong varchar(20) NOT NULL,DonGiaThue decimal(18,2) NOT NULL,NgayNhanPhong date NOT NULL,NgayTraPhong date NULL,GhiChu nvarchar(500) NULL,CONSTRAINT FK_CTHD_HopDong FOREIGN KEY(MaHopDong) REFERENCES dbo.HopDong(MaHopDong),CONSTRAINT FK_CTHD_Phong FOREIGN KEY(MaPhong) REFERENCES dbo.Phong(MaPhong),CONSTRAINT UQ_CTHD_HopDong_Phong UNIQUE(MaHopDong,MaPhong),CONSTRAINT CK_CTHD_DonGia CHECK(DonGiaThue>=0),CONSTRAINT CK_CTHD_Ngay CHECK(NgayTraPhong IS NULL OR NgayTraPhong>=NgayNhanPhong));
GO
IF OBJECT_ID('dbo.PhieuGiaHan','U') IS NULL
CREATE TABLE dbo.PhieuGiaHan(MaPhieuGiaHan varchar(20) NOT NULL PRIMARY KEY,MaChiTietHD varchar(20) NOT NULL,NgayLap date NOT NULL,TuNgay date NOT NULL,DenNgay date NOT NULL,SoThangGiaHan int NOT NULL,DonGiaGiaHan decimal(18,2) NOT NULL,TrangThai nvarchar(30) NOT NULL DEFAULT N'Đã duyệt',GhiChu nvarchar(500) NULL,CONSTRAINT FK_PGH_ChiTietHD FOREIGN KEY(MaChiTietHD) REFERENCES dbo.ChiTietHopDong(MaChiTietHD),CONSTRAINT CK_PGH_Thang CHECK(SoThangGiaHan>0),CONSTRAINT CK_PGH_Ngay CHECK(DenNgay>=TuNgay),CONSTRAINT CK_PGH_DonGia CHECK(DonGiaGiaHan>=0),CONSTRAINT CK_PGH_TrangThai CHECK(TrangThai IN(N'Chờ duyệt',N'Đã duyệt',N'Từ chối',N'Đã hủy')));
GO
IF OBJECT_ID('dbo.LoaiDichVu','U') IS NULL
CREATE TABLE dbo.LoaiDichVu(MaDichVu varchar(20) NOT NULL PRIMARY KEY,TenDichVu nvarchar(150) NOT NULL,DonViTinh nvarchar(50) NOT NULL,DonGia decimal(18,2) NOT NULL,DangSuDung bit NOT NULL DEFAULT 1,MoTa nvarchar(500) NULL,CONSTRAINT CK_LDV_DonGia CHECK(DonGia>=0));
GO
IF OBJECT_ID('dbo.SuDungDichVu','U') IS NULL
CREATE TABLE dbo.SuDungDichVu(MaSuDung varchar(20) NOT NULL PRIMARY KEY,MaChiTietHD varchar(20) NOT NULL,MaDichVu varchar(20) NOT NULL,Thang int NOT NULL,Nam int NOT NULL,SoLuong decimal(18,2) NOT NULL,DonGiaApDung decimal(18,2) NOT NULL,GhiChu nvarchar(500) NULL,CONSTRAINT FK_SDDV_ChiTietHD FOREIGN KEY(MaChiTietHD) REFERENCES dbo.ChiTietHopDong(MaChiTietHD),CONSTRAINT FK_SDDV_LoaiDichVu FOREIGN KEY(MaDichVu) REFERENCES dbo.LoaiDichVu(MaDichVu),CONSTRAINT UQ_SDDV_Ky UNIQUE(MaChiTietHD,MaDichVu,Thang,Nam),CONSTRAINT CK_SDDV_Thang CHECK(Thang BETWEEN 1 AND 12),CONSTRAINT CK_SDDV_Nam CHECK(Nam BETWEEN 2000 AND 2100),CONSTRAINT CK_SDDV_SoLuong CHECK(SoLuong>=0),CONSTRAINT CK_SDDV_DonGia CHECK(DonGiaApDung>=0));
GO
IF OBJECT_ID('dbo.HoaDonDichVu','U') IS NULL
CREATE TABLE dbo.HoaDonDichVu(MaHoaDon varchar(20) NOT NULL PRIMARY KEY,MaHopDong varchar(20) NOT NULL,Thang int NOT NULL,Nam int NOT NULL,NgayLap date NOT NULL,HanThanhToan date NOT NULL,TongTien decimal(18,2) NOT NULL DEFAULT 0,TrangThai nvarchar(30) NOT NULL DEFAULT N'Chưa thanh toán',CONSTRAINT FK_HDDV_HopDong FOREIGN KEY(MaHopDong) REFERENCES dbo.HopDong(MaHopDong),CONSTRAINT UQ_HDDV_Ky UNIQUE(MaHopDong,Thang,Nam),CONSTRAINT CK_HDDV_Thang CHECK(Thang BETWEEN 1 AND 12),CONSTRAINT CK_HDDV_Nam CHECK(Nam BETWEEN 2000 AND 2100),CONSTRAINT CK_HDDV_Ngay CHECK(HanThanhToan>=NgayLap),CONSTRAINT CK_HDDV_Tien CHECK(TongTien>=0),CONSTRAINT CK_HDDV_TrangThai CHECK(TrangThai IN(N'Chưa thanh toán',N'Đã thanh toán',N'Thanh toán một phần')));
GO
IF OBJECT_ID('dbo.ChiTietHoaDonDichVu','U') IS NULL
CREATE TABLE dbo.ChiTietHoaDonDichVu(MaHoaDon varchar(20) NOT NULL,MaSuDung varchar(20) NOT NULL,SoTien decimal(18,2) NOT NULL,CONSTRAINT PK_CTHDDV PRIMARY KEY(MaHoaDon,MaSuDung),CONSTRAINT FK_CTHDDV_HoaDon FOREIGN KEY(MaHoaDon) REFERENCES dbo.HoaDonDichVu(MaHoaDon),CONSTRAINT FK_CTHDDV_SuDung FOREIGN KEY(MaSuDung) REFERENCES dbo.SuDungDichVu(MaSuDung),CONSTRAINT UQ_CTHDDV_SuDung UNIQUE(MaSuDung),CONSTRAINT CK_CTHDDV_Tien CHECK(SoTien>=0));
GO
IF OBJECT_ID('dbo.ThanhToanTienThue','U') IS NULL
CREATE TABLE dbo.ThanhToanTienThue(MaThanhToan varchar(20) NOT NULL PRIMARY KEY,MaChiTietHD varchar(20) NOT NULL,NgayThanhToan date NOT NULL,TuNgay date NOT NULL,DenNgay date NOT NULL,SoThangThanhToan int NOT NULL,SoTien decimal(18,2) NOT NULL,PhuongThuc nvarchar(30) NOT NULL,GhiChu nvarchar(500) NULL,CONSTRAINT FK_TTTT_ChiTietHD FOREIGN KEY(MaChiTietHD) REFERENCES dbo.ChiTietHopDong(MaChiTietHD),CONSTRAINT CK_TTTT_Thang CHECK(SoThangThanhToan>0),CONSTRAINT CK_TTTT_Ngay CHECK(DenNgay>=TuNgay),CONSTRAINT CK_TTTT_Tien CHECK(SoTien>=0),CONSTRAINT CK_TTTT_PhuongThuc CHECK(PhuongThuc IN(N'Tiền mặt',N'Chuyển khoản')));
GO
IF OBJECT_ID('dbo.ThanhToanDichVu','U') IS NULL
CREATE TABLE dbo.ThanhToanDichVu(MaThanhToanDV varchar(20) NOT NULL PRIMARY KEY,MaHoaDon varchar(20) NOT NULL,NgayThanhToan date NOT NULL,SoTien decimal(18,2) NOT NULL,PhuongThuc nvarchar(30) NOT NULL,GhiChu nvarchar(500) NULL,CONSTRAINT FK_TTDV_HoaDon FOREIGN KEY(MaHoaDon) REFERENCES dbo.HoaDonDichVu(MaHoaDon),CONSTRAINT CK_TTDV_Tien CHECK(SoTien>0),CONSTRAINT CK_TTDV_PhuongThuc CHECK(PhuongThuc IN(N'Tiền mặt',N'Chuyển khoản')));
GO
IF OBJECT_ID('dbo.NhanVienVanPhong','U') IS NULL
CREATE TABLE dbo.NhanVienVanPhong(MaNhanVien varchar(20) NOT NULL PRIMARY KEY,MaKhachHang varchar(20) NOT NULL,MaPhong varchar(20) NULL,HoTen nvarchar(150) NOT NULL,NgaySinh date NULL,GioiTinh nvarchar(10) NULL,CCCD varchar(20) NULL,ChucVu nvarchar(100) NULL,DienThoai varchar(20) NULL,AnhNhanVien nvarchar(500) NULL,TrangThai nvarchar(30) NOT NULL DEFAULT N'Đang làm việc',CONSTRAINT FK_NVVP_KhachHang FOREIGN KEY(MaKhachHang) REFERENCES dbo.KhachHang(MaKhachHang),CONSTRAINT FK_NVVP_Phong FOREIGN KEY(MaPhong) REFERENCES dbo.Phong(MaPhong),CONSTRAINT CK_NVVP_GioiTinh CHECK(GioiTinh IS NULL OR GioiTinh IN(N'Nam',N'Nữ',N'Khác')),CONSTRAINT CK_NVVP_TrangThai CHECK(TrangThai IN(N'Đang làm việc',N'Đã nghỉ')));
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID('dbo.NhanVienVanPhong') AND name='UX_NVVP_CCCD_NotNull')
CREATE UNIQUE INDEX UX_NVVP_CCCD_NotNull ON dbo.NhanVienVanPhong(CCCD) WHERE CCCD IS NOT NULL;
GO
IF OBJECT_ID('dbo.TheGiuXe','U') IS NULL
CREATE TABLE dbo.TheGiuXe(MaThe varchar(20) NOT NULL PRIMARY KEY,MaNhanVien varchar(20) NOT NULL,BienSoXe varchar(30) NOT NULL,LoaiXe nvarchar(50) NOT NULL,MauXe nvarchar(50) NULL,NgayCap date NOT NULL,TrangThai nvarchar(30) NOT NULL DEFAULT N'Đang sử dụng',CONSTRAINT FK_TheXe_NhanVien FOREIGN KEY(MaNhanVien) REFERENCES dbo.NhanVienVanPhong(MaNhanVien),CONSTRAINT CK_TheXe_TrangThai CHECK(TrangThai IN(N'Đang sử dụng',N'Đã thu hồi',N'Bị mất')));
GO
IF OBJECT_ID('dbo.KiemTraPhong','U') IS NULL
CREATE TABLE dbo.KiemTraPhong(MaKiemTra varchar(20) NOT NULL PRIMARY KEY,MaPhong varchar(20) NOT NULL,NgayKiemTra date NOT NULL,TinhTrang nvarchar(30) NOT NULL,NoiDung nvarchar(1000) NULL,NguoiKiemTra nvarchar(150) NULL,DeXuatSuaChua nvarchar(1000) NULL,CONSTRAINT FK_KTP_Phong FOREIGN KEY(MaPhong) REFERENCES dbo.Phong(MaPhong),CONSTRAINT CK_KTP_TinhTrang CHECK(TinhTrang IN(N'Tốt',N'Cần sửa chữa',N'Hư hỏng')));
GO
IF OBJECT_ID('dbo.ViPhamPhong','U') IS NULL
CREATE TABLE dbo.ViPhamPhong(MaViPham varchar(20) NOT NULL PRIMARY KEY,MaChiTietHD varchar(20) NOT NULL,NgayPhatHien date NOT NULL,NoiDung nvarchar(1000) NOT NULL,SoTienPhat decimal(18,2) NOT NULL DEFAULT 0,TrangThai nvarchar(30) NOT NULL DEFAULT N'Chưa xử lý',NgayXuLy date NULL,GhiChu nvarchar(500) NULL,CONSTRAINT FK_VP_ChiTietHD FOREIGN KEY(MaChiTietHD) REFERENCES dbo.ChiTietHopDong(MaChiTietHD),CONSTRAINT CK_VP_Tien CHECK(SoTienPhat>=0),CONSTRAINT CK_VP_TrangThai CHECK(TrangThai IN(N'Chưa xử lý',N'Đã xử lý',N'Đã thu phạt')),CONSTRAINT CK_VP_Ngay CHECK(NgayXuLy IS NULL OR NgayXuLy>=NgayPhatHien));
GO
IF OBJECT_ID('dbo.SuaChuaPhong','U') IS NULL
CREATE TABLE dbo.SuaChuaPhong(MaSuaChua varchar(20) NOT NULL PRIMARY KEY,MaPhong varchar(20) NOT NULL,MaKiemTra varchar(20) NULL,NgayBatDau date NOT NULL,NgayHoanThanh date NULL,NoiDung nvarchar(1000) NOT NULL,ChiPhi decimal(18,2) NOT NULL DEFAULT 0,TrangThai nvarchar(30) NOT NULL DEFAULT N'Dự kiến',CONSTRAINT FK_SC_Phong FOREIGN KEY(MaPhong) REFERENCES dbo.Phong(MaPhong),CONSTRAINT FK_SC_KiemTra FOREIGN KEY(MaKiemTra) REFERENCES dbo.KiemTraPhong(MaKiemTra),CONSTRAINT CK_SC_Ngay CHECK(NgayHoanThanh IS NULL OR NgayHoanThanh>=NgayBatDau),CONSTRAINT CK_SC_ChiPhi CHECK(ChiPhi>=0),CONSTRAINT CK_SC_TrangThai CHECK(TrangThai IN(N'Dự kiến',N'Đang sửa chữa',N'Hoàn thành')));
GO
-- ==================================================
-- DU LIEU MAU: ghi theo thu tu bang cha -> bang con
-- Neu co loi: ROLLBACK tat ca insert trong lan chay nay.
-- ==================================================
SET XACT_ABORT ON;
BEGIN TRY
 BEGIN TRANSACTION;
 IF NOT EXISTS(SELECT 1 FROM dbo.Tang WHERE MaTang='T01') INSERT dbo.Tang VALUES('T01',N'Tầng 1',1,N'Tầng văn phòng');
 IF NOT EXISTS(SELECT 1 FROM dbo.Tang WHERE MaTang='T02') INSERT dbo.Tang VALUES('T02',N'Tầng 2',2,N'Tầng văn phòng');
 IF NOT EXISTS(SELECT 1 FROM dbo.Tang WHERE MaTang='T03') INSERT dbo.Tang VALUES('T03',N'Tầng 3',3,N'Tầng văn phòng');
 IF NOT EXISTS(SELECT 1 FROM dbo.Phong WHERE MaPhong='P101') INSERT dbo.Phong(MaPhong,MaTang,TenPhong,DienTich,HuongPhong,DonGiaThang,TrangThai) VALUES('P101','T01',N'Phòng 101',50,N'Đông',10000000,N'Đang thuê');
 IF NOT EXISTS(SELECT 1 FROM dbo.Phong WHERE MaPhong='P102') INSERT dbo.Phong(MaPhong,MaTang,TenPhong,DienTich,HuongPhong,DonGiaThang,TrangThai) VALUES('P102','T01',N'Phòng 102',60,N'Tây',12000000,N'Đang thuê');
 IF NOT EXISTS(SELECT 1 FROM dbo.Phong WHERE MaPhong='P201') INSERT dbo.Phong(MaPhong,MaTang,TenPhong,DienTich,HuongPhong,DonGiaThang,TrangThai) VALUES('P201','T02',N'Phòng 201',75,N'Nam',16000000,N'Trống');
 IF NOT EXISTS(SELECT 1 FROM dbo.Phong WHERE MaPhong='P301') INSERT dbo.Phong(MaPhong,MaTang,TenPhong,DienTich,HuongPhong,DonGiaThang,TrangThai) VALUES('P301','T03',N'Phòng 301',100,N'Bắc',22000000,N'Trống');
 IF NOT EXISTS(SELECT 1 FROM dbo.KhachHang WHERE MaKhachHang='KH001') INSERT dbo.KhachHang(MaKhachHang,TenKhachHang,LoaiKhachHang,NguoiDaiDien,DienThoai,Email,DiaChi) VALUES('KH001',N'Công ty Minh Phát',N'Công ty',N'Nguyễn Minh An','0900000001','contact@example.com',N'TP. Hồ Chí Minh');
 IF NOT EXISTS(SELECT 1 FROM dbo.KhachHang WHERE MaKhachHang='KH002') INSERT dbo.KhachHang(MaKhachHang,TenKhachHang,LoaiKhachHang,NguoiDaiDien,DienThoai,DiaChi) VALUES('KH002',N'Công ty Sao Việt',N'Công ty',N'Trần Thị Bình','0900000002',N'TP. Hồ Chí Minh');
 IF NOT EXISTS(SELECT 1 FROM dbo.HopDong WHERE MaHopDong='HD001') INSERT dbo.HopDong VALUES('HD001','KH001','2025-12-20','2026-01-01','2026-12-31',12,N'Thuê hai phòng P101 và P102, trả trước 6 tháng',N'Có hiệu lực');
 IF NOT EXISTS(SELECT 1 FROM dbo.ChiTietHopDong WHERE MaChiTietHD='CTHD001') INSERT dbo.ChiTietHopDong(MaChiTietHD,MaHopDong,MaPhong,DonGiaThue,NgayNhanPhong) VALUES('CTHD001','HD001','P101',10000000,'2026-01-01');
 IF NOT EXISTS(SELECT 1 FROM dbo.ChiTietHopDong WHERE MaChiTietHD='CTHD002') INSERT dbo.ChiTietHopDong(MaChiTietHD,MaHopDong,MaPhong,DonGiaThue,NgayNhanPhong) VALUES('CTHD002','HD001','P102',12000000,'2026-01-01');
 IF NOT EXISTS(SELECT 1 FROM dbo.PhieuGiaHan WHERE MaPhieuGiaHan='GH001') INSERT dbo.PhieuGiaHan VALUES('GH001','CTHD001','2026-10-01','2027-01-01','2027-06-30',6,10000000,N'Chờ duyệt',N'Chưa phê duyệt; cần kiểm tra công nợ');
 IF NOT EXISTS(SELECT 1 FROM dbo.LoaiDichVu WHERE MaDichVu='DV001') INSERT dbo.LoaiDichVu(MaDichVu,TenDichVu,DonViTinh,DonGia) VALUES('DV001',N'Điện',N'kWh',3500);
 IF NOT EXISTS(SELECT 1 FROM dbo.LoaiDichVu WHERE MaDichVu='DV002') INSERT dbo.LoaiDichVu(MaDichVu,TenDichVu,DonViTinh,DonGia) VALUES('DV002',N'Nước',N'm3',18000);
 IF NOT EXISTS(SELECT 1 FROM dbo.LoaiDichVu WHERE MaDichVu='DV003') INSERT dbo.LoaiDichVu(MaDichVu,TenDichVu,DonViTinh,DonGia) VALUES('DV003',N'Internet',N'Tháng',500000);
 IF NOT EXISTS(SELECT 1 FROM dbo.SuDungDichVu WHERE MaSuDung='SD001') INSERT dbo.SuDungDichVu(MaSuDung,MaChiTietHD,MaDichVu,Thang,Nam,SoLuong,DonGiaApDung) VALUES('SD001','CTHD001','DV001',9,2026,100,3500);
 IF NOT EXISTS(SELECT 1 FROM dbo.SuDungDichVu WHERE MaSuDung='SD002') INSERT dbo.SuDungDichVu(MaSuDung,MaChiTietHD,MaDichVu,Thang,Nam,SoLuong,DonGiaApDung) VALUES('SD002','CTHD001','DV002',9,2026,10,18000);
 IF NOT EXISTS(SELECT 1 FROM dbo.SuDungDichVu WHERE MaSuDung='SD003') INSERT dbo.SuDungDichVu(MaSuDung,MaChiTietHD,MaDichVu,Thang,Nam,SoLuong,DonGiaApDung) VALUES('SD003','CTHD001','DV003',9,2026,1,500000);
 IF NOT EXISTS(SELECT 1 FROM dbo.HoaDonDichVu WHERE MaHoaDon='HDDV001') INSERT dbo.HoaDonDichVu VALUES('HDDV001','HD001',9,2026,'2026-09-30','2026-10-05',1030000,N'Đã thanh toán');
 IF NOT EXISTS(SELECT 1 FROM dbo.ChiTietHoaDonDichVu WHERE MaHoaDon='HDDV001' AND MaSuDung='SD001') INSERT dbo.ChiTietHoaDonDichVu VALUES('HDDV001','SD001',350000);
 IF NOT EXISTS(SELECT 1 FROM dbo.ChiTietHoaDonDichVu WHERE MaHoaDon='HDDV001' AND MaSuDung='SD002') INSERT dbo.ChiTietHoaDonDichVu VALUES('HDDV001','SD002',180000);
 IF NOT EXISTS(SELECT 1 FROM dbo.ChiTietHoaDonDichVu WHERE MaHoaDon='HDDV001' AND MaSuDung='SD003') INSERT dbo.ChiTietHoaDonDichVu VALUES('HDDV001','SD003',500000);
 IF NOT EXISTS(SELECT 1 FROM dbo.ThanhToanTienThue WHERE MaThanhToan='TT001') INSERT dbo.ThanhToanTienThue VALUES('TT001','CTHD001','2025-12-25','2026-01-01','2026-06-30',6,60000000,N'Chuyển khoản',N'Trả trước P101');
 IF NOT EXISTS(SELECT 1 FROM dbo.ThanhToanTienThue WHERE MaThanhToan='TT002') INSERT dbo.ThanhToanTienThue VALUES('TT002','CTHD002','2025-12-25','2026-01-01','2026-06-30',6,72000000,N'Chuyển khoản',N'Trả trước P102');
 IF NOT EXISTS(SELECT 1 FROM dbo.ThanhToanTienThue WHERE MaThanhToan='TT003') INSERT dbo.ThanhToanTienThue VALUES('TT003','CTHD001','2026-07-01','2026-07-01','2026-07-31',1,10000000,N'Tiền mặt',N'Thuê P101 tháng 7');
 IF NOT EXISTS(SELECT 1 FROM dbo.ThanhToanDichVu WHERE MaThanhToanDV='TDV001') INSERT dbo.ThanhToanDichVu VALUES('TDV001','HDDV001','2026-10-02',1030000,N'Chuyển khoản',N'Thanh toán dịch vụ T9');
 IF NOT EXISTS(SELECT 1 FROM dbo.NhanVienVanPhong WHERE MaNhanVien='NV001') INSERT dbo.NhanVienVanPhong(MaNhanVien,MaKhachHang,MaPhong,HoTen,NgaySinh,GioiTinh,ChucVu,DienThoai) VALUES('NV001','KH001','P101',N'Nguyễn Văn Bình','1995-05-10',N'Nam',N'Nhân viên','0900000011');
 IF NOT EXISTS(SELECT 1 FROM dbo.NhanVienVanPhong WHERE MaNhanVien='NV002') INSERT dbo.NhanVienVanPhong(MaNhanVien,MaKhachHang,MaPhong,HoTen,NgaySinh,GioiTinh,ChucVu,DienThoai) VALUES('NV002','KH001','P102',N'Trần Thị Mai','1998-08-20',N'Nữ',N'Kế toán','0900000012');
 IF NOT EXISTS(SELECT 1 FROM dbo.TheGiuXe WHERE MaThe='THE001') INSERT dbo.TheGiuXe VALUES('THE001','NV001','DEMO-001',N'Xe máy',N'Đen','2026-01-05',N'Đang sử dụng');
 IF NOT EXISTS(SELECT 1 FROM dbo.KiemTraPhong WHERE MaKiemTra='KT001') INSERT dbo.KiemTraPhong VALUES('KT001','P201','2026-10-01',N'Cần sửa chữa',N'Kiểm tra định kỳ',N'Nhân viên quản lý',N'Bảo trì đèn');
 IF NOT EXISTS(SELECT 1 FROM dbo.ViPhamPhong WHERE MaViPham='VP001') INSERT dbo.ViPhamPhong VALUES('VP001','CTHD001','2026-09-15',N'Vi phạm giả lập phục vụ kiểm thử',500000,N'Chưa xử lý',NULL,N'Dữ liệu mẫu');
 IF NOT EXISTS(SELECT 1 FROM dbo.SuaChuaPhong WHERE MaSuaChua='SC001') INSERT dbo.SuaChuaPhong VALUES('SC001','P201','KT001','2026-10-05',NULL,N'Bảo trì hệ thống chiếu sáng',2000000,N'Đang sửa chữa');
 COMMIT TRANSACTION;
 PRINT N'OK - Du lieu mau da duoc kiem tra/bo sung.';
END TRY
BEGIN CATCH
 IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
 SELECT ERROR_NUMBER() AS MaLoi, ERROR_LINE() AS DongLoi, ERROR_MESSAGE() AS NoiDungLoi;
 THROW;
END CATCH;
GO
-- KIEM TRA KET QUA
SELECT COUNT(*) AS TongSoBang FROM sys.tables WHERE is_ms_shipped=0;
SELECT t.name AS TenBang, SUM(p.rows) AS SoDong FROM sys.tables t JOIN sys.partitions p ON t.object_id=p.object_id WHERE p.index_id IN(0,1) GROUP BY t.name ORDER BY t.name;
SELECT MaPhong,MaTang,DienTich,DonGiaThang,TrangThai FROM dbo.Phong ORDER BY MaPhong;
GO
-- Chu y: kiem tra no, gia han <= 1/2 thoi han thue va trung lich thue
-- can duoc kiem tra them o Service, khong chi dua vao CHECK constraint.
