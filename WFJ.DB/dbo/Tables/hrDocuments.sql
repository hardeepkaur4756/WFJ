CREATE TABLE [dbo].[hrDocuments] (
    [hrDocumentID] INT           IDENTITY (1, 1) NOT NULL,
    [documentName] VARCHAR (50)  NULL,
    [filename]     VARCHAR (150) NULL,
    [linkURL]      VARCHAR (150) NULL,
    [sequenceID]   INT           NULL,
    CONSTRAINT [PK_hrDocuments] PRIMARY KEY CLUSTERED ([hrDocumentID] ASC)
);

