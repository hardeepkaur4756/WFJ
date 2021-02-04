using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;

namespace WFJ.Repository
{
    public class PaymentsRepository : GenericRepository<Payment>, IPaymentsRepository
    {
        public PaymentsRepository()
        {
        }

        public List<Payment> GetByReqestId(int requestId)
        {
            _context.Configuration.LazyLoadingEnabled = true;
            _context.Configuration.ProxyCreationEnabled = true;
            return _context.Payments.Where(x => x.RequestID == requestId).Include(x=> x.User).Include(x => x.Request).Include(x => x.PaymentType).Include(x => x.currency).ToList();
        }
    }
}
