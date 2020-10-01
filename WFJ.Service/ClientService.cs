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
    }
}
