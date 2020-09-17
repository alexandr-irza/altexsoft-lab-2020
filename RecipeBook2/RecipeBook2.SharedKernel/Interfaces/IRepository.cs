using System;
using System.Collections.Generic;

namespace RecipeBook2.SharedKernel
{
    public interface IRepository<T> where T : class
    {
        T Get(int? id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Func<T, bool> predicate);
        T SingleOrDefault(Func<T, bool> predicate);
        void Add(T item);
        void Update(T item);
        void Remove(T item);
        void Save();
    }
}
