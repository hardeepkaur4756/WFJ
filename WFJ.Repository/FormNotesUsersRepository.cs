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
    public class FormNotesUsersRepository : GenericRepository<FormNotesUser>, IFormNotesUsersRepository
    {
        public IEnumerable<FormNotesUser> GetUsersByFormID(int formId)
        {
            return _context.FormNotesUsers.Include(x => x.User).Where(x => x.FormID == formId);
        }
    }
}
