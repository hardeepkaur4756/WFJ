CREATE TABLE [dbo].[FormNotesUsers] (
    [FormNotesUserID] INT IDENTITY (1, 1) NOT NULL,
    [FormID]          INT NULL,
    [UserID]          INT NULL,
    [SeqNo]           INT NULL,
    CONSTRAINT [PK_FormNotesUsers] PRIMARY KEY CLUSTERED ([FormNotesUserID] ASC)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[FormNotesUsers] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[FormNotesUsers] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[FormNotesUsers] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[FormNotesUsers] TO [wfjuser]
    AS [dbo];



