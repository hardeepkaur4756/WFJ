﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Models;

namespace WFJ.Service.Interfaces
{
    public interface IRequestsService
    {
        RequestViewModel GetByRequestId(int RequestID);
    }
}