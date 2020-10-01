CREATE TABLE [dbo].[Notifications] (
    [ID]                     INT IDENTITY (1, 1) NOT NULL,
    [FormID]                 INT NULL,
    [ServiceDeadlineFieldID] INT NULL,
    [FirstDateFieldID]       INT NULL,
    [LastDateFieldID]        INT NULL,
    [DateFiledFieldID]       INT NULL,
    [DatePaidFieldID]        INT NULL,
    [DateReleasedFieldID]    INT NULL,
    CONSTRAINT [PK_Notifications] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[Notifications] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[Notifications] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[Notifications] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[Notifications] TO [wfjuser]
    AS [dbo];



