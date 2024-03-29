USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[RemoveBookByAdmin]    Script Date: 10/8/2021 10:36:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[RemoveBookByAdmin]
@BookId INT ,
@result int output
AS
BEGIN
BEGIN TRY
SET XACT_ABORT on;
BEGIN TRANSACTION
    if exists(select * from Books where BookId=@BookId)
	begin
       Delete FROM Books Where Bookid=@BookId;
	   set @result=1;
	end
	else
	 begin
	   set @result=0;
	 end
COMMIT TRANSACTION;	
END TRY
BEGIN CATCH
IF(XACT_STATE()) = -1
	BEGIN
		ROLLBACK TRANSACTION;
		set @result=0;
	END;
ELSE IF(XACT_STATE()) = 1
	BEGIN
		PRINT
		'transaction is commitable' + ' commiting back transaction'
		COMMIT TRANSACTION;
		set @result=1;
	END;
END CATCH
END