CREATE TABLE [dbo].[Users] (
    [UserID]                 INT            IDENTITY (1, 1) NOT NULL,
    [UserName]               NVARCHAR (30)  NULL,
    [Password]               NVARCHAR (500) NULL,
    [Title]                  NVARCHAR (50)  NULL,
    [FirstName]              NVARCHAR (20)  NULL,
    [LastName]               NVARCHAR (20)  NULL,
    [Telephone]              NVARCHAR (20)  NULL,
    [Fax]                    NVARCHAR (20)  NULL,
    [Address1]               VARCHAR (50)   NULL,
    [Address2]               VARCHAR (50)   NULL,
    [City]                   VARCHAR (50)   NULL,
    [State]                  VARCHAR (2)    NULL,
    [PostalCode]             VARCHAR (10)   NULL,
    [Image]                  VARCHAR (100)  NULL,
    [ManagerName]            VARCHAR (50)   NULL,
    [DateAdded]              SMALLDATETIME  NULL,
    [UserType]               INT            NULL,
    [UserAccess]             INT            NULL,
    [LogonCount]             INT            NULL,
    [EMail]                  VARCHAR (50)   NULL,
    [ManagerUserID]          INT            NULL,
    [ClientID]               INT            NULL,
    [AttorneyID]             INT            NULL,
    [levelID]                INT            NULL,
    [PasswordExpirationDate] SMALLDATETIME  NULL,
    [Active]                 TINYINT        NULL,
    [IsCollector]            TINYINT        NULL,
    [IsAdminStaff]           TINYINT        NULL,
    [HireDate]               SMALLDATETIME  NULL,
    [Birthdate]              SMALLDATETIME  NULL,
    [AccessClientExtranet]   TINYINT        NULL,
    [ProfileText]            VARCHAR (5000) NULL,
    [showHRSection]          TINYINT        NULL,
    [dashboardUser]          TINYINT        NULL,
    [IsPasswordHashed]       BIT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_UserTable$] PRIMARY KEY CLUSTERED ([UserID] ASC),
    CONSTRAINT [FK_UserAccessID] FOREIGN KEY ([UserAccess]) REFERENCES [dbo].[AccessLevels] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_UserClientID] FOREIGN KEY ([ClientID]) REFERENCES [dbo].[Clients] ([ID])
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[Users] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[Users] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[Users] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[Users] TO [wfjuser]
    AS [dbo];



