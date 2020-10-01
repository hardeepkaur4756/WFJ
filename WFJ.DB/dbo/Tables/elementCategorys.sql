CREATE TABLE [dbo].[elementCategorys] (
    [elementCategoryID]   INT          NOT NULL,
    [elementCategoryName] VARCHAR (50) NULL
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[elementCategorys] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[elementCategorys] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[elementCategorys] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[elementCategorys] TO [wfjuser]
    AS [dbo];



