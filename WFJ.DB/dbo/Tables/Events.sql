CREATE TABLE [dbo].[Events] (
    [EventID]           INT          IDENTITY (1, 1) NOT NULL,
    [EventName]         VARCHAR (50) NULL,
    [Summary]           TEXT         NULL,
    [HowtoRegister]     TEXT         NULL,
    [EventLimit]        INT          NULL,
    [Document]          VARCHAR (80) NULL,
    [URL]               VARCHAR (80) NULL,
    [Sponsors]          VARCHAR (80) NULL,
    [Category]          INT          NULL,
    [Status]            INT          NULL,
    [ContactName]       VARCHAR (50) NULL,
    [ContactAddress]    VARCHAR (30) NULL,
    [ContactCity]       VARCHAR (30) NULL,
    [ContactState]      VARCHAR (16) NULL,
    [ContactPostalCode] VARCHAR (10) NULL,
    [ContactTelephone]  VARCHAR (20) NULL,
    [ContactEMail]      VARCHAR (30) NULL,
    CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED ([EventID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[Events] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[Events] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[Events] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[Events] TO [wfjuser]
    AS [dbo];



