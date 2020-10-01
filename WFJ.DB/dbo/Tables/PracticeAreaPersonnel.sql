CREATE TABLE [dbo].[PracticeAreaPersonnel] (
    [ID]             INT IDENTITY (1, 1) NOT NULL,
    [PracticeAreaID] INT NULL,
    [PersonnelID]    INT NULL,
    [SeqNo]          INT NULL,
    CONSTRAINT [PK_PracticeAreaPersonnel] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);




GO
CREATE NONCLUSTERED INDEX [IX_PracticeAreaPersonnel_1]
    ON [dbo].[PracticeAreaPersonnel]([PersonnelID] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_PracticeAreaPersonnel]
    ON [dbo].[PracticeAreaPersonnel]([PracticeAreaID] ASC) WITH (FILLFACTOR = 90);


GO
GRANT UPDATE
    ON OBJECT::[dbo].[PracticeAreaPersonnel] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[PracticeAreaPersonnel] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[PracticeAreaPersonnel] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[PracticeAreaPersonnel] TO [wfjuser]
    AS [dbo];



