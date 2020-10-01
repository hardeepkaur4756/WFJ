CREATE TABLE [dbo].[mailingAds] (
    [mailingAdID]    INT          IDENTITY (1, 1) NOT NULL,
    [enews_clientID] INT          NULL,
    [mailingAdName]  VARCHAR (50) NULL,
    [mailingAdHTML]  TEXT         NULL,
    CONSTRAINT [PK_mailingAds] PRIMARY KEY CLUSTERED ([mailingAdID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[mailingAds] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[mailingAds] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[mailingAds] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[mailingAds] TO [wfjuser]
    AS [dbo];



