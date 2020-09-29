using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
        public void EncryptionPassword()
        {
            IUserRepository userRepo = new UserRepository();
            List<User> users = userRepo.GetAll().Where(x => x.Salt == null && !string.IsNullOrEmpty(x.Password)).ToList();
            foreach (User user in users)
            {
                var ph = new Microsoft.AspNet.Identity.PasswordHasher();
                var hash = ph.HashPassword(user.Password);
                var respnse=ph.VerifyHashedPassword(hash, user.Password);
                user.Password = hash;
                userRepo.Update(user);
            }
            //return userRepo.GetAll();
        }

        public ResultModel SendForgotPasswordMail(string email)
        {
           IUserRepository userRepo = new UserRepository();
            ResultModel resultModel = new ResultModel();
            if (!string.IsNullOrEmpty(email))
            {
                bool isValidemail = ValidateEmail(email);
                if (isValidemail)
                {
                    try
                    {
                        User user = userRepo.GetByEmail(email);
                        if (user != null)
                        {
                            string baseUrl= HttpContext.Current.Request.UrlReferrer.ToString();
                            string queryString = baseUrl+"/Account/ResetPassword/"+"?" + Util.Encode("userId=" + user.UserID);
                            string subject = "Forgot Password";
                            string dirpath = HttpContext.Current.Server.MapPath("/EmailTemplate");
                            string xlsTemplatePath = dirpath + "/ForgotEmail.html";
                            string emailTemplate= File.ReadAllText(xlsTemplatePath);
                            emailTemplate = emailTemplate.Replace("[url]", queryString);
                            emailTemplate = emailTemplate.Replace("[baseurl]", baseUrl);
                            EmailHelper.SendMail(email, subject, emailTemplate);
                            resultModel.IsSuccess = true;
                            resultModel.Message = "Your reset password email has been sent to '" + email + "'.";
                        }
                    }
                    catch (Exception ex)
                    {
                        //message = ex.Message;
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

        public static bool ValidateEmail(string email)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})$";
            //@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z"
            bool isVaidEmail = Regex.IsMatch(email, strRegex, RegexOptions.IgnoreCase);
            if (isVaidEmail) { return (true); } else { return (false); }
        }
        public ResultModel UpdatePassword(string newPassword, string newConfirmPassword, int userId)
        {
            IUserRepository userRepo = new UserRepository();
            ResultModel resultModel = new ResultModel();
            if (newPassword == newConfirmPassword)
            {
               User user= userRepo.GetById(userId);
                if (user != null)
                {
                    var ph = new Microsoft.AspNet.Identity.PasswordHasher();
                    var hash = ph.HashPassword(newPassword);
                    user.Password = hash;
                    user.PasswordExpirationDate = DateTime.Now.AddDays(90);
                    userRepo.Update(user);
                    resultModel.IsSuccess = true;
                    resultModel.Message = "Password changed successfully";
                }
            }
            else
            {
                resultModel.IsSuccess = false;
                resultModel.Message = "The password and confirmation password do not match.";
            }
            return resultModel;
        }

        public ResultModel ChangePassword(string currentPassword,string newPassword, string newConfirmPassword)
        {
            IUserRepository userRepo = new UserRepository();
            ResultModel resultModel = new ResultModel();
            int userId = 0;
            //userid = HttpContext.Current.User.Identity.Name;
            if (newPassword == newConfirmPassword)
            {
                User user = userRepo.GetById(userId);
                if (user != null)
                {
                    var ph = new Microsoft.AspNet.Identity.PasswordHasher();
                    if (ph.VerifyHashedPassword(user.Password, currentPassword).ToString() =="Success")
                    {
                        var hash = ph.HashPassword(newPassword);
                        user.Password = hash;
                        user.PasswordExpirationDate = DateTime.Now.AddDays(90);
                        userRepo.Update(user);
                        resultModel.IsSuccess = true;
                        resultModel.Message = "Password changed successfully";
                    }
                    else
                    {
                        resultModel.IsSuccess = false;
                        resultModel.Message = "The current password is incorrect.";
                    }
                    
                }
            }
            else
            {
                resultModel.IsSuccess = false;
                resultModel.Message = "The password and confirmation password do not match.";
            }
            return resultModel;
        }
    }
}
