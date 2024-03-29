USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[GetCartItem]    Script Date: 10/2/2021 9:23:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[GetCartItem]
 @userId int

as
begin
    select Books.BookId,BookName,AuthorName,Price,OriginalPrice,CartId,CartList.BookCount,Books.BookCount,Image,CartList.Userid 
	from Books 
	inner join CartList 
	on CartList.BookId=Books.BookId where CartList.Userid=@userId
end