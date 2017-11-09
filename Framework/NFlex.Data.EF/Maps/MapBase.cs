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
    public abstract class MapBase<TEntity, TKey> : EntityTypeConfiguration<TEntity>, IMap where TEntity : class, IEntity
    {
        protected MapBase()
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

    public abstract class MapBase<TEntity>:MapBase<TEntity,Guid> where TEntity : class, IEntity
    { }
}
