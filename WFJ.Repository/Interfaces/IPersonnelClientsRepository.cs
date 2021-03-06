﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;

namespace WFJ.Repository.Interfaces
{
    public interface IPersonnelClientsRepository: IRepository<PersonnelClient>
    {
        IEnumerable<Personnel> GetPersonnelsByClientID(int ClientID);
        List<int?> GetClientsByPersonnelID(int PersonnelId);
    }
}
