USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[InsertIntoWishList]    Script Date: 9/29/2021 12:44:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[InsertIntoWishList]
@UserId INT ,
@BookId INT ,
@result int output
AS
BEGIN
BEGIN TRY
INSERT INTO WishList(
UserId,
BookId)
VALUES(
@UserId  ,
@BookId 
);
set @result=1;
END TRY
BEGIN CATCH
   set @result=0;
END CATCH
END