using Application.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping;


public class MapperFacade : IMapperFacade
{
    ICryptoMapper cryptoMapper;

    public MapperFacade(ICryptoMapper cryptoMapper)
    {
        this.cryptoMapper = cryptoMapper;
    }



    public TDestination Map<TSource, TDestination>(TSource source) => cryptoMapper.Map<TSource, TDestination>(source);
}
