CREATE TABLE [dbo].[enews_clients] (
    [enews_clientID] INT          NOT NULL,
    [clientName]     VARCHAR (50) NULL
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[enews_clients] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[enews_clients] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[enews_clients] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[enews_clients] TO [wfjuser]
    AS [dbo];



