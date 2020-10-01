CREATE TABLE [dbo].[Questions] (
    [QuestionID]             INT            NOT NULL,
    [QuestionName]           VARCHAR (4000) NULL,
    [FieldTypeID]            INT            NULL,
    [SeqNo]                  INT            NULL,
    [QuestionnaireSectionID] INT            NULL,
    [Required]               TINYINT        NULL,
    [SameAsQuestionID]       INT            NULL,
    [TableRows]              INT            NULL,
    [AttachFile]             TINYINT        NULL,
    [Length]                 INT            NULL,
    [BeginYear]              INT            NULL,
    [EndYear]                INT            NULL,
    [FormFieldID]            INT            NULL,
    [ExportSeqNo]            INT            NULL,
    [EPDBFieldName]          VARCHAR (40)   NULL,
    CONSTRAINT [PK_Questions] PRIMARY KEY CLUSTERED ([QuestionID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[Questions] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[Questions] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[Questions] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[Questions] TO [wfjuser]
    AS [dbo];



