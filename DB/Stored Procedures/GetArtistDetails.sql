CREATE PROCEDURE [dbo].[GetArtistDetails]
	@artistID INT
AS
BEGIN

	SELECT
		artistID,
		title AS artistName,
		biography,
		imageURL AS artistImageURL,
		heroURL
	FROM dbo.Artist
	WHERE artistID = @artistID;

	SELECT TOP(5)
		a.title AS artistName,
		s.title AS songTitle,
		al.title AS albumTitle,
		s.bpm,
		al.imageURL as albumImageURL
	FROM dbo.Artist a
	INNER JOIN dbo.Song s ON a.artistID = s.artistID
	INNER JOIN dbo.Album al ON s.albumID = al.albumID
	WHERE a.artistID = @artistID
	ORDER BY 2 ASC;

	SELECT 
		al.title AS albumTitle,
		al.imageURL AS albumImageURL, 
		a.title AS artistName
	FROM dbo.Album al
	INNER JOIN dbo.Artist a ON a.artistID = al.artistID
	WHERE a.artistID = @artistID;

END
