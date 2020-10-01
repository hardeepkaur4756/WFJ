CREATE TABLE [dbo].[FunctionsTable] (
    [ID]             INT            NOT NULL,
    [SeqNo]          INT            NULL,
    [PageName]       NVARCHAR (160) NULL,
    [ParentPageID]   INT            NULL,
    [ButtonFileName] NVARCHAR (60)  NULL,
    [Document]       NVARCHAR (60)  NULL,
    [HasChildren]    TINYINT        NULL,
    [LoginRequired]  TINYINT        NULL,
    [PathNameZ]      NVARCHAR (160) NULL,
    CONSTRAINT [PK_FunctionsTable] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[FunctionsTable] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[FunctionsTable] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[FunctionsTable] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[FunctionsTable] TO [wfjuser]
    AS [dbo];



