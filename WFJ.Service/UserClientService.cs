using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WFJ.Repository;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;
using WFJ.Service.Interfaces;

namespace WFJ.Service
{
    public class UserClientService : IUserClientService
    {
        private IUserClientRepository _UserClientRepo = new UserClientRepository();
        public List<UserClient> GetByUserId(int userId)
        {
            return _UserClientRepo.GetByUserId(userId);
        }
        public List<SelectListItem> GetUserClients(int userId)
        {
            List<SelectListItem> clientList = new List<SelectListItem>();
            clientList = _UserClientRepo.GetByUserId(userId).Select(x => new SelectListItem() { Text = x.Client.ClientName, Value = x.Client.ID.ToString() }
                ).ToList();
            return clientList;
        }
    }
}
