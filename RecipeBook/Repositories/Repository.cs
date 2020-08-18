using RecipeBook.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeBook.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class, new()
    {
        readonly IDataContext _context;
        readonly List<T> _items;

        protected Repository(IDataContext context)
        {
            _context = context;
            _items = _context.LoadFromFile<T>(typeof(T).Name + ".txt").ToList();
        }
        public void Save()
        {
            _context.SaveToFile(_items, typeof(T).Name + ".txt");
        }

        public T Add(T item)
        {
            _items.Add(item);
            return item;
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return _items.Where(predicate);
        }

        public abstract T Get(string id);

        public IEnumerable<T> GetAll()
        {
            return _items;
        }

        public void Remove(T item)
        {
            _items.Remove(item);
        }

        public T SingleOrDefault(Func<T, bool> predicate)
        {
            return _items.SingleOrDefault(predicate);
        }

    }
}
