using System.Data.Entity.ModelConfiguration.Configuration;

namespace NFlex.Data.EF.Maps
{
    public interface IEntityTypeMap
    {
        void AddTo(ConfigurationRegistrar registrar);
    }
}
