USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[GetCartItem]    Script Date: 9/29/2021 2:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[GetCartItem]
 @userId int

as
begin
    select Books.BookId,BookName,AuthorName,Price,OriginalPrice,CartId,CartList.BookCount,Books.BookCount,Image 
	from Books 
	inner join CartList 
	on CartList.BookId=Books.BookId where CartList.Userid=@userId
end