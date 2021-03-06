﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;
namespace WFJ.Repository.Interfaces
{
    public interface IClientRepository: IRepository<Client>
    {
        List<Client> GetAllClientsByXDays(int days);
        Client GetClientByDefaultUserId(int userId);
    }
}
