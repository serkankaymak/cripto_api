using Application.Mappings;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping;


public class CryptoMapper : ICryptoMapper
{
    AutoMapper.IMapper mapper;

    public CryptoMapper(AutoMapper.IMapper mapper)
    {
        this.mapper = mapper;
    }


    public TDestination Map<TSource, TDestination>(TSource source) => this.mapper.Map<TDestination>(source);

}

