﻿CREATE TABLE dbo.Album
(
	albumID INT PRIMARY KEY IDENTITY (1, 1),
	dateCreation SMALLDATETIME NOT NULL DEFAULT GETDATE(),
	artistID INT NOT NULL,
	title VARCHAR(255) NOT NULL,
	imageURL VARCHAR(500) NOT NULL,
	[year] INT NOT NULL
)
