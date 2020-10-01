CREATE TABLE [dbo].[StatusCodes] (
    [ID]                          INT          IDENTITY (1, 1) NOT NULL,
    [StatusCode]                  INT          NULL,
    [ClientID]                    INT          NULL,
    [FormID]                      INT          NULL,
    [StatusLevel]                 INT          NULL,
    [SeqNo]                       INT          NULL,
    [Description]                 VARCHAR (30) NULL,
    [DescriptionLong]             VARCHAR (80) NULL,
    [deleteIt]                    TINYINT      NULL,
    [complianceDuration]          INT          NULL,
    [OnCollectorComplianceReport] TINYINT      NULL,
    CONSTRAINT [PK_StatusCodes] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);




GO
CREATE NONCLUSTERED INDEX [_dta_index_StatusCodes_28_512720879__K3_K4_K5_2_9850]
    ON [dbo].[StatusCodes]([ClientID] ASC, [FormID] ASC, [StatusLevel] ASC)
    INCLUDE([StatusCode]);


GO
CREATE NONCLUSTERED INDEX [_dta_index_StatusCodes_28_512720879__K2_K4_K5_K3_2894]
    ON [dbo].[StatusCodes]([StatusCode] ASC, [FormID] ASC, [StatusLevel] ASC, [ClientID] ASC);


GO
CREATE NONCLUSTERED INDEX [_dta_index_StatusCodes_28_512720879__K4_K2_K1_K5_K3_1771]
    ON [dbo].[StatusCodes]([FormID] ASC, [StatusCode] ASC, [ID] ASC, [StatusLevel] ASC, [ClientID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_StatusCodes_3]
    ON [dbo].[StatusCodes]([SeqNo] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_StatusCodes_2]
    ON [dbo].[StatusCodes]([FormID] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_StatusCodes_1]
    ON [dbo].[StatusCodes]([ClientID] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_StatusCodes]
    ON [dbo].[StatusCodes]([StatusCode] ASC) WITH (FILLFACTOR = 90);


GO
CREATE STATISTICS [_dta_stat_512720879_1_4_2_5_3_9987]
    ON [dbo].[StatusCodes]([ID], [FormID], [StatusCode], [StatusLevel], [ClientID]);


GO
CREATE STATISTICS [_dta_stat_512720879_5_2_4_3_8809]
    ON [dbo].[StatusCodes]([StatusLevel], [StatusCode], [FormID], [ClientID]);


GO
CREATE STATISTICS [_dta_stat_512720879_4_5_2_7]
    ON [dbo].[StatusCodes]([FormID], [StatusLevel], [StatusCode], [Description]);


GO
CREATE STATISTICS [_dta_stat_512720879_2_1_5_3_8066]
    ON [dbo].[StatusCodes]([StatusCode], [ID], [StatusLevel], [ClientID]);


GO
CREATE STATISTICS [_dta_stat_512720879_4_1_3_6_8258]
    ON [dbo].[StatusCodes]([FormID], [ID], [ClientID], [SeqNo]);


GO
CREATE STATISTICS [_dta_stat_512720879_4_3_2_1_2533]
    ON [dbo].[StatusCodes]([FormID], [ClientID], [StatusCode], [ID]);


GO
CREATE STATISTICS [_dta_stat_512720879_1_3_2_8258]
    ON [dbo].[StatusCodes]([ID], [ClientID], [StatusCode]);


GO
CREATE STATISTICS [_dta_stat_512720879_6_4_3_1410]
    ON [dbo].[StatusCodes]([SeqNo], [FormID], [ClientID]);


GO
CREATE STATISTICS [_dta_stat_512720879_3_1_6_2894]
    ON [dbo].[StatusCodes]([ClientID], [ID], [SeqNo]);


GO
CREATE STATISTICS [_dta_stat_512720879_1_4_6_1912]
    ON [dbo].[StatusCodes]([ID], [FormID], [SeqNo]);


GO
CREATE STATISTICS [_dta_stat_512720879_1_6_4864]
    ON [dbo].[StatusCodes]([ID], [SeqNo]);


GO
CREATE STATISTICS [_dta_stat_512720879_3_2]
    ON [dbo].[StatusCodes]([ClientID], [StatusCode]);


GO
CREATE STATISTICS [_dta_stat_512720879_3_5_1912]
    ON [dbo].[StatusCodes]([ClientID], [StatusLevel]);


GO
GRANT UPDATE
    ON OBJECT::[dbo].[StatusCodes] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[StatusCodes] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[StatusCodes] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[StatusCodes] TO [wfjuser]
    AS [dbo];



