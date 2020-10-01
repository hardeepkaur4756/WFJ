CREATE TABLE [dbo].[pageContents] (
    [pageContentID] INT NOT NULL,
    [pageID]        INT NULL,
    [areaID]        INT NULL,
    [sequenceID]    INT NULL,
    [elementID]     INT NULL
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[pageContents] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[pageContents] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[pageContents] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[pageContents] TO [wfjuser]
    AS [dbo];



