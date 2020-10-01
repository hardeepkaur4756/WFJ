CREATE TABLE [dbo].[News] (
    [NewsID]         INT           IDENTITY (1, 1) NOT NULL,
    [Headline]       VARCHAR (150) NULL,
    [ShortText]      VARCHAR (200) NULL,
    [StoryText]      TEXT          NULL,
    [LinkURL]        VARCHAR (150) NULL,
    [NewWindow]      TINYINT       NULL,
    [Status]         INT           NULL,
    [Category]       INT           NULL,
    [ShowWholeStory] INT           NULL,
    [SeqNo]          INT           NULL,
    CONSTRAINT [PK_News] PRIMARY KEY CLUSTERED ([NewsID] ASC) WITH (FILLFACTOR = 90)
);




GO
GRANT UPDATE
    ON OBJECT::[dbo].[News] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[News] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[News] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[News] TO [wfjuser]
    AS [dbo];



