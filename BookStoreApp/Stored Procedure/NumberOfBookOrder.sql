USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[EditNumberOfBooks]    Script Date: 9/29/2021 2:23:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[EditNumberOfBooks]
 @CartId int,
 @BookId int,
 @Count int,
 @result int output

 as
 begin
  BEGIN TRY
      if Exists(select * from CartList where CartId=@CartId)
	     begin
		   update CartList
		   set BookCount=@Count
		   where CartId=@CartId and @Count<=(select BookCount from Books where BookId=@BookId)
		   set @result=1;
		 end
	  else
	     begin
		   set @result=0;
		 end
  END TRY
  begin catch
      set @result=0;
  end catch
 end