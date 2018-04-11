using System.Data.Entity.ModelConfiguration.Configuration;

namespace NFlex.Data.EF.Maps
{
    public interface IEntityMap
    {
        void AddTo(ConfigurationRegistrar registrar);
    }
}
