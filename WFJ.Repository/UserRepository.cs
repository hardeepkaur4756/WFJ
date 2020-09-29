using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;

namespace WFJ.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private WFJEntities context;
        public UserRepository()
        {
            context = new WFJEntities();
        }

        //public IEnumerable<User> GetAll()
        //{
        //    return context.Users.Where(x=>x.Salt == null && !string.IsNullOrEmpty(x.Password));
        //}

        public User GetByEmail(string email)
        {
            return context.Users.FirstOrDefault(x => x.EMail == email);
        }


    }
}
