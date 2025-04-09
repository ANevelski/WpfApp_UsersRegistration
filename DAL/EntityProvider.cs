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
        private readonly App_Context _context;

        public EntityProvider()
        {
            _context = new App_Context();
        }

        public List<T>GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public  List<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).ToList();
        }

        public Task Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return Task.CompletedTask;
        }

        public Task Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
            }
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
