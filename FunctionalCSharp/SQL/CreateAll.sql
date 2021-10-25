USE [master]
GO
/****** Object:  Database [Example]    Script Date: 10/25/2021 6:05:20 PM ******/
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'Example')
BEGIN
CREATE DATABASE [Example]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Example', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Example.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Example_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Example_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
END
GO
ALTER DATABASE [Example] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Example].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Example] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Example] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Example] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Example] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Example] SET ARITHABORT OFF 
GO
ALTER DATABASE [Example] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Example] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Example] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Example] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Example] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Example] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Example] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Example] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Example] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Example] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Example] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Example] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Example] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Example] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Example] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Example] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Example] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Example] SET RECOVERY FULL 
GO
ALTER DATABASE [Example] SET  MULTI_USER 
GO
ALTER DATABASE [Example] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Example] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Example] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Example] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Example] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Example', N'ON'
GO
ALTER DATABASE [Example] SET QUERY_STORE = OFF
GO
USE [Example]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 10/25/2021 6:05:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Employee]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Employee](
	[EmployeeId] [bigint] IDENTITY(1,1) NOT NULL,
	[EmployeeName] [nvarchar](50) NOT NULL,
	[EmployeeEmail] [nvarchar](50) NOT NULL,
	[DateHired] [datetime] NOT NULL,
	[Salary] [money] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[ManagedByEmployeeId] [bigint] NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[EmployeeData]    Script Date: 10/25/2021 6:05:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeData]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EmployeeData](
	[EmployeeDataId] [bigint] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [bigint] NOT NULL,
	[EmployeeDataTypeId] [bigint] NOT NULL,
	[EmployeeDataValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_EmployeeData] PRIMARY KEY CLUSTERED 
(
	[EmployeeDataId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[EmployeeDataType]    Script Date: 10/25/2021 6:05:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeDataType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EmployeeDataType](
	[EmployeeDataTypeId] [bigint] NOT NULL,
	[EmployeeDataTypeName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_EmployeeDataType] PRIMARY KEY CLUSTERED 
(
	[EmployeeDataTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Employee]    Script Date: 10/25/2021 6:05:20 PM ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Employee]') AND name = N'IX_Employee')
CREATE UNIQUE NONCLUSTERED INDEX [IX_Employee] ON [dbo].[Employee]
(
	[EmployeeEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeData]    Script Date: 10/25/2021 6:05:20 PM ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeData]') AND name = N'IX_EmployeeData')
CREATE UNIQUE NONCLUSTERED INDEX [IX_EmployeeData] ON [dbo].[EmployeeData]
(
	[EmployeeId] ASC,
	[EmployeeDataTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Employee_Employee]') AND parent_object_id = OBJECT_ID(N'[dbo].[Employee]'))
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Employee] FOREIGN KEY([ManagedByEmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Employee_Employee]') AND parent_object_id = OBJECT_ID(N'[dbo].[Employee]'))
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Employee]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EmployeeData_Employee]') AND parent_object_id = OBJECT_ID(N'[dbo].[EmployeeData]'))
ALTER TABLE [dbo].[EmployeeData]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeData_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EmployeeData_Employee]') AND parent_object_id = OBJECT_ID(N'[dbo].[EmployeeData]'))
ALTER TABLE [dbo].[EmployeeData] CHECK CONSTRAINT [FK_EmployeeData_Employee]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EmployeeData_EmployeeDataType]') AND parent_object_id = OBJECT_ID(N'[dbo].[EmployeeData]'))
ALTER TABLE [dbo].[EmployeeData]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeData_EmployeeDataType] FOREIGN KEY([EmployeeDataTypeId])
REFERENCES [dbo].[EmployeeDataType] ([EmployeeDataTypeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EmployeeData_EmployeeDataType]') AND parent_object_id = OBJECT_ID(N'[dbo].[EmployeeData]'))
ALTER TABLE [dbo].[EmployeeData] CHECK CONSTRAINT [FK_EmployeeData_EmployeeDataType]
GO
USE [master]
GO
ALTER DATABASE [Example] SET  READ_WRITE 
GO
