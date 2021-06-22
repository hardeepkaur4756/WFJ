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
            adminDashboardViewModel.RecentlyOpenedAccounts = GetRecentlyOpenedAccount(0);
            adminDashboardViewModel.FinalDemands = GetFinalDemand();
            adminDashboardViewModel.ActionRequireds = GetActionRequired(0);
            adminDashboardViewModel.ApprovedPayements = GetApprovedPayment(1,0);
            return adminDashboardViewModel;
        }

        public UserDashboardViewModel GetUserDashboardData(int userId)
        {
            UserDashboardViewModel userDashboardViewModel = new UserDashboardViewModel();
            userDashboardViewModel.RecentlyOpenedAccounts = new List<RecentlyOpenedAccountViewModel>();
            userDashboardViewModel.ActionRequireds = new List<ActionRequiredViewModel>();
            userDashboardViewModel.ApprovedPayements = new List<ApprovedRecentPayementViewModel>();

            //Bind Data
            userDashboardViewModel.RecentlyOpenedAccounts = GetRecentlyOpenedAccount(userId);
            userDashboardViewModel.ActionRequireds = GetActionRequired(userId);
            userDashboardViewModel.ApprovedPayements = GetApprovedPayment(0, userId);
            return userDashboardViewModel;
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
        public List<RecentlyOpenedAccountViewModel> GetRecentlyOpenedAccount(int userId)
        {
            List<RecentlyOpenedAccountViewModel> recentlyOpenedAccountViewModels = new List<RecentlyOpenedAccountViewModel>();
            IRequestsRepository _requestsRepository = new RequestsRepository();
            IStatusCodesRepository _statusCodesRepository = new StatusCodesRepository();
            var requests = _requestsRepository.GetRequestByXDays(-7,userId).GroupBy(x => x.Requestor)
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

        public List<ActionRequiredViewModel> GetActionRequired(int userId)
        {
            List<ActionRequiredViewModel> actionRequiredViewModels = new List<ActionRequiredViewModel>();
            IRequestsRepository _requestsRepository = new RequestsRepository();
            actionRequiredViewModels = _requestsRepository.GetRequestOutOfCompliance(userId)
                .Select(x => new ActionRequiredViewModel
                {
                    AttorneyName = x.Personnel?.FullName,
                    CustomerName = GetCustomerName(x.ID, Convert.ToInt32(x.FormID)),
                    ClientName = x.Form.Client.ClientName,
                    Status = GetStatus(Convert.ToInt32(x.StatusCode), Convert.ToInt32(x.FormID)),
                    ComplianceDuration = ComplianceDuration(Convert.ToInt32(x.StatusCode), Convert.ToInt32(x.FormID)),
                    LastNoteDate = x.LastNoteDate.HasValue ? x.LastNoteDate.Value.ToString("MM/dd/yyyy") : "",
                    LastNote = x.RequestNotes.Any() ? x.RequestNotes.OrderByDescending(y => y.NotesDate)?
                    .FirstOrDefault()?.Notes : ""
                }).OrderBy(x => x.AttorneyName).ThenBy(x => x.ClientName).ThenBy(x => x.Status).ThenBy(x => x.CustomerName).ToList();

            return actionRequiredViewModels;
        }

        /// <summary>
        /// Get Compliance Duration by Status Code and FormId
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="formId"></param>
        /// <returns></returns>
        public int? ComplianceDuration(int statusCode, int formId)
        {
            IStatusCodesRepository _statusCodeRepository = new StatusCodesRepository();
            var complianceDuration = _statusCodeRepository.GetByStatusCodeAndFormId(statusCode, formId)?.complianceDuration;
            return complianceDuration;
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

        /// <summary>
        /// get last 7 days created request
        /// </summary>
        /// <returns></returns>
        public List<ApprovedRecentPayementViewModel> GetApprovedPayment(int approved, int userId)
        {
            List<ApprovedRecentPayementViewModel> approvedPayements = new List<ApprovedRecentPayementViewModel>();
            IRequestsRepository _requestsRepository = new RequestsRepository();
            IStatusCodesRepository _statusCodesRepository = new StatusCodesRepository();
            IPaymentsRepository _paymentRepository = new PaymentsRepository();
            approvedPayements = _paymentRepository.GetPaymentByApprovedAndXDays(approved, userId)
                .Select(x => new ApprovedRecentPayementViewModel
                {
                    FormName = x.Request?.Form?.FormName,
                    PaymentAmount = x.Amount,
                    PaymentDate = x.PaymentDate.HasValue? x.PaymentDate.Value.ToString("MM/dd/yyyy"): "",
                    PaymentType = x.PaymentType?.PaymentTypeDesc,
                    WFJFees = x.WFJFees,
                    InvoicDate = x.WFJInvoiceDatePaid.HasValue ? x.WFJInvoiceDatePaid.Value.ToString("MM/dd/yyyy"):"",
                    CustomerName = GetCustomerName(x.ID, Convert.ToInt32(x.Request?.FormID)),
                    ClientName = x.Request?.Form.Client.ClientName,
                    Status = GetStatus(Convert.ToInt32(x.Request?.StatusCode), Convert.ToInt32(x.Request?.Form.ID)),
                    InvoiceNumber = x.CheckNumber
                })
                .OrderBy(x => x.ClientName).ThenBy(x => x.CustomerName).ThenBy(x => x.FormName).ThenBy(x => x.PaymentDate)
                .ToList();
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
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      