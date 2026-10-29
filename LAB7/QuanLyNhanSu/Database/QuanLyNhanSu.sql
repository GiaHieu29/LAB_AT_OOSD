/* Bài 7 - Quản lý nhân sự - công ty sơn Đông Á
   Chạy toàn bộ file này trong SSMS (hoặc để chương trình tự chạy ở lần mở đầu tiên). */
IF DB_ID(N'QuanLyNhanSu') IS NULL CREATE DATABASE QuanLyNhanSu;
GO
USE QuanLyNhanSu;
GO

IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_PB_TP') ALTER TABLE PhongBan DROP CONSTRAINT FK_PB_TP;
IF OBJECT_ID('PhieuThuNopPhat', 'U') IS NOT NULL DROP TABLE PhieuThuNopPhat;
IF OBJECT_ID('QuyetDinhKyLuat', 'U') IS NOT NULL DROP TABLE QuyetDinhKyLuat;
IF OBJECT_ID('PhieuChiKhenThuong', 'U') IS NOT NULL DROP TABLE PhieuChiKhenThuong;
IF OBJECT_ID('QuyetDinhKhenThuong', 'U') IS NOT NULL DROP TABLE QuyetDinhKhenThuong;
IF OBJECT_ID('ChiTietLuong', 'U') IS NOT NULL DROP TABLE ChiTietLuong;
IF OBJECT_ID('BangLuong', 'U') IS NOT NULL DROP TABLE BangLuong;
IF OBJECT_ID('ChiTietPhuTroi', 'U') IS NOT NULL DROP TABLE ChiTietPhuTroi;
IF OBJECT_ID('PhieuPhuTroi', 'U') IS NOT NULL DROP TABLE PhieuPhuTroi;
IF OBJECT_ID('ChamCong', 'U') IS NOT NULL DROP TABLE ChamCong;
IF OBJECT_ID('ChiTietPhanCong', 'U') IS NOT NULL DROP TABLE ChiTietPhanCong;
IF OBJECT_ID('KeHoachTuan', 'U') IS NOT NULL DROP TABLE KeHoachTuan;
IF OBJECT_ID('QuyetDinhLuong', 'U') IS NOT NULL DROP TABLE QuyetDinhLuong;
IF OBJECT_ID('QuyetDinhChucVu', 'U') IS NOT NULL DROP TABLE QuyetDinhChucVu;
IF OBJECT_ID('BaoHiem', 'U') IS NOT NULL DROP TABLE BaoHiem;
IF OBJECT_ID('HopDong', 'U') IS NOT NULL DROP TABLE HopDong;
IF OBJECT_ID('QuyetDinhTuyenDung', 'U') IS NOT NULL DROP TABLE QuyetDinhTuyenDung;
IF OBJECT_ID('NhanVien', 'U') IS NOT NULL DROP TABLE NhanVien;
IF OBJECT_ID('CaLamViec', 'U') IS NOT NULL DROP TABLE CaLamViec;
IF OBJECT_ID('ChucVu', 'U') IS NOT NULL DROP TABLE ChucVu;
IF OBJECT_ID('PhongBan', 'U') IS NOT NULL DROP TABLE PhongBan;
GO

CREATE TABLE PhongBan (
    MaPB          varchar(10)   NOT NULL PRIMARY KEY,
    TenPB         nvarchar(100) NOT NULL UNIQUE,
    MoTa          nvarchar(200) NULL,
    NhuCauTuyen   int           NOT NULL DEFAULT 0 CHECK (NhuCauTuyen >= 0),
    MaTruongPhong varchar(10)   NULL
);

CREATE TABLE ChucVu (
    MaCV          varchar(10)    NOT NULL PRIMARY KEY,
    TenCV         nvarchar(100)  NOT NULL UNIQUE,
    PhuCapMacDinh decimal(18, 0) NOT NULL DEFAULT 0 CHECK (PhuCapMacDinh >= 0)
);

CREATE TABLE CaLamViec (
    MaCa       varchar(10)  NOT NULL PRIMARY KEY,
    TenCa      nvarchar(50) NOT NULL,
    GioBatDau  time(0)      NOT NULL,
    GioKetThuc time(0)      NOT NULL,
    CONSTRAINT CK_Ca_Gio CHECK (GioBatDau <> GioKetThuc)
);

CREATE TABLE NhanVien (
    MaNV         varchar(10)   NOT NULL PRIMARY KEY,
    HoTen        nvarchar(100) NOT NULL,
    NgaySinh     date          NOT NULL,
    GioiTinh     nvarchar(3)   NOT NULL,
    DiaChi       nvarchar(200) NOT NULL,
    DienThoai    varchar(15)   NOT NULL,
    CCCD         varchar(12)   NOT NULL UNIQUE,
    MaPB         varchar(10)   NOT NULL REFERENCES PhongBan(MaPB),
    TrangThai    varchar(10)   NOT NULL,
    NgayChinhThuc date         NULL,
    NgayNghi     date          NULL,
    LyDoNghi     nvarchar(200) NULL,
    CONSTRAINT CK_NV_GT CHECK (GioiTinh IN (N'Nam', N'Nữ')),
    CONSTRAINT CK_NV_TT CHECK (TrangThai IN ('THUVIEC', 'CHINHTHUC', 'NGHIVIEC'))
);
ALTER TABLE PhongBan ADD CONSTRAINT FK_PB_TP FOREIGN KEY (MaTruongPhong) REFERENCES NhanVien(MaNV);

-- Quyết định tuyển dụng kèm thử việc: mỗi nhân viên có một quyết định
CREATE TABLE QuyetDinhTuyenDung (
    SoQD               varchar(20)    NOT NULL PRIMARY KEY,
    MaNV               varchar(10)    NOT NULL,
    MaPB               varchar(10)    NOT NULL,
    NgayQD             date           NOT NULL,
    NgayBatDau         date           NOT NULL,
    SoThang            tinyint        NOT NULL,
    NgayKetThuc        date           NOT NULL,
    LuongThuViec       decimal(18, 0) NOT NULL,
    DieuKhoanCongTy    nvarchar(500)  NOT NULL,
    DieuKhoanNhanVien  nvarchar(500)  NOT NULL,
    NguoiKy            nvarchar(100)  NOT NULL,
    CONSTRAINT UQ_QDTD_NV UNIQUE (MaNV),
    CONSTRAINT FK_QDTD_NV FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),
    CONSTRAINT FK_QDTD_PB FOREIGN KEY (MaPB) REFERENCES PhongBan(MaPB),
    CONSTRAINT CK_QDTD_Thang CHECK (SoThang BETWEEN 1 AND 6),
    CONSTRAINT CK_QDTD_Luong CHECK (LuongThuViec > 0),
    CONSTRAINT CK_QDTD_Han CHECK (NgayKetThuc > NgayBatDau)
);

-- Hợp đồng: dài hạn (không ngày kết thúc) hoặc ngắn hạn (có ngày kết thúc)
CREATE TABLE HopDong (
    SoHD        varchar(20)   NOT NULL PRIMARY KEY,
    MaNV        varchar(10)   NOT NULL REFERENCES NhanVien(MaNV),
    LoaiHD      varchar(10)   NOT NULL,
    NgayKy      date          NOT NULL,
    NgayBatDau  date          NOT NULL,
    NgayKetThuc date          NULL,
    GhiChu      nvarchar(300) NULL,
    CONSTRAINT CK_HD_Loai CHECK (LoaiHD IN ('DAIHAN', 'NGANHAN')),
    CONSTRAINT CK_HD_Han CHECK ((LoaiHD = 'NGANHAN' AND NgayKetThuc IS NOT NULL AND NgayKetThuc > NgayBatDau)
                             OR (LoaiHD = 'DAIHAN' AND NgayKetThuc IS NULL)),
    CONSTRAINT CK_HD_Ky CHECK (NgayBatDau >= NgayKy)
);

-- Hồ sơ BHYT, BHXH của nhân viên hợp đồng dài hạn; MucDong là % lương cơ bản người lao động đóng
CREATE TABLE BaoHiem (
    MaBH        int IDENTITY(1, 1) PRIMARY KEY,
    MaNV        varchar(10)  NOT NULL REFERENCES NhanVien(MaNV),
    LoaiBH      varchar(4)   NOT NULL,
    SoSo        varchar(20)  NOT NULL,
    NgayThamGia date         NOT NULL,
    MucDong     decimal(4, 1) NOT NULL,
    CONSTRAINT UQ_BH UNIQUE (MaNV, LoaiBH),
    CONSTRAINT CK_BH_Loai CHECK (LoaiBH IN ('BHYT', 'BHXH')),
    CONSTRAINT CK_BH_Muc CHECK (MucDong > 0 AND MucDong <= 20)
);

CREATE TABLE QuyetDinhChucVu (
    SoQD          varchar(20)    NOT NULL PRIMARY KEY,
    MaNV          varchar(10)    NOT NULL REFERENCES NhanVien(MaNV),
    MaCV          varchar(10)    NOT NULL REFERENCES ChucVu(MaCV),
    NgayHieuLuc   date           NOT NULL,
    PhuCapChucVu  decimal(18, 0) NOT NULL DEFAULT 0 CHECK (PhuCapChucVu >= 0),
    GhiChu        nvarchar(200)  NULL,
    CONSTRAINT UQ_QDCV UNIQUE (MaNV, NgayHieuLuc)
);

CREATE TABLE QuyetDinhLuong (
    SoQD          varchar(20)    NOT NULL PRIMARY KEY,
    MaNV          varchar(10)    NOT NULL REFERENCES NhanVien(MaNV),
    NgayHieuLuc   date           NOT NULL,
    LuongCoBan    decimal(18, 0) NOT NULL CHECK (LuongCoBan > 0),
    PhuCapLuong   decimal(18, 0) NOT NULL DEFAULT 0 CHECK (PhuCapLuong >= 0),
    PhuCapDocHai  decimal(18, 0) NOT NULL DEFAULT 0 CHECK (PhuCapDocHai >= 0),
    LyDo          nvarchar(200)  NULL,
    CONSTRAINT UQ_QDL UNIQUE (MaNV, NgayHieuLuc)
);

-- Kế hoạch phân công theo tuần do trưởng phòng lập và chuyển ban chấm công
CREATE TABLE KeHoachTuan (
    MaKH       int IDENTITY(1, 1) PRIMARY KEY,
    MaPB       varchar(10) NOT NULL REFERENCES PhongBan(MaPB),
    TuNgay     date        NOT NULL,
    DenNgay    date        NOT NULL,
    NguoiLap   varchar(10) NOT NULL REFERENCES NhanVien(MaNV),
    NgayLap    datetime2   NOT NULL,
    CONSTRAINT UQ_KH UNIQUE (MaPB, TuNgay),
    CONSTRAINT CK_KH_Tuan CHECK (DenNgay = DATEADD(DAY, 6, TuNgay))
);

CREATE TABLE ChiTietPhanCong (
    MaKH         int           NOT NULL REFERENCES KeHoachTuan(MaKH),
    MaNV         varchar(10)   NOT NULL REFERENCES NhanVien(MaNV),
    Ngay         date          NOT NULL,
    MaCa         varchar(10)   NOT NULL REFERENCES CaLamViec(MaCa),
    GioPhuTroiKH decimal(3, 1) NOT NULL DEFAULT 0 CHECK (GioPhuTroiKH BETWEEN 0 AND 4),
    PRIMARY KEY (MaKH, MaNV, Ngay)
);

CREATE TABLE ChamCong (
    MaCC       int IDENTITY(1, 1) PRIMARY KEY,
    MaNV       varchar(10)   NOT NULL REFERENCES NhanVien(MaNV),
    Ngay       date          NOT NULL,
    TrangThai  varchar(15)   NOT NULL,
    SoGioLam   decimal(3, 1) NOT NULL,
    MaCa       varchar(10)   NULL REFERENCES CaLamViec(MaCa),
    GhiChu     nvarchar(200) NULL,
    CONSTRAINT UQ_CC UNIQUE (MaNV, Ngay),
    CONSTRAINT CK_CC_TT CHECK (TrangThai IN ('DILAM', 'NGHIPHEP', 'NGHIKHONGPHEP')),
    CONSTRAINT CK_CC_Gio CHECK ((TrangThai = 'DILAM' AND SoGioLam > 0 AND SoGioLam <= 12) OR (TrangThai <> 'DILAM' AND SoGioLam = 0))
);

CREATE TABLE PhieuPhuTroi (
    SoPhieu    varchar(20)   NOT NULL PRIMARY KEY,
    NgayPhuTroi date         NOT NULL,
    SoGio      decimal(3, 1) NOT NULL CHECK (SoGio > 0 AND SoGio <= 12),
    HinhThuc   varchar(10)   NOT NULL CHECK (HinhThuc IN ('TANGCA', 'NGAYLE', 'NGAYNGHI')),
    NguoiLap   nvarchar(100) NOT NULL
);

CREATE TABLE ChiTietPhuTroi (
    SoPhieu varchar(20)   NOT NULL REFERENCES PhieuPhuTroi(SoPhieu),
    MaNV    varchar(10)   NOT NULL REFERENCES NhanVien(MaNV),
    SoGio   decimal(3, 1) NOT NULL CHECK (SoGio > 0 AND SoGio <= 12),
    PRIMARY KEY (SoPhieu, MaNV)
);

CREATE TABLE BangLuong (
    MaBL     int IDENTITY(1, 1) PRIMARY KEY,
    Thang    tinyint      NOT NULL CHECK (Thang BETWEEN 1 AND 12),
    Nam      smallint     NOT NULL,
    NgayLap  datetime2    NOT NULL,
    NguoiLap nvarchar(100) NOT NULL,
    CONSTRAINT UQ_BL UNIQUE (Thang, Nam)
);

CREATE TABLE ChiTietLuong (
    MaBL           int            NOT NULL REFERENCES BangLuong(MaBL),
    MaNV           varchar(10)    NOT NULL REFERENCES NhanVien(MaNV),
    LuongCoBan     decimal(18, 0) NOT NULL,
    PhuCapLuong    decimal(18, 0) NOT NULL,
    PhuCapDocHai   decimal(18, 0) NOT NULL,
    PhuCapChucVu   decimal(18, 0) NOT NULL,
    NgayCong       decimal(4, 1)  NOT NULL,
    LuongNgayCong  decimal(18, 0) NOT NULL,
    GioPhuTroi     decimal(5, 1)  NOT NULL,
    TienPhuTroi    decimal(18, 0) NOT NULL,
    TongThuNhap    decimal(18, 0) NOT NULL,
    TruBHXH        decimal(18, 0) NOT NULL,
    TruBHYT        decimal(18, 0) NOT NULL,
    TruPhat        decimal(18, 0) NOT NULL,
    ThucLanh       decimal(18, 0) NOT NULL,
    PRIMARY KEY (MaBL, MaNV)
);

CREATE TABLE QuyetDinhKhenThuong (
    SoQD         varchar(20)    NOT NULL PRIMARY KEY,
    NgayQD       date           NOT NULL,
    DoiTuong     varchar(6)     NOT NULL,
    MaNV         varchar(10)    NULL REFERENCES NhanVien(MaNV),
    MaPB         varchar(10)    NULL REFERENCES PhongBan(MaPB),
    LoaiKT       varchar(10)    NOT NULL,
    LyDo         nvarchar(300)  NOT NULL,
    SoTienMoiNguoi decimal(18, 0) NOT NULL CHECK (SoTienMoiNguoi > 0),
    NguoiKy      varchar(10)    NOT NULL REFERENCES NhanVien(MaNV),
    CONSTRAINT CK_KT_DT CHECK ((DoiTuong = 'CANHAN' AND MaNV IS NOT NULL AND MaPB IS NULL)
                            OR (DoiTuong = 'TAPTHE' AND MaPB IS NOT NULL AND MaNV IS NULL)),
    CONSTRAINT CK_KT_Loai CHECK (LoaiKT IN ('THANHTICH', 'NGAYLE', 'CUOINAM', 'LUONG13'))
);

CREATE TABLE PhieuChiKhenThuong (
    SoPhieu    int IDENTITY(1, 1) PRIMARY KEY,
    SoQD       varchar(20)    NOT NULL REFERENCES QuyetDinhKhenThuong(SoQD),
    MaNV       varchar(10)    NOT NULL REFERENCES NhanVien(MaNV),
    SoTien     decimal(18, 0) NOT NULL CHECK (SoTien > 0),
    TrangThai  varchar(8)     NOT NULL CHECK (TrangThai IN ('CHUACHI', 'DACHI')),
    NgayChi    date           NULL,
    HinhThucChi varchar(12)   NULL CHECK (HinhThucChi IN ('TIENMAT', 'CHUYENKHOAN')),
    CONSTRAINT UQ_PCKT UNIQUE (SoQD, MaNV)
);

CREATE TABLE QuyetDinhKyLuat (
    SoQD       varchar(20)    NOT NULL PRIMARY KEY,
    NgayQD     date           NOT NULL,
    DoiTuong   varchar(6)     NOT NULL,
    MaNV       varchar(10)    NULL REFERENCES NhanVien(MaNV),
    MaPB       varchar(10)    NULL REFERENCES PhongBan(MaPB),
    HinhThuc   varchar(10)    NOT NULL,
    LyDo       nvarchar(300)  NOT NULL,
    SoTienPhat decimal(18, 0) NOT NULL DEFAULT 0 CHECK (SoTienPhat >= 0),
    CachThu    varchar(8)     NULL,
    NguoiKy    varchar(10)    NOT NULL REFERENCES NhanVien(MaNV),
    CONSTRAINT CK_KL_DT CHECK ((DoiTuong = 'CANHAN' AND MaNV IS NOT NULL AND MaPB IS NULL)
                            OR (DoiTuong = 'TAPTHE' AND MaPB IS NOT NULL AND MaNV IS NULL)),
    CONSTRAINT CK_KL_HT CHECK (HinhThuc IN ('KHIENTRACH', 'CANHCAO', 'PHATTIEN', 'SATHAI')),
    CONSTRAINT CK_KL_Cach CHECK ((SoTienPhat = 0 AND CachThu IS NULL) OR (SoTienPhat > 0 AND CachThu IN ('NOPPHAT', 'TRULUONG')))
);

CREATE TABLE PhieuThuNopPhat (
    SoPhieu    int IDENTITY(1, 1) PRIMARY KEY,
    SoQD       varchar(20)    NOT NULL REFERENCES QuyetDinhKyLuat(SoQD),
    MaNV       varchar(10)    NOT NULL REFERENCES NhanVien(MaNV),
    SoTien     decimal(18, 0) NOT NULL CHECK (SoTien > 0),
    CachThu    varchar(8)     NOT NULL CHECK (CachThu IN ('NOPPHAT', 'TRULUONG')),
    TrangThai  varchar(8)     NOT NULL CHECK (TrangThai IN ('CHUATHU', 'DATHU')),
    NgayThu    date           NULL,
    ThangTru   tinyint        NULL,
    NamTru     smallint       NULL,
    CONSTRAINT UQ_PTNP UNIQUE (SoQD, MaNV)
);
GO

/* ===================== DỮ LIỆU MẪU ===================== */
INSERT INTO PhongBan(MaPB, TenPB, MoTa, NhuCauTuyen) VALUES
 ('NS',   N'Hành chính nhân sự', N'Quản lý nhân sự, hành chính', 0),
 ('TCKT', N'Tài chính kế toán',  N'Kế toán, thu chi, lương',     0),
 ('KT',   N'Kỹ thuật',           N'Nghiên cứu công thức sơn',    2),
 ('SX',   N'Sản xuất',           N'Phân xưởng sản xuất sơn',     3),
 ('BCC',  N'Ban chấm công',      N'Chấm công, theo dõi phụ trội',0);

INSERT INTO ChucVu(MaCV, TenCV, PhuCapMacDinh) VALUES
 ('GD', N'Giám đốc', 5000000), ('TP', N'Trưởng phòng', 2000000), ('PP', N'Phó phòng', 1000000),
 ('TT', N'Tổ trưởng', 500000), ('NV', N'Nhân viên', 0);

INSERT INTO CaLamViec(MaCa, TenCa, GioBatDau, GioKetThuc) VALUES
 ('CA1', N'Ca sáng',  '07:00', '15:00'), ('CA2', N'Ca chiều', '15:00', '23:00'),
 ('CA3', N'Ca đêm',   '23:00', '07:00'), ('HC',  N'Hành chính','08:00', '17:00');

INSERT INTO NhanVien(MaNV, HoTen, NgaySinh, GioiTinh, DiaChi, DienThoai, CCCD, MaPB, TrangThai, NgayChinhThuc, NgayNghi, LyDoNghi) VALUES
 ('NV001', N'Nguyễn Hoàng Long', '1975-03-12', N'Nam', N'12 Lê Lợi, Quận 1, TP.HCM',        '0901000001', '079075000001', 'NS',   'CHINHTHUC', '2019-04-01', NULL, NULL),
 ('NV002', N'Trần Thị Mai',      '1982-07-21', N'Nữ',  N'45 Nguyễn Trãi, Quận 5, TP.HCM',   '0901000002', '079082000002', 'TCKT', 'CHINHTHUC', '2020-02-01', NULL, NULL),
 ('NV003', N'Lê Văn Hùng',       '1980-11-05', N'Nam', N'78 Hai Bà Trưng, Quận 3, TP.HCM',  '0901000003', '079080000003', 'KT',   'CHINHTHUC', '2020-06-01', NULL, NULL),
 ('NV004', N'Phạm Quốc Bảo',     '1978-01-30', N'Nam', N'9 Cách Mạng Tháng 8, Quận 10',     '0901000004', '079078000004', 'SX',   'CHINHTHUC', '2021-01-01', NULL, NULL),
 ('NV005', N'Nguyễn Thị Hạnh',   '1988-09-09', N'Nữ',  N'23 Võ Văn Tần, Quận 3, TP.HCM',    '0901000005', '079088000005', 'BCC',  'CHINHTHUC', '2023-03-01', NULL, NULL),
 ('NV006', N'Đỗ Minh Khoa',      '1995-04-18', N'Nam', N'101 Lý Thường Kiệt, Quận 10',      '0901000006', '079095000006', 'KT',   'CHINHTHUC', '2024-03-01', NULL, NULL),
 ('NV007', N'Võ Thị Lan',        '1998-12-02', N'Nữ',  N'56 Phan Đăng Lưu, Bình Thạnh',     '0901000007', '079098000007', 'SX',   'CHINHTHUC', '2026-01-01', NULL, NULL),
 ('NV008', N'Bùi Anh Tuấn',      '1992-06-25', N'Nam', N'88 Quang Trung, Gò Vấp, TP.HCM',   '0901000008', '079092000008', 'SX',   'CHINHTHUC', '2025-06-01', NULL, NULL),
 ('NV009', N'Hoàng Thị Yến',     '2000-02-14', N'Nữ',  N'33 Nguyễn Huệ, Quận 1, TP.HCM',    '0901000009', '079100000009', 'TCKT', 'THUVIEC',   NULL,         NULL, NULL),
 ('NV010', N'Phan Văn Nam',      '1999-08-08', N'Nam', N'17 Điện Biên Phủ, Bình Thạnh',     '0901000010', '079099000010', 'KT',   'THUVIEC',   NULL,         NULL, NULL),
 ('NV011', N'Lý Thanh Sơn',      '1990-05-15', N'Nam', N'5 Trường Chinh, Tân Bình',         '0901000011', '079090000011', 'SX',   'NGHIVIEC',  '2025-07-01', '2026-06-30', N'Hết hạn hợp đồng, không gia hạn');

UPDATE PhongBan SET MaTruongPhong = 'NV001' WHERE MaPB = 'NS';
UPDATE PhongBan SET MaTruongPhong = 'NV002' WHERE MaPB = 'TCKT';
UPDATE PhongBan SET MaTruongPhong = 'NV003' WHERE MaPB = 'KT';
UPDATE PhongBan SET MaTruongPhong = 'NV004' WHERE MaPB = 'SX';
UPDATE PhongBan SET MaTruongPhong = 'NV005' WHERE MaPB = 'BCC';

INSERT INTO QuyetDinhTuyenDung(SoQD, MaNV, MaPB, NgayQD, NgayBatDau, SoThang, NgayKetThuc, LuongThuViec, DieuKhoanCongTy, DieuKhoanNhanVien, NguoiKy) VALUES
 ('TD001', 'NV001', 'NS',   '2019-01-25', '2019-02-01', 2, '2019-03-31', 17000000, N'Công ty trả lương thử việc đúng hạn, bố trí công việc phù hợp.', N'Nhân viên chấp hành nội quy, hoàn thành công việc được giao.', N'Hội đồng quản trị'),
 ('TD002', 'NV002', 'TCKT', '2019-11-25', '2019-12-01', 2, '2020-01-31', 12000000, N'Công ty trả lương thử việc đúng hạn, bố trí công việc phù hợp.', N'Nhân viên chấp hành nội quy, hoàn thành công việc được giao.', N'Nguyễn Hoàng Long'),
 ('TD003', 'NV003', 'KT',   '2020-03-25', '2020-04-01', 2, '2020-05-31', 12000000, N'Công ty trả lương thử việc đúng hạn, bố trí công việc phù hợp.', N'Nhân viên chấp hành nội quy, hoàn thành công việc được giao.', N'Nguyễn Hoàng Long'),
 ('TD004', 'NV004', 'SX',   '2020-10-25', '2020-11-01', 2, '2020-12-31', 11000000, N'Công ty trả lương thử việc đúng hạn, bố trí công việc phù hợp.', N'Nhân viên chấp hành nội quy, hoàn thành công việc được giao.', N'Nguyễn Hoàng Long'),
 ('TD005', 'NV005', 'BCC',  '2023-01-20', '2023-01-01', 2, '2023-02-28',  8000000, N'Công ty trả lương thử việc đúng hạn, bố trí công việc phù hợp.', N'Nhân viên chấp hành nội quy, hoàn thành công việc được giao.', N'Nguyễn Hoàng Long'),
 ('TD006', 'NV006', 'KT',   '2024-01-02', '2024-01-01', 2, '2024-02-29',  7500000, N'Công ty trả lương thử việc đúng hạn, bố trí công việc phù hợp.', N'Nhân viên chấp hành nội quy, hoàn thành công việc được giao.', N'Lê Văn Hùng'),
 ('TD007', 'NV007', 'SX',   '2025-10-25', '2025-11-01', 2, '2025-12-31',  5500000, N'Công ty trả lương thử việc đúng hạn, bố trí công việc phù hợp.', N'Nhân viên chấp hành nội quy, hoàn thành công việc được giao.', N'Phạm Quốc Bảo'),
 ('TD008', 'NV008', 'SX',   '2025-03-25', '2025-04-01', 2, '2025-05-31',  6000000, N'Công ty trả lương thử việc đúng hạn, bố trí công việc phù hợp.', N'Nhân viên chấp hành nội quy, hoàn thành công việc được giao.', N'Phạm Quốc Bảo'),
 ('TD009', 'NV009', 'TCKT', '2026-07-25', '2026-08-01', 2, '2026-09-30',  7000000, N'Công ty trả lương thử việc đúng hạn, bố trí công việc phù hợp.', N'Nhân viên chấp hành nội quy, hoàn thành công việc được giao.', N'Trần Thị Mai'),
 ('TD010', 'NV010', 'KT',   '2026-09-25', '2026-10-01', 2, '2026-11-30',  8000000, N'Công ty trả lương thử việc đúng hạn, bố trí công việc phù hợp.', N'Nhân viên chấp hành nội quy, hoàn thành công việc được giao.', N'Lê Văn Hùng'),
 ('TD011', 'NV011', 'SX',   '2025-05-25', '2025-06-01', 1, '2025-06-30',  5500000, N'Công ty trả lương thử việc đúng hạn, bố trí công việc phù hợp.', N'Nhân viên chấp hành nội quy, hoàn thành công việc được giao.', N'Phạm Quốc Bảo');

INSERT INTO HopDong(SoHD, MaNV, LoaiHD, NgayKy, NgayBatDau, NgayKetThuc, GhiChu) VALUES
 ('HD001', 'NV001', 'DAIHAN',  '2019-04-01', '2019-04-01', NULL, N'Hợp đồng dài hạn'),
 ('HD002', 'NV002', 'DAIHAN',  '2020-02-01', '2020-02-01', NULL, N'Hợp đồng dài hạn'),
 ('HD003', 'NV003', 'DAIHAN',  '2020-06-01', '2020-06-01', NULL, N'Hợp đồng dài hạn'),
 ('HD004', 'NV004', 'DAIHAN',  '2021-01-01', '2021-01-01', NULL, N'Hợp đồng dài hạn'),
 ('HD005', 'NV005', 'DAIHAN',  '2023-03-01', '2023-03-01', NULL, N'Hợp đồng dài hạn'),
 ('HD006', 'NV006', 'DAIHAN',  '2024-03-01', '2024-03-01', NULL, N'Hợp đồng dài hạn'),
 ('HD007', 'NV007', 'NGANHAN', '2026-01-01', '2026-01-01', '2026-12-31', N'Hợp đồng ngắn hạn 12 tháng'),
 ('HD008', 'NV008', 'DAIHAN',  '2025-06-01', '2025-06-01', NULL, N'Hợp đồng dài hạn'),
 ('HD009', 'NV011', 'NGANHAN', '2025-07-01', '2025-07-01', '2026-06-30', N'Hợp đồng ngắn hạn, đã hết hạn');

-- Hợp đồng dài hạn: làm hồ sơ BHXH (8%) và BHYT (1,5%) trích trừ trong bảng lương
INSERT INTO BaoHiem(MaNV, LoaiBH, SoSo, NgayThamGia, MucDong)
SELECT MaNV, 'BHXH', 'XH' + MaNV, NgayBatDau, 8 FROM HopDong WHERE LoaiHD = 'DAIHAN';
INSERT INTO BaoHiem(MaNV, LoaiBH, SoSo, NgayThamGia, MucDong)
SELECT MaNV, 'BHYT', 'YT' + MaNV, NgayBatDau, 1.5 FROM HopDong WHERE LoaiHD = 'DAIHAN';

INSERT INTO QuyetDinhChucVu(SoQD, MaNV, MaCV, NgayHieuLuc, PhuCapChucVu, GhiChu) VALUES
 ('CV001', 'NV001', 'GD', '2019-04-01', 5000000, N'Bổ nhiệm Giám đốc'),
 ('CV002', 'NV002', 'TP', '2020-02-01', 2000000, N'Bổ nhiệm Trưởng phòng'),
 ('CV003', 'NV003', 'TP', '2020-06-01', 2000000, N'Bổ nhiệm Trưởng phòng'),
 ('CV004', 'NV004', 'TP', '2021-01-01', 2000000, N'Bổ nhiệm Trưởng phòng'),
 ('CV005', 'NV005', 'TP', '2023-03-01', 2000000, N'Bổ nhiệm Trưởng ban'),
 ('CV006', 'NV006', 'NV', '2024-03-01', 0, NULL),
 ('CV007', 'NV007', 'NV', '2026-01-01', 0, NULL),
 ('CV008', 'NV008', 'NV', '2025-06-01', 0, NULL);

INSERT INTO QuyetDinhLuong(SoQD, MaNV, NgayHieuLuc, LuongCoBan, PhuCapLuong, PhuCapDocHai, LyDo) VALUES
 ('L001', 'NV001', '2019-04-01', 25000000, 0, 0, N'Lương theo hợp đồng'),
 ('L002', 'NV002', '2020-02-01', 18000000, 0, 0, N'Lương theo hợp đồng'),
 ('L003', 'NV003', '2020-06-01', 18000000, 0, 0, N'Lương theo hợp đồng'),
 ('L004', 'NV004', '2021-01-01', 16000000, 0, 1000000, N'Lương theo hợp đồng, phụ cấp độc hại phân xưởng'),
 ('L005', 'NV005', '2023-03-01', 12000000, 0, 0, N'Lương theo hợp đồng'),
 ('L006', 'NV006', '2024-03-01', 11000000, 0, 0, N'Lương theo hợp đồng'),
 ('L007', 'NV006', '2026-01-01', 12500000, 500000, 0, N'Tăng lương định kỳ'),
 ('L008', 'NV007', '2026-01-01',  8000000, 0, 800000, N'Lương theo hợp đồng, phụ cấp độc hại'),
 ('L009', 'NV008', '2025-06-01',  9000000, 0, 800000, N'Lương theo hợp đồng, phụ cấp độc hại');

-- Kế hoạch phân công tuần 19/10 - 25/10/2026
INSERT INTO KeHoachTuan(MaPB, TuNgay, DenNgay, NguoiLap, NgayLap) VALUES
 ('KT', '2026-10-19', '2026-10-25', 'NV003', '2026-10-16 16:30'),
 ('SX', '2026-10-19', '2026-10-25', 'NV004', '2026-10-16 17:00');
INSERT INTO ChiTietPhanCong(MaKH, MaNV, Ngay, MaCa, GioPhuTroiKH) VALUES
 (1, 'NV006', '2026-10-19', 'HC', 0), (1, 'NV006', '2026-10-20', 'HC', 0), (1, 'NV006', '2026-10-21', 'HC', 0), (1, 'NV006', '2026-10-22', 'HC', 2), (1, 'NV006', '2026-10-23', 'HC', 0),
 (1, 'NV010', '2026-10-19', 'HC', 0), (1, 'NV010', '2026-10-20', 'HC', 0), (1, 'NV010', '2026-10-21', 'HC', 0), (1, 'NV010', '2026-10-22', 'HC', 0), (1, 'NV010', '2026-10-23', 'HC', 0),
 (2, 'NV007', '2026-10-19', 'CA1', 0), (2, 'NV007', '2026-10-20', 'CA1', 0), (2, 'NV007', '2026-10-21', 'CA1', 2), (2, 'NV007', '2026-10-22', 'CA1', 0), (2, 'NV007', '2026-10-23', 'CA1', 0),
 (2, 'NV008', '2026-10-19', 'CA2', 0), (2, 'NV008', '2026-10-20', 'CA2', 0), (2, 'NV008', '2026-10-21', 'CA2', 2), (2, 'NV008', '2026-10-22', 'CA2', 0), (2, 'NV008', '2026-10-23', 'CA2', 0);

-- Chấm công các ngày thứ Hai - thứ Sáu của tháng 9 và tháng 10/2026 (đến 22/10) cho nhân viên đang làm việc
;WITH D AS (
    SELECT CAST('2026-09-01' AS date) AS Ngay
    UNION ALL
    SELECT DATEADD(DAY, 1, Ngay) FROM D WHERE Ngay < '2026-10-22'
)
INSERT INTO ChamCong(MaNV, Ngay, TrangThai, SoGioLam, MaCa)
SELECT n.MaNV, D.Ngay, 'DILAM', 8, CASE WHEN n.MaPB = 'SX' THEN 'CA1' ELSE 'HC' END
FROM NhanVien n CROSS JOIN D
WHERE n.TrangThai <> 'NGHIVIEC'
  AND DATEDIFF(DAY, '2026-01-05', D.Ngay) % 7 < 5
  AND D.Ngay >= COALESCE(n.NgayChinhThuc, (SELECT NgayBatDau FROM QuyetDinhTuyenDung q WHERE q.MaNV = n.MaNV))
OPTION (MAXRECURSION 100);

UPDATE ChamCong SET TrangThai = 'NGHIPHEP', SoGioLam = 0 WHERE MaNV = 'NV006' AND Ngay = '2026-09-10';
UPDATE ChamCong SET TrangThai = 'NGHIKHONGPHEP', SoGioLam = 0 WHERE MaNV = 'NV008' AND Ngay = '2026-09-14';
UPDATE ChamCong SET TrangThai = 'NGHIPHEP', SoGioLam = 0 WHERE MaNV = 'NV007' AND Ngay = '2026-10-08';

INSERT INTO PhieuPhuTroi(SoPhieu, NgayPhuTroi, SoGio, HinhThuc, NguoiLap) VALUES
 ('PT001', '2026-09-12', 4, 'NGAYNGHI', N'Ban chấm công'),
 ('PT002', '2026-09-02', 8, 'NGAYLE',   N'Ban chấm công'),
 ('PT003', '2026-10-15', 2, 'TANGCA',   N'Ban chấm công');
INSERT INTO ChiTietPhuTroi(SoPhieu, MaNV, SoGio) VALUES
 ('PT001', 'NV006', 4), ('PT001', 'NV008', 4), ('PT002', 'NV004', 8),
 ('PT003', 'NV006', 2), ('PT003', 'NV007', 2), ('PT003', 'NV008', 2);

INSERT INTO QuyetDinhKhenThuong(SoQD, NgayQD, DoiTuong, MaNV, MaPB, LoaiKT, LyDo, SoTienMoiNguoi, NguoiKy) VALUES
 ('KT001', '2026-09-30', 'TAPTHE', NULL, 'SX', 'THANHTICH', N'Vượt kế hoạch sản lượng quý 3', 500000, 'NV001'),
 ('KT002', '2026-09-02', 'CANHAN', 'NV006', NULL, 'NGAYLE', N'Thưởng lễ Quốc khánh', 300000, 'NV001');
INSERT INTO PhieuChiKhenThuong(SoQD, MaNV, SoTien, TrangThai, NgayChi, HinhThucChi) VALUES
 ('KT001', 'NV004', 500000, 'DACHI',   '2026-10-05', 'TIENMAT'),
 ('KT001', 'NV007', 500000, 'DACHI',   '2026-10-05', 'TIENMAT'),
 ('KT001', 'NV008', 500000, 'CHUACHI', NULL, NULL),
 ('KT002', 'NV006', 300000, 'DACHI',   '2026-09-05', 'CHUYENKHOAN');

INSERT INTO QuyetDinhKyLuat(SoQD, NgayQD, DoiTuong, MaNV, MaPB, HinhThuc, LyDo, SoTienPhat, CachThu, NguoiKy) VALUES
 ('KL001', '2026-09-20', 'CANHAN', 'NV008', NULL, 'PHATTIEN',   N'Vi phạm quy trình an toàn phân xưởng', 200000, 'TRULUONG', 'NV001'),
 ('KL002', '2026-08-15', 'CANHAN', 'NV007', NULL, 'CANHCAO',    N'Đi làm trễ nhiều lần',                0, NULL, 'NV001');
INSERT INTO PhieuThuNopPhat(SoQD, MaNV, SoTien, CachThu, TrangThai) VALUES
 ('KL001', 'NV008', 200000, 'TRULUONG', 'CHUATHU');
GO
PRINT N'Đã tạo CSDL QuanLyNhanSu và dữ liệu mẫu.';
GO
