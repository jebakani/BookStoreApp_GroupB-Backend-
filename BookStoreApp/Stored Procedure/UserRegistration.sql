USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[InsertIntoUsers]    Script Date: 9/27/2021 4:15:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[InsertIntoUsers]
	
	@FullName varchar(255),
	@EmailId varchar(255) ,
	@Password varchar(255) ,
	@Phone BigInt
AS
	BEGIN
		INSERT into Users(
		
		FullName,
		EmailId,
		Password,
		Phone)

		values
		(
		@FullName ,
		@EmailId  ,
		@Password  ,
		@Phone 
		)
	END
