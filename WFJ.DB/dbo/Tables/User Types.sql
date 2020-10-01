CREATE TABLE [dbo].[User Types] (
    [ID]       INT           NOT NULL,
    [UserType] NVARCHAR (30) NULL,
    CONSTRAINT [PK_User Types] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[User Types] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[User Types] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[User Types] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[User Types] TO [wfjuser]
    AS [dbo];



