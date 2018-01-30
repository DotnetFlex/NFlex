using NFlex.Data.EF.Maps;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Reflection;

namespace NFlex.Data.EF
{
    public abstract class DbContextBase:DbContext,IDbContext
    {
        public DbContextBase(string dbName):base(dbName)
        {
            TraceId = Guid.NewGuid();
        }
        
        public Guid TraceId { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            var maps = new List<IEntityTypeMap>();
            foreach(var ass in GetAssemblies())
            {
                maps.AddRange(Reflection.GetTypesByInterface<IEntityTypeMap>(ass));
            }
            foreach (IEntityTypeMap mapper in maps)
                mapper.AddTo(modelBuilder.Configurations);
        }

        protected virtual Assembly[] GetAssemblies()
        {
            return new[] { GetType().Assembly };
        }
    }
}
