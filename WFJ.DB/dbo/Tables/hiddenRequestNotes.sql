CREATE TABLE [dbo].[hiddenRequestNotes] (
    [hiddenRequestNoteID] INT IDENTITY (1, 1) NOT NULL,
    [userID]              INT NULL,
    [requestID]           INT NULL,
    [noteID]              INT NULL,
    CONSTRAINT [PK_hiddenRequestNotes] PRIMARY KEY CLUSTERED ([hiddenRequestNoteID] ASC)
);




GO
CREATE NONCLUSTERED INDEX [IX_hiddenRequestNotes_1]
    ON [dbo].[hiddenRequestNotes]([noteID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_hiddenRequestNotes]
    ON [dbo].[hiddenRequestNotes]([userID] ASC);


GO
GRANT UPDATE
    ON OBJECT::[dbo].[hiddenRequestNotes] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[hiddenRequestNotes] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[hiddenRequestNotes] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[hiddenRequestNotes] TO [wfjuser]
    AS [dbo];



