USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[RemoveFromWishList]    Script Date: 9/29/2021 12:44:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[RemoveFromWishList]
@WishListId INT ,
@result int output
AS
BEGIN
BEGIN TRY
Delete FROM WishList Where WishListId =@WishListId;
set @result=1;
END TRY
BEGIN CATCH
set @result=0;
END CATCH
END