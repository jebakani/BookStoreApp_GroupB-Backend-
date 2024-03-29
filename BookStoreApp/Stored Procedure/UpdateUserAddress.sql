USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[UpdateUserDetails]    Script Date: 9/29/2021 9:32:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[UpdateUserDetails]
(
@addressID int,
@address varchar(255),
@city varchar(50),
@state varchar(50),
@type varchar(10),
@result int output
)
AS
BEGIN
BEGIN TRY
       If exists(Select * from UserDetails where AddressId=@addressID)
	    begin
		  UPDATE UserDetails
          SET 
		   address= @address, city = @city,
		   state=@state,
		   type=@type 
		 WHERE AddressId=@addressID;
		 set @result=1;
		  end 
		  else
		  begin
		   set @result=0;
		  end
END TRY
BEGIN CATCH 
set @result=0;
END CATCH
END