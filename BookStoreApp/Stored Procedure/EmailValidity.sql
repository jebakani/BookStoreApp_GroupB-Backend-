USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[EmailValidity]    Script Date: 9/28/2021 11:58:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[EmailValidity]
    @EmailId Varchar(200),
	@userId INT OUTPUT,
	@result INT OUTPUT
AS
BEGIN
Begin Try
     IF(EXISTS(SELECT * FROM Users WHERE EmailId=@EmailId))
	  begin
	    set @result=1
	    select @userId=userId from Users where EmailId=@EmailId
	 end
	 else
	   begin
	    set @result=0
		set @userId=0;
	   end
end try
begin Catch
	    set @result=0
end catch
End