using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;

namespace WFJ.Repository
{
    public class StatusCodesRepository: GenericRepository<StatusCode>, IStatusCodesRepository
    {
        private WFJEntities context;
        public StatusCodesRepository()
        {
            context = new WFJEntities();
        }
        public List<StatusCode> GetByFormID(int FormID)
        {
            return context.StatusCodes.Where(x => x.FormID == FormID).ToList();
        }

    }
}
