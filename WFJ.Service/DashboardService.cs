using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Service.Interfaces;
using WFJ.Repository;
using WFJ.Repository.Interfaces;
using WFJ.Models;
using System.Web.Mvc;
using WFJ.Service.Model;
using WFJ.Repository.EntityModel;
using System.Globalization;
using System.Configuration;

namespace WFJ.Service
{
    public class DashboardService : IDashboardService
    {
        private IFormsRepository _formSearchRepository = new FormsRepository();
        private IUserRepository _userRepo = new UserRepository();
        IClientRepository _clientRepository = new ClientRepository();
        IRequestsRepository _requestsRepository = new RequestsRepository();
        IUserAttorneyRepository _userAttorneyRepository = new UserAttorneyRepository();
        IPersonnelClientsRepository _personnelClientsRepository = new PersonnelClientsRepository();
        IClientCollectorsRepository _clientCollectorsRepository = new ClientCollectorsRepository();
        public AdminDashboardViewModel GetAdminDashboardData(int formId)
        {
            AdminDashboardViewModel adminDashboardViewModel = new AdminDashboardViewModel();
            adminDashboardViewModel.RecentlyOpenedClients = new List<RecentlyOpenedClientViewModel>();
            adminDashboardViewModel.RecentlyOpenedAccounts = new List<DashboardBaseModel>();
            adminDashboardViewModel.FinalDemands = new List<DashboardBaseModel>();
            adminDashboardViewModel.ActionRequireds = new List<ActionRequiredViewModel>();
            adminDashboardViewModel.ApprovedPayements = new List<ApprovedRecentPayementViewModel>();
            adminDashboardViewModel.RemittancePayements = new List<ApprovedRecentPayementViewModel>();

            //Bind Data
            adminDashboardViewModel.RecentlyOpenedClients = GetRecentlyOpenedClient();
            adminDashboardViewModel.RecentlyOpenedAccounts = GetRecentlyOpenedAccount(formId);
            adminDashboardViewModel.FinalDemands = GetFinalDemand(formId);
            adminDashboardViewModel.ActionRequireds = GetActionRequired(formId);
            adminDashboardViewModel.ApprovedPayements = GetApprovedPayment((int)ApproveStatus.Approved, formId);
            adminDashboardViewModel.RemittancePayements = GetRemittancePayment(Convert.ToInt32(ConfigurationManager.AppSettings["SevenDays"]), formId);
            return adminDashboardViewModel;
        }

        public UserDashboardViewModel GetUserDashboardData(int formId)
        {
            UserDashboardViewModel userDashboardViewModel = new UserDashboardViewModel();
            userDashboardViewModel.RecentlyOpenedAccounts = new List<DashboardBaseModel>();
            userDashboardViewModel.ActionRequireds = new List<ActionRequiredViewModel>();
            userDashboardViewModel.ApprovedPayements = new List<ApprovedRecentPayementViewModel>();
            userDashboardViewModel.FollowUpAccounts = new List<DashboardBaseModel>();

            //Bind Data
            userDashboardViewModel.RecentlyOpenedAccounts = GetRecentlyOpenedAccount(formId);
            userDashboardViewModel.ActionRequireds = GetActionRequired(formId);
            userDashboardViewModel.ApprovedPayements = GetApprovedPayment((int)ApproveStatus.Unapproved, formId);
            userDashboardViewModel.FollowUpAccounts = GetFollowUpAccounts(formId);
            userDashboardViewModel.RemittancePayements = GetRemittancePayment(Convert.ToInt32(ConfigurationManager.AppSettings["SevenDays"]), formId);
            return userDashboardViewModel;
        }


        #region Client DashBoard Methods

        public ClientDashboardViewModel GetClientDashboardData(int formId)
        {
            ClientDashboardViewModel clientDashboardViewModel = new ClientDashboardViewModel();
            clientDashboardViewModel.RecentAccountView = new List<RecentAccountActivityViewModel>();
            clientDashboardViewModel.RecentActivityView = new List<RecentAccountActivityViewModel>();

            //Bind Data
            clientDashboardViewModel.RecentAccountView = GetRecentAccountView(Convert.ToInt32(ConfigurationManager.AppSettings["TenDays"]), formId, AccountActivity.Accounts.ToString());
            clientDashboardViewModel.RecentActivityView = GetRecentActivityView(Convert.ToInt32(ConfigurationManager.AppSettings["TenDays"]), formId, AccountActivity.Activity.ToString());
            clientDashboardViewModel.ApprovedPayements =  GetApprovedPayment((int)ApproveStatus.Unapproved, formId);
            clientDashboardViewModel.RemittancePayements = GetRemittancePayment(Convert.ToInt32(ConfigurationManager.AppSettings["SevenDays"]), formId);
            return clientDashboardViewModel;
        }

        public List<ChartBaseModel> GetActiveStatusPieChartData(int formId)
        {
            List<ChartBaseModel> activeStatusPieChartData = new List<ChartBaseModel>();
            IRequestsRepository _requestsRepository = new RequestsRepository();
            var request = _requestsRepository.GetByFormId(formId);
            activeStatusPieChartData = request.GroupBy(x => x.StatusCode).ToList()
                .Select(x => new ChartBaseModel
                {
                    Name = GetStatus(Convert.ToInt32(x.Key), formId),
                    Value = x.Count().ToString()
                }).ToList();

            return activeStatusPieChartData;
        }


        public ChartBaseModel3Yearly GetPlacementData(int formId)
        {
            IRequestsRepository _requestsRepository = new RequestsRepository();
            var request = _requestsRepository.GetByFormId(formId);
            var currentYear = request.Where(x => x.RequestDate.HasValue && x.RequestDate.Value.Year == DateTime.Now.Year)
                 .GroupBy(x => x.RequestDate.Value.Month)
                 .Select(x => new ChartBaseModel
                 {
                     Name = x.Key.ToString(),
                     Value = x.Count().ToString()
                 }).ToList();

            var lastYear = request.Where(x => x.RequestDate.HasValue && x.RequestDate.Value.Year == DateTime.Now.Year - 1)
                .GroupBy(x => x.RequestDate.Value.Month)
                .Select(x => new ChartBaseModel
                {
                    Name = x.Key.ToString(),
                    Value = x.Count().ToString()
                }).ToList();

            var last3rdYear = request.Where(x => x.RequestDate.HasValue && x.RequestDate.Value.Year == DateTime.Now.Year - 2)
               .GroupBy(x => x.RequestDate.Value.Month)
               .Select(x => new ChartBaseModel
               {
                   Name = x.Key.ToString(),
                   Value = x.Count().ToString()
               }).ToList();

            var chartBaseModel = BindDataMonthly(currentYear, lastYear, last3rdYear);

            return chartBaseModel;
        }

        public ChartBaseModel3Yearly GetPlacementCollectedData(int formId)
        {
            IRequestsRepository _requestsRepository = new RequestsRepository();
            IPaymentsRepository _paymentsRepository = new PaymentsRepository();

            var request = _requestsRepository.GetByFormId(formId);
            var payments = _paymentsRepository.GetByReqestIds(request.Select(x => x.ID).ToList());

            var currentYear = payments.Where(x => x.PaymentDate.HasValue && x.PaymentDate.Value.Year == DateTime.Now.Year)
                .GroupBy(x => x.PaymentDate.Value.Month)
                .Select(x => new ChartBaseModel
                {
                    Name = x.Key.ToString(),
                    Value = x.OrderBy(y => y.PaymentDate).FirstOrDefault()?.Amount.ToString()
                }).ToList();

            var lastYear = payments.Where(x => x.PaymentDate.HasValue && x.PaymentDate.Value.Year == DateTime.Now.Year - 1)
                 .GroupBy(x => x.PaymentDate.Value.Month)
                  .Select(x => new ChartBaseModel
                  {
                      Name = x.Key.ToString(),
                      Value = x.OrderBy(y => y.PaymentDate).FirstOrDefault()?.Amount.ToString()
                  }).ToList();

            var last3rdYear = payments.Where(x => x.PaymentDate.HasValue && x.PaymentDate.Value.Year == DateTime.Now.Year - 2)
               .GroupBy(x => x.PaymentDate.Value.Month)
                .Select(x => new ChartBaseModel
                {
                    Name = x.Key.ToString(),
                    Value = x.OrderBy(y => y.PaymentDate).FirstOrDefault()?.Amount.ToString()
                }).ToList();


            var chartBaseModel = BindDataMonthly(currentYear, lastYear, last3rdYear);
            return chartBaseModel;
        }

        public List<StackBarChartModel> GetPlacementAndCollectedBarChartData(int formId)
        {
            List<StackBarChartModel> stackBarChartModels = new List<StackBarChartModel>();
            ChartBaseModel3Yearly getPlacementData = new ChartBaseModel3Yearly();
            ChartBaseModel3Yearly getCollectedData = new ChartBaseModel3Yearly();

            getPlacementData = GetPlacementData(formId);
            getCollectedData = GetPlacementCollectedData(formId);
            StackBarChartModel chartModel = new StackBarChartModel();
            chartModel.data = new string[] { "1000", "2000", "1000", "2000", "1000", "2000", "1000", "2000", "1000", "8000", "1000", "8000" }; //getPlacementData.ChartBaseModelCurrentYear.Select(x => x.Value).ToArray();
            chartModel.backgroundColor = new string[] { "#0D47A1", "#0D47A1", "#0D47A1", "#0D47A1", "#0D47A1", "#0D47A1", "#0D47A1", "#0D47A1", "#0D47A1", "#0D47A1" };
            chartModel.fillColor = "#000000";
            chartModel.stack = "1";
            stackBarChartModels.Add(chartModel);

            chartModel = new StackBarChartModel();
            chartModel.data = getCollectedData.ChartBaseModelCurrentYear.Select(x => x.Value).ToArray();
            chartModel.backgroundColor = new string[] { "#f55f31", "#f55f31", "#f55f31", "#f55f31", "#f55f31", "#f55f31", "#f55f31", "#f55f31", "#f55f31", "#f55f31" };
            chartModel.fillColor = "#000000";
            chartModel.stack = "1";
            stackBarChartModels.Add(chartModel);

            chartModel = new StackBarChartModel();
            chartModel.data = new string[] { "1000", "2000", "1000", "2000", "1000", "2000", "1000", "2000", "1000", "8000", "1000", "8000" };//getPlacementData.ChartBaseModelPreviousYear.Select(x => x.Value).ToArray();
            chartModel.backgroundColor = new string[] { "#fcba03", "#fcba03", "#fcba03", "#fcba03", "#fcba03", "#fcba03", "#fcba03", "#fcba03", "#fcba03", "#fcba03" };
            chartModel.fillColor = "#000000";
            chartModel.stack = "2";
            stackBarChartModels.Add(chartModel);

            chartModel = new StackBarChartModel();
            chartModel.data = getCollectedData.ChartBaseModelPreviousYear.Select(x => x.Value).ToArray();
            chartModel.backgroundColor = new string[] { "#83d4eb", "#83d4eb", "#83d4eb", "#83d4eb", "#83d4eb", "#83d4eb", "#83d4eb", "#83d4eb", "#83d4eb", "#83d4eb" };
            chartModel.fillColor = "#000000";
            chartModel.stack = "2";
            stackBarChartModels.Add(chartModel);

            chartModel = new StackBarChartModel();
            chartModel.data = new string[] { "1000", "2000", "1000", "2000", "1000", "2000", "1000", "2000", "1000", "8000", "1000", "8000" };//getPlacementData.ChartBaseModellast3rdYear.Select(x => x.Value).ToArray();
            chartModel.backgroundColor = new string[] { "#210be3", "#210be3", "#210be3", "#210be3", "#210be3", "#210be3", "#210be3", "#210be3", "#210be3", "#210be3" };
            chartModel.fillColor = "#000000";
            chartModel.stack = "3";
            stackBarChartModels.Add(chartModel);

            chartModel = new StackBarChartModel();
            chartModel.data = getCollectedData.ChartBaseModellast3rdYear.Select(x => x.Value).ToArray();
            chartModel.backgroundColor = new string[] { "#612c2b", "#612c2b", "#612c2b", "#612c2b", "#612c2b", "#612c2b", "#612c2b", "#612c2b", "#612c2b", "#612c2b" };
            chartModel.fillColor = "#000000";
            chartModel.stack = "3";
            stackBarChartModels.Add(chartModel);

            //var chartData = Newtonsoft.Json.JsonConvert.SerializeObject(stackBarChartModels);
            return stackBarChartModels;
        }

        private static ChartBaseModelYearly BindDataMonthly(List<ChartBaseModel> currentYear, List<ChartBaseModel> lastYear)
        {
            ChartBaseModelYearly chartBaseModelYearly = new ChartBaseModelYearly();
            chartBaseModelYearly.ChartBaseModelCurrentYear = new List<ChartBaseModel>();
            chartBaseModelYearly.ChartBaseModelPreviousYear = new List<ChartBaseModel>();

            IEnumerable<int> months = Enumerable.Range(1, 12);
            foreach (var month in months)
            {
                ChartBaseModel order = new ChartBaseModel();
                order.Name = new DateTime(2020, month, 1).ToString("MMM", CultureInfo.InvariantCulture);
                order.Value = currentYear.FirstOrDefault(x => int.Parse(x.Name) == month) != null ? currentYear.FirstOrDefault(x => int.Parse(x.Name) == month)?.Value : "0";
                chartBaseModelYearly.ChartBaseModelCurrentYear.Add(order);
            }

            foreach (var month in months)
            {
                ChartBaseModel order = new ChartBaseModel();
                order.Name = new DateTime(2020, month, 1).ToString("MMM", CultureInfo.InvariantCulture);
                order.Value = lastYear.FirstOrDefault(x => int.Parse(x.Name) == month) != null ? lastYear.FirstOrDefault(x => int.Parse(x.Name) == month)?.Value : "0";
                chartBaseModelYearly.ChartBaseModelPreviousYear.Add(order);
            }
            return chartBaseModelYearly;
        }

        private static ChartBaseModel3Yearly BindDataMonthly(List<ChartBaseModel> currentYear, List<ChartBaseModel> lastYear, List<ChartBaseModel> last3rdYear)
        {
            ChartBaseModel3Yearly chartBaseModel3Yearly = new ChartBaseModel3Yearly();
            chartBaseModel3Yearly.ChartBaseModelCurrentYear = new List<ChartBaseModel>();
            chartBaseModel3Yearly.ChartBaseModelPreviousYear = new List<ChartBaseModel>();
            chartBaseModel3Yearly.ChartBaseModellast3rdYear = new List<ChartBaseModel>();

            IEnumerable<int> months = Enumerable.Range(1, 12);
            foreach (var month in months)
            {
                ChartBaseModel order = new ChartBaseModel();
                order.Name = new DateTime(2020, month, 1).ToString("MMM", CultureInfo.InvariantCulture);
                order.Value = currentYear.FirstOrDefault(x => int.Parse(x.Name) == month) != null ? currentYear.FirstOrDefault(x => int.Parse(x.Name) == month)?.Value : "0";
                chartBaseModel3Yearly.ChartBaseModelCurrentYear.Add(order);
            }

            foreach (var month in months)
            {
                ChartBaseModel order = new ChartBaseModel();
                order.Name = new DateTime(2020, month, 1).ToString("MMM", CultureInfo.InvariantCulture);
                order.Value = lastYear.FirstOrDefault(x => int.Parse(x.Name) == month) != null ? lastYear.FirstOrDefault(x => int.Parse(x.Name) == month)?.Value : "0";
                chartBaseModel3Yearly.ChartBaseModelPreviousYear.Add(order);
            }

            foreach (var month in months)
            {
                ChartBaseModel order = new ChartBaseModel();
                order.Name = new DateTime(2020, month, 1).ToString("MMM", CultureInfo.InvariantCulture);
                order.Value = lastYear.FirstOrDefault(x => int.Parse(x.Name) == month) != null ? lastYear.FirstOrDefault(x => int.Parse(x.Name) == month)?.Value : "0";
                chartBaseModel3Yearly.ChartBaseModellast3rdYear.Add(order);
            }

            return chartBaseModel3Yearly;
        }

        private long GetBalanceDue(List<int> requestids,int formFieldsId)
        {
            if(formFieldsId > 0)
            {
                IFormDataRepository _formDataRepository = new FormDataRepository();
                return _formDataRepository.GetAll().Where(x => x.FormFieldID == formFieldsId && requestids.Contains(x.RequestID.Value)).Sum(x => Convert.ToInt64(x.FieldValue));
            }
            else
            {
                return 0;
            }
            
        }

        public ChartBaseModelYearly GetDollarsPlacedLineChartData(int formId)
        {
            IRequestsRepository _requestsRepository = new RequestsRepository();
            var requests = _requestsRepository.GetByFormId(formId);
            var formFieldId = Convert.ToInt32(requests?.FirstOrDefault()?.Form?.AccountBalanceFieldID.Value);

            var currentYear = requests.Where(x => x.RequestDate.HasValue && x.RequestDate.Value.Year == DateTime.Now.Year)
                .GroupBy(x => x.RequestDate.Value.Month)
                .Select(x => new ChartBaseModel
                {
                    Name = x.Key.ToString(),
                    Value = GetBalanceDue(x.Select(y => y.ID).ToList(), formFieldId).ToString()
                }).ToList();

            var lastYear = requests.Where(x => x.RequestDate.HasValue && x.RequestDate.Value.Year == DateTime.Now.Year - 1)
               .GroupBy(x => x.RequestDate.Value.Month)
               .Select(x => new ChartBaseModel
               {
                   Name = x.Key.ToString(),
                   Value = GetBalanceDue(x.Select(y => y.ID).ToList(), formFieldId).ToString()
               }).ToList();

            var chartBaseModel = BindDataMonthly(currentYear, lastYear);
            return chartBaseModel;
        }

        

        public ChartBaseModelYearly GetDollarsCollectedLineChartData(int formId)
        {
            IRequestsRepository _requestsRepository = new RequestsRepository();
            IPaymentsRepository _paymentsRepository = new PaymentsRepository();

            var request = _requestsRepository.GetByFormId(formId);
            var payments = _paymentsRepository.GetByReqestIds(request.Select(x => x.ID).ToList());

            var currentYear = payments.Where(x => x.PaymentDate.HasValue && x.PaymentDate.Value.Year == DateTime.Now.Year)
                .GroupBy(x => x.PaymentDate.Value.Month)
                .Select(x => new ChartBaseModel
                {
                    Name = x.Key.ToString(),
                    Value = x.Sum(y => y.Amount).ToString()
                }).ToList();

            var lastYear = payments.Where(x => x.PaymentDate.HasValue && x.PaymentDate.Value.Year == DateTime.Now.Year - 1)
                .GroupBy(x => x.PaymentDate.Value.Month)
                .Select(x => new ChartBaseModel
                {
                    Name = x.Key.ToString(),
                    Value = x.Sum(y => y.Amount).ToString()
                }).ToList();

            var chartBaseModel = BindDataMonthly(currentYear, lastYear);
            return chartBaseModel;

        }


        /// <summary>
        /// Get Recent Account View
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        public List<RecentAccountActivityViewModel> GetRecentAccountView(int days, int formId, string type)
        {
            List<RecentAccountActivityViewModel> recentAccountViewModels = new List<RecentAccountActivityViewModel>();
            IRecentAccountActivitiesRepository _recentAccActRepo = new RecentAccountActivitiesRepository();

            recentAccountViewModels = _recentAccActRepo.GetRecentAccounts(days, formId, type).Select(x => new RecentAccountActivityViewModel
            {
                CustomerName = GetCustomerName(Convert.ToInt32(x.RequestID), Convert.ToInt32(x.Request?.FormID)),
                Status = GetStatus(Convert.ToInt32(x.Request?.StatusCode), Convert.ToInt32(x.Request?.FormID))
            }).ToList();

            return recentAccountViewModels;
        }

        /// <summary>
        /// Get Recent Activity View
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        public List<RecentAccountActivityViewModel> GetRecentActivityView(int days, int formId, string type)
        {
            List<RecentAccountActivityViewModel> recentAccountViewModels = new List<RecentAccountActivityViewModel>();
            IRecentAccountActivitiesRepository _recentAccActRepo = new RecentAccountActivitiesRepository();

            recentAccountViewModels = _recentAccActRepo.GetRecentAccounts(days, formId, type).Select(x => new RecentAccountActivityViewModel
            {
                CustomerName = GetCustomerName(Convert.ToInt32(x.RequestID), Convert.ToInt32(x.Request?.FormID)),
                Status = GetStatus(Convert.ToInt32(x.Request?.StatusCode), Convert.ToInt32(x.Request?.FormID))
            }).ToList();

            return recentAccountViewModels;
        }


        #endregion

        public (Form form, List<SelectListItem>) GetDashboardFilters(int userType, int userId)
        {
            List<SelectListItem> itemList = new List<SelectListItem>();
            User user = _userRepo.GetById(userId);
            var documents = new List<Form>();
            if (userType == (int)UserType.SystemAdministrator)
            {
                documents = _formSearchRepository.GetFormList(-1, -1, null).ToList();
            }
            if (userType == (int)UserType.ClientUser)
            {
                documents = _formSearchRepository.GetFormList(-1, -1, userId).ToList();
            }
            if (userType == (int)UserType.WFJUser)
            {
                var userAttorney = _userAttorneyRepository.GetPersonnelByUserID(userId);
                //If user is Collector
                if ((user.IsCollector ?? 0) == 1)
                {
                    var clientIds = (_clientCollectorsRepository.GetClientsByUserID(userId));
                    documents = _formSearchRepository.GetFormListByClientIds(clientIds.Where(x => x.HasValue).Select(x => x.Value).ToList()).ToList();
                }
                //If user AdminStaff
                else if ((user.IsAdminStaff ?? 0) == 1)
                {
                    if (userAttorney > 0)
                    {
                        var clientIds = (_personnelClientsRepository.GetClientsByPersonnelID(userAttorney.Value)).ToList();
                        documents = _formSearchRepository.GetFormListByClientIds(clientIds.Where(x => x.HasValue).Select(x => x.Value).ToList()).ToList();
                    }
                }
                //If user WFJ Attorney
                else if (userAttorney > 0 && (user.IsCollector ?? 0) == 0 && (user.IsAdminStaff ?? 0) == 0)
                {
                    if (userAttorney > 0)
                    {
                        var clientIds = (_personnelClientsRepository.GetClientsByPersonnelID(userAttorney.Value)).ToList();
                        documents = _formSearchRepository.GetFormListByClientIds(clientIds.Where(x => x.HasValue).Select(x => x.Value).ToList()).ToList();
                    }
                }
                else
                {
                    documents = _formSearchRepository.GetFormList(-1, -1, userId).ToList();
                }
            }

            //return (documents.FirstOrDefault(), documents?.Where(x => x.Client != null && x.Client.ID > 0)
            //  ?.Select(x => new SelectListItem() { Text = x.Client.ClientName + "(" + x.FormName + ")", Value = x.Client.ID + "-" + x.ID }).ToList());
            return (documents.FirstOrDefault(), documents?.Where(x => x.Client != null && x.Client.ID > 0)
              ?.Select(x => new SelectListItem() { Text = x.Client.ClientName + "(" + x.FormName + ")", Value = x.ID.ToString() }).ToList());
        }

        /// <summary>
        /// get last 30 days recently joined client
        /// </summary>
        /// <returns></returns>
        private List<RecentlyOpenedClientViewModel> GetRecentlyOpenedClient()
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
        private List<DashboardBaseModel> GetRecentlyOpenedAccount(int formId)
        {
            List<DashboardBaseModel> recentlyOpenedAccountViewModels = new List<DashboardBaseModel>();
            IRequestsRepository _requestsRepository = new RequestsRepository();
            IStatusCodesRepository _statusCodesRepository = new StatusCodesRepository();
            // Currently we are getting last seven days data.
            var requests = _requestsRepository.GetRequestByXDays(Convert.ToInt32(ConfigurationManager.AppSettings["LastXDays"]), formId).GroupBy(x => x.Requestor)
                .Select(x => new RequestModel
                {
                    FormId = x.Max(z => z.FormID),
                    RequestId = x.Max(z => z.ID),
                    StatusCode = x.Max(z => z.StatusCode),
                    RequestDate = x.Max(z => z.RequestDate),
                    ClientName = x.Max(z => z.Form?.Client?.ClientName),
                })
                .OrderByDescending(x => x.RequestDate)
                .ToList();

            if (requests != null && requests.Any())
            {
                foreach (var request in requests)
                {
                    DashboardBaseModel recentlyOpenedAccountViewModel = new DashboardBaseModel();
                    recentlyOpenedAccountViewModel.ClientName = request.ClientName;
                    recentlyOpenedAccountViewModel.Status = GetStatus(Convert.ToInt32(request.StatusCode), Convert.ToInt32(request.FormId));
                    recentlyOpenedAccountViewModel.CustomerName = GetCustomerName(request.RequestId, Convert.ToInt32(request.FormId));
                    recentlyOpenedAccountViewModel.RequestId = request.RequestId;
                    recentlyOpenedAccountViewModel.FormId = Convert.ToInt32(request.FormId);
                    recentlyOpenedAccountViewModels.Add(recentlyOpenedAccountViewModel);
                }
            }
            return recentlyOpenedAccountViewModels;
        }

        /// <summary>
        /// Get Final Demand
        /// </summary>
        /// <returns></returns>
        private List<DashboardBaseModel> GetFinalDemand(int formId)
        {
            List<DashboardBaseModel> finalDemandViewModels = new List<DashboardBaseModel>();
            finalDemandViewModels = _requestsRepository.GetRequestByStatusName("Final Demand Request", formId)
                .Select(x => new DashboardBaseModel
                {
                    CustomerName = GetCustomerName(x.ID, Convert.ToInt32(x.FormID)),
                    ClientName = x.Form.Client.ClientName,
                    Status = GetStatus(Convert.ToInt32(x.StatusCode), Convert.ToInt32(x.FormID))
                }).ToList();

            return finalDemandViewModels;
        }

        private List<ActionRequiredViewModel> GetActionRequired(int formId)
        {
            List<ActionRequiredViewModel> actionRequiredViewModels = new List<ActionRequiredViewModel>();
            IRequestsRepository _requestsRepository = new RequestsRepository();
            actionRequiredViewModels = _requestsRepository.GetRequestOutOfCompliance(formId)
                .Select(x => new ActionRequiredViewModel
                {
                    AttorneyName = x.Personnel?.FullName,
                    CustomerName = GetCustomerName(x.ID, Convert.ToInt32(x.FormID)),
                    ClientName = x.Form.Client.ClientName,
                    Status = GetStatus(Convert.ToInt32(x.StatusCode), Convert.ToInt32(x.FormID)),
                    ComplianceDuration = ComplianceDuration(Convert.ToInt32(x.StatusCode), Convert.ToInt32(x.FormID)),
                    LastNoteDate = x.LastNoteDate.HasValue ? x.LastNoteDate.Value.ToString("MM/dd/yyyy") : "",
                    LastNote = x.RequestNotes.Any() ? x.RequestNotes.OrderByDescending(y => y.NotesDate)?
                    .FirstOrDefault()?.Notes : "",
                    RequestId = x.ID,
                    FormId = Convert.ToInt32(x.FormID)
                }).OrderBy(x => x.AttorneyName).ThenBy(x => x.ClientName).ThenBy(x => x.Status).ThenBy(x => x.CustomerName).ToList();

            return actionRequiredViewModels;
        }

        /// <summary>
        /// Get Compliance Duration by Status Code and FormId
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="formId"></param>
        /// <returns></returns>
        private int? ComplianceDuration(int statusCode, int formId)
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
        private string GetCustomerName(int requestId, int formId)
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
        private string GetStatus(int statusCode, int formId)
        {
            IStatusCodesRepository _statusCodeRepository = new StatusCodesRepository();
            var statusDescription = _statusCodeRepository.GetByStatusCodeAndFormId(statusCode, formId)?.Description;
            return statusDescription;
        }

        /// <summary>
        /// get last 7 days created request
        /// </summary>
        /// <returns></returns>
        private List<ApprovedRecentPayementViewModel> GetApprovedPayment(int approved, int formId)
        {
            List<ApprovedRecentPayementViewModel> approvedPayements = new List<ApprovedRecentPayementViewModel>();
            IRequestsRepository _requestsRepository = new RequestsRepository();
            IStatusCodesRepository _statusCodesRepository = new StatusCodesRepository();
            IPaymentsRepository _paymentRepository = new PaymentsRepository();
            approvedPayements = _paymentRepository.GetPaymentByApprovedAndXDays(approved, formId)
                .Select(x => new ApprovedRecentPayementViewModel
                {
                    FormName = x.Request?.Form?.FormName,
                    PaymentAmount = x.Amount,
                    PaymentDate = x.PaymentDate.HasValue ? x.PaymentDate.Value.ToString("MM/dd/yyyy") : "",
                    PaymentType = x.PaymentType?.PaymentTypeDesc,
                    WFJFees = x.WFJFees,
                    InvoicDate = x.WFJInvoiceDatePaid.HasValue ? x.WFJInvoiceDatePaid.Value.ToString("MM/dd/yyyy") : "",
                    CustomerName = GetCustomerName(x.ID, Convert.ToInt32(x.Request?.FormID)),
                    ClientName = x.Request?.Form.Client.ClientName,
                    Status = GetStatus(Convert.ToInt32(x.Request?.StatusCode), Convert.ToInt32(x.Request?.Form.ID)),
                    InvoiceNumber = x.CheckNumber,
                    PaymentId = x.ID
                })
                .OrderBy(x => x.ClientName).ThenBy(x => x.CustomerName).ThenBy(x => x.FormName).ThenBy(x => x.PaymentDate)
                .ToList();
            return approvedPayements;
        }

        private List<ApprovedRecentPayementViewModel> GetRemittancePayment(int days, int formId)
        {
            List<ApprovedRecentPayementViewModel> remittancePayements = new List<ApprovedRecentPayementViewModel>();
            IRequestsRepository _requestsRepository = new RequestsRepository();
            IStatusCodesRepository _statusCodesRepository = new StatusCodesRepository();
            IPaymentsRepository _paymentRepository = new PaymentsRepository();
            remittancePayements = _paymentRepository.GetRemittancePaymentAndXDays(days, formId)
                .Select(x => new ApprovedRecentPayementViewModel
                {
                    FormName = x.Request?.Form?.FormName,
                    PaymentAmount = x.Amount,
                    PaymentDate = x.PaymentDate.HasValue ? x.PaymentDate.Value.ToString("MM/dd/yyyy") : "",
                    PaymentType = x.PaymentType?.PaymentTypeDesc,
                    WFJFees = x.WFJFees,
                    InvoicDate = x.WFJInvoiceDatePaid.HasValue ? x.WFJInvoiceDatePaid.Value.ToString("MM/dd/yyyy") : "",
                    CustomerName = GetCustomerName(x.ID, Convert.ToInt32(x.Request?.FormID)),
                    ClientName = x.Request?.Form.Client.ClientName,
                    Status = GetStatus(Convert.ToInt32(x.Request?.StatusCode), Convert.ToInt32(x.Request?.Form.ID)),
                    InvoiceNumber = x.CheckNumber,
                    PaymentId = x.ID
                })
                .OrderBy(x => x.ClientName).ThenBy(x => x.CustomerName).ThenBy(x => x.FormName).ThenBy(x => x.PaymentDate)
                .ToList();
            return remittancePayements;
        }

        private List<DashboardBaseModel> GetFollowUpAccounts(int formId)
        {
            List<DashboardBaseModel> followUpAccounts = new List<DashboardBaseModel>();
            return _requestsRepository.FollowUpAccounts(formId)
                .Select(x => new DashboardBaseModel
                {
                    CustomerName = GetCustomerName(x.ID, Convert.ToInt32(x.FormID)),
                    ClientName = x.Form?.Client?.ClientName,
                    Status = GetStatus(Convert.ToInt32(x.StatusCode), Convert.ToInt32(x.FormID)),
                    RequestId = x.ID,
                    FormId = Convert.ToInt32(x.FormID)
                }).ToList();
        }

        public class RequestModel
        {
            public int? StatusCode { get; set; }
            public int RequestId { get; set; }
            public int? FormId { get; set; }
            public DateTime? RequestDate { get; set; }
            public string ClientName { get; set; }
            public string CustomerName { get; set; }

        }

    }
}
