CREATE DATABASE eShopping;
GO
USE eShopping;
GO

-- ===== NHÓM A: dữ liệu cấu hình =====
CREATE TABLE LoaiThe (
    MaLoaiThe   VARCHAR(10)  PRIMARY KEY,          -- VISA, MASTER, DISCOVER, AMEX
    TenLoaiThe  NVARCHAR(50) NOT NULL,
    DoDaiSoThe  TINYINT      NOT NULL,             -- 16 hoặc 15
    DoDaiCSV    TINYINT      NOT NULL,             -- 3 hoặc 4
    LePhi       DECIMAL(18,0) NOT NULL DEFAULT 0 CHECK (LePhi >= 0)
);

CREATE TABLE LoaiPhieu (
    MaLoaiPhieu VARCHAR(10)  PRIMARY KEY,          -- THUONG, NHANH, NHANH_NGAY
    TenLoaiPhieu NVARCHAR(100) NOT NULL,
    NguongMienPhi DECIMAL(18,0) NULL               -- NULL = không bao giờ miễn phí
);

CREATE TABLE KhuVuc (
    MaKhuVuc  VARCHAR(10)  PRIMARY KEY,
    TenKhuVuc NVARCHAR(100) NOT NULL
);

CREATE TABLE BangPhiGiao (
    MaKhuVuc    VARCHAR(10) NOT NULL REFERENCES KhuVuc(MaKhuVuc),
    MaLoaiPhieu VARCHAR(10) NOT NULL REFERENCES LoaiPhieu(MaLoaiPhieu),
    Phi         DECIMAL(18,0) NOT NULL CHECK (Phi >= 0),
    PRIMARY KEY (MaKhuVuc, MaLoaiPhieu)
);

-- ===== NHÓM B: sản phẩm (bản sao từ HT quản lý sản phẩm) =====
CREATE TABLE NhomSanPham (
    MaNhom  VARCHAR(10)  PRIMARY KEY,
    TenNhom NVARCHAR(100) NOT NULL
);

CREATE TABLE SanPham (
    MaSP        VARCHAR(20)  PRIMARY KEY,          -- BR01: duy nhất
    TenSP       NVARCHAR(200) NOT NULL,
    NhaSX       NVARCHAR(100) NULL,
    MoTa        NVARCHAR(MAX) NULL,
    ThongSoKT   NVARCHAR(MAX) NULL,
    GiaHienHanh DECIMAL(18,0) NOT NULL CHECK (GiaHienHanh >= 0),
    ConHang     BIT NOT NULL DEFAULT 1,
    MaNhom      VARCHAR(10)  NOT NULL REFERENCES NhomSanPham(MaNhom)  -- BR01: đúng 1 nhóm
);

CREATE TABLE HinhAnh (
    MaHinh INT IDENTITY PRIMARY KEY,
    MaSP   VARCHAR(20) NOT NULL REFERENCES SanPham(MaSP),
    Url    NVARCHAR(500) NOT NULL,
    ThuTu  INT NOT NULL DEFAULT 1
);

-- ===== NHÓM C: khách hàng và đơn hàng =====
CREATE TABLE KhachHang (
    MaKH        INT IDENTITY PRIMARY KEY,
    HoTen       NVARCHAR(100) NOT NULL,
    NgaySinh    DATE NOT NULL,
    CMND_Passport VARCHAR(20) NOT NULL UNIQUE,
    DiaChi      NVARCHAR(200) NOT NULL,
    DienThoai   VARCHAR(15)  NOT NULL,
    TenDangNhap VARCHAR(50)  NOT NULL UNIQUE,
    MatKhauHash VARCHAR(200) NOT NULL,             -- không lưu mật khẩu thô
    Email       VARCHAR(100) NULL                  -- NULL = không gửi email (BR12)
);

CREATE TABLE NguoiNhan (                           -- BR08: tách khỏi KhachHang
    MaNguoiNhan INT IDENTITY PRIMARY KEY,
    HoTen       NVARCHAR(100) NOT NULL,
    DiaChi      NVARCHAR(200) NOT NULL,
    DienThoai   VARCHAR(15)  NOT NULL,
    MaKhuVuc    VARCHAR(10)  NOT NULL REFERENCES KhuVuc(MaKhuVuc)
);

CREATE TABLE TheTinDung (                          -- KHÔNG lưu số thẻ đầy đủ và CSV
    MaThe       INT IDENTITY PRIMARY KEY,
    MaLoaiThe   VARCHAR(10) NOT NULL REFERENCES LoaiThe(MaLoaiThe),
    BonSoCuoi   CHAR(4)     NOT NULL,
    NgayHetHan  CHAR(5)     NOT NULL,              -- MM/yy
    TenChuThe   NVARCHAR(100) NOT NULL
);

CREATE TABLE GiaoDichThanhToan (
    MaGiaoDich  VARCHAR(50) PRIMARY KEY,           -- mã do DV thanh toán trả về
    MaThe       INT NOT NULL REFERENCES TheTinDung(MaThe),
    KetQua      VARCHAR(20) NOT NULL,
    ThoiDiem    DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE DonDatHang (
    MaDon       INT IDENTITY PRIMARY KEY,
    MaKH        INT NOT NULL REFERENCES KhachHang(MaKH),
    MaNguoiNhan INT NOT NULL REFERENCES NguoiNhan(MaNguoiNhan),
    MaLoaiPhieu VARCHAR(10) NOT NULL REFERENCES LoaiPhieu(MaLoaiPhieu),
    MaGiaoDich  VARCHAR(50) NOT NULL UNIQUE REFERENCES GiaoDichThanhToan(MaGiaoDich), -- BR11
    ThoiDiemDat DATETIME NOT NULL DEFAULT GETDATE(),
    TienHang    DECIMAL(18,0) NOT NULL,
    PhiGiao     DECIMAL(18,0) NOT NULL,
    LePhiThe    DECIMAL(18,0) NOT NULL,
    TongHoaDon  DECIMAL(18,0) NOT NULL,
    TrangThai   NVARCHAR(30) NOT NULL DEFAULT N'ĐãGhiNhận',
    CONSTRAINT CK_Tong CHECK (TongHoaDon = TienHang + PhiGiao + LePhiThe)
);

CREATE TABLE ChiTietDonHang (
    MaDon   INT NOT NULL REFERENCES DonDatHang(MaDon),
    MaSP    VARCHAR(20) NOT NULL REFERENCES SanPham(MaSP),
    SoLuong INT NOT NULL CHECK (SoLuong > 0),
    DonGia  DECIMAL(18,0) NOT NULL,                -- BR02: giá chốt tại thời điểm đặt
    PRIMARY KEY (MaDon, MaSP)
);