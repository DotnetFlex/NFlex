using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFlex.AutoMapper
{
    public interface IMap
    {
        void Register(IMapperConfigurationExpression mapper);
    }
}
