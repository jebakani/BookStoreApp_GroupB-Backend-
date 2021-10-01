USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[AddFeedback]    Script Date: 01-10-2021 16:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[AddFeedback]
	
	@BookId int,
	@UserId int ,
	@Rating float,
	@FeedBack varchar (1000) 
	
AS
	BEGIN
		INSERT into CustomerFeedback(
		
		BookId,
		UserId,
		rating,
		Feedback
		)

		values
		(
		@BookId ,
	@UserId  ,
	@Rating,
	@FeedBack 

		)
	END
RETURN 0