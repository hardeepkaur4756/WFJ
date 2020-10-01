CREATE TABLE [dbo].[RequestNotes] (
    [ID]               INT           IDENTITY (1, 1) NOT NULL,
    [RequestType]      VARCHAR (1)   NULL,
    [RequestID]        INT           NULL,
    [UserID]           INT           NULL,
    [contactTypeID]    INT           NULL,
    [NotesDate]        SMALLDATETIME NULL,
    [Notes]            TEXT          NULL,
    [FollowupDate]     DATETIME      NULL,
    [ReminderSent]     TINYINT       NULL,
    [dateReminderSent] DATETIME      NULL,
    [SendToAuthorOnly] TINYINT       NULL,
    [flaggedNote]      TINYINT       NULL,
    [deadlineCalendar] TINYINT       NULL,
    [internalNote]     TINYINT       NULL,
    CONSTRAINT [PK_RequestNotes] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);




GO
CREATE NONCLUSTERED INDEX [IX_RequestNotes_2]
    ON [dbo].[RequestNotes]([FollowupDate] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_RequestNotes_1]
    ON [dbo].[RequestNotes]([NotesDate] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_RequestNotes]
    ON [dbo].[RequestNotes]([RequestID] ASC) WITH (FILLFACTOR = 90);


GO
GRANT UPDATE
    ON OBJECT::[dbo].[RequestNotes] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[RequestNotes] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[RequestNotes] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[RequestNotes] TO [wfjuser]
    AS [dbo];



