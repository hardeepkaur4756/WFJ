CREATE TABLE [dbo].[Clientsx] (
    [ClientID]      INT           NOT NULL,
    [ClientName]    NVARCHAR (50) NULL,
    [ContactName]   NVARCHAR (50) NULL,
    [Image]         NVARCHAR (50) NULL,
    [Address1]      NVARCHAR (30) NULL,
    [Address2]      NVARCHAR (30) NULL,
    [City]          NVARCHAR (30) NULL,
    [State]         NVARCHAR (2)  NULL,
    [PostalCode]    NVARCHAR (10) NULL,
    [EMail]         NVARCHAR (50) NULL,
    [Telephone]     NVARCHAR (18) NULL,
    [Fax]           NVARCHAR (18) NULL,
    [LevelName]     VARCHAR (50)  NULL,
    [ParentLevelID] INT           NULL,
    [ClientNumberx] INT           NULL,
    CONSTRAINT [PK_Clientsx] PRIMARY KEY CLUSTERED ([ClientID] ASC)
);




GO
GRANT SELECT
    ON OBJECT::[dbo].[Clientsx] TO [wfjuser]
    AS [dbo];



