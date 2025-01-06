using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping.abstractions;



public interface IMapper
{
    /// <summary>
    ///  Map<TSource, TDestination
    /// </summary>
    TDestination Map<TSource, TDestination>(TSource source);
}


public interface ICryptoMapper : IMapper { }

public interface IMapperFacade : IMapper { }
