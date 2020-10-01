CREATE TABLE [dbo].[FieldTypes] (
    [FieldTypeID]   INT          IDENTITY (1, 1) NOT NULL,
    [FieldTypeName] VARCHAR (30) NULL,
    CONSTRAINT [PK_FieldTypes] PRIMARY KEY CLUSTERED ([FieldTypeID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[FieldTypes] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[FieldTypes] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[FieldTypes] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[FieldTypes] TO [wfjuser]
    AS [dbo];



