CREATE TABLE [dbo].[Letters] (
    [letterID]         INT           IDENTITY (1, 1) NOT NULL,
    [letterName]       VARCHAR (100) NULL,
    [createDate]       DATETIME      NULL,
    [updateDate]       DATETIME      NULL,
    [letterValue]      TEXT          NULL,
    [letterDraftValue] TEXT          NULL,
    CONSTRAINT [PK_Letters] PRIMARY KEY CLUSTERED ([letterID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[Letters] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[Letters] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[Letters] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[Letters] TO [wfjuser]
    AS [dbo];



