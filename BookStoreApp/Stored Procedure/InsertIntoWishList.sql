USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[InsertIntoWishList]    Script Date: 9/30/2021 11:33:27 AM ******/
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
  if Exists(select * from WishList where UserId=@UserId and BookId=@BookId)
	     begin
		   set @result=0;
		 end
	  else
	     begin
		   INSERT INTO WishList(
UserId,
BookId)
VALUES(
@UserId  ,
@BookId 
)
set @result=1;
		 end

END TRY
BEGIN CATCH
		   set @result=0;
END CATCH
END