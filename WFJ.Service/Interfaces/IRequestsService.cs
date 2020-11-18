﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Models;
using WFJ.Service.Model;

namespace WFJ.Service.Interfaces
{
    public interface IRequestsService
    {
        RequestViewModel GetByRequestId(int RequestID);
        PlacementRequestsListViewModel GetPlacementRequests(int userId, int formId, UserType UserType, int requestor, int assignedAttorney, int collector, int status, string region,
                                                                    string startDate, string toDate, bool archived,
                                                                    DataTablesParam param, string sortDir, string sortCol, int pageNo);
        List<DatatableDynamicColumn> GetDatatableColumns(int UserId, int FormId, UserType UserType);
        void UpdateActiveCode(int code, int requestId);
    }
}
