CREATE TABLE [dbo].[PrepaidBenefits] (
    [ID]    INT            IDENTITY (1, 1) NOT NULL,
    [Title] NVARCHAR (120) NULL,
    [Text]  NTEXT          NULL,
    [SeqNo] INT            NULL,
    CONSTRAINT [PK_PrepaidBenefits] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[PrepaidBenefits] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[PrepaidBenefits] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[PrepaidBenefits] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[PrepaidBenefits] TO [wfjuser]
    AS [dbo];



