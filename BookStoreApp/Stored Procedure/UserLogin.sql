CREATE PROCEDURE [dbo].[UserLogin]
	@EmailId Varchar,
	@Password Varchar
AS
	SELECT @EmailId, @Password
RETURN 0
