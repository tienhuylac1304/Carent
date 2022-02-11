create database QLHeThongThueXe
use QLHeThongThueXe
CREATE TABLE [dbo].[User] (
    [IDUser]           INT   IDENTITY (1, 1)         NOT NULL,
    [HoTen]     NVARCHAR (MAX) NOT NULL,
    [NgaySinh]     VARCHAR (10) NOT NULL,
	[SDT]     VARCHAR (10) NOT NULL,
	[Account] NCHAR (50)   UNIQUE NOT NULL,
    [PasswordUser] NCHAR (50)    NOT NULL,
	[IDLoaiUser] INT default(1) NOT NULL,
	[SoXeDangThue] INT NOT NULL,
	[DiaChi] nvarchar(max) not null
    PRIMARY KEY CLUSTERED ([IDUser] ASC)
);
CREATE TABLE [dbo].[LoaiUser] (
    [Id]       INT    IDENTITY (1, 1)        NOT NULL,
    [Ten]   NVARCHAR (20)     NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);
--Bang Customer
CREATE TABLE [dbo].[Xe] (
    [ID]    INT		IDENTITY (1, 1)		  NOT NULL,
	[HinhAnh] text not null,
    [TinhTrang]  BIT NOT NULL default(0),
    [BienSo] NCHAR (11)  NOT NULL,
    [IDDongXe] INT NOT NULL,
	[IDHangXe] INT NOT NULL,
	[NamSX] INT NOT NULL,
	[TyLeGia] decimal(18,2) NOT NULL,
	[GhiChu] NVARCHAR(MAX) null,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);
--Bang Products
CREATE TABLE [dbo].[DongXe] (
    [ID]     INT      IDENTITY (1, 1)        NOT NULL,
    [Ten]       NVARCHAR (MAX)  NULL,
    [Gia] decimal (18,2)  NOT NULL,
    [SoCho]      int     NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
);
--Bang OrderPro
CREATE TABLE [dbo].[HangXe] (
    [ID]         INT IDENTITY (1, 1)    NOT NULL,
    [Ten]        NVARCHAR(Max)        NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
);
--Bang OrderDetail
CREATE TABLE [dbo].[SoDatXe] (
    [ID]        INT    IDENTITY (1, 1)    NOT NULL,
    [IDKH] INT       NOT NULL,
    [IDXe]   INT      not  NULL,
	[NgayDat] varchar(10) not null,
    PRIMARY KEY CLUSTERED ([ID] ASC),
);
CREATE TABLE [dbo].[SoXe] (
    [ID]        INT   IDENTITY (1, 1)     NOT NULL,
    [IDHD] INT       NOT NULL,
    [TGTraXeTT]  VARCHAR (10) NOT NULL,
	[SoTienTraThem] decimal(18,2) NOT NULL,
	[SoTienCanTra] decimal(18,2) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
);
CREATE TABLE [dbo].[HopDongThueXe] (
    [ID]        INT   IDENTITY (1, 1)     NOT NULL,
    [IDKH] INT       NOT NULL,
	[IDXe] INT       NOT NULL,
	[CMND] char(10) NOT NULL,
    [SoNgayThue]  int       NOT NULL,
    [TGGiaoXe]  VARCHAR (10) NOT NULL,
	[TGTraXe] VARCHAR (10) NOT NULL,
	[DiaDiemGiaoXe] nvarchar(max) not null,
	[ThanhToanTruoc] decimal(18,2) not null,
	[ConLai] decimal(18,2) not null,
	[TienDuKien] decimal(18,2) not null,
	[IDTrangThaiHD] int not null,
    PRIMARY KEY CLUSTERED ([ID] ASC),
);
CREATE TABLE [dbo].[TrangThaiHopDong] (
   [ID] int  IDENTITY (1, 1) not null ,
   [Ten] nvarchar(30) not null,
   PRIMARY KEY CLUSTERED ([ID] ASC),
);
CREATE TABLE [dbo].[ThamSo] (
    [TyLeTraThemKhiQuaNgay]               decimal(18,2)   NOT NULL,
    [TyLeTraThemKhiTonHaiXe]        decimal(18,2)       NOT NULL,
);