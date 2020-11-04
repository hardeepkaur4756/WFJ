using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WFJ.Repository.EntityModel;

namespace WFJ.Service.Interfaces
{
    public interface IUserClientService
    {
        List<UserClient> GetByUserId(int userId);
        List<SelectListItem> GetUserClients(int userId, byte? activeInactiveCients = null);
        List<SelectListItem> GetManageUsersByClient(List<int?> ClientIds, int userId);
    }
}
