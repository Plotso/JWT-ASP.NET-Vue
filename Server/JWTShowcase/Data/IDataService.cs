namespace JWTShowcase.Data;

public interface IDataService<in TEntity>
    where TEntity : class
{
    Task Save(TEntity entity);
}