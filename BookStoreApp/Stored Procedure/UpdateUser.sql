USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[UpdateUser]    Script Date: 9/30/2021 10:04:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[UpdateUser]
(
    @userId int,
    @FullName varchar(255),
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
END CATCH
END