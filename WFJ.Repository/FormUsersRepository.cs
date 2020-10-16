using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;

namespace WFJ.Repository
{
    public class FormUsersRepository : GenericRepository<FormUser>, IFormUsersRepository
    {
        private readonly WFJEntities _context;

        public FormUsersRepository()
        {
            _context = new WFJEntities();
        }
        public void DeleteByUserId(int userId)
        {
            _context.FormUsers.RemoveRange(_context.FormUsers.Where(x => x.UserID == userId));
            _context.SaveChanges();
        }
        public List<FormUser> GetByUserId(int userId)
        {
            return _context.FormUsers.Where(x => x.UserID == userId).ToList();
        }
    }
}
