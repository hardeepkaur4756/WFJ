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
        public IEnumerable<StatusCode> GetByFormID(int FormID)
        {
            return context.StatusCodes.Where(x => x.FormID == FormID);
        }

        public IEnumerable<int?> GetActiveStatusCode(int FormID)
        {
            return context.StatusCodes.Where(x => x.FormID == FormID && x.StatusLevel == 1).Select(x => x.StatusCode1).AsEnumerable();
        }

        public StatusCode GetByStatusCodeAndFormId(int statusCode, int formId)
        {
            return context.StatusCodes.FirstOrDefault(x => x.StatusCode1 == statusCode && x.FormID == formId);
        }
    }
}
