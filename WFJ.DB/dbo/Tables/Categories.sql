CREATE TABLE [dbo].[Categories] (
    [CategoryID]     INT          IDENTITY (1, 1) NOT NULL,
    [PracticeAreaID] INT          NULL,
    [CategoryName]   VARCHAR (30) NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED ([CategoryID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[Categories] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[Categories] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[Categories] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[Categories] TO [wfjuser]
    AS [dbo];



