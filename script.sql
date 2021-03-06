USE [master]
GO
/****** Object:  Database [Insurance]    Script Date: 21-04-2017 00:30:38 ******/
CREATE DATABASE [Insurance]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Insurance', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLSERVER\MSSQL\DATA\Insurance.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Insurance_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLSERVER\MSSQL\DATA\Insurance_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Insurance] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Insurance].[dbo].[sp_fulltext_database] @action = 'enable'
end
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
ALTER DATABASE [Insurance] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Insurance] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Insurance] SET DELAYED_DURABILITY = DISABLED 
GO
USE [Insurance]
GO
/****** Object:  Table [dbo].[Documents]    Script Date: 21-04-2017 00:30:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Documents](
	[DocumentId] [int] IDENTITY(1,1) NOT NULL,
	[DocumentName] [varchar](100) NULL,
	[DocumentPath] [varchar](max) NULL,
	[PolicyId] [int] NULL,
 CONSTRAINT [PK_Documents] PRIMARY KEY CLUSTERED 
(
	[DocumentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Policy]    Script Date: 21-04-2017 00:30:38 ******/
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
/****** Object:  Table [dbo].[PolicyHistory]    Script Date: 21-04-2017 00:30:38 ******/
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
/****** Object:  Table [dbo].[PolicyType]    Script Date: 21-04-2017 00:30:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PolicyType](
	[PolicyTypeID] [int] IDENTITY(1,1) NOT NULL,
	[PolicyTypeName] [varchar](150) NULL,
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
/****** Object:  Table [dbo].[Settings]    Script Date: 21-04-2017 00:30:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Settings](
	[Id] [int] NOT NULL,
	[SenderEmail] [varchar](100) NULL,
	[SenderPassword] [varchar](50) NULL,
	[SenderName] [varchar](50) NULL,
	[CredentialEmailID] [varchar](100) NULL,
	[CredentialPassword] [varchar](50) NULL,
	[Mobile] [varchar](15) NULL,
	[SMSUserName] [varchar](50) NULL,
	[SMSPassword] [varchar](50) NULL,
	[SMSSender] [varchar](50) NULL,
	[SMSRoute] [varchar](10) NULL,
	[SMSType] [varchar](10) NULL,
 CONSTRAINT [PK_Settings_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 21-04-2017 00:30:38 ******/
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
