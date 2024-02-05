using Dapper.Contrib.Extensions;
using CleanArchitecture.Domain.Interfaces.Configuration;
using CleanArchitecture.Domain.Interfaces.Data;
using Microsoft.Data.SqlClient;

namespace CleanArchitecture.Infrastructure.Data
{
    public class GenericRepositoryDapperAdapter<T, TKey> : IRepository<T, TKey>
        where T : class
    {
        protected readonly SqlConnection readerConnection;
        protected readonly SqlConnection writerConnection;

        public GenericRepositoryDapperAdapter(IConnectionStrings connectionStrings)
        {
            readerConnection = new SqlConnection(connectionStrings.ReaderConnection);
            writerConnection = new SqlConnection(connectionStrings.WriterConnection);
        }

        public virtual async Task<long> AddAsync(T entity)
        {
            // returns new item id or 1 if id type is not int
            return await writerConnection.InsertAsync<T>(entity);
        }

        public virtual async Task AddManyAsync(IEnumerable<T> entities)
        {
            await writerConnection.InsertAsync<IEnumerable<T>>(entities);
        }

        public virtual async Task DeleteAsync(T entity)
        {
            await writerConnection.DeleteAsync<T>(entity);
        }

        public virtual async Task DeleteManyAsync(IEnumerable<T> entities)
        {
            await writerConnection.DeleteAsync<IEnumerable<T>>(entities);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await readerConnection.GetAllAsync<T>();
        }

        public virtual async Task<T> GetByIdAsync(TKey id)
        {
            return await readerConnection.GetAsync<T>(id);
        }

        public virtual async Task UpdateAsync(T entity)
        {
            await writerConnection.UpdateAsync<T>(entity);
        }

        public virtual async Task UpdateManyAsync(IEnumerable<T> entities)
        {
            await writerConnection.UpdateAsync<IEnumerable<T>>(entities);
        }
    }
}
