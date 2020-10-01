CREATE TABLE [dbo].[Regions] (
    [ID]         INT           IDENTITY (1, 1) NOT NULL,
    [RegionName] NVARCHAR (50) NULL,
    [ClientID]   INT           NULL,
    [DivisionID] INT           NULL,
    CONSTRAINT [PK_Regions] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);




GO
CREATE NONCLUSTERED INDEX [IX_Regions_1]
    ON [dbo].[Regions]([DivisionID] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_Regions]
    ON [dbo].[Regions]([ClientID] ASC) WITH (FILLFACTOR = 90);


GO
GRANT UPDATE
    ON OBJECT::[dbo].[Regions] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[Regions] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[Regions] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[Regions] TO [wfjuser]
    AS [dbo];



