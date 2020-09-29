CREATE TABLE [dbo].[EventCategories] (
    [EventCategoryID] INT          IDENTITY (1, 1) NOT NULL,
    [EventCategory]   VARCHAR (20) NULL,
    [ImageFileName]   VARCHAR (80) NULL,
    CONSTRAINT [PK_EventCategories] PRIMARY KEY CLUSTERED ([EventCategoryID] ASC)
);

