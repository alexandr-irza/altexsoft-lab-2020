using RecipeBook.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeBook.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : class, new()
    {
        private IDataContext _context;
        private List<T> Items { get; set; }

        public Repository(IDataContext context)
        {
            _context = context;
            Items = _context.LoadFromFile<T>(typeof(T).Name + ".txt").ToList();
        }

        public T Add(T item)
        {
            Items.Add(item);
            return item;
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return Items.Where(predicate);
        }

        public abstract T Get(string id);

        public IEnumerable<T> GetAll()
        {
            return Items;
        }

        public void Remove(T item)
        {
            Items.Remove(item);
        }

        public T SingleOrDefault(Func<T, bool> predicate)
        {
            return Items.SingleOrDefault(predicate);
        }

        public void Save()
        {
            _context.SaveToFile(Items, typeof(T).Name + ".txt");
        }
    }
}
