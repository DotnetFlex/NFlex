using NFlex.Core;
using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace NFlex.Data.EF.Maps
{
    public abstract class EntityMap<TEntity, TKey> : EntityTypeConfiguration<TEntity>, IEntityMap where TEntity : class, IEntity
    {
        protected EntityMap()
        {
            MapTable();
            MapKey();
            MapVersion();
            MapProperties();
            MapAssociations();
        }

        /// <summary>
        /// 映射表名
        /// </summary>
        protected virtual void MapTable() { }
        protected virtual void MapKey() { }
        protected virtual void MapVersion() { }
        protected virtual void MapProperties() { }
        protected virtual void MapAssociations() { }

        public void AddTo(ConfigurationRegistrar registrar)
        {
            registrar.Add(this);
        }
    }

    public abstract class EntityMap<TEntity>:EntityMap<TEntity,Guid> where TEntity : class, IEntity
    { }
}
