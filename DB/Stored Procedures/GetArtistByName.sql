CREATE PROCEDURE [dbo].[GetArtistByName]
	@ArtistName varchar(80)
AS
BEGIN
SELECT [artistID]
		,[dateCreation]
		,[title]
		,[biography]
		,[imageURL]
		,[heroURL]
FROM [dbo].[Artist]
WHERE title LIKE '%' + @ArtistName + '%'
END
