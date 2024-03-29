USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[UserDetailsInsert]    Script Date: 9/28/2021 2:34:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[UserDetailsInsert]
(
@address varchar(600),
@city varchar(50),
@state varchar(50),
@type varchar(10),
@userId int,
@result int output
)
AS
BEGIN
BEGIN TRY
INSERT INTO UserDetails(address,city,state,type,userId)
values(@address,@city,@state,@type,@userId);
set @result=1;
END TRY
BEGIN CATCH 
   set @result=0;
END CATCH
END