GO
update RequestNotes set UserID = null where ID in( select ID from RequestNotes where UserID not in (select UserID from Users))
GO
update RequestNotes set RequestID = null where ID in(select ID from RequestNotes where RequestID not in (select ID from Requests))
GO
ALTER TABLE [dbo].[RequestNotes]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[RequestNotes]  WITH CHECK ADD FOREIGN KEY([RequestID])
REFERENCES [dbo].[Requests] ([ID])
GO
