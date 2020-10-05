using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;

namespace WFJ.Repository
{
    public class UserTypeRepository : GenericRepository<User_Type>, IUserTypeRepository
    {
        private WFJEntities context;
        public UserTypeRepository()
        {
            context = new WFJEntities();
        }
    }
}
