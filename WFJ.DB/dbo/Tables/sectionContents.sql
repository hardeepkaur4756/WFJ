CREATE TABLE [dbo].[sectionContents] (
    [sectionContentID] INT NOT NULL,
    [sectionID]        INT NULL,
    [areaID]           INT NULL,
    [sequenceID]       INT NULL,
    [elementID]        INT NULL
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[sectionContents] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[sectionContents] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[sectionContents] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[sectionContents] TO [wfjuser]
    AS [dbo];



