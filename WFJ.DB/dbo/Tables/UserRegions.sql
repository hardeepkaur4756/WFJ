CREATE TABLE [dbo].[UserRegions] (
    [UserID]   INT NOT NULL,
    [RegionID] INT NOT NULL,
    CONSTRAINT [PK_UserRegions] PRIMARY KEY CLUSTERED ([UserID] ASC, [RegionID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[UserRegions] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[UserRegions] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[UserRegions] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[UserRegions] TO [wfjuser]
    AS [dbo];



