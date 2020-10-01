CREATE TABLE [dbo].[mailformTypes] (
    [mailformTypeID]   INT          NOT NULL,
    [mailformTypeName] VARCHAR (30) NULL
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[mailformTypes] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[mailformTypes] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[mailformTypes] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[mailformTypes] TO [wfjuser]
    AS [dbo];



