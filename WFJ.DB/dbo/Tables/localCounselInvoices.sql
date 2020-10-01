CREATE TABLE [dbo].[localCounselInvoices] (
    [localCounselInvoiceID] INT           IDENTITY (1, 1) NOT NULL,
    [requestID]             INT           NULL,
    [firmID]                INT           NULL,
    [userID]                INT           NULL,
    [invoiceDate]           SMALLDATETIME NULL,
    [invoiceNumber]         VARCHAR (20)  NULL,
    [reason]                VARCHAR (200) NULL,
    [feesAmount]            FLOAT (53)    NULL,
    [costsAmount]           FLOAT (53)    NULL,
    [amount]                FLOAT (53)    NULL,
    [currencyID]            INT           NULL,
    [checkNumber]           VARCHAR (20)  NULL,
    [paymentDate]           SMALLDATETIME NULL,
    [filename]              VARCHAR (120) NULL,
    CONSTRAINT [PK_localCounselInvoices] PRIMARY KEY CLUSTERED ([localCounselInvoiceID] ASC)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[localCounselInvoices] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[localCounselInvoices] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[localCounselInvoices] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[localCounselInvoices] TO [wfjuser]
    AS [dbo];



