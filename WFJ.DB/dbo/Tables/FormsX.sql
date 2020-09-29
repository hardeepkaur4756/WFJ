CREATE TABLE [dbo].[FormsX] (
    [FormID]       INT          NOT NULL,
    [FormName]     VARCHAR (50) NULL,
    [FormTypeID]   INT          NULL,
    [ClientID]     INT          NULL,
    [hasCollector] TINYINT      NULL,
    [ClientNumber] INT          NULL,
    CONSTRAINT [PK_FormsX] PRIMARY KEY CLUSTERED ([FormID] ASC)
);

