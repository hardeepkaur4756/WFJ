CREATE TABLE [dbo].[Resources] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [ResourceName] NVARCHAR (80)  NULL,
    [URL]          NVARCHAR (100) NULL,
    [SeqNo]        INT            NULL,
    CONSTRAINT [PK_Resources] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[Resources] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[Resources] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[Resources] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[Resources] TO [wfjuser]
    AS [dbo];



