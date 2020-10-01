CREATE TABLE [dbo].[FAQs] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [ClientID]    INT           NULL,
    [Question]    NTEXT         NULL,
    [Answer]      NTEXT         NULL,
    [DateUpdated] SMALLDATETIME NULL,
    [ContentArea] INT           NULL,
    CONSTRAINT [PK_FAQs] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[FAQs] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[FAQs] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[FAQs] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[FAQs] TO [wfjuser]
    AS [dbo];



