using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;

namespace WFJ.Repository.Interfaces
{
    public interface ICodesRepository : IRepository<Code>
    {
        List<Code> GetAllByType(string type);
        string GetStateByCode(string code);
    }
}
