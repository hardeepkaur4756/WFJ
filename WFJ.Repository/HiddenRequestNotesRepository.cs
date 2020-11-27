using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;

namespace WFJ.Repository
{
    public class HiddenRequestNotesRepository : GenericRepository<hiddenRequestNote>, IHiddenRequestNotesRepository
    {
        public IEnumerable<hiddenRequestNote> GetHiddenNotesByUserAndRequestId(int userId, int requestId)
        {
            return _context.hiddenRequestNotes.Where(x => x.requestID == requestId && x.userID == userId);
        }
    }
}
