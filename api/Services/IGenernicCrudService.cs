namespace api.Services;

public interface IGenericCrudService<T>
{
    // IEnumerable<T> Get();
    T? Get(int id);
    T Create(T entity);
    T Update(T entity);
    void Delete(int id);
}
