using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;
using WFJ.Service.Interfaces;
using WFJ.Repository;
using WFJ.Models;
using AutoMapper;
using WFJ.Service;
using System.Web.Mvc;

namespace WFJ.Service
{
    public class ClientService:IClientService
    {
        public List<ClientModel> GetClients()
        {
            IClientRepository clientRepository = new ClientRepository();
            var clients=clientRepository.GetAll().ToList();
            return  MappingExtensions.MapList<Client,ClientModel>(clients);
         
        }
        public List<SelectListItem> GetAllClients()
        {
            IClientRepository clientRepo = new ClientRepository();
            List<SelectListItem> clientList = new List<SelectListItem>();
            clientList = clientRepo.GetAll().Select(x => new SelectListItem() { Text = x.ClientName, Value = x.ID.ToString() }
                ).ToList();
            return clientList;
        }
        public List<SelectListItem> GetActiveInactiveOrderedList()
        {
            IClientRepository clientRepo = new ClientRepository();

            var allClients = clientRepo.GetAll();
            List<SelectListItem> activeClientList = allClients.Where(x => x.Active == 1).Select(x => new SelectListItem() { Text = x.ClientName, Value = x.ID.ToString() }
                ).OrderBy(x => x.Text).ToList();
            List<SelectListItem> inactiveClientList = allClients.Where(x => x.Active == 0).Select(x => new SelectListItem() { Text = x.ClientName, Value = x.ID.ToString() }
                ).OrderBy(x => x.Text).ToList();
            activeClientList.AddRange(inactiveClientList);

            return activeClientList;
        }

    }
}
