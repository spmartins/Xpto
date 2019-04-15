USE [master]
GO

CREATE DATABASE [XPTO]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'XPTO', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\XPTO.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'XPTO_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\XPTO_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [XPTO] SET COMPATIBILITY_LEVEL = 120
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [XPTO].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [XPTO] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [XPTO] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [XPTO] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [XPTO] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [XPTO] SET ARITHABORT OFF 
GO

ALTER DATABASE [XPTO] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [XPTO] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [XPTO] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [XPTO] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [XPTO] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [XPTO] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [XPTO] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [XPTO] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [XPTO] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [XPTO] SET  DISABLE_BROKER 
GO

ALTER DATABASE [XPTO] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [XPTO] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [XPTO] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [XPTO] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [XPTO] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [XPTO] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [XPTO] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [XPTO] SET RECOVERY FULL 
GO

ALTER DATABASE [XPTO] SET  MULTI_USER 
GO

ALTER DATABASE [XPTO] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [XPTO] SET DB_CHAINING OFF 
GO

ALTER DATABASE [XPTO] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [XPTO] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO

ALTER DATABASE [XPTO] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [XPTO] SET  READ_WRITE 
GO


