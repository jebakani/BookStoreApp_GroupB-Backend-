USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[GetAllBooks]    Script Date: 9/29/2021 9:52:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[GetAllBooks]
AS
BEGIN
BEGIN TRY
   Select * from Books
END TRY
BEGIN CATCH
    SELECT  
    ERROR_NUMBER() AS ErrorNumber  
    ,ERROR_MESSAGE() AS ErrorMessage;
END CATCH
END