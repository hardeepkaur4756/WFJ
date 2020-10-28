using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using WFJ.Helper;
using WFJ.Models;
using WFJ.Repository;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;
using WFJ.Service.Interfaces;
using System.Linq.Dynamic;
using System.Threading;

using System.Web.Mvc;
namespace WFJ.Service
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepo = new UserRepository();
        private IUserClientRepository _userClientRepo = new UserClientRepository();
        private IUserLevelsRepository _userLevelsRepo = new UserLevelsRepository();
        private IFormUsersRepository _formUsersRepo = new FormUsersRepository();
        private ILevelService _levelService = new LevelService();
        private IFormService _formService = new FormService();
        private ILevelRepository _levelRepo = new LevelRepository();
        private IFormsRepository _formRepo = new FormsRepository();
        private IErrorLogService _errorLogService = new ErrorLogService();
        private IUserClientService _userClientService = new UserClientService();

        public void EncryptionPassword()
        {
            List<User> users = _userRepo.GetAll().Where(x => x.IsPasswordHashed == false && !string.IsNullOrEmpty(x.Password)).ToList();
            foreach (User user in users)
            {
                var ph = new Microsoft.AspNet.Identity.PasswordHasher();
                var hash = ph.HashPassword(user.Password);
                var respnse = ph.VerifyHashedPassword(hash, user.Password);
                user.Password = hash;
                user.PasswordExpirationDate = DateTime.Now.AddDays(Convert.ToInt32(ConfigurationManager.AppSettings["ExpiryDays"])); ;
                user.IsPasswordHashed = true;
                _userRepo.Update(user);
            }
        }

        public ResultModel SendForgotPasswordMail(string email)
        {
            ResultModel resultModel = new ResultModel();
            if (!string.IsNullOrEmpty(email))
            {
                bool isValidEmail = Util.ValidateEmail(email);
                if (isValidEmail)
                {
                    try
                    {
                        User user = _userRepo.GetByEmail(email);
                        if (user != null)
                        {
                            string baseUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace("/Account/ForgotPassword", "");
                            string queryString = baseUrl + "/Account/ResetPassword/" + "?" + Util.Encode("userId=" + user.UserID);
                            string subject = "Forgot Password";
                            string dirpath = HttpContext.Current.Server.MapPath("/EmailTemplate");
                            string xlsTemplatePath = dirpath + "/ForgotEmail.html";
                            string emailTemplate = File.ReadAllText(xlsTemplatePath);
                            emailTemplate = emailTemplate.Replace("[url]", queryString);
                            emailTemplate = emailTemplate.Replace("[baseurl]", baseUrl);
                            EmailHelper.SendMail(email, subject, emailTemplate);
                            resultModel.IsSuccess = true;
                            resultModel.Message = "Your reset password email has been sent to '" + email + "'.";
                        }
                        else
                        {
                            // Do code here.
                            resultModel.IsSuccess = false;
                            resultModel.Message = "No User Found.";
                        }
                    }
                    catch (Exception ex)
                    {
                        resultModel.IsSuccess = false;
                        _errorLogService.Add(new ErrorLogModel() { Page = "UserService/SendForgotPasswordMail", CreatedBy = Convert.ToInt32(HttpContext.Current.Session["UserId"]), CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                        resultModel.Message = "Sorry, there was an error while processing your request. Please try again later.";
                    }

                }
                else
                {
                    resultModel.IsSuccess = false;
                    resultModel.Message = "Not a valid email format.";
                }
            }
            else
            {
                resultModel.IsSuccess = false;
                resultModel.Message = "Email address can not be set Empty";
            }
            return resultModel;
        }

        public ResultModel UpdatePassword(string newPassword, string newConfirmPassword, int userId)
        {
            ResultModel resultModel = new ResultModel();
            if (newPassword == newConfirmPassword)
            {
                User user = _userRepo.GetById(userId);
                if (user != null)
                {
                    var ph = new Microsoft.AspNet.Identity.PasswordHasher();
                    var hash = ph.HashPassword(newPassword);
                    user.Password = hash;
                    user.PasswordExpirationDate = DateTime.Now.AddDays(Convert.ToInt32(ConfigurationManager.AppSettings["ExpiryDays"]));
                    _userRepo.Update(user);
                    resultModel.IsSuccess = true;
                    resultModel.Message = "Password changed successfully.";
                }
                else
                {
                    // do code here.
                    resultModel.IsSuccess = false;
                    resultModel.Message = "No User Found.";

                }
            }
            else
            {
                resultModel.IsSuccess = false;
                resultModel.Message = "The password and confirmation password do not match.";
            }
            return resultModel;
        }

        public ResultModel ChangePassword(int userId, string currentPassword, string newPassword, string newConfirmPassword)
        {
            ResultModel resultModel = new ResultModel();
            if (newPassword == newConfirmPassword)
            {
                User user = _userRepo.GetById(userId);
                if (user != null)
                {
                    var ph = new Microsoft.AspNet.Identity.PasswordHasher();
                    if (ph.VerifyHashedPassword(user.Password, currentPassword).ToString() == "Success")
                    {
                        if (user.Active ==1)
                        {
                            var hash = ph.HashPassword(newPassword);
                            user.Password = hash;
                            user.PasswordExpirationDate = DateTime.Now.AddDays(Convert.ToInt32(ConfigurationManager.AppSettings["ExpiryDays"]));
                            _userRepo.Update(user);
                            resultModel.IsSuccess = true;
                            resultModel.Message = "Password changed successfully.";
                        }
                        else
                        {
                            resultModel.IsSuccess = false;
                            resultModel.Message = "Your account is inactive, please contact your WFJ Administrator";
                        }
                       
                    }
                    else
                    {
                        resultModel.IsSuccess = false;
                        resultModel.Message = "The current password is incorrect.";
                    }
                }
                else
                {
                    // do code here.
                    resultModel.IsSuccess = false;
                    resultModel.Message = "No User Found.";
                }
            }
            else
            {
                resultModel.IsSuccess = false;
                resultModel.Message = "The password and confirmation password do not match.";
            }
            return resultModel;
        }

        public ResultModel Login(LoginViewModel loginViewModel)
        {
            ResultModel resultModel = new ResultModel();
            if (!string.IsNullOrEmpty(loginViewModel.Email))
            {
                var ph = new Microsoft.AspNet.Identity.PasswordHasher();
                var hash = ph.HashPassword(loginViewModel.Password);
                User user = _userRepo.GetByEmailOrUserName(loginViewModel.Email);
                if (user != null)
                {
                    if (ph.VerifyHashedPassword(user.Password, loginViewModel.Password).ToString() == "Success")
                    {
                        if (user.Active ==1)
                        {
                            HttpContext.Current.Session["UserType"] = user.UserType;
                            HttpContext.Current.Session["UserId"] = Convert.ToString(user.UserID);
                            //if (user.PasswordExpirationDate != null && (DateTime.Now >= Convert.ToDateTime(user.PasswordExpirationDate.Value.AddDays(Convert.ToInt32(ConfigurationManager.AppSettings["ExpiryDays"])))))
                            if (user.PasswordExpirationDate != null && (DateTime.Now > Convert.ToDateTime(user.PasswordExpirationDate.Value)))
                            {
                                resultModel.IsPasswordExpire = true;
                                resultModel.Message = "";
                                return resultModel;
                            }
                            resultModel.IsSuccess = true;
                            resultModel.Message = "";
                        }
                        else
                        {
                            resultModel.IsSuccess = false;
                            resultModel.Message = "Your account is inactive, please contact your WFJ Administrator";
                        }
                        
                    }
                    else
                    {
                        resultModel.IsSuccess = false;
                        resultModel.Message = "Please enter valid password.";
                    }
                }
                else
                {
                    resultModel.IsSuccess = false;
                    resultModel.Message = "Please enter vaild username/email address.";
                }
            }
            else
            {
                resultModel.IsSuccess = false;
                resultModel.Message = "Email address can not be set Empty.";
            }
            return resultModel;
        }

        public ProfileViewModel GetById(int userId)
        {
            ProfileViewModel profileViewModel = new ProfileViewModel();
            User user = _userRepo.GetById(userId);
            if (user != null)
            {
                profileViewModel.FirstName = user.FirstName;
                profileViewModel.LastName = user.LastName;
                profileViewModel.Telephone = user.Telephone;
                profileViewModel.Address1 = user.Address1;
                profileViewModel.Address2 = user.Address2;
                profileViewModel.City = user.City;
                profileViewModel.State = user.State;
                profileViewModel.PostalCode = user.PostalCode;
                profileViewModel.Email = user.EMail;
            }
            return profileViewModel;
        }
        public ProfileViewModel UpdateProfile(ProfileViewModel profileViewModel)
        {
            User user = _userRepo.GetById(profileViewModel.UserId);
            if (user != null)
            {
                user.FirstName = profileViewModel.FirstName;
                user.LastName = profileViewModel.LastName;
                user.Telephone = profileViewModel.Telephone;
                user.Address1 = profileViewModel.Address1;
                user.Address2 = profileViewModel.Address2;
                user.City = profileViewModel.City;
                user.State = profileViewModel.State;
                user.PostalCode = profileViewModel.PostalCode;
                user.EMail = profileViewModel.Email;
                _userRepo.Update(user);
            }
            else
            {
                throw new Exception("msg");
            }
            return profileViewModel;
        }

        public bool CheckDuplicateByEmailAndUser(string email, int userId)
        {
            return _userRepo.CheckDuplicateByEmailAndUser(email, userId);
        }
        public ManageUserModel GetUsers(int clientid, int active, string name, DataTablesParam param, int pageno, string sortDir, string sortCol)
        {
            ManageUserModel model = new ManageUserModel();
            IUserRepository userRepository = new UserRepository();
            var users = userRepository.GetUsers(clientid, active, name);
            model.totalUsersCount = users?.Count();
            if (users != null)
            {
                switch (sortCol)
                {
                    case "ClientName":
                        if (sortDir == "asc")
                        {
                            users = users.OrderBy(x => x.UserClients.FirstOrDefault()?.Client.ClientName).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            users = users.OrderByDescending(x => x.UserClients.FirstOrDefault()?.Client.ClientName).ToList();
                        }
                        break;

                    case "Fullname":
                        if (sortDir == "asc")
                        {
                            users = users.OrderBy(x => x.FirstName).ThenBy(x => x.LastName).ToList();
                        }

                        if (sortDir == "desc")
                        {
                            users = users.OrderByDescending(x => x.FirstName).ThenByDescending(x => x.LastName).ToList();
                        }
                        break;
                    case "ManagerName":
                        if (sortDir == "asc")
                        {
                            users = users.OrderBy(x => x.User1?.FirstName).ThenBy(x => x.User1?.LastName).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            users = users.OrderByDescending(x => x.User1?.FirstName).ThenByDescending(x => x.User1?.LastName).ToList();           
                        }
                        break;
                    case "LevelName":
                        if (sortDir == "asc")
                        {
                            users = users.OrderBy(x => x.UserLevels?.FirstOrDefault()?.Level.Name).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            users = users.OrderByDescending(x => x.UserLevels?.FirstOrDefault()?.Level.Name).ToList();
                        }

                        break;
                    case "AccessLevelName":
                        if (sortDir == "asc")
                        {
                            users = users.OrderBy(x => x.AccessLevel?.AccessLevel1).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            users = users.OrderByDescending(x => x.AccessLevel?.AccessLevel1).ToList();
                        }
                        break;
                    case "ActiveStatus":
                        if (sortDir == "asc")
                        {
                            users = users.OrderBy(x => x.Active).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            users = users.OrderByDescending(x => x.Active).ToList();
                        }
                        break;
                    default:
                        break;
                }
            }


            model.users = users == null ? new List<UserModel>() : users?.Skip((pageno - 1) * param.iDisplayLength).Take(param.iDisplayLength)?.Select(x => new UserModel
            {
                UserID = x.UserID,
                UserName = x.UserName,
                Password = x.Password,
                Title = x.Title,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Telephone = x.Telephone,
                Fax = x.Fax,
                Address1 = x.Address1,
                Address2 = x.Address2,
                City = x.City,
                State = x.State,
                PostalCode = x.PostalCode,
                Image = x.Image,
                DateAdded = x.DateAdded,
                UserType = x.UserType,
                UserAccess = x.UserAccess,
                LogonCount = x.LogonCount,
                EMail = x.EMail,
                ManagerUserID = x.ManagerUserID,
                ClientID = x.ClientID,
                AttorneyID = x.AttorneyID,
                levelID = x.levelID,
                PasswordExpirationDate = x.PasswordExpirationDate,
                Active = x.Active,
                IsCollector = x.IsCollector,
                IsAdminStaff = x.IsAdminStaff,
                HireDate = x.HireDate,
                Birthdate = x.Birthdate,
                AccessClientExtranet = x.AccessClientExtranet,
                ProfileText = x.ProfileText,
                showHRSection = x.showHRSection,
                dashboardUser = x.dashboardUser,
                ClientName = GetName(string.Join(", ", x.UserClients.Where(y => !string.IsNullOrEmpty(y.Client.ClientName)).Select(y => y.Client.ClientName))),
                LevelName = GetName(string.Join(", ", x.UserLevels.Where(y => !string.IsNullOrEmpty(y.Level.Name)).Select(y => y.Level.Name))),
                AccessLevelName = x.AccessLevel?.AccessLevel1,
                Fullname = !string.IsNullOrEmpty(x.FirstName) ? (x.FirstName + " " + x.LastName) : x.LastName,
                ManagerName = !string.IsNullOrEmpty(x.User1?.FirstName) ? (x.User1?.FirstName + " " + x.User1?.LastName) : x.User1?.LastName,
                ActiveStatus = x.Active == 1 ? "Yes" : "No"
            }).ToList();
            return model;
        }

        public List<SelectListItem> GetAllUserTypes()
        {
            IUserTypeRepository userTypeRepo = new UserTypeRepository();
            List<SelectListItem> userTypeList = new List<SelectListItem>();
            userTypeList = userTypeRepo.GetAll().Select(x => new SelectListItem() { Text = x.UserType, Value = x.ID.ToString() }
                ).ToList();
            return userTypeList;
        }

        public List<SelectListItem> GetAllRegions()
        {
            IRegionsRepository regionsRepo = new RegionsRepository();
            List<SelectListItem> regionList = new List<SelectListItem>();
            regionList = regionsRepo.GetAll().Where(x=>!string.IsNullOrEmpty(x.RegionName)).Select(x => new SelectListItem() { Text = x.RegionName, Value = x.ID.ToString() }
                ).ToList();
            return regionList;
        }

        public List<SelectListItem> GetAllForms()
        {
            IFormsRepository formsRepo = new FormsRepository();
            List<SelectListItem> fornList = new List<SelectListItem>();
            fornList = formsRepo.GetAll().Where(x => !string.IsNullOrEmpty(x.FormName)).Select(x => new SelectListItem() { Text = x.FormName, Value = x.ID.ToString() }
                ).ToList();
            return fornList;
        }

        public void AddOrUpdate(ManagerUserFilterViewModel managerUserFilterViewModel)
        {
            var ph = new Microsoft.AspNet.Identity.PasswordHasher();

            try
            {
                
                if (managerUserFilterViewModel.userViewModel.UserID > 0)
                {
                    if (!CheckDuplicateByEmailAndUser(managerUserFilterViewModel.userViewModel.Email, managerUserFilterViewModel.userViewModel.UserID))
                    {
                        User user = _userRepo.GetById(managerUserFilterViewModel.userViewModel.UserID);
                        user.UserName = managerUserFilterViewModel.userViewModel.FirstName + managerUserFilterViewModel.userViewModel.LastName;
                        user.FirstName = managerUserFilterViewModel.userViewModel.FirstName;
                        user.LastName = managerUserFilterViewModel.userViewModel.LastName;
                        user.Telephone = managerUserFilterViewModel.userViewModel.Telephone;
                        user.Address1 = managerUserFilterViewModel.userViewModel.Address1;
                        user.Address2 = managerUserFilterViewModel.userViewModel.Address2;
                        user.City = managerUserFilterViewModel.userViewModel.City;
                        user.State = managerUserFilterViewModel.userViewModel.State;
                        user.PostalCode = managerUserFilterViewModel.userViewModel.PostalCode;
                        user.EMail = managerUserFilterViewModel.userViewModel.Email;
                        user.UserType = managerUserFilterViewModel.userViewModel.UserType;
                        user.dashboardUser = managerUserFilterViewModel.userViewModel.IsDashboardUser;
                        user.Active = managerUserFilterViewModel.userViewModel.IsActive;
                        user.ManagerUserID = managerUserFilterViewModel.userViewModel.ManagerUserId>0 ? managerUserFilterViewModel.userViewModel.ManagerUserId : null;
                        user.UserAccess = managerUserFilterViewModel.userViewModel.AccessLevelId;
                        _userRepo.Update(user);
                        _userClientRepo.DeleteByUserId(managerUserFilterViewModel.userViewModel.UserID);
                        if (managerUserFilterViewModel.userViewModel.ClientId != null)
                        {
                            foreach (var itemId in managerUserFilterViewModel.userViewModel.ClientId)
                            {
                                UserClient uClient = new UserClient()
                                {
                                    UserID = managerUserFilterViewModel.userViewModel.UserID,
                                    ClientID = Convert.ToInt32(itemId)
                                };
                                _userClientRepo.Add(uClient);
                            }
                        }
                        _userLevelsRepo.DeleteByUserId(managerUserFilterViewModel.userViewModel.UserID);
                        if (managerUserFilterViewModel.userViewModel.RegionId != null)
                        {
                            foreach (var itemId in managerUserFilterViewModel.userViewModel.RegionId)
                            {
                                UserLevel userLevel = new UserLevel()
                                {
                                    UserID = managerUserFilterViewModel.userViewModel.UserID,
                                    LevelID = Convert.ToInt32(itemId)
                                };
                                _userLevelsRepo.Add(userLevel);
                            }
                        }
                        _formUsersRepo.DeleteByUserId(managerUserFilterViewModel.userViewModel.UserID);
                        if (managerUserFilterViewModel.userViewModel.FormId != null)
                        {
                            foreach (var itemId in managerUserFilterViewModel.userViewModel.FormId)
                            {
                                FormUser uClient = new FormUser()
                                {
                                    UserID = managerUserFilterViewModel.userViewModel.UserID,
                                    FormID = Convert.ToInt32(itemId)
                                };
                                _formUsersRepo.Add(uClient);
                            }
                        }
                        managerUserFilterViewModel.IsSuccess = true;
                        managerUserFilterViewModel.Message = "Record Updated Successfully.";
                    }
                    else
                    {
                        managerUserFilterViewModel.IsSuccess = false;
                        managerUserFilterViewModel.Message = "Email address already exist.";
                    }
                   
                }
                else
                {
                    if (!CheckDuplicateByEmail(managerUserFilterViewModel.userViewModel.Email))
                    {
                        User user = new User()
                        {
                            UserName = managerUserFilterViewModel.userViewModel.FirstName  + managerUserFilterViewModel.userViewModel.LastName,
                            Password = ph.HashPassword(managerUserFilterViewModel.userViewModel.Password),
                            FirstName = managerUserFilterViewModel.userViewModel.FirstName,
                            LastName = managerUserFilterViewModel.userViewModel.LastName,
                            Telephone = managerUserFilterViewModel.userViewModel.Telephone,
                            Address1 = managerUserFilterViewModel.userViewModel.Address1,
                            Address2 = managerUserFilterViewModel.userViewModel.Address2,
                            City = managerUserFilterViewModel.userViewModel.City,
                            State = managerUserFilterViewModel.userViewModel.State,
                            PostalCode = managerUserFilterViewModel.userViewModel.PostalCode,
                            EMail = managerUserFilterViewModel.userViewModel.Email,
                            UserType = managerUserFilterViewModel.userViewModel.UserType,
                            dashboardUser = managerUserFilterViewModel.userViewModel.IsDashboardUser,
                            Active = managerUserFilterViewModel.userViewModel.IsActive,
                            DateAdded = DateTime.Now,
                            PasswordExpirationDate = DateTime.Now.AddDays(Convert.ToInt32(ConfigurationManager.AppSettings["ExpiryDays"])),
                            IsPasswordHashed = false,
                            ManagerUserID= managerUserFilterViewModel.userViewModel.ManagerUserId>0? managerUserFilterViewModel.userViewModel.ManagerUserId:null,
                            UserAccess=managerUserFilterViewModel.userViewModel.AccessLevelId
                        };
                        _userRepo.Add(user);
                        if (user.UserID > 0 && (managerUserFilterViewModel.userViewModel.ClientId != null))
                        {
                            foreach (var itemId in managerUserFilterViewModel.userViewModel.ClientId)
                            {
                                UserClient dClient = new UserClient()
                                {
                                    UserID = user.UserID,
                                    ClientID = Convert.ToInt32(itemId)
                                };
                                _userClientRepo.Add(dClient);
                            }

                        }
                        if (user.UserID > 0 && (managerUserFilterViewModel.userViewModel.RegionId != null))
                        {
                            foreach (var itemId in managerUserFilterViewModel.userViewModel.RegionId)
                            {
                                UserLevel userLevel = new UserLevel()
                                {
                                    UserID = user.UserID,
                                    LevelID = Convert.ToInt32(itemId)
                                };
                                _userLevelsRepo.Add(userLevel);
                            }

                        }
                        if (user.UserID > 0 && (managerUserFilterViewModel.userViewModel.FormId != null))
                        {
                            foreach (var itemId in managerUserFilterViewModel.userViewModel.FormId)
                            {
                                FormUser formUser = new FormUser()
                                {
                                    UserID = user.UserID,
                                    FormID = Convert.ToInt32(itemId)
                                };
                                _formUsersRepo.Add(formUser);
                            }

                        }

                        managerUserFilterViewModel.IsSuccess = true;
                        managerUserFilterViewModel.Message = "Record Inserted Successfully.";
                    }
                    else
                    {
                        managerUserFilterViewModel.IsSuccess = false;
                        managerUserFilterViewModel.Message = "Email address already exist.";
                    }
                }
            }
            catch (Exception ex)
            {
                managerUserFilterViewModel.IsSuccess = false;
                managerUserFilterViewModel.Message = "Sorry, An error occurred!";
                
            }
            
        }

        public ManagerUserFilterViewModel GetManageUserById(int userId)
        {
           
            ManagerUserFilterViewModel managerUserFilterViewModel = new ManagerUserFilterViewModel();
            managerUserFilterViewModel.userViewModel = new UserViewModel();
            IClientService _clientService = new ClientService();
            managerUserFilterViewModel.Clients = _clientService.GetAllClients();
            managerUserFilterViewModel.AccessLevels = GetAllAccessLevels();
          

            User user = _userRepo.GetById(userId);
            if (user != null)
            {
                managerUserFilterViewModel.userViewModel.UserID = user.UserID;
                managerUserFilterViewModel.userViewModel.FirstName = user.FirstName;
                managerUserFilterViewModel.userViewModel.LastName = user.LastName;
                managerUserFilterViewModel.userViewModel.Telephone = user.Telephone;
                managerUserFilterViewModel.userViewModel.Address1 = user.Address1;
                managerUserFilterViewModel.userViewModel.Address2 = user.Address2;
                managerUserFilterViewModel.userViewModel.City = user.City;
                managerUserFilterViewModel.userViewModel.State = user.State;
                managerUserFilterViewModel.userViewModel.PostalCode = user.PostalCode;
                managerUserFilterViewModel.userViewModel.Email = user.EMail;
                managerUserFilterViewModel.userViewModel.UserType = user.UserType;
                managerUserFilterViewModel.userViewModel.IsActive = user.Active;
                managerUserFilterViewModel.userViewModel.IsDashboardUser = user.dashboardUser;
                managerUserFilterViewModel.userViewModel.Password = user.Password;
                managerUserFilterViewModel.userViewModel.ClientId = _userClientRepo.GetByUserId(userId).Select(x => Convert.ToInt32(x.ClientID)).ToArray();
                managerUserFilterViewModel.Regions = GetRegionsByClient(managerUserFilterViewModel.userViewModel.ClientId.Select(x=>(int?)x).ToList());
                managerUserFilterViewModel.Forms = GetFormsByClient(managerUserFilterViewModel.userViewModel.ClientId.Select(x => (int?)x).ToList());
                managerUserFilterViewModel.userViewModel.RegionId = _userLevelsRepo.GetByUserId(userId).Select(x => Convert.ToInt32(x.LevelID)).ToArray();
                managerUserFilterViewModel.userViewModel.FormId = _formUsersRepo.GetByUserId(userId).Select(x => Convert.ToInt32(x.FormID)).ToArray();
                managerUserFilterViewModel.userViewModel.AccessLevelId = user.UserAccess;
                managerUserFilterViewModel.ManageUsers =  _userClientService.GetManageUsersByClient(managerUserFilterViewModel.userViewModel.ClientId.Select(x => (int?)x).ToList(), userId);
                managerUserFilterViewModel.userViewModel.ManagerUserId = user.ManagerUserID;
            }

            foreach (var item in managerUserFilterViewModel.Clients)
            {
                if (managerUserFilterViewModel.userViewModel.ClientId.Any(x => x.ToString() == item.Value))
                {
                    item.Selected = true;
                }
            }
            foreach (var item in managerUserFilterViewModel.Regions)
            {
                if (managerUserFilterViewModel.userViewModel.RegionId.Any(x => x.ToString() == item.Value))
                {
                    item.Selected = true;
                }
            }
            foreach (var item in managerUserFilterViewModel.Forms)
            {
                if (managerUserFilterViewModel.userViewModel.FormId.Any(x => x.ToString() == item.Value))
                {
                    item.Selected = true;
                }
            }
            return managerUserFilterViewModel;
        }

       
        public bool CheckDuplicateByEmail(string email)
        {
            User user = _userRepo.GetByEmail(email);
            if (user != null)
            {
                return true;
            }
            return false;
        }
        public List<SelectListItem> GetRegionsByClient(List<int?> ClientIds)
        {
            ILevelRepository levelRepo = new LevelRepository();
            List<SelectListItem> regionList = new List<SelectListItem>();
            regionList = levelRepo.GetAll().Where(x => ClientIds.Contains(x.ClientID) && !string.IsNullOrEmpty(x.Name)).Select(x => new SelectListItem() { Text = x.Name, Value = x.ID.ToString() }
                ).ToList();
            return regionList;
        }
        public List<SelectListItem> GetFormsByClient(List<int?> ClientIds)
        {
            IFormsRepository formsRepo = new FormsRepository();
            List<SelectListItem> fornList = new List<SelectListItem>();
            fornList = formsRepo.GetAll().Where(x => ClientIds.Contains(x.ClientID) && !string.IsNullOrEmpty(x.FormName)).Select(x => new SelectListItem() { Text = x.FormName, Value = x.ID.ToString() }
                ).ToList();
            return fornList;
        }

        public List<SelectListItem> GetAllAccessLevels()
        {
            IAccessLevelsRepository accessLevelRepo = new AccessLevelsRepository();
            List<SelectListItem> accessLevelList = new List<SelectListItem>();
            accessLevelList = accessLevelRepo.GetAll().Select(x => new SelectListItem() { Text = x.AccessLevel1, Value = x.ID.ToString() }
                ).ToList();
            return accessLevelList;
        }

        private string GetName(string stringName)
        {
            if (!string.IsNullOrEmpty(stringName) && stringName.Length > 50)
            {
                stringName = stringName.Substring(0, 50) + "...";
            }
            return stringName;
        }
    }
}
