CREATE TABLE [dbo].[EventDates] (
    [EventDateID]      INT            IDENTITY (1, 1) NOT NULL,
    [EventID]          INT            NULL,
    [EventDateName]    VARCHAR (100)  NULL,
    [EventDate]        VARCHAR (50)   NULL,
    [EventTime]        VARCHAR (50)   NULL,
    [StartDate]        SMALLDATETIME  NULL,
    [EndDate]          SMALLDATETIME  NULL,
    [Location]         VARCHAR (120)  NULL,
    [Status]           INT            NULL,
    [EventDateSummary] VARCHAR (3000) NULL,
    CONSTRAINT [PK_EventDates] PRIMARY KEY CLUSTERED ([EventDateID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[EventDates] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[EventDates] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[EventDates] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[EventDates] TO [wfjuser]
    AS [dbo];



