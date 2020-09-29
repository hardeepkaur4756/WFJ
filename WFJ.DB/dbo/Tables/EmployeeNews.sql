CREATE TABLE [dbo].[EmployeeNews] (
    [EmployeeNewsID] INT           IDENTITY (1, 1) NOT NULL,
    [Headline]       VARCHAR (120) NULL,
    [ShortText]      VARCHAR (100) NULL,
    [StoryText]      TEXT          NULL,
    [LinkURL]        VARCHAR (100) NULL,
    [NewWindow]      TINYINT       NULL,
    [Status]         INT           NULL,
    [Category]       INT           NULL,
    [ShowWholeStory] INT           NULL,
    [SeqNo]          INT           NULL,
    CONSTRAINT [PK_EmployeeNews] PRIMARY KEY CLUSTERED ([EmployeeNewsID] ASC)
);

