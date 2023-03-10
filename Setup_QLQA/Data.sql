USE [master]
GO
/****** Object:  Database [QuanLiQuanAn]    Script Date: 18/04/2022 10:48:24 CH ******/
CREATE DATABASE [QuanLiQuanAn]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QuanLiQuanAn', FILENAME = N'F:\SQL\MSSQL15.SQLEXPRESS\MSSQL\DATA\QuanLiQuanAn.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'QuanLiQuanAn_log', FILENAME = N'F:\SQL\MSSQL15.SQLEXPRESS\MSSQL\DATA\QuanLiQuanAn_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [QuanLiQuanAn] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QuanLiQuanAn].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
USE QuanLiQuanAn
GO
EXEC sp_changedbowner 'sa'
ALTER DATABASE [QuanLiQuanAn] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QuanLiQuanAn] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QuanLiQuanAn] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QuanLiQuanAn] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QuanLiQuanAn] SET ARITHABORT OFF 
GO
ALTER DATABASE [QuanLiQuanAn] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [QuanLiQuanAn] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QuanLiQuanAn] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QuanLiQuanAn] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QuanLiQuanAn] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QuanLiQuanAn] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QuanLiQuanAn] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QuanLiQuanAn] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QuanLiQuanAn] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QuanLiQuanAn] SET  ENABLE_BROKER 
GO
ALTER DATABASE [QuanLiQuanAn] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QuanLiQuanAn] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QuanLiQuanAn] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QuanLiQuanAn] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QuanLiQuanAn] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QuanLiQuanAn] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QuanLiQuanAn] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QuanLiQuanAn] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [QuanLiQuanAn] SET  MULTI_USER 
GO
ALTER DATABASE [QuanLiQuanAn] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QuanLiQuanAn] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QuanLiQuanAn] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QuanLiQuanAn] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QuanLiQuanAn] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [QuanLiQuanAn] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [QuanLiQuanAn] SET QUERY_STORE = OFF
GO
USE [QuanLiQuanAn]
GO
/****** Object:  Table [dbo].[BanAn]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BanAn](
	[MaBanAn] [int] IDENTITY(1,1) NOT NULL,
	[TenBanAn] [nvarchar](30) NULL,
	[SoKhachNgoi] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaBanAn] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietHoaDon]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietHoaDon](
	[MaHoaDon] [int] NOT NULL,
	[MaMonAn] [int] NOT NULL,
	[SoLuongMon] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaMonAn] ASC,
	[MaHoaDon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChucVu]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChucVu](
	[MaChucVu] [int] IDENTITY(1,1) NOT NULL,
	[TenChucVu] [nvarchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaChucVu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HoaDon]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoaDon](
	[MaHoaDon] [int] IDENTITY(1,1) NOT NULL,
	[NgayThanhToan] [date] NULL,
	[MaBanAn] [int] NULL,
	[ThanhToan] [nvarchar](20) NULL,
	[ThanhTien] [int] NULL,
	[MaNhanVien] [int] NULL,
	[SoLuongNguoi] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaHoaDon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiMonAn]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiMonAn](
	[MaLoaiMonAn] [int] IDENTITY(1,1) NOT NULL,
	[TenLoaiMonAn] [nvarchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaLoaiMonAn] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MonAn]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MonAn](
	[MaMonAn] [int] IDENTITY(1,1) NOT NULL,
	[TenMonAn] [nvarchar](30) NULL,
	[MaLoaiMonAn] [int] NULL,
	[Gia] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaMonAn] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhanVien]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhanVien](
	[MaNhanVien] [int] IDENTITY(1,1) NOT NULL,
	[TenNhanVien] [nvarchar](50) NULL,
	[MatKhau] [varchar](100) NULL,
	[MaChucVu] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaNhanVien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[BanAn] ON 
INSERT [dbo].[BanAn] ([MaBanAn], [TenBanAn], [SoKhachNgoi]) VALUES (1, N'Bàn 1', 0)
INSERT [dbo].[BanAn] ([MaBanAn], [TenBanAn], [SoKhachNgoi]) VALUES (2, N'Bàn 2', 0)
INSERT [dbo].[BanAn] ([MaBanAn], [TenBanAn], [SoKhachNgoi]) VALUES (3, N'Bàn 3', 0)
INSERT [dbo].[BanAn] ([MaBanAn], [TenBanAn], [SoKhachNgoi]) VALUES (4, N'Bàn 4', 0)
INSERT [dbo].[BanAn] ([MaBanAn], [TenBanAn], [SoKhachNgoi]) VALUES (5, N'Bàn 5', 0)
INSERT [dbo].[BanAn] ([MaBanAn], [TenBanAn], [SoKhachNgoi]) VALUES (6, N'Bàn 6', 0)
INSERT [dbo].[BanAn] ([MaBanAn], [TenBanAn], [SoKhachNgoi]) VALUES (7, N'Bàn 7', 0)
INSERT [dbo].[BanAn] ([MaBanAn], [TenBanAn], [SoKhachNgoi]) VALUES (8, N'Bàn 8',0)
INSERT [dbo].[BanAn] ([MaBanAn], [TenBanAn], [SoKhachNgoi]) VALUES (9, N'Bàn 9', 0)
INSERT [dbo].[BanAn] ([MaBanAn], [TenBanAn], [SoKhachNgoi]) VALUES (10, N'Bàn 10', 0)
INSERT [dbo].[BanAn] ([MaBanAn], [TenBanAn], [SoKhachNgoi]) VALUES (11, N'Bàn 11', 0)
INSERT [dbo].[BanAn] ([MaBanAn], [TenBanAn], [SoKhachNgoi]) VALUES (12, N'Bàn 12', 0)
INSERT [dbo].[BanAn] ([MaBanAn], [TenBanAn], [SoKhachNgoi]) VALUES (13, N'Bàn 13', 0)
INSERT [dbo].[BanAn] ([MaBanAn], [TenBanAn], [SoKhachNgoi]) VALUES (14, N'Bàn 14', 0)
INSERT [dbo].[BanAn] ([MaBanAn], [TenBanAn], [SoKhachNgoi]) VALUES (15, N'Bàn 15', 0)
INSERT [dbo].[BanAn] ([MaBanAn], [TenBanAn], [SoKhachNgoi]) VALUES (16, N'Bàn 16', 0)
INSERT [dbo].[BanAn] ([MaBanAn], [TenBanAn], [SoKhachNgoi]) VALUES (17, N'Bàn 17', 0)
INSERT [dbo].[BanAn] ([MaBanAn], [TenBanAn], [SoKhachNgoi]) VALUES (18, N'Bàn 18', 0)
INSERT [dbo].[BanAn] ([MaBanAn], [TenBanAn], [SoKhachNgoi]) VALUES (19, N'Bàn 19', 0)
INSERT [dbo].[BanAn] ([MaBanAn], [TenBanAn], [SoKhachNgoi]) VALUES (20, N'Bàn 20', 0)
SET IDENTITY_INSERT [dbo].[BanAn] OFF
GO
SET IDENTITY_INSERT [dbo].[ChucVu] ON 

INSERT [dbo].[ChucVu] ([MaChucVu], [TenChucVu]) VALUES (1, N'Admin')
INSERT [dbo].[ChucVu] ([MaChucVu], [TenChucVu]) VALUES (2, N'Nhân viên')
INSERT [dbo].[ChucVu] ([MaChucVu], [TenChucVu]) VALUES (3, N'Guest')
SET IDENTITY_INSERT [dbo].[ChucVu] OFF
GO

SET IDENTITY_INSERT [dbo].[LoaiMonAn] ON 

INSERT [dbo].[LoaiMonAn] ([MaLoaiMonAn], [TenLoaiMonAn]) VALUES (1, N'Cơm')
INSERT [dbo].[LoaiMonAn] ([MaLoaiMonAn], [TenLoaiMonAn]) VALUES (2, N'Cafe')
INSERT [dbo].[LoaiMonAn] ([MaLoaiMonAn], [TenLoaiMonAn]) VALUES (3, N'Bún')
INSERT [dbo].[LoaiMonAn] ([MaLoaiMonAn], [TenLoaiMonAn]) VALUES (4, N'Bia')
INSERT [dbo].[LoaiMonAn] ([MaLoaiMonAn], [TenLoaiMonAn]) VALUES (5, N'Hủ tiếu')
INSERT [dbo].[LoaiMonAn] ([MaLoaiMonAn], [TenLoaiMonAn]) VALUES (7, N'Nước ép')
INSERT [dbo].[LoaiMonAn] ([MaLoaiMonAn], [TenLoaiMonAn]) VALUES (8, N'Nước ngọt')
INSERT [dbo].[LoaiMonAn] ([MaLoaiMonAn], [TenLoaiMonAn]) VALUES (9, N'Nước lọc')
SET IDENTITY_INSERT [dbo].[LoaiMonAn] OFF
GO
SET IDENTITY_INSERT [dbo].[MonAn] ON 

INSERT [dbo].[MonAn] ([MaMonAn], [TenMonAn], [MaLoaiMonAn], [Gia]) VALUES (1, N'Cơm chiên trứng', 1, 18000)
INSERT [dbo].[MonAn] ([MaMonAn], [TenMonAn], [MaLoaiMonAn], [Gia]) VALUES (2, N'Cơm thịt', 1, 25000)
INSERT [dbo].[MonAn] ([MaMonAn], [TenMonAn], [MaLoaiMonAn], [Gia]) VALUES (3, N'Cơm xào thập cẩm', 1, 20000)
INSERT [dbo].[MonAn] ([MaMonAn], [TenMonAn], [MaLoaiMonAn], [Gia]) VALUES (4, N'Cơm trắng', 1, 10000)
INSERT [dbo].[MonAn] ([MaMonAn], [TenMonAn], [MaLoaiMonAn], [Gia]) VALUES (5, N'Cafe sữa', 2, 20000)
INSERT [dbo].[MonAn] ([MaMonAn], [TenMonAn], [MaLoaiMonAn], [Gia]) VALUES (6, N'Cafe đen', 2, 15000)
INSERT [dbo].[MonAn] ([MaMonAn], [TenMonAn], [MaLoaiMonAn], [Gia]) VALUES (7, N'Cafe chồn', 2, 25000)
INSERT [dbo].[MonAn] ([MaMonAn], [TenMonAn], [MaLoaiMonAn], [Gia]) VALUES (8, N'Bún xào', 3, 20000)
INSERT [dbo].[MonAn] ([MaMonAn], [TenMonAn], [MaLoaiMonAn], [Gia]) VALUES (9, N'Bún nước', 3, 20000)
INSERT [dbo].[MonAn] ([MaMonAn], [TenMonAn], [MaLoaiMonAn], [Gia]) VALUES (10, N'Bún khô', 3, 20000)
INSERT [dbo].[MonAn] ([MaMonAn], [TenMonAn], [MaLoaiMonAn], [Gia]) VALUES (11, N'Bún mắm', 3, 25000)
INSERT [dbo].[MonAn] ([MaMonAn], [TenMonAn], [MaLoaiMonAn], [Gia]) VALUES (12, N'Bún bò huế', 3, 35000)
INSERT [dbo].[MonAn] ([MaMonAn], [TenMonAn], [MaLoaiMonAn], [Gia]) VALUES (13, N'Bún thịt nướng', 3, 22000)
INSERT [dbo].[MonAn] ([MaMonAn], [TenMonAn], [MaLoaiMonAn], [Gia]) VALUES (14, N'Bìa Sài gòn ', 4, 16000)
INSERT [dbo].[MonAn] ([MaMonAn], [TenMonAn], [MaLoaiMonAn], [Gia]) VALUES (15, N'Bia Heniken', 4, 18000)
INSERT [dbo].[MonAn] ([MaMonAn], [TenMonAn], [MaLoaiMonAn], [Gia]) VALUES (16, N'Bia Tiger', 4, 15000)
INSERT [dbo].[MonAn] ([MaMonAn], [TenMonAn], [MaLoaiMonAn], [Gia]) VALUES (17, N'Bia 333', 4, 15000)
INSERT [dbo].[MonAn] ([MaMonAn], [TenMonAn], [MaLoaiMonAn], [Gia]) VALUES (18, N'Bia Camel', 4, 15000)
INSERT [dbo].[MonAn] ([MaMonAn], [TenMonAn], [MaLoaiMonAn], [Gia]) VALUES (19, N'Hủ tiếu xào', 5, 21000)
INSERT [dbo].[MonAn] ([MaMonAn], [TenMonAn], [MaLoaiMonAn], [Gia]) VALUES (20, N'Hủ tiếu gõ', 5, 18000)
INSERT [dbo].[MonAn] ([MaMonAn], [TenMonAn], [MaLoaiMonAn], [Gia]) VALUES (21, N'Hủ tiếu Sa Đéc', 5, 27000)
INSERT [dbo].[MonAn] ([MaMonAn], [TenMonAn], [MaLoaiMonAn], [Gia]) VALUES (26, N'Nước ép xoài', 7, 28000)
INSERT [dbo].[MonAn] ([MaMonAn], [TenMonAn], [MaLoaiMonAn], [Gia]) VALUES (27, N'Nước ép táo', 7, 20000)
INSERT [dbo].[MonAn] ([MaMonAn], [TenMonAn], [MaLoaiMonAn], [Gia]) VALUES (28, N'Nước ép chanh', 7, 19000)
INSERT [dbo].[MonAn] ([MaMonAn], [TenMonAn], [MaLoaiMonAn], [Gia]) VALUES (29, N'Nước ép mận', 7, 20000)
INSERT [dbo].[MonAn] ([MaMonAn], [TenMonAn], [MaLoaiMonAn], [Gia]) VALUES (30, N'Nước ép lê', 7, 20000)
INSERT [dbo].[MonAn] ([MaMonAn], [TenMonAn], [MaLoaiMonAn], [Gia]) VALUES (31, N'Nước ép dâu', 7, 25000)
INSERT [dbo].[MonAn] ([MaMonAn], [TenMonAn], [MaLoaiMonAn], [Gia]) VALUES (32, N'Nước ép dưa hấu', 7, 21000)
INSERT [dbo].[MonAn] ([MaMonAn], [TenMonAn], [MaLoaiMonAn], [Gia]) VALUES (33, N'Nước ép carot', 7, 23000)
INSERT [dbo].[MonAn] ([MaMonAn], [TenMonAn], [MaLoaiMonAn], [Gia]) VALUES (34, N'Nước mía', 7, 18000)
INSERT [dbo].[MonAn] ([MaMonAn], [TenMonAn], [MaLoaiMonAn], [Gia]) VALUES (35, N'Mirinda', 8, 20000)
INSERT [dbo].[MonAn] ([MaMonAn], [TenMonAn], [MaLoaiMonAn], [Gia]) VALUES (36, N'Mirinda soda', 8, 20000)
INSERT [dbo].[MonAn] ([MaMonAn], [TenMonAn], [MaLoaiMonAn], [Gia]) VALUES (37, N'Sting ', 8, 20000)
INSERT [dbo].[MonAn] ([MaMonAn], [TenMonAn], [MaLoaiMonAn], [Gia]) VALUES (38, N'Pepsi ', 8, 20000)
INSERT [dbo].[MonAn] ([MaMonAn], [TenMonAn], [MaLoaiMonAn], [Gia]) VALUES (39, N'Fanta', 8, 20000)
INSERT [dbo].[MonAn] ([MaMonAn], [TenMonAn], [MaLoaiMonAn], [Gia]) VALUES (40, N'Xá xị', 8, 20000)
INSERT [dbo].[MonAn] ([MaMonAn], [TenMonAn], [MaLoaiMonAn], [Gia]) VALUES (41, N'Aquafina', 9, 10000)
INSERT [dbo].[MonAn] ([MaMonAn], [TenMonAn], [MaLoaiMonAn], [Gia]) VALUES (42, N'Lavie', 9, 10000)
INSERT [dbo].[MonAn] ([MaMonAn], [TenMonAn], [MaLoaiMonAn], [Gia]) VALUES (43, N'Dasani', 9, 10000)
INSERT [dbo].[MonAn] ([MaMonAn], [TenMonAn], [MaLoaiMonAn], [Gia]) VALUES (44, N'Vĩnh hảo', 9, 10000)
INSERT [dbo].[MonAn] ([MaMonAn], [TenMonAn], [MaLoaiMonAn], [Gia]) VALUES (45, N'Vina', 9, 10000)
SET IDENTITY_INSERT [dbo].[MonAn] OFF
GO
SET IDENTITY_INSERT [dbo].[NhanVien] ON 

INSERT [dbo].[NhanVien] ([MaNhanVien], [TenNhanVien], [MatKhau], [MaChucVu]) VALUES (1, N'Duy', N'D@12345678', 1)
INSERT [dbo].[NhanVien] ([MaNhanVien], [TenNhanVien], [MatKhau], [MaChucVu]) VALUES (2, N'Khánh', N'123', 2)
INSERT [dbo].[NhanVien] ([MaNhanVien], [TenNhanVien], [MatKhau], [MaChucVu]) VALUES (3, N'Cao', N'DS2@31313', 2)
SET IDENTITY_INSERT [dbo].[NhanVien] OFF
GO
ALTER TABLE [dbo].[ChiTietHoaDon]  WITH CHECK ADD FOREIGN KEY([MaHoaDon])
REFERENCES [dbo].[HoaDon] ([MaHoaDon])
GO
ALTER TABLE [dbo].[ChiTietHoaDon]  WITH CHECK ADD FOREIGN KEY([MaMonAn])
REFERENCES [dbo].[MonAn] ([MaMonAn])
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD FOREIGN KEY([MaBanAn])
REFERENCES [dbo].[BanAn] ([MaBanAn])
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD FOREIGN KEY([MaNhanVien])
REFERENCES [dbo].[NhanVien] ([MaNhanVien])
GO
ALTER TABLE [dbo].[MonAn]  WITH CHECK ADD FOREIGN KEY([MaLoaiMonAn])
REFERENCES [dbo].[LoaiMonAn] ([MaLoaiMonAn])
GO
ALTER TABLE [dbo].[NhanVien]  WITH CHECK ADD FOREIGN KEY([MaChucVu])
REFERENCES [dbo].[ChucVu] ([MaChucVu])
GO
/****** Object:  StoredProcedure [dbo].[delete_BanAn]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Xoa ban
Create proc [dbo].[delete_BanAn](@maban int)
as
begin
delete BanAn
where MaBanAn=@maban
end
GO
/****** Object:  StoredProcedure [dbo].[delete_cthd]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[delete_cthd](@mahoadon int,@mamonan int)
as 
begin
	delete ChiTietHoaDon 
	where MaHoaDon=@mahoadon and MaMonAn=@mamonan 
end
GO
/****** Object:  StoredProcedure [dbo].[delete_HoaDonByMaBan]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[delete_HoaDonByMaBan](@Mabanan int)
as
begin
delete hoadon 
where mabanan=@mabanan

end
GO
/****** Object:  StoredProcedure [dbo].[delete_HoaDonByMaHd]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  proc [dbo].[delete_HoaDonByMaHd](@Mahoadon int)
as
begin
delete ChiTietHoaDon where ChiTietHoaDon.MaHoaDon=@Mahoadon
delete HoaDon where HoaDon.MaHoaDon=@Mahoadon 

end
GO
/****** Object:  StoredProcedure [dbo].[delete_HoaDonByMaNv]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[delete_HoaDonByMaNv](@Manv int)
as
begin
delete hoadon 
where MaNhanVien=@Manv

end
GO
/****** Object:  StoredProcedure [dbo].[delete_HoaDonByNgay]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--xoa hd boi ngay
CREATE proc [dbo].[delete_HoaDonByNgay](@date date)
as
begin
delete hoadon 
where ngaythanhtoan=@date

end
GO
/****** Object:  StoredProcedure [dbo].[delete_MonAn]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[delete_MonAn]    Script Date: 25/03/2022 12:24:08 SA ******/

--Xoa mon
Create proc [dbo].[delete_MonAn](@TenMon nvarchar(30))
as
begin
delete monan 
where TenMonAn=@TenMon
end
GO
/****** Object:  StoredProcedure [dbo].[deleteLoaiMonAn]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[deleteLoaiMonAn]    Script Date: 25/03/2022 12:24:08 SA ******/

--deleteloaimon
create proc [dbo].[deleteLoaiMonAn](@tenloaimon nvarchar(30))
as
begin
delete LoaiMonAn
where TenLoaiMonAn=@tenloaimon

end
GO
/****** Object:  StoredProcedure [dbo].[insert_BanAn]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[insert_BanAn]    Script Date: 25/03/2022 12:24:08 SA ******/

--insert ban
Create proc [dbo].[insert_BanAn](@TenBanAn nvarchar(30))
as
begin
insert into BanAn values
(@TenBanAn,0)
end
--update ban
GO
/****** Object:  StoredProcedure [dbo].[insert_CTHD]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[insert_CTHD]    Script Date: 25/03/2022 12:24:08 SA ******/

Create proc [dbo].[insert_CTHD]( @MaHd int, @MaMonAn int,@SoLuong int)
as
begin
insert into ChiTietHoaDon values
(@MaHd,@MaMonAn,@SoLuong)
end
--insert hd

GO
/****** Object:  StoredProcedure [dbo].[insert_cthd_2]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--insert cthd 2
create proc [dbo].[insert_cthd_2](@mahoadon int,@mamonan int,@sl int)
as 
begin
	insert into ChiTietHoaDon values
	(@mahoadon,@mamonan ,@sl)
end
GO
/****** Object:  StoredProcedure [dbo].[insert_HoaDon]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[insert_HoaDon]    Script Date: 25/03/2022 12:24:08 SA ******/

Create proc [dbo].[insert_HoaDon](@Date date, @MaBanAn int,@ThanhToan nvarchar(20),@ThanhTien int,@MaNv int,@slNguoi int)
as
begin
insert into HoaDon values
(@Date,@MaBanAn,@ThanhToan,@ThanhTien,@MaNv,@slNguoi)
end
GO
/****** Object:  StoredProcedure [dbo].[insert_MonAn]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[insert_MonAn]    Script Date: 25/03/2022 12:24:08 SA ******/

--insert monan
Create proc [dbo].[insert_MonAn](@TenMonAn nvarchar(30),@MaLoaiMonAn int, @Gia int)
as
begin
insert into MonAn values
(@TenMonAn,@MaLoaiMonAn, @Gia )
end
GO
/****** Object:  StoredProcedure [dbo].[KiemTraAdmin]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[KiemTraAdmin]    Script Date: 25/03/2022 12:24:08 SA ******/


--admin
Create proc [dbo].[KiemTraAdmin](@ten nvarchar(100),@mk nvarchar(100))
as
begin
select MaNhanvien from NhanVien  as nv inner join ChucVu as cv
on nv.MaNhanvien=cv.MaChucVu
where TenNhanVien =@ten and MatKhau=@mk and nv.MaChucVu=1
end
--insert cthd

GO
/****** Object:  StoredProcedure [dbo].[KiemTraDangNhap]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[KiemTraDangNhap]    Script Date: 25/03/2022 12:24:08 SA ******/


--Login

Create proc [dbo].[KiemTraDangNhap](@ten nvarchar(100),@mk nvarchar(100))
as
begin
select * from NhanVien
where TenNhanVien=@ten and MatKhau=@mk
end
GO
/****** Object:  StoredProcedure [dbo].[Menu]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[Menu]    Script Date: 25/03/2022 12:24:08 SA ******/

--menu
Create proc [dbo].[Menu](@mahd int)
as
begin
select Hd.MaHoaDon,(CTHD.SoLuongMon*MonAn.Gia) as Tien,TenMonAn from HoaDon as HD 
inner join ChiTietHoaDon as CTHD 
on HD.MaHoaDon=CTHD.MaHoaDon
inner join MonAn
on MonAn.MaMonAn=CTHD.MaMonAn
where hd.MaHoaDon=@mahd
end

--update MonAn
GO
/****** Object:  StoredProcedure [dbo].[monThinhHanh]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--charts 
create proc [dbo].[monThinhHanh](@month int, @year int) as
begin
select top 7 monan.TenMonAn,sum( cthd.soluongmon) as SoLuong,sum(monan.gia* cthd.soluongmon ) as Tien,cast(round(cast(sum(monan.gia* cthd.soluongmon )as float)/cast((select sum( monan.gia* cthd.soluongmon) from monan
inner join chitiethoadon as cthd
on cthd.mamonan=monan.mamonan inner join HoaDon 
on HoaDon.MaHoaDon=cthd.MaHoaDon)as float)*100,2) as float) as PhanTram from monan
inner join chitiethoadon as cthd 
on cthd.mamonan=monan.mamonan inner join HoaDon
on HoaDon.MaHoaDon=cthd.MaHoaDon
where MONTH(hoadon.NgayThanhToan)=@month and year(hoadon.NgayThanhToan)=@year
group by monan.TenMonAn
end
GO
/****** Object:  StoredProcedure [dbo].[monThinhHanh2]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--charts 2
create proc [dbo].[monThinhHanh2] as
begin
select top 7 monan.TenMonAn,sum( cthd.soluongmon) as SoLuong,sum(monan.gia* cthd.soluongmon ) as Tien,cast(round(cast(sum(monan.gia* cthd.soluongmon )as float)/cast((select sum( monan.gia* cthd.soluongmon) from monan
inner join chitiethoadon as cthd
on cthd.mamonan=monan.mamonan inner join HoaDon 
on HoaDon.MaHoaDon=cthd.MaHoaDon)as float)*100,2) as float) as PhanTram from monan
inner join chitiethoadon as cthd 
on cthd.mamonan=monan.mamonan inner join HoaDon
on HoaDon.MaHoaDon=cthd.MaHoaDon
group by monan.TenMonAn
end


GO
/****** Object:  StoredProcedure [dbo].[reportHoaDon]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[reportHoaDon]    Script Date: 25/03/2022 12:24:08 SA ******/


--inHoaDon
create proc [dbo].[reportHoaDon](@mahd int)
as
begin
select cthd.MaHoaDon ,NgayThanhToan,MonAn.TenMonAn,MonAn.Gia ,SoLuongMon,(MonAn.Gia *SoLuongMon) as Tien, ThanhTien,NhanVien.TenNhanVien from HoaDon as hd inner join ChiTietHoaDon as cthd 
on hd.mahoadon=cthd.mahoadon
inner join MonAn on MonAn.MaMonAn=cthd.MaMonAn
inner join NhanVien on NhanVien.MaNhanVien=hd.MaNhanVien
where hd.MaHoaDon=@mahd
end
GO
/****** Object:  StoredProcedure [dbo].[themcv]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[themcv](@tenChucVu nvarchar(30))
as
begin
insert into ChucVu values
(@tenChucVu)
end
GO
/****** Object:  StoredProcedure [dbo].[themloaiman]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[themloaiman]    Script Date: 25/03/2022 12:24:08 SA ******/

--themloaimon
create proc [dbo].[themloaiman](@tenloaimon nvarchar(30))
as
begin
insert into LoaiMonAn values
(@tenloaimon)
end
GO
/****** Object:  StoredProcedure [dbo].[themNhanVien]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[themNhanVien](@tenNv nvarchar(30), @Mk varchar(30),@MaCv int)
as
begin
insert into NhanVien values
(@tenNv, @Mk,@MaCv)
end
GO
/****** Object:  StoredProcedure [dbo].[TimKiemBan]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[TimKiemBan]    Script Date: 25/03/2022 12:24:08 SA ******/

Create proc [dbo].[TimKiemBan](@key nvarchar(100))
as
begin
select * from BanAn 
where TenBanAn like  N'%'+@key+'%'
end
GO
/****** Object:  StoredProcedure [dbo].[TimKiemMon]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[TimKiemMon]    Script Date: 25/03/2022 12:24:08 SA ******/

Create proc [dbo].[TimKiemMon](@key nvarchar(100))
as
begin
select * from MonAn 
where TenMonAn like  N'%'+@key+'%'
end

--Tim bàn
GO
/****** Object:  StoredProcedure [dbo].[update_BanAn]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[update_BanAn]    Script Date: 25/03/2022 12:24:08 SA ******/

Create proc [dbo].[update_BanAn](@maban int,@TenBanAn nvarchar(30),@soKhach int)
as
begin
update BanAn
set SoKhachNgoi=@soKhach,TenBanAn=@TenBanAn
where MaBanAn=@maban
end
GO
/****** Object:  StoredProcedure [dbo].[update_cthd]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[update_cthd](@mahoadon int,@mamonancu int,@mamonanmoi int,@sl int)
as 
begin
	update ChiTietHoaDon 
	set SoLuongMon=@sl, MaMonAn=@mamonanmoi
	where MaHoaDon=@mahoadon and MaMonAn=@mamonancu
end
GO
/****** Object:  StoredProcedure [dbo].[update_hoadon]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[update_hoadon]    Script Date: 25/03/2022 12:24:08 SA ******/

--update hoadon
Create proc [dbo].[update_hoadon](@mahoadon int)
as
begin
update hoadon
set thanhtoan=N'Đã thanh toán'
where mahoadon=@mahoadon
end
GO
/****** Object:  StoredProcedure [dbo].[update_HoaDon2]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[update_HoaDon2](@mahd int, @maban int,@manv int, @date date, @sl int , @thanhtoan nvarchar(30))
as
begin
update HoaDon
set MaBanAn=@maban, MaNhanVien=@manv,NgayThanhToan=@date, ThanhToan=@thanhtoan, SoLuongNguoi=@sl
where MaHoaDon=@mahd
end
GO
/****** Object:  StoredProcedure [dbo].[update_MonAn]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[update_MonAn]    Script Date: 25/03/2022 12:24:08 SA ******/

Create proc [dbo].[update_MonAn](@TenMonAn nvarchar(30),@MaMonAn int,@MaloaiMonAn int,@Gia int)
as
begin
update monan
set gia=@Gia,maloaimonan=@maloaimonan,tenmonan=@TenmonAn
where MaMonAn=@MaMonAn
end
GO
/****** Object:  StoredProcedure [dbo].[update_sl_cthd]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[update_sl_cthd](@mahoadon int,@mamonan int,@sl int,@slCu int)
as 
begin
--declare @soLuong int
--	if(@slCu>@sl)
--	begin
--		update ChiTietHoaDon 
--		set SoLuongMon-=@slCu-@sl
--		where MaHoaDon=@mahoadon and MaMonAn=@mamonan
--	end
--	else if(@slCu<@sl)
--		begin
			update ChiTietHoaDon 
			set SoLuongMon+=@sl
			where MaHoaDon=@mahoadon and MaMonAn=@mamonan
	--	end
	--else if(@slCu=@sl)
	--	begin
	--		update ChiTietHoaDon 
	--		set SoLuongMon=@slCu
	--		where MaHoaDon=@mahoadon and MaMonAn=@mamonan
	--	end
end
GO
/****** Object:  StoredProcedure [dbo].[update_soKhach]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[update_soKhach]    Script Date: 25/03/2022 12:24:08 SA ******/

--update ban
Create proc [dbo].[update_soKhach](@maBan int,@soKhach int)
as
begin
update BanAn
set SoKhachNgoi+=@soKhach
where MaBanAn=@maBan
end
GO
/****** Object:  StoredProcedure [dbo].[update_soKhach2]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--update ban 2
create proc [dbo].[update_soKhach2](@MaBan int,@soKhach int,@soKhachcu int)
as
begin
update BanAn
set SoKhachNgoi-=@soKhachcu
where MaBanAn=@MaBan
update BanAn
set SoKhachNgoi+=@soKhach
where MaBanAn=@MaBan
end
--updatekhachrave
GO
/****** Object:  StoredProcedure [dbo].[update_soKhachRaVe]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[update_soKhachRaVe]    Script Date: 25/03/2022 12:24:08 SA ******/


Create proc [dbo].[update_soKhachRaVe](@maBan int,@soKhach int)
as
begin
update BanAn
set SoKhachNgoi-=@soKhach
where MaBanAn=@maBan
end
GO
/****** Object:  StoredProcedure [dbo].[update_tt_cthd]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[update_tt_cthd] (@mamonan int, @mahoadon int)as
begin
declare @tien int
set @tien=(select Gia*ChiTietHoaDon.SoLuongMon from MonAn inner join ChiTietHoaDon on MonAn.MaMonAn=ChiTietHoaDon.MaMonAn where ChiTietHoaDon.MaMonAn=@mamonan)
update HoaDon
set ThanhTien-=@tien
--from ChiTietHoaDon inner join HoaDon
--on HoaDon.MaHoaDon=ChiTietHoaDon.MaHoaDon
--inner join MonAn on MonAn.MaMonAn=ChiTietHoaDon.MaMonAn
where HoaDon.MaHoaDon=@mahoadon 
end
GO
/****** Object:  StoredProcedure [dbo].[updateLoaiMonAn]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[updateLoaiMonAn]    Script Date: 25/03/2022 12:24:08 SA ******/

--updateloaimon
create proc [dbo].[updateLoaiMonAn](@oldTenLoaiMon nvarchar(30),@tenloaimon nvarchar(30))
as
begin
update LoaiMonAn
set TenLoaiMonAn=@tenloaimon
where TenLoaiMonAn=@oldTenLoaiMon
end
GO
/****** Object:  StoredProcedure [dbo].[updateNhanVien]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[updateNhanVien](@tenNV nvarchar(30),@Mk varchar(30),@MaCv int,@MaNv int)
as
begin
update NhanVien
set TenNhanVien=@tenNV, MaChucVu=@MaCv, MatKhau=@Mk
where MaNhanVien=@MaNv
end
GO
/****** Object:  StoredProcedure [dbo].[updateTencv]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[updateTencv](@tenChucVu nvarchar(30),@tenChucVuMoi nvarchar(30))
as
begin
update chucvu
set tenchucvu=@tenChucVuMoi 
where tenchucvu=@tenchucvu
end
GO
/****** Object:  StoredProcedure [dbo].[xoaNv]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[xoaNv](@MaNv int)
as
begin
delete NhanVien
where MaNhanVien=@MaNv
end
GO
/****** Object:  StoredProcedure [dbo].[xoaTencv]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[xoaTencv](@tenChucVu nvarchar(30))
as
begin
delete chucvu 
where tenchucvu=@tenchucvu
end
GO
/****** Object:  Trigger [dbo].[trg_delete_hoadon]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE trigger [dbo].[trg_delete_hoadon] on [dbo].[BanAn]
INSTEAD OF DELETE
as
begin

delete HoaDon from deleted 
where HoaDon.MaBanAn=deleted.MaBanAn
delete BanAn from deleted where BanAn.MaBanAn=deleted.MaBanAn
end
GO
ALTER TABLE [dbo].[BanAn] ENABLE TRIGGER [trg_delete_hoadon]
GO
/****** Object:  Trigger [dbo].[trg_update_hoadon_cthd]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  trigger [dbo].[trg_update_hoadon_cthd] on [dbo].[ChiTietHoaDon]
for insert,update
as
begin
update HoaDon 
set ThanhTien=(select sum((MonAn.Gia* ChiTietHoaDon.SoLuongMon)) from ChiTietHoaDon inner join MonAn  on ChiTietHoaDon.MaMonAn=MonAn.MaMonAn where inserted.MaHoaDon=ChiTietHoaDon.MaHoaDon)
from inserted where HoaDon.MaHoaDon=inserted.MaHoaDon
end
GO
ALTER TABLE [dbo].[ChiTietHoaDon] ENABLE TRIGGER [trg_update_hoadon_cthd]
GO
/****** Object:  Trigger [dbo].[trg_delete_cv]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE trigger [dbo].[trg_delete_cv] on [dbo].[ChucVu]
INSTEAD OF DELETE
as
begin

delete NhanVien from deleted where deleted.MaChucVu=NhanVien.MaChucVu
delete ChucVu from deleted where ChucVu.MaChucVu=deleted.MaChucVu
end
GO
ALTER TABLE [dbo].[ChucVu] ENABLE TRIGGER [trg_delete_cv]
GO
/****** Object:  Trigger [dbo].[trg_delete_cthd]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE trigger [dbo].[trg_delete_cthd] on [dbo].[HoaDon]
INSTEAD OF DELETE
as
begin
delete ChiTietHoaDon from deleted 
where ChiTietHoaDon.MaHoaDon=deleted.MaHoaDon
delete HoaDon from deleted where deleted.MaHoaDon=HoaDon.MaHoaDon
update BanAn
set SoKhachNgoi-=deleted.SoLuongNguoi
from deleted
where BanAn.MaBanAn=deleted.MaBanAn
end
GO
ALTER TABLE [dbo].[HoaDon] ENABLE TRIGGER [trg_delete_cthd]
GO
/****** Object:  Trigger [dbo].[trg_delete_loaimonan]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE trigger [dbo].[trg_delete_loaimonan] on [dbo].[LoaiMonAn]
INSTEAD OF DELETE
as
begin
delete MonAn from deleted 
where MonAn.MaLoaiMonAn=deleted.MaLoaiMonAn
delete LoaiMonAn from deleted where LoaiMonAn.MaLoaiMonAn=deleted.MaLoaiMonAn

end
GO
ALTER TABLE [dbo].[LoaiMonAn] ENABLE TRIGGER [trg_delete_loaimonan]
GO
/****** Object:  Trigger [dbo].[trg_Mon_an]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--trg monan
CREATE trigger [dbo].[trg_Mon_an] on [dbo].[MonAn]
INSTEAD OF DELETE
as
begin

delete ChiTietHoaDon from deleted
where ChiTietHoaDon.MaMonAn=deleted.MaMonAn
delete MonAn from deleted where MonAn.MaMonAn=deleted.MaMonAn
end
GO
ALTER TABLE [dbo].[MonAn] ENABLE TRIGGER [trg_Mon_an]
GO
/****** Object:  Trigger [dbo].[trg_delete_nhanvien]    Script Date: 18/04/2022 10:48:25 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE trigger [dbo].[trg_delete_nhanvien] on [dbo].[NhanVien]
INSTEAD OF DELETE
as
begin

delete hoadon from deleted where HoaDon.MaNhanVien=deleted.MaNhanVien
delete NhanVien from deleted where NhanVien.MaNhanVien=deleted.MaNhanVien
end
GO
ALTER TABLE [dbo].[NhanVien] ENABLE TRIGGER [trg_delete_nhanvien]
GO
USE [master]
GO
ALTER DATABASE [QuanLiQuanAn] SET  READ_WRITE 
GO
