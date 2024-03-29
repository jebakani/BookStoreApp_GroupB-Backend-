USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[AddToCart]    Script Date: 9/29/2021 1:06:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
alter procedure [dbo].[AddToCart]
 @BookId int,
 @UserId int,
 @NoOfBook int,
 @result int output

 as
 begin
  BEGIN TRY
      if Exists(select * from CartList where UserId=@UserId and BookId=@BookId)
	     begin
		   set @result=0;
		 end
	  else
	     begin
		   insert into CartList values (@BookId,@UserId,@NoOfBook)
		   update Books
				   set Books.BookCount=Books.BookCount-1
				   where BookId=@BookId;
		   set @result=1;
		 end
  END TRY
  begin catch
      set @result=0;
  end catch
 end