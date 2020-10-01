CREATE TABLE [dbo].[pages] (
    [pageID]            INT           NOT NULL,
    [sectionID]         INT           NOT NULL,
    [pageName]          VARCHAR (50)  NULL,
    [childOfPageID]     INT           NULL,
    [navName]           VARCHAR (50)  NULL,
    [sequenceID]        INT           NULL,
    [templateFile]      VARCHAR (50)  NULL,
    [includeOnMenu]     TINYINT       NULL,
    [status]            TINYINT       NULL,
    [underConstruction] TINYINT       NULL,
    [approvedDraft]     TINYINT       NULL,
    [altHeader]         TINYINT       NULL,
    [altPageID]         INT           NULL,
    [linkText]          VARCHAR (50)  NULL,
    [createDate]        DATETIME      NULL,
    [updateDate]        DATETIME      NULL,
    [shortcutName]      VARCHAR (50)  NULL,
    [altURL]            VARCHAR (255) NULL,
    [includeOnSitemap]  TINYINT       NULL,
    [metakeywords]      VARCHAR (255) NULL,
    [metadescription]   VARCHAR (512) NULL
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[pages] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[pages] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[pages] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[pages] TO [wfjuser]
    AS [dbo];



