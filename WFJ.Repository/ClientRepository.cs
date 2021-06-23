using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;
namespace WFJ.Repository
{
   public class ClientRepository: GenericRepository<Client>, IClientRepository
    {
        private WFJEntities context;

        public ClientRepository()
        {
            context = new WFJEntities();
        }

        public List<Client> GetAllClientsByXDays(int days)
        {
            DateTime date = DateTime.Now.AddDays(days);
            return context.Clients.Where(x=> x.dtCreated != null && x.dtCreated.Value >= date).ToList();
        }
        public Client GetClientByDefaultUserId(int userId)
        {
            return context.Clients.FirstOrDefault(x => x.defaultUserID == userId);
        }
    }
}
