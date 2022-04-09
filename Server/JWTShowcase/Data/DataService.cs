namespace JWTShowcase.Data;

using Microsoft.EntityFrameworkCore;

public abstract class DataService<TEntity> : IDataService<TEntity>
    where TEntity : class
{
    protected DataService(DbContext db) => Data = db;

    protected DbContext Data { get; }

    protected IQueryable<TEntity> All() => Data.Set<TEntity>();

    public async Task Save(TEntity entity)
    {
        Data.Update(entity);

        await Data.SaveChangesAsync();
    }
}