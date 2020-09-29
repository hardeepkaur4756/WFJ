using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;


namespace WFJ.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        public readonly WFJEntities _context;
        public DbSet<T> _table;
        public GenericRepository()
        {
            _context = new WFJEntities();
            _table = _context.Set<T>();
        }

        public T Add(T record)
        {
            _table.Add(record);
            _context.SaveChanges();
            return record;
        }

        public void AddList(IEnumerable<T> records)
        {
            _table.AddRange(records);
            _context.SaveChanges();
        }

        public T Update(T record)
        {
            _table.AddOrUpdate(record);
            if (_context.Entry(record).State == EntityState.Detached)
            {
                _context.Entry(record).State = EntityState.Modified;
                _table.Attach(record);
            }
            _context.SaveChanges();
            return record;
        }

        public void Remove(object key)
        {
            T record = _table.Find(key);
            _table.Remove(record);
            _context.SaveChanges();
        }

        public void RemoveList(IEnumerable<T> records)
        {
            _table.RemoveRange(records);
            _context.SaveChanges();
        }


        public T GetById(object key)
        {
            return _table.Find(key);
        }

        public IEnumerable<T> GetAll()
        {
            return _table;
        }
    }

}
