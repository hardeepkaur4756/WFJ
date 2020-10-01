CREATE TABLE [dbo].[elements] (
    [elementID]         INT            NOT NULL,
    [elementName]       VARCHAR (100)  NULL,
    [typeID]            INT            NULL,
    [elementCategoryID] INT            NULL,
    [starttime]         DATETIME       NULL,
    [endtime]           DATETIME       NULL,
    [createDate]        DATETIME       NULL,
    [updateDate]        DATETIME       NULL,
    [SQLQuery]          VARCHAR (8000) NULL,
    [authorID]          INT            NULL,
    [draftExists]       INT            NULL,
    [draftLocked]       INT            NULL,
    [lockedElement]     TINYINT        NULL,
    [coreElement]       TINYINT        NULL,
    [elementValue]      TEXT           NULL,
    [elementDraftValue] TEXT           NULL
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[elements] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[elements] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[elements] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[elements] TO [wfjuser]
    AS [dbo];



