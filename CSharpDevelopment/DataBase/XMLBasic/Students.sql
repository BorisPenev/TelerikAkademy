USE [master]
GO
/****** Object:  Database [XMLStudents]    Script Date: 24.7.2013 г. 19:23:17 ******/
CREATE DATABASE [XMLStudents]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'XMLStudents', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\XMLStudents.mdf' , SIZE = 4160KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'XMLStudents_log', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\XMLStudents_log.ldf' , SIZE = 1040KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [XMLStudents] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [XMLStudents].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [XMLStudents] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [XMLStudents] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [XMLStudents] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [XMLStudents] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [XMLStudents] SET ARITHABORT OFF 
GO
ALTER DATABASE [XMLStudents] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [XMLStudents] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [XMLStudents] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [XMLStudents] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [XMLStudents] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [XMLStudents] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [XMLStudents] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [XMLStudents] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [XMLStudents] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [XMLStudents] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [XMLStudents] SET  DISABLE_BROKER 
GO
ALTER DATABASE [XMLStudents] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [XMLStudents] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [XMLStudents] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [XMLStudents] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [XMLStudents] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [XMLStudents] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [XMLStudents] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [XMLStudents] SET RECOVERY FULL 
GO
ALTER DATABASE [XMLStudents] SET  MULTI_USER 
GO
ALTER DATABASE [XMLStudents] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [XMLStudents] SET DB_CHAINING OFF 
GO
ALTER DATABASE [XMLStudents] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [XMLStudents] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'XMLStudents', N'ON'
GO
USE [XMLStudents]
GO
/****** Object:  Table [dbo].[Exams]    Script Date: 24.7.2013 г. 19:23:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Exams](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Tutor] [nvarchar](50) NOT NULL,
	[Score] [float] NOT NULL,
 CONSTRAINT [PK_Exams] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Student_Exams]    Script Date: 24.7.2013 г. 19:23:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student_Exams](
	[StudentID] [int] NOT NULL,
	[ExamID] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Students]    Script Date: 24.7.2013 г. 19:23:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Students](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](50) NOT NULL,
	[Sex] [nchar](1) NOT NULL,
	[Birthday] [datetime] NOT NULL,
	[Phone] [nvarchar](30) NOT NULL,
	[eMail] [varchar](50) NOT NULL,
	[Course] [int] NOT NULL,
	[Specialty] [nvarchar](100) NOT NULL,
	[FacultyNumber] [int] NOT NULL,
 CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Exams] ON 

INSERT [dbo].[Exams] ([ID], [Name], [Tutor], [Score]) VALUES (1, N'Math', N'phd Pesho', 12.12)
INSERT [dbo].[Exams] ([ID], [Name], [Tutor], [Score]) VALUES (2, N'Singing', N'Mozart', 120.03)
SET IDENTITY_INSERT [dbo].[Exams] OFF
INSERT [dbo].[Student_Exams] ([StudentID], [ExamID]) VALUES (1, 1)
INSERT [dbo].[Student_Exams] ([StudentID], [ExamID]) VALUES (1, 2)
INSERT [dbo].[Student_Exams] ([StudentID], [ExamID]) VALUES (2, 2)
SET IDENTITY_INSERT [dbo].[Students] ON 

INSERT [dbo].[Students] ([ID], [Name], [Sex], [Birthday], [Phone], [eMail], [Course], [Specialty], [FacultyNumber]) VALUES (1, N'Gosho Studentcheto                                ', N'M', CAST(0x0000734800000000 AS DateTime), N'088765432', N'piyandeto@gmail.com', 4, N'Mathematics', 1234)
INSERT [dbo].[Students] ([ID], [Name], [Sex], [Birthday], [Phone], [eMail], [Course], [Specialty], [FacultyNumber]) VALUES (2, N'Pesho Vrytkata                                    ', N'F', CAST(0x000081A100000000 AS DateTime), N'076543456', N'ringroad@abv.bg', 2, N'Vrytkane', 4321)
SET IDENTITY_INSERT [dbo].[Students] OFF
/****** Object:  Index [IX_FacNumber]    Script Date: 24.7.2013 г. 19:23:17 ******/
ALTER TABLE [dbo].[Students] ADD  CONSTRAINT [IX_FacNumber] UNIQUE NONCLUSTERED 
(
	[FacultyNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Student_Exams]  WITH CHECK ADD  CONSTRAINT [FK_Student_Exams_Exams] FOREIGN KEY([ExamID])
REFERENCES [dbo].[Exams] ([ID])
GO
ALTER TABLE [dbo].[Student_Exams] CHECK CONSTRAINT [FK_Student_Exams_Exams]
GO
ALTER TABLE [dbo].[Student_Exams]  WITH CHECK ADD  CONSTRAINT [FK_Student_Exams_Students] FOREIGN KEY([StudentID])
REFERENCES [dbo].[Students] ([ID])
GO
ALTER TABLE [dbo].[Student_Exams] CHECK CONSTRAINT [FK_Student_Exams_Students]
GO
USE [master]
GO
ALTER DATABASE [XMLStudents] SET  READ_WRITE 
GO
