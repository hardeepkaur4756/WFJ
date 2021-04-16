using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Models;

namespace WFJ.Service.Interfaces
{
    public interface ILocalCounselService
    {
        ManageLocalCounselModel GetLocalCounsels(string firmName, string attorneyName, string contactName, string city, string state, string country, DataTablesParam param, string sortDir, string sortCol, int pageNo);
        int SaveLocalCounsel(AddLocalCounselViewModel addLocalCounselViewModel);
        AddLocalCounselViewModel GetById(int firmId);
        List<FileInformation> GetFileInformation(int firmId);
        void DeleteAssociateCounse(int firmId);
        AssociateCounselModel GetByFirmId(int firmId);
    }
}
