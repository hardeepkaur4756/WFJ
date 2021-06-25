using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WFJ.Models;
using WFJ.Repository.EntityModel;
using WFJ.Service.Model;

namespace WFJ.Service.Interfaces
{
    public interface IRequestNotesService
    {
        RequestNotesGrid GetNotesGrid(int loginUserId, UserType userType, int requestId, DataTablesParam param, string sortDir, string sortCol, int pageNo);
        List<SelectListItem> GetSendToDropdown(int formId, int requestId);
        void FlagUnflagNotes(int requestId, IEnumerable<int> noteIds);

        void AddHiddenNotes(int userId, int requestId, IEnumerable<int> noteIds);
        void RemoveUserHiddenNotes(int userId, int requestId);
        AddRequestNoteViewModel GetEditRequestNote(int noteId);
        void AddUpdateRequestNote(AddRequestNoteViewModel model);
        void DeleteRequestNote(int noteId);
        void SendNotes(int requestId, List<int> notes, List<string> users);
        List<SelectListItem> GetFollowUpTime();
        List<SelectListItem> GetStandardNotes();

        void UpdateAlreadySeenStatus(int requestId);
    }

}
