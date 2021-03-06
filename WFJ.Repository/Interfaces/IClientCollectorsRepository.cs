﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;

namespace WFJ.Repository.Interfaces
{
   public interface IClientCollectorsRepository : IRepository<clientCollector>
    {
        List<int?> GetClientsByUserID(int UserID);
    }
}
