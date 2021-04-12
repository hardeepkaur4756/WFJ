using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;

namespace WFJ.Repository
{
   public class AssociateCounselRepository : GenericRepository<AssociateCounsel>, IAssociateCounselRepository
    {
        private readonly WFJEntities _context;
        public AssociateCounselRepository(WFJEntities context)
        {
            _context = context;
        }
        public AssociateCounselRepository()
        {
            _context = new WFJEntities();
            _context.Configuration.LazyLoadingEnabled = true;
            _context.Configuration.ProxyCreationEnabled = true;
        }

        public IEnumerable<AssociateCounsel> GetAssociateCounselList(string firmName, string attorneyName, string city, string state, string country)
        {
            IEnumerable<AssociateCounsel> counsels;

            counsels = _context.AssociateCounsels.Where(a => (a.DoNotUse ?? 0) == 0);
            if (!string.IsNullOrEmpty(firmName))
            {
                counsels = counsels.Where(x => x.FirmName == firmName);
            }
            //this column is not in local DB need to check live DB
            //if (!string.IsNullOrEmpty(attorneyName))
            //{
            //    counsels = counsels.Where(x => x.att == firmName);
            //}
            if (!string.IsNullOrEmpty(city))
            {
                counsels = counsels.Where(x => x.City == city);
            }
            if (!string.IsNullOrEmpty(state))
            {
                counsels = counsels.Where(x => x.State == state);
            }
            if (!string.IsNullOrEmpty(country))
            {
                counsels = counsels.Where(x => x.Country == country);
            }
            return counsels;
        }

        public AssociateCounsel GetAssociateCounselDetailByID(int associateCounselId)
        {
            return _context.AssociateCounsels.FirstOrDefault(x => x.FirmID == associateCounselId);
        }
    }
}
