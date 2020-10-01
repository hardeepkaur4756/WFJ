CREATE TABLE [dbo].[Clients] (
    [ID]                   INT           IDENTITY (1, 1) NOT NULL,
    [ClientName]           NVARCHAR (50) NULL,
    [ContactName]          NVARCHAR (50) NULL,
    [defaultUserID]        INT           NULL,
    [Image]                NVARCHAR (50) NULL,
    [Address1]             NVARCHAR (50) NULL,
    [Address2]             NVARCHAR (30) NULL,
    [City]                 NVARCHAR (30) NULL,
    [State]                NVARCHAR (2)  NULL,
    [PostalCode]           NVARCHAR (10) NULL,
    [EMail]                NVARCHAR (50) NULL,
    [Telephone]            NVARCHAR (18) NULL,
    [Fax]                  NVARCHAR (18) NULL,
    [LevelName]            VARCHAR (50)  NULL,
    [ParentLevelID]        INT           NULL,
    [ClientNumberx]        INT           NULL,
    [BusinessSummary]      TEXT          NULL,
    [RequestorTitle]       VARCHAR (30)  NULL,
    [RequestDateField]     VARCHAR (30)  NULL,
    [autoShowFiles]        TINYINT       NULL,
    [hasAccountSummary]    TINYINT       NULL,
    [hasCollectionSummary] TINYINT       NULL,
    [hasClientReport]      TINYINT       NULL,
    [hasExportFiles]       TINYINT       NULL,
    [showWFJFees]          TINYINT       NULL,
    [Active]               TINYINT       NULL,
    CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[Clients] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[Clients] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[Clients] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[Clients] TO [wfjuser]
    AS [dbo];



