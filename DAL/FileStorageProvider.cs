using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Linq.Expressions;

namespace WpfApp_UsersRegistration.DAL
{
    public class FileStorageProvider<T> : IStorageProvider<T> where T : class
    {
        private readonly string _filePath;

        public FileStorageProvider(string filePath)
        {
            _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            EnsureFileExists();
        }

        private void EnsureFileExists()
        {
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
        }

        public Task<List<T>> GetAllAsync()
        {
            var data = File.ReadAllText(_filePath); 
            var entities = JsonSerializer.Deserialize<List<T>>(data);
            return Task.FromResult(entities ?? new List<T>());
        }

        public Task<T> GetByIdAsync(int id)
        {
            var entities = GetAllAsync().Result;

            var entity = entities.FirstOrDefault(e =>
            {
                var idProperty = e.GetType().GetProperty("id");
                return idProperty != null && (int)idProperty.GetValue(e) == id;
            });

            return Task.FromResult(entity);
        }

        public Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            var entities = GetAllAsync().Result;
            return Task.FromResult(entities.AsQueryable().Where(predicate).ToList());
        }

        public Task AddAsync(T entity)
        {
            var entities = GetAllAsync().Result;
            entities.Add(entity);
            SaveToFile(entities); 
            return Task.CompletedTask;
        }

        public Task UpdateAsync(T entity)
        {
            var entities = GetAllAsync().Result;

            var idProperty = entity.GetType().GetProperty("id");
            if (idProperty == null)
            {
                throw new InvalidOperationException("Entity must have an 'id' property to update.");
            }

            var idValue = (int)idProperty.GetValue(entity);

            var existingEntity = entities.FirstOrDefault(e =>
            {
                var id = e.GetType().GetProperty("id")?.GetValue(e);
                return id != null && (int)id == idValue;
            });

            if (existingEntity != null)
            {
                entities.Remove(existingEntity);
                entities.Add(entity);
                SaveToFile(entities);
            }
            else
            {
                throw new KeyNotFoundException($"Entity with ID {idValue} was not found.");
            }

            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var entities = GetAllAsync().Result;

            var entityToDelete = entities.FirstOrDefault(e =>
            {
                var idProperty = e.GetType().GetProperty("id");
                return idProperty != null && (int)idProperty.GetValue(e) == id;
            });

            if (entityToDelete != null)
            {
                entities.Remove(entityToDelete);
                SaveToFile(entities); 
            }
            else
            {
                throw new KeyNotFoundException($"Entity with ID {id} was not found.");
            }

            return Task.CompletedTask;
        }

        public void SaveToFile(List<T> entities)
        {
            var data = JsonSerializer.Serialize(entities, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(_filePath, data);
        }

        public Task SaveChangesAsync()
        {
            return Task.CompletedTask;
        }       
    }
}
