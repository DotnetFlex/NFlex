using NFlex.Ioc;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace NFlex.Data.EF
{
    public interface IDbContext: IPerLifetimeDependency
    {
        Guid TraceId { get; set; }
        Database Database { get;}

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        int SaveChanges();
    }
}
