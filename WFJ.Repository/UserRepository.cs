using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Models;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;
using System.Data.Entity;

namespace WFJ.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly WFJEntities _context;
        public UserRepository(WFJEntities context)
        {
            _context = context;
        }
        public UserRepository()
        {
            _context = new WFJEntities();
            _context.Configuration.LazyLoadingEnabled = true;
            _context.Configuration.ProxyCreationEnabled = true;
        }
        

        public User GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(x => x.EMail.ToLower() == email.ToLower());
        }

        public User GetByEmailOrUserName(string email)
        {
            return _context.Users.FirstOrDefault(x => x.EMail.ToLower() == email.ToLower() || x.UserName.ToLower() == email.ToLower());
        }

        public User GetByEmailAndPassword(string email, string password)
        {
            return _context.Users.FirstOrDefault(x => x.EMail.ToLower() == email.ToLower() && x.Password == password);
        }

        public bool CheckDuplicateByEmailAndUser(string email, int userId)
        {
            User user = _context.Users.FirstOrDefault(x => x.EMail.ToLower() == email.ToLower() && x.UserID != userId);
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<User> GetUsers(int clientId, int active, string name)
        {

            IEnumerable<User> users;

            if (clientId != -1 || active != -1 || name != "")
            {
                users = _context.Users.Include(s => s.AccessLevel).Include(s => s.Client).ToList();
                if (clientId != -1)
                {
                    users = users.Where(x => x.ClientID == clientId);
                }
                if (active != -1)
                {
                    users = users.Where(x => x.Active == active);
                }
                if (name != "")
                {
                    users = users.Where(x => x.FirstName.ToLower().Contains(name.ToLower()) || x.LastName.ToLower().Contains(name.ToLower())
                    || x.UserName.ToLower().Contains(name.ToLower()) || x.EMail.ToLower().Contains(name.ToLower())
                    );
                }
            }
            else
            {
                return null;
            }
          
            return users.ToList(); 
        }
    }
}
