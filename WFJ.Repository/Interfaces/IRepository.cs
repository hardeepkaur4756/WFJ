using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFJ.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T GetById(object key);
        IEnumerable<T> GetAll();
        T Add(T record);
        void AddList(IEnumerable<T> records);
        T Update(T record);
        void Remove(object key);
        void RemoveList(IEnumerable<T> records);
    }
}
