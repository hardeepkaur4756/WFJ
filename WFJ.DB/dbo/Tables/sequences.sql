CREATE TABLE [dbo].[sequences] (
    [sequenceID] INT NOT NULL,
    [sequence]   INT NULL
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[sequences] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[sequences] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[sequences] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[sequences] TO [wfjuser]
    AS [dbo];



