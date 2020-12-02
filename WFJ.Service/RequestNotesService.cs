using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WFJ.Helper;
using WFJ.Models;
using WFJ.Repository;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;
using WFJ.Service.Interfaces;
using WFJ.Service.Model;

namespace WFJ.Service
{
    public class RequestNotesService : IRequestNotesService
    {
        IRequestNotesRepository _notesRepo = new RequestNotesRepository();
        IHiddenRequestNotesRepository _hiddenNotesRepo = new HiddenRequestNotesRepository();
        IRequestsRepository _requestsRepo = new RequestsRepository();
        IUserRepository _userRepo = new UserRepository();
        IPersonnelsRepository _personalRepo = new PersonnelsRepository();
        IStatusCodesRepository _statusCodesRepo = new StatusCodesRepository();
        ICodesRepository _codesRepo = new CodesRepository();

        public RequestNotesGrid GetNotesGrid(int loginUserId, UserType userType, int requestId, DataTablesParam param, string sortDir, string sortCol, int pageNo)
        {
            RequestNotesGrid model = new RequestNotesGrid();

            //	Internal Notes (requestNotes.InternalNote = 1) should only be displayed for WFJ users.
            bool includeInternal = userType == UserType.SystemAdministrator;

            var requests = _notesRepo.GetRequestNotes(requestId, includeInternal);
            model.totalNotesCount = requests?.Count();


            if (requests != null)
            {
                var list1 = requests.Select(x => new RequestNoteModel
                {
                    SendToAuthorOnly = x.SendToAuthorOnly,
                    AuthorName = x.User?.FirstName + " " + x.User?.LastName,
                    dateReminderSent = x.dateReminderSent,
                    deadlineCalendar = x.deadlineCalendar,
                    flaggedNote = x.flaggedNote,
                    FollowupDate = x.FollowupDate != null ? x.FollowupDate.Value.ToString("MM/dd/yyyy") : null,
                    ID = x.ID,
                    internalNote = x.internalNote,
                    IsHiddenNote = x.hiddenRequestNotes.Any(a => a.userID == loginUserId),
                    Notes = x.Notes,
                    NotesDate = x.NotesDate != null ? x.NotesDate.Value.ToString("MM/dd/yyyy") : null,
                    ReminderSent = x.ReminderSent,
                    RequestID = x.RequestID,
                    UserID = x.UserID,
                    NotesDateVal = x.NotesDate
                });

                // Omit from the display any records in the hiddenRequestNotes table.
                // Flagged Notes (requestNotes.flaggedNote = 1) appear at the top of the list.
                model.hiddenNotesCount = list1.Where(x => x.IsHiddenNote == true).Count();
                list1 = list1.Where(x => x.IsHiddenNote == false).OrderByDescending(x => x.flaggedNote).ThenByDescending(x => x.NotesDateVal); //	Display the notes in reverse NotesDate order (latest note first


                model.notes = list1.Skip((pageNo - 1) * param.iDisplayLength).Take(param.iDisplayLength).ToList();
            }
            else
            {
                model.notes = new List<RequestNoteModel>();
            }

            return model;
        }


        public List<SelectListItem> GetSendToDropdown(int FormId, int requestId)
        {
            List<SelectListItem> dropdown = new List<SelectListItem>();

            // 1) all users with an email address in the users table for the placement form’s client
            IFormService _formService = new FormService();
            var formClientUsers = _formService.GetUsersByFormID(FormId).Where(x => x.User.EMail != null).Select(x => new SelectListItem
            {
                Text = x.User.FirstName + " " + x.User.LastName,
                Value = x.User.EMail
            }).Where(x => x.Text.Trim() != "").OrderBy(x => x.Text).ToList();
            dropdown.AddRange(formClientUsers);


            // 2) all users in the formNotesUsers table (where formNotesUsers.formID is the placement form), 
            IFormNotesUsersRepository _formNotesUsersRepo = new FormNotesUsersRepository();
            var notesUsers = _formNotesUsersRepo.GetUsersByFormID(FormId).Where(x => x.User.EMail != null).OrderBy(x => x.SeqNo).Select(x => new SelectListItem
            {
                Text = x.User.FirstName + " " + x.User.LastName,
                Value = x.User.EMail
            }).Where(x => x.Text.Trim() != "").ToList();
            dropdown.AddRange(notesUsers);


            // 3) the Assigned Collector (requests.assignedCollectorID)
            var request = _requestsRepo.GetById(requestId);
            if(request.AssignedCollectorID != null)
            {
                var collector = _userRepo.GetById(request.AssignedCollectorID);
                if (collector != null && collector.EMail != null)
                    dropdown.Add(new SelectListItem { Text = collector.FirstName + " " + collector.LastName, Value = collector.EMail });
            }

            // 4) the Assigned Attorney (requests.assignedAttorney)—look up the Attorney’s Name in the personnel table where personnel.ID = requests.assignedAttorney.
            if (request.AssignedAttorney != null)
            {
                var attorney = _personalRepo.GetById(request.AssignedAttorney);
                if (attorney != null && attorney.EMail != null)
                    dropdown.Add(new SelectListItem { Text = attorney.FirstName + " " + attorney.LastName, Value = attorney.EMail });
            }

            return dropdown;
        }


        public void FlagUnflagNotes(int requestId, IEnumerable<int> noteIds)
        {
            var notes = _notesRepo.GetRequestNotes(requestId, true);
            foreach(var item in noteIds)
            {
                var note = notes.First(x => x.ID == item);
                note.flaggedNote = note.flaggedNote == 1 ? (byte?)null : 1;
                _notesRepo.Update(note);
            }
        }

        public void AddHiddenNotes(int userId, int requestId, IEnumerable<int> noteIds)
        {
            List<hiddenRequestNote> hiddenNotes = new List<hiddenRequestNote>();
            foreach(var item in noteIds)
            {
                hiddenRequestNote hnote = new hiddenRequestNote
                {
                    noteID = item,
                    requestID = requestId,
                    userID = userId
                };

                hiddenNotes.Add(hnote);
            }

            _hiddenNotesRepo.AddList(hiddenNotes);
        }

        public void RemoveUserHiddenNotes(int userId, int requestId)
        {
            var hiddenNotes = _hiddenNotesRepo.GetHiddenNotesByUserAndRequestId(userId, requestId);
            _hiddenNotesRepo.RemoveList(hiddenNotes);
        }

        public AddRequestNoteViewModel GetEditRequestNote(int noteId)
        {
            var note = _notesRepo.GetById(noteId);
            AddRequestNoteViewModel requestNote = new AddRequestNoteViewModel
            {
                AuthorID = note.UserID,
                SendToAuthorOnly = note.SendToAuthorOnly,
                ID = note.ID,
                internalNote = note.internalNote,
                FollowupDate = note.FollowupDate != null ? note.FollowupDate.Value.ToString("MM/dd/yyyy") : null,
                NotesDate = note.NotesDate != null ? note.NotesDate.Value.ToString("MM/dd/yyyy") : null,
                Notes = note.Notes,
                RequestID = note.RequestID,
                SelectedFollowUpTime = note.FollowupDate != null ? note.FollowupDate.Value.ToString("hh:mm tt") : null
            };

            return requestNote;
        }

        public void AddUpdateRequestNote(AddRequestNoteViewModel model)
        {

            // email code pending
            DateTime? followupDate = string.IsNullOrEmpty(model.FollowupDate) ? (DateTime?)null : Convert.ToDateTime(model.FollowupDate);
            DateTime? notedate = string.IsNullOrEmpty(model.NotesDate) ? (DateTime?)null : DateTime.ParseExact(model.NotesDate, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            if (model.ID == 0)
            {
                var request = _requestsRepo.GetRequestWithDetail(model.RequestID.Value);
                string status = "";
                if (request.StatusCode != null)
                {
                    var statuscode = _statusCodesRepo.GetByStatusCodeAndFormId(request.StatusCode.Value, request.FormID.Value);
                    status = statuscode.Description;
                }

                //If the note author is a client user, send the Assigned Attorney the note via email.  If there is no Assigned Attorney, send the note to UserID = 156.
                var author = _userRepo.GetById(model.AuthorID);
                if (author.UserType == (byte)UserType.ClientUser)
                {
                    var attorneyId = request.AssignedAttorney;
                    if (attorneyId == null)
                        attorneyId = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultAttorneyIDForAddNote"]);

                    var attorneyUser = _personalRepo.GetById(attorneyId);
                    string email = attorneyUser.EMail;
                    if (attorneyUser != null && email != null && Util.ValidateEmail(email))
                    {
                        string baseUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace("RequestNotes/AddNote", "");
                        string queryString = baseUrl + "/Placements/AddPlacement?value=" + Util.Encode(request.FormID + "|" + request.ID);
                        string subject = "WFJ Notes";
                        string dirpath = HttpContext.Current.Server.MapPath("/EmailTemplate");
                        string xlsTemplatePath = dirpath + "/RequestNotes.html";
                        string emailTemplate = File.ReadAllText(xlsTemplatePath);

                        StringBuilder sb1 = new StringBuilder();
                        sb1.Append(emailTemplate);
                        sb1.Replace("[RequestUrl]", queryString);
                        sb1.Replace("[baseurl]", baseUrl);
                        sb1.Replace("[Status]", status);
                        sb1.Replace("[Requestor]", request.User1 != null ? request.User1.FirstName + " " + request.User1.LastName : "" );
                        //sb1.Replace("[Collector]", request.User != null ? request.User.FirstName + " " + request.User.LastName : "" );
                        sb1.Replace("[Attorney]", request.Personnel != null ? request.Personnel.FirstName + " " + request.Personnel.LastName : "");

                        // Need to implement
                        sb1.Replace("[CustomerName]", "");
                        sb1.Replace("[CustomerAccount]", "");
                        sb1.Replace("[CollectMaxNo]", "");
                        sb1.Replace("[WFJFileNo]", "");

                        // Bind Notes
                        string xlsTemplatePath2 = dirpath + "/RequestNotesList.html";
                        string noteHtml = File.ReadAllText(xlsTemplatePath2);
                        noteHtml = noteHtml.Replace("[NoteDate]", model.NotesDate).Replace("[Author]", author.FirstName + " " + author.LastName)
                                            .Replace("[Notes]", model.Notes);

                        sb1.Replace("[NotesList]", noteHtml);


                        EmailHelper.SendMail(email, subject, sb1.ToString());
                    }
                    else
                    {
                        throw new Exception("Not a valid attorney found");
                    }

                }


                // if email sent successfylly add note

                RequestNote note = new RequestNote
                {
                    SendToAuthorOnly = model.SendToAuthorOnly,
                    FollowupDate = followupDate,
                    NotesDate = notedate,
                    internalNote = model.internalNote,
                    RequestID = model.RequestID,
                    Notes = model.Notes,
                    UserID = model.AuthorID,

                };
                _notesRepo.Add(note);

                request.LastViewed = DateTime.Now;
                request.LastNoteDate = DateTime.Now.Date;
                _requestsRepo.Update(request);

            }
            else
            {
                var note = _notesRepo.GetById(model.ID);
                note.FollowupDate = followupDate;
                note.NotesDate = notedate;
                note.internalNote = model.internalNote;
                note.Notes = model.Notes;
                note.SendToAuthorOnly = model.SendToAuthorOnly;
                note.UserID = model.AuthorID;
                _notesRepo.Update(note);
            }

        }

        public void DeleteRequestNote(int noteId)
        {
            _notesRepo.Remove(noteId);
        }


        public void SendNotes(int requestId, List<int> notes, List<string> users)
        {
            var request = _requestsRepo.GetRequestWithDetail(requestId);
            string status = "";
            if (request.StatusCode != null)
            {
                var statuscode = _statusCodesRepo.GetByStatusCodeAndFormId(request.StatusCode.Value, request.FormID.Value);
                status = statuscode.Description;
            }
            string baseUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace("RequestNotes/SendNotes", "");
            string queryString = baseUrl + "/Placements/AddPlacement" + "?" + Util.Encode("requestId=" + request.ID + "&formId=" + request.FormID);
            string subject = "WFJ Notes";
            string dirpath = HttpContext.Current.Server.MapPath("/EmailTemplate");
            string xlsTemplatePath = dirpath + "/RequestNotes.html";
            string emailTemplate = File.ReadAllText(xlsTemplatePath);

            StringBuilder sb1 = new StringBuilder();
            sb1.Append(emailTemplate);
            sb1.Replace("[RequestUrl]", queryString);
            sb1.Replace("[baseurl]", baseUrl);
            sb1.Replace("[Status]", status);
            sb1.Replace("[Requestor]", request.User1 != null ? request.User1.FirstName + " " + request.User1.LastName : "");
            //sb1.Replace("[Collector]", request.User != null ? request.User.FirstName + " " + request.User.LastName : "" );
            sb1.Replace("[Attorney]", request.Personnel != null ? request.Personnel.FirstName + " " + request.Personnel.LastName : "");

            // Need to implement
            sb1.Replace("[CustomerName]", "");
            sb1.Replace("[CustomerAccount]", "");
            sb1.Replace("[CollectMaxNo]", "");
            sb1.Replace("[WFJFileNo]", "");




            var notesFromDb = _notesRepo.GetRequestNotes(requestId, true);
            string xlsTemplatePath2 = dirpath + "/RequestNotesList.html";
            string notesList = "";

            StringBuilder sb = new StringBuilder();
            foreach(var id in notes)
            {
                var note = notesFromDb.First(x => x.ID == id);
                var author = _userRepo.GetById(note.UserID);
                string noteHtml = File.ReadAllText(xlsTemplatePath2);
                noteHtml = noteHtml.Replace("[NoteDate]", note.NotesDate != null ? note.NotesDate.Value.ToString("MM/dd/yyyy"):"")
                                   .Replace("[Author]", author.FirstName + " " + author.LastName)
                                    .Replace("[Notes]", note.Notes);
                notesList = notesList + noteHtml;
            }

            sb1.Replace("[NotesList]", notesList);
            string noteEmail = sb1.ToString();

            foreach (var email in users)
            {
                if (Util.ValidateEmail(email))
                {
                    EmailHelper.SendMail(email, subject, noteEmail);
                }
            }

            string userEmails = string.Join(",", users);
            foreach (var id in notes)
            {
                var note = notesFromDb.First(x => x.ID == id);
                note.LastSent = DateTime.Now;
                note.LastSentTo = userEmails;
                _notesRepo.Update(note);
            }

        }

        public List<SelectListItem> GetFollowUpTime()
        {
            var sTime = ConfigurationManager.AppSettings["FollowUpStartTime"].ToString();
            var eTime = ConfigurationManager.AppSettings["FollowUpEndTime"].ToString();
            var slot = Convert.ToInt32(ConfigurationManager.AppSettings["FollowUpSlot"]);
            DateTime startTime = Convert.ToDateTime(DateTime.Now.Date.ToString("dd/MM/yyyy") + " " + sTime);
            DateTime endTime = Convert.ToDateTime(DateTime.Now.Date.ToString("dd/MM/yyyy") + " " + eTime);
            List<SelectListItem> followupTimes = new List<SelectListItem>();
            while (startTime <= endTime)
            {
                followupTimes.Add(new SelectListItem { Text = startTime.ToString("hh:mm tt"), Value = startTime.ToString("hh:mm tt") });
                startTime = startTime.AddMinutes(slot);
            }
            return followupTimes;
        }

        public List<SelectListItem> GetStandardNotes()
        {
            var codes = _codesRepo.GetAllByType("STDNOTE");
            List<SelectListItem> standardCodes = new List<SelectListItem>();
            foreach (var code in codes)
            {
                standardCodes.Add(new SelectListItem { Text = code.Value, Value = code.ID.ToString() });
            }
            return standardCodes;
        }
    }
}
