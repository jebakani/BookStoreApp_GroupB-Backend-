USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[EditNumberOfBooks]    Script Date: 10/9/2021 8:56:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[EditNumberOfBooks]
 @CartId int,
 @type bit,
 @result int output

 as
 begin
  BEGIN TRY
  BEGIN Transaction
  declare @count  int , @bookid int;

      if Exists(select * from CartList where CartId=@CartId)
	     begin
		   select @count=BookCount,@bookid=BookId from CartList where CartId=@CartId; 
		   if(@type=1)		
 begin
			   if exists(select * from Books where Books.BookCount !=0 and BookId=@bookid)
			    begin
					update CartList
				   set BookCount=@count+1
				   where CartId=@CartId;
				   update Books
				   set Books.BookCount=Books.BookCount-1
				   where BookId=@bookid;
				   set @result=1;
				end
				else
				 begin
				  set @result=0;
				 end
			 end
			 else
			   begin
			     update CartList
				   set BookCount=@count-1
				   where CartId=@CartId;
				  update Books
				   set Books.BookCount=Books.BookCount+1
				   where BookId=@bookid;
				   set @result=1;
			   end
			end
	  else
	     begin
		   set @result=0;
		 end
   if(@result=1)
	      begin
		  Commit Tran
		end

  END TRY
  begin catch
  set @result=0;
  Rollback Tran
  end catch
 end