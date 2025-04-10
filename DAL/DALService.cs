using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace WpfApp_UsersRegistration.DAL
{
    public class DALService<T> where T : class
    {
        private readonly IStorageProvider<T> _provider;

        public DALService(IStorageProvider<T> storageProvider)
        {
            _provider = storageProvider ?? throw new ArgumentNullException(nameof(storageProvider));
        }
                
        public async Task SaveToStorageAsync(T item)
        {
            await _provider.AddAsync(item);
            await _provider.SaveChangesAsync();
        }          

        public async Task DeleteFromStorageAsync(int id)
        {
            await _provider.DeleteAsync(id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _provider.GetAllAsync();
        }

        public async Task<bool> ExistsAsync(string login, string email = null, string password = null)
        {
            var parameter = Expression.Parameter(typeof(T), "entity");
            Expression combinedCondition = null;

            if (!string.IsNullOrEmpty(login))
            {
                var loginProperty = Expression.Property(parameter, "Login");
                var loginCondition = Expression.Equal(loginProperty, Expression.Constant(login));
                combinedCondition = combinedCondition == null
                    ? loginCondition
                    : Expression.AndAlso(combinedCondition, loginCondition);
            }

            if (!string.IsNullOrEmpty(email))
            {
                var emailProperty = Expression.Property(parameter, "Email");
                var emailCondition = Expression.Equal(emailProperty, Expression.Constant(email));
                combinedCondition = combinedCondition == null
                    ? emailCondition
                    : Expression.AndAlso(combinedCondition, emailCondition);
            }

            if (!string.IsNullOrEmpty(password))
            {
                var passwordProperty = Expression.Property(parameter, "Password");
                var passwordCondition = Expression.Equal(passwordProperty, Expression.Constant(password));
                combinedCondition = combinedCondition == null
                    ? passwordCondition
                    : Expression.AndAlso(combinedCondition, passwordCondition);
            }

            if (combinedCondition == null)
            {
                throw new ArgumentException("At least one parameter (login, email, password) must be provided.");
            }

            var predicate = Expression.Lambda<Func<T, bool>>(combinedCondition, parameter);

            var exists = await _provider.FindAsync(predicate);
            return exists.Any();
        }
    }
}
