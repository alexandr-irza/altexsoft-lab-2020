using System;
using System.Collections.Generic;

namespace RecipeBook.Repositories
{
    public interface IRepository<T> where T : class
    {
        T Get(string id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Func<T, bool> predicate);
        T SingleOrDefault(Func<T, bool> predicate);
        T Add(T item);
        void Remove(T item);
        void Save();
    }
}
