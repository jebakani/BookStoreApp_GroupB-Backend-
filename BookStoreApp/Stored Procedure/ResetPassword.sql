USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[UpdatePassword]    Script Date: 9/28/2021 12:06:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[UpdatePassword]
(@UserId INT,  @NewPassword varchar(20),@result int output)
AS
BEGIN
BEGIN TRY
UPDATE Users 
SET Users.Password = @NewPassword where Users.userId = @UserId;
set @result=1;
END TRY
BEGIN CATCH
set @result=0;
END CATCH
END