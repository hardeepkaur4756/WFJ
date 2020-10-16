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
                return false;
        }

        public List<User> GetUsers(int clientId, int active, string name)
        {

            IEnumerable<User> users;
            IUserClientRepository _userClientRepo = new UserClientRepository();

            if (clientId != -1 || active != -1 || name != "")
            {
                users = _context.Users.Include(s => s.AccessLevel).Include(s=>s.UserClients).Include(s => s.UserClients.Select(y=>y.Client)).Include(s => s.Client).Include(s => s.UserLevels).Include(s => s.UserLevels.Select(y => y.Level)).ToList();
                if (clientId != -1)
                {
                    //users = users.Where(x => x.ClientID == clientId);
                    users = users.Where(x => _userClientRepo.GetByClientID(clientId).Select(y => y.UserID).Contains(x.UserID));
                }
                if (active != -1)
                {
                    users = users.Where(x => x.Active == active);
                }
                if (name != "")
                {
                    users = users.Where(x => (!string.IsNullOrEmpty(x.FirstName)? x.FirstName.ToLower().Contains(name.ToLower()):false)

                    || (!string.IsNullOrEmpty(x.LastName)? x.LastName.ToLower().Contains(name.ToLower()):false)
                    || (!string.IsNullOrEmpty(x.EMail)? x.EMail.ToLower().Contains(name.ToLower()):false)
                    || (!string.IsNullOrEmpty(x.UserName) ? x.UserName.ToLower().Contains(name.ToLower()) : false)
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
