CREATE TABLE [dbo].[areas] (
    [areaID]            INT          NOT NULL,
    [areaName]          VARCHAR (50) NULL,
    [sequenceID]        INT          NULL,
    [showOnAddElements] INT          NULL
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[areas] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[areas] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[areas] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[areas] TO [wfjuser]
    AS [dbo];



