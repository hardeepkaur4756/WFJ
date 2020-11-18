using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;

namespace WFJ.Repository.Interfaces
{
    public interface IStatusCodesRepository : IRepository<StatusCode>
    {
        IEnumerable<StatusCode> GetByFormID(int FormID);
        StatusCode GetByStatusCodeAndFormId(int statusCode, int formId);
    }
}
