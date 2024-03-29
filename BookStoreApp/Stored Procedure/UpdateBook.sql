USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[UpdateBook]    Script Date: 10/6/2021 10:21:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[UpdateBook]
   @BookId int,
  @originalPrice int,
  @BookName varchar(250),
  @AuthorName varchar(250),
  @Price int,
  @BookDescription varchar(500),
  @Image varchar(100),
  @Rating int,
   @BookCount int,
   @result int output
AS
	BEGIN
	if Exists(select * from Books where BookId=@BookId)
	 begin
		Update  Books 
		set
		 BookName =@BookName,
   AuthorName=@AuthorName,
  Price = @Price,
  BookDescription=@BookDescription,
  Image =@Image,
  Rating =@Rating,
  BookCount=@BookCount,
  OriginalPrice=@originalPrice
  where BookId=@BookId;
  set @result=1
  end
  else
  begin
   set @result=0;
   end
END