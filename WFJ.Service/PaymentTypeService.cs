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
   public class PaymentTypesService :IPaymentTypeService
    {
        public List<SelectListItem> GetPaymentTypeDropdown()
        {
            IPaymentTypesRepository _paymentTypeRepo = new PaymentTypesRepository();
            return _paymentTypeRepo.GetAll().Where(x => x.active == 1 && !string.IsNullOrEmpty(x.PaymentTypeDesc)).Select(x => new SelectListItem
            {
                Text = x.PaymentTypeDesc,
                Value = x.ID.ToString()
            }).ToList();
        }
    }
}
