CREATE TABLE [dbo].[Codes] (
    [ID]    INT            IDENTITY (1, 1) NOT NULL,
    [Type]  NVARCHAR (20)  NULL,
    [Code]  NVARCHAR (10)  NULL,
    [Value] NVARCHAR (120) NULL,
    CONSTRAINT [PK_Codes] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[Codes] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[Codes] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[Codes] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[Codes] TO [wfjuser]
    AS [dbo];



