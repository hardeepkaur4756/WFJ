CREATE TABLE [dbo].[Forms] (
    [ID]                     INT          IDENTITY (1, 1) NOT NULL,
    [FormName]               VARCHAR (50) NULL,
    [FormTypeID]             INT          NULL,
    [ClientID]               INT          NULL,
    [AccountBalanceFieldID]  INT          NULL,
    [TotalPaymentsFieldID]   INT          NULL,
    [CustomerNameFieldID]    INT          NULL,
    [WFJFileNbrFieldID]      INT          NULL,
    [CustomerAccountFieldID] INT          NULL,
    [JobNumberFieldID]       INT          NULL,
    [DefaultRequestorID]     INT          NULL,
    [hasCollector]           TINYINT      NULL,
    [hasAdmin]               TINYINT      NULL,
    [active]                 TINYINT      NULL,
    [ClientNumber]           INT          NULL,
    CONSTRAINT [PK_Forms] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);




GO
CREATE NONCLUSTERED INDEX [IX_Forms]
    ON [dbo].[Forms]([ClientID] ASC) WITH (FILLFACTOR = 90);


GO
GRANT UPDATE
    ON OBJECT::[dbo].[Forms] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[Forms] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[Forms] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[Forms] TO [wfjuser]
    AS [dbo];



