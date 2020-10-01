CREATE TABLE [dbo].[PracticeAreas] (
    [ID]               INT            IDENTITY (1, 1) NOT NULL,
    [PracticeAreaName] NVARCHAR (100) NULL,
    [Description]      NTEXT          NULL,
    [Status]           TINYINT        NULL,
    CONSTRAINT [PK_PracticeAreas] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[PracticeAreas] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[PracticeAreas] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[PracticeAreas] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[PracticeAreas] TO [wfjuser]
    AS [dbo];



