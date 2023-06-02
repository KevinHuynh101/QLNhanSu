CREATE DATABASE QLNS
GO
USE [QLNS]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhanSu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[HoTen] [nvarchar](50) NULL,
	[SoCCCD_CMT] [nvarchar](50) NULL,
	[GioiTinh] [nchar](10) NULL,
	[NgaySinh] [date] NULL,
	[Email] [nvarchar](50) NULL,
	[DiaChi] [nvarchar](50) NULL,
	[Phone] [nvarchar](20) NULL,
	[PhongBan] [nvarchar] (50) NULL,
	[ChucVu] [nvarchar] (50) NULL,
 CONSTRAINT [PK_NhanSu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[TaiKhoan](
	[tendangnhap] [nvarchar](50) NOT NULL,
	[matkhau] [nvarchar](50) NULL,
 CONSTRAINT [PK_TaiKhoan] PRIMARY KEY CLUSTERED 
(
	[tendangnhap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
 



--dang nhap
create proc [dbo].[Login]
@Username nvarchar(20),
@Password nvarchar(20)
as
begin
    if exists (select * from TaiKhoan where tendangnhap = @Username and matkhau = @Password)
        select 1 as code
    else if exists(select * from TaiKhoan where tendangnhap = @Username and matkhau != @Password) 
        select 2 as code
    else select 3 as code
end
--Xoa nhan vien
create proc [dbo].[SP_XoaNhanVien]
@ID int
as
begin
	delete NhanSu where ID = @ID
end
go

--Them nhan vien
create proc [dbo].[SP_ThemNhanVien]
@HoTen nvarchar(50),
@SoCCCD_CMT nvarchar(50),
@GioiTinh nchar(10),
@NgaySinh date,
@Email nvarchar(50),
@DiaChi nvarchar(50),
@Phone nvarchar(20),
@PhongBan nvarchar(50),
@ChucVu nvarchar(50)
as
begin
	insert into NhanSu values (@HoTen,@SoCCCD_CMT, @GioiTinh, @NgaySinh, @Email, @DiaChi, @Phone,@PhongBan,@ChucVu)
end
go
--Sua nhan vien
create proc [dbo].[SP_SuaNhanVien]
@ID int,
@HoTen nvarchar(50),
@SoCCCD_CMT nvarchar(50),
@GioiTinh nchar(10),
@NgaySinh date,
@Email nvarchar(50),
@DiaChi nvarchar(50),
@Phone nvarchar(20),
@PhongBan nvarchar(50),
@ChucVu nvarchar(50)

as
begin
	update NhanSu set
	HoTen = @HoTen,
	SoCCCD_CMT = @SoCCCD_CMT,
	GioiTinh = @GioiTinh,
	NgaySinh = @NgaySinh,
	Email = @Email,
	DiaChi = @DiaChi,
	Phone = @Phone,
	PhongBan = @PhongBan,
	ChucVu = @ChucVu
	

	where ID = @ID
end
go
--Lay DS nhan vien
create proc [dbo].[SP_LayDSNS]
as
begin
	select * from NhanSu
end
go



