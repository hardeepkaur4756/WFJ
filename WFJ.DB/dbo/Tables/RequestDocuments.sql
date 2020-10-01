CREATE TABLE [dbo].[RequestDocuments] (
    [ID]           INT          IDENTITY (1, 1) NOT NULL,
    [RequestType]  VARCHAR (1)  NULL,
    [RequestID]    INT          NULL,
    [FileName]     VARCHAR (55) NULL,
    [DocumentType] INT          NULL,
    CONSTRAINT [PK_RequestDocuments] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);




GO
CREATE NONCLUSTERED INDEX [IX_RequestDocuments]
    ON [dbo].[RequestDocuments]([RequestID] ASC) WITH (FILLFACTOR = 90);


GO
GRANT UPDATE
    ON OBJECT::[dbo].[RequestDocuments] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[RequestDocuments] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[RequestDocuments] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[RequestDocuments] TO [wfjuser]
    AS [dbo];



