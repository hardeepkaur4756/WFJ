CREATE TABLE [dbo].[UserLevels] (
    [UserID]  INT     NOT NULL,
    [LevelID] INT     NOT NULL,
    [active]  TINYINT NULL,
    CONSTRAINT [PK_UserLevels_1] PRIMARY KEY CLUSTERED ([UserID] ASC, [LevelID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[UserLevels] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[UserLevels] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[UserLevels] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[UserLevels] TO [wfjuser]
    AS [dbo];



