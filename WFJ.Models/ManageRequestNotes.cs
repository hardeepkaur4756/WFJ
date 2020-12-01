using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WFJ.Models
{
    public class RequestNotesGrid
    {
        public int? totalNotesCount { get; set; }
        public int hiddenNotesCount { get; set; }
        public List<RequestNoteModel> notes { get; set; }
    }

    public class RequestNoteModel
    {
        public int ID { get; set; }
        public string RequestType { get; set; }
        public Nullable<int> RequestID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> contactTypeID { get; set; }
        //public Nullable<System.DateTime> NotesDate { get; set; }
        public string Notes { get; set; }
        //public Nullable<System.DateTime> FollowupDate { get; set; }
        public Nullable<byte> ReminderSent { get; set; }
        public Nullable<System.DateTime> dateReminderSent { get; set; }
        public Nullable<byte> SendToAuthorOnly { get; set; }
        public Nullable<byte> flaggedNote { get; set; }
        public Nullable<byte> deadlineCalendar { get; set; }
        public Nullable<byte> internalNote { get; set; }

        public DateTime? NotesDateVal { get; set; }
        public DateTime? FollowupDateVal { get; set; }

        public bool IsHiddenNote { get; set; }
        public string AuthorName { get; set; }
        public string NotesDate { get; set; }
        public string FollowupDate { get; set; }
    }

    public class AddRequestNoteViewModel
    {
        public int ID { get; set; }
        [Required]
        public Nullable<int> RequestID { get; set; }
        [Required]
        public Nullable<int> AuthorID { get; set; }
        [Required]
        public string Notes { get; set; }

        public Nullable<byte> SendToAuthorOnly { get; set; }
        public Nullable<byte> flaggedNote { get; set; }
        public Nullable<byte> deadlineCalendar { get; set; }
        public Nullable<byte> internalNote { get; set; }

        //public bool IsHiddenNote { get; set; }
        //public string AuthorName { get; set; }
        [Required]
        public string NotesDate { get; set; }
        public string FollowupDate { get; set; }
        public List<SelectListItem> Authors { get; set; }
        public List<SelectListItem> FollowUpTimes { get; set; }
        public string SelectedFollowUpTime { get; set; }
        public List<SelectListItem> StandardNotes { get; set; }
        public string StandardNoteId { get; set; }
    }


}
