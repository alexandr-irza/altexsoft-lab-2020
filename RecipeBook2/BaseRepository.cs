using Microsoft.EntityFrameworkCore;
using RecipeBook2.Infrastructure.Data;
using RecipeBook2.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook2.Infrastructure.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        internal RecipeBookContext context;
        protected BaseRepository(RecipeBookContext context)
        {
            this.context = context;
        }

        public async Task<T> AddAsync<T>(T item) where T : BaseEntity
        {
            await context.Set<T>().AddAsync(item);
            return item;
        }

        public Task<List<T>> FindAsync<T>(Func<T, bool> predicate) where T : BaseEntity
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAllAsync<T>() where T : BaseEntity
        {
            return context.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync<T>(int? id) where T : BaseEntity
        {
            return await context.Set<T>().FindAsync(id);
        }

        public Task RemoveAsync<T>(T item) where T : BaseEntity
        {
            context.Set<T>().Remove(item);
            return context.SaveChangesAsync();
        }

        public Task<T> SingleOrDefault<T>(Func<T, bool> predicate) where T : BaseEntity
        {
            throw new NotImplementedException();
        }


        public Task UpdateAsync<T>(T item) where T : BaseEntity
        {
            context.Set<T>().Update(item);
            return context.SaveChangesAsync();
        }
    }
}
