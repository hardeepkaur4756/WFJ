CREATE TABLE [dbo].[pictures] (
    [PictureID]       INT           IDENTITY (1, 1) NOT NULL,
    [pictureFileName] VARCHAR (150) NULL,
    CONSTRAINT [PK_pictures] PRIMARY KEY CLUSTERED ([PictureID] ASC)
);

