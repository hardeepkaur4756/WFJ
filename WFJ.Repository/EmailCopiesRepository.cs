using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;
using System.Data.Entity;

namespace WFJ.Repository
{
    public class EmailCopiesRepository : GenericRepository<EMailCopy>, IEmailCopiesRepository
    {
        public EmailCopiesRepository()
        {
            _context.Configuration.LazyLoadingEnabled = true;
            _context.Configuration.ProxyCreationEnabled = true;
        }

        public List<User> GetUsers()
        {
            var userids = _context.EMailCopies.Where(x => x.Type == "2").Select(x=>x.UserID).ToList();
            return _context.Users.Where(x => userids.Contains(x.UserID)).ToList();
        }
    }
}
