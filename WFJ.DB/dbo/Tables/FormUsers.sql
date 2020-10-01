CREATE TABLE [dbo].[FormUsers] (
    [ID]     INT IDENTITY (1, 1) NOT NULL,
    [FormID] INT NULL,
    [UserID] INT NULL,
    CONSTRAINT [PK_FormUsers] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[FormUsers] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[FormUsers] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[FormUsers] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[FormUsers] TO [wfjuser]
    AS [dbo];



