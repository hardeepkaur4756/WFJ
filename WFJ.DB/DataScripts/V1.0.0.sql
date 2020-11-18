ALTER TABLE [dbo].[Users] 
ALTER COLUMN Password nvarchar(500);


ALTER TABLE [dbo].[Users] 
ADD IsPasswordHashed BIT DEFAULT 0 NOT NULL;

ALTER TABLE [dbo].[Users]  WITH NOCHECK 
ADD  FOREIGN KEY([levelID])
REFERENCES [dbo].Levels ([ID])


USE [wfj_testdb]
GO
ALTER TABLE [dbo].[Users]  WITH NOCHECK ADD FOREIGN KEY([ClientID])
REFERENCES [dbo].[Clients] ([ID])
GO

USE [wfj_testdb]
GO

ALTER TABLE [dbo].[Users]  WITH NOCHECK ADD FOREIGN KEY([levelID])
REFERENCES [dbo].[Levels] ([ID])
GO

USE [wfj_testdb]
GO

ALTER TABLE [dbo].[Users]  WITH NOCHECK ADD  CONSTRAINT [FK_UserAccessID] FOREIGN KEY([UserAccess])
REFERENCES [dbo].[AccessLevels] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_UserAccessID]
GO

USE [wfj_testdb]
GO

ALTER TABLE [dbo].[Documents]  WITH NOCHECK ADD  CONSTRAINT [FK_DocumentClientID] FOREIGN KEY([ClientID])
REFERENCES [dbo].[Clients] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Documents] CHECK CONSTRAINT [FK_DocumentClientID]
GO


USE [wfj_testdb]
GO

ALTER TABLE [dbo].[Documents]  WITH NOCHECK ADD  CONSTRAINT [FK_PracticeAreasID] FOREIGN KEY([PracticeAreaID])
REFERENCES [dbo].[PracticeAreas] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Documents] CHECK CONSTRAINT [FK_PracticeAreasID]
GO


USE [wfj_testdb]
GO

ALTER TABLE [dbo].[Categories]  WITH NOCHECK ADD  CONSTRAINT [FK_CategoryPracticeAreaID] FOREIGN KEY([PracticeAreaID])
REFERENCES [dbo].[PracticeAreas] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Categories] CHECK CONSTRAINT [FK_CategoryPracticeAreaID]
GO


USE [wfj_testdb]
GO

/****** Object:  Table [dbo].[UserClients]    Script Date: 10/21/2020 11:14:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserClients](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ClientID] [int] NULL,
	[UserID] [int] NULL,
 CONSTRAINT [PK_UserClients] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[UserClients]  WITH NOCHECK ADD  CONSTRAINT [FK_UserClients_ClientID] FOREIGN KEY([ClientID])
REFERENCES [dbo].[Clients] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[UserClients] CHECK CONSTRAINT [FK_UserClients_ClientID]
GO

ALTER TABLE [dbo].[UserClients]  WITH NOCHECK ADD  CONSTRAINT [FK_UserClients_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[UserClients] CHECK CONSTRAINT [FK_UserClients_UserID]
GO


USE [wfj_testdb]
GO

/****** Object:  Table [dbo].[ErrorLog]    Script Date: 10/21/2020 11:16:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ErrorLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ErrorText] [nvarchar](max) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreatedBy] [int] NULL,
	[Page] [nvarchar](500) NULL,
 CONSTRAINT [PK_dbo.ErrorLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


USE [wfj_testdb]
GO

ALTER TABLE [dbo].[UserLevels]  WITH NOCHECK ADD  CONSTRAINT [FK_UserLevels_LevelID] FOREIGN KEY([LevelID])
REFERENCES [dbo].[Levels] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[UserLevels] CHECK CONSTRAINT [FK_UserLevels_LevelID]
GO


USE [wfj_testdb]
GO

ALTER TABLE [dbo].[UserLevels]  WITH NOCHECK ADD  CONSTRAINT [FK_UserLevels_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[UserLevels] CHECK CONSTRAINT [FK_UserLevels_UserID]
GO


USE [wfj_testdb]
GO

ALTER TABLE [dbo].[documentClients]  WITH NOCHECK ADD  CONSTRAINT [FK_documentClients_Clients_Id] FOREIGN KEY([clientID])
REFERENCES [dbo].[Clients] ([ID])
GO

ALTER TABLE [dbo].[documentClients] CHECK CONSTRAINT [FK_documentClients_Clients_Id]
GO


USE [wfj_testdb]
GO

ALTER TABLE [dbo].[documentClients]  WITH NOCHECK ADD  CONSTRAINT [FK_documentClients_Documents_Id] FOREIGN KEY([documentID])
REFERENCES [dbo].[Documents] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[documentClients] CHECK CONSTRAINT [FK_documentClients_Documents_Id]
GO

USE [wfj_testdb]
GO

ALTER TABLE [dbo].[Users]  WITH NOCHECK ADD  CONSTRAINT [FK_User_UserManagerUserID] FOREIGN KEY([ManagerUserID])
REFERENCES [dbo].[Users] ([UserID])
GO

ALTER TABLE [dbo].[Documents]
ALTER COLUMN StateCodeID int

ALTER TABLE [dbo].[Documents]
ALTER COLUMN DocumentTypeID int;

USE [wfj_testdb]
GO

ALTER TABLE [dbo].[Documents]  WITH CHECK ADD  CONSTRAINT [FK_Documents_Codes] FOREIGN KEY([StateCodeID])
REFERENCES [dbo].[Codes] ([ID])
GO

ALTER TABLE [dbo].[Documents] CHECK CONSTRAINT [FK_Documents_Codes]
GO

USE [wfj_testdb]
GO

ALTER TABLE [dbo].[Documents]  WITH CHECK ADD  CONSTRAINT [FK_Documents_Codes1] FOREIGN KEY([DocumentTypeID])
REFERENCES [dbo].[Codes] ([ID])
GO

ALTER TABLE [dbo].[Documents] CHECK CONSTRAINT [FK_Documents_Codes1]
GO

DROP INDEX [IX_Documents_2] ON [dbo].[Documents]
GO

ALTER TABLE [dbo].[Documents]
DROP COLUMN StateCode;
GO

update Forms set ClientId = null where clientId not in (select ID from Clients)
update Forms set FormTypeId = null where FormTypeId not in (select FormTypeId from FormTypes)
update Requests set FormID = null where FormID not in (select ID from Forms)

ALTER TABLE Forms
ADD FOREIGN KEY (ClientID) REFERENCES Clients(ID);
ALTER TABLE Forms
ADD FOREIGN KEY (FormTypeId) REFERENCES FormTypes(FormTypeId);
ALTER TABLE Requests
ADD FOREIGN KEY (FormID) REFERENCES Forms(ID);

--select id from forms where clientid not in (select id from clients)

--select id from requests where FormID not in (select id from forms)


update [PersonnelClients] set PersonnelID = null where PersonnelID not in (select ID from [Personnel])

ALTER TABLE [PersonnelClients]
ADD FOREIGN KEY (PersonnelID) REFERENCES [Personnel](ID);

update FormSelectionLists set FormFieldID = null where FormFieldID not in (select ID from [FormFields])

ALTER TABLE FormSelectionLists
ADD FOREIGN KEY (FormFieldID) REFERENCES [FormFields](ID);


ALTER TABLE FormFields
ADD FOREIGN KEY (fieldSizeID) REFERENCES [FormFields](fieldSizeID);

UPDATE [dbo].[fieldSizes]
   SET [htmlCode] = 'col-12'
 WHERE fieldSizeID=0
GO
UPDATE [dbo].[fieldSizes]
   SET [htmlCode] = 'col-12'
 WHERE fieldSizeID=4
GO
UPDATE [dbo].[fieldSizes]
   SET [htmlCode] = 'col-3'
 WHERE fieldSizeID=1
GO
UPDATE [dbo].[fieldSizes]
   SET [htmlCode] = 'col-4'
 WHERE fieldSizeID=2
GO
UPDATE [dbo].[fieldSizes]
   SET [htmlCode] = 'col-6'
 WHERE fieldSizeID=3
GO


update [FormData] set FormFieldID = null where FormFieldID not in (select ID from [FormFields])
update FormAddressData set FormFieldID = null where FormFieldID not in (select ID from [FormFields])

ALTER TABLE [FormData]
ADD FOREIGN KEY (FormFieldID) REFERENCES [FormFields](ID);
ALTER TABLE FormAddressData
ADD FOREIGN KEY (FormFieldID) REFERENCES [FormFields](ID);

ALTER TABLE Requests
ADD FOREIGN KEY (Requestor) REFERENCES [Users](UserID);

ALTER TABLE Requests
ADD FOREIGN KEY (AssignedCollectorID) REFERENCES [Users](UserID);

ALTER TABLE Requests
ADD FOREIGN KEY (AssignedAttorney) REFERENCES [Personnel](ID);
