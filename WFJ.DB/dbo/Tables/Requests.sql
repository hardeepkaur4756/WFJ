CREATE TABLE [dbo].[Requests] (
    [ID]                            INT           IDENTITY (1, 1) NOT NULL,
    [FormID]                        INT           NULL,
    [Requestor]                     INT           NULL,
    [AssignedAttorney]              INT           NULL,
    [AssignedCollectorID]           INT           NULL,
    [CollectorStatusCode]           VARCHAR (10)  NULL,
    [CollectorStatusCodeChangeDate] DATETIME      NULL,
    [AssignedAdminStaffID]          INT           NULL,
    [LegalAssistantID]              INT           NULL,
    [RequestDate]                   DATETIME      NULL,
    [LevelID]                       INT           NULL,
    [newLevelID]                    INT           NULL,
    [oldLevelID]                    INT           NULL,
    [LocalCounselName]              VARCHAR (50)  NULL,
    [LocalCounselFileNumber]        VARCHAR (30)  NULL,
    [reservePercent]                INT           NULL,
    [agingReport]                   TINYINT       NULL,
    [StatusCode]                    INT           NULL,
    [CompletionDate]                SMALLDATETIME NULL,
    [active]                        TINYINT       NULL,
    [LastViewed]                    DATETIME      NULL,
    [LastNoteDate]                  DATETIME      NULL,
    [TotalPayments]                 FLOAT (53)    NULL,
    [FirstValue]                    VARCHAR (100) NULL,
    [Value1]                        VARCHAR (100) NULL,
    [Value2]                        VARCHAR (50)  NULL,
    [Value3]                        VARCHAR (50)  NULL,
    [Value4]                        VARCHAR (50)  NULL,
    [Value5]                        VARCHAR (50)  NULL,
    [Value6]                        VARCHAR (50)  NULL,
    [Value7]                        VARCHAR (50)  NULL,
    [Value8]                        VARCHAR (50)  NULL,
    [Value9]                        VARCHAR (50)  NULL,
    [Value10]                       VARCHAR (50)  NULL,
    CONSTRAINT [PK_RequestsNew] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);




GO
CREATE NONCLUSTERED INDEX [IX_Requests_8]
    ON [dbo].[Requests]([RequestDate] ASC);


GO
CREATE NONCLUSTERED INDEX [_dta_index_Requests_28_381244413__K2_K20_K19_K18_9085]
    ON [dbo].[Requests]([FormID] ASC, [active] ASC, [CompletionDate] ASC, [StatusCode] ASC);


GO
CREATE NONCLUSTERED INDEX [_dta_index_Requests_28_381244413__K20_K18_K2_K19_1912]
    ON [dbo].[Requests]([active] ASC, [StatusCode] ASC, [FormID] ASC, [CompletionDate] ASC);


GO
CREATE NONCLUSTERED INDEX [_dta_index_Requests_28_381244413__K19_K2_K20_K18_4364]
    ON [dbo].[Requests]([CompletionDate] ASC, [FormID] ASC, [active] ASC, [StatusCode] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Requests_7]
    ON [dbo].[Requests]([StatusCode] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_Requests_6]
    ON [dbo].[Requests]([AssignedAttorney] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_Requests_5]
    ON [dbo].[Requests]([Requestor] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_Requests_4]
    ON [dbo].[Requests]([LevelID] ASC) WITH (FILLFACTOR = 90);


GO
CREATE STATISTICS [_dta_stat_381244413_18_1_2_20_19_11_3_4_25_26_10]
    ON [dbo].[Requests]([StatusCode], [ID], [FormID], [active], [CompletionDate], [LevelID], [Requestor], [AssignedAttorney], [Value1], [Value2], [RequestDate]);


GO
CREATE STATISTICS [_dta_stat_381244413_1_11_3_4_2_20_19_25_26_10]
    ON [dbo].[Requests]([ID], [LevelID], [Requestor], [AssignedAttorney], [FormID], [active], [CompletionDate], [Value1], [Value2], [RequestDate]);


GO
CREATE STATISTICS [_dta_stat_381244413_2_18_4_3_11_25_26_10_1]
    ON [dbo].[Requests]([FormID], [StatusCode], [AssignedAttorney], [Requestor], [LevelID], [Value1], [Value2], [RequestDate], [ID]);


GO
CREATE STATISTICS [_dta_stat_381244413_11_25_26_10_1_19_18_2_20]
    ON [dbo].[Requests]([LevelID], [Value1], [Value2], [RequestDate], [ID], [CompletionDate], [StatusCode], [FormID], [active]);


GO
CREATE STATISTICS [_dta_stat_381244413_11_25_26_10_1_19_2_20_9987]
    ON [dbo].[Requests]([LevelID], [Value1], [Value2], [RequestDate], [ID], [CompletionDate], [FormID], [active]);


GO
CREATE STATISTICS [_dta_stat_381244413_1_11_3_4_18_2_20]
    ON [dbo].[Requests]([ID], [LevelID], [Requestor], [AssignedAttorney], [StatusCode], [FormID], [active]);


GO
CREATE STATISTICS [_dta_stat_381244413_2_20_19_1_11_3]
    ON [dbo].[Requests]([FormID], [active], [CompletionDate], [ID], [LevelID], [Requestor]);


GO
CREATE STATISTICS [_dta_stat_381244413_25_26_10_1_18]
    ON [dbo].[Requests]([Value1], [Value2], [RequestDate], [ID], [StatusCode]);


GO
CREATE STATISTICS [_dta_stat_381244413_1_2_3_4_1912]
    ON [dbo].[Requests]([ID], [FormID], [Requestor], [AssignedAttorney]);


GO
CREATE STATISTICS [_dta_stat_381244413_4_3_11_2]
    ON [dbo].[Requests]([AssignedAttorney], [Requestor], [LevelID], [FormID]);


GO
CREATE STATISTICS [_dta_stat_381244413_19_2_20_1]
    ON [dbo].[Requests]([CompletionDate], [FormID], [active], [ID]);


GO
CREATE STATISTICS [_dta_stat_381244413_19_18_2_20_6355]
    ON [dbo].[Requests]([CompletionDate], [StatusCode], [FormID], [active]);


GO
CREATE STATISTICS [_dta_stat_381244413_2_1_26]
    ON [dbo].[Requests]([FormID], [ID], [Value2]);


GO
CREATE STATISTICS [_dta_stat_381244413_19_1_2]
    ON [dbo].[Requests]([CompletionDate], [ID], [FormID]);


GO
CREATE STATISTICS [_dta_stat_381244413_18_2_20_8341]
    ON [dbo].[Requests]([StatusCode], [FormID], [active]);


GO
CREATE STATISTICS [_dta_stat_381244413_2_20_1_6497]
    ON [dbo].[Requests]([FormID], [active], [ID]);


GO
CREATE STATISTICS [_dta_stat_381244413_2_3_4]
    ON [dbo].[Requests]([FormID], [Requestor], [AssignedAttorney]);


GO
CREATE STATISTICS [_dta_stat_381244413_1_4_2_4149]
    ON [dbo].[Requests]([ID], [AssignedAttorney], [FormID]);


GO
CREATE STATISTICS [_dta_stat_381244413_20_18_1]
    ON [dbo].[Requests]([active], [StatusCode], [ID]);


GO
CREATE STATISTICS [_dta_stat_381244413_4_2]
    ON [dbo].[Requests]([AssignedAttorney], [FormID]);


GO
CREATE STATISTICS [_dta_stat_381244413_2_19]
    ON [dbo].[Requests]([FormID], [CompletionDate]);


GO
CREATE STATISTICS [_dta_stat_381244413_20_1_1771]
    ON [dbo].[Requests]([active], [ID]);


GO
GRANT UPDATE
    ON OBJECT::[dbo].[Requests] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[Requests] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[Requests] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[Requests] TO [wfjuser]
    AS [dbo];



