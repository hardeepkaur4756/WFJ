                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Service.Interfaces;
using WFJ.Repository;
using WFJ.Repository.Interfaces;
using WFJ.Models;

namespace WFJ.Service
{
    public class DashboardService : IDashboardService
    {
        public AdminDashboardViewModel GetAdminDashboardData()
        {
            AdminDashboardViewModel adminDashboardViewModel = new AdminDashboardViewModel();
            adminDashboardViewModel.RecentlyOpenedClients = new List<RecentlyOpenedClientViewModel>();
            adminDashboardViewModel.RecentlyOpenedAccounts = new List<RecentlyOpenedAccountViewModel>();
            adminDashboardViewModel.FinalDemands = new List<FinalDemandViewModel>();
            adminDashboardViewModel.ActionRequireds = new List<ActionRequiredViewModel>();
            adminDashboardViewModel.ApprovedPayements = new List<ApprovedRecentPayementViewModel>();

            //Bind Data
            adminDashboardViewModel.RecentlyOpenedClients = GetRecentlyOpenedClient();
            adminDashboardViewModel.RecentlyOpenedAccounts = GetRecentlyOpenedAccount();
            adminDashboardViewModel.FinalDemands = GetFinalDemand();
            adminDashboardViewModel.ActionRequireds = GetActionRequired();
            return adminDashboardViewModel;
        }

        /// <summary>
        /// get last 30 days recently joined client
        /// </summary>
        /// <returns></returns>
        public List<RecentlyOpenedClientViewModel> GetRecentlyOpenedClient()
        {
            IClientRepository _clientRepository = new ClientRepository();
            var clients = _clientRepository.GetAllClientsByXDays(-30).Select(x => new RecentlyOpenedClientViewModel
            {
                ClientName = x.ClientName,
                CreatedDate = x.dtCreated.Value.ToString("MM/dd/yyyy")
            }).ToList();
            return clients;
        }

        /// <summary>
        /// get last 7 days created request
        /// </summary>
        /// <returns></returns>
        public List<RecentlyOpenedAccountViewModel> GetRecentlyOpenedAccount()
        {
            List<RecentlyOpenedAccountViewModel> recentlyOpenedAccountViewModels = new List<RecentlyOpenedAccountViewModel>();
            IRequestsRepository _requestsRepository = new RequestsRepository();
            IStatusCodesRepository _statusCodesRepository = new StatusCodesRepository();
            var requests = _requestsRepository.GetRequestByXDays(-7).GroupBy(x => x.Requestor)
                .Select(x => new RequestModel
                {
                    FormId = x.Max(z=>z.FormID),
                    RequestId = x.Max(z => z.ID),
                    StatusCode = x.Max(z => z.StatusCode),
                    RequestDate = x.Max(z => z.RequestDate),
                    ClientName = x.Max(z => z.Form.Client.ClientName),
                })
                .OrderByDescending(x => x.RequestDate)
                .ToList();            

            if (requests != null && requests.Any())
            {
                foreach (var request in requests)
                {
                    RecentlyOpenedAccountViewModel recentlyOpenedAccountViewModel = new RecentlyOpenedAccountViewModel();
                    recentlyOpenedAccountViewModel.ClientName = request.ClientName;
                    recentlyOpenedAccountViewModel.Status = GetStatus(Convert.ToInt32(request.StatusCode), Convert.ToInt32(request.FormId));
                    recentlyOpenedAccountViewModel.CustomerName = GetCustomerName(request.RequestId, Convert.ToInt32(request.FormId));
                    recentlyOpenedAccountViewModels.Add(recentlyOpenedAccountViewModel);
                }
            }
            return recentlyOpenedAccountViewModels;
        }

        /// <summary>
        /// Get Final Demand
        /// </summary>
        /// <returns></returns>
        public List<FinalDemandViewModel> GetFinalDemand()
        {
            List<FinalDemandViewModel> finalDemandViewModels = new List<FinalDemandViewModel>();
            IRequestsRepository _requestsRepository = new RequestsRepository();
            finalDemandViewModels = _requestsRepository.GetRequestByStatusName("Final Demand Request")
                .Select(x => new FinalDemandViewModel
                {
                    CustomerName = GetCustomerName(x.ID, Convert.ToInt32(x.FormID)),
                    ClientName = x.Form.Client.ClientName,
                    Status = GetStatus(Convert.ToInt32(x.StatusCode), Convert.ToInt32(x.FormID))
                }).ToList();

            return finalDemandViewModels;
        }

        public List<ActionRequiredViewModel> GetActionRequired()
        {
            List<ActionRequiredViewModel> actionRequiredViewModels = new List<ActionRequiredViewModel>();
            IRequestsRepository _requestsRepository = new RequestsRepository();
            actionRequiredViewModels = _requestsRepository.GetRequestOutOfCompliance()
                .Select(x => new ActionRequiredViewModel
                {
                    AttorneyName = x.Personnel?.FullName,
                    CustomerName = GetCustomerName(x.ID, Convert.ToInt32(x.FormID)),
                    ClientName = x.Form.Client.ClientName,
                    Status = GetStatus(Convert.ToInt32(x.StatusCode), Convert.ToInt32(x.FormID)),
                    ComplianceDuration = ComplianceDuration(Convert.ToInt32(x.StatusCode), Convert.ToInt32(x.FormID)),
                    LastNoteDate = x.RequestNotes?.OrderByDescending(y=>y.NotesDate)?.FirstOrDefault().NotesDate.Value.ToString("MM/dd/yyyy"),
                    LastNote = x.RequestNotes?.OrderByDescending(y => y.NotesDate)?.FirstOrDefault()?.Notes
                }).ToList();

            return actionRequiredViewModels;
        }

        /// <summary>
        /// get customer name
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="formId"></param>
        /// <returns></returns>
        public string GetCustomerName(int requestId, int formId)
        {
            IFormDataRepository _formDataRepository = new FormDataRepository();
            IFormFieldsRepository _formFieldRepository = new FormFieldsRepository();
            int? formFieldId = 0;
            formFieldId = _formFieldRepository.GetFormFieldsByFormID(formId).FirstOrDefault(x => x.FieldName.Contains("Customer Name"))?.ID;
            var customerName = _formDataRepository.GetByRequestId(requestId).FirstOrDefault(x => x.FormFieldID == formFieldId)?.FieldValue;
            return customerName;
        }

        /// <summary>
        /// get status 
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="formId"></param>
        /// <returns></returns>
        public string GetStatus(int statusCode, int formId)
        {
            IStatusCodesRepository _statusCodeRepository = new StatusCodesRepository();
            var statusDescription = _statusCodeRepository.GetByStatusCodeAndFormId(statusCode, formId)?.Description;
            return statusDescription;
        }
        public int? ComplianceDuration(int statusCode, int formId)
        {
            IStatusCodesRepository _statusCodeRepository = new StatusCodesRepository();
            var complianceDuration = _statusCodeRepository.GetByStatusCodeAndFormId(statusCode, formId)?.complianceDuration;
            return complianceDuration;
        }

        /// <summary>
        /// get last 7 days created request
        /// </summary>
        /// <returns></returns>
        public List<ApprovedRecentPayementViewModel> GetApprovedPayment()
        {
            List<ApprovedRecentPayementViewModel> approvedPayements = new List<ApprovedRecentPayementViewModel>();
            IRequestsRepository _requestsRepository = new RequestsRepository();
            IStatusCodesRepository _statusCodesRepository = new StatusCodesRepository();
            IPaymentsRepository _paymentRepository = new PaymentsRepository();
            var requests = _paymentRepository.GetPaymentByApprovedAndXDays(1,7);
                //.GroupBy(x => x.Requestor)
                //.Select(x => new RequestModel
                //{
                //    FormId = x.Max(z => z.FormID),
                //    RequestId = x.Max(z => z.ID),
                //    StatusCode = x.Max(z => z.StatusCode),
                //    RequestDate = x.Max(z => z.RequestDate),
                //    ClientName = x.Max(z => z.Form.Client.ClientName),
                //})
                //.OrderByDescending(x => x.RequestDate)
                //.ToList();

            //if (requests != null && requests.Any())
            //{
            //    foreach (var request in requests)
            //    {
            //        ApprovedRecentPayementViewModel approvedRecentPayementViewModel = new ApprovedRecentPayementViewModel();
            //        approvedRecentPayementViewModel.ClientName = request.ClientName;
            //        approvedRecentPayementViewModel.Status = GetStatus(Convert.ToInt32(request.StatusCode), Convert.ToInt32(request.FormId));
            //        approvedRecentPayementViewModel.CustomerName = GetCustomerName(request.RequestId, Convert.ToInt32(request.FormId));
            //        approvedPayements.Add(approvedRecentPayementViewModel);
            //    }
            //}
            return approvedPayements;
        }

        public class RequestModel
        {
            public int? StatusCode { get; set; }
            public int RequestId { get; set; }
            public int?FormId { get; set; }
            public DateTime? RequestDate { get; set; }
            public string ClientName { get; set; }
            public string CustomerName { get; set; }

        }

    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      