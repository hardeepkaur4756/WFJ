using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;
using System.Data.Entity;

namespace WFJ.Repository
{
    public class PersonnelClientsRepository : GenericRepository<PersonnelClient>, IPersonnelClientsRepository
    {
        public IEnumerable<Personnel> GetPersonnelsByClientID(int ClientID)
        {
            return _context.PersonnelClients.Include(x => x.Personnel).Where(x => x.ClientID == ClientID && x.Personnel !=null).Select(x => x.Personnel).AsEnumerable();
        }

        public List<int?> GetClientsByPersonnelID(int PersonnelId)
        {
            return _context.PersonnelClients.Include(x => x.Personnel).Where(x => x.Personnel != null && x.Personnel.ID  == PersonnelId).Select(x => x.ClientID).ToList();
        }
    }
}
