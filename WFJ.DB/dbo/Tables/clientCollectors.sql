CREATE TABLE [dbo].[clientCollectors] (
    [clientCollectorID] INT IDENTITY (1, 1) NOT NULL,
    [clientID]          INT NULL,
    [collectorID]       INT NULL,
    CONSTRAINT [PK_clientCollectors] PRIMARY KEY CLUSTERED ([clientCollectorID] ASC)
);

