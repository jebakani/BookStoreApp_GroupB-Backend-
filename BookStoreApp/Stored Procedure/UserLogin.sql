USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[UserLogin]    Script Date: 10/7/2021 8:08:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[UserLogin]
@EmailId Varchar(200),
	@Password Varchar(50),
	@result INT OUTPUT
AS
BEGIN
BEGIN TRY
   set @result=0;
   
   
	 if(EXISTS(SELECT * FROM Admin WHERE AdminMail=@EmailId))
	 begin
	   if (Exists (select * from Admin Where AdminMail=@EmailId and Passwords=@Password))
	     begin
		  set @result=3 ;
		  select * from Admin Where AdminMail=@EmailId
      	end
	   else
	     begin
		   set @result=2;
		   THROW  52000, 'Incorrect Password', 1;
		 end
   	 end
	 else IF(EXISTS(SELECT * FROM Users WHERE EmailId=@EmailId))
	    begin
	   if (Exists (select * from Users Where EmailId=@EmailId and Password=@Password))
	     begin
		  set @result=1 ;
		  select * from Users where EmailId=@EmailId
      	end
	   else
	     begin
		   set @result=2;
		   THROW  52000, 'Incorrect Password', 1;
		 end
   	 end
	 else
	 begin
	   set @result=4;
	   THROW 52000, 'Invalid Email id', 1;
     end
END TRY
BEGIN CATCH  
   set @result=0;
END CATCH;
End
