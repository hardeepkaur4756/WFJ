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
        public List<SelectListItem> GetUserClients(int userId, byte? activeInactive = null)
        {
            List<SelectListItem> clientList = new List<SelectListItem>();
            var clients = _UserClientRepo.GetByUserId(userId);
            if(activeInactive == null)
                clientList = clients.Select(x => new SelectListItem() { Text = x.Client.ClientName, Value = x.Client.ID.ToString() }
                ).ToList();
            else
                clientList = clients.Where(x => x.Client.Active == activeInactive).Select(x => new SelectListItem() { Text = x.Client.ClientName, Value = x.Client.ID.ToString() }
                ).ToList();
            return clientList;
        }

        public List<SelectListItem> GetManageUsersByClient(List<int?> ClientIds, int userId)
        {
            List<SelectListItem> manageUserList = new List<SelectListItem>();
            if (userId == 0)
            {
                manageUserList = _UserClientRepo.GetAll().Where(x => ClientIds.Contains(x.ClientID) && !string.IsNullOrWhiteSpace(x.User.FirstName)).OrderBy(x => x.User.FirstName).Select(x => x.User).Distinct().Select(x => new SelectListItem() { Text = x.FirstName + " " + x.LastName, Value = x.UserID.ToString() }).ToList();
            }
            else if (userId != 0)
            {
                manageUserList = _UserClientRepo.GetAll().Where(x => ClientIds.Contains(x.ClientID) && x.UserID != userId && !string.IsNullOrWhiteSpace(x.User.FirstName)).OrderBy(x => x.User.FirstName).Select(x=>x.User).Distinct().Select(x => new SelectListItem() { Text = x.FirstName + " " + x.LastName, Value = x.UserID.ToString() }
                    ).ToList();
            }

            return manageUserList;
        }
    }
}
