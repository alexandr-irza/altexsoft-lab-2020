using Microsoft.EntityFrameworkCore;
using RecipeBook2.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeBook2.Infrastructure.Data
{
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity, new()
    {
        internal RecipeBookContext context;
        internal DbSet<T> dbSet;
        protected BaseRepository(RecipeBookContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public void Add(T item)
        {
            dbSet.Add(item);
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return dbSet.Where(predicate);
        }

        virtual public T Get(int? id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public void Remove(T item)
        {
            dbSet.Remove(item);
        }

        public T SingleOrDefault(Func<T, bool> predicate)
        {
            return dbSet.SingleOrDefault(predicate);
        }

        public void Update(T item)
        {
            dbSet.Update(item);
        }
    }
}
