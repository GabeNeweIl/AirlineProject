using System;
using System.Collections.Generic;

namespace Airline.DAL.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Create(TEntity item);
        IEnumerable<TEntity> GetAll();
        TEntity Get(int id);
        void Delete(TEntity item);
        void Update(TEntity item);
        int GetCount();
        void Save();
    }
}
