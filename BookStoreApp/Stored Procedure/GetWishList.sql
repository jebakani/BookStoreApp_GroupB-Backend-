USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[GetWishList]    Script Date: 10/6/2021 8:33:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[GetWishList]
(@userId INT)
AS
BEGIN
select 
Books.BookId,BookName,AuthorName,Price,OriginalPrice,Image,WishListId
FROM Books
inner join Wishlist
on WishList.BookId=Books.BookId where WishList.UserId=@userId
END