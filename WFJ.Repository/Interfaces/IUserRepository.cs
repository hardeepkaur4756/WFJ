using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Models;
using WFJ.Repository.EntityModel;

namespace WFJ.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        //IEnumerable<User> GetAll();
        User GetByEmail(string email);
        User GetByEmailOrUserName(string email);
        User GetByEmailAndPassword(string email, string password);
        bool CheckDuplicateByEmailAndUser(string email, int userId);
        List<User> GetUsers(int clientid, int active, string name);
    }
}
