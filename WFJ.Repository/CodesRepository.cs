﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;

namespace WFJ.Repository
{
    public class CodesRepository : GenericRepository<Code>, ICodesRepository
    {
        private WFJEntities context;
        public CodesRepository()
        {
            context = new WFJEntities();
        }
        public List<Code> GetAllByType(string type)
        {
            return context.Codes.Where(x => x.Type.ToLower() == type.ToLower()).ToList();
        }

        public string GetStateByCode(string code)
        {
            return context.Codes?.FirstOrDefault(x => x.Code1 == code && x.Type == "STATE")?.Value;
        }
        public Code GetById(int id)
        {
            return context.Codes?.FirstOrDefault(x => x.ID == id);
        }

        public List<Code> GetStateandProvince()
        {
            return context.Codes?.Where(x => x.Type == "PROVINCE" || x.Type == "STATE")?.OrderByDescending(x=>x.Type).ThenBy(x=>x.Code1).ToList();
        }
    }
}
