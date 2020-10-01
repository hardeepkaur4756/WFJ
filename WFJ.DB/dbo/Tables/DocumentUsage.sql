CREATE TABLE [dbo].[DocumentUsage] (
    [ID]           INT           IDENTITY (1, 1) NOT NULL,
    [UserID]       INT           NULL,
    [DocumentID]   INT           NULL,
    [DocumentName] VARCHAR (200) NULL,
    [UsageDate]    SMALLDATETIME NULL,
    [IPAddress]    VARCHAR (20)  NULL,
    CONSTRAINT [PK_DocumentUsage] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);




GO
CREATE NONCLUSTERED INDEX [IX_DocumentUsage]
    ON [dbo].[DocumentUsage]([DocumentID] ASC) WITH (FILLFACTOR = 90);


GO
GRANT UPDATE
    ON OBJECT::[dbo].[DocumentUsage] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[DocumentUsage] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[DocumentUsage] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[DocumentUsage] TO [wfjuser]
    AS [dbo];



