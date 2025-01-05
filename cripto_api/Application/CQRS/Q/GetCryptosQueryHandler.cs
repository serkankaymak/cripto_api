using Application.Dtos;
using Application.Mapping;
using Application.Mappings;
using Application.Repositories;
using Application.Services.ExternalServices;
using Application.Services.InternalServices;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Q;

public sealed class GetCryptosQueryHandler : ARequestHandler<GetCryptosQuery, List<CryptoDto>>
{
    IMapperFacade _mapper;
    ICriptoService _dataService;

    public GetCryptosQueryHandler(ICriptoService dataService, IMapperFacade mapper)
    {
        _dataService = dataService;
        _mapper = mapper;
    }


    protected override async Task<List<CryptoDto>> HandleRequestAsync(GetCryptosQuery request)
    {
        var entities = await _dataService.GetCryptosWithTickers(request.dateTime);
        var dtos = entities.Select(x => _mapper.Map<Crypto, CryptoDto>(x)).ToList();
        return dtos;
    }
}
