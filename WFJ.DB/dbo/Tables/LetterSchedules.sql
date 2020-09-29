CREATE TABLE [dbo].[LetterSchedules] (
    [LetterScheduleID] INT IDENTITY (1, 1) NOT NULL,
    [LetterID]         INT NULL,
    [StatusCode]       INT NULL,
    [RequestDays]      INT NULL,
    [CompletionDays]   INT NULL,
    CONSTRAINT [PK_LetterSchedules] PRIMARY KEY CLUSTERED ([LetterScheduleID] ASC)
);

