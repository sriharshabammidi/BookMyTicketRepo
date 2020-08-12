using System;
using System.Collections.Generic;

namespace BookMyTicket.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : new()
    {
        List<T> GetAll();
        T GetById(object Id);
        T Insert(T obj);
        T Update(T obj);
        void Delete(Object Id);
        void UpdateMany(List<T> objs);
    }
}
