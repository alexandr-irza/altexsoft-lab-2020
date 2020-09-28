using Microsoft.EntityFrameworkCore;
using RecipeBook2.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RecipeBook2.Infrastructure.Data
{
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity 
    {
        private RecipeBookContext _context;
        public BaseRepository(RecipeBookContext context)
        {
            _context = context;
        }

        public void Add(T item)
        {
            _context.Set<T>().Add(item);
        }

        public async Task AddAsync(T item)
        {
            await _context.Set<T>().AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public Task<List<T>> GetAllAsync() 
        {
            return _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(int? id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public void Remove(T item)
        {
            _context.Set<T>().Remove(item);
        }

        public Task RemoveAsync(T item) 
        {
            _context.Set<T>().Remove(item);
            return _context.SaveChangesAsync();
        }

        public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(predicate);
        }

        public void Update(T item)
        {
            _context.Set<T>().Update(item);
        }

        public async Task UpdateAsync(T item)
        {
            _context.Set<T>().Update(item);
            await _context.SaveChangesAsync();
        }

    }
}
