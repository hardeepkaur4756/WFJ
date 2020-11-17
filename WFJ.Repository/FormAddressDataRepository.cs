using System.Collections.Generic;
using System.Linq;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;

namespace WFJ.Repository
{
    public class FormAddressDataRepository : GenericRepository<FormAddressData>, IFormAddressDataRepository
    {
        private readonly WFJEntities _context;

        public FormAddressDataRepository()
        {
            _context = new WFJEntities();
        }

        public void Delete(List<FormAddressData> formAddressDatas)
        {
            _context.FormAddressDatas.RemoveRange(formAddressDatas);
            _context.SaveChanges();
        }

        public IEnumerable<FormAddressData> GetByRequestId(int requestId)
        {
            return _context.FormAddressDatas.Where(x => x.RequestID == requestId).ToList();
        }
    }
}
