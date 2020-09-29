CREATE TABLE [dbo].[EmployeeCategories] (
    [EmployeeCategoryID] INT          IDENTITY (1, 1) NOT NULL,
    [EmployeeCategory]   VARCHAR (30) NULL,
    [ParentCategoryID]   INT          NULL,
    [TopCategoryID]      INT          NULL,
    CONSTRAINT [PK_EmployeeCategories] PRIMARY KEY CLUSTERED ([EmployeeCategoryID] ASC)
);

