using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WFJ.Models;
using WFJ.Repository.EntityModel;
using WFJ.Service.Model;

namespace WFJ.Service.Interfaces
{
   public interface IClientService
    {
        List<ClientModel> GetClients();
        List<SelectListItem> GetAllClients();
        List<SelectListItem> GetActiveInactiveOrderedList(UserType userType);
        //List<SelectListItem> GetRegionsDropdown();
        string GetRequestorNameById(int clientId);
    }
}
