CREATE TABLE [dbo].[TableColumns] (
    [TableColumnID] INT          IDENTITY (1, 1) NOT NULL,
    [QuestionID]    INT          NULL,
    [ColumnNbr]     INT          NULL,
    [FieldName]     VARCHAR (30) NULL,
    [FieldTypeID]   INT          NULL,
    CONSTRAINT [PK_TableColumns] PRIMARY KEY CLUSTERED ([TableColumnID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[TableColumns] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[TableColumns] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[TableColumns] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[TableColumns] TO [wfjuser]
    AS [dbo];



