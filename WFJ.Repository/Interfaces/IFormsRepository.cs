using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;

namespace WFJ.Repository.Interfaces
{
    public interface IFormsRepository: IRepository<Form>
    {
        IEnumerable<Form> GetFormList(int clientId, int formTypeId, int? userId);
    }
}
