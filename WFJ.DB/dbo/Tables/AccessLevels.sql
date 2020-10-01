CREATE TABLE [dbo].[AccessLevels] (
    [ID]          INT           NOT NULL,
    [AccessLevel] NVARCHAR (30) NULL,
    CONSTRAINT [PK_AccessLevels] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[AccessLevels] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[AccessLevels] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[AccessLevels] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[AccessLevels] TO [wfjuser]
    AS [dbo];



