CREATE TABLE [dbo].[PaymentTypes] (
    [ID]              INT          NOT NULL,
    [PaymentTypeDesc] VARCHAR (20) NULL,
    [Source]          TINYINT      NULL,
    [ClientID]        INT          NULL,
    [active]          TINYINT      NULL,
    CONSTRAINT [PK_PaymentTypes] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[PaymentTypes] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[PaymentTypes] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[PaymentTypes] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[PaymentTypes] TO [wfjuser]
    AS [dbo];



