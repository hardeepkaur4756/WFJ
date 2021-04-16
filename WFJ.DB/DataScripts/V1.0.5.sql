Go
alter table [dbo].[Payments]
add WFJReferenceNumber varchar(20) NULL

alter table [dbo].[Payments]
add WFJReferenceDate SmallDateTime NULL

alter table [dbo].[Payments]
add WFJInvoiceDatePaid SmallDateTime NULL

Alter table Clients
add clientPaymentNotes varchar(8000) NULL

----Just run below queries on live DB (Above columns are already in live DB)

ALTER TABLE [dbo].[Payments]
ADD CONSTRAINT FK_Payment_Request
FOREIGN KEY (RequestID) REFERENCES Requests(ID);

ALTER TABLE [dbo].[Payments]
ADD CONSTRAINT FK_Payment_User
FOREIGN KEY (UserID) REFERENCES Users(UserID);

ALTER TABLE [dbo].[Payments]
ADD CONSTRAINT FK_Payment_PaymentType
FOREIGN KEY (PaymentTypeID) REFERENCES PaymentTypes(ID);

ALTER TABLE [dbo].[Payments]
ADD CONSTRAINT FK_Payment_Currency
FOREIGN KEY (currencyID) REFERENCES currencies(currencyID);

Alter table [dbo].[RequestNotes]
Add paymentDate smallDateTime NULL

update PersonnelRequests set requestId = NULL where requestId not in (select id from Requests)
select * from PersonnelRequests WHERE requestId not in (select id from Requests)
update PersonnelRequests set FirmID = NULL where FirmID not in (select FirmID from AssociateCounsel)
select * from PersonnelRequests WHERE FirmID  not in (select FirmID from AssociateCounsel)

ALTER TABLE [dbo].[PersonnelRequests]
ADD CONSTRAINT FK_PersonnelRequest_Request
FOREIGN KEY (RequestID) REFERENCES Requests(Id);

ALTER TABLE [dbo].[PersonnelRequests]
ADD CONSTRAINT FK_PersonnelRequest_AssociateCounsel
FOREIGN KEY (firmId) REFERENCES AssociateCounsel(firmId);