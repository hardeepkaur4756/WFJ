CREATE TABLE [dbo].[formDataSheets] (
    [formDataSheetID]        INT IDENTITY (1, 1) NOT NULL,
    [formID]                 INT NOT NULL,
    [caseFieldID]            INT NULL,
    [CustomerAccountFieldID] INT NULL,
    [balanceDueFieldID]      INT NULL,
    [addressFieldID]         INT NULL,
    [contactFieldID]         INT NULL,
    [projectFieldID]         INT NULL,
    [customerPhonefieldID]   INT NULL,
    [customerFaxFieldID]     INT NULL,
    [CustomerCellFieldID]    INT NULL,
    [CustomerEmailFieldID]   INT NULL,
    [billTypeFieldID]        INT NULL,
    [practiceGroupFieldID]   INT NULL,
    [billingRateFieldID]     INT NULL,
    CONSTRAINT [PK_formDataSheets] PRIMARY KEY CLUSTERED ([formDataSheetID] ASC)
);

