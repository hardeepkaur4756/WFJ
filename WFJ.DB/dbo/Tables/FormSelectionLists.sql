CREATE TABLE [dbo].[FormSelectionLists] (
    [ID]          INT          IDENTITY (1, 1) NOT NULL,
    [FormFieldID] INT          NULL,
    [Code]        VARCHAR (10) NULL,
    [TextValue]   VARCHAR (50) NULL,
    [SeqNo]       INT          NULL,
    CONSTRAINT [PK_FormSelectionLists] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[FormSelectionLists] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[FormSelectionLists] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[FormSelectionLists] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[FormSelectionLists] TO [wfjuser]
    AS [dbo];



