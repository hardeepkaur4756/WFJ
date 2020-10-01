CREATE TABLE [dbo].[clickCounts] (
    [clickCountID] INT           IDENTITY (1, 1) NOT NULL,
    [mailingID]    INT           NULL,
    [redirURL]     VARCHAR (255) NULL,
    [clickCount]   INT           NULL,
    CONSTRAINT [PK_clickCounts] PRIMARY KEY CLUSTERED ([clickCountID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[clickCounts] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[clickCounts] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[clickCounts] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[clickCounts] TO [wfjuser]
    AS [dbo];



