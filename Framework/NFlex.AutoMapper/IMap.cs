using AutoMapper;

namespace NFlex.AutoMapper
{
    public interface IMap
    {
        void Register(IMapperConfigurationExpression mapper);
    }
}
