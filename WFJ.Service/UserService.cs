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
                    resultModel.Message = "Not a valid email format";
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
                        resultModel.Message = "Password changed successfully";
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
                        if (user.PasswordExpirationDate != null && (DateTime.Now >= Convert.ToDateTime(user.PasswordExpirationDate.Value.AddDays(Convert.ToInt32(ConfigurationManager.AppSettings["ExpiryDays"])))))
                        {
                            resultModel.IsPasswordExpire = true;
                            resultModel.Message = "";
                            return resultModel;
                        }
                        HttpContext.Current.Session["UserType"] = user.UserType;
                        HttpContext.Current.Session["UserId"] = user.UserID.ToString();
                        resultModel.IsSuccess = true;
                        resultModel.Message = "";
                    }
                    else
                    {
                        resultModel.IsSuccess = false;
                        resultModel.Message = "Your passeord is invalid. Please enter valid password.";
                    }
                }
                else
                {
                    resultModel.IsSuccess = false;
                    resultModel.Message = "Your  username/email address is invalid. Please enter vaild username/email address.";
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
    }
}
