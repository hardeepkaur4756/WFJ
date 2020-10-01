CREATE TABLE [dbo].[FormFields] (
    [ID]                  INT            IDENTITY (1, 1) NOT NULL,
    [FormID]              INT            NULL,
    [FieldTypeID]         INT            NULL,
    [formSectionID]       INT            NULL,
    [Required]            TINYINT        NULL,
    [Length]              INT            NULL,
    [FieldName]           VARCHAR (600)  NULL,
    [SeqNo]               INT            NULL,
    [ListSeqNo]           INT            NULL,
    [AccessLevel]         INT            NULL,
    [EMailField]          TINYINT        NULL,
    [SelectionColumn]     INT            NULL,
    [EPDBFieldName]       VARCHAR (80)   NULL,
    [SQLStatement]        VARCHAR (3000) NULL,
    [AccountSummarySeqNo] INT            NULL,
    [showOnCalendar]      TINYINT        NULL,
    CONSTRAINT [PK_FormFields] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);




GO
CREATE NONCLUSTERED INDEX [_dta_index_FormFields_28_45243216__K2_1_7]
    ON [dbo].[FormFields]([FormID] ASC)
    INCLUDE([ID], [FieldName]);


GO
CREATE NONCLUSTERED INDEX [IX_FieldName]
    ON [dbo].[FormFields]([FieldName] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FormFields_2]
    ON [dbo].[FormFields]([FieldTypeID] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_FormFields_1]
    ON [dbo].[FormFields]([SeqNo] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_FormFields]
    ON [dbo].[FormFields]([FormID] ASC) WITH (FILLFACTOR = 90);


GO
CREATE STATISTICS [_dta_stat_45243216_2_1_10_4_8]
    ON [dbo].[FormFields]([FormID], [ID], [AccessLevel], [formSectionID], [SeqNo]);


GO
CREATE STATISTICS [_dta_stat_45243216_2_10_4_8_4364]
    ON [dbo].[FormFields]([FormID], [AccessLevel], [formSectionID], [SeqNo]);


GO
CREATE STATISTICS [_dta_stat_45243216_1_10_4_8]
    ON [dbo].[FormFields]([ID], [AccessLevel], [formSectionID], [SeqNo]);


GO
CREATE STATISTICS [_dta_stat_45243216_1_2_4_8_4149]
    ON [dbo].[FormFields]([ID], [FormID], [formSectionID], [SeqNo]);


GO
CREATE STATISTICS [_dta_stat_45243216_2_1_3_8_4864]
    ON [dbo].[FormFields]([FormID], [ID], [FieldTypeID], [SeqNo]);


GO
CREATE STATISTICS [_dta_stat_45243216_1_3_8_6960]
    ON [dbo].[FormFields]([ID], [FieldTypeID], [SeqNo]);


GO
CREATE STATISTICS [_dta_stat_45243216_1_7_8_1410]
    ON [dbo].[FormFields]([ID], [FieldName], [SeqNo]);


GO
CREATE STATISTICS [_dta_stat_45243216_2_1_9_4149]
    ON [dbo].[FormFields]([FormID], [ID], [ListSeqNo]);


GO
CREATE STATISTICS [_dta_stat_45243216_3_2_8_8066]
    ON [dbo].[FormFields]([FieldTypeID], [FormID], [SeqNo]);


GO
CREATE STATISTICS [_dta_stat_45243216_11_7_1]
    ON [dbo].[FormFields]([EMailField], [FieldName], [ID]);


GO
CREATE STATISTICS [_dta_stat_45243216_1_4_8_5201]
    ON [dbo].[FormFields]([ID], [formSectionID], [SeqNo]);


GO
CREATE STATISTICS [_dta_stat_45243216_1_2_8_1912]
    ON [dbo].[FormFields]([ID], [FormID], [SeqNo]);


GO
CREATE STATISTICS [_dta_stat_45243216_7_1_2]
    ON [dbo].[FormFields]([FieldName], [ID], [FormID]);


GO
CREATE STATISTICS [_dta_stat_45243216_2_4_8_8066]
    ON [dbo].[FormFields]([FormID], [formSectionID], [SeqNo]);


GO
CREATE STATISTICS [_dta_stat_45243216_8_2_4149]
    ON [dbo].[FormFields]([SeqNo], [FormID]);


GO
CREATE STATISTICS [_dta_stat_45243216_4_8_1912]
    ON [dbo].[FormFields]([formSectionID], [SeqNo]);


GO
CREATE STATISTICS [_dta_stat_45243216_2_7_6960]
    ON [dbo].[FormFields]([FormID], [FieldName]);


GO
CREATE STATISTICS [_dta_stat_45243216_3_8_9850]
    ON [dbo].[FormFields]([FieldTypeID], [SeqNo]);


GO
CREATE STATISTICS [_dta_stat_45243216_1_8_1912]
    ON [dbo].[FormFields]([ID], [SeqNo]);


GO
CREATE STATISTICS [_dta_stat_45243216_1_9_5201]
    ON [dbo].[FormFields]([ID], [ListSeqNo]);


GO
CREATE STATISTICS [_dta_stat_45243216_9_2_8066]
    ON [dbo].[FormFields]([ListSeqNo], [FormID]);


GO
GRANT UPDATE
    ON OBJECT::[dbo].[FormFields] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[FormFields] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[FormFields] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[FormFields] TO [wfjuser]
    AS [dbo];



