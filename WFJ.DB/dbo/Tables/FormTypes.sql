CREATE TABLE [dbo].[FormTypes] (
    [FormTypeID]      INT          IDENTITY (1, 1) NOT NULL,
    [FormType]        VARCHAR (50) NULL,
    [displayInSearch] TINYINT      NULL,
    [sequenceID]      INT          NULL,
    CONSTRAINT [PK_FormTypes] PRIMARY KEY CLUSTERED ([FormTypeID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[FormTypes] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[FormTypes] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[FormTypes] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[FormTypes] TO [wfjuser]
    AS [dbo];



