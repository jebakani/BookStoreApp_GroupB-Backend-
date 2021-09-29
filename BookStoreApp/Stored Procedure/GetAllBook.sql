USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[GetAllBooks]    Script Date: 9/29/2021 9:52:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter PROC [dbo].[GetAllBooks]
AS
BEGIN
BEGIN TRY
Select * from Books
END TRY
BEGIN CATCH
END CATCH
END