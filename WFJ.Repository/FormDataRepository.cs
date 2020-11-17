﻿using System.Collections.Generic;
using System.Linq;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;

namespace WFJ.Repository
{
    public class FormDataRepository : GenericRepository<FormData>, IFormDataRepository
    {
        private readonly WFJEntities _context;

        public FormDataRepository()
        {
            _context = new WFJEntities();
        }

        public IEnumerable<FormData> GetByRequestId(int requestId)
        {
            return _context.FormDatas.Where(x => x.RequestID == requestId).ToList();
        }
        public void Delete(List<FormData> FormDatas)
        {
            _context.FormDatas.RemoveRange(FormDatas);
            _context.SaveChanges();
        }
    }
}
