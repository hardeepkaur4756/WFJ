using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;
using System.Data.Entity;

namespace WFJ.Repository
{
    public class FormFieldsRepository : GenericRepository<FormField>, IFormFieldsRepository
    {
        public List<FormField> GetFormFieldsByFormID(int FormID)
        {
            return _context.FormFields.Include(x => x.FormSelectionLists).Include(x => x.fieldSize)
                                                                        .Include(x => x.FormDatas)
                                                                        .Include(x => x.FormAddressDatas)
                                                                        .Where(x => x.FormID == FormID).OrderBy(x=>x.rowNumber).ThenBy(x=>x.SeqNo).ToList();
        }

    }
}
