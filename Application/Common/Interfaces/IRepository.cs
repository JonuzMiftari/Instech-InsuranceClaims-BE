namespace Application.Common.Interfaces;
public interface IRepository<T>
{
    Task<IEnumerable<T>> GetClaimsAsync();
    Task<T> GetByIdAsync(string id);
    Task<T> AddAsync(T entity);
    Task DeleteAsync(string id);
}
