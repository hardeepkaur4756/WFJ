CREATE TABLE [dbo].[Documents] (
    [ID]                 INT            IDENTITY (1, 1) NOT NULL,
    [DocumentName]       NVARCHAR (120) NULL,
    [FileName]           NVARCHAR (150) NULL,
    [StateCode]          NVARCHAR (3)   NULL,
    [DocumentType]       NVARCHAR (5)   NULL,
    [ProjectType]        NVARCHAR (5)   NULL,
    [FormType]           VARCHAR (5)    NULL,
    [ClientID]           INT            NULL,
    [PracticeAreaID]     INT            NULL,
    [WFJFileNbr]         VARCHAR (50)   NULL,
    [Days]               INT            NULL,
    [Description]        NTEXT          NULL,
    [CategoryID]         INT            NULL,
    [EmployeeCategoryID] INT            NULL,
    [StateCodeID]        NVARCHAR (3)   NULL,
    [DocumentTypeID]     NVARCHAR (5)   NULL,
    [ProjectTypeID]      NVARCHAR (5)   NULL,
    [FormTypeID]         VARCHAR (5)    NULL,
    [SeqNo]              INT            NULL,
    CONSTRAINT [PK_Documents] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);




GO
CREATE NONCLUSTERED INDEX [IX_Documents_4]
    ON [dbo].[Documents]([ProjectType] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_Documents_3]
    ON [dbo].[Documents]([DocumentType] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_Documents_2]
    ON [dbo].[Documents]([StateCode] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_Documents_1]
    ON [dbo].[Documents]([PracticeAreaID] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_Documents]
    ON [dbo].[Documents]([ClientID] ASC) WITH (FILLFACTOR = 90);


GO
GRANT UPDATE
    ON OBJECT::[dbo].[Documents] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[Documents] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[Documents] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[Documents] TO [wfjuser]
    AS [dbo];



