CREATE TABLE [dbo].[formSections] (
    [formSectionID] INT          IDENTITY (1, 1) NOT NULL,
    [sectionName]   VARCHAR (50) NULL,
    [sequenceID]    INT          NULL,
    CONSTRAINT [PK_formSections] PRIMARY KEY CLUSTERED ([formSectionID] ASC)
);

