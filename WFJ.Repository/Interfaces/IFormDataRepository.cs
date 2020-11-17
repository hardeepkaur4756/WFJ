using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;

namespace WFJ.Repository.Interfaces
{
    public interface IFormDataRepository: IRepository<FormData>
    {
        IEnumerable<FormData> GetByRequestId(int requestId);
        void Delete(List<FormData> formDatas);
    }
}
