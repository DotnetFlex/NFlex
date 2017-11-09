using Demo.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Demo.Web
{
    public class DatabaseConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(new DatabaseInitializeStrategy());
        }
    }
}