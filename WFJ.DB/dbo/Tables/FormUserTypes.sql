CREATE TABLE [dbo].[FormUserTypes] (
    [ID]       INT IDENTITY (1, 1) NOT NULL,
    [FormID]   INT NULL,
    [UserType] INT NULL,
    CONSTRAINT [PK_FormUserTypes] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);




GO
CREATE NONCLUSTERED INDEX [IX_FormUserTypes_1]
    ON [dbo].[FormUserTypes]([FormID] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_FormUserTypes]
    ON [dbo].[FormUserTypes]([UserType] ASC) WITH (FILLFACTOR = 90);


GO
GRANT UPDATE
    ON OBJECT::[dbo].[FormUserTypes] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[FormUserTypes] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[FormUserTypes] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[FormUserTypes] TO [wfjuser]
    AS [dbo];



