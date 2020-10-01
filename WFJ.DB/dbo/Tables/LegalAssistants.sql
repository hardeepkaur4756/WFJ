CREATE TABLE [dbo].[LegalAssistants] (
    [LegalAssistantID]   INT          NOT NULL,
    [LegalAssistantName] VARCHAR (30) NULL,
    CONSTRAINT [PK_LegalAssistants] PRIMARY KEY CLUSTERED ([LegalAssistantID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[LegalAssistants] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[LegalAssistants] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[LegalAssistants] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[LegalAssistants] TO [wfjuser]
    AS [dbo];



