using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Models;
using WFJ.Repository;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;
using WFJ.Service.Interfaces;
using WFJ.Service.Model;

namespace WFJ.Service
{
    public class RequestsService : IRequestsService
    {
        IRequestsRepository _requestsRepo = new RequestsRepository();
        IFormFieldsRepository _formFieldRepo = new FormFieldsRepository();
        IListFieldRepository _listFieldRepo = new ListFieldRepository();
        //IStatusCodesRepository _statusCodesRepo = new StatusCodesRepository();
        public RequestViewModel GetByRequestId(int RequestID)
        {
            var request =_requestsRepo.GetById(RequestID);
            RequestViewModel model = new RequestViewModel
            {
                ID = RequestID,
                AssignedAttorney = request.AssignedAttorney,
                AssignedCollectorID = request.AssignedCollectorID,
                FormID = request.FormID,
                RequestDate = request.RequestDate,
                Requestor = request.Requestor,
                StatusCode = request.StatusCode,
                RequestDateString = request.RequestDate != null ? request.RequestDate.Value.ToString("MM/dd/yyyy") : null,
                CompletionDateString = request.CompletionDate != null ? request.CompletionDate.Value.ToString("MM/dd/yyyy"): null,
                active = request.active == null ? 1 : request.active
            };

            return model;
        }

        public PlacementRequestsListViewModel GetPlacementRequests(int userId, int formId, UserType UserType, int requestor, int assignedAttorney, int collector, int status, string region,
                                                                    string startDate, string toDate, bool archived,
                                                                    DataTablesParam param, string sortDir, string sortCol, int pageNo)
        {
            PlacementRequestsListViewModel model = new PlacementRequestsListViewModel();

            DateTime? beginDate = string.IsNullOrEmpty(startDate) ? (DateTime?)null : DateTime.ParseExact(startDate, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime? endDate = string.IsNullOrEmpty(toDate) ? (DateTime?)null : DateTime.ParseExact(toDate, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date.AddDays(1);

            //Initial State -> List all active requests for the Form selected.  
            //An active request is determined by looking at the statusCodes.statusLevel field (requests.statusCode  statusCodes.statusCode + statusCodes.formID).
            //Status Levels: 0-New, 1-Active, 2-Completed
            int statusLevel = status != -1 ? -1 : 1;

            var requests = _requestsRepo.GetRequestsList(formId, requestor, assignedAttorney, collector, status, statusLevel, beginDate, endDate, archived);
            model.TotalRequestsCount = requests?.Count();

            if (requests != null)
            {
                DateTime NowDate = DateTime.Now.Date;
                IStatusCodesRepository statusCodesRepo = new StatusCodesRepository();
                var StatusList = statusCodesRepo.GetByFormID(formId);

                var formData = _formFieldRepo.GetFormFieldsByFormID(formId).SelectMany(x => x.FormDatas).ToList();
                var formAddressData = _formFieldRepo.GetFormFieldsByFormID(formId).SelectMany(x => x.FormAddressDatas).ToList();
                var selectionColumns = GetSelectionColumnsByUserType(UserType);

                var list1 = requests.Select(x => new PlacementRequestModel
                {
                    RequestID = x.ID,
                    AssignedAttorney = x.Personnel != null ? x.Personnel.FirstName + " " + x.Personnel.LastName : null,
                    Requestor = x.User1 != null ? x.User1.FirstName + " " + x.User1.LastName : null,
                    AssignedCollectorID = x.User != null ? x.User.FirstName + " " + x.User.LastName : null,
                    CompletionDateVal = x.CompletionDate,
                    RequestDateVal = x.RequestDate,
                    CompletionDate = x.CompletionDate != null ? x.CompletionDate.Value.ToString("MM/dd/yyyy") : null,
                    RequestDate = x.RequestDate != null ? x.RequestDate.Value.ToString("MM/dd/yyyy") : null,
                    TotalPayments = (float)(x.TotalPayments != null ? x.TotalPayments : 0),
                    LastViewedVal = x.LastViewed,
                    LastViewed = x.LastViewed != null ? x.LastViewed.Value.ToString("MM/dd/yyyy") : null,
                    DaysOpen = x.RequestDate == null ? 0 :
                               x.CompletionDate == null ? (NowDate - x.RequestDate.Value).Days : (x.CompletionDate.Value - x.RequestDate.Value).Days,
                    StatusCode = x.StatusCode != null && StatusList.Any(s => s.StatusCode1 == x.StatusCode) ? StatusList.FirstOrDefault(s => s.StatusCode1 == x.StatusCode).Description : null,
                    LastNoteDate = x.LastNoteDate != null ? x.LastNoteDate.Value.ToString("MM/dd/yyyy") : null,

                    FormFields = formData.Where(f => f.RequestID == x.ID && f.FormField != null &&
                                                     (f.FormField.ListSeqNo != null && f.FormField.ListSeqNo != 0 && selectionColumns.Contains(f.FormField.SelectionColumn))
                                ).ToDictionary(d => "field_" + d.FormField.ID,

                                    // dropdown
                                    d => d.FormField.FieldTypeID == (byte)FieldTypes.SelectionList &&
                                    d.FormField.FormSelectionLists.FirstOrDefault(s => s.Code == d.FieldValue) != null ?
                                    d.FormField.FormSelectionLists.FirstOrDefault(s => s.Code == d.FieldValue).TextValue :

                                    // multi select checkbox
                                    d.FormField.FieldTypeID == (byte)FieldTypes.MultiSelectCheckboxes && d.FieldValue != null?
                                    string.Join(",", d.FormField.FormSelectionLists.Where(c => d.FieldValue.Split(',').Contains(c.Code)).Select(c => c.TextValue).ToArray())

                                    :
                                    // inputs
                                    d.FieldValue).Union(

                                // address
                                formAddressData.Where(f => f.RequestID == x.ID && f.FormField != null &&
                                                     (f.FormField.ListSeqNo != null && f.FormField.ListSeqNo != 0 && selectionColumns.Contains(f.FormField.SelectionColumn))

                                ).ToDictionary(d => "field_" + d.FormField.ID,
                                    d => (d.AddressLine1 + "<br>" + d.AddressLine2 + "<br>" + d.City + "," + d.State + "," + d.ZipCode + "," + d.Country).TrimEnd(',')
                                )).ToDictionary(d => d.Key, d=> d.Value)
                }).ToList();


                switch (sortCol)
                {
                    case "AssignedAttorneyName":
                        if (sortDir == "asc")
                        {
                            list1 = list1.OrderBy(x => x.AssignedAttorney).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            list1 = list1.OrderByDescending(x => x.AssignedAttorney).ToList();
                        }
                        break;
                    case "RequestorName":
                        if (sortDir == "asc")
                        {
                            list1 = list1.OrderBy(x => x.Requestor).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            list1 = list1.OrderByDescending(x => x.Requestor).ToList();
                        }
                        break;
                    case "CollectorName":
                        if (sortDir == "asc")
                        {
                            list1 = list1.OrderBy(x => x.AssignedCollectorID).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            list1 = list1.OrderByDescending(x => x.AssignedCollectorID).ToList();
                        }
                        break;
                    case "DaysOpen":
                        if (sortDir == "asc")
                        {
                            list1 = list1.OrderBy(x => x.DaysOpen).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            list1 = list1.OrderByDescending(x => x.DaysOpen).ToList();
                        }
                        break;
                    case "StatusDescription":
                        if (sortDir == "asc")
                        {
                            list1 = list1.OrderBy(x => x.StatusCode).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            list1 = list1.OrderByDescending(x => x.StatusCode).ToList();
                        }
                        break;
                    case "RequestDate":
                        if (sortDir == "asc")
                        {
                            list1 = list1.OrderBy(x => x.RequestDateVal).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            list1 = list1.OrderByDescending(x => x.RequestDateVal).ToList();
                        }
                        break;
                    case "CompletionDate":
                        if (sortDir == "asc")
                        {
                            list1 = list1.OrderBy(x => x.CompletionDateVal).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            list1 = list1.OrderByDescending(x => x.CompletionDateVal).ToList();
                        }
                        break;
                    case "LastViewed":
                        if (sortDir == "asc")
                        {
                            list1 = list1.OrderBy(x => x.LastViewedVal).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            list1 = list1.OrderByDescending(x => x.LastViewedVal).ToList();
                        }
                        break;
                    default:

                        if(sortDir == "asc")
                            list1 = list1.OrderBy(x => x.FormFields.ContainsKey(sortCol) && x.FormFields[sortCol] != null ? x.FormFields[sortCol] : null).ToList();
                        if(sortDir == "desc")
                            list1 = list1.OrderByDescending(x => x.FormFields.ContainsKey(sortCol) && x.FormFields[sortCol] != null ? x.FormFields[sortCol] : null).ToList();
                        break;
                }


                model.Requests = list1.Skip((pageNo - 1) * param.iDisplayLength).Take(param.iDisplayLength).ToList();
            }
            else
            {
                model.Requests = new List<PlacementRequestModel>();
            }


            return model;
        }

        public List<DatatableDynamicColumn> GetDatatableColumns(int UserId, int FormId, UserType UserType)
        {
            string dataFormat = "FormFields.field_";
            var selectionColumns = GetSelectionColumnsByUserType(UserType);
            var requestColumns = GetRequestColumns();
            var listFieds = _listFieldRepo.GetByUserAndFormID(UserId, FormId);
            var formFields = _formFieldRepo.GetFormFieldsByFormID(FormId).Where(x => selectionColumns.Contains(x.SelectionColumn));
            var columns = new List<DatatableDynamicColumn>();

            if (listFieds.Count() > 0)
            {
                columns = (from listf in listFieds

                           join r in requestColumns on listf.FieldID equals r.fieldID into re
                           from reqf in re.DefaultIfEmpty()

                           join f in formFields on listf.FieldID equals f.ID into ff
                           from formf in ff.DefaultIfEmpty()

                           select new DatatableDynamicColumn
                           {
                               data = reqf != null ? reqf.data :
                                      formf != null ? dataFormat + formf.ID : "",
                               fieldID = listf.FieldID != null ? listf.FieldID.Value : 0,
                               seqNo = listf.SeqNo != null ? listf.SeqNo :
                                       reqf != null ? reqf.seqNo :
                                       formf != null ? formf.ListSeqNo : null,
                               title = reqf != null ? reqf.title : formf != null ? formf.FieldName : "",
                               defaultContent = "",
                               visible = true
                           })
                               .OrderBy(x => x.seqNo != null ? x.seqNo : byte.MaxValue).ToList();
            }

            // Default visibe columns
            else
            {
                List<string> NamesFromWebConfig = ConfigurationManager.AppSettings["RequestsDatatableDefaultColumns"] != null ?
                                                  ConfigurationManager.AppSettings["RequestsDatatableDefaultColumns"].Split(',').ToList() : new List<string>();

                foreach(var item in NamesFromWebConfig)
                {
                    var requestColumn = requestColumns.FirstOrDefault(x => x.data.ToLower() == item);
                    if (requestColumn != null)
                    {
                        requestColumn.visible = true;
                        requestColumn.seqNo = 1;
                        columns.Add(requestColumn);
                    }
                }

                var FormFieldsNonZero = formFields.Where(x => x.ListSeqNo != null && x.ListSeqNo != 0).Select(x => new DatatableDynamicColumn
                {
                    data = dataFormat + x.ID,
                    defaultContent = "",
                    fieldID = x.ID,
                    seqNo = x.ListSeqNo,
                    title = x.FieldName,
                    visible = true
                });

                columns.AddRange(FormFieldsNonZero);
                columns = columns.OrderBy(x => x.seqNo).ToList();

            } // default columns end

            return columns;
        }


        public List<DatatableDynamicColumn> GetAllcolumns(int UserId, int FormId, UserType UserType)
        {
            var selectionColumns = GetSelectionColumnsByUserType(UserType);
            var visibleColumns = GetDatatableColumns(UserId, FormId, UserType);

            var requestColumns = GetRequestColumns().Select(x => new DatatableDynamicColumn
                                {
                                    fieldID = x.fieldID,
                                    title = x.title,
                                    visible = visibleColumns.Any(f => f.fieldID == x.fieldID),
                                    seqNo = visibleColumns.Any(f => f.fieldID == x.fieldID) ? visibleColumns.FirstOrDefault(f => f.fieldID == x.fieldID).seqNo : int.MaxValue
                                }).ToList();
            var formFields = _formFieldRepo.GetFormFieldsByFormID(FormId)
                                .Where(x => x.ListSeqNo != null && x.ListSeqNo != 0 && selectionColumns.Contains(x.SelectionColumn))
                                .Select(x => new DatatableDynamicColumn
                                {
                                    fieldID = x.ID,
                                    title = x.FieldName,
                                    visible = visibleColumns.Any(f => f.fieldID == x.ID),
                                    seqNo = visibleColumns.Any(f => f.fieldID == x.ID) ? visibleColumns.FirstOrDefault(f => f.fieldID == x.ID).seqNo : int.MaxValue
                                }).ToList();

            requestColumns.AddRange(formFields);

            return requestColumns.OrderBy(x => x.seqNo).ToList();
        }


        public List<DatatableDynamicColumn> GetRequestColumns()
        {
            List<DatatableDynamicColumn> columns1 = new List<DatatableDynamicColumn>();
            columns1.Add(new DatatableDynamicColumn
            {
                data = nameof(PlacementRequestModel.Requestor),
                title = "Requestor Name",
                fieldID = 1,
                //seqNo = 0,
            });
            columns1.Add(new DatatableDynamicColumn
            {
                data = nameof(PlacementRequestModel.RequestDate),
                title = "Request Date",
                fieldID = 2,
                //seqNo = 1,
            });
            columns1.Add(new DatatableDynamicColumn
            {
                data = nameof(PlacementRequestModel.AssignedAttorney),
                title = "Assigned Attorney",
                fieldID = 3,
                //seqNo = 2,
            });
            columns1.Add(new DatatableDynamicColumn
            {
                data = nameof(PlacementRequestModel.AssignedCollectorID),
                title = "Collector Name",
                fieldID = 4,
                //seqNo = 3,
            });
            columns1.Add(new DatatableDynamicColumn
            {
                data = nameof(PlacementRequestModel.StatusCode),
                title = "Status",
                fieldID = 5,
                //seqNo = 4,
            });
            columns1.Add(new DatatableDynamicColumn
            {
                data = nameof(PlacementRequestModel.TotalPayments),
                title = "Total Payments",
                fieldID = 6,
                //seqNo = 5,
            });
            columns1.Add(new DatatableDynamicColumn
            {
                data = nameof(PlacementRequestModel.LastViewed),
                title = "Last Viewed Date",
                fieldID = 7,
                //seqNo = 6,
            });
            columns1.Add(new DatatableDynamicColumn
            {
                data = nameof(PlacementRequestModel.CompletionDate),
                title = "Date Completed",
                fieldID = 8,
                //seqNo = 7,
            });
            columns1.Add(new DatatableDynamicColumn
            {
                data = nameof(PlacementRequestModel.DaysOpen),
                title = "Days open",
                fieldID = 9,
                //seqNo = 8,
            });
            columns1.Add(new DatatableDynamicColumn
            {
                data = nameof(PlacementRequestModel.LastNoteDate),
                title = "Last Note Date",
                fieldID = 10,
                //seqNo = 6,
            });
            return columns1;
        }

        public void UpdateListFields(int UserId, int FormId, List<int> fieldIDs)
        {
            var oldFields =_listFieldRepo.GetByUserAndFormID(UserId, FormId);
            var removeList = oldFields.Where(x => x.FieldID != null && fieldIDs.Contains(x.FieldID.Value) == false).AsEnumerable();

            int seqNew = oldFields.Count() - removeList.Count() + 1;

            var addList = fieldIDs.Where(x => oldFields.Any(o => o.FieldID == x) == false).Select((x, i) => new ListField
            {
                FieldID = x,
                FormID = FormId,
                UserID = UserId,
                SeqNo = seqNew + i
            });

            _listFieldRepo.RemoveList(removeList);
            _listFieldRepo.AddList(addList);

        }

        public void UpdateListFieldSequence(int UserId, int FormId, List<DatatableDynamicColumn> ListFields)
        {
            var oldFields = _listFieldRepo.GetByUserAndFormID(UserId, FormId).ToList();
            if(oldFields.Count() == 0)
            {
                var addList = ListFields.Select(x => new ListField
                {
                    FieldID = x.fieldID,
                    FormID = FormId,
                    SeqNo = x.seqNo,
                    UserID = UserId,
                });
                _listFieldRepo.AddList(addList);
            }
            else
            {
                foreach(var item in oldFields)
                {
                    var newItem = ListFields.FirstOrDefault(x => x.fieldID == item.FieldID);
                    if(newItem != null)
                    {
                        item.SeqNo = newItem.seqNo;
                        _listFieldRepo.Update(item);
                    }
                }
            }
        }

        List<int?> GetSelectionColumnsByUserType(UserType UserType)
        {
            List<int?> SelectionColumns;
            if (UserType == UserType.ClientUser)
            {
                SelectionColumns = new List<int?>() { 1, 2 };
            }
            else
            {
                SelectionColumns = new List<int?>() { 1, 2, 3 };
            }

            return SelectionColumns;
        }

        public void UpdateActiveCode(int code, int requestId)
        {
            var request = _requestsRepo.GetById(requestId);
            if(code == 1)
            {
                request.active = 0;
            }
            else
            {
                request.active = 1;
            }
            _requestsRepo.Update(request);
        }
    }
}
