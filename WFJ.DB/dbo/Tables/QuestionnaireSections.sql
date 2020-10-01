CREATE TABLE [dbo].[QuestionnaireSections] (
    [QuestionnaireSectionID]   INT          IDENTITY (1, 1) NOT NULL,
    [QuestionnaireSectionName] VARCHAR (50) NULL,
    [SeqNo]                    INT          NULL,
    [HeaderText]               TEXT         NULL,
    [FooterText]               TEXT         NULL,
    CONSTRAINT [PK_QuestionnaireSections] PRIMARY KEY CLUSTERED ([QuestionnaireSectionID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[QuestionnaireSections] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[QuestionnaireSections] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[QuestionnaireSections] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[QuestionnaireSections] TO [wfjuser]
    AS [dbo];



