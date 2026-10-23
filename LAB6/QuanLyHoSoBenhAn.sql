/*
  BAI 5 - QUAN LY HO SO BENH AN
  Tac gia: Pham Gia Hieu - 12_CNPM1
  SQL Server / SSMS
  Chay tren database hoc tap. Script khong DROP bang hoac xoa du lieu.
  Cac gia tri mau la gia lap; khong dung cho cham soc y te that.
*/
IF DB_ID(N'QuanLyHoSoBenhAn') IS NULL
    CREATE DATABASE QuanLyHoSoBenhAn;
GO
USE QuanLyHoSoBenhAn;
GO

-- 1. BENH NHAN
IF OBJECT_ID(N'dbo.BenhNhan',N'U') IS NULL
CREATE TABLE dbo.BenhNhan (
 MaBenhNhan varchar(20) NOT NULL CONSTRAINT PK_BenhNhan PRIMARY KEY,
 HoTen nvarchar(120) NOT NULL,
 NgaySinh date NOT NULL,
 GioiTinh nvarchar(10) NULL CONSTRAINT CK_BenhNhan_GioiTinh CHECK (GioiTinh IN (N'Nam',N'Nữ',N'Khác')),
 DiaChi nvarchar(250) NOT NULL,
 DienThoai varchar(20) NOT NULL,
 Anh3x4 nvarchar(500) NULL,
 NgayTao datetime2 NOT NULL CONSTRAINT DF_BenhNhan_NgayTao DEFAULT SYSDATETIME()
);
GO
-- 2. PHIEU DANG KY
IF OBJECT_ID(N'dbo.PhieuDangKy',N'U') IS NULL
CREATE TABLE dbo.PhieuDangKy (
 MaPhieuDK varchar(20) NOT NULL CONSTRAINT PK_PhieuDangKy PRIMARY KEY,
 MaBenhNhan varchar(20) NOT NULL CONSTRAINT UQ_PhieuDangKy_BenhNhan UNIQUE,
 NgayDangKy date NOT NULL CONSTRAINT DF_PhieuDangKy_Ngay DEFAULT CONVERT(date,GETDATE()),
 NoiDungDangKy nvarchar(500) NULL,
 TrangThai nvarchar(30) NOT NULL CONSTRAINT DF_PhieuDangKy_TrangThai DEFAULT N'Đã đăng ký',
 CONSTRAINT FK_PhieuDangKy_BenhNhan FOREIGN KEY (MaBenhNhan) REFERENCES dbo.BenhNhan(MaBenhNhan),
 CONSTRAINT CK_PhieuDangKy_TrangThai CHECK (TrangThai IN (N'Đã đăng ký',N'Đã cấp hồ sơ'))
);
GO
-- 3. HO SO BENH AN
IF OBJECT_ID(N'dbo.HoSoBenhAn',N'U') IS NULL
CREATE TABLE dbo.HoSoBenhAn (
 MaHoSo varchar(20) NOT NULL CONSTRAINT PK_HoSoBenhAn PRIMARY KEY,
 MaBenhNhan varchar(20) NOT NULL CONSTRAINT UQ_HoSoBenhAn_BenhNhan UNIQUE,
 MaPhieuDK varchar(20) NOT NULL CONSTRAINT UQ_HoSoBenhAn_PhieuDK UNIQUE,
 NgayLap date NOT NULL,
 TinhTrangSucKhoe nvarchar(1000) NULL,
 GhiChu nvarchar(1000) NULL,
 TrangThai nvarchar(30) NOT NULL CONSTRAINT DF_HoSo_TrangThai DEFAULT N'Đang theo dõi',
 CONSTRAINT FK_HoSo_BenhNhan FOREIGN KEY (MaBenhNhan) REFERENCES dbo.BenhNhan(MaBenhNhan),
 CONSTRAINT FK_HoSo_PhieuDK FOREIGN KEY (MaPhieuDK) REFERENCES dbo.PhieuDangKy(MaPhieuDK)
);
GO
-- 4. LE PHI HANG NAM
IF OBJECT_ID(N'dbo.LePhiHangNam',N'U') IS NULL
CREATE TABLE dbo.LePhiHangNam (
 MaLePhi varchar(20) NOT NULL CONSTRAINT PK_LePhiHangNam PRIMARY KEY,
 MaBenhNhan varchar(20) NOT NULL,
 NamDong int NOT NULL CONSTRAINT CK_LePhi_Nam CHECK (NamDong BETWEEN 2000 AND 2100),
 SoTien decimal(18,2) NOT NULL CONSTRAINT CK_LePhi_SoTien CHECK (SoTien > 0),
 NgayDong date NOT NULL,
 TrangThai nvarchar(30) NOT NULL CONSTRAINT DF_LePhi_TrangThai DEFAULT N'Đã thanh toán',
 GhiChu nvarchar(500) NULL,
 CONSTRAINT FK_LePhi_BenhNhan FOREIGN KEY (MaBenhNhan) REFERENCES dbo.BenhNhan(MaBenhNhan),
 CONSTRAINT UQ_LePhi_BenhNhan_Nam UNIQUE (MaBenhNhan,NamDong),
 CONSTRAINT CK_LePhi_TrangThai CHECK (TrangThai IN (N'Chưa thanh toán',N'Đã thanh toán'))
);
GO
-- 5. KHOA DIEU TRI
IF OBJECT_ID(N'dbo.KhoaDieuTri',N'U') IS NULL
CREATE TABLE dbo.KhoaDieuTri (
 MaKhoa varchar(20) NOT NULL CONSTRAINT PK_KhoaDieuTri PRIMARY KEY,
 TenKhoa nvarchar(150) NOT NULL CONSTRAINT UQ_KhoaDieuTri_Ten UNIQUE,
 ViTri nvarchar(200) NULL,
 DienThoai varchar(20) NULL,
 MoTa nvarchar(500) NULL
);
GO
-- 6. BAC SI
IF OBJECT_ID(N'dbo.BacSi',N'U') IS NULL
CREATE TABLE dbo.BacSi (
 MaBacSi varchar(20) NOT NULL CONSTRAINT PK_BacSi PRIMARY KEY,
 HoTen nvarchar(120) NOT NULL,
 MaKhoa varchar(20) NOT NULL,
 ChuyenMon nvarchar(150) NULL,
 DienThoai varchar(20) NULL,
 DangLamViec bit NOT NULL CONSTRAINT DF_BacSi_DangLamViec DEFAULT 1,
 CONSTRAINT FK_BacSi_Khoa FOREIGN KEY (MaKhoa) REFERENCES dbo.KhoaDieuTri(MaKhoa)
);
GO
-- 7. PHIEU KHAM
IF OBJECT_ID(N'dbo.PhieuKham',N'U') IS NULL
CREATE TABLE dbo.PhieuKham (
 MaPhieuKham varchar(20) NOT NULL CONSTRAINT PK_PhieuKham PRIMARY KEY,
 MaHoSo varchar(20) NOT NULL,
 MaBacSi varchar(20) NOT NULL,
 MaKhoa varchar(20) NOT NULL,
 NgayGioKham datetime2 NOT NULL,
 LoaiKham nvarchar(30) NOT NULL,
 TrieuChung nvarchar(1000) NULL,
 NhietDo decimal(4,1) NULL,
 HuyetAp varchar(20) NULL,
 ChanDoan nvarchar(1000) NULL,
 HuongDieuTri nvarchar(1000) NULL,
 NgayXuatVien date NULL,
 GhiChu nvarchar(1000) NULL,
 TrangThai nvarchar(30) NOT NULL CONSTRAINT DF_PhieuKham_TrangThai DEFAULT N'Đang khám',
 CONSTRAINT FK_PhieuKham_HoSo FOREIGN KEY (MaHoSo) REFERENCES dbo.HoSoBenhAn(MaHoSo),
 CONSTRAINT FK_PhieuKham_BacSi FOREIGN KEY (MaBacSi) REFERENCES dbo.BacSi(MaBacSi),
 CONSTRAINT FK_PhieuKham_Khoa FOREIGN KEY (MaKhoa) REFERENCES dbo.KhoaDieuTri(MaKhoa),
 CONSTRAINT CK_PhieuKham_Loai CHECK (LoaiKham IN (N'Khám bệnh',N'Khám định kỳ')),
 CONSTRAINT CK_PhieuKham_TrangThai CHECK (TrangThai IN (N'Đang khám',N'Đang điều trị',N'Đã hoàn tất')),
 CONSTRAINT CK_PhieuKham_NhietDo CHECK (NhietDo IS NULL OR NhietDo BETWEEN 25 AND 45),
 CONSTRAINT CK_PhieuKham_NgayXuatVien CHECK (NgayXuatVien IS NULL OR NgayXuatVien >= CONVERT(date,NgayGioKham))
);
GO
-- 8. XET NGHIEM
IF OBJECT_ID(N'dbo.XetNghiem',N'U') IS NULL
CREATE TABLE dbo.XetNghiem (
 MaXetNghiem varchar(20) NOT NULL CONSTRAINT PK_XetNghiem PRIMARY KEY,
 MaPhieuKham varchar(20) NOT NULL,
 TenXetNghiem nvarchar(200) NOT NULL,
 NgayChiDinh datetime2 NOT NULL,
 NgayCoKetQua datetime2 NULL,
 KetQua nvarchar(1500) NULL,
 GhiChu nvarchar(500) NULL,
 TrangThai nvarchar(30) NOT NULL CONSTRAINT DF_XetNghiem_TrangThai DEFAULT N'Đã chỉ định',
 CONSTRAINT FK_XetNghiem_PhieuKham FOREIGN KEY (MaPhieuKham) REFERENCES dbo.PhieuKham(MaPhieuKham),
 CONSTRAINT CK_XetNghiem_TrangThai CHECK (TrangThai IN (N'Đã chỉ định',N'Đang thực hiện',N'Đã có kết quả')),
 CONSTRAINT CK_XetNghiem_Ngay CHECK (NgayCoKetQua IS NULL OR NgayCoKetQua >= NgayChiDinh)
);
GO
-- 9. THUOC
IF OBJECT_ID(N'dbo.Thuoc',N'U') IS NULL
CREATE TABLE dbo.Thuoc (
 MaThuoc varchar(20) NOT NULL CONSTRAINT PK_Thuoc PRIMARY KEY,
 TenThuoc nvarchar(200) NOT NULL,
 DonViTinh nvarchar(50) NOT NULL,
 MoTa nvarchar(500) NULL,
 DangSuDung bit NOT NULL CONSTRAINT DF_Thuoc_DangSuDung DEFAULT 1
);
GO
-- 10. TOA THUOC
IF OBJECT_ID(N'dbo.ToaThuoc',N'U') IS NULL
CREATE TABLE dbo.ToaThuoc (
 MaToaThuoc varchar(20) NOT NULL CONSTRAINT PK_ToaThuoc PRIMARY KEY,
 MaPhieuKham varchar(20) NOT NULL CONSTRAINT UQ_ToaThuoc_PhieuKham UNIQUE,
 MaBacSi varchar(20) NOT NULL,
 NgayLap datetime2 NOT NULL,
 LoiDan nvarchar(1000) NULL,
 CONSTRAINT FK_ToaThuoc_PhieuKham FOREIGN KEY (MaPhieuKham) REFERENCES dbo.PhieuKham(MaPhieuKham),
 CONSTRAINT FK_ToaThuoc_BacSi FOREIGN KEY (MaBacSi) REFERENCES dbo.BacSi(MaBacSi)
);
GO
-- 11. CHI TIET TOA THUOC
IF OBJECT_ID(N'dbo.ChiTietToaThuoc',N'U') IS NULL
CREATE TABLE dbo.ChiTietToaThuoc (
 MaToaThuoc varchar(20) NOT NULL,
 MaThuoc varchar(20) NOT NULL,
 SoLuong int NOT NULL CONSTRAINT CK_CTToa_SoLuong CHECK (SoLuong > 0),
 LieuDung nvarchar(250) NOT NULL,
 CachDung nvarchar(500) NULL,
 SoNgayDung int NULL CONSTRAINT CK_CTToa_SoNgay CHECK (SoNgayDung IS NULL OR SoNgayDung > 0),
 CONSTRAINT PK_ChiTietToaThuoc PRIMARY KEY (MaToaThuoc,MaThuoc),
 CONSTRAINT FK_CTToa_ToaThuoc FOREIGN KEY (MaToaThuoc) REFERENCES dbo.ToaThuoc(MaToaThuoc),
 CONSTRAINT FK_CTToa_Thuoc FOREIGN KEY (MaThuoc) REFERENCES dbo.Thuoc(MaThuoc)
);
GO
-- 12. LICH HEN KHAM
IF OBJECT_ID(N'dbo.LichHenKham',N'U') IS NULL
CREATE TABLE dbo.LichHenKham (
 MaLichHen varchar(20) NOT NULL CONSTRAINT PK_LichHenKham PRIMARY KEY,
 MaHoSo varchar(20) NOT NULL,
 MaKhoa varchar(20) NOT NULL,
 NgayGioHen datetime2 NOT NULL,
 LoaiHen nvarchar(30) NOT NULL,
 NoiDung nvarchar(500) NULL,
 TrangThai nvarchar(30) NOT NULL CONSTRAINT DF_LichHen_TrangThai DEFAULT N'Đã đặt lịch',
 CONSTRAINT FK_LichHen_HoSo FOREIGN KEY (MaHoSo) REFERENCES dbo.HoSoBenhAn(MaHoSo),
 CONSTRAINT FK_LichHen_Khoa FOREIGN KEY (MaKhoa) REFERENCES dbo.KhoaDieuTri(MaKhoa),
 CONSTRAINT CK_LichHen_Loai CHECK (LoaiHen IN (N'Khám định kỳ',N'Tái khám')),
 CONSTRAINT CK_LichHen_TrangThai CHECK (TrangThai IN (N'Đã đặt lịch',N'Đã khám',N'Đã hủy')),
 CONSTRAINT UQ_LichHen_HoSo_Ngay UNIQUE (MaHoSo,NgayGioHen)
);
GO
-- 13. THANH TOAN VIEN PHI
IF OBJECT_ID(N'dbo.ThanhToanVienPhi',N'U') IS NULL
CREATE TABLE dbo.ThanhToanVienPhi (
 MaThanhToan varchar(20) NOT NULL CONSTRAINT PK_ThanhToanVienPhi PRIMARY KEY,
 MaPhieuKham varchar(20) NOT NULL,
 NgayThanhToan datetime2 NOT NULL,
 SoTien decimal(18,2) NOT NULL CONSTRAINT CK_ThanhToan_SoTien CHECK (SoTien >= 0),
 PhuongThuc nvarchar(30) NOT NULL,
 GhiChu nvarchar(500) NULL,
 CONSTRAINT FK_ThanhToan_PhieuKham FOREIGN KEY (MaPhieuKham) REFERENCES dbo.PhieuKham(MaPhieuKham),
 CONSTRAINT CK_ThanhToan_PhuongThuc CHECK (PhuongThuc IN (N'Tiền mặt',N'Chuyển khoản',N'Thẻ',N'Miễn phí'))
);
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.PhieuKham') AND name=N'IX_PhieuKham_HoSo_Ngay')
 CREATE INDEX IX_PhieuKham_HoSo_Ngay ON dbo.PhieuKham(MaHoSo,NgayGioKham);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID(N'dbo.LichHenKham') AND name=N'IX_LichHen_Ngay')
 CREATE INDEX IX_LichHen_Ngay ON dbo.LichHenKham(NgayGioHen);
GO

/* DU LIEU MAU: moi ma duoc kiem tra rieng, chay lai khong them trung. */
IF NOT EXISTS (SELECT 1 FROM dbo.KhoaDieuTri WHERE MaKhoa='K001') INSERT dbo.KhoaDieuTri(MaKhoa,TenKhoa,ViTri) VALUES('K001',N'Khoa Nội tổng quát',N'Tầng 1');
IF NOT EXISTS (SELECT 1 FROM dbo.KhoaDieuTri WHERE MaKhoa='K002') INSERT dbo.KhoaDieuTri(MaKhoa,TenKhoa,ViTri) VALUES('K002',N'Khoa Ngoại',N'Tầng 2');
IF NOT EXISTS (SELECT 1 FROM dbo.KhoaDieuTri WHERE MaKhoa='K003') INSERT dbo.KhoaDieuTri(MaKhoa,TenKhoa,ViTri) VALUES('K003',N'Khoa Nhi',N'Tầng 3');

IF NOT EXISTS (SELECT 1 FROM dbo.BacSi WHERE MaBacSi='BS001') INSERT dbo.BacSi(MaBacSi,HoTen,MaKhoa,ChuyenMon) VALUES('BS001',N'Bác sĩ Nguyễn Minh Anh','K001',N'Nội tổng quát');
IF NOT EXISTS (SELECT 1 FROM dbo.BacSi WHERE MaBacSi='BS002') INSERT dbo.BacSi(MaBacSi,HoTen,MaKhoa,ChuyenMon) VALUES('BS002',N'Bác sĩ Trần Quốc Bình','K002',N'Ngoại khoa');
IF NOT EXISTS (SELECT 1 FROM dbo.BacSi WHERE MaBacSi='BS003') INSERT dbo.BacSi(MaBacSi,HoTen,MaKhoa,ChuyenMon) VALUES('BS003',N'Bác sĩ Lê Thu Hà','K003',N'Nhi khoa');

IF NOT EXISTS (SELECT 1 FROM dbo.Thuoc WHERE MaThuoc='TH001') INSERT dbo.Thuoc(MaThuoc,TenThuoc,DonViTinh) VALUES('TH001',N'Thuốc mẫu A',N'Viên');
IF NOT EXISTS (SELECT 1 FROM dbo.Thuoc WHERE MaThuoc='TH002') INSERT dbo.Thuoc(MaThuoc,TenThuoc,DonViTinh) VALUES('TH002',N'Thuốc mẫu B',N'Viên');
IF NOT EXISTS (SELECT 1 FROM dbo.Thuoc WHERE MaThuoc='TH003') INSERT dbo.Thuoc(MaThuoc,TenThuoc,DonViTinh) VALUES('TH003',N'Thuốc mẫu C',N'Gói');

IF NOT EXISTS (SELECT 1 FROM dbo.BenhNhan WHERE MaBenhNhan='BN001') INSERT dbo.BenhNhan(MaBenhNhan,HoTen,NgaySinh,GioiTinh,DiaChi,DienThoai) VALUES('BN001',N'Nguyễn Văn An','2000-05-15',N'Nam',N'TP. Hồ Chí Minh','0900000001');
IF NOT EXISTS (SELECT 1 FROM dbo.BenhNhan WHERE MaBenhNhan='BN002') INSERT dbo.BenhNhan(MaBenhNhan,HoTen,NgaySinh,GioiTinh,DiaChi,DienThoai) VALUES('BN002',N'Trần Thị Bình','1998-08-20',N'Nữ',N'TP. Hồ Chí Minh','0900000002');

IF NOT EXISTS (SELECT 1 FROM dbo.PhieuDangKy WHERE MaPhieuDK='PDK001') INSERT dbo.PhieuDangKy(MaPhieuDK,MaBenhNhan,NgayDangKy,TrangThai) VALUES('PDK001','BN001','2026-01-10',N'Đã cấp hồ sơ');
IF NOT EXISTS (SELECT 1 FROM dbo.PhieuDangKy WHERE MaPhieuDK='PDK002') INSERT dbo.PhieuDangKy(MaPhieuDK,MaBenhNhan,NgayDangKy,TrangThai) VALUES('PDK002','BN002','2026-02-15',N'Đã cấp hồ sơ');

IF NOT EXISTS (SELECT 1 FROM dbo.HoSoBenhAn WHERE MaHoSo='HS001') INSERT dbo.HoSoBenhAn(MaHoSo,MaBenhNhan,MaPhieuDK,NgayLap) VALUES('HS001','BN001','PDK001','2026-01-10');
IF NOT EXISTS (SELECT 1 FROM dbo.HoSoBenhAn WHERE MaHoSo='HS002') INSERT dbo.HoSoBenhAn(MaHoSo,MaBenhNhan,MaPhieuDK,NgayLap) VALUES('HS002','BN002','PDK002','2026-02-15');

-- Le phi 500.000 la con so gia lap, khong phai muc phi cua mot benh vien thuc te.
IF NOT EXISTS (SELECT 1 FROM dbo.LePhiHangNam WHERE MaLePhi='LP001') INSERT dbo.LePhiHangNam(MaLePhi,MaBenhNhan,NamDong,SoTien,NgayDong,TrangThai) VALUES('LP001','BN001',2026,500000,'2026-01-10',N'Đã thanh toán');
IF NOT EXISTS (SELECT 1 FROM dbo.LePhiHangNam WHERE MaLePhi='LP002') INSERT dbo.LePhiHangNam(MaLePhi,MaBenhNhan,NamDong,SoTien,NgayDong,TrangThai) VALUES('LP002','BN002',2026,500000,'2026-02-15',N'Đã thanh toán');

IF NOT EXISTS (SELECT 1 FROM dbo.PhieuKham WHERE MaPhieuKham='PK001') INSERT dbo.PhieuKham(MaPhieuKham,MaHoSo,MaBacSi,MaKhoa,NgayGioKham,LoaiKham,TrieuChung,TrangThai) VALUES('PK001','HS001','BS001','K001','2026-03-10T08:30:00',N'Khám bệnh',N'Dữ liệu mô phỏng để kiểm thử phần mềm',N'Đã hoàn tất');
IF NOT EXISTS (SELECT 1 FROM dbo.PhieuKham WHERE MaPhieuKham='PK002') INSERT dbo.PhieuKham(MaPhieuKham,MaHoSo,MaBacSi,MaKhoa,NgayGioKham,LoaiKham,TrieuChung,TrangThai) VALUES('PK002','HS002','BS001','K001','2026-04-15T09:00:00',N'Khám định kỳ',N'Dữ liệu mô phỏng kiểm tra định kỳ',N'Đã hoàn tất');

IF NOT EXISTS (SELECT 1 FROM dbo.XetNghiem WHERE MaXetNghiem='XN001') INSERT dbo.XetNghiem(MaXetNghiem,MaPhieuKham,TenXetNghiem,NgayChiDinh,TrangThai) VALUES('XN001','PK001',N'Xét nghiệm mẫu','2026-03-10T09:00:00',N'Đã chỉ định');
IF NOT EXISTS (SELECT 1 FROM dbo.ToaThuoc WHERE MaToaThuoc='TT001') INSERT dbo.ToaThuoc(MaToaThuoc,MaPhieuKham,MaBacSi,NgayLap,LoiDan) VALUES('TT001','PK001','BS001','2026-03-10T10:00:00',N'Toa thuốc mô phỏng, không sử dụng điều trị thực tế');
IF NOT EXISTS (SELECT 1 FROM dbo.LichHenKham WHERE MaLichHen='LH001') INSERT dbo.LichHenKham(MaLichHen,MaHoSo,MaKhoa,NgayGioHen,LoaiHen,NoiDung) VALUES('LH001','HS001','K001','2026-11-10T08:00:00',N'Khám định kỳ',N'Lịch hẹn giả lập');
IF NOT EXISTS (SELECT 1 FROM dbo.ThanhToanVienPhi WHERE MaThanhToan='TP001') INSERT dbo.ThanhToanVienPhi(MaThanhToan,MaPhieuKham,NgayThanhToan,SoTien,PhuongThuc) VALUES('TP001','PK001','2026-03-10T11:00:00',200000,N'Tiền mặt');
IF NOT EXISTS (SELECT 1 FROM dbo.ThanhToanVienPhi WHERE MaThanhToan='TP002') INSERT dbo.ThanhToanVienPhi(MaThanhToan,MaPhieuKham,NgayThanhToan,SoTien,PhuongThuc) VALUES('TP002','PK002','2026-04-15T10:30:00',0,N'Miễn phí');
GO

/* VIEW THONG KE - dung OUTER APPLY de tranh COUNT(CASE WHEN EXISTS(...)) bi loi SQL Server */
CREATE OR ALTER VIEW dbo.vw_ThongKePhieuKham
AS
SELECT
 pk.MaPhieuKham,pk.MaHoSo,pk.MaBacSi,pk.MaKhoa,
 k.TenKhoa,pk.NgayGioKham,pk.LoaiKham,pk.TrangThai,
 CASE WHEN xnFlag.MaPhieuKham IS NULL THEN 0 ELSE 1 END AS CoXetNghiem
FROM dbo.PhieuKham pk
INNER JOIN dbo.KhoaDieuTri k ON k.MaKhoa=pk.MaKhoa
OUTER APPLY (
 SELECT TOP (1) xn.MaPhieuKham
 FROM dbo.XetNghiem xn
 WHERE xn.MaPhieuKham=pk.MaPhieuKham
) AS xnFlag;
GO

/* KIEM TRA KET QUA */
SELECT COUNT(*) AS SoLuongBang
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_SCHEMA='dbo' AND TABLE_TYPE='BASE TABLE';
GO
SELECT TABLE_NAME AS TenBang
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_SCHEMA='dbo' AND TABLE_TYPE='BASE TABLE'
ORDER BY TABLE_NAME;
GO
SELECT COUNT(*) AS TongLuotKham,
 SUM(CASE WHEN TrangThai=N'Đang khám' THEN 1 ELSE 0 END) AS DangKham,
 SUM(CASE WHEN TrangThai=N'Đang điều trị' THEN 1 ELSE 0 END) AS DangDieuTri,
 SUM(CASE WHEN TrangThai=N'Đã hoàn tất' THEN 1 ELSE 0 END) AS HoanTat,
 SUM(CoXetNghiem) AS CoXetNghiem
FROM dbo.vw_ThongKePhieuKham
WHERE NgayGioKham >= '20260101' AND NgayGioKham < '20270101';
GO
SELECT TrangThai AS Nhom,COUNT(*) AS SoLuong,SUM(CoXetNghiem) AS CoXetNghiem
FROM dbo.vw_ThongKePhieuKham
WHERE NgayGioKham >= '20260101' AND NgayGioKham < '20270101'
GROUP BY TrangThai ORDER BY TrangThai;
GO
PRINT N'Hoan tat thiet lap QuanLyHoSoBenhAn. Khong xoa du lieu hien co.';
GO
