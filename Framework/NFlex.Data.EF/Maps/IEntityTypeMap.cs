using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.Data.EF.Maps
{
    public interface IEntityTypeMap
    {
        void AddTo(ConfigurationRegistrar registrar);
    }
}
