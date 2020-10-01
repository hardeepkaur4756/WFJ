CREATE TABLE [dbo].[PersonnelRequests] (
    [ID]            INT          IDENTITY (1, 1) NOT NULL,
    [RequestID]     INT          NULL,
    [FirmID]        INT          NULL,
    [AssociateName] VARCHAR (50) NULL,
    [Telephone]     VARCHAR (20) NULL,
    [PersonnelID]   INT          NULL,
    CONSTRAINT [PK_PersonnelRequests] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);




GO
CREATE NONCLUSTERED INDEX [IX_PersonnelRequests_1]
    ON [dbo].[PersonnelRequests]([RequestID] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_PersonnelRequests]
    ON [dbo].[PersonnelRequests]([PersonnelID] ASC) WITH (FILLFACTOR = 90);


GO
GRANT UPDATE
    ON OBJECT::[dbo].[PersonnelRequests] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[PersonnelRequests] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[PersonnelRequests] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[PersonnelRequests] TO [wfjuser]
    AS [dbo];



