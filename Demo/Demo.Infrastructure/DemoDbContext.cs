using Demo.Domain.Models;
using NFlex.Data.EF;
using NFlex.Ioc;
using System.Data.Entity;

namespace Demo.Infrastructure
{
    public class DemoDbContext:DbContextBase
    {
        public DemoDbContext() : base("NFlexDemoDB") { }

        public DbSet<UserInfo> UserInfo { get; set; }
    }
}
