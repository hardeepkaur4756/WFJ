using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WFJ.Repository;
using WFJ.Repository.Interfaces;
using WFJ.Service.Interfaces;

namespace WFJ.Service
{
    public class PersonnelService : IPersonnelService
    {
        private IPersonnelsRepository _personnelsRepository = new PersonnelsRepository();
        public List<SelectListItem> GetPersonnelsDropdown()
        {
            return _personnelsRepository.GetAll().Where(x => x.FirstName != null).Select(x => new SelectListItem { 
            Text = x.FirstName + " " + x.LastName,
            Value = x.ID.ToString()
            }).OrderBy(x => x.Text).Prepend(new SelectListItem { Text = "Select", Value = "-1" }).ToList();
        }
    }
}
