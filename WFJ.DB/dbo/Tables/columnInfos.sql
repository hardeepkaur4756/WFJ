CREATE TABLE [dbo].[columnInfos] (
    [columnInfoID]      INT            IDENTITY (1, 1) NOT NULL,
    [tableName]         VARCHAR (50)   NULL,
    [columnName]        VARCHAR (50)   NULL,
    [promptName]        VARCHAR (512)  NULL,
    [sequenceID]        INT            NULL,
    [helpText]          VARCHAR (255)  NULL,
    [imageSelectDir]    VARCHAR (50)   NULL,
    [textareaRows]      INT            NULL,
    [textareaCols]      INT            NULL,
    [lookupTable]       VARCHAR (50)   NULL,
    [lookupIDColumn]    VARCHAR (50)   NULL,
    [lookupFieldName]   VARCHAR (50)   NULL,
    [lookupFieldName2]  VARCHAR (50)   NULL,
    [dropdownListQuery] VARCHAR (1000) NULL,
    [hiddenField]       INT            NULL,
    [hiddenValue]       VARCHAR (50)   NULL,
    [coolEditor]        TINYINT        NULL,
    [dateFmt]           VARCHAR (1)    NULL,
    [tinyintText]       VARCHAR (20)   NULL,
    CONSTRAINT [PK_columnInfos] PRIMARY KEY CLUSTERED ([columnInfoID] ASC)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[columnInfos] TO [wfjuser]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[dbo].[columnInfos] TO [wfjuser]
    AS [dbo];


GO
GRANT INSERT
    ON OBJECT::[dbo].[columnInfos] TO [wfjuser]
    AS [dbo];


GO
GRANT DELETE
    ON OBJECT::[dbo].[columnInfos] TO [wfjuser]
    AS [dbo];

