CREATE TABLE [dbo].[FormAddressData] (
    [ID]           INT           IDENTITY (1, 1) NOT NULL,
    [RequestID]    INT           NULL,
    [FormFieldID]  INT           NULL,
    [AddressLine1] VARCHAR (300) NULL,
    [AddressLine2] VARCHAR (80)  NULL,
    [AddressLine3] VARCHAR (80)  NULL,
    [City]         VARCHAR (50)  NULL,
    [State]        VARCHAR (3)   NULL,
    [ZipCode]      VARCHAR (10)  NULL,
    [Country]      VARCHAR (20)  NULL,
    CONSTRAINT [PK_FormAddressData] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);




GO
CREATE NONCLUSTERED INDEX [_dta_index_FormAddressData_28_379148396__K2_K3_4_5_6_7_8_9_10_9987]
    ON [dbo].[FormAddressData]([RequestID] ASC, [FormFieldID] ASC)
    INCLUDE([AddressLine1], [AddressLine2], [AddressLine3], [City], [State], [ZipCode], [Country]);


GO
GRANT UPDATE
    ON OBJECT::[dbo].[FormAddressData] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[FormAddressData] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[FormAddressData] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[FormAddressData] TO [wfjuser]
    AS [dbo];



