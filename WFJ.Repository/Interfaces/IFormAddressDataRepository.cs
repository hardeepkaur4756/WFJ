using System.Collections.Generic;
using WFJ.Repository.EntityModel;

namespace WFJ.Repository.Interfaces
{
    public interface IFormAddressDataRepository: IRepository<FormAddressData>
    {
        IEnumerable<FormAddressData> GetByRequestId(int requestId);
        void Delete(List<FormAddressData> formAddressDatas);
    }
}
