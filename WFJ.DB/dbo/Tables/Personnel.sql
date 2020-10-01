CREATE TABLE [dbo].[Personnel] (
    [ID]                        INT           IDENTITY (1, 1) NOT NULL,
    [Type]                      NVARCHAR (2)  NULL,
    [FirstName]                 NVARCHAR (30) NULL,
    [LastName]                  NVARCHAR (30) NULL,
    [FullName]                  NVARCHAR (60) NULL,
    [Title]                     NVARCHAR (80) NULL,
    [Location]                  NVARCHAR (50) NULL,
    [Image]                     NVARCHAR (50) NULL,
    [Telephone]                 NVARCHAR (18) NULL,
    [Fax]                       NVARCHAR (18) NULL,
    [EMail]                     NVARCHAR (50) NULL,
    [ProfessionalOrganizations] NTEXT         NULL,
    [StateBar]                  NTEXT         NULL,
    [FederalBar]                NTEXT         NULL,
    [Education]                 NTEXT         NULL,
    [Publications]              NTEXT         NULL,
    [Cases]                     NTEXT         NULL,
    [TraneAttorney]             TINYINT       NULL,
    [OtherExperience]           NTEXT         NULL,
    [HonorsAwards]              NTEXT         NULL,
    [SuccessStories]            NTEXT         NULL,
    [CommunityActivities]       NTEXT         NULL,
    [isShareholder]             TINYINT       NULL,
    CONSTRAINT [PK_Personnel] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);




GO
CREATE STATISTICS [_dta_stat_702625546_2_1_3_1912]
    ON [dbo].[Personnel]([Type], [ID], [FirstName]);


GO
GRANT UPDATE
    ON OBJECT::[dbo].[Personnel] TO [wfjuser]
    AS [dbo];




GO
GRANT SELECT
    ON OBJECT::[dbo].[Personnel] TO [wfjuser]
    AS [dbo];




GO
GRANT INSERT
    ON OBJECT::[dbo].[Personnel] TO [wfjuser]
    AS [dbo];




GO
GRANT DELETE
    ON OBJECT::[dbo].[Personnel] TO [wfjuser]
    AS [dbo];



