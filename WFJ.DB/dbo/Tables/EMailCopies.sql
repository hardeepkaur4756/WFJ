CREATE TABLE [dbo].[EMailCopies] (
    [UserID] INT         NOT NULL,
    [Type]   VARCHAR (1) NOT NULL,
    CONSTRAINT [PK_EMailCopies] PRIMARY KEY CLUSTERED ([UserID] ASC, [Type] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[EMailCopies] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[EMailCopies] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[EMailCopies] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[EMailCopies] TO [wfjuser]
    AS [dbo];



