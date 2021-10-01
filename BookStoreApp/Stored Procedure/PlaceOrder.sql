ALTER procedure [dbo].[PlaceTheOrder]
 @BookId int,
 @UserId int,
 @OrderDate varchar(20),
 @CartId int,
 @result int output

 as
 begin
  BEGIN TRY
  BEGIN TRAN
	     begin
		   insert into Orders values (@BookId,@UserId,@OrderDate)
		   set @result=1
		   end
	    if(@result=1)
	      begin
	       delete from CartList where CartId=@CartId
		  Commit Tran
		end
  END TRY
  begin catch
      set @result=0;
	  Rollback Tran
  end catch
 end