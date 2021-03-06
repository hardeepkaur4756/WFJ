﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;

namespace WFJ.Repository
{
   public class AssociateCounselRepository : GenericRepository<AssociateCounsel>, IAssociateCounselRepository
    {
        private readonly WFJEntities _context;
        public AssociateCounselRepository(WFJEntities context)
        {
            _context = context;
        }
        public AssociateCounselRepository()
        {
            _context = new WFJEntities();
            _context.Configuration.LazyLoadingEnabled = true;
            _context.Configuration.ProxyCreationEnabled = true;
        }

        public IEnumerable<AssociateCounsel> GetAssociateCounselList(string firmName, string attorneyName, string contactName, string city, string state, string country, int wfjAttorneyId)
        {
            IEnumerable<AssociateCounsel> counsels;
            counsels = _context.AssociateCounsels.Where(a => !string.IsNullOrEmpty(a.FirmName));
            if (!string.IsNullOrEmpty(firmName))
            {
                counsels = counsels.Where(x => x.FirmName != null && x.FirmName.Contains(firmName));
            }
            //this column is not in local DB need to check live DB
            if (!string.IsNullOrEmpty(attorneyName))
            {
                counsels = counsels.Where(x => x?.PersonnelRequests?.FirstOrDefault()?.AssociateName == attorneyName);
            }
            if (!string.IsNullOrEmpty(contactName))
            {
                counsels = counsels.Where(x => x.Name != null && x.Name.Contains(contactName));
            }
            if (!string.IsNullOrEmpty(city))
            {
                counsels = counsels.Where(x => x.City != null && x.City.Contains(city));
            }
            if (!string.IsNullOrEmpty(state))
            {
                counsels = counsels.Where(x => x.State != null && x.State.Contains(state));
            }
            if (!string.IsNullOrEmpty(country))
            {
                counsels = counsels.Where(x => x.Country != null && x.Country == country);
            }
            if (wfjAttorneyId > 0)
            {
                counsels = counsels.Where(x => x.PersonnelRequests?.FirstOrDefault()?.Request?.AssignedAttorney == wfjAttorneyId);
            }
            return counsels;
        }

        public AssociateCounsel GetAssociateCounselDetailByID(int associateCounselId)
        {
            return _context.AssociateCounsels.FirstOrDefault(x => x.FirmID == associateCounselId);
        }
    }
}
