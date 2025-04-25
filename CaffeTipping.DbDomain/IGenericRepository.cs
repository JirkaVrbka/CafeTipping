using System.Linq.Expressions;

namespace CaffeTipping.DbDomain;

public interface IGenericRepository<T> where T : class
{
    T? Get(Guid id);
    T? Get(Expression<Func<T, bool>> expression);
    ValueTask<T?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<T?> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
    List<T> GetAll();
    List<T> GetAll(Expression<Func<T, bool>> expression);
    void Add(T entity);
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    void AddRange(IEnumerable<T> entities);
    Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    void Remove(T entity);
    Task RemoveAsync(T entity);
    void RemoveRange(IEnumerable<T> entities);
    void Update(T entity);
    Task UpdateAsync(T entity);
    void UpdateRange(IEnumerable<T> entities);
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression,
        CancellationToken cancellationToken = default);
}