using NFlex.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace NFlex.Data.EF.Maps
{
    public abstract class EntityTypeMap<TEntity, TKey> : EntityTypeConfiguration<TEntity>, IEntityTypeMap where TEntity : class, IEntity
    {
        protected EntityTypeMap()
        {
            MapTable();
            MapKey();
            MapVersion();
            MapProperties();
            MapAssociations();
        }

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

    public abstract class EntityTypeMap<TEntity>:EntityTypeMap<TEntity,Guid> where TEntity : class, IEntity
    { }
}
