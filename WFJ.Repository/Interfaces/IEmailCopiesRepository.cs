using System.Collections.Generic;
using WFJ.Repository.EntityModel;

namespace WFJ.Repository.Interfaces
{
    public interface IEmailCopiesRepository : IRepository<EMailCopy>
    {
        List<User> GetUsers();
    }
}
