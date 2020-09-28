using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeBook2.SharedKernel
{
    public interface IRepository 
    {
        Task<T> GetAsync<T>(int? id) where T : BaseEntity;
        Task<List<T>> GetAllAsync<T>() where T : BaseEntity;
        Task<List<T>> FindAsync<T>(Func<T, bool> predicate) where T : BaseEntity;
        Task<T> SingleOrDefault<T>(Func<T, bool> predicate) where T : BaseEntity;
        Task<T> AddAsync<T>(T item) where T : BaseEntity;
        Task UpdateAsync<T>(T item) where T : BaseEntity;
        Task RemoveAsync<T>(T item) where T : BaseEntity;
    }
}
