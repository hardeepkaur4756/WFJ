using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;

namespace WFJ.Repository.Interfaces
{
    public interface IUserLevelsRepository : IRepository<UserLevel>
    {
        void DeleteByUserId(int userId);
        List<UserLevel> GetByUserId(int userId);
    }
}
