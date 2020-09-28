using Microsoft.EntityFrameworkCore;
using RecipeBook2.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook2.Infrastructure.Data
{
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity 
    {
        internal RecipeBookContext context;
        public BaseRepository(RecipeBookContext context)
        {
            this.context = context;
        }

        public void Add(T item)
        {
            context.Set<T>().Add(item);
        }

        public async Task AddAsync(T item)
        {
            await context.Set<T>().AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task<List<T>> FindAsync(Func<T, bool> predicate)
        {
            //       return await context.Set<T>().Where(predicate).ToListAsync();
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAllAsync() 
        {
            return context.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(int? id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public void Remove(T item)
        {
            context.Set<T>().Remove(item);
        }

        public Task RemoveAsync(T item) 
        {
            context.Set<T>().Remove(item);
            return context.SaveChangesAsync();
        }

        public async Task<T> SingleOrDefaultAsync(Func<T, bool> predicate)
        {
            throw new NotImplementedException();
            //       return await context.Set<T>().SingleOrDefaultAsync(predicate);
        }

        public void Update(T item)
        {
            context.Set<T>().Update(item);
        }

        public async Task UpdateAsync(T item)
        {
            context.Set<T>().Update(item);
            await context.SaveChangesAsync();
        }

    }
}
