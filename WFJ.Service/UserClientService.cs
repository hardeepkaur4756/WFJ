using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WFJ.Helper;
using WFJ.Repository;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;
using WFJ.Service.Interfaces;
using WFJ.Service.Model;

namespace WFJ.Service
{
    public class UserClientService : IUserClientService
    {
        private IUserClientRepository _UserClientRepo = new UserClientRepository();
        public List<UserClient> GetByUserId(int userId)
        {
            return _UserClientRepo.GetByUserId(userId);
        }
        public List<SelectListItem> GetUserClients(UserType userType, int userId, byte? activeInactive = null)
        {
            var clients = _UserClientRepo.GetByUserId(userId);
            var clientList = clients.Select(x => new SelectListItem() { Text = x.Client.ClientName, Value = x.Client.ID.ToString() }).Where(x => x.Text != null).ToList();

            if (activeInactive != null)
                clientList = clients.Where(x => x.Client.Active == activeInactive).Select(x => new SelectListItem() { Text = x.Client.ClientName, Value = x.Client.ID.ToString() }).Where(x => x.Text != null).ToList();

            if (UserType.ClientUser != userType || ((UserType.ClientUser == userType && clientList.Count() > 1)))
            {
                clientList = DropdownHelpers.PrependALL(clientList);
            }

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
