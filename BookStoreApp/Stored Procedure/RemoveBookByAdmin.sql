USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[RemoveBookByAdmin]    Script Date: 10/7/2021 8:39:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[RemoveBookByAdmin]
@BookId INT 
AS
BEGIN
BEGIN TRY
Delete FROM Books Where Bookid=@BookId  
END TRY
BEGIN CATCH
END CATCH
END