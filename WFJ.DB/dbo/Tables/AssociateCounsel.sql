CREATE TABLE [dbo].[AssociateCounsel] (
    [FirmID]       INT           IDENTITY (1, 1) NOT NULL,
    [FirmName]     VARCHAR (80)  NULL,
    [Name]         VARCHAR (60)  NULL,
    [AddressLine1] VARCHAR (50)  NULL,
    [AddressLine2] VARCHAR (30)  NULL,
    [City]         VARCHAR (30)  NULL,
    [State]        VARCHAR (10)  NULL,
    [ZipCode]      VARCHAR (10)  NULL,
    [Check]        BIT           NULL,
    [W9Image]      VARCHAR (50)  NULL,
    [W9]           BIT           NULL,
    [Country]      VARCHAR (30)  NULL,
    [Telephone1]   VARCHAR (20)  NULL,
    [Ext]          VARCHAR (20)  NULL,
    [Telephone2]   VARCHAR (20)  NULL,
    [Fax]          VARCHAR (20)  NULL,
    [GB]           BIT           NULL,
    [ALQ]          BIT           NULL,
    [WH]           BIT           NULL,
    [FederalTaxID] VARCHAR (12)  NULL,
    [Website]      VARCHAR (255) NULL,
    [Email]        VARCHAR (80)  NULL,
    [DoNotUse]     TINYINT       NULL,
    [Notes]        TEXT          NULL,
    CONSTRAINT [PK_AssociateCounselNew] PRIMARY KEY CLUSTERED ([FirmID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[AssociateCounsel] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[AssociateCounsel] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[AssociateCounsel] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[AssociateCounsel] TO [wfjuser]
    AS [dbo];



