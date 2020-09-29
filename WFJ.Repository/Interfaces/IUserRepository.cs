using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;

namespace WFJ.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        //IEnumerable<User> GetAll();
        User GetByEmail(string email);
    }
}
