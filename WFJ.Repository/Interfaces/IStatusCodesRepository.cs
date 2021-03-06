﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;

namespace WFJ.Repository.Interfaces
{
    public interface IStatusCodesRepository : IRepository<StatusCode>
    {
        IEnumerable<StatusCode> GetByFormID(int FormID);
        IEnumerable<int?> GetActiveStatusCode(int FormID);
        StatusCode GetByStatusCodeAndFormId(int statusCode, int formId);
        IEnumerable<int?> GetNewAndActiveStatusCode();
        IEnumerable<int?> GetCodesByStatusName(string statusName);
    }
}
