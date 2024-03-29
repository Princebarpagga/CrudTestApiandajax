USE [master]
GO
/****** Object:  Database [CURD]    Script Date: 04-03-2024 16:25:36 ******/
CREATE DATABASE [CURD]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CURD', FILENAME = N'C:\Users\princebarpagga\CURD.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CURD_log', FILENAME = N'C:\Users\princebarpagga\CURD_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [CURD] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CURD].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CURD] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CURD] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CURD] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CURD] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CURD] SET ARITHABORT OFF 
GO
ALTER DATABASE [CURD] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [CURD] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CURD] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CURD] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CURD] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CURD] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CURD] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CURD] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CURD] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CURD] SET  ENABLE_BROKER 
GO
ALTER DATABASE [CURD] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CURD] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CURD] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CURD] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CURD] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CURD] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CURD] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CURD] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CURD] SET  MULTI_USER 
GO
ALTER DATABASE [CURD] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CURD] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CURD] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CURD] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CURD] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CURD] SET QUERY_STORE = OFF
GO
USE [CURD]
GO
/****** Object:  Table [dbo].[Departments]    Script Date: 04-03-2024 16:25:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departments](
	[DepartmentId] [int] NOT NULL,
	[DepartmentName] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 04-03-2024 16:25:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmpId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeName] [nvarchar](100) NOT NULL,
	[Salary] [decimal](10, 2) NOT NULL,
	[DepartmentId] [int] NULL,
	[SkillId] [int] NULL,
	[Email] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[EmpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Skills]    Script Date: 04-03-2024 16:25:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Skills](
	[SkillId] [int] NOT NULL,
	[SkillName] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SkillId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblDepartment]    Script Date: 04-03-2024 16:25:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblDepartment](
	[DepId] [int] NOT NULL,
	[DepartmentName] [nvarchar](100) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblQualification]    Script Date: 04-03-2024 16:25:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblQualification](
	[QualifId] [int] NOT NULL,
	[QualificationName] [nvarchar](100) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblStudent]    Script Date: 04-03-2024 16:25:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblStudent](
	[StudentId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[DepId] [int] NOT NULL,
	[QualifId] [int] NOT NULL,
	[Percentage] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_tblStudent] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Departments] ([DepartmentId], [DepartmentName]) VALUES (1, N'Engineering')
INSERT [dbo].[Departments] ([DepartmentId], [DepartmentName]) VALUES (2, N'Marketing')
INSERT [dbo].[Departments] ([DepartmentId], [DepartmentName]) VALUES (3, N'Human Resources')
SET IDENTITY_INSERT [dbo].[Employees] ON 

INSERT [dbo].[Employees] ([EmpId], [EmployeeName], [Salary], [DepartmentId], [SkillId], [Email]) VALUES (98, N'Jane Smith', CAST(60000.00 AS Decimal(10, 2)), 2, 2, N'jane.smith@example.com')
INSERT [dbo].[Employees] ([EmpId], [EmployeeName], [Salary], [DepartmentId], [SkillId], [Email]) VALUES (102, N'Michael Davis', CAST(58000.00 AS Decimal(10, 2)), 1, 2, N'michael.davis@example.com')
INSERT [dbo].[Employees] ([EmpId], [EmployeeName], [Salary], [DepartmentId], [SkillId], [Email]) VALUES (105, N'Emily Anderson', CAST(59000.00 AS Decimal(10, 2)), 1, 3, N'emily.anderson@example.com')
INSERT [dbo].[Employees] ([EmpId], [EmployeeName], [Salary], [DepartmentId], [SkillId], [Email]) VALUES (106, N'James Taylor', CAST(57000.00 AS Decimal(10, 2)), 3, 1, N'james.taylor@example.com')
INSERT [dbo].[Employees] ([EmpId], [EmployeeName], [Salary], [DepartmentId], [SkillId], [Email]) VALUES (107, N'Olivia Thomas', CAST(62000.00 AS Decimal(10, 2)), 2, 2, N'olivia.thomas@example.com')
INSERT [dbo].[Employees] ([EmpId], [EmployeeName], [Salary], [DepartmentId], [SkillId], [Email]) VALUES (111, N'Ava King', CAST(60000.00 AS Decimal(10, 2)), 2, 1, N'ava.king@example.com')
INSERT [dbo].[Employees] ([EmpId], [EmployeeName], [Salary], [DepartmentId], [SkillId], [Email]) VALUES (113, N'Charlotte Hill', CAST(58000.00 AS Decimal(10, 2)), 1, 2, N'charlotte.hill@example.com')
INSERT [dbo].[Employees] ([EmpId], [EmployeeName], [Salary], [DepartmentId], [SkillId], [Email]) VALUES (114, N'Jacob Scott', CAST(59000.00 AS Decimal(10, 2)), 2, 1, N'jacob.scott@example.com')
SET IDENTITY_INSERT [dbo].[Employees] OFF
INSERT [dbo].[Skills] ([SkillId], [SkillName]) VALUES (1, N'Programming')
INSERT [dbo].[Skills] ([SkillId], [SkillName]) VALUES (2, N'Marketing Strategy')
INSERT [dbo].[Skills] ([SkillId], [SkillName]) VALUES (3, N'Recruitment')
INSERT [dbo].[tblDepartment] ([DepId], [DepartmentName]) VALUES (1, N'Engineering')
INSERT [dbo].[tblDepartment] ([DepId], [DepartmentName]) VALUES (2, N'Marketing')
INSERT [dbo].[tblDepartment] ([DepId], [DepartmentName]) VALUES (3, N'Human Resources')
INSERT [dbo].[tblQualification] ([QualifId], [QualificationName]) VALUES (1, N'Btech')
INSERT [dbo].[tblQualification] ([QualifId], [QualificationName]) VALUES (2, N'Bca')
INSERT [dbo].[tblQualification] ([QualifId], [QualificationName]) VALUES (3, N'Ba')
INSERT [dbo].[tblQualification] ([QualifId], [QualificationName]) VALUES (4, N'Ma')
SET IDENTITY_INSERT [dbo].[tblStudent] ON 

INSERT [dbo].[tblStudent] ([StudentId], [FirstName], [LastName], [DepId], [QualifId], [Percentage]) VALUES (1, N'Pankaj', N'Sharma', 1, 4, CAST(85.00 AS Decimal(18, 2)))
INSERT [dbo].[tblStudent] ([StudentId], [FirstName], [LastName], [DepId], [QualifId], [Percentage]) VALUES (2, N'Pankaj', N'Kumar', 2, 2, CAST(85.00 AS Decimal(18, 2)))
INSERT [dbo].[tblStudent] ([StudentId], [FirstName], [LastName], [DepId], [QualifId], [Percentage]) VALUES (5, N'Aman', N'Kumar', 1, 4, CAST(77.00 AS Decimal(18, 2)))
INSERT [dbo].[tblStudent] ([StudentId], [FirstName], [LastName], [DepId], [QualifId], [Percentage]) VALUES (6, N'Ankit', N'Dogra', 1, 4, CAST(85.00 AS Decimal(18, 2)))
INSERT [dbo].[tblStudent] ([StudentId], [FirstName], [LastName], [DepId], [QualifId], [Percentage]) VALUES (13, N'Prince', N'Barpagga', 1, 2, CAST(68.00 AS Decimal(18, 2)))
INSERT [dbo].[tblStudent] ([StudentId], [FirstName], [LastName], [DepId], [QualifId], [Percentage]) VALUES (19, N'Rahul', N'Kumar', 1, 1, CAST(89.00 AS Decimal(18, 2)))
INSERT [dbo].[tblStudent] ([StudentId], [FirstName], [LastName], [DepId], [QualifId], [Percentage]) VALUES (20, N'Ankit', N'Kumar', 3, 4, CAST(78.00 AS Decimal(18, 2)))
INSERT [dbo].[tblStudent] ([StudentId], [FirstName], [LastName], [DepId], [QualifId], [Percentage]) VALUES (22, N'Raman', N'Kumar', 2, 3, CAST(85.00 AS Decimal(18, 2)))
INSERT [dbo].[tblStudent] ([StudentId], [FirstName], [LastName], [DepId], [QualifId], [Percentage]) VALUES (24, N'Rohit', N'Kumar', 3, 2, CAST(85.00 AS Decimal(18, 2)))
INSERT [dbo].[tblStudent] ([StudentId], [FirstName], [LastName], [DepId], [QualifId], [Percentage]) VALUES (25, N'Johny', N'Kumar', 1, 2, CAST(89.00 AS Decimal(18, 2)))
INSERT [dbo].[tblStudent] ([StudentId], [FirstName], [LastName], [DepId], [QualifId], [Percentage]) VALUES (26, N'Deppak', N'Kumar', 1, 2, CAST(78.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[tblStudent] OFF
USE [master]
GO
ALTER DATABASE [CURD] SET  READ_WRITE 
GO
