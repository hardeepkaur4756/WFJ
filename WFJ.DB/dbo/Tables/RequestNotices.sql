CREATE TABLE [dbo].[RequestNotices] (
    [RequestNoticeID] INT            IDENTITY (1, 1) NOT NULL,
    [RequestID]       INT            NULL,
    [NoticeDate]      DATETIME       NULL,
    [RecipientList]   VARCHAR (500)  NULL,
    [NoticeSubject]   VARCHAR (250)  NULL,
    [NoticeText]      VARCHAR (1500) NULL,
    CONSTRAINT [PK_RequestNotices] PRIMARY KEY CLUSTERED ([RequestNoticeID] ASC) WITH (FILLFACTOR = 90)
);




GO
CREATE NONCLUSTERED INDEX [IX_RequestNotices]
    ON [dbo].[RequestNotices]([RequestID] ASC) WITH (FILLFACTOR = 90);


GO
GRANT UPDATE
    ON OBJECT::[dbo].[RequestNotices] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[RequestNotices] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[RequestNotices] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[RequestNotices] TO [wfjuser]
    AS [dbo];



