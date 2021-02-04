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
    public class CurrenciesService : ICurrenciesService
    {
        public List<SelectListItem> GetCurrencyDropdown()
        {
            ICurrenciesRepository _currencyRepo = new CurrenciesRepository();
            return _currencyRepo.GetAll().Where(x => !string.IsNullOrEmpty(x.currencyCode)).OrderBy(x => x.sequenceID).Select(x => new SelectListItem
            {
                Text = x.currencyCode,
                Value = x.currencyID.ToString()
            }).ToList();
        }
    }
}
