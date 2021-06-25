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