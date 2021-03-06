﻿using System;
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
using WFJ.Service.Model;

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


        public List<SelectListItem> GetActiveInactiveOrderedList(UserType userType)
        {
            IClientRepository clientRepo = new ClientRepository();

            var allClients = clientRepo.GetAll();
            List<SelectListItem> activeClientList = allClients.Where(x => x.Active == 1).Select(x => new SelectListItem() { Text = x.ClientName, Value = x.ID.ToString() }
                ).OrderBy(x => x.Text).ToList();
            List<SelectListItem> inactiveClientList = allClients.Where(x => x.Active == 0).Select(x => new SelectListItem() { Text = x.ClientName, Value = x.ID.ToString() }
                ).OrderBy(x => x.Text).ToList();


            List<SelectListItem> clientDropdownList = new List<SelectListItem>();

            if (UserType.ClientUser != userType || ((UserType.ClientUser == userType && activeClientList.Count() > 1 || inactiveClientList.Count() > 1)))
            {
                clientDropdownList.Add(new SelectListItem { Text = "All", Value = "-1" });
            }

            clientDropdownList.AddRange(activeClientList);
            clientDropdownList.AddRange(inactiveClientList);

            return clientDropdownList;
        }


        /* Client changed dropdown binding
         * public List<SelectListItem> GetRegionsDropdown()
        {
            IClientRepository clientRepo = new ClientRepository();
            List<SelectListItem> regionList = clientRepo.GetAll().Where(x =>x .LevelName != null).Select(x => x.LevelName.Trim()).Where(x => x != "").Distinct().OrderBy(s => s).Select(s => new SelectListItem() { Text = s, Value = s })
                                            .ToList();
            return regionList;
        }*/

        public string GetRequestorNameById(int clientId)
        {
            IClientRepository clientRepo = new ClientRepository();
            return clientRepo.GetById(clientId).RequestorTitle;
        }
    }
}
