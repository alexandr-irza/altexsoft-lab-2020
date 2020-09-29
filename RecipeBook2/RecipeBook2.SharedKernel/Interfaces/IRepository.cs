using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;

namespace RecipeBook2.SharedKernel
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetAsync(int? id);
        Task<List<T>> GetAllAsync();
        Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T item);
        Task UpdateAsync(T item);
        Task RemoveAsync(T item);
        void Add(T item);
        void Remove(T item);
        void Update(T item);
    }
}
