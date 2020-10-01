CREATE TABLE [dbo].[Newsletters] (
    [ID]             INT           IDENTITY (1, 1) NOT NULL,
    [NewsletterDate] SMALLDATETIME NULL,
    [Title]          NVARCHAR (50) NULL,
    [FileName]       NVARCHAR (50) NULL,
    [Contents]       NTEXT         NULL,
    [NewsletterType] INT           NULL,
    CONSTRAINT [PK_Newsletters] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[Newsletters] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[Newsletters] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[Newsletters] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[Newsletters] TO [wfjuser]
    AS [dbo];



