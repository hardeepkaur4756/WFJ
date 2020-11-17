using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Models;
using WFJ.Repository;
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
                RequestDateString = request.RequestDate != null ? request.RequestDate.Value.ToString("dd/MM/yyyy") : null,
                CompletionDateString = request.CompletionDate != null ? request.CompletionDate.Value.ToString("dd/MM/yyyy"): null
            };

            return model;
        }

        public PlacementRequestsListViewModel GetPlacementRequests(int userId, int formId, UserType UserType, int requestor, int assignedAttorney, int collector, int status, string region,
                                                                    string startDate, string toDate,
                                                                    DataTablesParam param, string sortDir, string sortCol, int pageNo)
        {
            PlacementRequestsListViewModel model = new PlacementRequestsListViewModel();

            DateTime? beginDate = string.IsNullOrEmpty(startDate) ? (DateTime?)null : DateTime.ParseExact(startDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime? endDate = string.IsNullOrEmpty(toDate) ? (DateTime?)null : DateTime.ParseExact(toDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date.AddDays(1);


            var documents = _requestsRepo.GetRequestsList(formId, requestor, assignedAttorney, collector, status, beginDate, endDate);
            model.TotalRequestsCount = documents?.Count();

            if (documents != null)
            {
                DateTime NowDate = DateTime.Now.Date;
                IStatusCodesRepository statusCodesRepo = new StatusCodesRepository();
                var StatusList = statusCodesRepo.GetByFormID(formId);

                var formData = _formFieldRepo.GetFormFieldsByFormID(formId).SelectMany(x => x.FormDatas).ToList();
                var formAddressData = _formFieldRepo.GetFormFieldsByFormID(formId).SelectMany(x => x.FormAddressDatas).ToList();
                var selectionColumns = GetSelectionColumnsByUserType(UserType);

                var list1 = documents.Select(x => new PlacementRequestModel
                {
                    RequestID = x.ID,
                    AssignedAttorneyName = x.Personnel != null ? x.Personnel.FirstName + " " + x.Personnel.LastName : null,
                    RequestorName = x.User1 != null ? x.User1.FirstName + " " + x.User1.LastName : null,
                    CollectorName = x.User != null ? x.User.FirstName + " " + x.User.LastName : null,
                    CompletionDate = x.CompletionDate,
                    RequestDate = x.RequestDate,
                    CompletionDateString = x.CompletionDate != null ? x.CompletionDate.Value.ToString("dd/MM/yyyy") : null,
                    RequestDateString = x.RequestDate != null ? x.RequestDate.Value.ToString("dd/MM/yyyy") : null,
                    TotalPaymentsAmount = (float)(x.TotalPayments != null ? x.TotalPayments : 0),
                    LastViewed = x.LastViewed,
                    LastViewedDateString = x.LastViewed != null ? x.LastViewed.Value.ToString("dd/MM/yyyy") : null,
                    DaysOpen = x.RequestDate == null ? 0 :
                               x.CompletionDate == null ? (NowDate - x.RequestDate.Value).Days : (x.CompletionDate.Value - x.RequestDate.Value).Days,
                    StatusDescription = x.StatusCode != null && StatusList.Any(s => s.StatusCode1 == x.StatusCode) ? StatusList.FirstOrDefault(s => s.StatusCode1 == x.StatusCode).Description : null,

                    FormFields = formData.Where(f => f.RequestID == x.ID && f.FormField != null &&
                                                     (f.FormField.ListSeqNo != null && f.FormField.ListSeqNo != 0 && selectionColumns.Contains(f.FormField.SelectionColumn))
                                ).ToDictionary(d => "field_" + d.FormField.ID,

                                    // dropdown
                                    d => d.FormField.FieldTypeID == 7 &&
                                    d.FormField.FormSelectionLists.FirstOrDefault(s => s.Code == d.FieldValue) != null ?
                                    d.FormField.FormSelectionLists.FirstOrDefault(s => s.Code == d.FieldValue).TextValue :

                                    // multi select checkbox
                                    d.FormField.FieldTypeID == 16 && d.FieldValue != null?
                                    string.Join(",", d.FormField.FormSelectionLists.Where(c => d.FieldValue.Split(',').Select(s => int.Parse(s)).Contains(c.ID)).Select(c => c.TextValue).ToArray())

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


                /*//var listFields = _listFieldRepo.GetByUserAndFormID(userId, formId).ToList();
                var formFields = _formFieldRepo.GetFormFieldsByFormID(formId);//.Where(x => listFields.Any(a => a.FieldID == x.ID))
                var allFormData = formFields   .SelectMany(x => x.FormDatas);

                foreach (var item in list1)
                {
                    Dictionary<string, string> fields = new Dictionary<string, string>();
                    foreach(var f in formFields)
                    {
                        string fieldVal = "";
                        var field = allFormData.FirstOrDefault(x => x.RequestID == item.RequestID && x.FormFieldID == f.ID);
                        if (field != null)
                            fieldVal = field.FieldValue;

                        fields.Add("field_" + f.ID, fieldVal);
                    }
                    item.FormFields = fields;
                }*/

                switch (sortCol)
                {
                    case "AssignedAttorneyName":
                        if (sortDir == "asc")
                        {
                            list1 = list1.OrderBy(x => x.AssignedAttorneyName).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            list1 = list1.OrderByDescending(x => x.AssignedAttorneyName).ToList();
                        }
                        break;
                    case "RequestorName":
                        if (sortDir == "asc")
                        {
                            list1 = list1.OrderBy(x => x.RequestorName).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            list1 = list1.OrderByDescending(x => x.RequestorName).ToList();
                        }
                        break;
                    case "CollectorName":
                        if (sortDir == "asc")
                        {
                            list1 = list1.OrderBy(x => x.CollectorName).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            list1 = list1.OrderByDescending(x => x.CollectorName).ToList();
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
                            list1 = list1.OrderBy(x => x.StatusDescription).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            list1 = list1.OrderByDescending(x => x.StatusDescription).ToList();
                        }
                        break;
                    case "RequestDateString":
                        if (sortDir == "asc")
                        {
                            list1 = list1.OrderBy(x => x.RequestDate).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            list1 = list1.OrderByDescending(x => x.RequestDate).ToList();
                        }
                        break;
                    case "CompletionDateString":
                        if (sortDir == "asc")
                        {
                            list1 = list1.OrderBy(x => x.CompletionDate).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            list1 = list1.OrderByDescending(x => x.CompletionDate).ToList();
                        }
                        break;
                    case "LastViewedDateString":
                        if (sortDir == "asc")
                        {
                            list1 = list1.OrderBy(x => x.LastViewed).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            list1 = list1.OrderByDescending(x => x.LastViewed).ToList();
                        }
                        break;
                    default:

                        if(sortDir == "asc")
                            list1 = list1.OrderBy(x => x.FormFields.ContainsKey(sortCol) && x.FormFields[sortCol] != null ? x.FormFields[sortCol] : null).ToList();
                        if(sortDir == "desc")
                            list1 = list1.OrderByDescending(x => x.FormFields.ContainsKey(sortCol) && x.FormFields[sortCol] != null ? x.FormFields[sortCol] : null).ToList();
                        //var sort = list1.OrderBy(x => x.FormFields.ContainsKey(sortCol) ? x.FormFields[sortCol] : string.Empty);
                        //var sort2 = list1.OrderBy(x => x.FormFields.ContainsKey(sortCol) ? x.FormFields[sortCol] : string.Empty);
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
            var requestColumns = GetRequestColumns();
            var selectionColumns = GetSelectionColumnsByUserType(UserType);

            var columns = (from listf in _listFieldRepo.GetByUserAndFormID(UserId, FormId)

                           join r in requestColumns on listf.FieldID equals r.fieldID into re
                           from reqf in re.DefaultIfEmpty()

                           join f in _formFieldRepo.GetFormFieldsByFormID(FormId) on listf.FieldID equals f.ID into ff
                           from formf in ff.DefaultIfEmpty()

                           where formf == null || (formf.ListSeqNo != null && formf.ListSeqNo != 0 && selectionColumns.Contains(formf.SelectionColumn))

                           select new DatatableDynamicColumn
                           {
                               data = reqf != null ? reqf.data :
                                      formf != null ? "FormFields.field_" + formf.ID : "",
                               fieldID = listf.FieldID != null ? listf.FieldID.Value : 0,
                               seqNo = listf.SeqNo != null ? listf.SeqNo :
                                       reqf != null ? reqf.seqNo :
                                       formf != null ? formf.ListSeqNo : null,
                               title = reqf != null ? reqf.title : formf != null ? formf.FieldName : "",
                               defaultContent="",
                               visible = true
                           })
                           .OrderBy(x => x.seqNo != null ? x.seqNo : byte.MaxValue).ToList();


            // Default visibe columns
            if(columns.Count == 0)
            {
                List<string> NamesFromWebConfig = ConfigurationManager.AppSettings["RequestsDatatableDefaultColumns"] != null ?
                                                  ConfigurationManager.AppSettings["RequestsDatatableDefaultColumns"].Split(',').ToList() : new List<string>();

                foreach(var item in NamesFromWebConfig)
                {
                    var requestColumn = requestColumns.FirstOrDefault(x => x.data.ToLower() == item);
                    if (requestColumn != null)
                    {
                        requestColumn.visible = true;
                        columns.Add(requestColumn);
                    }
                    else
                    {
                        var formFields = _formFieldRepo.GetFormFieldsByFormID(FormId);
                        var formField = formFields.FirstOrDefault(x => x.FieldName.Trim().ToLower() == item && x.ListSeqNo != null && x.ListSeqNo != 0 && selectionColumns.Contains(x.SelectionColumn));
                        if(formField != null)
                        {
                            DatatableDynamicColumn formFieldColumn = new DatatableDynamicColumn
                            {
                                data = "FormFields.field_" + formField.ID,
                                fieldID = formField.ID,
                                title = formField.FieldName,
                                defaultContent = "",
                                visible = true,
                            };

                            columns.Add(formFieldColumn);
                        }
                    }
                } // foreach ends
            } // default columns end

            return columns;

            //var columns =  (from formf in _formFieldRepo.GetFormFieldsByFormID(FormId)

            //                join l in _listFieldRepo.GetByUserAndFormID(UserId, FormId) on formf.ID equals l.FieldID into li
            //                from lif in li.DefaultIfEmpty()

            //                select new DatatableDynamicColumn
            //                {
            //                    data = "FormFields.field_" + formf.ID,
            //                    formFieldId = formf.ID,
            //                    seqNo = lif != null ? lif.SeqNo : formf.ListSeqNo,
            //                    title = formf.FieldName,
            //                    visible = lif != null ? true : false
            //                }).OrderBy(x=> x.seqNo != null ? x.seqNo : int.MaxValue).ToList();


        }

        public List<DatatableDynamicColumn> GetRequestColumns()
        {
            List<DatatableDynamicColumn> columns1 = new List<DatatableDynamicColumn>();
            columns1.Add(new DatatableDynamicColumn
            {
                data = nameof(PlacementRequestModel.RequestorName),
                title = "Requestor Name",
                fieldID = 1,
                //seqNo = 0,
            });
            columns1.Add(new DatatableDynamicColumn
            {
                data = nameof(PlacementRequestModel.RequestDateString),
                title = "Request Date",
                fieldID = 2,
                //seqNo = 1,
            });
            columns1.Add(new DatatableDynamicColumn
            {
                data = nameof(PlacementRequestModel.AssignedAttorneyName),
                title = "Assigned Attorney",
                fieldID = 3,
                //seqNo = 2,
            });
            columns1.Add(new DatatableDynamicColumn
            {
                data = nameof(PlacementRequestModel.CollectorName),
                title = "Collector Name",
                fieldID = 4,
                //seqNo = 3,
            });
            columns1.Add(new DatatableDynamicColumn
            {
                data = nameof(PlacementRequestModel.StatusDescription),
                title = "Status",
                fieldID = 5,
                //seqNo = 4,
            });
            columns1.Add(new DatatableDynamicColumn
            {
                data = nameof(PlacementRequestModel.TotalPaymentsAmount),
                title = "Total Payments",
                fieldID = 6,
                //seqNo = 5,
            });
            columns1.Add(new DatatableDynamicColumn
            {
                data = nameof(PlacementRequestModel.LastViewedDateString),
                title = "Last Viewed Date",
                fieldID = 7,
                //seqNo = 6,
            });
            columns1.Add(new DatatableDynamicColumn
            {
                data = nameof(PlacementRequestModel.CompletionDateString),
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
            return columns1;
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
    }
}
