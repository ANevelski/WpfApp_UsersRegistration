using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WpfApp_UsersRegistration.DAL.DBconnect_AppContext;

namespace WpfApp_UsersRegistration.DAL
{
    public class EntityProvider<T> : IStorageProvider<T>, IDisposable where T : class
    {
        private readonly App_Context _context;

        public EntityProvider()
        {
            _context = new App_Context();
        }  

        public async Task<List<T>> GetAllAsync()
        {
            return await Task.FromResult(_context.Set<T>().ToList());
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await Task.FromResult(_context.Set<T>().Find(id));
        }

        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await SaveChangesAsync();
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
