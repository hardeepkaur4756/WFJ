CREATE TABLE [dbo].[WebPages] (
    [ID]       INT           IDENTITY (1, 1) NOT NULL,
    [PageName] NVARCHAR (50) NULL,
    [PageText] NTEXT         NULL,
    CONSTRAINT [PK_WebPages] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[WebPages] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[WebPages] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[WebPages] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[WebPages] TO [wfjuser]
    AS [dbo];



