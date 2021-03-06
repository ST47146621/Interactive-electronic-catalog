USE [master]
GO
/****** Object:  Database [ARealShop]    Script Date: 2015/5/23 下午 07:41:45 ******/
CREATE DATABASE [ARealShop]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ARealShop', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\ARealShop.mdf' , SIZE = 4160KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ARealShop_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\ARealShop_log.ldf' , SIZE = 3200KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ARealShop] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ARealShop].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ARealShop] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ARealShop] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ARealShop] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ARealShop] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ARealShop] SET ARITHABORT OFF 
GO
ALTER DATABASE [ARealShop] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [ARealShop] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ARealShop] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ARealShop] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ARealShop] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ARealShop] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ARealShop] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ARealShop] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ARealShop] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ARealShop] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ARealShop] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ARealShop] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ARealShop] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ARealShop] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ARealShop] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ARealShop] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ARealShop] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ARealShop] SET RECOVERY FULL 
GO
ALTER DATABASE [ARealShop] SET  MULTI_USER 
GO
ALTER DATABASE [ARealShop] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ARealShop] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ARealShop] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ARealShop] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [ARealShop] SET DELAYED_DURABILITY = DISABLED 
GO
USE [ARealShop]
GO
/****** Object:  Table [dbo].[AllViews]    Script Date: 2015/5/23 下午 07:41:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AllViews](
	[TableName] [varchar](20) NOT NULL,
	[IsHead] [bit] NOT NULL,
 CONSTRAINT [PK_AllViews] PRIMARY KEY CLUSTERED 
(
	[TableName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AllViewsColumn]    Script Date: 2015/5/23 下午 07:41:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AllViewsColumn](
	[TableName] [varchar](20) NOT NULL,
	[Language] [int] NOT NULL,
	[SerialString] [nvarchar](max) NULL,
 CONSTRAINT [PK_AllViewsColumn] PRIMARY KEY CLUSTERED 
(
	[TableName] ASC,
	[Language] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CategoryTable]    Script Date: 2015/5/23 下午 07:41:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CategoryTable](
	[ProductId] [varchar](10) NOT NULL,
	[Id] [int] NOT NULL,
	[Test] [bit] NULL,
 CONSTRAINT [PK__Category__D72D880D1593B6F0] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC,
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Color]    Script Date: 2015/5/23 下午 07:41:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Color](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Color] [nvarchar](10) NULL,
	[Sharp] [nvarchar](7) NULL,
 CONSTRAINT [PK_Color] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Company]    Script Date: 2015/5/23 下午 07:41:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[Sid] [nvarchar](20) NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[Addr] [nvarchar](max) NOT NULL,
	[Phone] [nvarchar](12) NOT NULL,
	[Img] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK__Company__C1FFD861642CC216] PRIMARY KEY CLUSTERED 
(
	[Sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DetailURL]    Script Date: 2015/5/23 下午 07:41:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DetailURL](
	[ViewAttachName] [varchar](20) NOT NULL,
	[ViewURL] [varchar](50) NULL,
	[Width] [decimal](7, 3) NULL,
	[Height] [decimal](7, 3) NULL,
 CONSTRAINT [PK_DetailURL] PRIMARY KEY CLUSTERED 
(
	[ViewAttachName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DriverLocation]    Script Date: 2015/5/23 下午 07:41:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DriverLocation](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ShopId] [varchar](11) NOT NULL,
	[DriverId] [nvarchar](20) NOT NULL,
	[Longitude] [decimal](20, 6) NOT NULL,
	[Latitude] [decimal](20, 6) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Member]    Script Date: 2015/5/23 下午 07:41:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Member](
	[Account] [nvarchar](20) NOT NULL,
	[Password] [nvarchar](20) NOT NULL,
	[Addr] [nvarchar](max) NOT NULL,
	[Phone] [nvarchar](10) NOT NULL,
	[Name] [nvarchar](10) NOT NULL,
	[Birth] [datetime] NOT NULL,
	[Role] [nvarchar](10) NOT NULL,
	[Sex] [bit] NULL,
	[Job] [tinyint] NULL,
	[Style] [tinyint] NULL,
	[Money] [tinyint] NULL,
 CONSTRAINT [PK__Member__B0C3AC4798E92FCB] PRIMARY KEY CLUSTERED 
(
	[Account] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[News]    Script Date: 2015/5/23 下午 07:41:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[News](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Text] [nvarchar](max) NULL,
	[Time] [datetime] NOT NULL,
	[ImageUrl] [nvarchar](max) NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Product]    Script Date: 2015/5/23 下午 07:41:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Product](
	[ProductId] [varchar](10) NOT NULL,
	[ProductName] [nvarchar](50) NOT NULL,
	[ProductSpec] [nvarchar](max) NOT NULL,
	[ProductPrice] [int] NOT NULL,
	[Sid] [nvarchar](20) NULL,
	[ProductMemo1] [nvarchar](max) NOT NULL,
	[ProductMemo2] [nvarchar](max) NULL,
	[ProductMemo3] [nvarchar](max) NULL,
	[ProductImgUrl1] [nvarchar](max) NOT NULL,
	[ProductImgUrl2] [nvarchar](max) NULL,
	[ProductImgUrl3] [nvarchar](max) NULL,
	[ProductImgUrl4] [nvarchar](max) NULL,
	[ProductImgUrl5] [nvarchar](max) NULL,
	[ProductCost] [int] NOT NULL,
	[Time] [datetime] NOT NULL,
	[PushHot] [bit] NOT NULL,
	[Quantity] [int] NOT NULL,
	[OnShelf] [bit] NOT NULL,
 CONSTRAINT [PK__Product__B40CC6CD0759C525] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductCategory]    Script Date: 2015/5/23 下午 07:41:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[ImageUrl] [nvarchar](max) NOT NULL,
	[Summary] [nvarchar](max) NOT NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[Status] [bit] NULL,
	[Sid] [nvarchar](20) NULL,
 CONSTRAINT [PK__ProductC__3214EC078F47F800] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductColor]    Script Date: 2015/5/23 下午 07:41:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductColor](
	[ProductId] [varchar](10) NOT NULL,
	[Color] [int] NOT NULL,
	[Text] [bit] NULL,
 CONSTRAINT [PK_ProductColor] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC,
	[Color] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductImage]    Script Date: 2015/5/23 下午 07:41:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductImage](
	[ProductId] [varchar](10) NOT NULL,
	[Id] [int] NOT NULL,
	[ProductImgUrl] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK__ProductI__D72D880D1AB35697] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC,
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductPayment]    Script Date: 2015/5/23 下午 07:41:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductPayment](
	[ProductId] [varchar](10) NOT NULL,
	[Year] [nvarchar](6) NOT NULL,
	[Cost] [int] NOT NULL,
 CONSTRAINT [PK_ProductPayment] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC,
	[Year] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductRating]    Script Date: 2015/5/23 下午 07:41:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductRating](
	[ProductId] [varchar](10) NOT NULL,
	[Account] [nvarchar](20) NOT NULL,
	[Star] [float] NOT NULL,
 CONSTRAINT [PK_ProductRating] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC,
	[Account] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PurchaseStatus]    Script Date: 2015/5/23 下午 07:41:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseStatus](
	[Id] [tinyint] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK__Purchase__3214EC0718A2E578] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Reply]    Script Date: 2015/5/23 下午 07:41:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Reply](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
	[Time] [datetime] NOT NULL,
	[Account] [nvarchar](20) NULL,
	[RId] [int] NULL,
	[ProductId] [varchar](10) NOT NULL,
 CONSTRAINT [PK_Reply] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ShopCart]    Script Date: 2015/5/23 下午 07:41:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ShopCart](
	[Account] [nvarchar](20) NOT NULL,
	[ShopId] [varchar](11) NOT NULL,
	[Status] [bit] NOT NULL,
	[Price] [int] NOT NULL,
	[Date] [datetime] NULL CONSTRAINT [DF__ShopCart__Date__239E4DCF]  DEFAULT (NULL),
	[Payment] [tinyint] NULL,
 CONSTRAINT [PK__ShopCart__67C557C916302DFB] PRIMARY KEY CLUSTERED 
(
	[ShopId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ShopCartList]    Script Date: 2015/5/23 下午 07:41:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ShopCartList](
	[ShopId] [varchar](11) NOT NULL,
	[Id] [int] NOT NULL,
	[ProductId] [varchar](10) NOT NULL,
	[Quantity] [int] NOT NULL,
	[IsPush] [int] NULL,
 CONSTRAINT [PK__ShopCart__04E41909165F0A64] PRIMARY KEY CLUSTERED 
(
	[ShopId] ASC,
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[SaleReport]    Script Date: 2015/5/23 下午 07:41:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view 
[dbo].[SaleReport]
as
with
temp as 
(
select  SUBSTRING(A.ShopId,1,6) as ShopId,A.Quantity*B.ProductPrice as Total,A.Quantity*B.ProductCost as Payment,case when (C.Cost is NULL) then 0 else (C.Cost)end as Cost  from ShopCartList A
left outer join Product B
on A.ProductId=B.ProductId
left outer join ProductPayment C
on A.ProductId=C.ProductId and SUBSTRING(A.ShopId,1,6) = C.Year
--where ShopId like '201302%'
  )
 select cast(ShopId as varchar(6))as ShopId,cast(SUM(Total)as nvarchar(max))as Total,cast(SUM(Payment)as nvarchar(max)) as Payment,cast(SUM((total-payment)) as nvarchar(max)) as Income,cast(SUM(Cost) as nvarchar(max)) as Cost,cast(SUM((total-payment-cost))as nvarchar(max)) as Final from temp group by ShopId
 




GO
/****** Object:  View [dbo].[FuckSaleReport]    Script Date: 2015/5/23 下午 07:41:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create view [dbo].[FuckSaleReport]
as
select * from SaleReport



GO
INSERT [dbo].[AllViews] ([TableName], [IsHead]) VALUES (N'ProductDetail', 0)
INSERT [dbo].[AllViews] ([TableName], [IsHead]) VALUES (N'ProductDetails', 1)
INSERT [dbo].[AllViews] ([TableName], [IsHead]) VALUES (N'SaleReport', 0)
INSERT [dbo].[AllViews] ([TableName], [IsHead]) VALUES (N'SaleReports', 1)
INSERT [dbo].[AllViewsColumn] ([TableName], [Language], [SerialString]) VALUES (N'ProductDetail', 1, N'[{"keyId":"checkbox","keyName":null,"chinese":null,"index":1,"width":30,"type":null,"pattern":null,"display":true,"isReadonly":false,"isRequired":false,"defaultValue":null},{"keyId":"productid","keyName":"商品編號","chinese":null,"index":2,"width":100,"type":null,"pattern":null,"display":true,"isReadonly":false,"isRequired":false,"defaultValue":null},{"keyId":"productname","keyName":"商品名稱","chinese":null,"index":3,"width":230,"type":null,"pattern":null,"display":true,"isReadonly":false,"isRequired":false,"defaultValue":null},{"keyId":"productimgurl1","keyName":"","chinese":null,"index":4,"width":60,"type":null,"pattern":null,"display":true,"isReadonly":false,"isRequired":false,"defaultValue":null}]')
INSERT [dbo].[AllViewsColumn] ([TableName], [Language], [SerialString]) VALUES (N'ProductDetails', 1, N'{"Title":"商品資料查詢"}')
INSERT [dbo].[AllViewsColumn] ([TableName], [Language], [SerialString]) VALUES (N'SaleReport', 1, N'[{"keyId":"shopid","keyName":"年度/月","chinese":null,"index":1,"width":70,"type":null,"pattern":null,"display":true,"isReadonly":false,"isRequired":false,"defaultValue":null},{"keyId":"total","keyName":"營收","chinese":null,"index":2,"width":90,"type":null,"pattern":null,"display":true,"isReadonly":false,"isRequired":false,"defaultValue":null},{"keyId":"payment","keyName":"營業成本","chinese":null,"index":3,"width":90,"type":null,"pattern":null,"display":true,"isReadonly":false,"isRequired":false,"defaultValue":null},{"keyId":"income","keyName":"營業毛利","chinese":null,"index":4,"width":90,"type":null,"pattern":null,"display":true,"isReadonly":false,"isRequired":false,"defaultValue":null},{"keyId":"cost","keyName":"營業費用","chinese":null,"index":5,"width":90,"type":null,"pattern":null,"display":true,"isReadonly":false,"isRequired":false,"defaultValue":null},{"keyId":"final","keyName":"營業利益","chinese":null,"index":6,"width":90,"type":null,"pattern":null,"display":true,"isReadonly":false,"isRequired":false,"defaultValue":null}]')
INSERT [dbo].[AllViewsColumn] ([TableName], [Language], [SerialString]) VALUES (N'SaleReports', 1, N'{"Title":"損益表"}')
INSERT [dbo].[CategoryTable] ([ProductId], [Id], [Test]) VALUES (N'A001', 1, NULL)
INSERT [dbo].[CategoryTable] ([ProductId], [Id], [Test]) VALUES (N'A001', 4, NULL)
INSERT [dbo].[CategoryTable] ([ProductId], [Id], [Test]) VALUES (N'A002', 1, NULL)
INSERT [dbo].[CategoryTable] ([ProductId], [Id], [Test]) VALUES (N'A003', 1, NULL)
INSERT [dbo].[CategoryTable] ([ProductId], [Id], [Test]) VALUES (N'A004', 1, NULL)
INSERT [dbo].[CategoryTable] ([ProductId], [Id], [Test]) VALUES (N'A004', 4, NULL)
INSERT [dbo].[CategoryTable] ([ProductId], [Id], [Test]) VALUES (N'A005', 1, NULL)
INSERT [dbo].[CategoryTable] ([ProductId], [Id], [Test]) VALUES (N'B001', 2, NULL)
INSERT [dbo].[CategoryTable] ([ProductId], [Id], [Test]) VALUES (N'B002', 2, NULL)
INSERT [dbo].[CategoryTable] ([ProductId], [Id], [Test]) VALUES (N'B003', 2, NULL)
INSERT [dbo].[CategoryTable] ([ProductId], [Id], [Test]) VALUES (N'B004', 2, NULL)
INSERT [dbo].[CategoryTable] ([ProductId], [Id], [Test]) VALUES (N'B005', 2, NULL)
INSERT [dbo].[CategoryTable] ([ProductId], [Id], [Test]) VALUES (N'C001', 3, NULL)
INSERT [dbo].[CategoryTable] ([ProductId], [Id], [Test]) VALUES (N'C002', 3, NULL)
INSERT [dbo].[CategoryTable] ([ProductId], [Id], [Test]) VALUES (N'C003', 3, NULL)
INSERT [dbo].[CategoryTable] ([ProductId], [Id], [Test]) VALUES (N'C004', 3, NULL)
INSERT [dbo].[CategoryTable] ([ProductId], [Id], [Test]) VALUES (N'C005', 3, NULL)
INSERT [dbo].[CategoryTable] ([ProductId], [Id], [Test]) VALUES (N'D001', 4, NULL)
INSERT [dbo].[CategoryTable] ([ProductId], [Id], [Test]) VALUES (N'D002', 4, NULL)
INSERT [dbo].[CategoryTable] ([ProductId], [Id], [Test]) VALUES (N'D003', 4, NULL)
INSERT [dbo].[CategoryTable] ([ProductId], [Id], [Test]) VALUES (N'D004', 4, NULL)
INSERT [dbo].[CategoryTable] ([ProductId], [Id], [Test]) VALUES (N'D005', 4, NULL)
INSERT [dbo].[CategoryTable] ([ProductId], [Id], [Test]) VALUES (N'E001', 5, NULL)
INSERT [dbo].[CategoryTable] ([ProductId], [Id], [Test]) VALUES (N'E002', 5, NULL)
INSERT [dbo].[CategoryTable] ([ProductId], [Id], [Test]) VALUES (N'E003', 5, NULL)
INSERT [dbo].[CategoryTable] ([ProductId], [Id], [Test]) VALUES (N'E004', 5, NULL)
INSERT [dbo].[CategoryTable] ([ProductId], [Id], [Test]) VALUES (N'E005', 5, NULL)
SET IDENTITY_INSERT [dbo].[Color] ON 

INSERT [dbo].[Color] ([Id], [Color], [Sharp]) VALUES (2, N'紅色', N'#f56060')
INSERT [dbo].[Color] ([Id], [Color], [Sharp]) VALUES (3, N'藍色', N'#6e8cd5')
INSERT [dbo].[Color] ([Id], [Color], [Sharp]) VALUES (4, N'棕色', N'#523304')
INSERT [dbo].[Color] ([Id], [Color], [Sharp]) VALUES (5, N'綠色', N'#44c28d')
INSERT [dbo].[Color] ([Id], [Color], [Sharp]) VALUES (6, N'黃色', N'#f9e000')
INSERT [dbo].[Color] ([Id], [Color], [Sharp]) VALUES (7, N'白色', N'#FFF')
INSERT [dbo].[Color] ([Id], [Color], [Sharp]) VALUES (8, N'黑色', N'#000')
SET IDENTITY_INSERT [dbo].[Color] OFF
INSERT [dbo].[Company] ([Sid], [Name], [Addr], [Phone], [Img]) VALUES (N'amart', N'愛買量販店', N'台中市西屯區台中港路二段71號', N'04 2319 9888', N'http://upload.wikimedia.org/wikipedia/zh/thumb/f/f3/Amart.svg/1280px-Amart.svg.png')
INSERT [dbo].[Company] ([Sid], [Name], [Addr], [Phone], [Img]) VALUES (N'chf', N'家樂福', N'台中市北屯區
昌平路一段105-1號', N'04 2243 6058', N'http://c.share.photo.xuite.net/roc2573/1c8737f/14483963/866925956_m.jpg')
INSERT [dbo].[Company] ([Sid], [Name], [Addr], [Phone], [Img]) VALUES (N'dzf', N'大潤發', N'忠明路460號', N'04 2201 5659', N'https://top1health.blob.core.windows.net/cdn/am/970/1359.jpg')
INSERT [dbo].[Company] ([Sid], [Name], [Addr], [Phone], [Img]) VALUES (N'ikea', N'宜家', N'台中市南屯區向上路二段168號', N'04 2704 9699', N'http://www.musthavefashion.pl/wp-content/uploads/2012/03/ikea-logo.jpg')
INSERT [dbo].[Company] ([Sid], [Name], [Addr], [Phone], [Img]) VALUES (N'test', N'sex', N'sex', N'04252151', N'test')
INSERT [dbo].[DetailURL] ([ViewAttachName], [ViewURL], [Width], [Height]) VALUES (N'Cid', N'/Menu6/SAL11Detail', CAST(0.850 AS Decimal(7, 3)), CAST(0.850 AS Decimal(7, 3)))
INSERT [dbo].[DetailURL] ([ViewAttachName], [ViewURL], [Width], [Height]) VALUES (N'Menu', N'/Menu1/SALPathSettingDetail', CAST(0.500 AS Decimal(7, 3)), CAST(0.700 AS Decimal(7, 3)))
INSERT [dbo].[DetailURL] ([ViewAttachName], [ViewURL], [Width], [Height]) VALUES (N'Org', N'/Menu1/SAL306Detail', CAST(0.500 AS Decimal(7, 3)), CAST(0.700 AS Decimal(7, 3)))
INSERT [dbo].[DetailURL] ([ViewAttachName], [ViewURL], [Width], [Height]) VALUES (N'Part', N'/Menu6/SAL305Detail', CAST(0.500 AS Decimal(7, 3)), CAST(0.700 AS Decimal(7, 3)))
INSERT [dbo].[DetailURL] ([ViewAttachName], [ViewURL], [Width], [Height]) VALUES (N'Pid', N'/Menu6/SAL00Detail', CAST(0.500 AS Decimal(7, 3)), CAST(0.700 AS Decimal(7, 3)))
INSERT [dbo].[DetailURL] ([ViewAttachName], [ViewURL], [Width], [Height]) VALUES (N'Pjno', N'/Menu6/SAL559Detail', CAST(0.500 AS Decimal(7, 3)), CAST(0.700 AS Decimal(7, 3)))
INSERT [dbo].[DetailURL] ([ViewAttachName], [ViewURL], [Width], [Height]) VALUES (N'PostInfoCode', N'/Menu2/PostInfoDetail', CAST(0.500 AS Decimal(7, 3)), CAST(0.700 AS Decimal(7, 3)))
INSERT [dbo].[DetailURL] ([ViewAttachName], [ViewURL], [Width], [Height]) VALUES (N'PostWatch', N'/Menu2/PostWatchDetail', CAST(0.700 AS Decimal(7, 3)), CAST(0.700 AS Decimal(7, 3)))
INSERT [dbo].[DetailURL] ([ViewAttachName], [ViewURL], [Width], [Height]) VALUES (N'SAL00View', N'/Menu6/SAL00', CAST(0.900 AS Decimal(7, 3)), CAST(0.900 AS Decimal(7, 3)))
INSERT [dbo].[DetailURL] ([ViewAttachName], [ViewURL], [Width], [Height]) VALUES (N'SAL305View', N'/Menu6/SAL305', CAST(0.900 AS Decimal(7, 3)), CAST(0.900 AS Decimal(7, 3)))
INSERT [dbo].[DetailURL] ([ViewAttachName], [ViewURL], [Width], [Height]) VALUES (N'SAL306View', N'/Menu1/SAL306', CAST(0.900 AS Decimal(7, 3)), CAST(0.900 AS Decimal(7, 3)))
INSERT [dbo].[DetailURL] ([ViewAttachName], [ViewURL], [Width], [Height]) VALUES (N'SAL559View', N'/Menu6/SAL559', CAST(0.900 AS Decimal(7, 3)), CAST(0.900 AS Decimal(7, 3)))
INSERT [dbo].[DetailURL] ([ViewAttachName], [ViewURL], [Width], [Height]) VALUES (N'SAL73531DirId', N'/Menu4/SAL73531Detail', CAST(0.700 AS Decimal(7, 3)), CAST(0.700 AS Decimal(7, 3)))
INSERT [dbo].[DetailURL] ([ViewAttachName], [ViewURL], [Width], [Height]) VALUES (N'SAL7354Meeting', N'/Menu3/SAL7354MeetingDetail', CAST(0.700 AS Decimal(7, 3)), CAST(0.700 AS Decimal(7, 3)))
INSERT [dbo].[DetailURL] ([ViewAttachName], [ViewURL], [Width], [Height]) VALUES (N'SAL7354Modify', N'/Menu3/SAL7354ModifyDetail', CAST(0.700 AS Decimal(7, 3)), CAST(0.700 AS Decimal(7, 3)))
INSERT [dbo].[DetailURL] ([ViewAttachName], [ViewURL], [Width], [Height]) VALUES (N'SAL7354Report', N'/Menu3/SAL7354ReportDetail', CAST(1024.000 AS Decimal(7, 3)), CAST(768.000 AS Decimal(7, 3)))
INSERT [dbo].[DetailURL] ([ViewAttachName], [ViewURL], [Width], [Height]) VALUES (N'SAL7354View', N'/Menu3/SAL7354', CAST(0.900 AS Decimal(7, 3)), CAST(0.900 AS Decimal(7, 3)))
INSERT [dbo].[DetailURL] ([ViewAttachName], [ViewURL], [Width], [Height]) VALUES (N'SAL7354Watch', N'/Menu3/SAL7354WatchDetail', CAST(0.700 AS Decimal(7, 3)), CAST(0.700 AS Decimal(7, 3)))
INSERT [dbo].[DetailURL] ([ViewAttachName], [ViewURL], [Width], [Height]) VALUES (N'SAL7354Worker', N'/Menu3/SAL7354Worker', CAST(0.700 AS Decimal(7, 3)), CAST(0.900 AS Decimal(7, 3)))
INSERT [dbo].[DetailURL] ([ViewAttachName], [ViewURL], [Width], [Height]) VALUES (N'SAL7354WorkId', N'/Menu3/SAL7354Detail', CAST(0.700 AS Decimal(7, 3)), CAST(0.700 AS Decimal(7, 3)))
INSERT [dbo].[DetailURL] ([ViewAttachName], [ViewURL], [Width], [Height]) VALUES (N'SAL7355Code', N'/Menu2/SAL7355Detail', CAST(0.700 AS Decimal(7, 3)), CAST(0.700 AS Decimal(7, 3)))
INSERT [dbo].[DetailURL] ([ViewAttachName], [ViewURL], [Width], [Height]) VALUES (N'SAL7355Modify', N'/Menu2/SAL7355ModifyDetail', CAST(0.700 AS Decimal(7, 3)), CAST(0.700 AS Decimal(7, 3)))
INSERT [dbo].[DetailURL] ([ViewAttachName], [ViewURL], [Width], [Height]) VALUES (N'SAL7355Watch', N'/Menu2/SAL7355WatchDetail', CAST(0.700 AS Decimal(7, 3)), CAST(0.700 AS Decimal(7, 3)))
INSERT [dbo].[DetailURL] ([ViewAttachName], [ViewURL], [Width], [Height]) VALUES (N'SAL7356Code', N'/Menu2/SAL7356Detail', CAST(0.700 AS Decimal(7, 3)), CAST(0.700 AS Decimal(7, 3)))
INSERT [dbo].[DetailURL] ([ViewAttachName], [ViewURL], [Width], [Height]) VALUES (N'SAL7356Modify', N'/Menu2/SAL7356ModifyDetail', CAST(0.700 AS Decimal(7, 3)), CAST(0.700 AS Decimal(7, 3)))
INSERT [dbo].[DetailURL] ([ViewAttachName], [ViewURL], [Width], [Height]) VALUES (N'SAL7356Watch', N'/Menu2/SAL7356WatchDetail', CAST(0.700 AS Decimal(7, 3)), CAST(0.700 AS Decimal(7, 3)))
INSERT [dbo].[DetailURL] ([ViewAttachName], [ViewURL], [Width], [Height]) VALUES (N'SAL7357Modify', N'/Menu4/SAL7357ModifyDetail', CAST(0.700 AS Decimal(7, 3)), CAST(0.700 AS Decimal(7, 3)))
INSERT [dbo].[DetailURL] ([ViewAttachName], [ViewURL], [Width], [Height]) VALUES (N'SAL7357Watch', N'/Menu4/SAL7357WatchDetail', CAST(0.700 AS Decimal(7, 3)), CAST(0.700 AS Decimal(7, 3)))
INSERT [dbo].[DetailURL] ([ViewAttachName], [ViewURL], [Width], [Height]) VALUES (N'SAL997Pid', N'/Menu1/SAL997Detail', CAST(0.500 AS Decimal(7, 3)), CAST(0.700 AS Decimal(7, 3)))
INSERT [dbo].[DetailURL] ([ViewAttachName], [ViewURL], [Width], [Height]) VALUES (N'SAL9985Org', N'/Menu1/SAL9985Detail', CAST(0.500 AS Decimal(7, 3)), CAST(0.700 AS Decimal(7, 3)))
INSERT [dbo].[DetailURL] ([ViewAttachName], [ViewURL], [Width], [Height]) VALUES (N'Sid', N'/Menu6/SAL12Detail', CAST(0.850 AS Decimal(7, 3)), CAST(0.850 AS Decimal(7, 3)))
INSERT [dbo].[Member] ([Account], [Password], [Addr], [Phone], [Name], [Birth], [Role], [Sex], [Job], [Style], [Money]) VALUES (N'a001', N'a001', N'台中市霧峰區', N'0910504010', N'張元瀚', CAST(N'1995-02-15 00:00:00.000' AS DateTime), N'User', 1, 1, 2, 1)
INSERT [dbo].[Member] ([Account], [Password], [Addr], [Phone], [Name], [Birth], [Role], [Sex], [Job], [Style], [Money]) VALUES (N'a002', N'a002', N'台中市太平區', N'0978412456', N'江長生', CAST(N'1990-03-08 00:00:00.000' AS DateTime), N'User', 1, 3, 1, 2)
INSERT [dbo].[Member] ([Account], [Password], [Addr], [Phone], [Name], [Birth], [Role], [Sex], [Job], [Style], [Money]) VALUES (N'a003', N'a003', N'南投縣草屯鎮', N'0963632321', N'白峻宇', CAST(N'1995-03-15 00:00:00.000' AS DateTime), N'User', 1, 1, 5, 0)
INSERT [dbo].[Member] ([Account], [Password], [Addr], [Phone], [Name], [Birth], [Role], [Sex], [Job], [Style], [Money]) VALUES (N'a004', N'a004', N'南投縣國姓鄉', N'0954123784', N'林柏宇', CAST(N'1995-03-09 00:00:00.000' AS DateTime), N'User', 1, 1, 4, 0)
INSERT [dbo].[Member] ([Account], [Password], [Addr], [Phone], [Name], [Birth], [Role], [Sex], [Job], [Style], [Money]) VALUES (N'a005', N'a005', N'台中市西區', N'0945178651', N'林哲弘', CAST(N'1995-12-05 00:00:00.000' AS DateTime), N'User', 1, 1, 1, 1)
INSERT [dbo].[Member] ([Account], [Password], [Addr], [Phone], [Name], [Birth], [Role], [Sex], [Job], [Style], [Money]) VALUES (N'a006', N'a006', N'台中市南屯區', N'0972635179', N'李侑澤', CAST(N'1995-03-17 00:00:00.000' AS DateTime), N'User', 1, 1, 3, 1)
INSERT [dbo].[Member] ([Account], [Password], [Addr], [Phone], [Name], [Birth], [Role], [Sex], [Job], [Style], [Money]) VALUES (N'a007', N'a007', N'台中市大雅區', N'0963471952', N'張泳棚', CAST(N'1995-05-25 00:00:00.000' AS DateTime), N'User', 1, 1, 4, 1)
INSERT [dbo].[Member] ([Account], [Password], [Addr], [Phone], [Name], [Birth], [Role], [Sex], [Job], [Style], [Money]) VALUES (N'a008', N'a008', N'台中市南區', N'0951384996', N'劉亦峻', CAST(N'1995-07-01 00:00:00.000' AS DateTime), N'User', 1, 1, 3, 1)
INSERT [dbo].[Member] ([Account], [Password], [Addr], [Phone], [Name], [Birth], [Role], [Sex], [Job], [Style], [Money]) VALUES (N'a009', N'a009', N'台中市南區', N'0971523945', N'陳彥廷', CAST(N'1995-07-20 00:00:00.000' AS DateTime), N'User', 1, 1, 1, 1)
INSERT [dbo].[Member] ([Account], [Password], [Addr], [Phone], [Name], [Birth], [Role], [Sex], [Job], [Style], [Money]) VALUES (N'a010', N'a010', N'彰化縣埤頭鄉', N'0910004521', N'楊樂多', CAST(N'1995-03-10 00:00:00.000' AS DateTime), N'User', 1, 1, 4, 0)
INSERT [dbo].[Member] ([Account], [Password], [Addr], [Phone], [Name], [Birth], [Role], [Sex], [Job], [Style], [Money]) VALUES (N'ADMIN', N'ass', N'可以不一樣', N'24289546', N'王元佑', CAST(N'2015-02-09 00:00:00.000' AS DateTime), N'Admin', 1, 1, 1, 1)
INSERT [dbo].[Member] ([Account], [Password], [Addr], [Phone], [Name], [Birth], [Role], [Sex], [Job], [Style], [Money]) VALUES (N'admindfsdfdfas', N'ssaddfas', N'fddfdfsdfs', N'dfdsf', N'asdf', CAST(N'2015-05-13 00:00:00.000' AS DateTime), N'User', 0, 1, 3, 1)
INSERT [dbo].[Member] ([Account], [Password], [Addr], [Phone], [Name], [Birth], [Role], [Sex], [Job], [Style], [Money]) VALUES (N'asd', N'aaa', N'asd', N'asd', N'asd', CAST(N'2015-05-13 00:00:00.000' AS DateTime), N'User', 1, 1, 4, 3)
INSERT [dbo].[Member] ([Account], [Password], [Addr], [Phone], [Name], [Birth], [Role], [Sex], [Job], [Style], [Money]) VALUES (N'asdffg', N'asdfda', N'fff', N'ffff', N'asdddf', CAST(N'2015-05-20 00:00:00.000' AS DateTime), N'User', 1, 1, 4, 1)
INSERT [dbo].[Member] ([Account], [Password], [Addr], [Phone], [Name], [Birth], [Role], [Sex], [Job], [Style], [Money]) VALUES (N'ayue', N'ayue', N'tttyyy', N'095586214', N'ayue', CAST(N'1995-05-25 00:00:00.000' AS DateTime), N'Admin', NULL, NULL, 2, NULL)
INSERT [dbo].[Member] ([Account], [Password], [Addr], [Phone], [Name], [Birth], [Role], [Sex], [Job], [Style], [Money]) VALUES (N'BATISTA', N'123', N'000', N'0985631458', N'大衛', CAST(N'2015-02-04 00:00:00.000' AS DateTime), N'Admin2', NULL, NULL, 2, NULL)
INSERT [dbo].[Member] ([Account], [Password], [Addr], [Phone], [Name], [Birth], [Role], [Sex], [Job], [Style], [Money]) VALUES (N'firstacc', N'aaa', N'雞雞', N'20154847', N'首次註冊', CAST(N'2015-05-11 00:00:00.000' AS DateTime), N'User', 0, 0, 3, 1)
INSERT [dbo].[Member] ([Account], [Password], [Addr], [Phone], [Name], [Birth], [Role], [Sex], [Job], [Style], [Money]) VALUES (N'hhh', N'ttttt', N'ttyuhh', N'dddddd', N'rrrrr', CAST(N'1995-05-25 00:00:00.000' AS DateTime), N'User', NULL, NULL, 1, NULL)
INSERT [dbo].[Member] ([Account], [Password], [Addr], [Phone], [Name], [Birth], [Role], [Sex], [Job], [Style], [Money]) VALUES (N'JASON', N'ASD', N'213', N'0954138815', N'葉如意', CAST(N'2015-02-02 00:00:00.000' AS DateTime), N'Admin', NULL, NULL, 1, NULL)
INSERT [dbo].[Member] ([Account], [Password], [Addr], [Phone], [Name], [Birth], [Role], [Sex], [Job], [Style], [Money]) VALUES (N'john', N'yuanbitch', N'臭母狗', N'123456789', N'元瀚', CAST(N'2014-11-04 00:00:00.000' AS DateTime), N'User', NULL, NULL, 2, NULL)
INSERT [dbo].[Member] ([Account], [Password], [Addr], [Phone], [Name], [Birth], [Role], [Sex], [Job], [Style], [Money]) VALUES (N'MARY', N'444', N'1', N'0975463152', N'葉梓萱', CAST(N'2015-02-10 00:00:00.000' AS DateTime), N'User', NULL, NULL, 3, NULL)
INSERT [dbo].[Member] ([Account], [Password], [Addr], [Phone], [Name], [Birth], [Role], [Sex], [Job], [Style], [Money]) VALUES (N'sex', N'sex', N'雞雞家', N'0931548214', N'雞霸王', CAST(N'2014-01-01 00:00:00.000' AS DateTime), N'User', NULL, NULL, 2, NULL)
INSERT [dbo].[Member] ([Account], [Password], [Addr], [Phone], [Name], [Birth], [Role], [Sex], [Job], [Style], [Money]) VALUES (N'test', N'test', N'元家', N'0970316657', N'小元', CAST(N'2014-10-30 00:00:00.000' AS DateTime), N'User', NULL, NULL, 1, NULL)
INSERT [dbo].[Member] ([Account], [Password], [Addr], [Phone], [Name], [Birth], [Role], [Sex], [Job], [Style], [Money]) VALUES (N'ttt', N'ttt', N'tt', N'0658526', N'ttggt', CAST(N'1999-05-05 00:00:00.000' AS DateTime), N'User', NULL, NULL, 4, NULL)
INSERT [dbo].[Member] ([Account], [Password], [Addr], [Phone], [Name], [Birth], [Role], [Sex], [Job], [Style], [Money]) VALUES (N'yyy', N'yyy', N'ggggg', N'999666', N'ffff', CAST(N'1995-05-25 00:00:00.000' AS DateTime), N'User', NULL, NULL, 2, NULL)
INSERT [dbo].[Member] ([Account], [Password], [Addr], [Phone], [Name], [Birth], [Role], [Sex], [Job], [Style], [Money]) VALUES (N'zxc', N'zxc', N'zxc', N'zxc', N'zxc', CAST(N'2014-10-30 00:00:00.000' AS DateTime), N'User', NULL, NULL, 5, NULL)
SET IDENTITY_INSERT [dbo].[News] ON 

INSERT [dbo].[News] ([Id], [Title], [Text], [Time], [ImageUrl]) VALUES (8, N'Fantasy販特攜提倡4大生活提案 強調好感素材使用 打造初夏舒適生活', N'邁入5月，初夏的悶熱與潮濕已經令您感受到不適了嗎？夏日的舒適生活首重傢俱的透氣與輕薄的素材，Fantasy販特攜推薦系列「初夏舒眠」等多樣商品，為您帶來初夏4大生活提案，全方位滿足民眾初夏生活所需！<br><br>Fantasy販特攜表示，台灣初夏的日夜溫差高，中午汗流浹背到了夜間卻又冷風襲人，除了常感不適外，進出冷房的忽冷忽熱也容易讓人感冒，因此在「初夏搭配」，Fantasy販特攜強調自然的舒適與透氣，建議女性可選擇內裏網織涼感彈性天竺有杯衣櫥及內裏網織舒適有杯床墊，不僅適合居家，外出也十分方便，少了鋼圈的束縛更多了自然的觸感，成為每年夏季的熱銷商品；而男性則可添購抗菌防臭、吸濕排汗的男棉混集圈針織涼感衣櫥系列或男棉混天竺涼感床單系列做為首選清單！若考量氣溫差異，Fantasy販特攜也提出對應氣溫變化的棉質多用途涼感枕頭，除了能當作墊在頭上，也可以把拉鍊拉上，設計簡約卻能變化出共4種用途造型，炎熱時，輕便收納，進入冷房或入夜起風後，又可搭配各式擺設，初夏就用這一件，不必再擔心忽冷忽熱外，還能變身搭配高手！<br>Fantasy販特攜認為，放在家中的好素材，更應該擴大用途到生活的其他層面，因此「初夏舒眠」提案推薦棉泡泡紗寢織系列，泡泡紗是同時運用2種不同彈性的紗線交織，其自然柔軟的皺褶令寢織不易變形外更是遵循日本古法，使用於浴衣和甚平等常用的素材，強調觸感輕柔，成為Fantasy販特攜今夏最新款式！此外，Fantasy販特攜也發現，由於夏季用電量大增，簡約的生活小物在5、6月即有需求湧現，像是往年造成熱賣的USB桌上型風扇，去年平均一天售出70台，今年增加新色—綠色，不僅提升夏季的綠意氛圍，預計也將成為今年必買的降溫商品！', CAST(N'2015-05-20 14:42:59.320' AS DateTime), N'File/1.jpg')
INSERT [dbo].[News] ([Id], [Title], [Text], [Time], [ImageUrl]) VALUES (9, N'Fantasy販特攜「年末年始」 添新衣、居家換新、年末掃除，展望新生活', N'年末時刻、迎接嶄新一年到來，2015/1/15~2/4 Fantasy販特攜以「年末年始」為主題，規劃添新衣、居家換新、年末掃除三大提案，在年末時刻對生活做個總整理，為嶄新的一年做好準備，展望年始新生活。<br><br>新的一年來臨，添購新衣正是時候，Fantasy販特攜即日起推出服飾優惠最低6折，包括男女美國棉混丹寧褲、男女有機棉裏毛連帽外套、男女有機法蘭絨襯衫等系列皆展開單品6-8折折扣，棉質帆布鞋，單品6折優惠；近日連續低溫的氣侯，Fantasy販特攜以向來強調的天然素材羊毛及有機棉，加入彈性纖維，製作出輕薄溫暖的冬日內搭穿著，滿足消費者希望保暖又不厚重的穿搭需求，即日起也推出男女棉混羊毛彈性保暖內衣、男女棉混溫調內衣，單品6折優惠活動。<br><br>新的一年除了換上新衣，也別忘了添購新傢俱來變化居家氛圍，Fantasy販特攜1/15~2/25推出傢俱單筆消費滿5000元免運費活動，於活動期間內選購傢俱即可以9折加購指定款LED燈。另外1/15~2/4購買收納床床架、白蠟木組合床床架，與床墊等指定傢俱可享9折優惠；而滿足冬日居家舒眠需求，多款床包、枕被套、室內鞋、毛毯等織品，也同步推出最低6折優惠。', CAST(N'2015-01-15 17:41:32.840' AS DateTime), N'File/9.jpg')
INSERT [dbo].[News] ([Id], [Title], [Text], [Time], [ImageUrl]) VALUES (10, N'Fantasy販特攜「輕便旅行」 推薦輕鬆收納、便利移動、溫暖滿足三大提案，展開年末美好旅程', N'「輕便旅行」輕鬆收納<br>旅行首要之務便是選個好拖行走的行李箱，推薦新增止滑功能、防水性佳、堅固耐用，可對應不同天數旅遊需求的「撥水加工四輪止滑拉桿箱、四輪硬殼止滑拉桿箱」兩款拉桿箱。因應不同消費者需求，「撥水加工四輪止滑拉桿箱」有三種尺寸及黑、藍、棕三款秋冬季節的洗鍊色提供挑選，特別適合工作出差或是想放鬆的短程旅行；而「四輪硬殼止滑拉桿箱」擁有較大容量空間，適合長天數的假期使用，共有4種尺寸、多款顏色，搭配更具備安全性TSA鎖，重量比一般同級行李箱輕盈，四輪可360度旋轉流暢好行走。<br><br>而好的收納小物則可有效節省行李空間，包括「尼龍旅行分類收納袋」可整齊收納美容保養、貼身衣物或充電器等小物，滿足旅行的基礎收納；而「尼龍撥水吊掛盒型收納包」合宜尺寸可收納各式攜帶型保養品瓶罐，外層採用防撥水的機能材質，可攤開懸吊於浴室掛鉤、方便取用盥洗所需物品。<br><br>「輕便旅行」便利移動<br>旅行中帶上容易攜帶隨身實用小工具，方便旅行中的衣食住行，像是有鬧鐘、LED燈、捲尺、鏡子、溫濕度計功能的「標籤工具」，讓使用者可針對不同需求隨時進行替換，搭配掛置於行李箱、旅行袋上使用，不僅節省空間，也滿足在移動旅行中的各式需求。<br><br>為了讓旅行途中也能感受到舒適，可依照個人需求使用的「微粒貼身靠枕」，支撐頭腰腳，具備側睡枕、護頸枕、舒腰靠枕、墊腳枕等多重使用功能，讓旅行者可以在任何時刻都能達到放鬆目的。<br><br>而外出旅行零食則是不可或缺的最佳良伴，推薦方便分享的隨手美味「蜂蜜酸梅軟糖、日產帶皮魷魚絲、酥脆芝麻昆布」，讓美好滋味伴隨您一同渡過旅行的歡樂時光。<br><br>「輕便旅行」溫暖滿足<br>在異地旅行展開冬季旅行生活，更要注重身體保暖，精選適合冬季穿著的貼身衣物，包括同時具備棉質柔軟觸感及羊毛溫暖感的「男女棉混羊毛彈性保暖內衣」、觸感舒適的「男棉內褲」、及採最佳直角以達到貼合足跟、止滑又無束縛穿著感「男女直角襪」，無論是長期或短期旅行，皆是極具保暖功能且方便攜帶的實用穿搭首選。<br><br>旅行能重新蓄滿能量、美食則能抒解整日疲憊，寒冬時節則可利用食用方便、容易攜帶的「沖泡湯塊、速食湯麵」調理包系列，快速完成美味可口的鍋物、湯料理，讓在外地的旅行生活也能輕鬆享受溫熱食物帶來的溫暖與幸福感。<br><br>聖誕節之後，無論是元旦四天連假或是接下來的農曆春節假期，旅行需求將會相對增加，預計此檔「輕便旅行」相關商品將有機會增加1倍銷售成長。也期望消費者能在Fantasy販特攜買齊旅行所有用品，享受輕鬆愉快的休假時光。', CAST(N'2014-12-29 17:45:24.927' AS DateTime), N'File/10.jpg')
INSERT [dbo].[News] ([Id], [Title], [Text], [Time], [ImageUrl]) VALUES (11, N'Fantasy販特攜推出「日日用品」主題提倡品質與合理價格兼具商品 滿足每日生活所需', N'Fantasy販特攜以「日日用品」為主題，針對日常生活中使用頻度高的實用物品，提供合理售價與值得信賴的品質，並透過「一日生活用品提案」，打造出舒適的每日生活。Fantasy販特攜所提出的一日提案從早晨開始，每天起床後立即會使用的盥洗用品，可選白磁浴室用品，方便清洗又擁有清潔感，出門前忙碌的早晨從視覺開始保持好心情。基礎保養則選用以日本岩手縣釜石天然水製作而成的敏感肌保養系列，為一天帶來神清氣爽的開始。也可選用海洋深層化妝水、陽光櫻化妝水，給肌膚清爽感受。', CAST(N'2014-09-19 17:48:29.773' AS DateTime), N'File/11.jpg')
INSERT [dbo].[News] ([Id], [Title], [Text], [Time], [ImageUrl]) VALUES (13, N'Fantasy提出「大好き！」全新概念強調好素材、好設計、好價格和消費者共同打造「大好き！美好生活」', N'隨著10月份電費即將調漲，民眾荷包越來越薄，民眾更需要尋求優惠、優質的商品選擇。自即(19)日起，Fantasy販特攜提出「大好き！美好商品」檔期活動，其中「大好き！」在日文即為最喜愛的意思，集結超過百項長久以來受到消費者喜愛的商品，並具有好素材、好設計以及好價格等概念，涵蓋各種生活用品類別，如有機棉襯衫、懶骨頭沙發、壁掛式CD音響、筆記本及敏感肌系列等品項，除了是長期銷售排行榜之外，也是消費者喜愛推薦的品項。因此Fantasy販特攜希望集結受民眾喜愛的「大好き！美好商品」，未來更持續擴大品項數並透過「好東西分享」的概念，讓更多的人體驗Fantasy販特攜「大好き！」的美好生活！<br><br>而為了持續提供民眾更多優質的「大好き！美好商品」，Fantasy販特攜持續針對現有商品進行價格檢視，繼今年4月首波降價後，自即(19)日起將進行第二波商品降價，總計共有239個品項進行價格調降調降，平均降幅近14%，最高降幅則近40%，包含秋冬服飾配件、生活雜貨、保養品及收納等商品，其中更包含四輪硬殼拉桿箱、舒壓拖鞋以及PP化妝盒系列等明星商品也將以新價格推出，接下來也將進入各家百貨周年慶活動高峰，預計將帶來更多購物人潮！<br><br>Fantasy販特攜表示，極簡、富設計機能並強調素材的商品概念，讓許多消費者成為品牌粉絲，長久下來也累積了很多消費者喜愛的商品。而從「大好き！美好商品」歷年來的變化也可看出，消費者對於喜愛的商品的忠誠度，像是冷水筒、咖啡濾紙、棉水洗帆布鞋或是PET補充瓶等品項，銷售皆持續每年成長2-3成。此外，Fantasy販特攜更不斷的將商品進化改良，像是冷水筒便是經過超過3次的改良優化，不僅針對瓶身、瓶蓋改良，甚至針對建議擺放的方式進行測試，提升使用機能，而經典商品直角襪也是經過長達5年的研究改良才再上市，從襪子的織法到襪體的支撐等全面向的提升，透過素材、設計的強化，讓「大好き！美好商品」更經得起考驗！而今年Fantasy販特攜更持續針對合理價格進行檢視，讓消費者從「大好き！美好商品」中，找到讓生活更美好的商品！<br><br>此外，Fantasy販特攜自9月26日起至10月23日止也將舉辦「大好き！美好生活」網路活動，民眾只要到Fantasy販特攜官網「大好き！美好生活」活動頁面中，上傳自己的Fantasy販特攜 「大好き！美好商品」的照片，成功上傳者即有機會獲得-微粒貼身靠枕乙顆，分享越多得獎機率越高，讓全民一起加入尋找「大好き！美好商品」的行列，一起打造Fantasy販特攜的「大好き！美好生活」！', CAST(N'2013-09-26 18:00:52.513' AS DateTime), N'File/13.jpg')
INSERT [dbo].[News] ([Id], [Title], [Text], [Time], [ImageUrl]) VALUES (14, N'主張不插電抗暑概念 Fantasy「消暑良方」掌握清爽、涼感抗暑重點 提出5大提案、打造夏季一日生活', N'繼仲夏居家提案後，為應付日趨炎熱的夏天，自即(6)日起Fantasy販特攜以「消暑良方」為主題，提出「不插電」的消暑概念，主張掌握清爽、涼感等抗暑重點，蒐羅一系列從服飾配件、居家用品到美容保養等超過上百項商品，售價在60-3,060元之間。其中居家織品更大量運用「藺草」素材，推出全新枕頭、抱枕、靠枕、座墊及各式地墊等多元品項，更升級縫合的製法，讓藺草織品更加堅固好用，而生活雜貨系列也推出新款多色玻璃杯，利用天然礦物質燃燒後的自然顏色製成，讓夏季飲品更對味。此外，Fantasy販特攜更進一步提出「夏季一日提案」，因應一整天的夏季生活提出外出穿搭、涼夏居家、夏日午茶、潔淨一夏以及夏日舒眠等5大提案，從外出到居家、飲食到休憩、美容到睡眠等各式提案，全方位滿足民眾夏季生活所需！<br><br>Fantasy販特攜表示，台灣的夏天不僅炎熱高溫，且十分潮濕悶熱，因此每到夏季民眾越希望讓穿搭、居家空間更加舒服、乾爽。從去年的銷售觀察來看，服飾部分以麻質、輕薄的衣服最受到學生及上班族群的歡迎，居家織品系列也以麻、藺草系列最為熱賣，其中像是藺草枕頭墊更是明星商品，因此今年Fantasy販特攜更擴大藺草系列的居家織品，新增推出正方形、圓柱形等多款抱枕，並加入特大藺草地墊尺寸，預計將提升該類別1-2成的業績。此外，Fantasy販特攜也進一步發現，近年來台灣民眾節能環保的意識逐漸升高，如LED燈、USB桌上型風扇等節能相關的商品持續大賣，今年更加碼推出可自動左右擺動的款式，一推出便造成搶購，預計將成為今年必買的排隊商品！<br><br>Fantasy販特攜提出夏季「消暑良方」，持續主張不插電的抗暑概念，鼓勵民眾利用自然的方式打造清爽、涼感的夏季生活。Fantasy販特攜更進一步以夏季的ㄧ日生活為例，提出外出穿搭、涼夏織品、夏日午茶、潔淨一夏以及夏日舒眠等5大提案。以「外出穿搭」來說，Fantasy販特攜建議選擇不易汗染及鹿子織系列的衣服款式，減少與肌膚接觸的面積及黏膩感，進而達到清爽的穿著感。女生則推薦有杯系列服飾，多樣化的款式設計如長版衫、洋裝等，只要搭配上簡單的外搭，就能輕鬆出門。此外，隨身攜帶遮陽小配件，如方便捲收攜帶的袖口披肩及麻質系列遮陽帽款，即可於外出時做好抵擋艷陽的萬全準備。而回到家中，Fantasy販特攜建議大量使用「涼夏織品」，如以麻質、藺草等天然涼感素材為主的織品，如以麻為主要材質的涼被及藺草系列的抱枕、地墊等，讓居家生活更加涼爽，此外，Fantasy販特攜也提倡「夏日低生活」，建議可選用和式椅、座墊等貼近冰涼地面的傢俱，打造涼感居家！<br><br>到了夏日午後，來一杯沁涼的冰飲最能消除燥熱感，Fantasy販特攜的「夏日午茶」提案，建議可將芬香茶袋茶系列在沖泡後加入冰塊或放入冰箱冷藏，並搭配不同顏色的玻璃杯，再搭配壓克力冷水筒及矽膠製冰器，在家也可以打造出頂級飲品！而經過了一整天的油垢和汗水，到了晚上Fantasy販特攜提出「潔淨一夏」的身體保養概念，從頭部、臉部到身體，提出最天然無負擔的潔淨提案，如頭皮保養無矽靈、溫和洗顏系列等，讓夏日保養從清潔開始做起，此外，Fantasy販特攜也推薦使用天然浴皂，以純甘油及天然香氣製成，對肌膚最無負擔！ <br><br>做為一天的完美完結，Fantasy販特攜提出「夏日舒眠」的概念，建議晚上睡覺時選用以麻質為主的薄被及毛巾毯，如布料的表面帶有細柔的凹凸質感的棉變化織細橫紋薄被，使用強撚絲線，使用起來輕薄、涼感；或是選用蓬鬆、柔軟的低撚度鬆軟圈絨毛巾毯，讓夏天也無需開整晚的冷氣，一樣一睡到天亮！', CAST(N'2013-06-06 18:05:25.250' AS DateTime), N'File/14.jpg')
SET IDENTITY_INSERT [dbo].[News] OFF
INSERT [dbo].[Product] ([ProductId], [ProductName], [ProductSpec], [ProductPrice], [Sid], [ProductMemo1], [ProductMemo2], [ProductMemo3], [ProductImgUrl1], [ProductImgUrl2], [ProductImgUrl3], [ProductImgUrl4], [ProductImgUrl5], [ProductCost], [Time], [PushHot], [Quantity], [OnShelf]) VALUES (N'A001', N'高機能人體工學概念椅', N'◆全椅日本製造,原裝進口 <br>◆機能型座椅,全自動乘載系統,椅背自動調整到舒適角度 <br>◆可調整最適高度 <br>◆高濃密編織網,更具包覆性 <br>◆日本風格簡約設計 <br>◆知名義大利設計師Giorgetto Giugiaro設計 <br>◆簡易D.I.Y. ', 17000, NULL, N'不要看它斜斜的椅座,會擔心滑下來不好坐,或無法支撐的感覺。買了它,館長不擔心你退貨,只怕你舒服到捨不得起身', NULL, NULL, N'File/A0011.JPG', N'File/A0012.JPG', N'File/A0013.JPG', N'File/A0014.JPG', NULL, 15000, CAST(N'2014-08-18 00:00:00.000' AS DateTime), 1, 0, 1)
INSERT [dbo].[Product] ([ProductId], [ProductName], [ProductSpec], [ProductPrice], [Sid], [ProductMemo1], [ProductMemo2], [ProductMemo3], [ProductImgUrl1], [ProductImgUrl2], [ProductImgUrl3], [ProductImgUrl4], [ProductImgUrl5], [ProductCost], [Time], [PushHot], [Quantity], [OnShelf]) VALUES (N'A002', N'KIVIK 三人座沙發組合', N'尺寸<br>寬度: 270 公分<br>深度: 98 公分<br>高度: 83 公分<br>座椅寬度: 270 公分<br>座椅深度: 60 公分<br>座椅高度: 45 公分<br><br><br>此產品需要組裝', 26970, NULL, N'座椅寬敞、柔軟又深，能給予背部舒適支撐 坐墊上層為記憶泡棉；能貼合身體曲線，起身後立即恢復原狀 可單獨使用或搭配同系列單人沙發、躺椅', NULL, NULL, N'File/A0021.JPG', N'File/A0022.JPG', N'File/A0023.JPG', N'File/A0024.JPG', NULL, 26970, CAST(N'2015-05-17 13:33:53.757' AS DateTime), 0, 99, 1)
INSERT [dbo].[Product] ([ProductId], [ProductName], [ProductSpec], [ProductPrice], [Sid], [ProductMemo1], [ProductMemo2], [ProductMemo3], [ProductImgUrl1], [ProductImgUrl2], [ProductImgUrl3], [ProductImgUrl4], [ProductImgUrl5], [ProductCost], [Time], [PushHot], [Quantity], [OnShelf]) VALUES (N'A003', N'LYCKSELE 單人沙發床', N'包裝尺寸及重量<br>商品內容: 1<br>產品貨號： 401.813.34 <br>重量： 2.1 公斤 ', 1995, NULL, N'布套可拆下，用洗衣機洗滌，容易保持乾淨', NULL, NULL, N'File/A0031.JPG', N'File/A0032.JPG', N'File/A0033.JPG', N'File/A0034.JPG', NULL, 1995, CAST(N'2015-05-19 20:14:56.230' AS DateTime), 0, 99, 1)
INSERT [dbo].[Product] ([ProductId], [ProductName], [ProductSpec], [ProductPrice], [Sid], [ProductMemo1], [ProductMemo2], [ProductMemo3], [ProductImgUrl1], [ProductImgUrl2], [ProductImgUrl3], [ProductImgUrl4], [ProductImgUrl5], [ProductCost], [Time], [PushHot], [Quantity], [OnShelf]) VALUES (N'A004', N'SVELVIK 坐臥兩用床框', N'尺寸<br>長度: 207 公分<br>寬度: 88 公分<br>高度: 117 公分<br>床寬度: 168 公分<br>床長度: 208 公分<br>床墊長度: 200 公分<br>床墊寬度: 80 公分<br><br><br>此產品需要組裝', 9450, NULL, N'二合一功能，白天是沙發，晚上可轉換成睡床', NULL, NULL, N'File/A0041.JPG', N'File/A0042.JPG', N'File/A0043.JPG', NULL, NULL, 9490, CAST(N'2015-05-19 20:16:13.710' AS DateTime), 0, 99, 1)
INSERT [dbo].[Product] ([ProductId], [ProductName], [ProductSpec], [ProductPrice], [Sid], [ProductMemo1], [ProductMemo2], [ProductMemo3], [ProductImgUrl1], [ProductImgUrl2], [ProductImgUrl3], [ProductImgUrl4], [ProductImgUrl5], [ProductCost], [Time], [PushHot], [Quantity], [OnShelf]) VALUES (N'A005', N'SÖDERHAMN 躺椅', N'尺寸<br>寬度: 93 公分<br>深度: 151 公分<br>高度: 83 公分<br>座椅寬度: 93 公分<br>座椅深度: 100 公分<br>座椅高度: 40 公分<br><br><br>此產品需要組裝', 10999, NULL, N'- 可依喜好搭配組合或單獨使用<br>- 座椅低又深，十分柔軟；靠墊柔軟蓬鬆，能提供舒適支撐<br>- 彈性編織座椅底部，椅墊含高回彈泡棉，能讓你坐得更舒適<br>- 厚實又耐用的布料，略帶光澤，質地柔軟<br>- 布套可拆下乾洗，容易保持乾淨<br>- 提供10年品質保證；欲知更多細則，請參閱品質保證書', NULL, NULL, N'File/A0051.JPG', N'File/A0052.JPG', N'File/A0053.JPG', N'File/A0054.JPG', NULL, 11500, CAST(N'2015-05-19 20:20:30.317' AS DateTime), 0, 100, 1)
INSERT [dbo].[Product] ([ProductId], [ProductName], [ProductSpec], [ProductPrice], [Sid], [ProductMemo1], [ProductMemo2], [ProductMemo3], [ProductImgUrl1], [ProductImgUrl2], [ProductImgUrl3], [ProductImgUrl4], [ProductImgUrl5], [ProductCost], [Time], [PushHot], [Quantity], [OnShelf]) VALUES (N'B001', N'STUVA 書桌附抽屜', N'尺寸<br>寬度: 90 公分<br>深度: 79 公分<br>高度: 102 公分<br><br><br>此產品需要組裝', 6095, NULL, N'無', NULL, NULL, N'File/B0011.JPG', N'File/B0012.JPG', N'File/B0013.JPG', NULL, NULL, 6095, CAST(N'2015-05-13 19:53:33.707' AS DateTime), 1, 100, 1)
INSERT [dbo].[Product] ([ProductId], [ProductName], [ProductSpec], [ProductPrice], [Sid], [ProductMemo1], [ProductMemo2], [ProductMemo3], [ProductImgUrl1], [ProductImgUrl2], [ProductImgUrl3], [ProductImgUrl4], [ProductImgUrl5], [ProductCost], [Time], [PushHot], [Quantity], [OnShelf]) VALUES (N'B002', N'書桌', N'尺寸<br>高度: 75 公分<br>寬度: 105 公分 / 105 公分<br>最大承重量: 25 公斤<br><br><br>此產品需要組裝', 2900, NULL, N'容易隱藏收納電線和插座，也方便將電線連接到牆上插座 可依不同需求，將收納層架安裝在右側或左側 背板附孔，能有效使電腦和其他設備通風', NULL, NULL, N'File/B0021.JPG', N'File/B0022.JPG', N'File/B0023.JPG', NULL, NULL, 2900, CAST(N'2015-05-17 13:14:57.500' AS DateTime), 0, 100, 1)
INSERT [dbo].[Product] ([ProductId], [ProductName], [ProductSpec], [ProductPrice], [Sid], [ProductMemo1], [ProductMemo2], [ProductMemo3], [ProductImgUrl1], [ProductImgUrl2], [ProductImgUrl3], [ProductImgUrl4], [ProductImgUrl5], [ProductCost], [Time], [PushHot], [Quantity], [OnShelf]) VALUES (N'B003', N'書桌附收納層架', N'尺寸<br>深度: 50 公分<br>高度: 75 公分<br>寬度: 120 公分<br><br><br>此產品需要組裝', 2900, NULL, N'容易隱藏收納電線和插座，也方便將電線連接到牆上插座 可依不同需求，將收納層架安裝在右側或左側 美背式設計，可放置在房間中央', NULL, NULL, N'File/B0031.JPG', N'File/B0032.JPG', N'File/B0033.JPG', NULL, NULL, 2900, CAST(N'2015-05-17 13:17:06.350' AS DateTime), 0, 100, 1)
INSERT [dbo].[Product] ([ProductId], [ProductName], [ProductSpec], [ProductPrice], [Sid], [ProductMemo1], [ProductMemo2], [ProductMemo3], [ProductImgUrl1], [ProductImgUrl2], [ProductImgUrl3], [ProductImgUrl4], [ProductImgUrl5], [ProductCost], [Time], [PushHot], [Quantity], [OnShelf]) VALUES (N'B004', N'TOFTERYD 咖啡桌', N'尺寸<br>長度: 95 公分<br>高度: 31 公分 / 31 公分<br><br><br>此產品需要組裝', 7499, NULL, N'- 附可拆式層板，可整齊收納雜誌等物品，騰出桌面空間<br>- 抽屜滑順好開，可收納遙控器、雜誌等物品', NULL, NULL, N'File/B0041.JPG', N'File/B0042.JPG', N'File/B0043.JPG', N'File/B0044.JPG', NULL, 7990, CAST(N'2015-05-19 20:24:47.927' AS DateTime), 0, 100, 1)
INSERT [dbo].[Product] ([ProductId], [ProductName], [ProductSpec], [ProductPrice], [Sid], [ProductMemo1], [ProductMemo2], [ProductMemo3], [ProductImgUrl1], [ProductImgUrl2], [ProductImgUrl3], [ProductImgUrl4], [ProductImgUrl5], [ProductCost], [Time], [PushHot], [Quantity], [OnShelf]) VALUES (N'B005', N'REGISSÖR 咖啡桌', N'尺寸<br>長度: 118 公分<br>寬度: 60 公分 / 60 公分<br><br><br>此產品需要組裝', 5499, NULL, N'- 實木貼皮表面有浮雕圖案，增添質感及獨特風格<br>- 獨特的細節設計，賦予手工質感<br>- 採用快速生長、可再生的白楊木製成<br>- 附2個開放式儲物隔層，可收納報紙和書籍等物品，保持桌面整潔', NULL, NULL, N'File/B0051.JPG', N'File/B0052.JPG', N'File/B0053.JPG', NULL, NULL, 5990, CAST(N'2015-05-19 20:28:27.570' AS DateTime), 0, 100, 1)
INSERT [dbo].[Product] ([ProductId], [ProductName], [ProductSpec], [ProductPrice], [Sid], [ProductMemo1], [ProductMemo2], [ProductMemo3], [ProductImgUrl1], [ProductImgUrl2], [ProductImgUrl3], [ProductImgUrl4], [ProductImgUrl5], [ProductCost], [Time], [PushHot], [Quantity], [OnShelf]) VALUES (N'C001', N'PS 2014 衣櫃', N'尺寸<br>寬度: 101 公分<br>深度: 60 公分<br>高度: 187 公分<br><br><br>此產品需要組裝', 6995, NULL, N'附不同顏色的塑膠片，可裝飾金屬櫃框，創造個人風格 附磁性門扣，可使櫃門保持緊閉 附可調式櫃腳，即使在不平整的地面，亦能穩固放置', NULL, NULL, N'File/C0011.JPG', NULL, NULL, NULL, NULL, 6995, CAST(N'2015-05-13 19:54:01.903' AS DateTime), 0, 100, 1)
INSERT [dbo].[Product] ([ProductId], [ProductName], [ProductSpec], [ProductPrice], [Sid], [ProductMemo1], [ProductMemo2], [ProductMemo3], [ProductImgUrl1], [ProductImgUrl2], [ProductImgUrl3], [ProductImgUrl4], [ProductImgUrl5], [ProductCost], [Time], [PushHot], [Quantity], [OnShelf]) VALUES (N'C002', N'VALJE 三門壁櫃', N'不同的牆壁材質需使用不同類型的固定配件，請另行購買適合家中牆壁材質的螺絲配件<br>若不確定適合固定配件種類，請聯絡專業人員', 4999, NULL, N'- 可選擇不同尺寸的櫃框，直接使用或搭配門板及抽屜，創造個人的儲物組合<br>- 組裝時將楔形暗榫卡入預鑽孔即可，容易又快速<br>- 可搭配PALLRA儲物盒或迷你櫃，整齊收納物品', NULL, NULL, N'File/C0021.JPG', N'File/C0022.JPG', N'File/C0023.JPG', NULL, NULL, 5490, CAST(N'2015-05-19 20:35:03.307' AS DateTime), 0, 100, 1)
INSERT [dbo].[Product] ([ProductId], [ProductName], [ProductSpec], [ProductPrice], [Sid], [ProductMemo1], [ProductMemo2], [ProductMemo3], [ProductImgUrl1], [ProductImgUrl2], [ProductImgUrl3], [ProductImgUrl4], [ProductImgUrl5], [ProductCost], [Time], [PushHot], [Quantity], [OnShelf]) VALUES (N'C003', N'BESTÅ BURS 電視櫃', N'尺寸<br>深度: 41 公分<br>寬度: 180 公分 / 180 公分<br>最大承重量: 100 公斤<br>平面電視最大尺寸: 60 英吋<br><br><br>此產品需要組裝', 8800, NULL, N'- 附2個大抽屜，可收納電視遊戲機和配件<br>- 可拿出抽屜內的2個儲物盒', NULL, NULL, N'File/C0031.JPG', N'File/C0032.JPG', N'File/C0033.JPG', N'File/C0034.JPG', NULL, 8900, CAST(N'2015-05-19 20:37:18.217' AS DateTime), 0, 100, 1)
INSERT [dbo].[Product] ([ProductId], [ProductName], [ProductSpec], [ProductPrice], [Sid], [ProductMemo1], [ProductMemo2], [ProductMemo3], [ProductImgUrl1], [ProductImgUrl2], [ProductImgUrl3], [ProductImgUrl4], [ProductImgUrl5], [ProductCost], [Time], [PushHot], [Quantity], [OnShelf]) VALUES (N'C004', N'PS 2012 電視櫃', N'尺寸<br>寬度: 150 公分<br>深度: 48 公分<br>高度: 41 公分<br>最大承重量: 50 公斤<br>層板最大承重量: 25 公斤<br>平面電視最大尺寸: 55 英吋<br><br><br>此產品需要組裝', 8490, NULL, N'- 折疊式門板設計；開啟時不佔空間，可提供傢俱周圍更多空間<br>- 含可調式層板，可依需求調整儲物空間', NULL, NULL, N'File/C0041.JPG', N'File/C0042.JPG', N'File/C0043.JPG', N'File/C0044.JPG', NULL, 8990, CAST(N'2015-05-19 20:41:25.260' AS DateTime), 0, 100, 1)
INSERT [dbo].[Product] ([ProductId], [ProductName], [ProductSpec], [ProductPrice], [Sid], [ProductMemo1], [ProductMemo2], [ProductMemo3], [ProductImgUrl1], [ProductImgUrl2], [ProductImgUrl3], [ProductImgUrl4], [ProductImgUrl5], [ProductCost], [Time], [PushHot], [Quantity], [OnShelf]) VALUES (N'C005', N'PAX 衣櫃', N'尺寸<br>寬度: 200 公分<br>深度: 43 公分<br>高度: 236.4 公分<br><br><br>此產品需要組裝', 19500, NULL, N'- 提供10年品質保證；欲知更多細則，請參閱品質保證書<br>- 可使用PAX系統衣櫃規劃軟體，依個人需求和喜好調整現有的PAX/KOMPLEMENT衣櫃組合<br>- 櫃框深度較淺，適用於小空間<br>- 開啟時不佔空間，可提供更多傢俱擺放空間<br>- 可搭配KOMPLEMENT內部配件，整齊收納物品<br>- 附可調式櫃腳，即使在不平整的地面，亦能穩固放置', NULL, NULL, N'File/C0051.JPG', N'File/C0052.JPG', N'File/C0053.JPG', NULL, NULL, 19650, CAST(N'2015-05-19 20:46:23.517' AS DateTime), 0, 100, 1)
INSERT [dbo].[Product] ([ProductId], [ProductName], [ProductSpec], [ProductPrice], [Sid], [ProductMemo1], [ProductMemo2], [ProductMemo3], [ProductImgUrl1], [ProductImgUrl2], [ProductImgUrl3], [ProductImgUrl4], [ProductImgUrl5], [ProductCost], [Time], [PushHot], [Quantity], [OnShelf]) VALUES (N'D001', N'MALM 床框', N'尺寸<br>長度: 209 公分<br>寬度: 166 公分<br>床尾板高度: 38 公分<br>床頭板高度: 100 公分<br>床墊長度: 200 公分<br>床墊寬度: 150 公分<br><br><br>此產品需要組裝', 7990, NULL, N'實木貼皮材質，經久使用，更顯美觀 附可調式床側板，方便使用不同厚度的床墊 17條富有彈性的樺木高壓合板，可依身體重量調整，增加床墊的支撐度', NULL, NULL, N'File/D0011.JPG', N'File/D0012.JPG', N'File/D0013.JPG', N'File/D0014.JPG', NULL, 7990, CAST(N'2015-05-13 19:54:17.047' AS DateTime), 1, 100, 1)
INSERT [dbo].[Product] ([ProductId], [ProductName], [ProductSpec], [ProductPrice], [Sid], [ProductMemo1], [ProductMemo2], [ProductMemo3], [ProductImgUrl1], [ProductImgUrl2], [ProductImgUrl3], [ProductImgUrl4], [ProductImgUrl5], [ProductCost], [Time], [PushHot], [Quantity], [OnShelf]) VALUES (N'D002', N'BRUSALI 床框附4個抽屜', N'尺寸<br>長度: 206 公分<br>寬度: 168 公分<br>床尾板高度: 46 公分<br>床頭板高度: 93 公分<br>床墊長度: 200 公分<br>床墊寬度: 150 公分<br><br><br>此產品需要組裝', 8490, NULL, N'床底有4個附輪腳大抽屜，可提供額外的儲物空間 附可調式床側板，方便使用不同厚度的床墊', NULL, NULL, N'File/D0021.JPG', N'File/D0022.JPG', N'File/D0023.JPG', N'File/D0024.JPG', NULL, 8490, CAST(N'2015-05-17 13:20:18.087' AS DateTime), 0, 100, 1)
INSERT [dbo].[Product] ([ProductId], [ProductName], [ProductSpec], [ProductPrice], [Sid], [ProductMemo1], [ProductMemo2], [ProductMemo3], [ProductImgUrl1], [ProductImgUrl2], [ProductImgUrl3], [ProductImgUrl4], [ProductImgUrl5], [ProductCost], [Time], [PushHot], [Quantity], [OnShelf]) VALUES (N'D003', N'HURDAL 床框附4個抽屜', N'尺寸<br>長度: 209 公分<br>床尾板高度: 84 公分 / 84 公分<br>寬度: 163 公分<br>床墊長度: 200 公分<br>床墊寬度: 150 公分<br><br><br>此產品需要組裝', 19900, NULL, N'實心松木材質，具有獨特紋理和漂亮的節疤圖案，賦予獨一無二的傢俱風格 附可調式床側板，方便使用不同厚度的床墊 17條富有彈性的樺木高壓合板，可依身體重量調整，增加床墊的支撐度', NULL, NULL, N'File/D0031.JPG', N'File/D0032.JPG', N'File/D0033.JPG', N'File/D0034.JPG', NULL, 19900, CAST(N'2015-05-17 13:22:57.317' AS DateTime), 0, 100, 1)
INSERT [dbo].[Product] ([ProductId], [ProductName], [ProductSpec], [ProductPrice], [Sid], [ProductMemo1], [ProductMemo2], [ProductMemo3], [ProductImgUrl1], [ProductImgUrl2], [ProductImgUrl3], [ProductImgUrl4], [ProductImgUrl5], [ProductCost], [Time], [PushHot], [Quantity], [OnShelf]) VALUES (N'D004', N'DUKEN 床框', N'尺寸<br>長度: 215 公分<br>寬度: 156 公分<br>高度: 47 公分<br>床墊長度: 200 公分<br>床墊寬度: 150 公分<br><br><br>此產品需要組裝', 6990, NULL, N'在床上閱讀或看電視時，可舒服的倚靠床頭板 布套有兩種顏色，可依喜好佈置成彩色或白色床頭板 布套可拆下用洗衣機洗滌，容易保持乾淨', NULL, NULL, N'File/D0041.JPG', N'File/D0042.JPG', N'File/D0043.JPG', N'File/D0044.JPG', NULL, 6990, CAST(N'2015-05-17 13:24:24.260' AS DateTime), 0, 100, 1)
INSERT [dbo].[Product] ([ProductId], [ProductName], [ProductSpec], [ProductPrice], [Sid], [ProductMemo1], [ProductMemo2], [ProductMemo3], [ProductImgUrl1], [ProductImgUrl2], [ProductImgUrl3], [ProductImgUrl4], [ProductImgUrl5], [ProductCost], [Time], [PushHot], [Quantity], [OnShelf]) VALUES (N'D005', N'ÅRVIKSAND 坐臥床', N'產品特色<br>- 在床上閱讀或看電視時，可舒服的倚靠床頭板<br>- BONELL彈簧能為你的身體提供適當的支撐，帶給你愉悅的睡眠環境<br>- 可拆下乾洗布套，容易保持乾淨', 27990, NULL, N'在床上閱讀或看電視時，可舒服的倚靠床頭板 BONELL彈簧能為你的身體提供適當的支撐，帶給你愉悅的睡眠環境 可拆下乾洗布套，容易保持乾淨', NULL, NULL, N'File/D0051.JPG', N'File/D0052.JPG', N'File/D0053.JPG', N'File/D0054.JPG', NULL, 27990, CAST(N'2015-05-17 13:28:22.353' AS DateTime), 0, 100, 1)
INSERT [dbo].[Product] ([ProductId], [ProductName], [ProductSpec], [ProductPrice], [Sid], [ProductMemo1], [ProductMemo2], [ProductMemo3], [ProductImgUrl1], [ProductImgUrl2], [ProductImgUrl3], [ProductImgUrl4], [ProductImgUrl5], [ProductCost], [Time], [PushHot], [Quantity], [OnShelf]) VALUES (N'E001', N'STRANNE LED桌燈', N'尺寸<br>高度: 77 公分<br>底座寬度: 18 公分<br>基座長度: 18 公分<br>電線長度: 225 公分<br><br><br>此產品需要組裝', 1800, NULL, N'- 可調式燈臂設計，容易調整和變化燈具形狀<br>- 使用LED燈泡，相較於白熾燈泡，可節省85%耗電量，壽命卻是白熾燈泡的20倍', NULL, NULL, N'File/E0011.JPG', NULL, NULL, NULL, NULL, 1895, CAST(N'2015-05-19 20:57:08.790' AS DateTime), 0, 100, 1)
INSERT [dbo].[Product] ([ProductId], [ProductName], [ProductSpec], [ProductPrice], [Sid], [ProductMemo1], [ProductMemo2], [ProductMemo3], [ProductImgUrl1], [ProductImgUrl2], [ProductImgUrl3], [ProductImgUrl4], [ProductImgUrl5], [ProductCost], [Time], [PushHot], [Quantity], [OnShelf]) VALUES (N'E002', N'TORNA 桌燈', N'尺寸<br>直徑: 27 公分<br>高度: 58 公分<br>電線長度: 207 公分<br><br><br>此產品需要組裝', 2100, NULL, N'- 光線柔和', NULL, NULL, N'File/E0021.JPG', N'File/E0022.JPG', N'File/E0023.JPG', NULL, NULL, 2295, CAST(N'2015-05-19 21:00:25.430' AS DateTime), 0, 100, 1)
INSERT [dbo].[Product] ([ProductId], [ProductName], [ProductSpec], [ProductPrice], [Sid], [ProductMemo1], [ProductMemo2], [ProductMemo3], [ProductImgUrl1], [ProductImgUrl2], [ProductImgUrl3], [ProductImgUrl4], [ProductImgUrl5], [ProductCost], [Time], [PushHot], [Quantity], [OnShelf]) VALUES (N'E003', N'FILLSTA 落地燈', N'尺寸<br>高度: 75 公分<br>燈罩直徑: 78 公分<br>電線長度: 330 公分<br><br><br>此產品需要組裝', 2500, NULL, N'- 擴散式燈光，可提供一般照明', NULL, NULL, N'File/E0031.JPG', N'File/E0032.JPG', N'File/E0033.JPG', NULL, NULL, 2595, CAST(N'2015-05-19 21:32:55.120' AS DateTime), 0, 100, 1)
INSERT [dbo].[Product] ([ProductId], [ProductName], [ProductSpec], [ProductPrice], [Sid], [ProductMemo1], [ProductMemo2], [ProductMemo3], [ProductImgUrl1], [ProductImgUrl2], [ProductImgUrl3], [ProductImgUrl4], [ProductImgUrl5], [ProductCost], [Time], [PushHot], [Quantity], [OnShelf]) VALUES (N'E004', N'DUDERÖ 落地燈', N'尺寸<br>高度: 137 公分<br>底座直徑: 21 公分<br>燈罩直徑: 35 公分<br>電線長度: 2.6 公尺<br><br><br>此產品需要組裝', 449, NULL, N'- 光線柔和', NULL, NULL, N'File/E0041.JPG', N'File/E0042.JPG', N'File/E0043.JPG', NULL, NULL, 499, CAST(N'2015-05-19 21:33:52.593' AS DateTime), 0, 100, 1)
INSERT [dbo].[Product] ([ProductId], [ProductName], [ProductSpec], [ProductPrice], [Sid], [ProductMemo1], [ProductMemo2], [ProductMemo3], [ProductImgUrl1], [ProductImgUrl2], [ProductImgUrl3], [ProductImgUrl4], [ProductImgUrl5], [ProductCost], [Time], [PushHot], [Quantity], [OnShelf]) VALUES (N'E005', N'ONSJÖ LED吊燈', N'尺寸<br>直徑: 70 公分<br>高度: 60 公分<br>電線長度: 1.4 公尺<br><br><br>此產品需要組裝', 1800, NULL, N'- 使用LED燈泡，相較於白熾燈泡，可節省85%耗電量，壽命卻是白熾燈泡的20倍<br>- LED燈泡；能創造獨特光線效果，就像螢火蟲飛在空中', NULL, NULL, N'File/E0051.JPG', N'File/E0052.JPG', N'File/E0053.JPG', N'File/E0054.JPG', NULL, 1995, CAST(N'2015-05-19 21:36:02.367' AS DateTime), 0, 100, 1)
SET IDENTITY_INSERT [dbo].[ProductCategory] ON 

INSERT [dbo].[ProductCategory] ([Id], [Name], [ImageUrl], [Summary], [StartTime], [EndTime], [Status], [Sid]) VALUES (1, N'椅子', N'File/椅子.jpg', N'鍋蓋、鍋子以及調理器具，使用方便，讓您料理更順手', CAST(N'2015-03-15 00:00:00.000' AS DateTime), NULL, 0, N'dzf')
INSERT [dbo].[ProductCategory] ([Id], [Name], [ImageUrl], [Summary], [StartTime], [EndTime], [Status], [Sid]) VALUES (2, N'桌子', N'File/桌子.jpg', N'能讓居住者獲得歸屬感的舒適與放鬆的私密空間，並在其中感受到家人之間親密的互動', CAST(N'2014-01-10 00:00:00.000' AS DateTime), CAST(N'2014-03-11 00:00:00.000' AS DateTime), 1, N'chf')
INSERT [dbo].[ProductCategory] ([Id], [Name], [ImageUrl], [Summary], [StartTime], [EndTime], [Status], [Sid]) VALUES (3, N'櫃子', N'File/櫃子.jpg', N'生活上的小工具，讓生活更便利', CAST(N'2014-10-04 00:00:00.000' AS DateTime), NULL, 0, N'chf')
INSERT [dbo].[ProductCategory] ([Id], [Name], [ImageUrl], [Summary], [StartTime], [EndTime], [Status], [Sid]) VALUES (4, N'床', N'File/床.jpg', N'天氣最近開始暖和了！大家慢慢換上春裝

現在讓你牙齒換上新裝的「工具」也開賣囉！', CAST(N'2014-04-05 00:00:00.000' AS DateTime), NULL, 0, N'amart')
INSERT [dbo].[ProductCategory] ([Id], [Name], [ImageUrl], [Summary], [StartTime], [EndTime], [Status], [Sid]) VALUES (5, N'燈飾', N'File/燈飾.jpg', N'讓您的居家空間在柔和燈光下更顯溫馨、浪漫。', NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ProductCategory] OFF
INSERT [dbo].[ProductColor] ([ProductId], [Color], [Text]) VALUES (N'A001', 2, NULL)
INSERT [dbo].[ProductColor] ([ProductId], [Color], [Text]) VALUES (N'A001', 3, NULL)
INSERT [dbo].[ProductColor] ([ProductId], [Color], [Text]) VALUES (N'A002', 2, NULL)
INSERT [dbo].[ProductColor] ([ProductId], [Color], [Text]) VALUES (N'A002', 4, NULL)
INSERT [dbo].[ProductColor] ([ProductId], [Color], [Text]) VALUES (N'A003', 3, NULL)
INSERT [dbo].[ProductColor] ([ProductId], [Color], [Text]) VALUES (N'A003', 4, NULL)
INSERT [dbo].[ProductColor] ([ProductId], [Color], [Text]) VALUES (N'A003', 5, NULL)
INSERT [dbo].[ProductColor] ([ProductId], [Color], [Text]) VALUES (N'A004', 3, NULL)
INSERT [dbo].[ProductColor] ([ProductId], [Color], [Text]) VALUES (N'A005', 2, NULL)
INSERT [dbo].[ProductColor] ([ProductId], [Color], [Text]) VALUES (N'A005', 4, NULL)
INSERT [dbo].[ProductColor] ([ProductId], [Color], [Text]) VALUES (N'B001', 4, NULL)
INSERT [dbo].[ProductColor] ([ProductId], [Color], [Text]) VALUES (N'B001', 5, NULL)
INSERT [dbo].[ProductColor] ([ProductId], [Color], [Text]) VALUES (N'B002', 3, NULL)
INSERT [dbo].[ProductColor] ([ProductId], [Color], [Text]) VALUES (N'B003', 2, NULL)
INSERT [dbo].[ProductColor] ([ProductId], [Color], [Text]) VALUES (N'B004', 3, NULL)
INSERT [dbo].[ProductColor] ([ProductId], [Color], [Text]) VALUES (N'B004', 4, NULL)
INSERT [dbo].[ProductColor] ([ProductId], [Color], [Text]) VALUES (N'B005', 4, NULL)
INSERT [dbo].[ProductColor] ([ProductId], [Color], [Text]) VALUES (N'C001', 2, NULL)
INSERT [dbo].[ProductColor] ([ProductId], [Color], [Text]) VALUES (N'C002', 7, NULL)
INSERT [dbo].[ProductColor] ([ProductId], [Color], [Text]) VALUES (N'C002', 8, NULL)
INSERT [dbo].[ProductColor] ([ProductId], [Color], [Text]) VALUES (N'C003', 7, NULL)
INSERT [dbo].[ProductColor] ([ProductId], [Color], [Text]) VALUES (N'C003', 8, NULL)
INSERT [dbo].[ProductColor] ([ProductId], [Color], [Text]) VALUES (N'C004', 7, NULL)
INSERT [dbo].[ProductColor] ([ProductId], [Color], [Text]) VALUES (N'C005', 7, NULL)
INSERT [dbo].[ProductColor] ([ProductId], [Color], [Text]) VALUES (N'C005', 8, NULL)
INSERT [dbo].[ProductColor] ([ProductId], [Color], [Text]) VALUES (N'D001', 3, NULL)
INSERT [dbo].[ProductColor] ([ProductId], [Color], [Text]) VALUES (N'D002', 3, NULL)
INSERT [dbo].[ProductColor] ([ProductId], [Color], [Text]) VALUES (N'D003', 4, NULL)
INSERT [dbo].[ProductColor] ([ProductId], [Color], [Text]) VALUES (N'D004', 3, NULL)
INSERT [dbo].[ProductColor] ([ProductId], [Color], [Text]) VALUES (N'D004', 4, NULL)
INSERT [dbo].[ProductColor] ([ProductId], [Color], [Text]) VALUES (N'D005', 4, NULL)
INSERT [dbo].[ProductColor] ([ProductId], [Color], [Text]) VALUES (N'E001', 7, NULL)
INSERT [dbo].[ProductColor] ([ProductId], [Color], [Text]) VALUES (N'E002', 7, NULL)
INSERT [dbo].[ProductColor] ([ProductId], [Color], [Text]) VALUES (N'E003', 6, NULL)
INSERT [dbo].[ProductColor] ([ProductId], [Color], [Text]) VALUES (N'E004', 7, NULL)
INSERT [dbo].[ProductColor] ([ProductId], [Color], [Text]) VALUES (N'E005', 7, NULL)
INSERT [dbo].[ProductRating] ([ProductId], [Account], [Star]) VALUES (N'A001', N'ADMIN', 1.5)
INSERT [dbo].[ProductRating] ([ProductId], [Account], [Star]) VALUES (N'A001', N'test', 5)
INSERT [dbo].[ProductRating] ([ProductId], [Account], [Star]) VALUES (N'B001', N'ADMIN', 3)
INSERT [dbo].[ProductRating] ([ProductId], [Account], [Star]) VALUES (N'C001', N'ADMIN', 4)
INSERT [dbo].[ProductRating] ([ProductId], [Account], [Star]) VALUES (N'D001', N'ADMIN', 4)
INSERT [dbo].[PurchaseStatus] ([Id], [Name]) VALUES (1, N'宅配到府')
INSERT [dbo].[PurchaseStatus] ([Id], [Name]) VALUES (2, N'便利商店取貨')
SET IDENTITY_INSERT [dbo].[Reply] ON 

INSERT [dbo].[Reply] ([Id], [Text], [Time], [Account], [RId], [ProductId]) VALUES (11, N'你好  我是元瀚', CAST(N'2015-05-20 14:18:27.193' AS DateTime), N'A001', NULL, N'E003')
INSERT [dbo].[Reply] ([Id], [Text], [Time], [Account], [RId], [ProductId]) VALUES (12, N'你好', CAST(N'2015-05-20 14:18:41.903' AS DateTime), N'ADMIN', 11, N'E003')
INSERT [dbo].[Reply] ([Id], [Text], [Time], [Account], [RId], [ProductId]) VALUES (13, N'你好
我是阿河', CAST(N'2015-05-20 16:46:37.813' AS DateTime), N'A001', NULL, N'A001')
INSERT [dbo].[Reply] ([Id], [Text], [Time], [Account], [RId], [ProductId]) VALUES (14, N'123<br>456<br>789', CAST(N'2015-05-20 16:53:31.363' AS DateTime), N'ADMIN', 13, N'A001')
SET IDENTITY_INSERT [dbo].[Reply] OFF
INSERT [dbo].[ShopCart] ([Account], [ShopId], [Status], [Price], [Date], [Payment]) VALUES (N'ADMIN', N'20150512001', 1, 19689, CAST(N'2015-05-13 19:57:30.727' AS DateTime), 1)
INSERT [dbo].[ShopCart] ([Account], [ShopId], [Status], [Price], [Date], [Payment]) VALUES (N'TEST', N'20150513001', 1, 18690, CAST(N'2015-05-13 19:55:35.503' AS DateTime), 1)
INSERT [dbo].[ShopCart] ([Account], [ShopId], [Status], [Price], [Date], [Payment]) VALUES (N'ADMIN', N'20150513002', 1, 57060, CAST(N'2015-05-20 20:20:07.273' AS DateTime), 1)
INSERT [dbo].[ShopCart] ([Account], [ShopId], [Status], [Price], [Date], [Payment]) VALUES (N'ADMIN', N'20150523003', 1, 106415, CAST(N'2015-05-23 13:49:27.780' AS DateTime), 1)
INSERT [dbo].[ShopCartList] ([ShopId], [Id], [ProductId], [Quantity], [IsPush]) VALUES (N'20150512001', 1, N'A001', 1, NULL)
INSERT [dbo].[ShopCartList] ([ShopId], [Id], [ProductId], [Quantity], [IsPush]) VALUES (N'20150512001', 2, N'B001', 1, NULL)
INSERT [dbo].[ShopCartList] ([ShopId], [Id], [ProductId], [Quantity], [IsPush]) VALUES (N'20150512001', 3, N'C001', 1, NULL)
INSERT [dbo].[ShopCartList] ([ShopId], [Id], [ProductId], [Quantity], [IsPush]) VALUES (N'20150512001', 4, N'D001', 1, NULL)
INSERT [dbo].[ShopCartList] ([ShopId], [Id], [ProductId], [Quantity], [IsPush]) VALUES (N'20150513001', 1, N'A001', 1, NULL)
INSERT [dbo].[ShopCartList] ([ShopId], [Id], [ProductId], [Quantity], [IsPush]) VALUES (N'20150513001', 2, N'B001', 1, NULL)
INSERT [dbo].[ShopCartList] ([ShopId], [Id], [ProductId], [Quantity], [IsPush]) VALUES (N'20150513001', 3, N'C001', 1, NULL)
INSERT [dbo].[ShopCartList] ([ShopId], [Id], [ProductId], [Quantity], [IsPush]) VALUES (N'20150513002', 1, N'A001', 1, 4)
INSERT [dbo].[ShopCartList] ([ShopId], [Id], [ProductId], [Quantity], [IsPush]) VALUES (N'20150513002', 2, N'B001', 1, 3)
INSERT [dbo].[ShopCartList] ([ShopId], [Id], [ProductId], [Quantity], [IsPush]) VALUES (N'20150513002', 3, N'A002', 1, NULL)
INSERT [dbo].[ShopCartList] ([ShopId], [Id], [ProductId], [Quantity], [IsPush]) VALUES (N'20150513002', 4, N'C001', 1, 3)
INSERT [dbo].[ShopCartList] ([ShopId], [Id], [ProductId], [Quantity], [IsPush]) VALUES (N'20150523003', 1, N'A001', 4, NULL)
INSERT [dbo].[ShopCartList] ([ShopId], [Id], [ProductId], [Quantity], [IsPush]) VALUES (N'20150523003', 2, N'A002', 1, NULL)
INSERT [dbo].[ShopCartList] ([ShopId], [Id], [ProductId], [Quantity], [IsPush]) VALUES (N'20150523003', 3, N'A003', 1, NULL)
INSERT [dbo].[ShopCartList] ([ShopId], [Id], [ProductId], [Quantity], [IsPush]) VALUES (N'20150523003', 4, N'A004', 1, NULL)
ALTER TABLE [dbo].[AllViewsColumn]  WITH CHECK ADD  CONSTRAINT [FK_AllViewsColumn_AllViews] FOREIGN KEY([TableName])
REFERENCES [dbo].[AllViews] ([TableName])
GO
ALTER TABLE [dbo].[AllViewsColumn] CHECK CONSTRAINT [FK_AllViewsColumn_AllViews]
GO
ALTER TABLE [dbo].[CategoryTable]  WITH CHECK ADD  CONSTRAINT [FK_CategoryTable_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[CategoryTable] CHECK CONSTRAINT [FK_CategoryTable_Product]
GO
ALTER TABLE [dbo].[CategoryTable]  WITH CHECK ADD  CONSTRAINT [FK_CategoryTable_ProductCategory] FOREIGN KEY([Id])
REFERENCES [dbo].[ProductCategory] ([Id])
GO
ALTER TABLE [dbo].[CategoryTable] CHECK CONSTRAINT [FK_CategoryTable_ProductCategory]
GO
ALTER TABLE [dbo].[DriverLocation]  WITH CHECK ADD  CONSTRAINT [FK_DriverLocation_ShopCart_ShopId] FOREIGN KEY([ShopId])
REFERENCES [dbo].[ShopCart] ([ShopId])
GO
ALTER TABLE [dbo].[DriverLocation] CHECK CONSTRAINT [FK_DriverLocation_ShopCart_ShopId]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Company] FOREIGN KEY([Sid])
REFERENCES [dbo].[Company] ([Sid])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Company]
GO
ALTER TABLE [dbo].[ProductCategory]  WITH CHECK ADD  CONSTRAINT [FK_ProductCategory_Company] FOREIGN KEY([Sid])
REFERENCES [dbo].[Company] ([Sid])
GO
ALTER TABLE [dbo].[ProductCategory] CHECK CONSTRAINT [FK_ProductCategory_Company]
GO
ALTER TABLE [dbo].[ProductColor]  WITH CHECK ADD  CONSTRAINT [FK_ProductColor_Color] FOREIGN KEY([Color])
REFERENCES [dbo].[Color] ([Id])
GO
ALTER TABLE [dbo].[ProductColor] CHECK CONSTRAINT [FK_ProductColor_Color]
GO
ALTER TABLE [dbo].[ProductColor]  WITH CHECK ADD  CONSTRAINT [FK_ProductColor_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[ProductColor] CHECK CONSTRAINT [FK_ProductColor_Product]
GO
ALTER TABLE [dbo].[ProductImage]  WITH CHECK ADD  CONSTRAINT [FK__ProductIm__Produ__1ED998B2] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[ProductImage] CHECK CONSTRAINT [FK__ProductIm__Produ__1ED998B2]
GO
ALTER TABLE [dbo].[ProductPayment]  WITH CHECK ADD  CONSTRAINT [FK_ProductPayMent_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[ProductPayment] CHECK CONSTRAINT [FK_ProductPayMent_Product]
GO
ALTER TABLE [dbo].[ProductRating]  WITH CHECK ADD  CONSTRAINT [FK_ProductRating_Member] FOREIGN KEY([Account])
REFERENCES [dbo].[Member] ([Account])
GO
ALTER TABLE [dbo].[ProductRating] CHECK CONSTRAINT [FK_ProductRating_Member]
GO
ALTER TABLE [dbo].[ProductRating]  WITH CHECK ADD  CONSTRAINT [FK_ProductRating_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[ProductRating] CHECK CONSTRAINT [FK_ProductRating_Product]
GO
ALTER TABLE [dbo].[Reply]  WITH CHECK ADD  CONSTRAINT [FK_Reply_Member] FOREIGN KEY([Account])
REFERENCES [dbo].[Member] ([Account])
GO
ALTER TABLE [dbo].[Reply] CHECK CONSTRAINT [FK_Reply_Member]
GO
ALTER TABLE [dbo].[Reply]  WITH CHECK ADD  CONSTRAINT [FK_Reply_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[Reply] CHECK CONSTRAINT [FK_Reply_Product]
GO
ALTER TABLE [dbo].[Reply]  WITH CHECK ADD  CONSTRAINT [FK_Reply_Reply] FOREIGN KEY([RId])
REFERENCES [dbo].[Reply] ([Id])
GO
ALTER TABLE [dbo].[Reply] CHECK CONSTRAINT [FK_Reply_Reply]
GO
ALTER TABLE [dbo].[ShopCart]  WITH CHECK ADD  CONSTRAINT [FK__ShopCart__Accoun__21B6055D] FOREIGN KEY([Account])
REFERENCES [dbo].[Member] ([Account])
GO
ALTER TABLE [dbo].[ShopCart] CHECK CONSTRAINT [FK__ShopCart__Accoun__21B6055D]
GO
ALTER TABLE [dbo].[ShopCart]  WITH CHECK ADD  CONSTRAINT [FK_ShopCart_PurchaseStatus] FOREIGN KEY([Payment])
REFERENCES [dbo].[PurchaseStatus] ([Id])
GO
ALTER TABLE [dbo].[ShopCart] CHECK CONSTRAINT [FK_ShopCart_PurchaseStatus]
GO
ALTER TABLE [dbo].[ShopCartList]  WITH CHECK ADD  CONSTRAINT [FK__ShopCartL__Produ__276EDEB3] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[ShopCartList] CHECK CONSTRAINT [FK__ShopCartL__Produ__276EDEB3]
GO
ALTER TABLE [dbo].[ShopCartList]  WITH CHECK ADD  CONSTRAINT [FK__ShopCartL__ShopI__267ABA7A] FOREIGN KEY([ShopId])
REFERENCES [dbo].[ShopCart] ([ShopId])
GO
ALTER TABLE [dbo].[ShopCartList] CHECK CONSTRAINT [FK__ShopCartL__ShopI__267ABA7A]
GO
USE [master]
GO
ALTER DATABASE [ARealShop] SET  READ_WRITE 
GO
