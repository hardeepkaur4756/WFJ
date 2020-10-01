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
    }
}
