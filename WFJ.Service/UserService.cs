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

        public void EncryptionPassword()
        {
            List<User> users = _userRepo.GetAll().Where(x => x.IsPasswordHashed == false && !string.IsNullOrEmpty(x.Password)).ToList();
            foreach (User user in users)
            {
                var ph = new Microsoft.AspNet.Identity.PasswordHasher();
                var hash = ph.HashPassword(user.Password);
                var respnse = ph.VerifyHashedPassword(hash, user.Password);
                user.Password = hash;
                user.PasswordExpirationDate = DateTime.Now;
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
                            string baseUrl = HttpContext.Current.Request.UrlReferrer.ToString();
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
                throw new Exception();
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
            switch (sortCol)
            {
                case "ClientName":
                    if (sortDir == "asc")
                    {
                        users=users.Where(x => x.Client != null)?.OrderBy(x => x.Client.ClientName).ToList();
                    }
                    if (sortDir == "desc")
                    {
                        users = users.Where(x => x.Client != null)?.OrderByDescending(x => x.Client.ClientName).ToList();
                    }
                    break;

                case "Fullname":
                    if (sortDir == "asc")
                    {
                        if (users != null) {
                            users = users.OrderBy(x => x.FirstName).ToList();
                        }                     
                    }
                    
                    if (sortDir == "desc")
                    {
                        if (users!=null)
                        {
                            users = users.OrderByDescending(x => x.FirstName).ToList();
                        }
                        
                    }
                    break;
                case "ManagerName":
                    if (sortDir == "asc")
                    {
                        users = users.OrderBy(x => x.FirstName).ToList();
                    }
                    if (sortDir == "desc")
                    {
                        users = users.OrderByDescending(x => x.FirstName).ToList();
                    }
                    break;
                case "LevelName":
                    if (sortDir == "asc")
                    {
                        users = users.Where(x => x.Level != null)?.OrderBy(x => x.Level.Name).ToList();
                    }
                    if (sortDir == "desc")
                    {
                        users = users.Where(x => x.Level != null)?.OrderByDescending(x => x.Level.Name).ToList();
                    }

                    break;
                case "AccessLevelName":
                    if (sortDir == "asc") {
                        users = users.Where(x => x.AccessLevel != null)?.OrderBy(x => x.AccessLevel.AccessLevel1).ToList();
                    }
                    if (sortDir == "desc")
                    {
                        users = users.Where(x => x.AccessLevel != null)?.OrderByDescending(x => x.AccessLevel.AccessLevel1).ToList();
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

            model.users = MappingExtensions.MapList<User, UserModel>(users?.Skip((pageno - 1) * param.iDisplayLength).Take(param.iDisplayLength).ToList());
            foreach (var item in model.users)
            {
                string FullName = "";
              
                if (item.FirstName != null)
                {
                    FullName = item.FirstName;
                   
                }
                if (item.LastName != null) { FullName = FullName +" "+ item.LastName; }
                item.Fullname = FullName;
                item.ManagerName = FullName;
                if (item.Active == 1 ){
                    item.ActiveStatus = "Yes";
                } else { item.ActiveStatus = "No"; };
            }
            return model;
        }

        public List<SelectListItem> GetAllUserTypes()
        {
            IUserTypeRepository userTypeRepo = new UserTypeRepository();
            List<SelectListItem> userTypeList = new List<SelectListItem>();
            userTypeList = userTypeRepo.GetAll().Select(x => new SelectListItem() { Text = x.UserType, Value = x.ID.ToString() }
                ).Take(5).ToList();
            return userTypeList;
        }

        public List<SelectListItem> GetAllRegions()
        {
            IRegionsRepository regionsRepo = new RegionsRepository();
            List<SelectListItem> regionList = new List<SelectListItem>();
            regionList = regionsRepo.GetAll().Select(x => new SelectListItem() { Text = x.RegionName, Value = x.ID.ToString() }
                ).ToList();
            return regionList;
        }

        public List<SelectListItem> GetAllForms()
        {
            IFormsRepository formsRepo = new FormsRepository();
            List<SelectListItem> fornList = new List<SelectListItem>();
            fornList = formsRepo.GetAll().Select(x => new SelectListItem() { Text = x.FormName, Value = x.ID.ToString() }
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
                    User user = _userRepo.GetById(managerUserFilterViewModel.userViewModel.UserID);
                    user.UserName = managerUserFilterViewModel.userViewModel.FirstName + " " + managerUserFilterViewModel.userViewModel.LastName;
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
                    user.DateAdded = DateTime.Now;
                    user.PasswordExpirationDate = DateTime.Now.AddDays(Convert.ToInt32(ConfigurationManager.AppSettings["ExpiryDays"]));
                    user.IsPasswordHashed = false;
                    _userRepo.Update(user);
                    managerUserFilterViewModel.IsSuccess = true;
                    managerUserFilterViewModel.Message = "Record Updated Successfully.";
                }
                else
                {
                    User user = new User()
                    {
                        UserName = managerUserFilterViewModel.userViewModel.FirstName + " " + managerUserFilterViewModel.userViewModel.LastName,
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
                    };
                    _userRepo.Add(user);
                    managerUserFilterViewModel.IsSuccess = true;
                    managerUserFilterViewModel.Message = "Record Inserted Successfully.";
                }
            }
            catch (Exception ex)
            {
                managerUserFilterViewModel.IsSuccess = false;
                managerUserFilterViewModel.Message = "Sorry, there was an error while processing your request. Please try again later.";
                //throw;
            }
            
        }

        public ManagerUserFilterViewModel GetManageUserById(int userId)
        {
            ManagerUserFilterViewModel managerUserFilterViewModel = new ManagerUserFilterViewModel();
            managerUserFilterViewModel.userViewModel = new UserViewModel();
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
            }
            return managerUserFilterViewModel;
        }

        //public Billing InsertUpdate(BillingViewModel billingVM)
        //{
        //    try
        //    {
        //        if (billingVM != null)
        //        {
        //            // Get  exitting billing by Id
        //            Billing billing = _billingRepository.GetbyId(billingVM.Id);

        //            if (billingVM.Id > 0)
        //            {
        //                billingVM.CreatedOn = billing.CreatedOn;
        //                billingVM.UserId = billing.UserId;
        //                AutoMapper.Mapper.Map(billingVM, billing);

        //                if (billingVM.ProjectTypeId == (int)ProjectTypeEnum.ProjectType.FixPrice)
        //                    billing.Hours = default;
        //                else
        //                    billing.Amount = default;

        //                _billingRepository.Update(billing);
        //                return billing;
        //            }
        //            else
        //            {  // insert billing 
        //                billingVM.CreatedOn = DateTime.Now;
        //                Billing billingEntity = new Billing();
        //                AutoMapper.Mapper.Map(billingVM, billingEntity);
        //                _billingRepository.Insert(billingEntity);
        //                return billingEntity;
        //            }
        //        }
        //        else
        //        {
        //            throw new Exception("billingVM is null.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
    }
}
