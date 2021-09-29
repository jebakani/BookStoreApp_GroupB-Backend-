USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[UpdatePassword]    Script Date: 9/28/2021 12:06:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[UpdatePassword](@UserId INT,  @NewPassword varchar(20))
AS
BEGIN
BEGIN TRY
UPDATE Users 
SET Users.Password = @NewPassword where Users.userId = @UserId
END TRY
BEGIN CATCH
END CATCH
END