CREATE TABLE [dbo].[Levels] (
    [ID]          INT          IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (50) NULL,
    [ContactName] VARCHAR (50) NULL,
    [Image]       VARCHAR (50) NULL,
    [Address1]    VARCHAR (50) NULL,
    [Address2]    VARCHAR (50) NULL,
    [City]        VARCHAR (50) NULL,
    [State]       VARCHAR (10) NULL,
    [PostalCode]  VARCHAR (50) NULL,
    [EMail]       VARCHAR (50) NULL,
    [Telephone]   VARCHAR (20) NULL,
    [Fax]         VARCHAR (20) NULL,
    [ParentID]    INT          NULL,
    [SeqNo]       INT          NULL,
    [ClientID]    INT          NULL,
    CONSTRAINT [PK_LevelsNew] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);




GO
CREATE NONCLUSTERED INDEX [_dta_index_Levels_28_500196832__K13_K2_1_3_4_5_6_7_8_9_10_11_12_14_15]
    ON [dbo].[Levels]([ParentID] ASC, [Name] ASC)
    INCLUDE([ID], [ContactName], [Image], [Address1], [Address2], [City], [State], [PostalCode], [EMail], [Telephone], [Fax], [SeqNo], [ClientID]);


GO
CREATE NONCLUSTERED INDEX [IX_Levels]
    ON [dbo].[Levels]([ParentID] ASC) WITH (FILLFACTOR = 90);


GO
CREATE STATISTICS [_dta_stat_500196832_1_13_14_1771]
    ON [dbo].[Levels]([ID], [ParentID], [SeqNo]);


GO
CREATE STATISTICS [_dta_stat_500196832_2_13]
    ON [dbo].[Levels]([Name], [ParentID]);


GO
CREATE STATISTICS [_dta_stat_500196832_14_1_1040]
    ON [dbo].[Levels]([SeqNo], [ID]);


GO
GRANT UPDATE
    ON OBJECT::[dbo].[Levels] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[Levels] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[Levels] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[Levels] TO [wfjuser]
    AS [dbo];



