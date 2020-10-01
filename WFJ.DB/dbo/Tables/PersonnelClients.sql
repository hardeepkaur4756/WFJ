CREATE TABLE [dbo].[PersonnelClients] (
    [ID]              INT     IDENTITY (1, 1) NOT NULL,
    [PersonnelID]     INT     NULL,
    [ClientID]        INT     NULL,
    [NewRequestsFlag] TINYINT NULL,
    CONSTRAINT [PK_PersonnelClients] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);




GO
CREATE NONCLUSTERED INDEX [IX_PersonnelClients_1]
    ON [dbo].[PersonnelClients]([ClientID] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_PersonnelClients]
    ON [dbo].[PersonnelClients]([PersonnelID] ASC) WITH (FILLFACTOR = 90);


GO
GRANT UPDATE
    ON OBJECT::[dbo].[PersonnelClients] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[PersonnelClients] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[PersonnelClients] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[PersonnelClients] TO [wfjuser]
    AS [dbo];



