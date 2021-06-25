using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;

namespace WFJ.Repository.Interfaces
{
    public interface IRequestNotesRepository : IRepository<RequestNote>
    {
        IEnumerable<RequestNote> GetRequestNotes(int requestID, bool includeInternalNotes);
        IEnumerable<RequestNote> GetRequestNotesByRequestId(int requestId);

        void Update(List<RequestNote> requestNotes);
    }
}
