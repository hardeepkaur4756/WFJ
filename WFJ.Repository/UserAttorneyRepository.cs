using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;

namespace WFJ.Repository
{
  public  class UserAttorneyRepository : GenericRepository<UserAttorney>,IUserAttorneyRepository
    {
        public int? GetPersonnelByUserID(int UserID)
        {
            return (_context.UserAttorneys.FirstOrDefault(x => x.UserID == UserID))?.PersonnelID;
        }
    }
}
