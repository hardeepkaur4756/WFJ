CREATE TABLE [dbo].[currencies] (
    [currencyID]   INT          IDENTITY (1, 1) NOT NULL,
    [currencyCode] VARCHAR (10) NULL,
    [currencyName] VARCHAR (50) NULL,
    [sequenceID]   INT          NULL,
    CONSTRAINT [PK_currencies] PRIMARY KEY CLUSTERED ([currencyID] ASC)
);

