CREATE TABLE [dbo].[User Functions] (
    [UserFunction] INT NOT NULL,
    [UserType]     INT NOT NULL,
    [AccessLevel]  INT NULL,
    CONSTRAINT [PK_User Functions] PRIMARY KEY CLUSTERED ([UserFunction] ASC, [UserType] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[User Functions] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[User Functions] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[User Functions] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[User Functions] TO [wfjuser]
    AS [dbo];



