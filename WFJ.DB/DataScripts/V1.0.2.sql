-- Request Notes
Delete [hiddenRequestNotes] where noteID not in (select id from RequestNotes)

ALTER TABLE [hiddenRequestNotes]
ADD FOREIGN KEY (noteID) REFERENCES [RequestNotes](ID);



ALTER TABLE [FormNotesUsers]
ADD FOREIGN KEY (UserID) REFERENCES [Users](UserID);



ALTER TABLE RequestNotes
ADD LastSent datetime null;
ALTER TABLE RequestNotes
ADD LastSentTo varchar(max) null;
