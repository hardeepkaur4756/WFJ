using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Models;
using WFJ.Repository.EntityModel;

namespace WFJ.Service.Interfaces
{
    public interface IUserService
    {
        void EncryptionPassword();
        ResultModel SendForgotPasswordMail(string email);
        ResultModel UpdatePassword(string newPassword, string newConfirmPassword, int userId);
        ResultModel ChangePassword(int userId, string currentPassword, string newPassword, string newConfirmPassword);
        ResultModel Login(LoginViewModel loginViewModel);
        ProfileViewModel GetById(int userId);
        ProfileViewModel UpdateProfile(ProfileViewModel profileViewModel);
        bool CheckDuplicateByEmailAndUser(string email, int userId);
        List<UserModel> GetUsers(int clientid, int active, string name, DataTablesParam param, int pageno);

    }
}
