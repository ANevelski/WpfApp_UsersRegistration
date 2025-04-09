using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WpfApp_UsersRegistration.DAL.DBconnect_AppContext;

namespace WpfApp_UsersRegistration.DAL
{
    public class EntityProvider<T>  where T : class
    {
        private App_Context _context;
        private static readonly object _lock = new object();

        public EntityProvider()
        {
            _context = new App_Context();
        }

        public List<T>GetAll()
        {
            InitializeContext();
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            InitializeContext();
            return _context.Set<T>().Find(id);
        }

        public  List<T> Find(Expression<Func<T, bool>> predicate)
        {
            InitializeContext();
            List<T> result = new List<T>();
            result = _context.Set<T>().Where(predicate).ToList();
           
            return result;
        }

        public Task Add(T entity)
        {
            InitializeContext();
            _context.Set<T>().Add(entity);
            return Task.CompletedTask;
        }

        public Task Update(T entity)
        {
            InitializeContext();
            _context.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public void Delete(int id)
        {
            InitializeContext();
            var entity = GetById(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                SaveChanges();
            }
        }
        public void SaveChanges()
        {
            InitializeContext();
            _context.SaveChanges();
        }

        private void InitializeContext()
        {
            lock (_lock)
            {
                if (_context == null)
                {
                    _context = new App_Context();
                }
            }
        }
    }
}
