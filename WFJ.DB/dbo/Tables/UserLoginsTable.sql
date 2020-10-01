CREATE TABLE [dbo].[UserLoginsTable] (
    [ID]            INT           IDENTITY (1, 1) NOT NULL,
    [UserID]        INT           NULL,
    [LoginDateTime] SMALLDATETIME NULL,
    [IPAddress]     NVARCHAR (20) NULL,
    [ZipCode]       VARCHAR (10)  NULL,
    CONSTRAINT [PK_UserLoginsTable] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[UserLoginsTable] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[UserLoginsTable] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[UserLoginsTable] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[UserLoginsTable] TO [wfjuser]
    AS [dbo];



