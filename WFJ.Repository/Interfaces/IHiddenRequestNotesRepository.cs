﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;

namespace WFJ.Repository.Interfaces
{
    public interface IHiddenRequestNotesRepository : IRepository<hiddenRequestNote>
    {
        IEnumerable<hiddenRequestNote> GetHiddenNotesByUserAndRequestId(int userId, int requestId);
    }
}
