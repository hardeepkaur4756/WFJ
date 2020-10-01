CREATE TABLE [dbo].[mailingListNames] (
    [mailingListNameID] INT          IDENTITY (1, 1) NOT NULL,
    [mailingListID]     INT          NOT NULL,
    [mailingListEmail]  VARCHAR (50) NOT NULL,
    [status]            TINYINT      NULL,
    CONSTRAINT [PK_mailingListNames] PRIMARY KEY CLUSTERED ([mailingListNameID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[mailingListNames] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[mailingListNames] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[mailingListNames] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[mailingListNames] TO [wfjuser]
    AS [dbo];



