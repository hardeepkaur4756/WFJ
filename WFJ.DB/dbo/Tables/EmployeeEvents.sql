CREATE TABLE [dbo].[EmployeeEvents] (
    [EmployeeEventID] INT           IDENTITY (1, 1) NOT NULL,
    [EventName]       VARCHAR (50)  NULL,
    [EventCategoryID] INT           NULL,
    [EventDateName]   VARCHAR (100) NULL,
    [EventTime]       VARCHAR (50)  NULL,
    [EventDate]       SMALLDATETIME NULL,
    [Location]        VARCHAR (120) NULL,
    [Status]          INT           NULL,
    [Summary]         VARCHAR (500) NULL,
    CONSTRAINT [PK_EmployeeEvents] PRIMARY KEY CLUSTERED ([EmployeeEventID] ASC)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[EmployeeEvents] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[EmployeeEvents] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[EmployeeEvents] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[EmployeeEvents] TO [wfjuser]
    AS [dbo];



