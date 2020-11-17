using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;

namespace WFJ.Repository
{
    public class ListFieldRepository : GenericRepository<ListField>, IListFieldRepository
    {
        public IEnumerable<ListField> GetByUserAndFormID(int UserId, int FormId)
        {
            return _context.ListFields.Where(x => x.UserID == UserId && x.FormID == FormId).AsEnumerable();
        }
    }
}
