using System;
using System.Collections.Generic;

namespace Airline.DAL.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Create(TEntity item);
        TEntity FindById(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
        void Delete(TEntity item);
        void Update(TEntity item);
        int GetCount();
        void Save();
    }
}
