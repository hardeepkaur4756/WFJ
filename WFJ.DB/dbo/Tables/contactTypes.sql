CREATE TABLE [dbo].[contactTypes] (
    [contactTypeID] INT          IDENTITY (1, 1) NOT NULL,
    [contactType]   VARCHAR (50) NULL,
    CONSTRAINT [PK_contactTypes] PRIMARY KEY CLUSTERED ([contactTypeID] ASC)
);




GO;


GO
GRANT UPDATE
    ON OBJECT::[dbo].[contactTypes] TO [wfjuser]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[dbo].[contactTypes] TO [wfjuser]
    AS [dbo];


GO
GRANT INSERT
    ON OBJECT::[dbo].[contactTypes] TO [wfjuser]
    AS [dbo];


GO
GRANT DELETE
    ON OBJECT::[dbo].[contactTypes] TO [wfjuser]
    AS [dbo];

