//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WFJ.Repository.EntityModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class RequestNote
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RequestNote()
        {
            this.hiddenRequestNotes = new HashSet<hiddenRequestNote>();
        }
    
        public int ID { get; set; }
        public string RequestType { get; set; }
        public Nullable<int> RequestID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> contactTypeID { get; set; }
        public Nullable<System.DateTime> NotesDate { get; set; }
        public string Notes { get; set; }
        public Nullable<System.DateTime> FollowupDate { get; set; }
        public Nullable<byte> ReminderSent { get; set; }
        public Nullable<System.DateTime> dateReminderSent { get; set; }
        public Nullable<byte> SendToAuthorOnly { get; set; }
        public Nullable<byte> flaggedNote { get; set; }
        public Nullable<byte> deadlineCalendar { get; set; }
        public Nullable<byte> internalNote { get; set; }
        public Nullable<System.DateTime> LastSent { get; set; }
        public string LastSentTo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hiddenRequestNote> hiddenRequestNotes { get; set; }
        public virtual Request Request { get; set; }
        public virtual User User { get; set; }
    }
}
