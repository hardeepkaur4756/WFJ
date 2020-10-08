using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Models;
using WFJ.Repository;
using WFJ.Service.Interfaces;
using WFJ.Repository.Interfaces;
using AutoMapper;
using WFJ.Repository.EntityModel;

namespace WFJ.Service
{
    public class ErrorLogService: IErrorLogService
    {
        public void Add(ErrorLogModel errorLogModel)
        {
            IErrorLogRepository errorLogRepo = new ErrorLogRepository();
            errorLogRepo.Add(MappingExtensions.MapTo<ErrorLogModel,ErrorLog>(errorLogModel));
        }

    }
}
