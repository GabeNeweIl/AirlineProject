using System;
using System.Collections.Generic;
using System.Linq;
using Airline.DAL.EF;
using System.Data.Entity;

namespace Airline.DAL.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        AirlineDbContext context;
        DbSet<TEntity> dbSet;

        public Repository(AirlineDbContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbSet.AsNoTracking().ToList();
        }

        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return dbSet.AsNoTracking().Where(predicate).ToList();
        }
        public TEntity FindById(int id)
        {
            return dbSet.Find(id);
        }

        public void Create(TEntity item)
        {
            dbSet.Add(item);
        }
        public void Update(TEntity item)
        {
            context.Entry(item).State = EntityState.Modified;
        }
        public int GetCount()
        {
            return dbSet.Count();
        }
        public void Delete(TEntity item)
        {
            dbSet.Remove(item);
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
