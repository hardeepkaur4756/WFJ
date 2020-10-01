CREATE TABLE [dbo].[UserStatusCodePermissions] (
    [UserID]     INT NOT NULL,
    [FormID]     INT NOT NULL,
    [StatusCode] INT NOT NULL,
    CONSTRAINT [PK_UserStatusCodePermissions] PRIMARY KEY CLUSTERED ([UserID] ASC, [FormID] ASC, [StatusCode] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[UserStatusCodePermissions] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[UserStatusCodePermissions] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[UserStatusCodePermissions] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[UserStatusCodePermissions] TO [wfjuser]
    AS [dbo];



