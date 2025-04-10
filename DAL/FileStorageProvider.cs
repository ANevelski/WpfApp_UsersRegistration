
//******************* FOr example of different storage ****************************


//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq.Expressions;
//using System.Threading.Tasks;

//namespace WpfApp_UsersRegistration.DAL
//{
//    public class FileStorageProvider<T> : IStorageProvider<T> where T : class
//    {
//        private readonly string _filePath;

//        public FileStorageProvider(string filePath)
//        {
//            _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
//        }

//        public Task<List<T>> GetAllAsync()
//        {
//            if (!File.Exists(_filePath))
//                return Task.FromResult(new List<T>());

//            var data = File.ReadAllText(_filePath);
//            var entities = JsonSerializer.Deserialize<List<T>>(data);
//            return Task.FromResult(entities ?? new List<T>());
//        }

//        public Task<T> GetByIdAsync(int id)
//        {
//            throw new NotImplementedException(); // Можно реализовать для файлов, используя поиск по ключу
//        }

//        public Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
//        {
//            throw new NotImplementedException(); // Реализация сложных фильтров для файлов может быть специфичной
//        }

//        public async Task AddAsync(T entity)
//        {
//            var entities = await GetAllAsync();
//            entities.Add(entity);
//            await SaveToFileAsync(entities);
//        }

//        public async Task UpdateAsync(T entity)
//        {
//            throw new NotImplementedException(); // Реализация для обновления данных в файле
//        }

//        public async Task DeleteAsync(int id)
//        {
//            throw new NotImplementedException(); // Реализация для удаления данных
//        }

//        public async Task SaveChangesAsync()
//        {
//            // Метод необязателен для файлов — изменения сохраняются сразу
//        }

//        private async Task SaveToFileAsync(List<T> entities)
//        {
//            var data = JsonSerializer.Serialize(entities);
//            await File.WriteAllTextAsync(_filePath, data);
//        }
//    }
//}
