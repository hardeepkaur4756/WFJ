CREATE TABLE [dbo].[messages] (
    [messageID]   INT           NOT NULL,
    [messageName] VARCHAR (100) NULL,
    [messageFlag] TINYINT       NULL,
    [messageDesc] VARCHAR (500) NULL,
    [messageText] TEXT          NULL
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[messages] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[messages] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[messages] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[messages] TO [wfjuser]
    AS [dbo];



