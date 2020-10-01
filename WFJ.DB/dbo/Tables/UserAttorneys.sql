CREATE TABLE [dbo].[UserAttorneys] (
    [UserID]      INT NOT NULL,
    [PersonnelID] INT NOT NULL,
    CONSTRAINT [PK_UserAttorneys] PRIMARY KEY CLUSTERED ([UserID] ASC, [PersonnelID] ASC)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[UserAttorneys] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[UserAttorneys] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[UserAttorneys] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[UserAttorneys] TO [wfjuser]
    AS [dbo];



