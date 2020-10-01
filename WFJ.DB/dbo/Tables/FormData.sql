CREATE TABLE [dbo].[FormData] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [RequestID]   INT            NULL,
    [FormFieldID] INT            NULL,
    [FieldValue]  VARCHAR (2000) NULL,
    [currencyID]  INT            NULL,
    CONSTRAINT [PK_FormData] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);




GO
CREATE NONCLUSTERED INDEX [_dta_index_FormData_28_1296723672__K3_K2_K1_4_4364]
    ON [dbo].[FormData]([FormFieldID] ASC, [RequestID] ASC, [ID] ASC)
    INCLUDE([FieldValue]);


GO
CREATE NONCLUSTERED INDEX [IX_RequestField]
    ON [dbo].[FormData]([RequestID] ASC, [FormFieldID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FormFieldID]
    ON [dbo].[FormData]([FormFieldID] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_RequestID]
    ON [dbo].[FormData]([RequestID] ASC) WITH (FILLFACTOR = 90);


GO
CREATE STATISTICS [_dta_stat_1296723672_1_3_4864]
    ON [dbo].[FormData]([ID], [FormFieldID]);


GO
CREATE STATISTICS [_dta_stat_1296723672_2_1_6960]
    ON [dbo].[FormData]([RequestID], [ID]);


GO
GRANT UPDATE
    ON OBJECT::[dbo].[FormData] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[FormData] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[FormData] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[FormData] TO [wfjuser]
    AS [dbo];



