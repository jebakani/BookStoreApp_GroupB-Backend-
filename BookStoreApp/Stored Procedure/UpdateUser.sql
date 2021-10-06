USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[UpdateUserDetails]    Script Date: 9/29/2021 10:54:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Alter PROCEDURE [dbo].[UpdateUser]
(
    @userId int,
    @FullName varchar(255),
	@EmailId varchar(255) ,
	@Password varchar(255) ,
	@Phone BigInt,
	@result int output
)
AS
BEGIN
BEGIN TRY
       If exists(Select * from Users where userId=@userId)
	    begin
		  UPDATE Users
          SET 
		   FullName=@FullName,
		   EmailId=@EmailId,
		   Password=@Password,
		   Phone=@Phone
		  WHERE userId=@userId;
		 set @result=1;
		  end 
		  else
		  begin
		   set @result=0;
		  end
END TRY
BEGIN CATCH 
set @result=0;
END CATCH
END