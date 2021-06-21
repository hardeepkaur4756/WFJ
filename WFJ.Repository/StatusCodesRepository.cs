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
        //private WFJEntities context;
        //public StatusCodesRepository()
        //{
        //    context = new WFJEntities();
        //}
        public IEnumerable<StatusCode> GetByFormID(int FormID)
        {
            return _context.StatusCodes.Where(x => x.FormID == FormID);
        }

        public IEnumerable<int?> GetActiveStatusCode(int FormID)
        {
            return _context.StatusCodes.Where(x => x.FormID == FormID && x.StatusLevel == 1).Select(x => x.StatusCode1);
        }

        public StatusCode GetByStatusCodeAndFormId(int statusCode, int formId)
        {
            return _context.StatusCodes.FirstOrDefault(x => x.StatusCode1 == statusCode && x.FormID == formId);
        }

        public IEnumerable<int?> GetNewAndActiveStatusCode()
        {
            List<int> codes = new List<int>();
            codes.Add(0);
            codes.Add(1);
            return _context.StatusCodes.Where(x => codes.Contains((Int32)x.StatusLevel)).Select(x => x.StatusCode1).Distinct();
        }

        public IEnumerable<int?> GetCodesByStatusName(string statusName)
        {
            return _context.StatusCodes.Where(x => x.Description.ToLower().Contains(statusName.ToLower())).Select(x => x.StatusCode1).Distinct();
        }
    }
}
