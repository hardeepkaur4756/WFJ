CREATE TABLE [dbo].[tableInfos] (
    [tableInfoID]   INT           IDENTITY (1, 1) NOT NULL,
    [tableName]     VARCHAR (50)  NULL,
    [tableOrderBy]  VARCHAR (100) NULL,
    [tableFirstCol] VARCHAR (50)  NULL,
    [tableListCols] VARCHAR (200) NULL,
    [sequenceID]    INT           NULL,
    [showInAdmin]   TINYINT       NULL,
    [pageSize]      INT           NULL,
    [joinTable1]    VARCHAR (50)  NULL,
    [joinCol1]      VARCHAR (50)  NULL,
    [joinTable2]    VARCHAR (50)  NULL,
    [joinCol2]      VARCHAR (50)  NULL,
    [joinTable3]    VARCHAR (50)  NULL,
    [joinCol3]      VARCHAR (50)  NULL,
    [adminName]     VARCHAR (100) NULL,
    [authoredTable] TINYINT       NULL,
    CONSTRAINT [PK_tableInfos] PRIMARY KEY CLUSTERED ([tableInfoID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[tableInfos] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[tableInfos] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[tableInfos] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[tableInfos] TO [wfjuser]
    AS [dbo];



