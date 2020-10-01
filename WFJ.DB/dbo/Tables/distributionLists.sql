CREATE TABLE [dbo].[distributionLists] (
    [distributionListID]   INT            NOT NULL,
    [distributionListName] VARCHAR (100)  NULL,
    [useWinHttp]           TINYINT        NULL,
    [httpURL]              VARCHAR (200)  NULL,
    [dsConnStr]            VARCHAR (500)  NULL,
    [dsLoginName]          VARCHAR (20)   NULL,
    [dsPassword]           VARCHAR (20)   NULL,
    [dsQuery]              VARCHAR (3000) NULL
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[distributionLists] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[distributionLists] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[distributionLists] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[distributionLists] TO [wfjuser]
    AS [dbo];



