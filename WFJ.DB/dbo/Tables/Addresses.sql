CREATE TABLE [dbo].[Addresses] (
    [ClientID]   INT           NULL,
    [ClientName] NVARCHAR (50) NULL,
    [FormID]     INT           NOT NULL,
    [FormName]   VARCHAR (50)  NULL,
    [RequestID]  INT           NULL,
    [FieldID]    INT           NOT NULL,
    [FieldName]  VARCHAR (180) NULL,
    [FieldValue] VARCHAR (400) NULL
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[Addresses] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[Addresses] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[Addresses] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[Addresses] TO [wfjuser]
    AS [dbo];



