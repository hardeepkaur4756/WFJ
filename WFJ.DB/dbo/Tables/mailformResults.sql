CREATE TABLE [dbo].[mailformResults] (
    [mailformResultID] INT            IDENTITY (1, 1) NOT NULL,
    [mailformID]       INT            NULL,
    [submitDate]       DATETIME       NULL,
    [field01result]    VARCHAR (100)  NULL,
    [field02result]    VARCHAR (100)  NULL,
    [field03result]    VARCHAR (100)  NULL,
    [field04result]    VARCHAR (100)  NULL,
    [field05result]    VARCHAR (1000) NULL,
    [field06result]    VARCHAR (100)  NULL,
    [field07result]    VARCHAR (100)  NULL,
    [field08result]    VARCHAR (100)  NULL,
    [field09result]    VARCHAR (100)  NULL,
    [field10result]    VARCHAR (100)  NULL,
    [field11result]    VARCHAR (100)  NULL,
    [field12result]    VARCHAR (100)  NULL,
    [field13result]    VARCHAR (100)  NULL,
    [field14result]    VARCHAR (100)  NULL,
    [field15result]    VARCHAR (100)  NULL,
    [field16result]    VARCHAR (100)  NULL,
    [field17result]    VARCHAR (100)  NULL,
    [field18result]    VARCHAR (100)  NULL,
    [field19result]    VARCHAR (100)  NULL,
    [field20result]    VARCHAR (100)  NULL,
    [field21result]    VARCHAR (100)  NULL,
    [field22result]    VARCHAR (100)  NULL,
    [field23result]    VARCHAR (100)  NULL,
    [field24result]    VARCHAR (100)  NULL,
    [field25result]    VARCHAR (100)  NULL,
    [field26result]    VARCHAR (100)  NULL,
    [field27result]    VARCHAR (100)  NULL,
    [field28result]    VARCHAR (100)  NULL,
    [field29result]    VARCHAR (100)  NULL,
    [field30result]    VARCHAR (100)  NULL,
    CONSTRAINT [PK_mailformResults] PRIMARY KEY CLUSTERED ([mailformResultID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[mailformResults] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[mailformResults] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[mailformResults] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[mailformResults] TO [wfjuser]
    AS [dbo];



