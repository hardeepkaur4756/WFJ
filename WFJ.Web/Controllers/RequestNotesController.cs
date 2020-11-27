using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WFJ.Helper;
using WFJ.Models;
using WFJ.Service;
using WFJ.Service.Interfaces;
using WFJ.Service.Model;

namespace WFJ.Web.Controllers
{
    [CustomAttribute.AuthorizeActivity((int)Web.Models.Enums.UserType.None)]
    public class RequestNotesController : Controller
    {
        private IErrorLogService _errorLogService = new ErrorLogService();
        IRequestNotesService _notesService = new RequestNotesService();
        

        private int UserType = 0;
        private int UserId = 0;
        private int? UserAccess;


        public JsonResult GetRequestNotesList(DataTablesParam param, string sortDir, string sortCol, int requestId)
        {
            try
            {
                GetSessionUser(out UserId, out UserType, out UserAccess);

                RequestNotesGrid model = new RequestNotesGrid();
                int pageNo = 1;
                if (param.iDisplayStart >= param.iDisplayLength)
                    pageNo = (param.iDisplayStart / param.iDisplayLength) + 1;


                model = _notesService.GetNotesGrid(UserId, (UserType)((byte)UserType), requestId, param, sortDir, sortCol, pageNo);


                return Json(new
                {
                    aaData = model.notes,
                    param.sEcho,
                    iTotalDisplayRecords = model.totalNotesCount,
                    iTotalRecords = model.totalNotesCount,

                    hiddenNotesCount = model.hiddenNotesCount,
                    Success = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "RequestNotes/GetRequestNotesList?requestId=" + requestId, CreatedBy = UserId, CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return Json(new { Message = "Sorry, An error occurred!", Success = false });
            }
        }



        [HttpPost]
        public ActionResult HideNotes(List<int> notes, int requestId)
        {
            bool isSuccess = false;
            try
            {
                GetSessionUser(out UserId, out UserType, out UserAccess);
                _notesService.AddHiddenNotes(UserId, requestId, notes);

                isSuccess = true;
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "RequestNotes/HideNotes", CreatedBy = UserId, CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
            }

            return Json(new { success = isSuccess });
        }

        [HttpPost]
        public ActionResult FlagUnflagNotes(List<int> notes, int requestId)
        {
            bool isSuccess = false;
            try
            {
                _notesService.FlagUnflagNotes(requestId, notes);

                isSuccess = true;
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "RequestNotes/FlagUnflagNotes", CreatedBy = UserId, CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
            }

            return Json(new { success = isSuccess });
        }

        [HttpPost]
        public ActionResult ShowNotes(int requestId)
        {
            bool isSuccess = false;
            try
            {
                GetSessionUser(out UserId, out UserType, out UserAccess);
                _notesService.RemoveUserHiddenNotes(UserId, requestId);

                isSuccess = true;
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "RequestNotes/ShowNotes", CreatedBy = UserId, CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
            }

            return Json(new { success = isSuccess });
        }

        [HttpPost]
        public ActionResult DeleteNote(int noteId)
        {
            bool isSuccess = false;
            try
            {
                GetSessionUser(out UserId, out UserType, out UserAccess);

                if (UserType == (byte)WFJ.Service.Model.UserType.SystemAdministrator)
                _notesService.DeleteRequestNote(noteId);

                isSuccess = true;
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "RequestNotes/DeleteNote", CreatedBy = UserId, CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
            }

            return Json(new { success = isSuccess });
        }


        public ActionResult AddEditNote(int? noteId)
        {
            try
            {
                GetSessionUser(out UserId, out UserType, out UserAccess);
                IUserService _userService = new UserService();
                var user = _userService.GetById(UserId);
                ViewData["LoginUserName"] = user.FirstName + " " + user.LastName;

                AddRequestNoteViewModel model = new AddRequestNoteViewModel
                {
                    NotesDate = DateTime.Now.ToString("MM/dd/yyyy"),
                    AuthorID = UserId,
                };
                if (noteId != null)
                {
                    model = _notesService.GetEditRequestNote(noteId.Value);
                }

                return Json(new { Success = true, Html = this.RenderPartialViewToString("_AddRequestNotes", model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "RequestNotes/AddEditNote", CreatedBy = UserId, CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return Json(new { Message = "Sorry, An error occurred!", Success = false });
            }

        }

        [HttpPost]
        public ActionResult AddNote(AddRequestNoteViewModel model)
        {
            bool isSuccess = false;
            string errorMessage = "";

            if (ModelState.IsValid)
            {
                try
                {
                    GetSessionUser(out UserId, out UserType, out UserAccess);
                    _notesService.AddUpdateRequestNote(model);
                    //_notesService.RemoveUserHiddenNotes(UserId, requestId);

                    isSuccess = true;
                }
                catch (Exception ex)
                {
                    _errorLogService.Add(new ErrorLogModel() { Page = "RequestNotes/AddNote", CreatedBy = UserId, CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                }
            }
            else
            {
                errorMessage = "Form is not valid. Please add mandatory fields";
            }


            return Json(new { success = isSuccess, errorMessage = errorMessage });
        }

        [HttpPost]
        public ActionResult SendNotes(List<int> notes, List<string> users, int requestId)
        {
            bool isSuccess = false;
            try
            {
                _notesService.SendNotes(requestId, notes, users);

                isSuccess = true;
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "RequestNotes/SendNotes", CreatedBy = UserId, CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
            }

            return Json(new { success = isSuccess });
        }





        public void GetSessionUser(out int userId, out int userType, out int? userAccess)
        {
            if (Session["UserId"] != null)
            {
                userId = Convert.ToInt32(Session["UserId"].ToString());
                userType = Convert.ToInt32(Session["UserType"].ToString());
                userAccess = Session["UserAccess"] != null ? Convert.ToInt32(Session["UserAccess"].ToString()) : (int?)null;
            }
            else
            {
                userId = 0;
                userType = 0;
                userAccess = 0;
            }
        }


    }
}