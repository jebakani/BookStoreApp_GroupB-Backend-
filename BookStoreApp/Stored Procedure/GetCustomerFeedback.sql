USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[GetCustomerFeedback]    Script Date: 01-10-2021 12:02:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[GetCustomerFeedback]
   @bookid int
 as
 begin
    select Users.userId,FullName,Feedback,Rating
	from Users 
	inner join CustomerFeedback 
	on CustomerFeedback.UserId=Users.userId where CustomerFeedback.BookId=@bookid
 end