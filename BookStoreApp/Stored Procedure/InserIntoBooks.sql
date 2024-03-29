USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[InsertBooks]    Script Date: 9/29/2021 9:29:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[InsertBooks]
	@originalPrice int,
  @BookName varchar(250),
  @AuthorName varchar(250),
  @Price int,
  @BookDescription varchar(500),
  @Image varchar(100),
  @Rating int,
   @BookCount int
AS
	BEGIN
		INSERT into Books (
		 BookName ,
   AuthorName ,
  Price ,
  BookDescription,
  Image ,
  Rating ,
  BookCount,
  OriginalPrice)

		values
		(
		 @BookName ,
  @AuthorName ,
  @Price ,
  @BookDescription ,
  @Image ,
  @Rating ,
   @BookCount ,
   @originalPrice
		)
END