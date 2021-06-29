Alter table [dbo].RequestNotes
Add  AlreadySeen bit null

GO

CREATE TABLE [dbo].[RecentAccountActivities](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [varchar](50) NULL,
	[RequestID] [int] NULL,
	[UserID] [int] NULL,
	[CreatedDate] [datetime] NULL,
	CONSTRAINT PK_RecentAccountActivities PRIMARY KEY CLUSTERED (ID),
	CONSTRAINT FK_RecentAccountActivities_Users FOREIGN KEY (UserID) REFERENCES Users(UserID),
	CONSTRAINT FK_RecentAccountActivities_Requests FOREIGN KEY (RequestID) REFERENCES Requests(ID)
 )


-- drop current primary key constraint
ALTER TABLE [dbo].[UserAttorneys]
DROP CONSTRAINT PK_UserAttorneys;
GO

-- add new auto incremented field
ALTER TABLE [dbo].[UserAttorneys] 
ADD ID INT IDENTITY;
GO

-- create new primary key constraint
ALTER TABLE [dbo].[UserAttorneys]  
ADD CONSTRAINT PK_UserAttorney PRIMARY KEY (ID)
GO
