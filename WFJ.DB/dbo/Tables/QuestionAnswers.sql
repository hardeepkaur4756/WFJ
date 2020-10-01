CREATE TABLE [dbo].[QuestionAnswers] (
    [QuestionAnswerID] INT           IDENTITY (1, 1) NOT NULL,
    [QuestionID]       INT           NULL,
    [ColumnNbr]        INT           NULL,
    [AnswerText]       VARCHAR (500) NULL,
    [SeqNo]            INT           NULL,
    [DefaultItem]      TINYINT       NULL,
    CONSTRAINT [PK_QuestionAnswers] PRIMARY KEY CLUSTERED ([QuestionAnswerID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[QuestionAnswers] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[QuestionAnswers] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[QuestionAnswers] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[QuestionAnswers] TO [wfjuser]
    AS [dbo];



