using NFlex.Core;
using NFlex.Data.EF.Maps;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.IO;
using System.Linq;
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

            var maps = new List<IMap>();
            foreach(var ass in GetAssemblies())
            {
                maps.AddRange(Reflection.GetTypesByInterface<IMap>(ass));
            }
            foreach (IMap mapper in maps)
                mapper.AddTo(modelBuilder.Configurations);
        }

        protected virtual Assembly[] GetAssemblies()
        {
            return new[] { GetType().Assembly };
        }
    }
}
