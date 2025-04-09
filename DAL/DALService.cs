
using System.Collections.Generic;
using System.Linq;

namespace WpfApp_UsersRegistration.DAL
{
    
    public static class DALService<T> where T : class
    {   
        private static EntityProvider<T> _db;
             
        private static readonly object _lock = new object();

        public static void SaveToStorage(T item)
        {
            InitializeDb();
                       
            _db.Add(item);
            _db.SaveChanges();
        }

        public static void DeleteFromStorage(int id)
        {
            InitializeDb();                      
            _db.Delete(id);
        }

        public static EntityProvider<T> GetStorageProvider()
        {
            InitializeDb();        
            return _db;
        }

        public static List<T> GetAll()
        {
            InitializeDb();       
            return _db.GetAll(); 
        }


        private static void InitializeDb()
        {
            lock (_lock)
            {
                if (_db == null)
                {
                    _db = new EntityProvider<T>();
                }
            }
        }
    }
}
