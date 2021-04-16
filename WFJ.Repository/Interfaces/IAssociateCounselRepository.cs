using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;

namespace WFJ.Repository.Interfaces
{
   public interface IAssociateCounselRepository : IRepository<AssociateCounsel>
    {
        IEnumerable<AssociateCounsel> GetAssociateCounselList(string firmName, string attorneyName,string contactName, string city, string state, string country);
        AssociateCounsel GetAssociateCounselDetailByID(int associateCounselId);
    }
}
