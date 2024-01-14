USE [master]
GO
/****** Object:  Database [Jobs]    Script Date: 1/14/2024 14:15:51 ******/
CREATE DATABASE [Jobs]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Jobs', FILENAME = N'D:\Database\Jobs.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Jobs_log', FILENAME = N'D:\Database\Jobs_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Jobs] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Jobs].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Jobs] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Jobs] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Jobs] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Jobs] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Jobs] SET ARITHABORT OFF 
GO
ALTER DATABASE [Jobs] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Jobs] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Jobs] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Jobs] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Jobs] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Jobs] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Jobs] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Jobs] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Jobs] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Jobs] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Jobs] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Jobs] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Jobs] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Jobs] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Jobs] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Jobs] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Jobs] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Jobs] SET RECOVERY FULL 
GO
ALTER DATABASE [Jobs] SET  MULTI_USER 
GO
ALTER DATABASE [Jobs] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Jobs] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Jobs] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Jobs] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Jobs] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Jobs] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Jobs', N'ON'
GO
ALTER DATABASE [Jobs] SET QUERY_STORE = OFF
GO
USE [Jobs]
GO
/****** Object:  Table [dbo].[JobDetail]    Script Date: 1/14/2024 14:16:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobDetail](
	[JobDetailId] [int] IDENTITY(1,1) NOT NULL,
	[JobCode] [varchar](50) NULL,
	[Title] [nvarchar](50) NULL,
	[MinimumQualification] [nvarchar](100) NULL,
	[SortDescription] [nvarchar](250) NULL,
	[LastDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_JobDetail] PRIMARY KEY CLUSTERED 
(
	[JobDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 1/14/2024 14:16:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Username] [varchar](100) NULL,
	[Password] [nvarchar](100) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[JobDetail] ADD  CONSTRAINT [DF_JobDetail_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[JobDetail] ADD  CONSTRAINT [DF_JobDetail_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
/****** Object:  StoredProcedure [dbo].[USP_Add_Users]    Script Date: 1/14/2024 14:16:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[USP_Add_Users]
(
@UserId INT=NULL,
@Name NVARCHAR(100)=NULL,
@Username Varchar(100)=NULL,
@Password Nvarchar(100)=NULL,
@CreatedBy INT=NULL
)
As
Begin
	SET  NOCOUNT ON; 
	if not exists(select UserId from Users where Username=@Username and IsActive=1)
		BEGIN
			Insert into Users
			(
				Name
				,Username
				,Password
				,CreatedBy
			)
			Values
			(
				@Name
				,@Username
				,@Password
				,@CreatedBy
			)
			select @@IDENTITY
		END
	else 
	select -2
End
GO
/****** Object:  StoredProcedure [dbo].[USP_Get_All_Jobs]    Script Date: 1/14/2024 14:16:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[USP_Get_All_Jobs]
As
Begin
Select 
JobDetailId
,JobCode
,Title
,MinimumQualification
,SortDescription
,Convert(varchar(20),LastDate,103)strLastDate
,jd.IsActive
,usr.Name as CreatedByName
from
JobDetail jd
inner join Users usr on usr.UserId=jd.CreatedBy
where jd.IsActive=1
End
GO
/****** Object:  StoredProcedure [dbo].[USP_Get_JobDetail_By_Id]    Script Date: 1/14/2024 14:16:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[USP_Get_JobDetail_By_Id]
(
@jobId Int
)
As
Begin
Select 
JobDetailId,
JobCode,
Title,
MinimumQualification,
SortDescription,
Convert(varchar(20),LastDate,101)strLastDate,
IsActive
from 
JobDetail where JobDetailId=@jobId and IsActive=1
End
GO
/****** Object:  StoredProcedure [dbo].[USP_Get_Users_By_UserName_Password]    Script Date: 1/14/2024 14:16:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[USP_Get_Users_By_UserName_Password]
(
@Username Varchar(100)=NULL,
@Password Nvarchar(100)=NULL
)
As
BEGIN
Select 
UserId,
Username,
Password
from Users where Username=@Username and Password=@Password and IsActive=1
END
GO
/****** Object:  StoredProcedure [dbo].[USP_JobDetail]    Script Date: 1/14/2024 14:16:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[USP_JobDetail]
(
@JobDetailId INT=NULL,
@JobCode Varchar(50)=NULL,
@Title NVARCHAR(50)=NULL,
@MinimumQualification NVARCHAR(100)=NULL,
@SortDescription NVARCHAR(250)=NULL,
@LastDate Datetime=NULL,
@CreatedBy INT=NULL,
@ModifiedBy INT=NULL,
@SpType Int
)
AS
Begin
	if(@SpType=1)
	BEGIN
		if not exists(select JobDetailId from JobDetail where JobCode=@JobCode and IsActive=1)
			BEGIN
				Insert into JobDetail
					(
						JobCode
						,Title
						,MinimumQualification
						,SortDescription
						,LastDate
						,CreatedBy
					)
				Values
					(
						@JobCode
						,@Title
						,@MinimumQualification
						,@SortDescription
						,@LastDate
						,@CreatedBy
					)
				select @@IDENTITY
			END
		else select -2
	END
	if(@SpType=2)
	BEGIN
		Update JobDetail set 
			JobCode=@JobCode,
			Title=@Title,
			MinimumQualification=@MinimumQualification,
			SortDescription=@SortDescription,
			LastDate=@LastDate,
			ModifiedBy=@ModifiedBy,
			ModifiedDate=GETDATE()
			where JobDetailId=@JobDetailId and IsActive=1
			select @@IDENTITY
	END
	if(@SpType=3)
	BEGIN
		Update JobDetail set 
			IsActive=0,
			ModifiedBy=@ModifiedBy,
			ModifiedDate=GETDATE()
			where JobDetailId=@JobDetailId and IsActive=1
	END
End
GO
USE [master]
GO
ALTER DATABASE [Jobs] SET  READ_WRITE 
GO
