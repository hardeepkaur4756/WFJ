CREATE TABLE [dbo].[Payments] (
    [ID]            INT           IDENTITY (1, 1) NOT NULL,
    [RequestID]     INT           NULL,
    [UserID]        INT           NULL,
    [PaymentDate]   SMALLDATETIME NULL,
    [remitDate]     SMALLDATETIME NULL,
    [CheckNumber]   VARCHAR (20)  NULL,
    [Amount]        FLOAT (53)    NULL,
    [WFJFees]       FLOAT (53)    NULL,
    [PaymentTypeID] INT           NULL,
    [currencyID]    INT           NULL,
    CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);




GO
CREATE NONCLUSTERED INDEX [IX_Payments_2]
    ON [dbo].[Payments]([PaymentDate] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_Payments_1]
    ON [dbo].[Payments]([RequestID] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_Payments]
    ON [dbo].[Payments]([UserID] ASC) WITH (FILLFACTOR = 90);


GO
GRANT UPDATE
    ON OBJECT::[dbo].[Payments] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[Payments] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[Payments] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[Payments] TO [wfjuser]
    AS [dbo];



