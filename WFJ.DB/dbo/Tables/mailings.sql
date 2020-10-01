CREATE TABLE [dbo].[mailings] (
    [mailingID]          INT           IDENTITY (1, 1) NOT NULL,
    [mailingName]        VARCHAR (100) NULL,
    [mailingAdID]        INT           NOT NULL,
    [mailingListID]      INT           NOT NULL,
    [distributionListID] INT           NULL,
    [dateSent]           DATETIME      NULL,
    [mailingFrom]        VARCHAR (100) NULL,
    [mailingReplyTo]     VARCHAR (100) NULL,
    [mailingSubject]     VARCHAR (200) NULL,
    [mailingAttachment]  VARCHAR (100) NULL,
    CONSTRAINT [PK_mailings] PRIMARY KEY CLUSTERED ([mailingID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[mailings] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[mailings] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[mailings] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[mailings] TO [wfjuser]
    AS [dbo];



