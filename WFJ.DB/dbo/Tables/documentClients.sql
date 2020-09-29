CREATE TABLE [dbo].[documentClients] (
    [documentID] INT NOT NULL,
    [clientID]   INT NOT NULL,
    CONSTRAINT [PK_documentClients] PRIMARY KEY CLUSTERED ([documentID] ASC, [clientID] ASC)
);

