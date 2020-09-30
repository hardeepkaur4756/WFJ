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
                bool isValidemail = Util.ValidateEmail(email);
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

        public ResultModel ChangePassword(int userId,string currentPassword,string newPassword, string newConfirmPassword)
        {
            IUserRepository userRepo = new UserRepository();
            ResultModel resultModel = new ResultModel();
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

        public ResultModel Login(LoginModel loginModel)
        {
            ResultModel resultModel = new ResultModel();
            IUserRepository userRepo = new UserRepository();
            if (!string.IsNullOrEmpty(loginModel.EMail))
            {
                bool validEmail = Util.ValidateEmail(loginModel.EMail.Trim());
                if (validEmail)
                {
                    var ph = new Microsoft.AspNet.Identity.PasswordHasher();
                    var hash = ph.HashPassword(loginModel.Password);
                    User user = userRepo.GetByEmail(loginModel.EMail);
                    //User user = userRepo.GetByEmailAndPassword(userModel.EMail, hash);
                    if (user != null)
                    {
                        if (user.PasswordExpirationDate != null && (DateTime.Now >= Convert.ToDateTime(user.PasswordExpirationDate.Value.AddDays(90))))
                        {
                            resultModel.IsPasswordExpire = true;
                            resultModel.Message = "";
                        }
                        else if (ph.VerifyHashedPassword(user.Password, loginModel.Password).ToString() == "Success")
                        {
                            HttpContext.Current.Session["UserType"] = user.UserType;
                            HttpContext.Current.Session["UserId"] = user.UserID.ToString();
                            resultModel.IsSuccess = true;
                            resultModel.Message = "";
                        }
                        else
                        {
                            resultModel.IsSuccess = false;
                            resultModel.Message = "Your passeord is invalid. Please enter valid password";
                        }
                    }
                    else
                    {
                        //User Name not found in SponsorInputs345x and also in Sponsor_Closed_Accounts
                        resultModel.IsSuccess = false;
                        resultModel.Message = "Your  username is invalid. Please enter vaild username";
                        //resultModel.Message = "Your  username or passeord is invalid. Please try again";
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

        public UserModel EditProfile(UserModel userModel)
        {
            ResultModel resultModel = new ResultModel();
            IUserRepository userRepo = new UserRepository();
            User user = userRepo.GetById(userModel.UserID);
            if (user !=null)
            {
                user.FirstName = userModel.FirstName;
                user.LastName = userModel.LastName;
                user.Telephone = userModel.Telephone;
                user.Address1 = userModel.Telephone;
                user.Address2 = userModel.LastName;
                user.City = userModel.City;
                user.State = userModel.State;
                user.PostalCode = userModel.PostalCode;
                user.EMail = userModel.EMail;
                userRepo.Update(user);
            }
            return userModel;
        }
       
    }
}
