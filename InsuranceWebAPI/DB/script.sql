USE [master]
GO
/****** Object:  Database [Insurance]    Script Date: 10-03-2017 00:31:56 ******/
CREATE DATABASE [Insurance] ON  PRIMARY 
GO
ALTER DATABASE [Insurance] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Insurance] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Insurance] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Insurance] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Insurance] SET ARITHABORT OFF 
GO
ALTER DATABASE [Insurance] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Insurance] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Insurance] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Insurance] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Insurance] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Insurance] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Insurance] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Insurance] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Insurance] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Insurance] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Insurance] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Insurance] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Insurance] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Insurance] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Insurance] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Insurance] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Insurance] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Insurance] SET RECOVERY FULL 
GO
ALTER DATABASE [Insurance] SET  MULTI_USER 
GO
ALTER DATABASE [Insurance] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Insurance] SET DB_CHAINING OFF 
GO
USE [Insurance]
GO
/****** Object:  Table [dbo].[Documents]    Script Date: 10-03-2017 00:31:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Documents](
	[DocumentId] [int] IDENTITY(1,1) NOT NULL,
	[DocumentName] [varchar](100) NULL,
	[PolicyId] [int] NULL,
 CONSTRAINT [PK_Documents] PRIMARY KEY CLUSTERED 
(
	[DocumentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Policy]    Script Date: 10-03-2017 00:31:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Policy](
	[PolicyID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](150) NULL,
	[PolicyType] [int] NULL,
	[Mobile] [varchar](15) NULL,
	[Mobile1] [varchar](15) NULL,
	[Address1] [varchar](100) NULL,
	[Address2] [varchar](100) NULL,
	[City] [varchar](50) NULL,
	[Email] [varchar](100) NULL,
	[AdditionalName1] [varchar](100) NULL,
	[AdditionalName2] [varchar](100) NULL,
	[AdditionalName3] [varchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [bit] NULL CONSTRAINT [DF_Policy_IsActive]  DEFAULT ((1)),
	[IsDeleted] [bit] NULL CONSTRAINT [DF_Policy_IsDeleted]  DEFAULT ((0)),
 CONSTRAINT [PK_Policy] PRIMARY KEY CLUSTERED 
(
	[PolicyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PolicyHistory]    Script Date: 10-03-2017 00:31:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PolicyHistory](
	[PolicyHistoryID] [int] IDENTITY(1,1) NOT NULL,
	[PolicyID] [int] NOT NULL,
	[PolicyNumber] [varchar](50) NULL,
	[PolicyAmount] [decimal](18, 2) NULL,
	[IsCurrent] [bit] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL CONSTRAINT [DF_PolicyHistory_IsDeleted]  DEFAULT ((0)),
 CONSTRAINT [PK_PolicyHistory] PRIMARY KEY CLUSTERED 
(
	[PolicyHistoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PolicyType]    Script Date: 10-03-2017 00:31:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PolicyType](
	[PolicyTypeID] [int] IDENTITY(1,1) NOT NULL,
	[PolicyType] [varchar](150) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_PolicyType] PRIMARY KEY CLUSTERED 
(
	[PolicyTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 10-03-2017 00:31:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](100) NOT NULL,
	[Password] [varchar](50) NULL,
	[Salt] [varchar](50) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Policy] ON 

GO
INSERT [dbo].[Policy] ([PolicyID], [Name], [PolicyType], [Mobile], [Mobile1], [Address1], [Address2], [City], [Email], [AdditionalName1], [AdditionalName2], [AdditionalName3], [CreatedDate], [ModifiedDate], [IsActive], [IsDeleted]) VALUES (1, N'Kaushik Modi', 1, N'9898098980', NULL, NULL, NULL, NULL, N'abcdef@gmail.com', NULL, NULL, NULL, NULL, NULL, 1, 0)
GO
INSERT [dbo].[Policy] ([PolicyID], [Name], [PolicyType], [Mobile], [Mobile1], [Address1], [Address2], [City], [Email], [AdditionalName1], [AdditionalName2], [AdditionalName3], [CreatedDate], [ModifiedDate], [IsActive], [IsDeleted]) VALUES (2, N'Kaushik Modi', 2, N'9898098980', NULL, NULL, NULL, NULL, N'rmodi2012@gmail.com', NULL, NULL, NULL, NULL, NULL, 1, 0)
GO
SET IDENTITY_INSERT [dbo].[Policy] OFF
GO
SET IDENTITY_INSERT [dbo].[PolicyHistory] ON 

GO
INSERT [dbo].[PolicyHistory] ([PolicyHistoryID], [PolicyID], [PolicyNumber], [PolicyAmount], [IsCurrent], [StartDate], [EndDate], [ModifiedDate], [IsDeleted]) VALUES (1, 1, N'300800/14/48/10005467', CAST(2500.00 AS Decimal(18, 2)), 1, CAST(N'2017-01-01 00:00:00.000' AS DateTime), CAST(N'2017-05-01 00:00:00.000' AS DateTime), NULL, 0)
GO
INSERT [dbo].[PolicyHistory] ([PolicyHistoryID], [PolicyID], [PolicyNumber], [PolicyAmount], [IsCurrent], [StartDate], [EndDate], [ModifiedDate], [IsDeleted]) VALUES (2, 2, N'300800/14/48/10005468', CAST(3000.00 AS Decimal(18, 2)), 1, CAST(N'2016-12-01 00:00:00.000' AS DateTime), CAST(N'2017-04-01 00:00:00.000' AS DateTime), NULL, 0)
GO
INSERT [dbo].[PolicyHistory] ([PolicyHistoryID], [PolicyID], [PolicyNumber], [PolicyAmount], [IsCurrent], [StartDate], [EndDate], [ModifiedDate], [IsDeleted]) VALUES (3, 1, N'300800/14/48/10005467', CAST(2500.00 AS Decimal(18, 2)), 0, CAST(N'2016-06-01 00:00:00.000' AS DateTime), CAST(N'2016-12-31 00:00:00.000' AS DateTime), NULL, 0)
GO
SET IDENTITY_INSERT [dbo].[PolicyHistory] OFF
GO
SET IDENTITY_INSERT [dbo].[PolicyType] ON 

GO
INSERT [dbo].[PolicyType] ([PolicyTypeID], [PolicyType], [CreatedDate], [ModifiedDate]) VALUES (1, N'Individual Medical', CAST(N'2017-03-06 00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[PolicyType] ([PolicyTypeID], [PolicyType], [CreatedDate], [ModifiedDate]) VALUES (2, N'Family Floater Medical', CAST(N'2017-03-06 00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[PolicyType] ([PolicyTypeID], [PolicyType], [CreatedDate], [ModifiedDate]) VALUES (3, N'Vehicle', CAST(N'2017-03-06 00:00:00.000' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[PolicyType] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

GO
INSERT [dbo].[User] ([UserID], [UserName], [Password], [Salt]) VALUES (1, N'nilay', N'4981E70192EE6E60C255BEB8F0E5CDF3A856DCA6', N'fW+PEZM=')
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[Documents]  WITH CHECK ADD  CONSTRAINT [FK_Documents_Policy] FOREIGN KEY([PolicyId])
REFERENCES [dbo].[Policy] ([PolicyID])
GO
ALTER TABLE [dbo].[Documents] CHECK CONSTRAINT [FK_Documents_Policy]
GO
ALTER TABLE [dbo].[Policy]  WITH CHECK ADD  CONSTRAINT [FK_Policy_PolicyType] FOREIGN KEY([PolicyType])
REFERENCES [dbo].[PolicyType] ([PolicyTypeID])
GO
ALTER TABLE [dbo].[Policy] CHECK CONSTRAINT [FK_Policy_PolicyType]
GO
ALTER TABLE [dbo].[PolicyHistory]  WITH CHECK ADD  CONSTRAINT [FK_PolicyHistory_Policy] FOREIGN KEY([PolicyID])
REFERENCES [dbo].[Policy] ([PolicyID])
GO
ALTER TABLE [dbo].[PolicyHistory] CHECK CONSTRAINT [FK_PolicyHistory_Policy]
GO
USE [master]
GO
ALTER DATABASE [Insurance] SET  READ_WRITE 
GO
