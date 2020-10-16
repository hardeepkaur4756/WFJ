using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;

namespace WFJ.Repository.Interfaces
{
    public interface IUserClientRepository : IRepository<UserClient>
    {
        void DeleteByUserId(int userId);
        List<UserClient> GetByUserId(int userId);
        List<UserClient> GetByClientID(int clientID);
    }
}
