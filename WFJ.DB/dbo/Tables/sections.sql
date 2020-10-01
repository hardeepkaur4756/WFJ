CREATE TABLE [dbo].[sections] (
    [sectionID]          INT          NOT NULL,
    [sectionName]        VARCHAR (50) NULL,
    [sequenceID]         INT          NULL,
    [includeOnMenu]      TINYINT      NULL,
    [status]             TINYINT      NULL,
    [pageHeaderImage]    VARCHAR (50) NULL,
    [pageHeaderImage2]   VARCHAR (50) NULL,
    [navImage]           VARCHAR (50) NULL,
    [navRolloverImage]   VARCHAR (50) NULL,
    [sectionPageID]      INT          NULL,
    [dropdown1MenuSize]  VARCHAR (20) NULL,
    [dropdown2MenuSize]  VARCHAR (20) NULL,
    [dropdown3MenuSize]  VARCHAR (20) NULL,
    [dropdown4MenuSize]  VARCHAR (20) NULL,
    [dropdownMenuOffset] VARCHAR (20) NULL
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[sections] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[sections] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[sections] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[sections] TO [wfjuser]
    AS [dbo];



