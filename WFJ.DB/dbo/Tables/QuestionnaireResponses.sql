CREATE TABLE [dbo].[QuestionnaireResponses] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [RequestID]   INT           NULL,
    [Field1Nbr]   INT           NULL,
    [Field2Nbr]   INT           NULL,
    [Field3Nbr]   INT           NULL,
    [Field4Nbr]   INT           NULL,
    [FieldValue1] VARCHAR (200) NULL,
    [FieldValue2] VARCHAR (50)  NULL,
    [FieldValue3] VARCHAR (50)  NULL,
    [FieldValue4] VARCHAR (50)  NULL,
    [FieldValue5] VARCHAR (50)  NULL,
    [FieldValue6] VARCHAR (50)  NULL,
    CONSTRAINT [PK_QuestionnaireResponses] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[QuestionnaireResponses] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[QuestionnaireResponses] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[QuestionnaireResponses] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[QuestionnaireResponses] TO [wfjuser]
    AS [dbo];



