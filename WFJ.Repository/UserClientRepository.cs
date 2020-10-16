using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;

namespace WFJ.Repository
{
    public class UserClientRepository : GenericRepository<UserClient>, IUserClientRepository
    {
        private readonly WFJEntities _context;

        public UserClientRepository()
        {
            _context = new WFJEntities();
        }
        public void DeleteByUserId(int userId)
        {
            _context.UserClients.RemoveRange(_context.UserClients.Where(x => x.UserID == userId));
            _context.SaveChanges();
        }
        public List<UserClient> GetByUserId(int userId)
        {
            return _context.UserClients.Where(x => x.UserID == userId).ToList();
        }
        public List<UserClient> GetByClientID(int clientID)
        {
            return _context.UserClients.Where(x => x.ClientID == clientID).ToList();
        }
    }
}
