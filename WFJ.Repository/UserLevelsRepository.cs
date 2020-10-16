using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;

namespace WFJ.Repository
{
    public class UserLevelsRepository : GenericRepository<UserLevel>, IUserLevelsRepository
    {
        private readonly WFJEntities _context;

        public UserLevelsRepository()
        {
            _context = new WFJEntities();
        }
        public void DeleteByUserId(int userId)
        {
            _context.UserLevels.RemoveRange(_context.UserLevels.Where(x => x.UserID == userId));
            _context.SaveChanges();
        }
        public List<UserLevel> GetByUserId(int userId)
        {
            return _context.UserLevels.Where(x => x.UserID == userId).ToList();
        }
    }
}
