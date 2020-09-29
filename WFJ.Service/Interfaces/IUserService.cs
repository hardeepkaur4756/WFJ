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
        void SendForgotPasswordMail(string email);
        void UpdatePassword(string newPassword, string newConfirmPassword, int userId);
    }
}
