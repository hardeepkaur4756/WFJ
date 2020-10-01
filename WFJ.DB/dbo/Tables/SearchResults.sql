CREATE TABLE [dbo].[SearchResults] (
    [ID]              INT           IDENTITY (1, 1) NOT NULL,
    [SessionID]       INT           NULL,
    [LinkURL]         VARCHAR (150) NULL,
    [LinkDescription] VARCHAR (150) NULL,
    [DateField]       SMALLDATETIME NULL,
    [GroupName]       VARCHAR (50)  NULL,
    [GroupSeqNo]      INT           NULL,
    [RecordDate]      SMALLDATETIME NULL,
    CONSTRAINT [PK_SearchResults] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[SearchResults] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[SearchResults] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[SearchResults] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[SearchResults] TO [wfjuser]
    AS [dbo];



