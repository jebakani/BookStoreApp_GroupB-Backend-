USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[UserLogin]    Script Date: 9/27/2021 3:52:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[UserLogin]
	@EmailId Varchar,
	@Password Varchar,
	@result INT OUTPUT
AS
BEGIN
BEGIN TRY
   BEGIN TRAN
     IF(EXISTS(SELECT * FROM Users WHERE EmailId=@EmailId))
	 begin
	   if (Exists (select * from Users Where EmailId=@EmailId and Password=@Password))
	     begin
		  set @result=1
		 end
	   else
	     begin
		   set @result=2
		   end
   	 end
	 else
	   begin
	    set @result=3;
	    end
END TRY
Begin Catch
End catch
END
