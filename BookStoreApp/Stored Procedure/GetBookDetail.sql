USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[GetBook]    Script Date: 9/29/2021 10:38:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetBook]
  @BookId int
AS
BEGIN
BEGIN TRY
     IF(EXISTS(SELECT * FROM Books WHERE BookId=@BookId))
	 begin
	   SELECT * FROM Books WHERE BookId=@BookId;
   	 end
	 else
	   THROW  52000, 'Book Not Available', 1;
END TRY
BEGIN CATCH  
       SELECT  
    ERROR_NUMBER() AS ErrorNumber  
    ,ERROR_MESSAGE() AS ErrorMessage;
END CATCH;
End
