namespace api.Services;

public interface IGenericCrudService<T>
{
    Task<IEnumerable<T>> Get();
    Task<T?> Get(int id);
    Task<T> Create(T entity);
    Task<T> Update(T entity);
    Task Delete(int id);
}
