﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;

namespace WFJ.Repository.Interfaces
{
    public interface IPersonnelsRepository : IRepository<Personnel>
    {
        Personnel GetEmailByPersonelRequestId(int requestId);
        IEnumerable<PersonnelRequest> GetPersonnelByFirmId(int firmId);
        IEnumerable<PersonnelRequest> GetPersonnelRequestsByFirmId(int firmId);
    }
}
