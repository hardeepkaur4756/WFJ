using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Models;
using WFJ.Repository;
using WFJ.Repository.Interfaces;

namespace WFJ.Service.Interfaces
{
    public class LocalCounselService : ILocalCounselService
    {
        IAssociateCounselRepository _associateCouncelRepo = new AssociateCounselRepository();
        private IErrorLogService _errorLogService = new ErrorLogService();
        public ManageLocalCounselModel GetLocalCounsels(string firmName, string attorneyName, string city, string state, string country, DataTablesParam param, string sortDir, string sortCol, int pageNo)
        {
            ManageLocalCounselModel model = new ManageLocalCounselModel();
            var counsels = _associateCouncelRepo.GetAssociateCounselList(firmName, attorneyName, city, state, country);

            model.totalLocalCounselsCount = counsels?.Count();


            if (counsels != null)
            {


                var list1 = counsels.Select(x => new LocalCounselModel
                {
                    Id = x.FirmID,
                    FirmName = x.FirmName,
                    //no column in local DB need to check live DB
                    //ContactName = x.c
                    //AttorneyName
                    City = x.City,
                    State = x.State,
                    Country = x.Country
                }).AsEnumerable();

                switch (sortCol)
                {
                    case "FirmName":
                        if (sortDir == "asc")
                        {
                            list1 = list1.OrderBy(x => x.FirmName).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            list1 = list1.OrderByDescending(x => x.FirmName).ToList();
                        }
                        break;
                    case "ContactName":
                        if (sortDir == "asc")
                        {
                            list1 = list1.OrderBy(x => x.ContactName).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            list1 = list1.OrderByDescending(x => x.ContactName).ToList();
                        }
                        break;
                    case "AttorneyName":
                        if (sortDir == "asc")
                        {
                            list1 = list1.OrderBy(x => x.AttorneyName).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            list1 = list1.OrderByDescending(x => x.AttorneyName).ToList();
                        }
                        break;
                    case "City":
                        if (sortDir == "asc")
                        {
                            list1 = list1.OrderBy(x => x.City).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            list1 = list1.OrderByDescending(x => x.City).ToList();
                        }
                        break;
                    case "State":
                        if (sortDir == "asc")
                        {
                            list1 = list1.OrderBy(x => x.State).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            list1 = list1.OrderByDescending(x => x.State).ToList();
                        }
                        break;
                    case "Country":
                        if (sortDir == "asc")
                        {
                            list1 = list1.OrderBy(x => x.Country).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            list1 = list1.OrderByDescending(x => x.Country).ToList();
                        }
                        break;
                    default:
                        break;
                }

                model.localCounsels = list1.Skip((pageNo - 1) * param.iDisplayLength).Take(param.iDisplayLength).ToList();
            }
            else
            {
                model.localCounsels = new List<LocalCounselModel>();
            }


            return model;
        }
    }
}
