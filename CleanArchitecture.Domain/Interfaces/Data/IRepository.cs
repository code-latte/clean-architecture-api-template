namespace CleanArchitecture.Domain.Interfaces.Data
{
    public interface IRepository<T, TKey>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(TKey id);
        Task<long> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task AddManyAsync(IEnumerable<T> entities);
        Task UpdateManyAsync(IEnumerable<T> entities);
        Task DeleteManyAsync(IEnumerable<T> entities);
    }
}
