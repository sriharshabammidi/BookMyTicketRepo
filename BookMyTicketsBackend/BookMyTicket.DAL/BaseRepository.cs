using BookMyTicket.DAL.Configurations;
using BookMyTicket.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookMyTicket.DAL
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {
        private BookMyTicketDBContext _context;
        private DbSet<T> table;

        public BaseRepository(BookMyTicketDBContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }
        public void Delete(object Id)
        {
            T record = table.Find(Id);
            table.Remove(record);
        }

        public List<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(object Id)
        {
            return table.Find(Id);
        }
        public List<T> GetBy(Func<T, bool> condition)
        {
            return table.Where(condition).ToList();
        }

        public T Insert(T obj)
        {
            table.Add(obj);
            this.Save();
            return obj;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public T Update(T obj)
        {
            table.Update(obj);
            //table.Attach(obj);
            //_context.Entry(obj).State = EntityState.Modified;
            this.Save();
            return obj;
        }

        public void UpdateMany(List<T> objs)
        {
            table.UpdateRange(objs);
        }

    }
}
