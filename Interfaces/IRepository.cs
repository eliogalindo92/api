namespace api.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    Task <Boolean> CreateAsync(TEntity entity);
    Task<List<TEntity>> FindAllAsync();
    Task<TEntity?> FindByIdAsync(int id);
    Task <Boolean> UpdateAsync(TEntity entity);
    Task <Boolean> DeleteAsync(int id);
}