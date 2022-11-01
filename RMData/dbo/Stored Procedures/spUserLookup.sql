CREATE PROCEDURE [dbo].[spUserLookup]
	@Id nvarchar(128)
AS
Begin

	Set nocount on;

	SELECT Id,FirstName,LastName,EmailAddress,CreatedDate
	From [dbo].[User]
	where id = @Id;
end

RETURN 0
