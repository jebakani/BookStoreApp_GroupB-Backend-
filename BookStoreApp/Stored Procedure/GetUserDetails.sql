USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[GetUSerDetails]    Script Date: 9/28/2021 3:40:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[GetUSerDetails]
 
  @userId int

  as
  begin
  begin try 
    if EXISTS(Select * from UserDetails where userId=@userId)
	  begin
	     select * from UserDetails where userId=@userId
		 end
  end try
 begin catch 
      SELECT  
    ERROR_NUMBER() AS ErrorNumber  
    ,ERROR_MESSAGE() AS ErrorMessage;
	
   end catch
   end








