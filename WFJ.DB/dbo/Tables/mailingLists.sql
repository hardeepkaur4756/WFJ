CREATE TABLE [dbo].[mailingLists] (
    [mailingListID]       INT          IDENTITY (1, 1) NOT NULL,
    [clientID]            INT          NULL,
    [mailingListName]     VARCHAR (50) NULL,
    [mailingListFilename] VARCHAR (50) NULL,
    [uploadDate]          DATETIME     NULL,
    [emailColumn]         INT          NULL,
    [skipFirstColumn]     TINYINT      NULL,
    CONSTRAINT [PK_mailingLists] PRIMARY KEY CLUSTERED ([mailingListID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[mailingLists] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[mailingLists] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[mailingLists] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[mailingLists] TO [wfjuser]
    AS [dbo];



