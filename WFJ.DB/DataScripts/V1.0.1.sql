 -- For Regions dropdown
update [wfj_testdb].[dbo].[Levels] set ParentID= 0 where ParentID not in (select id from Levels)

ALTER TABLE [wfj_testdb].[dbo].[Levels]
ADD FOREIGN KEY (ParentID) REFERENCES [wfj_testdb].[dbo].[Levels](ID);


Update Requests set LevelID=null where LevelID not in (select id from levels)

ALTER TABLE Requests
ADD FOREIGN KEY (LevelID) REFERENCES [Levels](ID);
