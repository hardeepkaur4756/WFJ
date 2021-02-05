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

        ICurrenciesRepository _currencyRepo = new CurrenciesRepository();
        public List<SelectListItem> GetCurrencyDropdown()
        {
            return _currencyRepo.GetAll().Where(x => !string.IsNullOrEmpty(x.currencyCode)).OrderBy(x => x.sequenceID).Select(x => new SelectListItem
            {
                Text = x.currencyCode,
                Value = x.currencyID.ToString()
            }).ToList();
        }
        public int? GetDefaultCurrencyId(string currecyCode) {
            return _currencyRepo.GetAll().Where(x => x.currencyCode != null && x.currencyCode.ToUpper() == currecyCode.ToUpper())?.FirstOrDefault()?.currencyID;
        }
        public string GetCurrencyById(int currencyId)
        {
            return _currencyRepo.GetById(currencyId)?.currencyCode;
        }
    }
}
