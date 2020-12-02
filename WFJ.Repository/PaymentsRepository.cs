using System.Collections.Generic;
using System.Linq;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;

namespace WFJ.Repository
{
    public class PaymentsRepository : IPaymentsRepository
    {
        private readonly WFJEntities _context;
        public PaymentsRepository(WFJEntities context)
        {
            _context = context;
        }
        public PaymentsRepository()
        {
            _context = new WFJEntities();
            _context.Configuration.LazyLoadingEnabled = true;
            _context.Configuration.ProxyCreationEnabled = true;
        }

        public List<Payment> GetByReqestId(int requestId)
        {
            return _context.Payments.Where(x => x.RequestID == requestId).ToList();
        }
    }
}
