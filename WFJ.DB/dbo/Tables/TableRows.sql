CREATE TABLE [dbo].[TableRows] (
    [TableRowID]   INT          IDENTITY (1, 1) NOT NULL,
    [QuestionID]   INT          NULL,
    [RowNbr]       VARCHAR (6)  NULL,
    [RowTitle]     VARCHAR (50) NULL,
    [SeqNo]        INT          NULL,
    [NumberOfRows] INT          NULL,
    CONSTRAINT [PK_TableRows] PRIMARY KEY CLUSTERED ([TableRowID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[TableRows] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[TableRows] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[TableRows] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[TableRows] TO [wfjuser]
    AS [dbo];



