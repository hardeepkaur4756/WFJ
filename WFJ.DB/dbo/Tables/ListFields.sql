CREATE TABLE [dbo].[ListFields] (
    [ID]      INT IDENTITY (1, 1) NOT NULL,
    [UserID]  INT NULL,
    [FormID]  INT NULL,
    [SeqNo]   INT NULL,
    [FieldID] INT NULL,
    CONSTRAINT [PK_ListFields] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[ListFields] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[ListFields] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[ListFields] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[ListFields] TO [wfjuser]
    AS [dbo];



